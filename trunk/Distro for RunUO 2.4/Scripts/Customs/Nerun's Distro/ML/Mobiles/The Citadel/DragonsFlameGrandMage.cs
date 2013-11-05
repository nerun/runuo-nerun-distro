using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a black order grand mage corpse" )] 
	public class DragonsFlameGrandMage : DragonsFlameMage
	{	
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }


            [Constructable]
		public DragonsFlameGrandMage() : base()
		{
			Name = "Black Order Grand Mage";
			Title = "of the Dragon's Flame Sect";
		}

		public DragonsFlameGrandMage( Serial serial ) : base( serial )
		{
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 6 );
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
