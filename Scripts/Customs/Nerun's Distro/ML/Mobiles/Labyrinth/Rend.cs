using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a rend corpse" )]	
	public class Rend : Reptalon
	{
		[Constructable]
		public Rend() : base()
		{
			Name = "a rend";
			Hue = 0x455;

			SetStr( 1261, 1284 );
			SetDex( 363, 384 );
			SetInt( 601, 642 );

			SetHits( 5176, 5966 );

			SetDamage( 21, 28 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 75, 85 );
			SetResistance( ResistanceType.Fire, 81, 94 );
			SetResistance( ResistanceType.Cold, 46, 55 );
			SetResistance( ResistanceType.Poison, 35, 44 );
			SetResistance( ResistanceType.Energy, 45, 52 );

			SetSkill( SkillName.Wrestling, 136.3, 150.3 );
			SetSkill( SkillName.Tactics, 133.4, 141.4 );
			SetSkill( SkillName.MagicResist, 90.9, 105.8 );
			SetSkill( SkillName.Anatomy, 66.6, 72.0 );
			
			Tamable = false;	
			
			if ( Paragon.ChestChance > Utility.RandomDouble() )
				PackItem( new ParagonChest( Name, TreasureMapLevel ) );
		}
				
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 4 );
		}
		
		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 2 ) )
			{
				case 0: return WeaponAbility.ParalyzingBlow;
				case 1: return WeaponAbility.BleedAttack;
			}
		
			return null;
		}
		
//OFF		public override bool CanAnimateDead{ get{ return true; } }
//OFF		public override BaseCreature Animates{ get{ return new SkeletalDragon(); } }
//OFF		public override int AnimateScalar{ get{ return 50; } } // dragon loses 50% hits & str

		public Rend( Serial serial ) : base( serial )
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