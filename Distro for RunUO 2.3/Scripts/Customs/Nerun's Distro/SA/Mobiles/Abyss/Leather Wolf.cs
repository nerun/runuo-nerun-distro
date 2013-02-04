using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a leather wolf corpse" )]
	public class LeatherWolf : BaseCreature
	{
		[Constructable]
		public LeatherWolf() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a leather wolf";
			Body = 739;
			Hue = 2213;//guessing
			BaseSoundID = 0xE5;

			SetStr( 102 );
			SetDex( 120 );
			SetInt( 34 );

			SetHits( 180 );
			SetMana( 20 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 10, 15 );
			SetResistance( ResistanceType.Cold, 20, 25 );
			SetResistance( ResistanceType.Poison, 10, 15 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 20.1, 35.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 16;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 0.1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 6; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Canine; } }

		public LeatherWolf(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}