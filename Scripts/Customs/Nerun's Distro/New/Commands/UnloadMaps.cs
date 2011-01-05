using System;
using System.Collections;
using System.IO;
using Server;
using Server.Mobiles; 
using Server.Items;
using Server.Commands;
using Server.Network;
using Server.Gumps;

namespace Server.Commands
{
	public class Unload
	{
		public Unload()
		{
		}

		public static void Initialize() 
		{
			CommandSystem.Register( "UnloadTrammel", AccessLevel.Administrator, new CommandEventHandler( UnloadTrammel_OnCommand ) );
			CommandSystem.Register( "UnloadMalas", AccessLevel.Administrator, new CommandEventHandler( UnloadMalas_OnCommand ) );
			CommandSystem.Register( "UnloadIlshenar", AccessLevel.Administrator, new CommandEventHandler( UnloadIlshenar_OnCommand ) );
			CommandSystem.Register( "UnloadTokuno", AccessLevel.Administrator, new CommandEventHandler( UnloadTokuno_OnCommand ) );
			CommandSystem.Register( "UnloadFelucca", AccessLevel.Administrator, new CommandEventHandler( UnloadFelucca_OnCommand ) );
			CommandSystem.Register( "UnloadTerMur", AccessLevel.Administrator, new CommandEventHandler( UnloadTerMur_OnCommand ) );
		}

		[Usage( "[Unloadtrammel" )]
		[Description( "Unload Trammel maps with a menu." )] 
		private static void UnloadTrammel_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new UnloadTrammelGump( e ) );
		}

		[Usage( "[Unloadfelucca" )]
		[Description( "Unload Felucca maps with a menu." )] 
		private static void UnloadFelucca_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new UnloadFeluccaGump( e ) );
		}

		[Usage( "[Unloadmalas" )]
		[Description( "Unload Malas maps with a menu." )] 
		private static void UnloadMalas_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new UnloadMalasGump( e ) );
		}

		[Usage( "[Unloadilshenar" )]
		[Description( "Unload Ilshenar maps with a menu." )] 
		private static void UnloadIlshenar_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new UnloadIlshenarGump( e ) );
		}

		[Usage( "[Unloadtokuno" )]
		[Description( "Unload Tokuno maps with a menu." )] 
		private static void UnloadTokuno_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new UnloadTokunoGump( e ) );
		}

		[Usage( "[Unloadtermur" )]
		[Description( "Unload Ter Mur maps with a menu." )] 
		private static void UnloadTerMur_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new UnloadTerMurGump( e ) );
		}
	}
}

namespace Server.Gumps
{

	public class UnloadTrammelGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public UnloadTrammelGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "TRAMMEL" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 218, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Blighted Grove" );
			AddLabel( 35, 76, 200, "Britain Sewer" );
			AddLabel( 35, 101, 200, "Covetous" );
			AddLabel( 35, 126, 200, "Deceit" );
			AddLabel( 35, 151, 200, "Despise" );
			AddLabel( 35, 176, 200, "Destard" );
			AddLabel( 35, 201, 200, "Fire" );
			AddLabel( 35, 226, 200, "Graveyards" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, false, 101 );
			AddCheck( 182, 73, 210, 211, false, 102 );
			AddCheck( 182, 98, 210, 211, false, 103 );
			AddCheck( 182, 123, 210, 211, false, 104 );
			AddCheck( 182, 148, 210, 211, false, 105 );
			AddCheck( 182, 173, 210, 211, false, 106 );
			AddCheck( 182, 198, 210, 211, false, 107 );
			AddCheck( 182, 223, 210, 211, false, 108 );

			AddLabel( 110, 255, 200, "1/4" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 2 );

