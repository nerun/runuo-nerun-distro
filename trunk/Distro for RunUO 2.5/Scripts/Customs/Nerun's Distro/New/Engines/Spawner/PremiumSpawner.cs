//Engine r147
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Server;
using Server.Commands;
using Server.Items;
using Server.Network;
using CPA = Server.CommandPropertyAttribute;

namespace Server.Mobiles
{
	public class PremiumSpawner : Item
	{
		private int m_Team;
		private int m_HomeRange; // = old SpawnRange
		private int m_WalkingRange = -1; // = old HomeRange
		private int m_SpawnID = 1;
		private int m_Count;
		private int m_CountA;
		private int m_CountB;
		private int m_CountC;
		private int m_CountD;
		private int m_CountE;
		private TimeSpan m_MinDelay;
		private TimeSpan m_MaxDelay;
		private List<string> m_CreaturesName; // creatures to be spawned
		private List<IEntity> m_Creatures; // spawned creatures
		private List<string> m_CreaturesNameA;
		private List<IEntity> m_CreaturesA;
		private List<string> m_CreaturesNameB;
		private List<IEntity> m_CreaturesB;
		private List<string> m_CreaturesNameC;
		private List<IEntity> m_CreaturesC;
		private List<string> m_CreaturesNameD;
		private List<IEntity> m_CreaturesD;
		private List<string> m_CreaturesNameE;
		private List<IEntity> m_CreaturesE;
		private DateTime m_End;
		private InternalTimer m_Timer;
		private bool m_Running;
		private bool m_Water;
		private bool m_Group;
		private WayPoint m_WayPoint;

		public bool IsFull{ get{ return ( m_Creatures != null && m_Creatures.Count >= m_Count ); } }
		public bool IsFulla{ get{ return ( m_CreaturesA != null && m_CreaturesA.Count >= m_CountA ); } }
		public bool IsFullb{ get{ return ( m_CreaturesB != null && m_CreaturesB.Count >= m_CountB ); } }
		public bool IsFullc{ get{ return ( m_CreaturesC != null && m_CreaturesC.Count >= m_CountC ); } }
		public bool IsFulld{ get{ return ( m_CreaturesD != null && m_CreaturesD.Count >= m_CountD ); } }
		public bool IsFulle{ get{ return ( m_CreaturesE != null && m_CreaturesE.Count >= m_CountE ); } }

