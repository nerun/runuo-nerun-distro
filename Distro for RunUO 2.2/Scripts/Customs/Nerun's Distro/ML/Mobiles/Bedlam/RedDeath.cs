using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a red death corpse" )]
	public class RedDeath : BaseMount
	{
		[Constructable]
		public RedDeath() : base( "a red death", 793, 0x3EBB, AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.015, 0.075 )
		{
			Hue = 0x21;
			BaseSoundID = 0xE5;

			SetStr( 319, 324 );
			SetDex( 241, 244 );
			SetInt( 242, 255 );

			SetHits( 1540, 1605 );

			SetDamage( 25, 29 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Fire, 75 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			SetResistance( ResistanceType.Fire, 90 );
			SetResistance( ResistanceType.Poison, 100 );

			SetSkill( SkillName.Wrestling, 121.4, 143.7 );
			SetSkill( SkillName.Tactics, 120.9, 142.2 );
			SetSkill( SkillName.MagicResist, 120.1, 142.3 );
			SetSkill( SkillName.Anatomy, 120.2, 144.0 );	
			
			for ( int i = 0; i < 1; i ++ )
				if ( Utility.RandomBool() )
					PackNecroScroll( Utility.RandomMinMax( 5, 9 ) );
				else
					PackScroll( 4, 7 );
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 4 );
		}
		
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.WhirlwindAttack;
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
			
//			c.DropItem( new ResolvesBridle() );
		}
		
//OFF		public override bool GivesMinorArtifact{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool HasBreath{ get{ return true; } }
	
		public RedDeath( Serial serial ) : base( serial )
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

