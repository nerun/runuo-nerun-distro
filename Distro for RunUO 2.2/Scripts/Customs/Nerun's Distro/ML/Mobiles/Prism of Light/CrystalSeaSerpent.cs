using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a crystal sea serpent corpse" )]
	public class CrystalSeaSerpent : BaseCreature
	{
		[Constructable]
		public CrystalSeaSerpent() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a crystal sea serpent";
			Body = 0x96;
			Hue = 0x47E;
			BaseSoundID = 0x1BF;

			SetStr( 259, 411 );
			SetDex( 103, 141 );
			SetInt( 96, 151 );

			SetHits( 231, 323 );

			SetDamage( 10, 18 );

			SetDamageType( ResistanceType.Physical, 10 );
			SetDamageType( ResistanceType.Cold, 45 );
			SetDamageType( ResistanceType.Energy, 45 );

			SetResistance( ResistanceType.Physical, 51, 64 );
			SetResistance( ResistanceType.Cold, 72, 90 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 65, 80 );

			SetSkill( SkillName.MagicResist, 61.5, 74.8 );
			SetSkill( SkillName.Tactics, 60.8, 69.8 );
			SetSkill( SkillName.Wrestling, 60.6, 70.0 );
			
			CanSwim = true;
			CantWalk = true;
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosAverage );
		}		
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
/*
			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new CrushedCrystals() );
				
			if ( Utility.RandomDouble() < 0.3 )
				c.DropItem( new IcyHeart() );
				
			if ( Utility.RandomDouble() < 0.3 )
				c.DropItem( new LuckyDagger() );
*/
		}
		
		#region Breath
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 100; } }		
		public override int BreathEffectHue{ get{ return 0x47E; } }
		public override int BreathEffectSound{ get{ return 0x56D; } }
		public override bool HasBreath{ get{ return true; } } 
		#endregion

		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Horned; } }
		public override int Scales{ get{ return 8; } }
		public override ScaleType ScaleType{ get{ return ScaleType.Blue; } }
		public override int Meat{ get{ return 10; } }
		public override int TreasureMapLevel{ get{ return 3; } }

		public CrystalSeaSerpent( Serial serial ) : base( serial )
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
