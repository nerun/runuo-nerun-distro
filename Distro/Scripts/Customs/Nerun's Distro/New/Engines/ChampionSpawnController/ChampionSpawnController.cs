using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Engines.CannedEvil;

namespace Server.Engines.CannedEvil
{
	public class ChampionSpawnController : Item
	{
		bool m_Active;

		private ArrayList m_AllSpawn;
		private ArrayList m_DungeonsSpawn;
		private ArrayList m_LostLandsSpawn;
		private ArrayList m_IlshenarSpawn;
		private ArrayList m_TokunoSpawn;
		private ArrayList m_MalasSpawn;

		private TimeSpan m_ExpireDelay;
		private TimeSpan m_RestartDelay;

		private TimeSpan m_RandomizeDelay;

		private int m_ActiveAltars;

		private RandomizeTimer m_Timer;

		public struct SpawnRecord
		{
			public int type,x,y,z;

			public SpawnRecord( int type, int x, int y, int z )			
			{
				this.type = type;
				this.x = x;
				this.y = y;
				this.z = z;
			}
		}

		private SpawnRecord[] m_Dungeons = new SpawnRecord[]
		{
			new SpawnRecord( (int)ChampionSpawnType.UnholyTerror, 5178, 708, 20 ), //Deceit
			new SpawnRecord( (int)ChampionSpawnType.VerminHorde, 5557, 824, 65 ), // Despise
			new SpawnRecord( (int)ChampionSpawnType.ColdBlood, 5259, 837, 61 ), // Destard
			new SpawnRecord( (int)ChampionSpawnType.Abyss, 5814, 1350, 2 ), // Fire
			new SpawnRecord( (int)ChampionSpawnType.Arachnid, 5190, 1605, 20 ), // TerathanKeep
		};

		private SpawnRecord[] m_LostLands = new SpawnRecord[]
		{
			new SpawnRecord( 0xff, 5636, 2916, 37 ), // Desert
			new SpawnRecord( 0xff, 5724, 3991, 42 ), // Tortoise
			new SpawnRecord( 0xff, 5511, 2360, 40 ), // Ice West

			new SpawnRecord( 0xff, 5549, 2640, 15 ), // Oasis
			new SpawnRecord( 0xff, 6035, 2944, 52 ), // Terra Sanctum
			new SpawnRecord( (int)ChampionSpawnType.ForestLord, 5559, 3757, 21 ), // Lord Oaks

			new SpawnRecord( 0xff, 5267, 3171, 104 ), // Marble
			new SpawnRecord( 0xff, 5954, 3475, 25 ), // Hoppers Boog
			new SpawnRecord( 0xff, 5982, 3882, 20 ), // Khaldun

			new SpawnRecord( 0xff, 6038, 2400, 46 ), // Ice East
			new SpawnRecord( 0xff, 5281, 3368, 51 ), // Damwin
			new SpawnRecord( 0xff, 5207, 3637, 20 ), // City of Death
		};

		private SpawnRecord[] m_Ilshenar = new SpawnRecord[]
		{
			new SpawnRecord( 0xff, 382, 328, -30 ), // Valor
			new SpawnRecord( 0xff, 462, 926, -67 ), // Humility
			new SpawnRecord( (int)ChampionSpawnType.ForestLord, 1645, 1107, 8 ), // Spirituality
			new SpawnRecord( (int)ChampionSpawnType.Glade, 2210, 1260, 23 ), // Twisted Glade
		};

		private SpawnRecord[] m_Tokuno = new SpawnRecord[]
		{
			new SpawnRecord( (int)ChampionSpawnType.SleepingDragon, 948, 434, 29 ), // Isamu Jima
		};

