using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a stone slith corpse" )]
	public class StoneSlith : BaseCreature
	{
		[Constructable]
		public StoneSlith() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Stone Slith";
			Body = 734;
			Hue = 2500;//guessing here 

			SetStr( 273 );
			SetDex( 72, 75 );
			SetInt( 80 );

			SetHits( 161 );

			SetDamage( 6, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 35, 45 );
			SetResistance( ResistanceType.Poison, 25, 35 );
			SetResistance( ResistanceType.Energy, 25, 30 );

			SetSkill( SkillName.MagicResist, 59.1, 63.5 );
			SetSkill( SkillName.Tactics, 74.6, 76.4 );
			SetSkill( SkillName.Wrestling, 62.0, 77.1 );

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 65.7;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int Meat{ get{ return 6; } }
		public override int Hides{ get{ return 10; } }

		public StoneSlith( Serial serial ) : base( serial )
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