		public List<string> CreaturesName
		{
			get { return m_CreaturesName; }
			set
			{
				m_CreaturesName = value;
				if ( m_CreaturesName.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<string> SubSpawnerA
		{
			get { return m_CreaturesNameA; }
			set
			{
				m_CreaturesNameA = value;
				if ( m_CreaturesNameA.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<string> SubSpawnerB
		{
			get { return m_CreaturesNameB; }
			set
			{
				m_CreaturesNameB = value;
				if ( m_CreaturesNameB.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<string> SubSpawnerC
		{
			get { return m_CreaturesNameC; }
			set
			{
				m_CreaturesNameC = value;
				if ( m_CreaturesNameC.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<string> SubSpawnerD
		{
			get { return m_CreaturesNameD; }
			set
			{
				m_CreaturesNameD = value;
				if ( m_CreaturesNameD.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<string> SubSpawnerE
		{
			get { return m_CreaturesNameE; }
			set
			{
				m_CreaturesNameE = value;
				if ( m_CreaturesNameE.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<IEntity> Creatures
		{
			get { return m_Creatures; }
			set
			{
				m_Creatures = value;
				if ( m_Creatures.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<IEntity> CreaturesA
		{
			get { return m_CreaturesA; }
			set
			{
				m_CreaturesA = value;
				if ( m_CreaturesA.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}
		
		public List<IEntity> CreaturesB
		{
			get { return m_CreaturesB; }
			set
			{
				m_CreaturesB = value;
				if ( m_CreaturesB.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<IEntity> CreaturesC
		{
			get { return m_CreaturesC; }
			set
			{
				m_CreaturesC = value;
				if ( m_CreaturesC.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<IEntity> CreaturesD
		{
			get { return m_CreaturesD; }
			set
			{
				m_CreaturesD = value;
				if ( m_CreaturesD.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public List<IEntity> CreaturesE
		{
			get { return m_CreaturesE; }
			set
			{
				m_CreaturesE = value;
				if ( m_CreaturesE.Count < 1 )
					Stop();

				InvalidateProperties();
			}
		}

		public virtual int CreaturesNameCount { get { return m_CreaturesName.Count; } }
		public virtual int CreaturesNameCountA { get { return m_CreaturesNameA.Count; } }
		public virtual int CreaturesNameCountB { get { return m_CreaturesNameB.Count; } }
		public virtual int CreaturesNameCountC { get { return m_CreaturesNameC.Count; } }
		public virtual int CreaturesNameCountD { get { return m_CreaturesNameD.Count; } }
		public virtual int CreaturesNameCountE { get { return m_CreaturesNameE.Count; } }

		public override void OnAfterDuped( Item newItem )
		{
			PremiumSpawner s = newItem as PremiumSpawner;

			if ( s == null )
				return;

			s.m_CreaturesName = new List<string>( m_CreaturesName );
			s.m_CreaturesNameA = new List<string>( m_CreaturesNameA );
			s.m_CreaturesNameB = new List<string>( m_CreaturesNameB );
			s.m_CreaturesNameC = new List<string>( m_CreaturesNameC );
			s.m_CreaturesNameD = new List<string>( m_CreaturesNameD );
			s.m_CreaturesNameE = new List<string>( m_CreaturesNameE );
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Count
		{
			get { return m_Count; }
			set { m_Count = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CountA
		{
			get { return m_CountA; }
			set { m_CountA = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CountB
		{
			get { return m_CountB; }
			set { m_CountB = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CountC
		{
			get { return m_CountC; }
			set { m_CountC = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CountD
		{
			get { return m_CountD; }
			set { m_CountD = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CountE
		{
			get { return m_CountE; }
			set { m_CountE = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public WayPoint WayPoint
		{
			get
			{
				return m_WayPoint;
			}
			set
			{
				m_WayPoint = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Running
		{
			get { return m_Running; }
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
		public int HomeRange
		{
			get { return m_HomeRange; }
			set { m_HomeRange = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )] 
		public int WalkingRange 
		{ 
		   get { return m_WalkingRange; } 
		   set { m_WalkingRange = value; InvalidateProperties(); } 
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int SpawnID
		{
			get { return m_SpawnID; }
			set { m_SpawnID = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Team
		{
			get { return m_Team; }
			set { m_Team = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan MinDelay
		{
			get { return m_MinDelay; }
			set { m_MinDelay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan MaxDelay
		{
			get { return m_MaxDelay; }
			set { m_MaxDelay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan NextSpawn
		{
			get
			{
				if ( m_Running )
					return m_End - DateTime.Now;
				else
					return TimeSpan.FromSeconds( 0 );
			}
			set
			{
				Start();
				DoTimer( value );
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Group
		{
			get { return m_Group; }
			set { m_Group = value; InvalidateProperties(); }
		}

		[Constructable]
		public PremiumSpawner( int amount, int subamountA, int subamountB, int subamountC, int subamountD, int subamountE, int spawnid, int minDelay, int maxDelay, int team, int homeRange, int walkingRange, string creatureName, string creatureNameA, string creatureNameB, string creatureNameC, string creatureNameD, string creatureNameE ) : base( 0x1f13 )
		{
			List<string> creaturesName = new List<string>();
			creaturesName.Add( creatureName );

			List<string> creatureNameAA = new List<string>();
			creaturesName.Add( creatureNameA );

			List<string> creatureNameBB = new List<string>();
			creaturesName.Add( creatureNameB );

			List<string> creatureNameCC = new List<string>();
			creaturesName.Add( creatureNameC );

			List<string> creatureNameDD = new List<string>();
			creaturesName.Add( creatureNameD );

			List<string> creatureNameEE = new List<string>();
			creaturesName.Add( creatureNameE );

			InitSpawn( amount, subamountA, subamountB, subamountC, subamountD, subamountE, spawnid, TimeSpan.FromMinutes( minDelay ), TimeSpan.FromMinutes( maxDelay ), team, homeRange, walkingRange, creaturesName, creatureNameAA, creatureNameBB, creatureNameCC, creatureNameDD, creatureNameEE );
		}

		[Constructable]
		public PremiumSpawner( string creatureName ) : base( 0x1f13 )
		{
			List<string> creaturesName = new List<string>();
			creaturesName.Add( creatureName );

			List<string> creatureNameAA = new List<string>();
			List<string> creatureNameBB = new List<string>();
			List<string> creatureNameCC = new List<string>();
			List<string> creatureNameDD = new List<string>();
			List<string> creatureNameEE = new List<string>();

			InitSpawn( 1, 0, 0, 0, 0, 0, 1, TimeSpan.FromMinutes( 5 ), TimeSpan.FromMinutes( 10 ), 0, 4, -1, creaturesName, creatureNameAA, creatureNameBB, creatureNameCC, creatureNameDD, creatureNameEE );
		}

		[Constructable]
		public PremiumSpawner() : base( 0x1f13 )
		{
			List<string> creaturesName = new List<string>();

			List<string> creatureNameAA = new List<string>();
			List<string> creatureNameBB = new List<string>();
			List<string> creatureNameCC = new List<string>();
			List<string> creatureNameDD = new List<string>();
			List<string> creatureNameEE = new List<string>();

			InitSpawn( 1, 0, 0, 0, 0, 0, 1, TimeSpan.FromMinutes( 5 ), TimeSpan.FromMinutes( 10 ), 0, 4, -1, creaturesName, creatureNameAA, creatureNameBB, creatureNameCC, creatureNameDD, creatureNameEE );
		}

		public PremiumSpawner( int amount, int subamountA, int subamountB, int subamountC, int subamountD, int subamountE, int spawnid, TimeSpan minDelay, TimeSpan maxDelay, int team, int homeRange, int walkingRange, List<string> creaturesName, List<string> creatureNameAA, List<string> creatureNameBB, List<string> creatureNameCC, List<string> creatureNameDD, List<string> creatureNameEE )
			: base( 0x1f13 )
		{
			InitSpawn( amount, subamountA, subamountB, subamountC, subamountD, subamountE, spawnid, minDelay, maxDelay, team, homeRange, walkingRange, creaturesName, creatureNameAA, creatureNameBB, creatureNameCC, creatureNameDD, creatureNameEE );
		}

		public override string DefaultName
		{
			get { return "PremiumSpawner"; }
		}

		public void InitSpawn( int amount, int subamountA, int subamountB, int subamountC, int subamountD, int subamountE, int SpawnID, TimeSpan minDelay, TimeSpan maxDelay, int team, int homeRange, int walkingRange, List<string> creaturesName, List<string> creatureNameAA, List<string> creatureNameBB, List<string> creatureNameCC, List<string> creatureNameDD, List<string> creatureNameEE )
		{
			Name = "PremiumSpawner";
			m_SpawnID = SpawnID;
			Visible = false;
			Movable = false;
			m_Running = true;
			m_Water = false;
			m_Group = false;
			m_MinDelay = minDelay;
			m_MaxDelay = maxDelay;
			m_Count = amount;
			m_CountA = subamountA;
			m_CountB = subamountB;
			m_CountC = subamountC;
			m_CountD = subamountD;
			m_CountE = subamountE;
			m_Team = team;
			m_HomeRange = homeRange;
			m_WalkingRange = walkingRange;
			m_CreaturesName = creaturesName;
			m_CreaturesNameA = creatureNameAA;
			m_CreaturesNameB = creatureNameBB;
			m_CreaturesNameC = creatureNameCC;
			m_CreaturesNameD = creatureNameDD;
			m_CreaturesNameE = creatureNameEE;
			m_Creatures = new List<IEntity>();
			m_CreaturesA = new List<IEntity>();
			m_CreaturesB = new List<IEntity>();
			m_CreaturesC = new List<IEntity>();
			m_CreaturesD = new List<IEntity>();
			m_CreaturesE = new List<IEntity>();
			DoTimer( TimeSpan.FromSeconds( 1 ) );
		}
			
		public PremiumSpawner( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel < AccessLevel.GameMaster )
				return;

			PremiumSpawnerGump g = new PremiumSpawnerGump( this );
			from.SendGump( g );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Running )
			{
				list.Add( 1060742 ); // active

				list.Add( 1060656, m_Count.ToString() );
				list.Add( 1061169, m_HomeRange.ToString() );
				list.Add( 1060658, "walking range\t{0}", m_WalkingRange );

				list.Add( 1060663, "SpawnID\t{0}", m_SpawnID.ToString() );

//				list.Add( 1060659, "group\t{0}", m_Group );
//				list.Add( 1060660, "team\t{0}", m_Team );
				list.Add( 1060661, "speed\t{0} to {1}", m_MinDelay, m_MaxDelay );

				for ( int i = 0; i < 2 && i < m_CreaturesName.Count; ++i )
					list.Add( 1060662 + i, "{0}\t{1}", m_CreaturesName[i], CountCreatures( Creatures, m_CreaturesName[i] ) );
			}
			else
			{
				list.Add( 1060743 ); // inactive
			}
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			if ( m_Running )
				LabelTo( from, "[Running]" );
			else
				LabelTo( from, "[Off]" );
		}
		
		public void SpawnWater()
		{
		}

		public void Start()
		{
			if ( !m_Running )
			{
				if ( m_CreaturesName.Count > 0 || m_CreaturesNameA.Count > 0 || m_CreaturesNameB.Count > 0 || m_CreaturesNameC.Count > 0 || m_CreaturesNameD.Count > 0 || m_CreaturesNameE.Count > 0 )
				{
					m_Running = true;
					DoTimer();
				}
			}
		}

		public void Stop()
		{
			if ( m_Running )
			{
				m_Timer.Stop();
				m_Running = false;
				RemoveCreatures(m_Creatures);
				RemoveCreatures(m_CreaturesA);
				RemoveCreatures(m_CreaturesB);
				RemoveCreatures(m_CreaturesC);
				RemoveCreatures(m_CreaturesD);
				RemoveCreatures(m_CreaturesE);
			}
		}

		public static string ParseType( string s )
		{
			return s.Split( null, 2 )[0];
		}

		public void Defrag( List<IEntity> m_Beings )
		{
			bool removed = false;

			for ( int i = 0; i < m_Beings.Count; ++i )
			{
				IEntity e = m_Beings[i];

				if ( e is Item )
				{
					Item item = (Item)e;

					if ( item.Deleted || item.Parent != null )
					{
						m_Beings.RemoveAt( i );
						--i;
						removed = true;
					}
				}
				else if ( e is Mobile )
				{
					Mobile m = (Mobile)e;

					if ( m.Deleted )
					{
						m_Beings.RemoveAt( i );
						--i;
						removed = true;
					}
					else if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;
						if ( bc.Controlled || bc.IsStabled )
						{
							m_Beings.RemoveAt( i );
							--i;
							removed = true;
						}
					}
				}
				else
				{
					m_Beings.RemoveAt( i );
					--i;
					removed = true;
				}
			}

			if ( removed )
				InvalidateProperties();
		}

		public void OnTick()
		{
			DoTimer();

			if ( m_Group )
			{
				Defrag(m_Creatures);
				Defrag(m_CreaturesA);
				Defrag(m_CreaturesB);
				Defrag(m_CreaturesC);
				Defrag(m_CreaturesD);
				Defrag(m_CreaturesE);

				if  ( m_Creatures.Count == 0 || m_CreaturesA.Count == 0 || m_CreaturesB.Count == 0 || m_CreaturesC.Count == 0 || m_CreaturesD.Count == 0 || m_CreaturesE.Count == 0 )
				{
					Respawn();
				}
				else
				{
					return;
				}
			}
			else
			{
				Spawn(CreaturesNameCount, m_Creatures, m_Count, m_CreaturesName);
				Spawn(CreaturesNameCountA, m_CreaturesA, m_CountA, m_CreaturesNameA);
				Spawn(CreaturesNameCountB, m_CreaturesB, m_CountB, m_CreaturesNameB);
				Spawn(CreaturesNameCountC, m_CreaturesC, m_CountC, m_CreaturesNameC);
				Spawn(CreaturesNameCountD, m_CreaturesD, m_CountD, m_CreaturesNameD);
				Spawn(CreaturesNameCountE, m_CreaturesE, m_CountE, m_CreaturesNameE);
			}
		}
		
		public void Respawn() // remove all creatures and spawn all again
		{
			RemoveCreatures(m_Creatures);
			RemoveCreatures(m_CreaturesA);
			RemoveCreatures(m_CreaturesB);
			RemoveCreatures(m_CreaturesC);
			RemoveCreatures(m_CreaturesD);
			RemoveCreatures(m_CreaturesE);

			for ( int i = 0; i < m_Count; i++ )
				Spawn(CreaturesNameCount, m_Creatures, m_Count, m_CreaturesName);
			for ( int i = 0; i < m_CountA; i++ )
				Spawn(CreaturesNameCountA, m_CreaturesA, m_CountA, m_CreaturesNameA);
			for ( int i = 0; i < m_CountB; i++ )
				Spawn(CreaturesNameCountB, m_CreaturesB, m_CountB, m_CreaturesNameB);
			for ( int i = 0; i < m_CountC; i++ )
				Spawn(CreaturesNameCountC, m_CreaturesC, m_CountC, m_CreaturesNameC);
			for ( int i = 0; i < m_CountD; i++ )
				Spawn(CreaturesNameCountD, m_CreaturesD, m_CountD, m_CreaturesNameD);
			for ( int i = 0; i < m_CountE; i++ )
				Spawn(CreaturesNameCountE, m_CreaturesE, m_CountE, m_CreaturesNameE);
		}
		
		public void Spawn( int CreatNameCount, List<IEntity> m_Creat, int m_Countt, List<string> m_CreatName )
		{
			if ( CreatNameCount > 0 )
				SpawnTwo( Utility.Random( CreatNameCount ), CreatNameCount, m_Creat, m_Countt, m_CreatName );

		}

		// Used only by PremiumSpawnerGump(line 415-45)
		// BROKEN
		public void SpawnFromGump( List<string> m_subspawnName, List<IEntity> m_subspawn, int subCount, int subNameCount, string creatureName )
		{
			for ( int i = 0; i < m_subspawnName.Count; i++ )
			{
				if ( m_subspawnName[i] == creatureName )
				{
					SpawnTwo( i, subNameCount, m_subspawn, subCount, m_subspawnName );
					break;
				}
			}
		}

		protected virtual IEntity CreateSpawnedObject( int index, List<string> m_CreatName )
		{
			if ( index >= m_CreatName.Count )
				return null;

			Type type = ScriptCompiler.FindTypeByName( ParseType( m_CreatName[index] ) );

			if ( type != null )
			{
				try
				{
					return Build( CommandSystem.Split( m_CreatName[index] ) );
				}
				catch
				{
				}
			}

			return null;
		}

		public static IEntity Build( string[] args )
		{
			string name = args[0];

			Add.FixArgs( ref args );

			string[,] props = null;

			for ( int i = 0; i < args.Length; ++i )
			{
				if ( Insensitive.Equals( args[i], "set" ) )
				{
					int remains = args.Length - i - 1;

					if ( remains >= 2 )
					{
						props = new string[remains / 2, 2];

						remains /= 2;

						for ( int j = 0; j < remains; ++j )
						{
							props[j, 0] = args[i + (j * 2) + 1];
							props[j, 1] = args[i + (j * 2) + 2];
						}

						Add.FixSetString( ref args, i );
					}

					break;
				}
			}

			Type type = ScriptCompiler.FindTypeByName( name );

			if ( !Add.IsEntity( type ) ) {
				return null;
			}

			PropertyInfo[] realProps = null;

			if ( props != null )
			{
				realProps = new PropertyInfo[props.GetLength( 0 )];

				PropertyInfo[] allProps = type.GetProperties( BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public );

				for ( int i = 0; i < realProps.Length; ++i )
				{
					PropertyInfo thisProp = null;

					string propName = props[i, 0];

					for ( int j = 0; thisProp == null && j < allProps.Length; ++j )
					{
						if ( Insensitive.Equals( propName, allProps[j].Name ) )
							thisProp = allProps[j];
					}

					if ( thisProp != null )
					{
						CPA attr = Properties.GetCPA( thisProp );

						if ( attr != null && AccessLevel.GameMaster >= attr.WriteLevel && thisProp.CanWrite && !attr.ReadOnly )
							realProps[i] = thisProp;
					}
				}
			}

			ConstructorInfo[] ctors = type.GetConstructors();

			for ( int i = 0; i < ctors.Length; ++i )
			{
				ConstructorInfo ctor = ctors[i];

				if ( !Add.IsConstructable( ctor, AccessLevel.GameMaster ) )
					continue;

				ParameterInfo[] paramList = ctor.GetParameters();

				if ( args.Length == paramList.Length )
				{
					object[] paramValues = Add.ParseValues( paramList, args );

					if ( paramValues == null )
						continue;

					object built = ctor.Invoke( paramValues );

					if ( built != null && realProps != null )
					{
						for ( int j = 0; j < realProps.Length; ++j )
						{
							if ( realProps[j] == null )
								continue;

							string result = Properties.InternalSetValue( built, realProps[j], props[j, 1] );
						}
					}

					return (IEntity)built;
				}
			}

			return null;
		}

		public void SpawnTwo( int index, int CreatNameCount, List<IEntity> m_Creat, int m_Countt, List<string> m_CreatName )
		{
			Map map = Map;

			if ( map == null || map == Map.Internal || CreatNameCount == 0 || index >= CreatNameCount || Parent != null )
				return;

			Defrag(m_Creatures);
			Defrag(m_CreaturesA);
			Defrag(m_CreaturesB);
			Defrag(m_CreaturesC);
			Defrag(m_CreaturesD);
			Defrag(m_CreaturesE);

			if ( m_Creat.Count >= m_Countt )
				return;


			IEntity ent = CreateSpawnedObject( index, m_CreatName );

			if ( ent is Mobile )
			{
				Mobile m = (Mobile)ent;

				if ( m.CanSwim )
				{
					m_Water = true;
				}
				else 
				{
					m_Water = false;
				}

				m_Creat.Add( m );
				

				Point3D loc = ( m is BaseVendor ? this.Location : GetSpawnPosition() );
				
				if ( m is WanderingHealer || m is EvilWanderingHealer || m is EvilHealer )
				{
					loc = GetSpawnPosition();
				}

				m.OnBeforeSpawn( loc, map );
				InvalidateProperties();


				m.MoveToWorld( loc, map );

				if ( m is BaseCreature )
				{
					BaseCreature c = (BaseCreature)m;
					
					if( m_WalkingRange >= 0 )
						c.RangeHome = m_WalkingRange;
					else
						c.RangeHome = m_HomeRange;

					c.CurrentWayPoint = m_WayPoint;

					if ( m_Team > 0 )
						c.Team = m_Team;

					c.Home = this.Location;
				}

				m.OnAfterSpawn();
			}
			else if ( ent is Item )
			{
				Item item = (Item)ent;

				m_Creat.Add( item );

				Point3D loc = GetSpawnPosition();

				item.OnBeforeSpawn( loc, map );
				InvalidateProperties();

				item.MoveToWorld( loc, map );

				item.OnAfterSpawn();
			}
		}

		public Point3D GetSpawnPosition()
		{
			Map map = Map;

			if ( map == null )
				return Location;

			// Try 10 times to find a Spawnable location.
			for ( int i = 0; i < 10; i++ )
			{
				int x, y;

				if ( m_HomeRange > 0 ) {
					x = Location.X + (Utility.Random( (m_HomeRange * 2) + 1 ) - m_HomeRange);
					y = Location.Y + (Utility.Random( (m_HomeRange * 2) + 1 ) - m_HomeRange);
				} else {
					x = Location.X;
					y = Location.Y;
				}

				int z = Map.GetAverageZ( x, y );

			   if ( m_Water )
			   {
					   TileMatrix tiles = Map.Tiles;
					   LandTile _tile = tiles.GetLandTile(x,y);
					   int id = _tile.ID;
					   if((id >= 168 && id <= 171) || id == 100)
					   {
						   return new Point3D( x, y, this.Z );
					   }
					   else
					   {
						   continue;
					   }
			   }

				if ( Map.CanSpawnMobile( new Point2D( x, y ), this.Z ) )
					return new Point3D( x, y, this.Z );
				else if ( Map.CanSpawnMobile( new Point2D( x, y ), z ) )
					return new Point3D( x, y, z );
			}
			
			return this.Location;
		}
		
		public void DoTimer()
		{
			if ( !m_Running )
				return;

			int minSeconds = (int)m_MinDelay.TotalSeconds;
			int maxSeconds = (int)m_MaxDelay.TotalSeconds;

			TimeSpan delay = TimeSpan.FromSeconds( Utility.RandomMinMax( minSeconds, maxSeconds ) );
			DoTimer( delay );
		}

		public void DoTimer( TimeSpan delay )
		{
			if ( !m_Running )
				return;

			m_End = DateTime.Now + delay;

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = new InternalTimer( this, delay );
			m_Timer.Start();
		}

		private class InternalTimer : Timer
		{
			private PremiumSpawner m_PremiumSpawner;

			public InternalTimer( PremiumSpawner spawner, TimeSpan delay ) : base( delay )
			{
				if ( spawner.IsFull || spawner.IsFulla || spawner.IsFullb || spawner.IsFullc || spawner.IsFulld || spawner.IsFulle )
					Priority = TimerPriority.FiveSeconds;
				else
					Priority = TimerPriority.OneSecond;

				m_PremiumSpawner = spawner;
			}

			protected override void OnTick()
			{
				if ( m_PremiumSpawner != null )
					if ( !m_PremiumSpawner.Deleted )
						m_PremiumSpawner.OnTick();
			}
		}

		// Used only by PremiumSpawnerGump (except 1st, used here too)
		public int CountCreatures( List<IEntity> m_subspawn, string creatureName )
		{
			Defrag(m_subspawn);

			int count = 0;

			for ( int i = 0; i < m_subspawn.Count; ++i )
				if ( Insensitive.Equals( creatureName, m_subspawn[i].GetType().Name ) )
					++count;

			return count;
		}

		// Used only by PremiumSpawnerGump (lines 446-76)
		// BROKEN
		public void RemoveCreaturesFromGump( List<IEntity> m_subspawn, string creatureName )
		{
			Defrag(m_subspawn);

			for ( int i = 0; i < m_subspawn.Count; ++i )
			{
				IEntity e = m_subspawn[i];

				if ( Insensitive.Equals( creatureName, e.GetType().Name ) )
						e.Delete();
			}

			InvalidateProperties();
		}

		// Used only here
		public void RemoveCreatures( List<IEntity> m_Creatur )
		{
			Defrag(m_Creatur);

			for ( int i = 0; i < m_Creatur.Count; ++i )
				m_Creatur[i].Delete();

			InvalidateProperties();
		}

		//Used by PremiumSpawnerGump
		public void BringToHome( List<IEntity> m_Beings )
		{
			for ( int i = 0; i < m_Beings.Count; ++i )
			{
				IEntity e = m_Beings[i];

				if ( e is Mobile )
				{
					Mobile m = (Mobile)e;

					m.MoveToWorld( Location, Map );
				}
				else if ( e is Item )
				{
					Item item = (Item)e;

					item.MoveToWorld( Location, Map );
				}
			}
		}

		//Used by PremiumSpawnerGump
		public void BringToHome()
		{
			Defrag(m_Creatures);
			Defrag(m_CreaturesA);
			Defrag(m_CreaturesB);
			Defrag(m_CreaturesC);
			Defrag(m_CreaturesD);
			Defrag(m_CreaturesE);

			BringToHome(m_Creatures);
			BringToHome(m_CreaturesA);
			BringToHome(m_CreaturesB);
			BringToHome(m_CreaturesC);
			BringToHome(m_CreaturesD);
			BringToHome(m_CreaturesE);
		}

		public override void OnDelete()
		{
			base.OnDelete();

			RemoveCreatures(m_Creatures);
			RemoveCreatures(m_CreaturesA);
			RemoveCreatures(m_CreaturesB);
			RemoveCreatures(m_CreaturesC);
			RemoveCreatures(m_CreaturesD);
			RemoveCreatures(m_CreaturesE);
			if ( m_Timer != null )
				m_Timer.Stop();
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 4 ); // version
			writer.Write( m_WalkingRange );

			writer.Write( m_SpawnID );
			writer.Write( m_CountA );
			writer.Write( m_CountB );
			writer.Write( m_CountC );
			writer.Write( m_CountD );
			writer.Write( m_CountE );

			writer.Write( m_WayPoint );

			writer.Write( m_Group );

			writer.Write( m_MinDelay );
			writer.Write( m_MaxDelay );
			writer.Write( m_Count );
			writer.Write( m_Team );
			writer.Write( m_HomeRange );
			writer.Write( m_Running );
			
			if ( m_Running )
				writer.WriteDeltaTime( m_End );

			writer.Write( m_CreaturesName.Count );

			for ( int i = 0; i < m_CreaturesName.Count; ++i )
				writer.Write( m_CreaturesName[i] );

			writer.Write( m_CreaturesNameA.Count );

			for ( int i = 0; i < m_CreaturesNameA.Count; ++i )
				writer.Write( (string)m_CreaturesNameA[i] );

			writer.Write( m_CreaturesNameB.Count );

			for ( int i = 0; i < m_CreaturesNameB.Count; ++i )
				writer.Write( (string)m_CreaturesNameB[i] );

			writer.Write( m_CreaturesNameC.Count );

			for ( int i = 0; i < m_CreaturesNameC.Count; ++i )
				writer.Write( (string)m_CreaturesNameC[i] );

			writer.Write( m_CreaturesNameD.Count );

			for ( int i = 0; i < m_CreaturesNameD.Count; ++i )
				writer.Write( (string)m_CreaturesNameD[i] );

			writer.Write( m_CreaturesNameE.Count );

			for ( int i = 0; i < m_CreaturesNameE.Count; ++i )
				writer.Write( (string)m_CreaturesNameE[i] );

			writer.Write( m_Creatures.Count );

			for ( int i = 0; i < m_Creatures.Count; ++i )
			{
				IEntity e = m_Creatures[i];

				if ( e is Item )
					writer.Write( (Item)e );
				else if ( e is Mobile )
					writer.Write( (Mobile)e );
				else
					writer.Write( Serial.MinusOne );
			}

			writer.Write( m_CreaturesA.Count );

			for ( int i = 0; i < m_CreaturesA.Count; ++i )
			{
				IEntity e = m_CreaturesA[i];

				if ( e is Item )
					writer.Write( (Item)e );
				else if ( e is Mobile )
					writer.Write( (Mobile)e );
				else
					writer.Write( Serial.MinusOne );
			}

			writer.Write( m_CreaturesB.Count );

			for ( int i = 0; i < m_CreaturesB.Count; ++i )
			{
				IEntity e = m_CreaturesB[i];

				if ( e is Item )
					writer.Write( (Item)e );
				else if ( e is Mobile )
					writer.Write( (Mobile)e );
				else
					writer.Write( Serial.MinusOne );
			}

			writer.Write( m_CreaturesC.Count );

			for ( int i = 0; i < m_CreaturesC.Count; ++i )
			{
				IEntity e = m_CreaturesC[i];

				if ( e is Item )
					writer.Write( (Item)e );
				else if ( e is Mobile )
					writer.Write( (Mobile)e );
				else
					writer.Write( Serial.MinusOne );
			}

			writer.Write( m_CreaturesD.Count );

			for ( int i = 0; i < m_CreaturesD.Count; ++i )
			{
				IEntity e = m_CreaturesD[i];

				if ( e is Item )
					writer.Write( (Item)e );
				else if ( e is Mobile )
					writer.Write( (Mobile)e );
				else
					writer.Write( Serial.MinusOne );
			}

			writer.Write( m_CreaturesE.Count );

			for ( int i = 0; i < m_CreaturesE.Count; ++i )
			{
				IEntity e = m_CreaturesE[i];

				if ( e is Item )
					writer.Write( (Item)e );
				else if ( e is Mobile )
					writer.Write( (Mobile)e );
				else
					writer.Write( Serial.MinusOne );
			}

		}

		private static WarnTimer m_WarnTimer;

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 4:
				{
					m_WalkingRange = reader.ReadInt();
					m_SpawnID = reader.ReadInt();
					m_CountA = reader.ReadInt();
					m_CountB = reader.ReadInt();
					m_CountC = reader.ReadInt();
					m_CountD = reader.ReadInt();
					m_CountE = reader.ReadInt();

					goto case 3;
				}
				case 3:
				case 2:
				{
					m_WayPoint = reader.ReadItem() as WayPoint;

					goto case 1;
				}

				case 1:
				{
					m_Group = reader.ReadBool();
					
					goto case 0;
				}

				case 0:
				{
					m_MinDelay = reader.ReadTimeSpan();
					m_MaxDelay = reader.ReadTimeSpan();
					m_Count = reader.ReadInt();
					m_Team = reader.ReadInt();
					m_HomeRange = reader.ReadInt();
					m_Running = reader.ReadBool();

					TimeSpan ts = TimeSpan.Zero;

					if ( m_Running )
						ts = reader.ReadDeltaTime() - DateTime.Now;
					
					int size = reader.ReadInt();
					m_CreaturesName = new List<string>( size );
					for ( int i = 0; i < size; ++i )
					{
						string creatureString = reader.ReadString();

						m_CreaturesName.Add( creatureString );
						string typeName = ParseType( creatureString );

						if ( ScriptCompiler.FindTypeByName( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int sizeA = reader.ReadInt();
					m_CreaturesNameA = new List<string>( sizeA );
					for ( int i = 0; i < sizeA; ++i )
					{
						string creatureString = reader.ReadString();

						m_CreaturesNameA.Add( creatureString );
						string typeName = ParseType( creatureString );

						if ( ScriptCompiler.FindTypeByName( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int sizeB = reader.ReadInt();
					m_CreaturesNameB = new List<string>( sizeB );
					for ( int i = 0; i < sizeB; ++i )
					{
						string creatureString = reader.ReadString();

						m_CreaturesNameB.Add( creatureString );
						string typeName = ParseType( creatureString );

						if ( ScriptCompiler.FindTypeByName( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int sizeC = reader.ReadInt();
					m_CreaturesNameC = new List<string>( sizeC );
					for ( int i = 0; i < sizeC; ++i )
					{
						string creatureString = reader.ReadString();

						m_CreaturesNameC.Add( creatureString );
						string typeName = ParseType( creatureString );

						if ( ScriptCompiler.FindTypeByName( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int sizeD = reader.ReadInt();
					m_CreaturesNameD = new List<string>( sizeD );
					for ( int i = 0; i < sizeD; ++i )
					{
						string creatureString = reader.ReadString();

						m_CreaturesNameD.Add( creatureString );
						string typeName = ParseType( creatureString );

						if ( ScriptCompiler.FindTypeByName( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int sizeE = reader.ReadInt();
					m_CreaturesNameE = new List<string>( sizeE );
					for ( int i = 0; i < sizeE; ++i )
					{
						string creatureString = reader.ReadString();

						m_CreaturesNameE.Add( creatureString );
						string typeName = ParseType( creatureString );

						if ( ScriptCompiler.FindTypeByName( typeName ) == null )
						{
							if ( m_WarnTimer == null )
								m_WarnTimer = new WarnTimer();

							m_WarnTimer.Add( Location, Map, typeName );
						}
					}

					int count = reader.ReadInt();
					m_Creatures = new List<IEntity>( count );
					for ( int i = 0; i < count; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_Creatures.Add( e );
					}

					int countA = reader.ReadInt();
					m_CreaturesA = new List<IEntity>( countA );
					for ( int i = 0; i < countA; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_CreaturesA.Add( e );
					}

					int countB = reader.ReadInt();
					m_CreaturesB = new List<IEntity>( countB );
					for ( int i = 0; i < countB; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_CreaturesB.Add( e );
					}

					int countC = reader.ReadInt();
					m_CreaturesC = new List<IEntity>( countC );
					for ( int i = 0; i < countC; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_CreaturesC.Add( e );
					}

					int countD = reader.ReadInt();
					m_CreaturesD = new List<IEntity>( countD );
					for ( int i = 0; i < countD; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_CreaturesD.Add( e );
					}

					int countE = reader.ReadInt();
					m_CreaturesE = new List<IEntity>( countE );
					for ( int i = 0; i < countE; ++i )
					{
						IEntity e = World.FindEntity( reader.ReadInt() );

						if ( e != null )
							m_CreaturesE.Add( e );
					}

					if ( m_Running )
						DoTimer( ts );

					break;
				}
			}

			if ( version < 3 && Weight == 0 )
				Weight = -1;
		}

		private class WarnTimer : Timer
		{
			private List<WarnEntry> m_List;

			private class WarnEntry
			{
				public Point3D m_Point;
				public Map m_Map;
				public string m_Name;

				public WarnEntry( Point3D p, Map map, string name )
				{
					m_Point = p;
					m_Map = map;
					m_Name = name;
				}
			}

			public WarnTimer() : base( TimeSpan.FromSeconds( 1.0 ) )
			{
				m_List = new List<WarnEntry>();
				Start();
			}

			public void Add( Point3D p, Map map, string name )
			{
				m_List.Add( new WarnEntry( p, map, name ) );
			}

			protected override void OnTick()
			{
				try
				{
					Console.WriteLine( "Warning: {0} bad spawns detected, logged: 'PremiumBadspawn.log'", m_List.Count );

					using ( StreamWriter op = new StreamWriter( "PremiumBadspawn.log", true ) )
					{
						op.WriteLine( "# Bad spawns : {0}", DateTime.Now );
						op.WriteLine( "# Format: X Y Z F Name" );
						op.WriteLine();

						foreach ( WarnEntry e in m_List )
							op.WriteLine( "{0}\t{1}\t{2}\t{3}\t{4}", e.m_Point.X, e.m_Point.Y, e.m_Point.Z, e.m_Map, e.m_Name );

						op.WriteLine();
						op.WriteLine();
					}
				}
				catch
				{
				}
			}
		}
	}
}
