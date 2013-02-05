// Engine r114
using System;
using System.Collections;
using System.IO;
using Server;
using Server.Commands; 
using Server.Gumps;
using Server.Items;
using Server.Mobiles; 
using Server.Network;

namespace Server.Commands 
{
	public class CreateWorld
	{
		public CreateWorld()
		{
		}

		public static void Initialize() 
		{ 
			CommandSystem.Register( "Createworld", AccessLevel.Administrator, new CommandEventHandler( Create_OnCommand ) ); 
		} 

		[Usage( "[CreateWorld" )]
		[Description( "Generates world decorations, doors, signs and moongates with a menu." )]
		private static void Create_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new CreateWorldGump( e ) );
		}
	}
}

namespace Server.Gumps
{
	public class CreateWorldGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;

        public CreateWorldGump( CommandEventArgs e ) : base( 70, 70 )
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

	//NOTE: generic button format (x, y, ItemID, ItemID pressed, ButtonID )

			AddPage(1);

	//Background (x, y, largura, altura, item)
			AddBackground(76, 70, 606, 472, 9200);
			AddImageTiled(26, 42, 74, 414, 10440);
	//Page change
			AddLabel(614, 80, 52, @"1/2");
			AddButton(647, 82, 5601, 5605, 0, GumpButtonType.Page, 2);
	//Titles
			AddLabel(330, 80, 52, @"CREATE WORLD");
			AddLabel(104, 109, 52, @"Select UO Era");
			AddLabel(220, 109, 52, @"Short description of the Era");
	//Options
			AddLabel(104, 142, 87, @"Samurai Empire");
			AddButton(104, 168, 4005, 4006, 10, GumpButtonType.Reply, 0);
			AddHtml( 220, 142, 444, 81, @"DATE: Nov/2004
				CLIENTS: 4.0.5a to 4.0.11c
				DESCRIPTION: This expansion added Tokuno facet, new monsters and two new classes: Samurai and Ninja.
				EVENTS: Britain invasion by lizardmen (Jan-May/2005) brought a few new decorations.", (bool)true, (bool)true);
			AddLabel(104, 242, 87, @"Mondain's Legacy");
			AddButton(104, 268, 4005, 4006, 11, GumpButtonType.Reply, 0);
			AddHtml( 220, 242, 444, 81, @"DATE: Aug/2005
				CLIENTS: 4.0.11d to 5.0.9.1
				DESCRIPTION: This expansion added new race Elves, spellweaving, Heartwood town, new dungeons and new monsters.
				EVENTS: New town and dungeons brought new decorations. There was no special event.", (bool)true, (bool)true);
			AddLabel(104, 342, 87, @"Kingdom's Reborn");
			AddLabel(170, 368, 87, @"Age I");
			AddButton(104, 368, 4005, 4006, 12, GumpButtonType.Reply, 0);
			AddHtml( 220, 342, 444, 81, @"DATE: Apr/2007
				CLIENTS: 6.0.0.0 to 6.0.3.1
				DESCRIPTION: Kingdom Reborn was not considered an expansion pack, but I use the coincidence of this client release to date some important events that, in practice, occurred during Mondain's Legacy Era.
				EVENTS: Haven was destroyed, presumably at the hands of Blackrock traders. There are now two cities: New Haven (starting city), and Old Haven (in ruins). So we have new decorations.", (bool)true, (bool)true);
			AddLabel(104, 442, 87, @"Kingdom's Reborn");
			AddLabel(170, 469, 87, @"Age II");
			AddButton(104, 468, 4005, 4006, 13, GumpButtonType.Reply, 0);
			AddHtml( 220, 442, 444, 81, @"DATE: Oct/2007
				CLIENTS: 6.0.4.0 to 6.0.14.3
				DESCRIPTION: As explained in Kingdom Reborn, Age I.
				EVENTS: Warriors of Destiny event cycle. City of Magincia was invaded, and destroyed, by Demonic entities. So we have new decorations.", (bool)true, (bool)true);

			AddPage(2);

	//Background (x, y, largura, altura, item)
			AddBackground(76, 70, 606, 472, 9200);
			AddImageTiled(26, 42, 74, 414, 10440);
	//Page change
			AddLabel(614, 80, 52, @"2/2");
			AddButton(590, 82, 5603, 5607, 0, GumpButtonType.Page, 1);
	//Titles
			AddLabel(330, 80, 52, @"CREATE WORLD");
			AddLabel(104, 109, 52, @"Select UO Era");
			AddLabel(220, 109, 52, @"Short description of the Era");
	//Options
			AddLabel(104, 142, 87, @"Stygian Abyss");
			AddButton(104, 168, 4005, 4006, 14, GumpButtonType.Reply, 0);
			AddHtml( 220, 142, 444, 81, @"DATE: Nov/2009
				CLIENTS: 6.0.14.4 to 7.0.8.2
				DESCRIPTION: This expansion added the new race Gargoyles and new places, towns and dungeons.
				EVENTS: New town and dungeons brought new decorations. There was no special event.", (bool)true, (bool)true);
			AddLabel(104, 242, 87, @"High Seas - Age I");
			AddButton(104, 268, 4005, 4006, 15, GumpButtonType.Reply, 0);
			AddHtml( 220, 242, 444, 81, @"DATE: Oct/2010
				CLIENTS: 7.0.9.0 to 7.0.13.0
				DESCRIPTION: This expansion added sea stuff: new ships, sea market (Dock Town), pirates etc.
				EVENTS: New town and stuff brought new decorations. There was no special event, yet.", (bool)true, (bool)true);
			AddLabel(104, 342, 87, @"High Seas - Age II");
			AddButton(104, 368, 4005, 4006, 16, GumpButtonType.Reply, 0);
			AddHtml( 220, 342, 444, 81, @"DATE: Mar/2011
				CLIENTS: 7.0.13.1 up to the present day.
				DESCRIPTION: As explained in High Seas, Age I.
				EVENTS: A 'New Magincia' was built. So 'Old Magincia' rubbles was removed, and was added new decorations.", (bool)true, (bool)true);
        }
		
		public static void DoThis( Mobile from, string command)
		{
			from.Say( "Generating world decoration..." );
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( from, String.Format( "{0}{1}", prefix, command ) );
		}
		
		public static void DoBegin()
		{
			DoThis( from, "Moongen" );
			DoThis( from, "DoorGen" );
		}

		public static void DoEnd()
		{
			DoThis( from, "SignGen" );
			DoThis( from, "TelGen" );
			DoThis( from, "GenGauntlet" );
			DoThis( from, "GenChampions" );
			DoThis( from, "GenKhaldun" );
			DoThis( from, "GenerateFactions" );
			DoThis( from, "GenStealArties" );
			DoThis( from, "SHTelGen" );
			DoThis( from, "SecretLocGen" );
		}

		public override void OnResponse( NetState sender, RelayInfo info ) 
		{ 
			Mobile from = sender.Mobile; 

			switch( info.ButtonID ) 
			{ 
				case 0: // Closed or Canceled
				{
					break;
				}
				case 10:
				{
					DoBegin();
					DoThis( from, "DecorateSE" );
					DoEnd();
					break;
				}
				case 11:
				{
					DoBegin();
					DoThis( from, "DecorateML" );
					DoEnd();
					break;
				}
				case 12:
				{
					DoBegin();
					DoThis( from, "DecorateKRfirstAge" );
					DoEnd();
					break;
				}
				case 13:
				{
					DoBegin();
					DoThis( from, "DecorateKRsecondAge" );
					DoEnd();
					break;
				}
				case 14:
				{
					DoBegin();
					DoThis( from, "DecorateSA" );
					DoEnd();
					break;
				}
				case 15:
				{
					DoBegin();
					DoThis( from, "DecorateHSfirstAge" );
					DoEnd();
					break;
				}
				case 16:
				{
					DoBegin();
					DoThis( from, "DecorateHSsecondAge" );
					DoEnd();
					break;
				}
			} 
		}
	}
}