using System; 
using Server.Items; 

namespace Server.Items 
{ 
   	public class Korpre3Dust: Item 
   	{ 
		[Constructable]
		public Korpre3Dust() : this( 1 )
		{
		}

		[Constructable]
		public Korpre3Dust( int amount ) : base( 0x26B8 )
		{
			Stackable = true;
			Weight = 0.0;
			Amount = amount;
			Name = "korpre 3 dust";
			Hue = 1759;
		}

            	public Korpre3Dust( Serial serial ) : base ( serial ) 
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