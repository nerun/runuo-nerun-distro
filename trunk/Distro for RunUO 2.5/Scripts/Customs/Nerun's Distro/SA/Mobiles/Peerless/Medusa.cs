using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a medusa corpse" )]
	public class Medusa : BaseCreature
	{
		[Constructable]
		public Medusa() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Medusa";
			Body = 728; 

			SetStr( 1378, 1442 );
			SetDex( 129, 143 );
			SetInt( 575, 671 );

			SetHits( 50000, 55000 );
			SetStam( 129, 143 );
			SetMana( 575, 671 );

			SetDamage( 21, 28 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 55, 65 );
			SetResistance( ResistanceType.Cold, 55, 65 );
			SetResistance( ResistanceType.Poison, 70, 90 );
			SetResistance( ResistanceType.Energy, 65, 75 );

			SetSkill( SkillName.Anatomy, 111.5, 117.9 );
			SetSkill( SkillName.EvalInt, 103.1, 128.5 );
			SetSkill( SkillName.Magery, 114.7, 120.8 );
			SetSkill( SkillName.Meditation, 100.0 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 124.8, 130.5 );
			SetSkill( SkillName.Wrestling, 119.7, 122.9 );
		}

		public override int GetIdleSound() { return 1557; } 
		public override int GetAngerSound() { return 1554; } 
		public override int GetHurtSound() { return 1556; } 
		public override int GetDeathSound()	{ return 1555; }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 2 );
			AddLoot( LootPack.UltraRich, 2 );
			AddLoot( LootPack.FilthyRich, 3 );
		}

		public Medusa( Serial serial ) : base( serial )
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