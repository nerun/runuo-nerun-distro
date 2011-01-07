using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a mantra effervescence corpse" )]
	public class MantraEffervescence : BaseCreature
	{
		[Constructable]
		public MantraEffervescence() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a mantra effervescence";
			Body = 0x111;
			BaseSoundID = 0x56E;

			SetStr( 137, 146 );
			SetDex( 124, 130 );
			SetInt( 191, 229 );

			SetHits( 152, 236 );

			SetDamage( 14, 17 );

			SetDamageType( ResistanceType.Physical, 30 );
			SetDamageType( ResistanceType.Energy, 70 );

			SetResistance( ResistanceType.Physical, 61, 65 );
			SetResistance( ResistanceType.Fire, 45, 46 );
			SetResistance( ResistanceType.Cold, 44, 46 );
			SetResistance( ResistanceType.Poison, 53, 59 );
			SetResistance( ResistanceType.Energy, 100 );

			SetSkill( SkillName.Wrestling, 84.2, 85.0 );
			SetSkill( SkillName.Tactics, 81.7, 83.5 );
			SetSkill( SkillName.MagicResist, 106.0, 111.6 );
			SetSkill( SkillName.Magery, 95.6, 108.0 );
			SetSkill( SkillName.EvalInt, 84.6, 90.0 );
			SetSkill( SkillName.Meditation, 91.8, 95.7 );
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 3 );
		}		
		
		public override bool Unprovokable{ get{ return true; } }

		public MantraEffervescence( Serial serial ) : base( serial )
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
