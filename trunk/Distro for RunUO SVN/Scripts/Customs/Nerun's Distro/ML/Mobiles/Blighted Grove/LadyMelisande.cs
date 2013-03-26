using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a lady's corpse" )]
	public class LadyMelisande : BaseCreature
	{
		[Constructable]
		public LadyMelisande() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a lady melisande";
			Body = 0x102;
			BaseSoundID = 451;

			SetStr( 420, 976 );
			SetDex( 306, 327 );
			SetInt( 1588, 1676 );

			SetHits( 30000 );	

			SetDamage( 27, 31 );
			
			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 49, 55 );
			SetResistance( ResistanceType.Fire, 41, 48 );
			SetResistance( ResistanceType.Cold, 57, 63 );
			SetResistance( ResistanceType.Poison, 70, 72 );
			SetResistance( ResistanceType.Energy, 74, 80 );
			
			SetSkill( SkillName.Wrestling, 100.7, 102.0 );
			SetSkill( SkillName.Tactics, 100.1, 101.9 );
			SetSkill( SkillName.MagicResist, 120 );
			SetSkill( SkillName.Magery, 120 );
			SetSkill( SkillName.EvalInt, 120 );
			SetSkill( SkillName.Meditation, 120 );
			SetSkill( SkillName.Necromancy, 120 );
			SetSkill( SkillName.SpiritSpeak, 120 );

			Fame = 18000;
			Karma = -18000;

			VirtualArmor = 50;

			PackItem( new GnarledStaff() );
			PackNecroReg( 50, 80 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 8 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 4; } }

		public LadyMelisande( Serial serial ) : base( serial )
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