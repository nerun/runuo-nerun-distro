using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a sentinel spider spider corpse" )] 
	public class SentinelSpider : BaseCreature
	{
		[Constructable]
		public SentinelSpider() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a sentinel spider";
			Body =  0x9D;
			BaseSoundID = 0x388; // TODO: validate

			SetStr( 113, 118 );
			SetDex( 150, 155 );
			SetInt( 88, 90 );

			SetHits( 336, 336 );

			SetDamage( 15, 22 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 47, 50 );
			SetResistance( ResistanceType.Fire, 36, 40 );
			SetResistance( ResistanceType.Cold, 33, 35 );
			SetResistance( ResistanceType.Poison, 76, 80 );
			SetResistance( ResistanceType.Energy, 34, 40 );

			SetSkill( SkillName.Anatomy, 96.7, 97.0 );
			SetSkill( SkillName.Poisoning, 107.8, 108.0 );
			SetSkill( SkillName.MagicResist, 89.5, 90.0 );
			SetSkill( SkillName.Tactics, 107.7, 108.0 );
			SetSkill( SkillName.Wrestling, 109.8, 110.0 );
			SetSkill( SkillName.DetectHidden, 120.4, 139.6);

			Fame = 18900;
			Karma = -3500;

			VirtualArmor = 24;

			PackItem( new SpidersSilk( 10 ) );
			PackItem( new LesserPoisonPotion() );
			PackItem( new LesserPoisonPotion() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
		}

		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }

		public SentinelSpider( Serial serial ) : base( serial )
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