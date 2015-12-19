//By Nerun
using System;
using Server;
using Server.Items;
using EDI = Server.Mobiles.EscortDestinationInfo;

namespace Server.Mobiles
{
	public class EscortableWanderingHealer : BaseEscortable
	{
		[Constructable]
		public EscortableWanderingHealer()
		{
			Title = "the wandering healer";

			AddItem( new GnarledStaff() );

			SetSkill( SkillName.Tactics, 82.0, 100.0 );
			SetSkill( SkillName.MagicResist, 82.0, 100.0 );
			SetSkill( SkillName.Anatomy, 75.0, 97.5 );
			SetSkill( SkillName.Magery, 82.0, 100.0 );
			SetSkill( SkillName.EvalInt, 82.0, 100.0 );
		}

		public override bool CanTeach{ get{ return true; } }
		public override bool ClickTitle{ get{ return false; } } // Do not display title in OnSingleClick

		public virtual int GetRobeColor()
		{
			return Utility.RandomYellowHue();
		}

		public override void InitOutfit()
		{
			AddItem( new Robe( GetRobeColor() ) );
			AddItem( new Sandals() );

			Utility.AssignRandomHair( this );

			PackGold( 50, 100 );
		}

		public EscortableWanderingHealer( Serial serial ) : base( serial )
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