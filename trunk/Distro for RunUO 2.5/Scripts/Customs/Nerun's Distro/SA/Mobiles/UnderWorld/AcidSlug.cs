using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "an acid slug corpse" )]
	public class AcidSlug : BaseCreature//, IAcidCreature
	{
		[Constructable]
		public AcidSlug() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an acid slug";
			Body = 51;
            Hue = 44;

			SetStr( 213, 294 );
			SetDex( 80, 82 );
			SetInt( 18, 22 );

			SetHits( 333, 370 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 10, 15 );
			SetResistance( ResistanceType.Fire, 0 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 10, 15 );

			SetSkill( SkillName.MagicResist, 25.0 );
			SetSkill( SkillName.Tactics, 50.0 );
			SetSkill( SkillName.Wrestling, 80.0 );

			if ( 0.1 > Utility.RandomDouble() )
				PackItem( new VialOfVitriol() );
				
			if ( 0.75 > Utility.RandomDouble() )
				PackItem( new AcidSac() );
			
			//PackItem( new CongealedSlugAcid() );
			
			Fame = 1000;
			Karma = -1000;
			
			Tamable = false;
		}
		
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override int GetIdleSound() { return 1499; } 
		public override int GetAngerSound() { return 1496; } 
		public override int GetHurtSound() { return 1498; } 
		public override int GetDeathSound()	{ return 1497; }

		public AcidSlug( Serial serial ) : base( serial )
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