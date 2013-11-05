using System; 
using Server.Items; 

namespace Server.Items 
{ 
   	public class Korpre2Dust: Item 
   	{ 
		[Constructable]
		public Korpre2Dust() : this( 1 )
		{
		}

		[Constructable]
		public Korpre2Dust( int amount ) : base( 0x26B8 )
		{
			Stackable = true;
			Weight = 0.0;
			Amount = amount;
			Name = "korpre 2 dust";
			Hue = 1759;
		}

            	public Korpre2Dust( Serial serial ) : base ( serial ) 
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