using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a PitFiend corpse" )]
	public class PitFiend : BaseCreature
	{
		[Constructable]
		public PitFiend () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a pit fiend";
			Body = 40;
			Hue = 1136;
			BaseSoundID = 357;

			SetStr( 382 );
			SetDex( 178 );
			SetInt( 212 );

			SetHits( 237 );

			SetDamage( 8, 19 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Cold, 80 );

			SetResistance( ResistanceType.Physical, 65 );
			SetResistance( ResistanceType.Fire, 10 );
			SetResistance( ResistanceType.Cold, 69 );
			SetResistance( ResistanceType.Poison, 24 );
			SetResistance( ResistanceType.Energy, 38 );

			SetSkill( SkillName.Anatomy, 25.1, 50.0 );
			SetSkill( SkillName.EvalInt, 112.2 );
			SetSkill( SkillName.Magery, 105.5 );
			SetSkill( SkillName.Meditation, 8.5 );
			SetSkill( SkillName.MagicResist, 107.0 );
			SetSkill( SkillName.Tactics, 108.9 );
			SetSkill( SkillName.Wrestling, 105.2 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 90;

			PackItem( new Longsword() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		public override int Meat{ get{ return 1; } }

		public PitFiend( Serial serial ) : base( serial )
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