		private SpawnRecord[] m_Malas = new SpawnRecord[]
		{
			new SpawnRecord( (int)ChampionSpawnType.Pestilence, 174, 1629, 10 ), // Bedlam cemetery
			// Minotaur champ not scripted by RunUO team yet.
		};

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Active
		{
			get
			{
				return m_Active;
			}
			set
			{
				if ( value )
					Start();
				else
					Stop();

				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int ActiveAltars
		{
			get
			{
				return m_ActiveAltars;
			}
			set
			{
				m_ActiveAltars = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan ExpireDelay
		{
			get
			{
				return m_ExpireDelay;
			}
			set
			{
				m_ExpireDelay = value;

				foreach( ChampionSpawn cs in m_AllSpawn )
				{
					cs.ExpireDelay = m_ExpireDelay;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan RestartDelay
		{
			get
			{
				return m_RestartDelay;
			}
			set
			{
				m_RestartDelay = value;

				foreach( ChampionSpawn cs in m_IlshenarSpawn )
				{
					cs.RestartDelay = m_RestartDelay;
				}

				foreach( ChampionSpawn cs in m_TokunoSpawn )
				{
					cs.RestartDelay = m_RestartDelay;
				}

				foreach( ChampionSpawn cs in m_MalasSpawn )
				{
					cs.RestartDelay = m_RestartDelay;
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan RandomizeDelay
		{
			get
			{
				return m_RandomizeDelay;
			}
			set
			{
				m_RandomizeDelay = value;
			}
		}

		[Constructable]
		public ChampionSpawnController() : base( 0x3EDD )
		{
			if ( Check() )
			{
				World.Broadcast( 0x35, true, "Another champion spawn controller exist in the world !" );
				Delete();
				return;
			}

			Visible = false;
			Movable = false;
			Name = "champion spawn controller";

			m_Active = false;

			m_AllSpawn = new ArrayList();
			m_DungeonsSpawn = new ArrayList();
			m_LostLandsSpawn = new ArrayList();
			m_IlshenarSpawn = new ArrayList();
			m_TokunoSpawn = new ArrayList();
			m_MalasSpawn = new ArrayList();

			m_ActiveAltars = 1;

			m_ExpireDelay = TimeSpan.FromMinutes( 10.0 );
			m_RestartDelay = TimeSpan.FromMinutes( 5.0 );

			m_RandomizeDelay = TimeSpan.FromHours( 72.0 );
			                    
			DeleteAll();
			Generate();

			World.Broadcast( 0x35, true, "Champion spawn generation complete. Old spawns removed." );
		}

		private bool Check()
		{
			foreach ( Item item in World.Items.Values )
			{
				if ( item is ChampionSpawnController && !item.Deleted && item != this ) 
						return true;
			}	

			return false;
		}

		private void DeleteAll()
		{
			ArrayList list = new ArrayList();

			foreach ( Item item in World.Items.Values )
			{
				if ( item is ChampionSpawn && !item.Deleted ) 
						list.Add( item );
			}	

			foreach ( ChampionSpawn cs in list )
			{
				cs.Delete();
			}
		}

		private ChampionSpawn CreateAltar( SpawnRecord r, Map m, bool restartdisable )
		{
			ChampionSpawn cs = new ChampionSpawn();

			Point3D loc = new Point3D( r.x, r.y, r.z );

			if ( r.type == 0xff )	
			{
				cs.RandomizeType = true;

				switch ( Utility.Random( 5 ) )
				{
					case 0: cs.Type = ChampionSpawnType.VerminHorde; break;
					case 1: cs.Type = ChampionSpawnType.UnholyTerror; break;
					case 2: cs.Type = ChampionSpawnType.ColdBlood; break;
					case 3: cs.Type = ChampionSpawnType.Abyss; break;
					case 4: cs.Type = ChampionSpawnType.Arachnid; break;
				}
			}
			else
			{
				cs.RandomizeType = false;
				cs.Type = (ChampionSpawnType)r.type;
			}

// Prevent autorestart of felucca & t2a the spawns

			if ( restartdisable )
				cs.RestartDelay = TimeSpan.FromDays( 9999 );

			cs.MoveToWorld( loc, m );

			return cs;
		}

		private void Generate()
		{
			int i = 0;

			for ( i = 0; i<m_Dungeons.Length; i++ )
			{       
				ChampionSpawn cs = CreateAltar( m_Dungeons[i], Map.Felucca, true );

				m_AllSpawn.Add( cs );
				m_DungeonsSpawn.Add( cs );
			}

			for ( i = 0; i<m_LostLands.Length; i++ )
			{
				ChampionSpawn cs = CreateAltar( m_LostLands[i], Map.Felucca, true );

				m_AllSpawn.Add( cs );
				m_LostLandsSpawn.Add( cs );
			}

			for ( i = 0; i<m_Ilshenar.Length; i++ )
			{
				ChampionSpawn cs = CreateAltar( m_Ilshenar[i], Map.Ilshenar, false );
				
				m_IlshenarSpawn.Add( cs );
				m_AllSpawn.Add( cs  );
			}

			for ( i = 0; i<m_Tokuno.Length; i++ )
			{
				ChampionSpawn cs = CreateAltar( m_Tokuno[i], Map.Tokuno, false );
			
				m_TokunoSpawn.Add( cs );
				m_AllSpawn.Add( cs );
			}

			for ( i = 0; i<m_Malas.Length; i++ )
			{
				ChampionSpawn cs = CreateAltar( m_Malas[i], Map.Malas, false );
			
				m_MalasSpawn.Add( cs );
				m_AllSpawn.Add( cs );
			}
		}

		public ChampionSpawnController( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Active )
			{
				list.Add( 1060742 ); // active
			}
			else
			{
				list.Add( 1060743 ); // inactive
			}
		}

		public override void OnDelete()
		{
			Stop();

			if ( m_AllSpawn != null )
			{
				foreach( ChampionSpawn cs in m_AllSpawn )
				{
					if ( !cs.Deleted ) cs.Delete();
				}
			}

			base.OnDelete();
		}

		public void Start()
		{
			if ( m_Active || Deleted ) return;

			m_Active = true;

			if (m_Timer == null)
			{
				m_Timer = new RandomizeTimer( this, m_RandomizeDelay );
			}

			Randomize( m_DungeonsSpawn );
			Randomize( m_LostLandsSpawn );

			foreach( ChampionSpawn cs in m_IlshenarSpawn )
			{
				if ( !cs.Deleted )
					cs.Active = true;
			}

			foreach( ChampionSpawn cs in m_TokunoSpawn )
			{
				if ( !cs.Deleted )
					cs.Active = true;
			}

			foreach( ChampionSpawn cs in m_MalasSpawn )
			{
				if ( !cs.Deleted )
					cs.Active = true;
			}

			m_Timer.Start();
		}

		public void Stop()
		{
			if ( !m_Active || Deleted ) return;

			m_Active = false;

			if ( m_Timer != null ) m_Timer.Stop();

			foreach( ChampionSpawn cs in m_AllSpawn )
			{
				if ( !cs.Deleted && cs.Active )
				{
					cs.Active = false;
					//cs.IsValorUsed = false;
				}
			}
		}

		public void Randomize( ArrayList list )
		{
			foreach( ChampionSpawn cs in list )
			{
				if ( !cs.Deleted && cs.Active )
					cs.Active = false;
			}

			for ( int i=0; i<m_ActiveAltars; i++ )
			{
				int trynum = 0;

				while( trynum < 7 )
				{
					int index = Utility.Random( list.Count );

					if ( !((ChampionSpawn)list[index]).Active )
					{
						((ChampionSpawn)list[index]).Active = true;
						break;
					}

					trynum++;
				}	
			}
		}

		public void Slice()
		{
			if ( !m_Active || Deleted )
			{
				if ( m_Timer != null ) m_Timer.Stop();
				return;
			}

			Randomize( m_DungeonsSpawn );
			Randomize( m_LostLandsSpawn );

			m_Timer.Start();
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendGump( new PropertiesGump( from, this ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
	
			writer.Write ( m_Active );

			writer.WriteItemList( m_AllSpawn );
			writer.WriteItemList( m_DungeonsSpawn );
			writer.WriteItemList( m_LostLandsSpawn );
			writer.WriteItemList( m_IlshenarSpawn );
			writer.WriteItemList( m_TokunoSpawn );
			writer.WriteItemList( m_MalasSpawn );

			writer.Write( m_RandomizeDelay );

			writer.Write( m_ActiveAltars );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			m_Active  = reader.ReadBool();

			m_AllSpawn = reader.ReadItemList();
			m_DungeonsSpawn = reader.ReadItemList();
			m_LostLandsSpawn = reader.ReadItemList();
			m_IlshenarSpawn = reader.ReadItemList();
			m_TokunoSpawn = reader.ReadItemList();
			m_MalasSpawn = reader.ReadItemList();

			m_RandomizeDelay = reader.ReadTimeSpan();

			m_ActiveAltars = reader.ReadInt();

			m_ExpireDelay = TimeSpan.FromMinutes( 10.0 );
			m_RestartDelay = TimeSpan.FromMinutes( 5.0 );

			if ( m_Active )
			{
				m_Timer = new RandomizeTimer( this, m_RandomizeDelay );

				m_Timer.Start();
			}
		}
	}
}
