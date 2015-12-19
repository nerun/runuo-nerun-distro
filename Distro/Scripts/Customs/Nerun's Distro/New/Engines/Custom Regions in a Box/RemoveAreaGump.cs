using System;
using Server;
using Server.Gumps;
using Server.Items;
using System.Collections;
using Server.Network;

namespace Server.Gumps
{
	public class RemoveAreaGump : Gump
	{
		RegionControl m_Control;

		public RemoveAreaGump( RegionControl r )	: base( 25, 250 )
		{
			m_Control = r;

			Closable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);
			AddBackground(23, 32, 412, 256, 9270);
			AddAlphaRegion(19, 29, 418, 263);

			AddLabel(186, 45, 1152, "REMOVE AREA");


			//+25 between 'em.

			int itemsThisPage = 0;
			int nextPageNumber = 1;


            if (r.RegionArea != null)
            {
                for(int i = 0; i < r.RegionArea.Length; i++)
                {
                    Rectangle3D rect = ((Rectangle3D)r.RegionArea[i]);

                    if (itemsThisPage >= 8 || itemsThisPage == 0)
                    {
                        itemsThisPage = 0;

                        if (nextPageNumber != 1)
                        {
                            AddButton(393, 45, 4007, 4009, 0, GumpButtonType.Page, nextPageNumber);
                            //Forward button -> #0
                        }

                        AddPage(nextPageNumber++);

                        if (nextPageNumber != 2)
                        {
                            AddButton(35, 45, 4014, 4016, 1, GumpButtonType.Page, nextPageNumber - 2);
                            //Back Button -> #1
                        }
                    }

                    AddButton(70, 75 + 25 * itemsThisPage, 4017, 4019, 100 + i, GumpButtonType.Reply, 0);
                    //Button is 100 + i

                    //AddLabel(116, 77 + 25*i, 0, "(1234, 5678)");
                    AddLabel(116, 77 + 25 * itemsThisPage, 1152, String.Format("({0}, {1}, {2})", rect.Start.X, rect.Start.Y, rect.Start.Z));


                    AddLabel(232, 78 + 25 * itemsThisPage, 1152, "<-->");

                    //AddLabel(294, 77 + 25*i, 0, "(9876, 5432)");
                    AddLabel(294, 77 + 25 * itemsThisPage, 1152, String.Format("({0}, {1}, {2})", rect.End.X, rect.End.Y, rect.End.Z));

                    itemsThisPage++;

                }
            }

		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
            Mobile from = sender.Mobile;

            if (from == null)
                return;

			if( info.ButtonID >= 100 )
			{
				m_Control.RemoveArea( info.ButtonID - 100, from );

				sender.Mobile.CloseGump( typeof( RemoveAreaGump ) );

				sender.Mobile.SendGump( new RemoveAreaGump( m_Control ) );
			}
		}
		
	}
}