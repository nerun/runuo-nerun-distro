using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a lady marai corpse" )]
	public class LadyMarai : BaseCreature
	{
		[Constructable]
		public LadyMarai() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.015, 0.075 )
		{
			Name = "a lady marai";
			Hue = 0x76D;
			Body = 0x26;
			BaseSoundID = 0x1C3;

			SetStr( 221, 304 );
			SetDex( 98, 138 );
			SetInt( 54, 99 );

			SetHits( 694, 846 );

			SetDamage( 15, 20 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Cold, 60 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.Wrestling, 126.6-137.2 );
			SetSkill( SkillName.Tactics, 128.7-134.5 );
			SetSkill( SkillName.MagicResist, 102.1-119.1 );
			SetSkill( SkillName.Anatomy, 126.2-136.5 );
			
			AddItem( new PlateLegs() );
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 4 );
		}
		
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.CrushingBlow;
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
/*
OFF
			if ( Utility.RandomDouble() < 0.15 )
				c.DropItem( new DisintegratingThesisNotes() );

			if ( Utility.RandomDouble() < 0.1 )
				c.DropItem( new ParrotItem() );
*/
		}
		
//OFF		public override bool GivesMinorArtifact{ get{ return true; } }
	
		public LadyMarai( Serial serial ) : base( serial )
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

