using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a slasher of veils corpse" )]
	public class SlasherOfVeils : BaseCreature
	{
		[Constructable]
		public SlasherOfVeils() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "The Slasher of Veils";
			Body = 741; 

			SetStr( 949, 1039 );
			SetDex( 133, 142 );
			SetInt( 1041, 1256 );

			SetHits( 100000, 150000 );
			SetStam( 133, 142 );
			SetMana( 480, 500 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 66, 77 );
			SetResistance( ResistanceType.Fire, 70, 78 );
			SetResistance( ResistanceType.Cold, 77, 80 );
			SetResistance( ResistanceType.Poison, 72, 75 );
			SetResistance( ResistanceType.Energy, 76, 78 );

			SetSkill( SkillName.Anatomy, 113.9, 125.0 );
			SetSkill( SkillName.EvalInt, 111.1, 123.5 );
			SetSkill( SkillName.Magery, 115.3, 119 );
			SetSkill( SkillName.Meditation, 118.2, 127.8 );
			SetSkill( SkillName.MagicResist, 125.7, 188.7 );
			SetSkill( SkillName.Tactics, 124.0, 126.8 );
			SetSkill( SkillName.Wrestling, 118.2, 124.0 );
		}
		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 1 );
			AddLoot( LootPack.UltraRich, 2 );
			AddLoot( LootPack.FilthyRich, 3 );
		}

		public override int GetIdleSound() { return 1589; } 
		public override int GetAngerSound() { return 1586; } 
		public override int GetHurtSound() { return 1588; } 
		public override int GetDeathSound()	{ return 1587; }

		public SlasherOfVeils( Serial serial ) : base( serial )
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