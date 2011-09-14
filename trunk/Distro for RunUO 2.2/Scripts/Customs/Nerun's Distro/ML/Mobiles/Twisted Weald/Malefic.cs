using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dread spider corpse" )]
	public class Malefic : BaseCreature
	{
		[Constructable]
		public Malefic () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.01, 0.4 )
		{
			Name = "a Malefic";
			Body = 0x9D;
			Hue = 0x497;
			BaseSoundID = 0x388; 

			SetStr( 210, 284 );
			SetDex( 153, 197 );
			SetInt( 349, 390 );

			SetHits( 600, 747 );
			SetStam( 153, 197 );
			SetMana( 349, 390 );

			SetDamage( 20, 25 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Poison, 80 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			SetResistance( ResistanceType.Fire, 41, 50 );
			SetResistance( ResistanceType.Cold, 40, 49 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 41, 48 );
			
			SetSkill( SkillName.Wrestling, 96.9, 112.4 );
			SetSkill( SkillName.Tactics, 91.3, 105.4 );
			SetSkill( SkillName.MagicResist, 79.8, 95.1 );
			SetSkill( SkillName.Magery, 103.0, 118.6 );
			SetSkill( SkillName.EvalInt, 105.7, 119.6 );
			
			PackItem( new SpidersSilk( 8 ) );			
/*
			if ( Utility.RandomDouble() < 0.1 )
				PackItem( new ParrotItem() );
*/
		}

		public Malefic( Serial serial ) : base( serial )
		{
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 4 );
		}

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Dismount;
		}
				
//OFF		public override bool GivesMinorArtifact{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		
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