			AddPage(2);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "TRAMMEL" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 218, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Hythloth" );
			AddLabel( 35, 76, 200, "Ice" );
			AddLabel( 35, 101, 200, "Lost Lands" );
			AddLabel( 35, 126, 200, "Orc Caves" );
			AddLabel( 35, 151, 200, "Outdoors" );
			AddLabel( 35, 176, 200, "Painted Caves" );
			AddLabel( 35, 201, 200, "Palace of Paroxysmus" );
			AddLabel( 35, 226, 200, "Prism of Light" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, false, 109 );
			AddCheck( 182, 73, 210, 211, false, 110 );
			AddCheck( 182, 98, 210, 211, false, 111 );
			AddCheck( 182, 123, 210, 211, false, 112 );
			AddCheck( 182, 148, 210, 211, false, 113 );
			AddCheck( 182, 173, 210, 211, false, 114 );
			AddCheck( 182, 198, 210, 211, false, 115 );
			AddCheck( 182, 223, 210, 211, false, 116 );

			AddLabel( 110, 255, 200, "2/4" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 3 );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 1 );

			AddPage(3);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "TRAMMEL" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 218, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Sanctuary" );
			AddLabel( 35, 76, 200, "Sea Life" );
			AddLabel( 35, 101, 200, "Shame" );
			AddLabel( 35, 126, 200, "Solen Hive" );
			AddLabel( 35, 151, 200, "Terathan Keep" );
			AddLabel( 35, 176, 200, "Towns Life" );
			AddLabel( 35, 201, 200, "Towns People" );
			AddLabel( 35, 226, 200, "Trinsic Passage" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, false, 117 );
			AddCheck( 182, 73, 210, 211, false, 118 );
			AddCheck( 182, 98, 210, 211, false, 119 );
			AddCheck( 182, 123, 210, 211, false, 120 );
			AddCheck( 182, 148, 210, 211, false, 121 );
			AddCheck( 182, 173, 210, 211, false, 122 );
			AddCheck( 182, 198, 210, 211, false, 123 );
			AddCheck( 182, 223, 210, 211, false, 124 );

			AddLabel( 110, 255, 200, "3/4" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 4 );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 2 );

			AddPage(4);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "TRAMMEL" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 97, 10003 );
			AddImageTiled( 163, 25, 2, 97, 10003 );
			AddImageTiled( 218, 25, 2, 97, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			//AddImageTiled( 20, 145, 200, 2, 10001 );
			//AddImageTiled( 20, 170, 200, 2, 10001 );
			//AddImageTiled( 20, 195, 200, 2, 10001 );
			//AddImageTiled( 20, 220, 200, 2, 10001 );
			//AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Vendors" );
			AddLabel( 35, 76, 200, "Wild Life" );
			AddLabel( 35, 101, 200, "Wrong" );
			//AddLabel( 35, 126, 200, "28" );
			//AddLabel( 35, 151, 200, "29" );
			//AddLabel( 35, 176, 200, "30" );
			//AddLabel( 35, 201, 200, "31" );
			//AddLabel( 35, 226, 200, "32" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, false, 125 );
			AddCheck( 182, 73, 210, 211, false, 126 );
			AddCheck( 182, 98, 210, 211, false, 127 );
			//AddCheck( 182, 123, 210, 211, false, 128 );
			//AddCheck( 182, 148, 210, 211, false, 129 );
			//AddCheck( 182, 173, 210, 211, false, 130 );
			//AddCheck( 182, 198, 210, 211, false, 131 );
			//AddCheck( 182, 223, 210, 211, false, 132 );

			AddLabel( 110, 255, 200, "4/4" );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 3 );

			//Ok, Cancel
			AddButton( 55, 280, 247, 249, 1, GumpButtonType.Reply, 0 );
			AddButton( 125, 280, 241, 243, 0, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0: // Closed or Cancel
				{
					return;
				}
				default:
				{
					// Make sure that the OK, button was pressed
					if( info.ButtonID == 1 )
					{
						// Get the array of switches selected
						ArrayList Selections = new ArrayList( info.Switches );
						string prefix = Server.Commands.CommandSystem.Prefix;

						// Now unloading any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 101", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 102", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 103", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 104", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 105", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 106", prefix ) );
						}
						if( Selections.Contains( 107 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 107", prefix ) );
						}
						if( Selections.Contains( 108 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 108", prefix ) );
						}
						if( Selections.Contains( 109 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 109", prefix ) );
						}
						if( Selections.Contains( 110 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 110", prefix ) );
						}
						if( Selections.Contains( 111 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 111", prefix ) );
						}
						if( Selections.Contains( 112 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 112", prefix ) );
						}
						if( Selections.Contains( 113 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 113", prefix ) );
						}
						if( Selections.Contains( 114 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 114", prefix ) );
						}
						if( Selections.Contains( 115 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 115", prefix ) );
						}
						if( Selections.Contains( 116 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 116", prefix ) );
						}
						if( Selections.Contains( 117 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 117", prefix ) );
						}
						if( Selections.Contains( 118 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 118", prefix ) );
						}
						if( Selections.Contains( 119 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 119", prefix ) );
						}
						if( Selections.Contains( 120 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 120", prefix ) );
						}
						if( Selections.Contains( 121 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 121", prefix ) );
						}
						if( Selections.Contains( 122 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 122", prefix ) );
						}
						if( Selections.Contains( 123 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 123", prefix ) );
						}
						if( Selections.Contains( 124 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 124", prefix ) );
						}
						if( Selections.Contains( 125 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 125", prefix ) );
						}
						if( Selections.Contains( 126 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 126", prefix ) );
						}
						if( Selections.Contains( 127 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 127", prefix ) );
						}
					}

					break;
				}
			}
		}
	}

	public class UnloadFeluccaGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public UnloadFeluccaGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "FELUCCA" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 218, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Blighted Grove" );
			AddLabel( 35, 76, 200, "Britain Sewer" );
			AddLabel( 35, 101, 200, "Covetous" );
			AddLabel( 35, 126, 200, "Deceit" );
			AddLabel( 35, 151, 200, "Despise" );
			AddLabel( 35, 176, 200, "Destard" );
			AddLabel( 35, 201, 200, "Fire" );
			AddLabel( 35, 226, 200, "Graveyards" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, false, 101 );
			AddCheck( 182, 73, 210, 211, false, 102 );
			AddCheck( 182, 98, 210, 211, false, 103 );
			AddCheck( 182, 123, 210, 211, false, 104 );
			AddCheck( 182, 148, 210, 211, false, 105 );
			AddCheck( 182, 173, 210, 211, false, 106 );
			AddCheck( 182, 198, 210, 211, false, 107 );
			AddCheck( 182, 223, 210, 211, false, 108 );

			AddLabel( 110, 255, 200, "1/4" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 2 );

			AddPage(2);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "FELUCCA" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 218, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Hythloth" );
			AddLabel( 35, 76, 200, "Ice" );
			AddLabel( 35, 101, 200, "Khaldun" );
			AddLabel( 35, 126, 200, "Lost Lands" );
			AddLabel( 35, 151, 200, "Orc Caves" );
			AddLabel( 35, 176, 200, "Outdoors" );
			AddLabel( 35, 201, 200, "Painted Caves" );
			AddLabel( 35, 226, 200, "Palace of Paroxysmus" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, false, 109 );
			AddCheck( 182, 73, 210, 211, false, 110 );
			AddCheck( 182, 98, 210, 211, false, 111 );
			AddCheck( 182, 123, 210, 211, false, 112 );
			AddCheck( 182, 148, 210, 211, false, 113 );
			AddCheck( 182, 173, 210, 211, false, 114 );
			AddCheck( 182, 198, 210, 211, false, 115 );
			AddCheck( 182, 223, 210, 211, false, 116 );

			AddLabel( 110, 255, 200, "2/4" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 3 );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 1 );

			AddPage(3);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "FELUCCA" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 218, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Prism of Light" );
			AddLabel( 35, 76, 200, "Sanctuary" );
			AddLabel( 35, 101, 200, "Sea Life" );
			AddLabel( 35, 126, 200, "Shame" );
			AddLabel( 35, 151, 200, "Solen Hive" );
			AddLabel( 35, 176, 200, "Terathan Keep" );
			AddLabel( 35, 201, 200, "Towns Life" );
			AddLabel( 35, 226, 200, "Towns People" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, false, 117 );
			AddCheck( 182, 73, 210, 211, false, 118 );
			AddCheck( 182, 98, 210, 211, false, 119 );
			AddCheck( 182, 123, 210, 211, false, 120 );
			AddCheck( 182, 148, 210, 211, false, 121 );
			AddCheck( 182, 173, 210, 211, false, 122 );
			AddCheck( 182, 198, 210, 211, false, 123 );
			AddCheck( 182, 223, 210, 211, false, 124 );

			AddLabel( 110, 255, 200, "3/4" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 4 );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 2 );

			AddPage(4);

			//grey background
			AddBackground( 0, 0, 240, 310, 5054 );

			//----------
			AddLabel( 95, 2, 200, "FELUCCA" );

			//white background
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );

			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );

			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 122, 10003 );
			AddImageTiled( 163, 25, 2, 122, 10003 );
			AddImageTiled( 218, 25, 2, 122, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			//AddImageTiled( 20, 170, 200, 2, 10001 );
			//AddImageTiled( 20, 195, 200, 2, 10001 );
			//AddImageTiled( 20, 220, 200, 2, 10001 );
			//AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Trinsic Passage" );
			AddLabel( 35, 76, 200, "Vendors" );
			AddLabel( 35, 101, 200, "Wild Life" );
			AddLabel( 35, 126, 200, "Wrong" );
			//AddLabel( 35, 151, 200, "29" );
			//AddLabel( 35, 176, 200, "30" );
			//AddLabel( 35, 201, 200, "31" );
			//AddLabel( 35, 226, 200, "32" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, false, 125 );
			AddCheck( 182, 73, 210, 211, false, 126 );
			AddCheck( 182, 98, 210, 211, false, 127 );
			AddCheck( 182, 123, 210, 211, false, 128 );
			//AddCheck( 182, 148, 210, 211, false, 129 );
			//AddCheck( 182, 173, 210, 211, false, 130 );
			//AddCheck( 182, 198, 210, 211, false, 131 );
			//AddCheck( 182, 223, 210, 211, false, 132 );

			AddLabel( 110, 255, 200, "4/4" );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 3 );

			//Ok, Cancel
			AddButton( 55, 280, 247, 249, 1, GumpButtonType.Reply, 0 );
			AddButton( 125, 280, 241, 243, 0, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0: // Closed or Cancel
				{
					return;
				}
				default:
				{
					// Make sure that the OK, button was pressed
					if( info.ButtonID == 1 )
					{
						// Get the array of switches selected
						ArrayList Selections = new ArrayList( info.Switches );
						string prefix = Server.Commands.CommandSystem.Prefix;

						// Now unloading any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 201", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 202", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 203", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 204", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 205", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 206", prefix ) );
						}
						if( Selections.Contains( 107 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 207", prefix ) );
						}
						if( Selections.Contains( 108 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 208", prefix ) );
						}
						if( Selections.Contains( 109 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 209", prefix ) );
						}
						if( Selections.Contains( 110 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 210", prefix ) );
						}
						if( Selections.Contains( 111 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 228", prefix ) );
						}
						if( Selections.Contains( 112 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 211", prefix ) );
						}
						if( Selections.Contains( 113 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 212", prefix ) );
						}
						if( Selections.Contains( 114 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 213", prefix ) );
						}
						if( Selections.Contains( 115 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 214", prefix ) );
						}
						if( Selections.Contains( 116 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 215", prefix ) );
						}
						if( Selections.Contains( 117 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 216", prefix ) );
						}
						if( Selections.Contains( 118 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 217", prefix ) );
						}
						if( Selections.Contains( 119 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 218", prefix ) );
						}
						if( Selections.Contains( 120 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 219", prefix ) );
						}
						if( Selections.Contains( 121 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 220", prefix ) );
						}
						if( Selections.Contains( 122 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 221", prefix ) );
						}
						if( Selections.Contains( 123 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 222", prefix ) );
						}
						if( Selections.Contains( 124 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 223", prefix ) );
						}
						if( Selections.Contains( 125 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 224", prefix ) );
						}
						if( Selections.Contains( 126 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 225", prefix ) );
						}
						if( Selections.Contains( 127 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 226", prefix ) );
						}
						if( Selections.Contains( 128 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 227", prefix ) );
						}
					}

					break;
				}
			}
		}
	}

	public class UnloadIlshenarGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public UnloadIlshenarGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

			//fundo cinza
			AddBackground( 0, 0, 243, 310, 5054 );
			//----------
			AddLabel( 93, 2, 200, "ILSHENAR" );
			//fundo branco
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );
			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );
			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 222, 10003 );
			AddImageTiled( 163, 25, 2, 222, 10003 );
			AddImageTiled( 220, 25, 2, 222, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			AddImageTiled( 20, 245, 200, 2, 10001 );

			//Map names
			AddLabel( 35, 51, 200, "Ancient Lair" );
			AddLabel( 35, 76, 200, "Ankh" );
			AddLabel( 35, 101, 200, "Blood" );
			AddLabel( 35, 126, 200, "Exodus" );
			AddLabel( 35, 151, 200, "Mushroom" );
			AddLabel( 35, 176, 200, "Outdoors" );
			AddLabel( 35, 201, 200, "Ratman cave" );
			AddLabel( 35, 226, 200, "Rock" );

			//Options
			AddCheck( 182, 48, 210, 211, false, 101 );
			AddCheck( 182, 73, 210, 211, false, 102 );
			AddCheck( 182, 98, 210, 211, false, 103 );
			AddCheck( 182, 123, 210, 211, false, 104 );
			AddCheck( 182, 148, 210, 211, false, 105 );
			AddCheck( 182, 173, 210, 211, false, 106 );
			AddCheck( 182, 198, 210, 211, false, 107 );
			AddCheck( 182, 223, 210, 211, false, 108 );

			AddLabel( 110, 255, 200, "1/2" );
			AddButton( 200, 255, 0xFA5, 0xFA7, 0, GumpButtonType.Page, 2 );

			AddPage(2);

			//fundo cinza
			AddBackground( 0, 0, 243, 310, 5054 );
			//----------
			AddLabel( 93, 2, 200, "ILSHENAR" );
			//fundo branco
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 232, 3004 );
			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );
			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 171, 10003 );
			AddImageTiled( 163, 25, 2, 171, 10003 );
			AddImageTiled( 220, 25, 2, 171, 10003 );

			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			//AddImageTiled( 20, 220, 200, 2, 10001 );
			//AddImageTiled( 20, 245, 200, 2, 10001 );

			//----------
			AddLabel( 35, 51, 200, "Sorcerers" );
			AddLabel( 35, 76, 200, "Spectre" );
			AddLabel( 35, 101, 200, "Towns" );
			AddLabel( 35, 126, 200, "Vendors" );
			AddLabel( 35, 151, 200, "Wisp" );
			AddLabel( 35, 176, 200, "Twisted Weald" );
			//AddLabel( 35, 201, 200, "15" );
			//AddLabel( 35, 226, 200, "16" );

			//Options
			AddCheck( 182, 48, 210, 211, false, 109 );
			AddCheck( 182, 73, 210, 211, false, 110 );
			AddCheck( 182, 98, 210, 211, false, 111 );
			AddCheck( 182, 123, 210, 211, false, 112 );
			AddCheck( 182, 148, 210, 211, false, 113 );
			AddCheck( 182, 173, 210, 211, false, 114 );
			//AddCheck( 182, 198, 210, 211, false, 115 );
			//AddCheck( 182, 223, 210, 211, false, 116 );

			AddLabel( 110, 255, 200, "2/2" );
			AddButton( 10, 255, 0xFAE, 0xFB0, 0, GumpButtonType.Page, 1 );

			//Ok, Cancel
			AddButton( 55, 280, 247, 249, 1, GumpButtonType.Reply, 0 );
			AddButton( 125, 280, 241, 243, 0, GumpButtonType.Reply, 0 );

		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0: // Closed or Cancel
				{
					return;
				}

				default:
				{
					// Make sure that the OK, button was pressed
					if( info.ButtonID == 1 )
					{
						// Get the array of switches selected
						ArrayList Selections = new ArrayList( info.Switches );
						string prefix = Server.Commands.CommandSystem.Prefix;

						// Now unloading any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 301", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 302", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 303", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 304", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 305", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 306", prefix ) );
						}
						if( Selections.Contains( 107 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 307", prefix ) );
						}
						if( Selections.Contains( 108 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 308", prefix ) );
						}
						if( Selections.Contains( 109 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 309", prefix ) );
						}
						if( Selections.Contains( 110 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 310", prefix ) );
						}
						if( Selections.Contains( 111 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 311", prefix ) );
						}
						if( Selections.Contains( 112 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 312", prefix ) );
						}
						if( Selections.Contains( 113 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 313", prefix ) );
						}
						if( Selections.Contains( 114 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 314", prefix ) );
						}
					}

					break;
				}
			}
		}
	}

	public class UnloadMalasGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public UnloadMalasGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

			//fundo cinza
			//alt era 310
			AddBackground( 0, 0, 243, 270, 5054 );
			//----------
			AddLabel( 100, 2, 200, "MALAS" );
			//fundo branco
			//x, y, largura, altura, item
			//alt era 232
			AddImageTiled( 10, 20, 220, 215, 3004 );
			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );
			//colunas
			//x, y, comprimento, ?, item
			//comp era 222
			AddImageTiled( 20, 25, 2, 196, 10003 );
			AddImageTiled( 163, 25, 2, 196, 10003 );
			AddImageTiled( 220, 25, 2, 196, 10003 );
			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			AddImageTiled( 20, 220, 200, 2, 10001 );
			//Map names
			AddLabel( 35, 51, 200, "Doom" );
			AddLabel( 35, 76, 200, "North" );
			AddLabel( 35, 101, 200, "OrcForts" );
			AddLabel( 35, 126, 200, "South" );
			AddLabel( 35, 151, 200, "Vendors" );
			AddLabel( 35, 176, 200, "Citadel" );
			AddLabel( 35, 201, 200, "Labyrinth" );

			//Options
			AddCheck( 182, 48, 210, 211, false, 101 );
			AddCheck( 182, 73, 210, 211, false, 102 );
			AddCheck( 182, 98, 210, 211, false, 103 );
			AddCheck( 182, 123, 210, 211, false, 104 );
			AddCheck( 182, 148, 210, 211, false, 105 );
			AddCheck( 182, 173, 210, 211, false, 106 );
			AddCheck( 182, 198, 210, 211, false, 107 );

			//Ok, Cancel
			// alt era 280
			AddButton( 55, 240, 247, 249, 1, GumpButtonType.Reply, 0 );
			AddButton( 125, 240, 241, 243, 0, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0: // Closed or Cancel
				{
					return;
				}

				default:
				{
					// Make sure that the OK, button was pressed
					if( info.ButtonID == 1 )
					{
						// Get the array of switches selected
						ArrayList Selections = new ArrayList( info.Switches );
						string prefix = Server.Commands.CommandSystem.Prefix;

						// Now unloading any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 401", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 402", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 403", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 404", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 405", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 406", prefix ) );
						}
						if( Selections.Contains( 107 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 407", prefix ) );
						}
					}

					break;
				}
			}
		}
	}

	public class UnloadTokunoGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public UnloadTokunoGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

			//fundo cinza
			//alt era 310
			AddBackground( 0, 0, 243, 250, 5054 );
			//----------
			AddLabel( 95, 2, 200, "TOKUNO" );
			//fundo branco
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 183, 3004 );
			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );
			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 172, 10003 );
			AddImageTiled( 163, 25, 2, 172, 10003 );
			AddImageTiled( 220, 25, 2, 172, 10003 );
			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			//Map names
			AddLabel( 35, 51, 200, "Fan Dancers Dojo" );
			AddLabel( 35, 76, 200, "Outdoors" );
			AddLabel( 35, 101, 200, "Towns Life" );
			AddLabel( 35, 126, 200, "Vendors" );
			AddLabel( 35, 151, 200, "Wild Life" );
			AddLabel( 35, 176, 200, "Yomutso Mines" );

			//Options
			AddCheck( 182, 48, 210, 211, false, 101 );
			AddCheck( 182, 73, 210, 211, false, 102 );
			AddCheck( 182, 98, 210, 211, false, 103 );
			AddCheck( 182, 123, 210, 211, false, 104 );
			AddCheck( 182, 148, 210, 211, false, 105 );
			AddCheck( 182, 173, 210, 211, false, 106 );

			//Ok, Cancel
			// alt era 280
			AddButton( 55, 220, 247, 249, 1, GumpButtonType.Reply, 0 );
			AddButton( 125, 220, 241, 243, 0, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0: // Closed or Cancel
				{
					return;
				}

				default:
				{
					// Make sure that the OK, button was pressed
					if( info.ButtonID == 1 )
					{
						// Get the array of switches selected
						ArrayList Selections = new ArrayList( info.Switches );
						string prefix = Server.Commands.CommandSystem.Prefix;

						// Now unloading any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 501", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 502", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 503", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 504", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 505", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 506", prefix ) );
						}
					}

					break;
				}
			}
		}
	}

	public class UnloadTerMurGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public UnloadTerMurGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

			//fundo cinza
			//alt era 310
			AddBackground( 0, 0, 243, 250, 5054 );
			//----------
			AddLabel( 95, 2, 200, "TER MUR" );
			//fundo branco
			//x, y, largura, altura, item
			AddImageTiled( 10, 20, 220, 183, 3004 );
			//----------
			AddLabel( 30, 27, 200, "Map name" );
			AddLabel( 172, 27, 200, "Unload" );
			//colunas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 2, 172, 10003 );
			AddImageTiled( 163, 25, 2, 172, 10003 );
			AddImageTiled( 220, 25, 2, 172, 10003 );
			//Linhas
			//x, y, comprimento, ?, item
			AddImageTiled( 20, 25, 200, 2, 10001 );
			AddImageTiled( 20, 45, 200, 2, 10001 );
			AddImageTiled( 20, 70, 200, 2, 10001 );
			AddImageTiled( 20, 95, 200, 2, 10001 );
			AddImageTiled( 20, 120, 200, 2, 10001 );
			AddImageTiled( 20, 145, 200, 2, 10001 );
			AddImageTiled( 20, 170, 200, 2, 10001 );
			AddImageTiled( 20, 195, 200, 2, 10001 );
			//Map names
			AddLabel( 35, 51, 200, "Vendors" );
			AddLabel( 35, 76, 200, "none" );
			AddLabel( 35, 101, 200, "none" );
			AddLabel( 35, 126, 200, "none" );
			AddLabel( 35, 151, 200, "none" );
			AddLabel( 35, 176, 200, "none" );

			//Options
			AddCheck( 182, 48, 210, 211, false, 101 );
			AddCheck( 182, 73, 210, 211, false, 102 );
			AddCheck( 182, 98, 210, 211, false, 103 );
			AddCheck( 182, 123, 210, 211, false, 104 );
			AddCheck( 182, 148, 210, 211, false, 105 );
			AddCheck( 182, 173, 210, 211, false, 106 );

			//Ok, Cancel
			// alt era 280
			AddButton( 55, 220, 247, 249, 1, GumpButtonType.Reply, 0 );
			AddButton( 125, 220, 241, 243, 0, GumpButtonType.Reply, 0 );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 0: // Closed or Cancel
				{
					return;
				}

				default:
				{
					// Make sure that the OK, button was pressed
					if( info.ButtonID == 1 )
					{
						// Get the array of switches selected
						ArrayList Selections = new ArrayList( info.Switches );
						string prefix = Server.Commands.CommandSystem.Prefix;

						// Now unloading any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 601", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							//CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 602", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							//CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 603", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							//CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 604", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							//CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 605", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							//CommandSystem.Handle( from, String.Format( "{0}Spawngen unload 606", prefix ) );
						}
					}

					break;
				}
			}
		}
	}
}