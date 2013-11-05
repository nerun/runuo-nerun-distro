using System;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class AcidSac : Item
	{
		public override int LabelNumber{ get{ return 1111654; } } // acid sac

		[Constructable]
		public AcidSac() : base(0x0C67)
		{
			Stackable = true;
			Weight = 1.0;
			Hue = 648;
		}
		
		public override void OnDoubleClick(Mobile from)
        {
            from.SendLocalizedMessage(1111656); // What do you wish to use the acid on?

            from.Target = new InternalTarget(this);
        }
		
		private class InternalTarget : Target
        {
            private Item m_Item;
			private Item wall;
			private Item wallandvine;

            public InternalTarget(Item item) : base(2, false, TargetFlags.None)
            {
                m_Item = item;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
				PlayerMobile pm = from as PlayerMobile;

				if (m_Item.Deleted)
					return;

				if(targeted is AddonComponent)
				{
					AddonComponent addoncomponent = (AddonComponent)targeted;

					if ( addoncomponent is MagicVinesComponent || addoncomponent is StoneWallComponent || addoncomponent is DungeonWallComponent )
					{
						int Xs = addoncomponent.X;
						
						if ( addoncomponent is MagicVinesComponent )
							Xs += -1;

						if ( addoncomponent.Addon is StoneWallAndVineAddon )
						{
							wall = new SecretStoneWallNS();
							wallandvine = new StoneWallAndVineAddon();
						}
						else if ( addoncomponent.Addon is DungeonWallAndVineAddon )
						{
							wall = new SecretDungeonWallNS();
							wallandvine = new DungeonWallAndVineAddon();
						}

						wall.MoveToWorld( new Point3D( Xs, addoncomponent.Y, addoncomponent.Z ), addoncomponent.Map);

						addoncomponent.Delete();

						m_Item.Consume();

						wall.PublicOverheadMessage(0, 1358, 1111662); // The acid quickly burns through the writhing wallvines, revealing the strange wall.
						
						Timer.DelayCall( TimeSpan.FromSeconds( 15.0 ), delegate()
						{
							wallandvine.MoveToWorld( wall.Location, wall.Map);
							
							wall.Delete();
							wallandvine.PublicOverheadMessage(0, 1358, 1111663); // The vines recover from the acid and, spreading like tentacles, reclaim their grip over the wall.
						} );
					}
				}
				else
				{
					from.SendLocalizedMessage(1111657); // The acid swiftly burn through it.
					m_Item.Consume();
					return; // Exit the method, because addoncomponent is null
				}
            }
        }
		
		public AcidSac( Serial serial ) : base( serial )
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