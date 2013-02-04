using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a spider corpse" )]
	public class NavreyNightEyes : BaseCreature
	{
		[Constructable]
		public NavreyNightEyes () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Navrey Night-Eyes";
			Body = 735;
			BaseSoundID = 1170;

			SetStr( 1000, 1500 );
			SetDex( 190, 250 );
			SetInt( 170, 200 );

			SetHits( 30000, 30000 );
			SetStam( 200, 250 );
			SetMana( 150, 200 );

			SetDamage( 22, 29 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 25 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 50, 60 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 60, 80 );

			SetSkill( SkillName.Wrestling, 90.0, 100.7 );
			SetSkill( SkillName.Tactics, 90.0, 100.0 );
			SetSkill( SkillName.MagicResist, 100.0, 130.0 );
			SetSkill( SkillName.Poisoning, 100.0 );
			SetSkill( SkillName.Magery, 90.0, 100.0 );
			SetSkill( SkillName.EvalInt, 90.0, 100.0 );
			SetSkill( SkillName.Meditation, 80.0, 100.0 );
			SetSkill( SkillName.Anatomy, 50.0, 80.0 );
			
			PackItem( new SpidersSilk( 8 ) );

            Fame = 24000;
            Karma = -24000;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 4 );
		}		
				
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.ParalyzingBlow;
		}

		public NavreyNightEyes( Serial serial ) : base( serial )
		{
		}
            public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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
