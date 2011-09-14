using System;
using System.Collections;
using System.IO;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	public class Jamal : Fisherman
	{
		[Constructable]
		public Jamal() : base()
		{
			Name = "Jamal";
		}

		public Jamal( Serial serial ) : base( serial )
		{
		}

		public override void InitBody()
		{
			InitStats( 100, 100, 25 );
			
			Female = false;
			Race = Race.Human;
			
			Hue = 0x83FB;
			HairItemID = 0x2049;
			HairHue = 0x45E;
		}
		
		public override void InitOutfit()
		{
			AddItem( new Backpack() );
			AddItem( new ThighBoots( 0x901 ) );
			AddItem( new ShortPants( 0x730 ) );
			AddItem( new Shirt( 0x1BB ) );
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

		public override void OnSpeech( SpeechEventArgs m )
		{
			if ( m.Mobile.InRange( this, 5 ) )
			{
					Say( String.Format( "Hi {0}! My name is Jamal. I am supposed to be a Quest NPC.", m.Mobile.Name ) );
					Say( String.Format( "Sorry, but this Quest is currently not working." ) );
			}
		}
	}
}