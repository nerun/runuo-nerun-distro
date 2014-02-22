using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	public class Georgio : HumanBrigand
	{				
		public override bool InitialInnocent{ get{ return true; } }
		
		[Constructable]
		public Georgio() : base()
		{			
			Name = "Georgio";
			Title = "the brigand";
			Female = false;
			Hue = 0x8412;
			
			while ( Items.Count > 1 )
				if ( !( Items[ 0 ] is Backpack ) )
					Items[ 0 ].Delete();
						
			AddItem( new Sandals( 0x75E ) );
			AddItem( new Shirt() );
			AddItem( new ShortPants( 0x66C ) );
			AddItem( new SkullCap( 0x649 ) );
			AddItem( new Pitchfork() );
			
			HairItemID = 0x203C;
			HairHue = 0x47A;
			FacialHairItemID = 0x204D;
			FacialHairHue = 0x47A;
		}
		
		public Georgio( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( from is PlayerMobile )
				base.OnDamage( amount, from, willKill );
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