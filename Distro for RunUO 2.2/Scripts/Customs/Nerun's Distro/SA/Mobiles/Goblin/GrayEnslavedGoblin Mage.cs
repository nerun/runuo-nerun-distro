using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a goblin corpse" )]
	public class EnslavedGoblinMage : BaseCreature
	{
		[Constructable]
		public EnslavedGoblinMage() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an enslaved goblin mage";
			Body = 723;
			Hue = 2500;
			BaseSoundID = 0x45A;

			SetStr( 294 );
			SetDex( 79 );
			SetInt( 488 );

			SetHits( 154 );
			SetStam( 79 );
			SetMana( 488 );

			SetDamage( 5, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 29 );
			SetResistance( ResistanceType.Fire, 36 );
			SetResistance( ResistanceType.Cold, 34 );
			SetResistance( ResistanceType.Poison, 41 );
			SetResistance( ResistanceType.Energy, 20 );

			SetSkill( SkillName.EvalInt, 107.7 );
			SetSkill( SkillName.Magery, 104.4 );
			SetSkill( SkillName.MagicResist, 145.2 );
			SetSkill( SkillName.Tactics, 89.2 );
			SetSkill( SkillName.Anatomy, 87.0 );
			SetSkill( SkillName.Wrestling, 93.4 );

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 28;

			switch ( Utility.Random( 20 ) )
			{
				case 0: PackItem( new Scimitar() ); break;
				case 1: PackItem( new Katana() ); break;
				case 2: PackItem( new WarMace() ); break;
				case 3: PackItem( new WarHammer() ); break;
				case 4: PackItem( new Kryss() ); break;
				case 5: PackItem( new Pitchfork() ); break;
			}

			PackItem( new ThighBoots() );

			switch ( Utility.Random( 3 ) )
			{
				case 0: PackItem( new Ribs() ); break;
				case 1: PackItem( new Shaft() ); break;
				case 2: PackItem( new Candle() ); break;
			}

			if ( 0.2 > Utility.RandomDouble() )
				PackItem( new BolaBall() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
		public override int Meat{ get{ return 1; } }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.SavagesAndOrcs; }
		}

		public EnslavedGoblinMage( Serial serial ) : base( serial )
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