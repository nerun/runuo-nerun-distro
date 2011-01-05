using System;
using System.Collections;
using Server;
using Server.Targeting;
using Server.Spells;
using Server.Spells.Ninjitsu;
using Server.Network;
using Server.Items;

namespace Server.Mobiles
{
	public class NinjaAI : BaseAI
	{
		public override bool Think()
		{
			try
			{
			if( m_Mobile.Deleted )
				return false;

			if( CheckFlee() )
				return true;

			switch( Action )
			{
				case ActionType.Wander:
				m_Mobile.OnActionWander();
				return DoActionWander();

				case ActionType.Combat:
				m_Mobile.OnActionCombat();
				return DoActionCombat();

				case ActionType.Guard:
				m_Mobile.OnActionGuard();
				return DoActionGuard();

				case ActionType.Flee:
				m_Mobile.OnActionFlee();
				return DoActionFlee();

				case ActionType.Interact:
				m_Mobile.OnActionInteract();
				return DoActionInteract();

				case ActionType.Backoff:
				m_Mobile.OnActionBackoff();
				return DoActionBackoff();

				default:
				return false;
			}
			}
			catch ( Exception e )
			{
				Console.WriteLine( "Catched Exception from EliteNinja when " + Action.ToString() );
				Console.WriteLine( e.ToString() );
				return false;
			}
		}

		private static double shadowJumpChance = 0.3;
		private static double mirrorChance = 0.4;
		private static double kiChance = 0.5;
		private static double focusChance = 0.5;
		private static double hideChance = 0.1;
		private static double turnOrHideChance = 0.05;

		private static double comboChance = 0.7;

		public NinjaAI( BaseCreature m ) : base( m )
		{
		}

		private double MagnitudeBySkill()
		{
			return (m_Mobile.Skills[ SkillName.Ninjitsu ].Value/1000.0);
		}

		private bool CanUseAbility( double limit, int mana, double chance )
		{
			if ( m_Mobile.Skills[ SkillName.Ninjitsu ].Value >= limit && m_Mobile.Mana >= mana )
			{
				if ( (chance + MagnitudeBySkill()) >= Utility.RandomDouble() )
				{
					return true;
				}
			}

			return false;
		}

		private static int[] m_Bodies = new int[]
		{
			0x84,
			0x7A,
			0xF6,
			0x19,
			0xDC,
			0xDA,
			0x51,
			0x15,
			0xD9,
			0xC9,
			0xEE,					
			0xCD
		};

		private static int[] m_Offsets = new int[]
		{
		 	 0, 0,
			-1,-1,
			 0,-1,
			 1,-1,
			-1, 0,
			 1, 0,
			-1,-1,
			 0, 1,
			 1, 1,
		};

		private void ChangeForm( int body )
		{
			m_Mobile.FixedEffect( 0x37C4, 10, 42, 4, 3 );

			m_Mobile.BodyMod = body;
		}

		// Shadowjump 
		private bool PerformShadowjump( Mobile toTarget )
		{
			if ( m_Mobile.Skills[ SkillName.Ninjitsu ].Value < 50.0 )
			{
				return false;
			}

			if ( toTarget != null )
			{
				Map map = m_Mobile.Map;

				if ( map == null )
				{
					return false;
				}

				int px, py, ioffset = 0;

				px = toTarget.X;
				py = toTarget.Y;

				if ( Action == ActionType.Flee )
				{
					double outerradius = m_Mobile.Skills[ SkillName.Ninjitsu ].Value/10.0;
					double radiusoffset = 2.0;
					// random point for direction vector
					int rpx = Utility.Random( 40 ) - 20 + toTarget.X;
					int rpy = Utility.Random( 40 ) - 20 + toTarget.Y;
					// get vector
					int dx = rpx - toTarget.X;
					int dy = rpy - toTarget.Y;
					// get vector's length
					double l = Math.Sqrt( (double) (dx*dx + dy*dy) );

					if ( l == 0 )
					{
						return false;
					}
					// normalize vector
					double dpx = ((double) dx)/l;
					double dpy = ((double) dy)/l;
					// move 
					px += (int) (dpx*(outerradius - radiusoffset) + Math.Sign( dpx )*(radiusoffset + 0.5));
					py += (int) (dpy*(outerradius - radiusoffset) + Math.Sign( dpy )*(radiusoffset + 0.5));
				}
				else
				{
					ioffset = 2;
				}

				for ( int i = ioffset; i < m_Offsets.Length; i += 2 )
				{
					int x = m_Offsets[ i ], y = m_Offsets[ i + 1 ];

					Point3D p = new Point3D( px + x, py + y, 0 );

					LandTarget lt = new LandTarget( p, map );

					if ( m_Mobile.InLOS( lt ) && map.CanSpawnMobile( px + x, py + y, lt.Z ) && !SpellHelper.CheckMulti( p, map ) )
					{
						m_Mobile.Location = new Point3D( lt.X, lt.Y, lt.Z );
						m_Mobile.ProcessDelta();

						return true;
					}
				}
			}

			return false;
		}

