using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a crystal hydra corpse" )]
	public class CrystalHydra : BaseCreature
	{
		[Constructable]
		public CrystalHydra() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a crystal hydra";
			Body = 0x109;
			Hue = 0x47E;
			BaseSoundID = 362;

			SetStr( 804, 827 );
			SetDex( 103, 119 );
			SetInt( 101, 109 );

			SetHits( 1486, 1500 );

			SetDamage( 21, 26 );

			SetDamageType( ResistanceType.Physical, 5 );
			SetDamageType( ResistanceType.Fire, 5 );
			SetDamageType( ResistanceType.Cold, 80 );
			SetDamageType( ResistanceType.Poison, 5 );
			SetDamageType( ResistanceType.Energy, 5 );

			SetResistance( ResistanceType.Physical, 67, 74 );
			SetResistance( ResistanceType.Fire, 20, 29 );
			SetResistance( ResistanceType.Cold, 87, 98 );
			SetResistance( ResistanceType.Poison, 36, 45 );
			SetResistance( ResistanceType.Energy, 80, 100 );

			SetSkill( SkillName.Wrestling, 100.6, 115.1 );
			SetSkill( SkillName.Tactics, 101.7, 108.1 );
			SetSkill( SkillName.MagicResist, 89.9, 99.5 );
			SetSkill( SkillName.Anatomy, 75.2, 79.1 );
		}
		
		public CrystalHydra( Serial serial ) : base( serial )
		{
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 3 );
		}		
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
/*
			if ( Utility.RandomDouble() < 0.4 )
				c.DropItem( new ShatteredCrystals() );				

			if ( Utility.RandomDouble() < 0.1 )				
				c.DropItem( new ParrotItem() );
*/
		}
		
		#region Breath
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 100; } }		
		public override int BreathEffectHue{ get{ return 0x47E; } }
		public override int BreathEffectSound{ get{ return 0x56D; } }
		public override bool HasBreath{ get{ return true; } } 
		#endregion
		
		public override int Hides{ get{ return 40; } }
		public override int Meat{ get{ return 19; } }
		public override int TreasureMapLevel{ get{ return 5; } }
		
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