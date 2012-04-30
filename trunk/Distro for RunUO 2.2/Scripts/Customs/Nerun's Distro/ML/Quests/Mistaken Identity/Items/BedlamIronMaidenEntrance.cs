// By Nerun
using System;

namespace Server.Items
{
	public class BedlamIronMaiden : Item
	{
		[Constructable]
		public BedlamIronMaiden() : base( 0x124D )
		{
			Name = "iron maiden";
			Stackable = false;
			Weight = 1.0;
			Movable = false;
		}

		public override void OnDoubleClick( Mobile m ) 
		{
			m.X = 120;
			m.Y = 1680;
			m.Z = 0;
			m.Map = Map.Malas;

			m.SendMessage( "Mistaken Identity quest is not implemented yet. So you may enter." );
		}

		public BedlamIronMaiden( Serial serial ) : base( serial )
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