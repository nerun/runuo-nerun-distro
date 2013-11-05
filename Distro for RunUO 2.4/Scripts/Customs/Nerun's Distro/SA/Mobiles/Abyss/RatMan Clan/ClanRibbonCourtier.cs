using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a ratman's corpse" )]
	public class ClanRibbonCourtier : BaseCreature
	{
		public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Ratman; } }

		[Constructable]
		public ClanRibbonCourtier() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = ( "Clan Ribbon Courtier" );
			Body = 42;
			BaseSoundID = 437;

			SetStr( 220, 250 );
			SetDex( 240, 260 );
			SetInt( 100, 150 );

			SetHits( 2000, 2100 );

			SetDamage( 7, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 100.1, 120.0 );
			SetSkill( SkillName.Tactics, 50.1, 75.0 );
			SetSkill( SkillName.Wrestling, 50.1, 75.0 );

			Fame = 1500;
			Karma = -1500;

			Hue = 258;
			
			VirtualArmor = 28;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
			// TODO: weapon, misc
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Hides{ get{ return 9; } }
		public override HideType HideType{ get{ return HideType.Spined; } }

		public ClanRibbonCourtier( Serial serial ) : base( serial )
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