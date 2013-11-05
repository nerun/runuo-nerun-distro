using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dryad corpse" )]
	public class DryadA : BaseCreature
	{
		public override bool InitialInnocent{ get{ return true; } }

		[Constructable]
		public DryadA() : base( AIType.AI_Mage, FightMode.Evil, 10, 1, 0.2, 0.4 )
		{
			Name = "a Dryad";
			Body = 266;
			BaseSoundID = 0x467;

			SetStr( 135, 150 );
			SetDex( 153, 166 );
			SetInt( 253, 281 );

			SetHits( 302, 314 );
			SetStam( 153, 166 );
			SetMana( 253, 281 );

			SetDamage( 11, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 41, 50 );
			SetResistance( ResistanceType.Fire, 15, 24 );
			SetResistance( ResistanceType.Cold, 40, 45 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 25, 32 );

			SetSkill( SkillName.Wrestling, 71.5, 77.8 );
			SetSkill( SkillName.Tactics, 70.1, 77.3 );
			SetSkill( SkillName.MagicResist, 100.7, 118.8 );			
			SetSkill( SkillName.Magery, 72.1, 77.3 );
			SetSkill( SkillName.EvalInt, 71.0, 79.5 );
			SetSkill( SkillName.Meditation, 80.1, 89.7 );
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosRich, 3 );
		}
		
		public override double WeaponAbilityChance{ get{ return 0.05; } }
		public override int Meat{ get{ return 1; } }	
/*
		public override WeaponAbility GetWeaponAbility()
		{
			 AreaPeace();
			
			return null;
		}

		public virtual int PeaceRange{ get{ return 5; } }
		public virtual TimeSpan PeaceDuration{ get{ return TimeSpan.FromMinutes( 1 ); } }
		
		public virtual void AreaPeace()
		{
			IPooledEnumerable eable = Map.GetClientsInRange( Location, PeaceRange );
			
			foreach( Server.Network.NetState state in eable )
			{
				if ( state.Mobile is PlayerMobile && state.Mobile.CanSee( this )  )
				{
					PlayerMobile player = (PlayerMobile) state.Mobile;
					
					if ( player.PeacedUntil < DateTime.Now )
					{
						player.PeacedUntil = DateTime.Now + PeaceDuration;
						player.SendLocalizedMessage( 1072065 ); // You gaze upon the dryad's beauty, and forget to continue battling!
					}
				}
			}
		}
*/
		public override int GetDeathSound()	{ return 0x57A; }
		public override int GetAttackSound() { return 0x57B; }
		public override int GetIdleSound() { return 0x57C; }
		public override int GetAngerSound() { return 0x57D; }
		public override int GetHurtSound() { return 0x57E; }
		
		public DryadA( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}
