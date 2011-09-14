using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class Spellbinder : BaseCreature
	{
		[Constructable]
		public Spellbinder() : base( AIType.AI_Spellbinder, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Spectral Spellbinder";
			Body = 153;
			BaseSoundID = 0x482;

			SetStr( 76, 100 );
			SetDex( 76, 95 );
			SetInt( 36, 60 );

			SetHits( 46, 60 );
			SetMana(100 );

			SetDamage( 0, 1 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 45.1, 60.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 55.0 );
            SetSkill( SkillName.Magery, 70.0, 80.0 );
            SetSkill( SkillName.Meditation, 100.0, 120.0 );
            SetSkill( SkillName.Necromancy, 100.0, 120.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 28;

			PackItem( Loot.RandomWeapon() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }

		public Spellbinder( Serial serial ) : base( serial )
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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