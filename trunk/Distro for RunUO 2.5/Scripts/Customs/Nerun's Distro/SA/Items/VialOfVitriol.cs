using System;

namespace Server.Items
{
	public class VialOfVitriol : Item
	{
		public override int LabelNumber{ get{ return 1113331; } } // vial of vitriol

		[Constructable]
		public VialOfVitriol() : base(0x5722)
		{
		}

		public VialOfVitriol( Serial serial ) : base( serial )
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