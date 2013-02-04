using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.ContextMenus;
using Server.Gumps;

namespace Server.Mobiles
{
	[CorpseName( "a sphynx corpse" )]
	public class Sphynx : BaseCreature
	{
		[Constructable]
		public Sphynx() : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Body = 788;
			Name = "Sphynx";

			SetStr( 1001, 1200 );
			SetDex( 176, 195 );
			SetInt( 301, 400 );
			SetHits( 1001, 1200 );
			SetDamage( 10, 15 );
			SetMana( 301, 400 );

			SetDamageType( ResistanceType.Physical, 85 );
			SetDamageType( ResistanceType.Energy, 15 );

			SetResistance( ResistanceType.Physical, 60, 80 );
			SetResistance( ResistanceType.Fire, 30, 50 );
			SetResistance( ResistanceType.Cold, 40, 60 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Wrestling, 90.1, 100 );
			SetSkill( SkillName.Tactics, 90.1, 100 );
			SetSkill( SkillName.MagicResist, 100.5, 150 );
			SetSkill( SkillName.Anatomy, 25.1, 50 );
			SetSkill( SkillName.Magery, 95.5, 100 );
			SetSkill( SkillName.EvalInt, 90.1, 100 );
			SetSkill( SkillName.Meditation, 95.1, 120 );

			VirtualArmor = 78;
			Fame = 15000;
			Karma = 0;

			PackGold( 845, 1250 );
		}

		public override void GenerateLoot()
		{
			//AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
/*
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			list.Add( new FortuneEntry( from ) );
		}

		public class FortuneEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;

			public FortuneEntry( Mobile from ) : base( 6199, 8 )
			{
				m_Mobile = from;
			}

			public override void OnClick()
			{
				m_Mobile.SendGump( new FortuneGump( m_Mobile ) );
			}
		}
*/
		public Sphynx( Serial serial ) : base( serial )
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