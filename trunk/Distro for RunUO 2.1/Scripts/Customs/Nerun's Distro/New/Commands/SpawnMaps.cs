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
	public class Spawn
	{
		public Spawn()
		{
		}

		public static void Initialize()
		{ 
			CommandSystem.Register( "SpawnTrammel", AccessLevel.Administrator, new CommandEventHandler( SpawnTrammel_OnCommand ) );
			CommandSystem.Register( "SpawnFelucca", AccessLevel.Administrator, new CommandEventHandler( SpawnFelucca_OnCommand ) );
			CommandSystem.Register( "SpawnMalas", AccessLevel.Administrator, new CommandEventHandler( SpawnMalas_OnCommand ) );
			CommandSystem.Register( "SpawnIlshenar", AccessLevel.Administrator, new CommandEventHandler( SpawnIlshenar_OnCommand ) );
			CommandSystem.Register( "SpawnTokuno", AccessLevel.Administrator, new CommandEventHandler( SpawnTokuno_OnCommand ) );
			CommandSystem.Register( "SpawnTerMur", AccessLevel.Administrator, new CommandEventHandler( SpawnTerMur_OnCommand ) );
		}

		[Usage( "[spawntrammel" )]
		[Description( "Spawn Trammel with a menu." )] 
		private static void SpawnTrammel_OnCommand( CommandEventArgs e )
		{ 
			e.Mobile.SendGump( new TrammelGump( e ) );
		}

		[Usage( "[spawnfelucca" )]
		[Description( "Spawn Felucca with a menu." )] 
		private static void SpawnFelucca_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new FeluccaGump( e ) );
		}

		[Usage( "[spawnmalas" )]
		[Description( "Spawn Malas with a menu." )] 
		private static void SpawnMalas_OnCommand( CommandEventArgs e )
		{ 
			e.Mobile.SendGump( new MalasGump( e ) );
		}

		[Usage( "[spawnilshenar" )]
		[Description( "Spawn Ilshenar with a menu." )] 
		private static void SpawnIlshenar_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new IlshenarGump( e ) );
		}

		[Usage( "[spawntokuno" )]
		[Description( "Spawn Tokuno with a menu." )] 
		private static void SpawnTokuno_OnCommand( CommandEventArgs e )
		{ 
			e.Mobile.SendGump( new TokunoGump( e ) );
		}

		[Usage( "[spawntermur" )]
		[Description( "Spawn Ter Mur with a menu." )] 
		private static void SpawnTerMur_OnCommand( CommandEventArgs e )
		{ 
			e.Mobile.SendGump( new TerMurGump( e ) );
		}
	}
}

namespace Server.Gumps
{

