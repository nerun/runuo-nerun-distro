using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a wolf spider corpse" )]
	public class WolfSpider : BaseCreature
	{
		[Constructable]
		public WolfSpider() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			ActiveSpeed = 0.1;
			PassiveSpeed = 0.2;
			
			Name = "a wolf spider";
			Body = 736;
			BaseSoundID = 0x388;

			SetStr( 230, 263 );
			SetDex( 145, 152 );
			SetInt( 285, 310 );

			SetHits( 150, 200 );
			SetMana( 285, 310 );

			SetDamage( 15, 18 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Poison, 30 );
			
			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 100, 100 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.Poisoning, 65.0, 80.0 );
			SetSkill( SkillName.MagicResist, 60.0, 90.0 );
			SetSkill( SkillName.Tactics, 84.0, 96.0);
			SetSkill( SkillName.Wrestling, 80.0, 90.0 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 16;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 59.1;

			PackItem( new SpidersSilk( 8 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Arachnid; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Greater; } }

		public WolfSpider( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}