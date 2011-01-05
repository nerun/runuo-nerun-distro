using System;
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a grobu's corpse" )] 
	public class Grobu : BaseCreature 
	{ 		
		[Constructable] 
		public Grobu() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 			
			Name = "a grobu";
			Hue = 0x455;
			Body = 0xD3;
			
			SetStr( 192, 210 );
			SetDex( 132, 150 );
			SetInt( 50, 52 );
			
			SetHits( 1244, 1299 );
			SetStam( 132, 150 );
			SetMana( 8 );

			SetDamage( 6, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 45 );
			SetResistance( ResistanceType.Fire, 20, 32 );
			SetResistance( ResistanceType.Cold, 32, 35 );
			SetResistance( ResistanceType.Poison, 25, 29 );
			SetResistance( ResistanceType.Energy, 22, 34 );

			SetSkill( SkillName.Wrestling, 107.4, 119.0 );	
			SetSkill( SkillName.Tactics, 97.3, 116.5 );
			SetSkill( SkillName.MagicResist, 66.2, 83.7 );

			Fame = 1000;
			Karma = 1000;			
		}

		public Grobu( Serial serial ) : base( serial )
		{
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 3 );
		}		
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );	
			
//			c.DropItem( new GrobusFur() );
		}
		
//OFF		public override bool GivesMinorArtifact{ get{ return true; } }
		public override int Hides{ get{ return 12; } }
		public override int Meat{ get{ return 1; } }

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