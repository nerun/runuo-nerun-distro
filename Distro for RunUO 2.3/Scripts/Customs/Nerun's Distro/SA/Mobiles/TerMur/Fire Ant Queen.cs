using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a fire ant corpse" )]
	public class FireAntQueen : BaseCreature
	{
		[Constructable]
		public FireAntQueen() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fire ant queen";
			Body = 787; 
			Hue = 1161;

			SetStr( 1200 );
			SetDex( 300 );
			SetInt( 200 );

			SetHits( 900 );

			SetDamage( 15, 18 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Fire, 60 );

			SetResistance( ResistanceType.Physical, 52 );
			SetResistance( ResistanceType.Fire, 96 );
			SetResistance( ResistanceType.Cold, 36 );
			SetResistance( ResistanceType.Poison, 40 );
			SetResistance( ResistanceType.Energy, 36 );

			SetSkill( SkillName.Anatomy, 8.7 );
			SetSkill( SkillName.MagicResist, 53.1 );
			SetSkill( SkillName.Tactics, 77.2 );
			SetSkill( SkillName.Wrestling, 75.4 );

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 1 );
		}

		public override int GetIdleSound() { return 846; } 
		public override int GetAngerSound() { return 849; } 
		public override int GetHurtSound() { return 852; } 
		public override int GetDeathSound()	{ return 850; }

		public FireAntQueen( Serial serial ) : base( serial )
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