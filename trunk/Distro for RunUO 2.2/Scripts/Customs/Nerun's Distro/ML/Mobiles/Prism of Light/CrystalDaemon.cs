using System;
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a crystal daemon corpse" )] 
	public class CrystalDaemon : BaseCreature 
	{ 		
		[Constructable] 
		public CrystalDaemon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 			
			Hue = Race.Human.RandomSkinHue();

			Body = 0x310;
			Hue = 0x3E8;
			Name = "a crystal daemon";
			BaseSoundID = 0x47D;
					
			SetStr( 142, 190 );
			SetDex( 128, 145 );
			SetInt( 801, 850 );
			
			SetHits( 202, 215 );
			SetStam( 128, 145 );
			SetMana( 801, 850 );

			SetDamage( 16, 20 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 60 );

			SetResistance( ResistanceType.Physical, 20, 35 );
			SetResistance( ResistanceType.Fire, 6, 18 );
			SetResistance( ResistanceType.Cold, 60, 80 );
			SetResistance( ResistanceType.Poison, 31, 40 );
			SetResistance( ResistanceType.Energy, 65, 74 );

			SetSkill( SkillName.Wrestling, 63.1, 79.6 );	
			SetSkill( SkillName.Tactics, 71.2, 79.6 );
			SetSkill( SkillName.MagicResist, 100.4, 108.4 );
			SetSkill( SkillName.Magery, 120.9, 128.5 );
			SetSkill( SkillName.EvalInt, 101.5, 108.7 );
			SetSkill( SkillName.Meditation, 100.2, 109.1 );
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 2 );
		}

		public CrystalDaemon( Serial serial ) : base( serial )
		{
		}		
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );	
/*
			if ( Utility.RandomDouble() < 0.4 )
				c.DropItem( new ScatteredCrystals() );
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