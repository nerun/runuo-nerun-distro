using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Lady Sabrix corpse" )] 
	public class LadySabrix : BaseCreature
	{
		[Constructable]
		public LadySabrix() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Lady Sabrix";
			Body =  0x9D;
			Hue = 0x497;
			BaseSoundID = 0x388; 

			SetStr( 82, 130 );
			SetDex( 117, 146 );
			SetInt( 50, 98 );
			
			SetHits( 233, 361 );
			SetStam( 117, 146 );
			SetMana( 50, 98 );

			SetDamage( 20, 25 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 41, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 39 );
			SetResistance( ResistanceType.Poison, 70, 80 );
			SetResistance( ResistanceType.Energy, 44, 35 );

			SetSkill( SkillName.Wrestling, 109.8, 122.8 );
			SetSkill( SkillName.Tactics, 102.8, 120.0 );
			SetSkill( SkillName.MagicResist, 81.1, 95.1 );
			SetSkill( SkillName.Anatomy, 68.8, 94.9 );
			SetSkill( SkillName.Poisoning, 97.8, 116.7 );
			
			PackItem( new SpidersSilk( 5 ) );
			PackItem( new LesserPoisonPotion() );
			PackItem( new LesserPoisonPotion() );
		}

		public LadySabrix( Serial serial ) : base( serial )
		{
		}		
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 3 );
		}
		
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.ArmorIgnore;
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
/*
OFF
			if ( Utility.RandomDouble() < 0.2 )	
				c.DropItem( new SabrixsEye() );
				
			if ( Utility.RandomDouble() < 0.25 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: AddToBackpack( new PaladinArms() ); break;
					case 1: AddToBackpack( new HunterLegs() ); break;
				}
			}

			if ( Utility.RandomDouble() < 0.1 )
				c.DropItem( new ParrotItem() );
*/
		}

//OFF		public override bool GivesMinorArtifact{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }

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
