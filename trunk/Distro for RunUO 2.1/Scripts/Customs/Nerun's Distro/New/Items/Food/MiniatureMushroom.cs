// By Nerun
using System;

namespace Server.Items
{
	public class MiniatureMushroom : Food
	{
		[Constructable]
		public MiniatureMushroom() : base( 0x1125 )
		{
			Name = "Miniature Mushroom";
			Stackable = true;
			Weight = 1.0;
			FillFactor = 1;
			LootType = LootType.Blessed;
		}

		public MiniatureMushroom( Serial serial ) : base( serial )
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