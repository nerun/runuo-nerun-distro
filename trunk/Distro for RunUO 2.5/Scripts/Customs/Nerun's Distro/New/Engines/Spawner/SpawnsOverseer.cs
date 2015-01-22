/*************************
 *       By Nerun        *
 *      Engine r103      *
 *************************
 */

using System;
using System.IO;
using Server;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Network;

namespace Server.Items
{
	public class SpawnsOverseer : Item
	{
		private int m_Range;
		private int m_InRangeDelay;
		private int m_OutRangeDelay;
		private int m_Overseeing;
		private CheckTimer m_Timer;
		private bool m_Enable;
		private bool m_Gatekeeper;
		private TimeSpan m_CurrentDelay; // players out dungeon
		private DateTime m_End;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Enable
		{
			get { return m_Enable; }
			set
			{
				if ( value )
					Begin();

				else
					End();

				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Gatekeeper
		{
			get { return m_Gatekeeper; }
			set	{ m_Gatekeeper = value;	InvalidateProperties();	}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Range
		{
			get { return m_Range; }
			set { m_Range = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan CurrentDelay
		{
			get { return m_CurrentDelay; }
			set { m_CurrentDelay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TimeSpan NextDelay
		{
			get
			{
				if ( m_Enable )
					return m_End - DateTime.Now;
				else
					return TimeSpan.FromSeconds( 0 );
			}
			set
			{
				Begin();
				DoTimer( value );
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int InRangeDelay
		{
			get { return m_InRangeDelay; }
			set { m_InRangeDelay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int OutRangeDelay
		{
			get { return m_OutRangeDelay; }
			set { m_OutRangeDelay = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Overseeing
		{
			get { return m_Overseeing; }
			set { m_Overseeing = value; InvalidateProperties(); }
		}

		public void Begin()
		{
			if ( !m_Enable )
			{
				m_Enable = true;
				DoTimer();
			}
		}

		public void End()
		{
			if ( m_Enable )
			{
				m_Timer.Stop();
				m_Enable = false;
			}
		}

		public void Restart()
		{
			if ( m_Enable )
			{
				m_Timer.Stop();
				DoTimer();
			}
		}

		public void DoTimer()
		{
			if ( !m_Enable )
				return;

			int mSeconds = (int)m_CurrentDelay.TotalSeconds;

			TimeSpan delay = TimeSpan.FromSeconds( mSeconds );
			DoTimer( delay );
		}

		public void DoTimer( TimeSpan delay )
		{
			if ( !m_Enable )
				return;

			if ( m_Timer != null )
				m_Timer.Stop();

			m_End = DateTime.Now + delay;

			m_Timer = new CheckTimer( this, delay );
			m_Timer.Start();
		}

		[Constructable]
		public SpawnsOverseer() : this( 20, 30, 5, 0 )
		{
		}

		[Constructable]
		public SpawnsOverseer( int startrange ) : this( startrange, 30, 5, 0 )
		{
		}
		
		[Constructable]
		public SpawnsOverseer( int startrange, int startIRD, int startORD, int startOverseeing ) : base( 0x1F1E )
		{
			InitSeer( startrange, startIRD, startORD, startOverseeing );
		}
		
		private void InitSeer( int startrange, int startIRD, int startORD, int startOverseeing )
		{
			Name = "Spawns' Overseer";
			Movable = false;
			Light = LightType.Circle150;
			Weight = 1;
			Visible = false;
			Enable = true;
			Gatekeeper = false;
			Range = startrange;
			CurrentDelay = TimeSpan.FromSeconds( 5 );
			InRangeDelay = startIRD; //minutes
			OutRangeDelay = startORD; //seconds
			Overseeing = startOverseeing; //PremiumSpawners under control
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Enable )
			{
				list.Add( 1060742 ); // active

				list.Add( 1060662, "Range\t{0}", Range.ToString() );
				list.Add( 1060663, "In Range Delay\t{0} min", InRangeDelay.ToString() );
				list.Add( 1060661, "Out Range Delay\t{0} sec", OutRangeDelay.ToString() );
				list.Add( 1060658, "Overseeing\t{0} PremiumSpawners", Overseeing.ToString() );
			}
			else
			{
				list.Add( 1060743 ); // inactive
			}
		}

		private class CheckTimer : Timer
		{
			private SpawnsOverseer m_SpawnsOverseer;

			public CheckTimer( SpawnsOverseer Crystal, TimeSpan delay ) : base( delay, delay )
			{
				Priority = TimerPriority.OneSecond;

				m_SpawnsOverseer = Crystal;
			}

			protected override void OnTick()
			{
				if ( ( m_SpawnsOverseer != null ) && ( !m_SpawnsOverseer.Deleted ) )
				{
					m_SpawnsOverseer.OnTickDoThis();
				}

				else
				{
					Stop();
				}
			}
		}

		public void OnTickDoThis()
		{

			if ( this.Gatekeeper == false) // Gatekeeper mode OFF
			{
				List<Item> ClosePremiumSpawners = new List<Item>();

				List<Mobile> ClosePlayers = new List<Mobile>();

				List<Mobile> MobsCleaning = new List<Mobile>();

				List<Item> ItemsCleaning = new List<Item>();

				foreach ( Item item in this.GetItemsInRange( Range ) )
				{
					if( item is PremiumSpawner )
						ClosePremiumSpawners.Add( item );
				}

				if ( ClosePremiumSpawners.Count > 0 )
				{
					this.Overseeing = ClosePremiumSpawners.Count;
			
					foreach ( Mobile m in this.GetMobilesInRange( Range ) )
					{
						if( m is PlayerMobile && m.AccessLevel == AccessLevel.Player || m is PlayerMobile && m.AccessLevel > AccessLevel.Player && m.Hidden == false ) // if player or GM not hidden
							ClosePlayers.Add( m );
					}

					if ( ClosePlayers.Count > 0 ) // at least one player close
					{
						this.CurrentDelay = TimeSpan.FromMinutes( InRangeDelay ); // time to new check
						Restart();

						foreach ( Item pspawner in ClosePremiumSpawners ) // for each PremiumSpawner close (in the List)
						{
							if ( ((PremiumSpawner)pspawner).Running == false ) // if it is inactive
							{
								((PremiumSpawner)pspawner).Running = true; // activate it!
								((PremiumSpawner)pspawner).NextSpawn = TimeSpan.FromSeconds( 1 ); // and do a total respawn of it!
							}
						}
					}

					else if ( ClosePlayers.Count <= 0 ) // nobody close
					{
						this.CurrentDelay = TimeSpan.FromSeconds( OutRangeDelay ); // time to new check
						Restart();

						foreach ( Item pspawner in ClosePremiumSpawners ) // for each PremiumSpawner close (in the List)
						{
							if ( ((PremiumSpawner)pspawner).Running == true ) // if it is active
							{
								((PremiumSpawner)pspawner).Running = false; // make inactive!

								// NOW delete items and mobiles (not only the spawned, but all movable in range)

								foreach ( Mobile mobdel in this.GetMobilesInRange( Range ) )
								{
									if( mobdel is BaseCreature || mobdel is TownCrier )
										MobsCleaning.Add( mobdel );
								}

								if ( MobsCleaning.Count > 0 )
								{
									foreach ( Mobile mDel in MobsCleaning )
										mDel.Delete();
								}

								foreach ( Item itemdel in this.GetItemsInRange( Range ) )
								{
									if( itemdel.Movable == true ) //se for um item móvel (não decoração)
										ItemsCleaning.Add( itemdel );
								}

								if ( ItemsCleaning.Count > 0 )
								{
									foreach ( Item iDel in ItemsCleaning )
										iDel.Delete();
								}
							}
						}
					}
				}

				else if ( ClosePremiumSpawners.Count <= 0 ) // if SpawnsOverseer has no PremiumSpawner to seer, deactivate it
				{
					this.Enable = false;
					this.CurrentDelay = TimeSpan.FromSeconds( OutRangeDelay );
					this.Overseeing = ClosePremiumSpawners.Count;
				}
			}
			
			else // Gatekeeper mode ON
			{
				
			}
		}

		public SpawnsOverseer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			writer.Write( m_Range );
			writer.Write( m_InRangeDelay );
			writer.Write( m_OutRangeDelay );
			writer.Write( m_Overseeing );
			writer.Write( m_Gatekeeper );
			writer.Write( m_Enable );
			if ( m_Enable )
				writer.WriteDeltaTime( m_End );
		}

		public override void Deserialize( GenericReader reader )

		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Range = reader.ReadInt();
			m_InRangeDelay = reader.ReadInt();
			m_OutRangeDelay = reader.ReadInt();
			m_Overseeing = reader.ReadInt();
			m_Gatekeeper = reader.ReadBool();
			m_Enable = reader.ReadBool();
			TimeSpan ts = TimeSpan.Zero;
			if ( m_Enable )
				ts = reader.ReadDeltaTime() - DateTime.Now;
				DoTimer( ts );
		}
	}
}
