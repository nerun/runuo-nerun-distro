using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a black order mage corpse" )] 
      public class DragonsFlameMage : BaseCreature
	{
		private DateTime m_DecayTime;
		private Timer m_Timer;

		public override bool AlwaysMurderer{ get{ return true; } }
            public override bool ShowFameTitle{ get{ return false; } }

		[Constructable]
		public DragonsFlameMage() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Black Order Mage";
			Title = "of the Dragon's Flame Sect";
			Female = Utility.RandomBool();
			Race = Race.Human;
			Hue = Race.RandomSkinHue();
			HairItemID = Race.RandomHair( Female );
			HairHue = Race.RandomHairHue();
			Race.RandomFacialHair( this );
			
			AddItem( new NinjaTabi() );
			AddItem( new FancyShirt( 0x51D ) );
			AddItem( new Hakama( 0x51D ) );
			AddItem( new Kasa( 0x51D ) );


			SetStr( 476, 505 );
			SetDex( 89, 95 );
			SetInt( 301, 325 );

			SetHits( 286, 303 );

			SetDamage( 10, 16 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );
                  
			SetSkill( SkillName.EvalInt, 70.1, 80.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
                  SetSkill( SkillName.MagicResist, 85.1, 95.0 );
			SetSkill( SkillName.Tactics, 70.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );
                  


			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 58;

			

			m_DecayTime = DateTime.Now + TimeSpan.FromMinutes( 10.0 );

			m_Timer = new InternalTimer( this, m_DecayTime );
			m_Timer.Start();
		}

		

		public DragonsFlameMage( Serial serial ) : base( serial )
		{
		}
            public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 4 );
		}
		
		public override void AlterSpellDamageFrom( Mobile from, ref int damage )
		{
			if ( from != null )
				from.Damage( damage / 2, from );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );	
			
//			if ( Utility.RandomDouble() < 0.3 )
//				c.DropItem( new DragonFlameSectBadge() );
						
//			if ( Utility.RandomDouble() < 0.1 )
//				c.DropItem( new ParrotItem() );
		}

		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			base.OnAfterDelete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.WriteDeltaTime( m_DecayTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_DecayTime = reader.ReadDeltaTime();

					m_Timer = new InternalTimer( this, m_DecayTime );
					m_Timer.Start();

					break;
				}
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Mob;

			public InternalTimer( Mobile mob, DateTime end ) : base( end - DateTime.Now )
			{
				m_Mob = mob;
			}

			protected override void OnTick()
			{
				m_Mob.FixedParticles( 14120, 10, 15, 5012, EffectLayer.Waist );
				m_Mob.PlaySound( 510 );
				m_Mob.Delete();
				Stop();
			}
		}
	}
}
