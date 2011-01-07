using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a travesty's corpse" )]
	public class Travesty : BaseCreature
	{
		[Constructable]
		public Travesty() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a travesty";
			Body = 0x108;

			SetStr( 909, 949 );
			SetDex( 901, 948 );
			SetInt( 903, 947 );

			SetHits( 35000 );

			SetDamage( 25, 30 );
			
			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 52, 67 );
			SetResistance( ResistanceType.Fire, 51, 68 );
			SetResistance( ResistanceType.Cold, 51, 69 );
			SetResistance( ResistanceType.Poison, 51, 70 );
			SetResistance( ResistanceType.Energy, 50, 68 );

			SetSkill( SkillName.Wrestling, 100.1, 119.7 );
			SetSkill( SkillName.Tactics, 102.3, 118.5 );
			SetSkill( SkillName.MagicResist, 101.2, 119.6 );
			SetSkill( SkillName.Anatomy, 100.1, 117.5 );

			Fame = 8000;
			Karma = -8000;

			VirtualArmor = 50;
			
			PackItem( new GnarledStaff() );
			PackNecroReg( 15, 25 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 8 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public Travesty( Serial serial ) : base( serial )
		{
		}

		public override int GetIdleSound()
		{
			return 0x1BF;
		}

		public override int GetAttackSound()
		{
			return 0x1C0;
		}

		public override int GetHurtSound()
		{
			return 0x1C1;
		}

		public override int GetDeathSound()
		{
			return 0x1C2;
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