	public class TrammelGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public TrammelGump( CommandEventArgs e ) : base( 50,50 )
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
			AddLabel( 167, 27, 200, "Spawn It" );

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
			AddLabel( 35, 51, 246, "Blighted Grove" );
			AddLabel( 35, 76, 200, "Britain Sewer" );
			AddLabel( 35, 101, 200, "Covetous" );
			AddLabel( 35, 126, 200, "Deceit" );
			AddLabel( 35, 151, 200, "Despise" );
			AddLabel( 35, 176, 200, "Destard" );
			AddLabel( 35, 201, 200, "Fire" );
			AddLabel( 35, 226, 200, "Graveyards" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 101 );
			AddCheck( 182, 73, 210, 211, true, 102 );
			AddCheck( 182, 98, 210, 211, true, 103 );
			AddCheck( 182, 123, 210, 211, true, 104 );
			AddCheck( 182, 148, 210, 211, true, 105 );
			AddCheck( 182, 173, 210, 211, true, 106 );
			AddCheck( 182, 198, 210, 211, true, 107 );
			AddCheck( 182, 223, 210, 211, true, 108 );

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
			AddLabel( 167, 27, 200, "Spawn It" );

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
			AddLabel( 35, 176, 246, "Painted Caves" );
			AddLabel( 35, 201, 246, "Palace of Paroxysmus" );
			AddLabel( 35, 226, 246, "Prism of Light" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 109 );
			AddCheck( 182, 73, 210, 211, true, 110 );
			AddCheck( 182, 98, 210, 211, true, 111 );
			AddCheck( 182, 123, 210, 211, true, 112 );
			AddCheck( 182, 148, 210, 211, true, 113 );
			AddCheck( 182, 173, 210, 211, true, 114 );
			AddCheck( 182, 198, 210, 211, true, 115 );
			AddCheck( 182, 223, 210, 211, true, 116 );

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
			AddLabel( 167, 27, 200, "Spawn It" );

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
			AddLabel( 35, 51, 246, "Sanctuary" );
			AddLabel( 35, 76, 200, "Sea Life" );
			AddLabel( 35, 101, 200, "Shame" );
			AddLabel( 35, 126, 200, "Solen Hive" );
			AddLabel( 35, 151, 200, "Terathan Keep" );
			AddLabel( 35, 176, 200, "Towns Life" );
			AddLabel( 35, 201, 200, "Towns People" );
			AddLabel( 35, 226, 200, "Trinsic Passage" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 117 );
			AddCheck( 182, 73, 210, 211, true, 118 );
			AddCheck( 182, 98, 210, 211, true, 119 );
			AddCheck( 182, 123, 210, 211, true, 120 );
			AddCheck( 182, 148, 210, 211, true, 121 );
			AddCheck( 182, 173, 210, 211, true, 122 );
			AddCheck( 182, 198, 210, 211, true, 123 );
			AddCheck( 182, 223, 210, 211, true, 124 );

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
			AddLabel( 167, 27, 200, "Spawn It" );

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
			AddCheck( 182, 48, 210, 211, true, 125 );
			AddCheck( 182, 73, 210, 211, true, 126 );
			AddCheck( 182, 98, 210, 211, true, 127 );
			//AddCheck( 182, 123, 210, 211, true, 128 );
			//AddCheck( 182, 148, 210, 211, true, 129 );
			//AddCheck( 182, 173, 210, 211, true, 130 );
			//AddCheck( 182, 198, 210, 211, true, 131 );
			//AddCheck( 182, 223, 210, 211, true, 132 );

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

						from.Say( "SPAWNING TRAMMEL..." );

