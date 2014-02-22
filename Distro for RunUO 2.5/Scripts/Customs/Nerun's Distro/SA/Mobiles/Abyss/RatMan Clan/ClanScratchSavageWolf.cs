using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a wolf corpse" )]
	[TypeAlias( "Server.Mobiles.ClanScratchSavageWolf" )]
	public class ClanScratchSavageWolf : BaseCreature
	{
		[Constructable]
		public ClanScratchSavageWolf() : base( AIType.AI_Melee,FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Clan Scratch Savage Wolf";
			Body = 23;
			BaseSoundID = 0xE5;

			SetStr( 150, 180 );
			SetDex( 230, 250 );
			SetInt( 36, 60 );

			SetHits( 58, 72 );
			SetStam( 230, 250 );
			SetMana( 35, 60 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 35 );
			SetResistance( ResistanceType.Fire, 30, 45 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 10, 25 );

			SetSkill( SkillName.MagicResist, 40.6, 60.0 );
			SetSkill( SkillName.Tactics, 40.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 22;
			
			Hue = 1172;

			Tamable = false;
			ControlSlots = 1;
			MinTameSkill = 83.1;
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 7; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public ClanScratchSavageWolf(Serial serial) : base(serial)
		{
		}
            public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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