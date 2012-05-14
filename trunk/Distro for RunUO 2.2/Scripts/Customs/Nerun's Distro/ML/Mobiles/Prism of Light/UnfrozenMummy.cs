using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a mummy corpse" )]
	public class UnfrozenMummy : BaseCreature
	{
		[Constructable]
		public UnfrozenMummy() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			Name = "an unfrozen mummy";
			Body = 0x9A;
			Hue = 0x480;
			BaseSoundID = 0x1D7;

			SetStr( 498 );
			SetDex( 211 );
			SetInt( 849 );

			SetHits( 1500 );

			SetDamage( 16, 20 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Energy, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 36, 40 );
			SetResistance( ResistanceType.Fire, 22, 29 );
			SetResistance( ResistanceType.Cold, 60, 78 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 69, 78 );

			SetSkill( SkillName.Wrestling, 90.7, 96.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.MagicResist, 250.0 );
			SetSkill( SkillName.Magery, 51.0, 57.3 );
			SetSkill( SkillName.EvalInt, 52.8, 59.5 );
			SetSkill( SkillName.Meditation, 80.1, 80.8 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 3 );
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
/*
			if ( Utility.RandomDouble() < 0.6 )
				c.DropItem( new BrokenCrystals() );

			if ( Utility.RandomDouble() < 0.1 )				
				c.DropItem( new ParrotItem() );
*/
		}

		public UnfrozenMummy( Serial serial ) : base( serial )
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