						// Now spawn any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/BlightedGrove.map", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/BritainSewer.map", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Covetous.map", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Deceit.map", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Despise.map", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Destard.map", prefix ) );
						}
						if( Selections.Contains( 107 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Fire.map", prefix ) );
						}
						if( Selections.Contains( 108 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Graveyards.map", prefix ) );
						}
						if( Selections.Contains( 109 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Hythloth.map", prefix ) );
						}
						if( Selections.Contains( 110 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Ice.map", prefix ) );
						}
						if( Selections.Contains( 111 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/LostLands.map", prefix ) );
						}
						if( Selections.Contains( 112 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/OrcCaves.map", prefix ) );
						}
						if( Selections.Contains( 113 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Outdoors.map", prefix ) );
						}
						if( Selections.Contains( 114 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/PaintedCaves.map", prefix ) );
						}
						if( Selections.Contains( 115 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/PalaceOfParoxysmus.map", prefix ) );
						}
						if( Selections.Contains( 116 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/PrismOfLight.map", prefix ) );
						}
						if( Selections.Contains( 117 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Sanctuary.map", prefix ) );
						}
						if( Selections.Contains( 118 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/SeaLife.map", prefix ) );
						}
						if( Selections.Contains( 119 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Shame.map", prefix ) );
						}
						if( Selections.Contains( 120 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/SolenHive.map", prefix ) );
						}
						if( Selections.Contains( 121 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/TerathanKeep.map", prefix ) );
						}
						if( Selections.Contains( 122 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/TownsLife.map", prefix ) );
						}
						if( Selections.Contains( 123 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/TownsPeople.map", prefix ) );
						}
						if( Selections.Contains( 124 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/TrinsicPassage.map", prefix ) );
						}
						if( Selections.Contains( 125 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Vendors.map", prefix ) );
						}
						if( Selections.Contains( 126 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/WildLife.map", prefix ) );
						}
						if( Selections.Contains( 127 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen trammel/Wrong.map", prefix ) );
						}
					}

					from.Say( "Spawn generation completed!" );
					break;
				}
			}
		}
	}

	public class FeluccaGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public FeluccaGump( CommandEventArgs e ) : base( 50,50 )
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
			AddLabel( 167, 27, 200, "Spawn It" );

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
			AddLabel( 35, 51, 246, "Blighted Grove" );
			AddLabel( 35, 76, 200, "Britain Sewer" );
			AddLabel( 35, 101, 200, "Covetous" );
			AddLabel( 35, 126, 200, "Deceit" );
			AddLabel( 35, 151, 200, "Despise" );
			AddLabel( 35, 176, 200, "Destard" );
			AddLabel( 35, 201, 200, "Fire" );
			AddLabel( 35, 226, 200, "Graveyards" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 101 );
			AddCheck( 182, 73, 210, 211, true, 102 );
			AddCheck( 182, 98, 210, 211, true, 103 );
			AddCheck( 182, 123, 210, 211, true, 104 );
			AddCheck( 182, 148, 210, 211, true, 105 );
			AddCheck( 182, 173, 210, 211, true, 106 );
			AddCheck( 182, 198, 210, 211, true, 107 );
			AddCheck( 182, 223, 210, 211, true, 108 );

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
			AddLabel( 167, 27, 200, "Spawn It" );

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
			AddLabel( 35, 201, 246, "Painted Caves" );
			AddLabel( 35, 226, 246, "Palace of Paroxysmus" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 109 );
			AddCheck( 182, 73, 210, 211, true, 110 );
			AddCheck( 182, 98, 210, 211, true, 111 );
			AddCheck( 182, 123, 210, 211, true, 112 );
			AddCheck( 182, 148, 210, 211, true, 113 );
			AddCheck( 182, 173, 210, 211, true, 114 );
			AddCheck( 182, 198, 210, 211, true, 115 );
			AddCheck( 182, 223, 210, 211, true, 116 );

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
			AddLabel( 167, 27, 200, "Spawn It" );

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
			AddLabel( 35, 51, 246, "Prism of Light" );
			AddLabel( 35, 76, 246, "Sanctuary" );
			AddLabel( 35, 101, 200, "Sea Life" );
			AddLabel( 35, 126, 200, "Shame" );
			AddLabel( 35, 151, 200, "Solen Hive" );
			AddLabel( 35, 176, 200, "Terathan Keep" );
			AddLabel( 35, 201, 200, "Towns Life" );
			AddLabel( 35, 226, 200, "Towns People" );

			//Check boxes
			AddCheck( 182, 48, 210, 211, true, 117 );
			AddCheck( 182, 73, 210, 211, true, 118 );
			AddCheck( 182, 98, 210, 211, true, 119 );
			AddCheck( 182, 123, 210, 211, true, 120 );
			AddCheck( 182, 148, 210, 211, true, 121 );
			AddCheck( 182, 173, 210, 211, true, 122 );
			AddCheck( 182, 198, 210, 211, true, 123 );
			AddCheck( 182, 223, 210, 211, true, 124 );

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
			AddLabel( 167, 27, 200, "Spawn It" );

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
			AddCheck( 182, 48, 210, 211, true, 125 );
			AddCheck( 182, 73, 210, 211, true, 126 );
			AddCheck( 182, 98, 210, 211, true, 127 );
			AddCheck( 182, 123, 210, 211, true, 128 );
			//AddCheck( 182, 148, 210, 211, true, 129 );
			//AddCheck( 182, 173, 210, 211, true, 130 );
			//AddCheck( 182, 198, 210, 211, true, 131 );
			//AddCheck( 182, 223, 210, 211, true, 132 );

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

						from.Say( "SPAWNING FELUCCA..." );

						// Now spawn any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/BlightedGrove.map", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/BritainSewer.map", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Covetous.map", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Deceit.map", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Despise.map", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Destard.map", prefix ) );
						}
						if( Selections.Contains( 107 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Fire.map", prefix ) );
						}
						if( Selections.Contains( 108 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Graveyards.map", prefix ) );
						}
						if( Selections.Contains( 109 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Hythloth.map", prefix ) );
						}
						if( Selections.Contains( 110 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Ice.map", prefix ) );
						}
						if( Selections.Contains( 111 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Khaldun.map", prefix ) );
						}
						if( Selections.Contains( 112 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/LostLands.map", prefix ) );
						}
						if( Selections.Contains( 113 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/OrcCaves.map", prefix ) );
						}
						if( Selections.Contains( 114 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Outdoors.map", prefix ) );
						}
						if( Selections.Contains( 115 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/PaintedCaves.map", prefix ) );
						}
						if( Selections.Contains( 116 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/PalaceOfParoxysmus.map", prefix ) );
						}
						if( Selections.Contains( 117 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/PrismOfLight.map", prefix ) );
						}
						if( Selections.Contains( 118 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Sanctuary.map", prefix ) );
						}
						if( Selections.Contains( 119 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/SeaLife.map", prefix ) );
						}
						if( Selections.Contains( 120 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Shame.map", prefix ) );
						}
						if( Selections.Contains( 121 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/SolenHive.map", prefix ) );
						}
						if( Selections.Contains( 122 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/TerathanKeep.map", prefix ) );
						}
						if( Selections.Contains( 123 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/TownsLife.map", prefix ) );
						}
						if( Selections.Contains( 124 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/TownsPeople.map", prefix ) );
						}
						if( Selections.Contains( 125 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/TrinsicPassage.map", prefix ) );
						}
						if( Selections.Contains( 126 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Vendors.map", prefix ) );
						}
						if( Selections.Contains( 127 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/WildLife.map", prefix ) );
						}
						if( Selections.Contains( 128 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen felucca/Wrong.map", prefix ) );
						}
					}

					from.Say( "Spawn generation completed!" );
					break;
				}
			}
		}
	}

	public class IlshenarGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public IlshenarGump( CommandEventArgs e ) : base( 50,50 )
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
			AddLabel( 167, 27, 200, "Spawn It" );
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
			AddCheck( 182, 48, 210, 211, true, 101 );
			AddCheck( 182, 73, 210, 211, true, 102 );
			AddCheck( 182, 98, 210, 211, true, 103 );
			AddCheck( 182, 123, 210, 211, true, 104 );
			AddCheck( 182, 148, 210, 211, true, 105 );
			AddCheck( 182, 173, 210, 211, true, 106 );
			AddCheck( 182, 198, 210, 211, true, 107 );
			AddCheck( 182, 223, 210, 211, true, 108 );

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
			AddLabel( 167, 27, 200, "Spawn It" );
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
			AddLabel( 35, 176, 246, "Twisted Weald" );
			//AddLabel( 35, 201, 200, "15" );
			//AddLabel( 35, 226, 200, "16" );

			//Options
			AddCheck( 182, 48, 210, 211, true, 109 );
			AddCheck( 182, 73, 210, 211, true, 110 );
			AddCheck( 182, 98, 210, 211, true, 111 );
			AddCheck( 182, 123, 210, 211, true, 112 );
			AddCheck( 182, 148, 210, 211, true, 113 );
			AddCheck( 182, 173, 210, 211, true, 114 );
			//AddCheck( 182, 198, 210, 211, true, 115 );
			//AddCheck( 182, 223, 210, 211, true, 116 );

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

						from.Say( "SPAWNING ILSHENAR..." );

						// Now spawn any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Ancientlair.map", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Ankh.map", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Blood.map", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Exodus.map", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Mushroom.map", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Outdoors.map", prefix ) );
						}
						if( Selections.Contains( 107 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Ratmancave.map", prefix ) );
						}
						if( Selections.Contains( 108 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Rock.map", prefix ) );
						}
						if( Selections.Contains( 109 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Sorcerers.map", prefix ) );
						}
						if( Selections.Contains( 110 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Spectre.map", prefix ) );
						}
						if( Selections.Contains( 111 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Towns.map", prefix ) );
						}
						if( Selections.Contains( 112 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Vendors.map", prefix ) );
						}
						if( Selections.Contains( 113 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/Wisp.map", prefix ) );
						}
						if( Selections.Contains( 114 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen ilshenar/TwistedWeald.map", prefix ) );
						}
					}

					from.Say( "Spawn generation completed!" );

					break;
				}
			}
		}
	}

	public class MalasGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public MalasGump( CommandEventArgs e ) : base( 50,50 )
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
			AddLabel( 167, 27, 200, "Spawn It" );
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
			AddLabel( 35, 176, 246, "Citadel" );
			AddLabel( 35, 201, 246, "Labyrinth" );

			//Options
			AddCheck( 182, 48, 210, 211, true, 101 );
			AddCheck( 182, 73, 210, 211, true, 102 );
			AddCheck( 182, 98, 210, 211, true, 103 );
			AddCheck( 182, 123, 210, 211, true, 104 );
			AddCheck( 182, 148, 210, 211, true, 105 );
			AddCheck( 182, 173, 210, 211, true, 106 );
			AddCheck( 182, 198, 210, 211, true, 107 );

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

						from.Say( "SPAWNING MALAS..." );

						// Now spawn any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen malas/Doom.map", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen malas/North.map", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen malas/OrcForts.map", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen malas/South.map", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen malas/Vendors.map", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen malas/Citadel.map", prefix ) );
						}
						if( Selections.Contains( 107 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen malas/Labyrinth.map", prefix ) );
						}
					}

					from.Say( "Spawn generation completed!" );

					break;
				}
			}
		}
	}

	public class TokunoGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public TokunoGump( CommandEventArgs e ) : base( 50,50 )
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
			AddLabel( 167, 27, 200, "Spawn It" );
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
			AddCheck( 182, 48, 210, 211, true, 101 );
			AddCheck( 182, 73, 210, 211, true, 102 );
			AddCheck( 182, 98, 210, 211, true, 103 );
			AddCheck( 182, 123, 210, 211, true, 104 );
			AddCheck( 182, 148, 210, 211, true, 105 );
			AddCheck( 182, 173, 210, 211, true, 106 );

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

						from.Say( "SPAWNING TOKUNO..." );

						// Now spawn any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen tokuno/FanDancersDojo.map", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen tokuno/Outdoors.map", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen tokuno/TownsLife.map", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen tokuno/Vendors.map", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen tokuno/WildLife.map", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen tokuno/YomutsoMines.map", prefix ) );
						}
					}

					from.Say( "Spawn generation completed!" );

					break;
				}
			}
		}
	}

	public class TerMurGump : Gump
	{
		private CommandEventArgs m_CommandEventArgs;
		public TerMurGump( CommandEventArgs e ) : base( 50,50 )
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
			AddLabel( 167, 27, 200, "Spawn It" );
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
			AddCheck( 182, 48, 210, 211, true, 101 );
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

						from.Say( "SPAWNING TER MUR..." );

						// Now spawn any selected maps

						if( Selections.Contains( 101 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}Spawngen termur/Vendors.map", prefix ) );
						}
						if( Selections.Contains( 102 ) == true )
						{
							//CommandSystem.Handle( from, String.Format( "{0}Spawngen termur/.map", prefix ) );
						}
						if( Selections.Contains( 103 ) == true )
						{
							//CommandSystem.Handle( from, String.Format( "{0}Spawngen termur/.map", prefix ) );
						}
						if( Selections.Contains( 104 ) == true )
						{
							//CommandSystem.Handle( from, String.Format( "{0}Spawngen termur/.map", prefix ) );
						}
						if( Selections.Contains( 105 ) == true )
						{
							//CommandSystem.Handle( from, String.Format( "{0}Spawngen termur/.map", prefix ) );
						}
						if( Selections.Contains( 106 ) == true )
						{
							//CommandSystem.Handle( from, String.Format( "{0}Spawngen termur/.map", prefix ) );
						}
					}

					from.Say( "Spawn generation completed!" );

					break;
				}
			}
		}
	}
}