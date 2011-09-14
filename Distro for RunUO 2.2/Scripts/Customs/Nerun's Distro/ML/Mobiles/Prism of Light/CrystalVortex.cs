using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a crystal vortex corpse" )]
	public class CrystalVortex : BaseCreature
	{
		[Constructable]
		public CrystalVortex() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a crystal vortex";
			Body = 0xA4;
			Hue = 0x2B2;
			BaseSoundID = 0x107;

			SetStr( 831, 896 );
			SetDex( 542, 595 );
			SetInt( 200 );

			SetHits( 359, 395 );
			SetStam( 450 );

			SetDamage( 20, 30 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Cold, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 60, 77 );
			SetResistance( ResistanceType.Fire, 0, 8 );
			SetResistance( ResistanceType.Cold, 70, 78 );
			SetResistance( ResistanceType.Poison, 40, 49 );
			SetResistance( ResistanceType.Energy, 62, 88 );

			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 3 );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
/*
			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new JaggedCrystals() );			

			if ( Utility.RandomDouble() < 0.1 )
				c.DropItem( new ParrotItem() );
*/
		}

		public override int GetAngerSound() { return 0x15; }
		public override int GetAttackSound() { return 0x28; }

		public CrystalVortex( Serial serial ) : base( serial )
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