		private void PerformHide()
		{
			Effects.SendLocationParticles( EffectItem.Create( m_Mobile.Location, m_Mobile.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
			m_Mobile.PlaySound( 0x22F );
			m_Mobile.Hidden = true;
		}

		private bool PerformFocusAttack()
		{
			if ( Utility.RandomBool() && CanUseAbility( 60.0, 20, focusChance ) )
			{
				SpecialMove.SetCurrentMove( m_Mobile, new FocusAttack() );
				return true;
			}

			return false;
		}

		private bool PerformMirror()
		{
			if ( CanUseAbility( 40.0, 10, mirrorChance ) && (m_Mobile.Followers < m_Mobile.FollowersMax) )
			{
				new MirrorImage( m_Mobile, null ).Cast();
				return true;
			}

			return false;
		}

		public override bool DoActionWander()
		{
			m_Mobile.DebugSay( "I am wandering around." );

			if ( turnOrHideChance > Utility.RandomDouble() && !m_Mobile.Hidden )
			{
				if ( Utility.RandomBool() )
				{
					if ( m_Mobile.BodyMod == 0 )
					{
						ChangeForm( m_Bodies[ Utility.Random( m_Bodies.Length ) ] );
					}
					else
					{
						ChangeForm( 0 );
					}
				}
				else
				{
					PerformHide();
					m_Mobile.UseSkill( SkillName.Stealth );
				}
			}

			if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				m_Mobile.DebugSay( "I have detected {0}, going to try and sneak up on them!", m_Mobile.FocusMob.Name );

				if( m_Mobile.Hidden ) // we are hidden
				{
					if( m_Mobile.GetDistanceToSqrt( m_Mobile.FocusMob ) <= 2 )
					{
						if ( CanUseAbility( 85.0, 30, 1.0 ) )
						{
							SpecialMove.SetCurrentMove( m_Mobile, new DeathStrike() );
							m_Mobile.Combatant = m_Mobile.FocusMob;
							Action = ActionType.Combat;
						}

						else
						{
							m_Mobile.Combatant = m_Mobile.FocusMob;
							Action = ActionType.Combat;
						}
						
					}
					else if( m_Mobile.GetDistanceToSqrt( m_Mobile.FocusMob ) <= 10 )
					{
						//MoveTo( m_Mobile.FocusMob, true, 1 );
						WalkMobileRange(m_Mobile.FocusMob, 10, true, 2, 1);
					}
				}

				else
				{
					m_Mobile.Combatant = m_Mobile.FocusMob;
					Action = ActionType.Combat;
				}

				
			}
			else
				return base.DoActionWander();

			return true;
		}

		public override bool DoActionCombat()
		{
			if ( m_Mobile.BodyMod != 0 )
			{
				ChangeForm( 0 );
			}

			Mobile combatant = m_Mobile.Combatant;

			if ( combatant == null || combatant.Deleted || combatant.Map != m_Mobile.Map || !combatant.Alive || combatant.IsDeadBondedPet )
			{
				m_Mobile.DebugSay( "My combatant is gone, so my guard is up" );

				Action = ActionType.Guard;

				return true;
			}

			/*if ( !m_Mobile.InLOS( combatant ) )
			{
				if ( AquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
				{
					m_Mobile.Combatant = combatant = m_Mobile.FocusMob;
					m_Mobile.FocusMob = null;
				}
			}*/

			if ( MoveTo( combatant, true, m_Mobile.RangeFight ) )
			{
				m_Mobile.Direction = m_Mobile.GetDirectionTo( combatant );
			}
			else if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				m_Mobile.DebugSay( "My move is blocked, so I am going to attack {0}", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;

				return true;
			}
			else if ( m_Mobile.GetDistanceToSqrt( combatant ) > m_Mobile.RangePerception + 1 )
			{
				m_Mobile.DebugSay( "I cannot find {0}, so my guard is up", combatant.Name );

				Action = ActionType.Guard;

				return true;
			}
			else
			{
				m_Mobile.DebugSay( "I should be closer to {0}", combatant.Name );
			}

			if ( !m_Mobile.Controlled && !m_Mobile.Summoned )
			{
				if ( m_Mobile.Hits < m_Mobile.HitsMax*20/100 )
				{
					// We are low on health, should we flee?

					bool flee = false;

					if ( m_Mobile.Hits < combatant.Hits )
					{
						// We are more hurt than them

						int diff = combatant.Hits - m_Mobile.Hits;

						flee = (Utility.Random( 0, 100 ) < (10 + diff)); // (10 + diff)% chance to flee
					}
					else
					{
						flee = Utility.Random( 0, 100 ) < 10; // 10% chance to flee
					}

					if ( flee )
					{
						if ( m_Mobile.Debug )
						{
							m_Mobile.DebugSay( "I am going to flee from {0}", combatant.Name );
						}

						Action = ActionType.Flee;

						if ( CanUseAbility( 50.0, 15, shadowJumpChance ) )
						{
							PerformHide();

							m_Mobile.UseSkill( SkillName.Stealth );

							if ( m_Mobile.AllowedStealthSteps != 0 )
							{
								if ( PerformShadowjump( combatant ) )
								{
									m_Mobile.Mana -= 15;
								}
							}
						}
					}
				}
			}

			if ( combatant.Hits < (Utility.Random( 10 ) + 10) )
			{
				if ( CanUseAbility( 85.0, 30, 1.0 ) )
				{
					SpecialMove.SetCurrentMove( m_Mobile, new DeathStrike() );
					return true;
				}
			}

			double dstToTarget = m_Mobile.GetDistanceToSqrt( combatant );

			if ( dstToTarget > 2.0 && dstToTarget <= (m_Mobile.Skills[ SkillName.Ninjitsu ].Value/10.0) && m_Mobile.Mana > 45 && comboChance > (Utility.RandomDouble() + MagnitudeBySkill()) )
			{
				PerformHide();

				m_Mobile.UseSkill( SkillName.Stealth );

				if ( m_Mobile.AllowedStealthSteps != 0 )
				{
					if ( CanUseAbility( 20.0, 30, 1.0 ) )
					{
						SpecialMove.SetCurrentMove( m_Mobile, new Backstab() );
					}

					if ( CanUseAbility( 30.0, 20, 1.0 ) )
					{
						SpecialMove.SetCurrentMove( m_Mobile, new SurpriseAttack() );
					}

					PerformFocusAttack();

					if ( PerformShadowjump( combatant ) )
					{
						m_Mobile.Mana -= 15;
					}
				}

				return true;
			}

			if ( PerformMirror() )
			{
				return true;
			}

			if ( CanUseAbility( 80.0, 25, kiChance ) && m_Mobile.GetDistanceToSqrt( combatant ) < 2.0 )
			{
				SpecialMove.SetCurrentMove( m_Mobile, new KiAttack() );
				return true;
			}

			PerformFocusAttack();

			return true;
		}

		public override bool DoActionGuard()
		{
			if ( AcquireFocusMob( m_Mobile.RangePerception, m_Mobile.FightMode, false, false, true ) )
			{
				m_Mobile.DebugSay( "I have detected {0}, attacking", m_Mobile.FocusMob.Name );

				m_Mobile.Combatant = m_Mobile.FocusMob;
				Action = ActionType.Combat;
			}
			else
			{
				base.DoActionGuard();
			}

			return true;
		}

		public override bool DoActionFlee()
		{
			if ( m_Mobile.Hits > m_Mobile.HitsMax/2 )
			{
				m_Mobile.DebugSay( "I am stronger now, so I will continue fighting" );
				Action = ActionType.Combat;
			}
			else
			{
				m_Mobile.FocusMob = m_Mobile.Combatant;

				if ( m_Mobile.FocusMob == null || m_Mobile.FocusMob.Deleted || m_Mobile.FocusMob.Map != m_Mobile.Map )
				{
					m_Mobile.DebugSay( "I have lost im" );
					Action = ActionType.Guard;
					return true;
				}

				if ( !m_Mobile.Hidden )
				{
					PerformMirror();

					if ( hideChance > (Utility.RandomDouble() + MagnitudeBySkill()) )
					{
						PerformHide();

						m_Mobile.UseSkill( SkillName.Stealth );
					}
				}

				if ( WalkMobileRange( m_Mobile.FocusMob, 1, false, m_Mobile.RangePerception*2, m_Mobile.RangePerception*3 ) )
				{
					m_Mobile.DebugSay( "I Have fled" );
					Action = ActionType.Guard;
					return true;
				}
				else
				{
					m_Mobile.DebugSay( "I am fleeing!" );
				}
			}

			return true;
		}
	}
}
