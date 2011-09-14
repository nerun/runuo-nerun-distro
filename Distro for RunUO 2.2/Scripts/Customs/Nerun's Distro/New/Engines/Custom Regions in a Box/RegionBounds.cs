using System;
using Server;
using System.Collections;
using Server.Regions;
using Server.Targeting;
using Server.Items;

namespace Server.Commands
{
	public class RegionBounds
	{
		public static void Initialize() 
		{
			CommandSystem.Register( "RegionBounds", AccessLevel.GameMaster, new CommandEventHandler( RegionBounds_OnCommand ) ); 
		}

		[Usage( "RegionBounds" )]
		[Description( "Displays the bounding area of either a targeted Mobile's region or the Bounding area of a targeted RegionControl." )]
		private static void RegionBounds_OnCommand( CommandEventArgs e ) 
		{
			e.Mobile.Target = new RegionBoundTarget();
			e.Mobile.SendMessage( "Target a Mobile or RegionControl" );
			e.Mobile.SendMessage( "Please note that Players will also be able to see the bounds of the Region." );
		}

		private class RegionBoundTarget : Target
		{
			public RegionBoundTarget() : base( -1, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if( targeted is Mobile )
				{
					Mobile m = (Mobile)targeted;

					Region r = m.Region;			

					if( r == m.Map.DefaultRegion )
					{
						from.SendMessage( "The Region is the Default region for the entire map and as such, cannot have it's bounds displayed." );
						return;
					}

					from.SendMessage( String.Format( "That Mobile's region is of type {0}, with a priority of {1}.", r.GetType().FullName, r.Priority.ToString() ));

					ShowRegionBounds(r, from, false, true);
				}
				else if( targeted is RegionControl )
				{
                    RegionControl control = ((RegionControl)targeted);
					Region r = control.Region;

                    if (control.Active)
                    {
                        if (control.RegionArea == null || control.RegionArea.Length == 0 || r == null)
                        {
                            from.SendMessage("Region area not defined for targeted RegionControl.");
                            return;
                        }

                        from.SendMessage("Displaying targeted RegionControl's ACTIVE Region...");

                        ShowRegionBounds(r, from, true, true);
                    }
                    else
                    {
                        if (control.RegionArea == null || control.RegionArea.Length == 0)
                        {
                            from.SendMessage("Region area not defined for targeted RegionControl.");
                            return;
                        }

                        r = new CustomRegion(control);

                        if (r == null)
                        {
                            from.SendMessage("Region area not defined for targeted RegionControl.");
                            return;
                        }

                        from.SendMessage("Displaying targeted RegionControl's INACTIVE Region...");

                        ShowRegionBounds(r, from, true, false);                      
                    }
				}
				else
				{
					from.SendMessage( "That is not a Mobile or a RegionControl" );
				}
			}
		}

        public static void ShowRectBounds(Rectangle3D r, Map m, Mobile from, bool control, bool active)
        {
            if (m == Map.Internal || m == null)
                return;

            int hue = 0;
            if(control && active)
                hue = 0x3F;
            else if(control && !active)
                hue = 0x26;
            else if(!control)
                hue = 1151;

            Point3D p1 = new Point3D(r.Start.X, r.Start.Y - 1, from.Z);	//So we dont' need to create a new one each point
            Point3D p2 = new Point3D(r.Start.X, r.Start.Y + r.Height - 1, from.Z);	//So we dont' need to create a new one each point

            Effects.SendLocationEffect(new Point3D(r.Start.X - 1, r.Start.Y - 1, from.Z), m, 251, 75, 1, hue, 3);	//Top Corner	//Testing color

            for (int x = r.Start.X; x <= (r.Start.X + r.Width - 1); x++)
            {
                p1.X = x;
                p2.X = x;

                p1.Z = from.Z;
                p2.Z = from.Z;
 
                Effects.SendLocationEffect(p1, m, 249, 75, 1, hue, 3);	//North bound
                Effects.SendLocationEffect(p2, m, 249, 75, 1, hue, 3);	//South bound
            }

            p1 = new Point3D(r.Start.X - 1, r.Start.Y - 1, from.Z);
            p2 = new Point3D(r.Start.X + r.Width - 1, r.Start.Y, from.Z);

            for (int y = r.Start.Y; y <= (r.Start.Y + r.Height - 1); y++)
            {
                p1.Y = y;
                p2.Y = y;

                p1.Z = from.Z;
                p2.Z = from.Z;

                Effects.SendLocationEffect(p1, m, 250, 75, 1, hue, 3);	//West Bound
                Effects.SendLocationEffect(p2, m, 250, 75, 1, hue, 3);	//East Bound
            }
        }

        public static void ShowRegionBounds(Region r, Mobile from, bool control, bool active)
        {
            if (r == null)
                return;

            foreach (Rectangle3D rect in r.Area)
            {
                ShowRectBounds(rect, r.Map, from, control, active);
            }
        }
	}
}
