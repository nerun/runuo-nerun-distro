using System;
using Server.Items;
using Server;
using Server.Misc;

namespace Server.Mobiles
{
	public class GenericGuard : BaseCreature
	{ 
		[Constructable]
		public GenericGuard () : base( AIType.AI_Animal, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			InitStats( 100, 100, 25 ); 

			Hue = Utility.RandomSkinHue(); 

			Female = false;
			this.Body = 0x190;
			this.Name = NameList.RandomName( "male" );
			Title = "the guard";

			HairItemID = 0x203B;
			HairHue = Utility.RandomNeutralHue();
			FacialHairItemID = 0;

			AddItem( new PlateChest() );
			AddItem( new PlateArms() );
			AddItem( new PlateGloves() );
			AddItem( new PlateLegs() );
			
			//Longsword weapon = new Longsword();
			//weapon.Movable = false;
			//AddItem( weapon );
		}

		public override bool ClickTitle{ get{ return false; } }

		public GenericGuard( Serial serial ) : base( serial )
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