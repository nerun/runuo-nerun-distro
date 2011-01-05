using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles 
{
	public class HirePaladin : BaseHire 
	{
		[Constructable] 
		public HirePaladin()
		{
			SpeechHue = Utility.RandomDyedHue();
			Hue = Utility.RandomSkinHue();

			if ( this.Female = Utility.RandomBool() ) 
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
			}
			else 
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
			}
			Title = "the paladin";
			HairItemID = Race.RandomHair( Female );
			HairHue = Race.RandomHairHue();
			Race.RandomFacialHair( this );

			switch( Utility.Random( 5 ) )
			{
				case 0: break;
				case 1: AddItem( new Bascinet() );break;
				case 2: AddItem( new CloseHelm() );break;
				case 3: AddItem( new NorseHelm() );break;
				case 4: AddItem( new Helmet() );break;
			}

			SetStr( 86, 100 );
			SetDex( 81, 95 );
			SetInt( 61, 75 );

			SetDamage( 10, 23 );

			SetSkill( SkillName.Swords, 66.0, 97.5 );
			SetSkill( SkillName.Anatomy, 65.0, 87.5 );
			SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			SetSkill( SkillName.Healing, 65.0, 87.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );
			SetSkill( SkillName.Parry, 45.0, 60.5 );
			SetSkill( SkillName.Chivalry, 85, 100 );

			Fame = 100;
			Karma = 250;

			AddItem( new Shoes( Utility.RandomNeutralHue() ) );
			AddItem( new Shirt());
			AddItem( new VikingSword() );
			AddItem( new MetalKiteShield () );
 
		  
			AddItem( new PlateChest() );
			AddItem( new PlateLegs() );
			AddItem( new PlateArms() );
			AddItem( new LeatherGorget() );
			PackGold( 20, 100 );

		}
		public override bool ClickTitle{get{return false;}}

		public HirePaladin( Serial serial ) : base( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer ) 
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );// version 
		}

		public override void Deserialize( GenericReader reader ) 
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
