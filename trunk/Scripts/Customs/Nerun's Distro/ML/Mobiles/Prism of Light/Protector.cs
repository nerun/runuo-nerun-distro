using System;
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a human corpse" )] 
	public class Protector : BaseCreature 
	{ 
		public override bool AlwaysMurderer{ get{ return true; } }
		
		[Constructable] 
		public Protector() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 			
			Hue = Race.Human.RandomSkinHue();

			Body = 401;
			Female = true;
			Name = "a protector";
			Title = "the mystic llamaherder";
					
			SetStr( 101, 117 );
			SetDex( 98, 118 );
			SetInt( 55, 69 );
			
			SetHits( 374, 438 );
			SetStam( 98, 118 );
			SetMana( 55, 69 );

			SetDamage( 6, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 33 );
			SetResistance( ResistanceType.Fire, 21, 29 );
			SetResistance( ResistanceType.Cold, 35, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.Wrestling, 70.7, 93.2 );	
			SetSkill( SkillName.Tactics, 82.0, 94.3 );
			SetSkill( SkillName.MagicResist, 50.3, 64.6 );
			SetSkill( SkillName.Anatomy, 74.8, 94.3 );
			
			// outfit
			AddItem( new ThighBoots() );
			AddItem( new DeathShroud( Utility.Random( 1 ) ) );
			
			if ( Utility.RandomBool() )
				PackItem( new RawLambLeg() );
			else
				PackItem( new RawChickenLeg() );
		}

		public Protector( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
/*
			if ( Utility.RandomDouble() < 0.4 )
				c.DropItem( new ProtectorsEssence() );
*/
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