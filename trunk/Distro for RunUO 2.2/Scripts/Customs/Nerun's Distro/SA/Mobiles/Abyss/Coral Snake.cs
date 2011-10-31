using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a snake corpse" )]
	public class CoralSnake : BaseCreature
	{
		[Constructable]
		public CoralSnake() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a coral snake";
			Body = 52;
			Hue = 41;
			BaseSoundID = 0xDB;

			SetStr( 205 );
			SetDex( 248 );
			SetInt( 28 );

			SetHits( 132 );
			SetMana( 28 );

			SetDamage( 5, 21 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 42 );
			SetResistance( ResistanceType.Fire, 5 );
			SetResistance( ResistanceType.Cold, 5 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 5 );

			SetSkill( SkillName.Poisoning, 99.7 );
			SetSkill( SkillName.MagicResist, 98.1 );
			SetSkill( SkillName.Tactics, 82.0 );
			SetSkill( SkillName.Wrestling, 90.3 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 16;
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }

		public override bool DeathAdderCharmable{ get{ return true; } }

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Eggs; } }

		public CoralSnake(Serial serial) : base(serial)
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