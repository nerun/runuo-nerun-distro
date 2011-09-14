/*************************
 *       By Nerun        *
 *      Engine r22       *
 *************************
 */

using System;
using System.IO;
using Server;
using Server.Mobiles;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class SpawnsOverseer : Item
	{
		private int m_Range;
		private int m_InRangeDelay;
		private int m_OutRangeDelay;
		private CheckTimer m_Timer;
		private bool m_Enable;
		private TimeSpan m_CurrentDelay; // players fora da dungeon
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
		public SpawnsOverseer() : base( 7966 )
		{
			Name = "Spawns' Overseer";
			Movable = false;
			Light = LightType.Circle150;
			Weight = 1;
			Visible = false;
			Enable = true;
			Range = 20;
			CurrentDelay = TimeSpan.FromSeconds( 5 );
			InRangeDelay = 30; //minutes
			OutRangeDelay = 5; //seconds
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
			ArrayList ClosePremiumSpawners = new ArrayList();

			ArrayList ClosePlayers = new ArrayList();

			ArrayList MobsCleaning = new ArrayList();

			ArrayList ItemsCleaning = new ArrayList();

			foreach ( Item item in this.GetItemsInRange( Range ) ) // para cada item dentro do raio de alcance
			{
				if( item is PremiumSpawner ) // se for um PremiumSpawner
				{
					ClosePremiumSpawners.Add( item );
				}
			}

			if ( ClosePremiumSpawners.Count > 0 )
			{
				foreach ( Mobile m in this.GetMobilesInRange( Range ) ) // para cada mobile dentro do raio de alcance
				{
					if( m is PlayerMobile && m.AccessLevel == AccessLevel.Player || m is PlayerMobile && m.AccessLevel > AccessLevel.Player && m.Hidden == false ) //se fôr player ou GM não oculto (hidden)
					{
						ClosePlayers.Add( m );
					}
				}

				if ( ClosePlayers.Count > 0 ) // há pelo menos um player próximo
				{
					this.CurrentDelay = TimeSpan.FromMinutes( InRangeDelay ); // tempo para nova checagem
					Restart();

					foreach ( Item pspawner in ClosePremiumSpawners ) // pra cada PremiumSpawner próximo (na lista)
					{
						if ( ((PremiumSpawner)pspawner).Running == false ) // se estiver desativado
						{
							((PremiumSpawner)pspawner).Running = true; // ativar!
							((PremiumSpawner)pspawner).NextSpawn = TimeSpan.FromSeconds( 1 ); // respawn total!
						}
					}
				}

				else if ( ClosePlayers.Count <= 0 ) // não tem ninguém perto
				{
					this.CurrentDelay = TimeSpan.FromSeconds( OutRangeDelay ); // tempo para nova checagem
					Restart();

					foreach ( Item pspawner in ClosePremiumSpawners ) // pra cada Premium PremiumSpawner próximo (na lista)
					{
						if ( ((PremiumSpawner)pspawner).Running == true ) // se estiver ativado
						{
							((PremiumSpawner)pspawner).Running = false; // desativar!

							foreach ( Mobile mobdel in this.GetMobilesInRange( Range ) )
							{
								if( mobdel is BaseCreature || mobdel is TownCrier )
								{
									MobsCleaning.Add( mobdel );
								}
							}

							if ( MobsCleaning.Count > 0 )
							{
								foreach ( Mobile mDel in MobsCleaning )
								{
									mDel.Delete();
								}
							}

							foreach ( Item itemdel in this.GetItemsInRange( Range ) )
							{
								if( itemdel.Movable == true ) //se fôr um item móvel (não decoração)
								{
									ItemsCleaning.Add( itemdel );
								}
							}

							if ( ItemsCleaning.Count > 0 )
							{
								foreach ( Item iDel in ItemsCleaning )
								{
									iDel.Delete();
								}
							}
						}
					}
				}
			}

			else if ( ClosePremiumSpawners.Count <= 0 )
			{
				this.Enable = false;
				this.CurrentDelay = TimeSpan.FromSeconds( OutRangeDelay );
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
			m_Enable = reader.ReadBool();
			TimeSpan ts = TimeSpan.Zero;
			if ( m_Enable )
				ts = reader.ReadDeltaTime() - DateTime.Now;
				DoTimer( ts );
		}
	}
}