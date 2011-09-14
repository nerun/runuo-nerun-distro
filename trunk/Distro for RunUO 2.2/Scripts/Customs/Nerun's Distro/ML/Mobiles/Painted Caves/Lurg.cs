using System;
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a lurg's corpse" )] 
	public class Lurg : Troglodyte 
	{ 		
		[Constructable] 
		public Lurg() : base() 
		{ 			
			Name = "a lurg";
			Hue = 0x455;
			
			SetStr( 584, 625 );
			SetDex( 163, 176 );
			SetInt( 102, 106 );
			
			SetHits( 3034, 3189 );
			SetStam( 163, 176 );
			SetMana( 102, 106 );

			SetDamage( 12, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 53 );
			SetResistance( ResistanceType.Fire, 45, 47 );
			SetResistance( ResistanceType.Cold, 56, 57 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 41, 54 );

			SetSkill( SkillName.Wrestling, 122.7, 130.5 );	
			SetSkill( SkillName.Tactics, 109.3, 118.5 );
			SetSkill( SkillName.MagicResist, 72.9, 87.6 );
			SetSkill( SkillName.Anatomy, 110.5, 124.0 );
			SetSkill( SkillName.Healing, 93.6, 99.6 );
			
			if ( Paragon.ChestChance > Utility.RandomDouble() )
				PackItem( new ParagonChest( Name, TreasureMapLevel ) );
		}

		public Lurg( Serial serial ) : base( serial )
		{
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 3 );
		}
		
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.CrushingBlow;
		}
		
//OFF		public override bool GivesMinorArtifact{ get{ return true; } }

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