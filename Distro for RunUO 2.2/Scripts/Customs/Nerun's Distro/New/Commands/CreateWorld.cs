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
	public class CreateWorld
	{
		public CreateWorld()
		{
		}

		public static void Initialize() 
		{ 
			CommandSystem.Register( "Createworld", AccessLevel.Administrator, new CommandEventHandler( Create_OnCommand ) ); 
		} 

		[Usage( "[createworld" )]
		[Description( "Create world with a menu." )]
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
		public CreateWorldGump( CommandEventArgs e ) : base( 50,50 )
		{
			m_CommandEventArgs = e;
			Closable = true;
			Dragable = true;

			AddPage(1);

	//fundo cinza
	//x, y, largura, altura, item
			AddBackground( 0, 0, 200, 400, 5054 );
	//----------
			AddLabel( 30, 2, 200, "CREATE WORLD GUMP" );
	//fundo branco
	//x, y, largura, altura, item
			AddImageTiled( 10, 20, 180, 335, 3004 );
	//----------
			AddLabel( 20, 26, 200, "Moongen" );
			AddLabel( 20, 51, 200, "DoorGen" );
			AddLabel( 20, 76, 200, "Decorate" );
			AddLabel( 20, 101, 200, "SignGen" );
			AddLabel( 20, 126, 200, "TelGen" );
			AddLabel( 20, 151, 200, "GenGauntlet" );
			AddLabel( 20, 176, 246, "GenChampions" );
			AddLabel( 20, 201, 200, "GenKhaldun" );
			AddLabel( 20, 226, 200, "GenerateFactions" );
			AddLabel( 20, 251, 200, "GenStealArties" );
			AddLabel( 20, 276, 200, "SHTelGen" );
			AddLabel( 20, 301, 200, "SecretLocGen" );
			AddLabel( 20, 326, 246, "DecorateML" );
	//Options
			AddCheck( 160, 23, 210, 211, true, 101 );
			AddCheck( 160, 48, 210, 211, true, 102 );
			AddCheck( 160, 73, 210, 211, true, 103 );
			AddCheck( 160, 98, 210, 211, true, 104 );
			AddCheck( 160, 123, 210, 211, true, 105 );
			AddCheck( 160, 148, 210, 211, true, 106 );
			AddCheck( 160, 173, 210, 211, true, 107 );
			AddCheck( 160, 198, 210, 211, true, 108 );
			AddCheck( 160, 223, 210, 211, true, 109 );
			AddCheck( 160, 248, 210, 211, true, 110 );
			AddCheck( 160, 273, 210, 211, true, 111 );
			AddCheck( 160, 298, 210, 211, true, 112 );
			AddCheck( 160, 323, 210, 211, true, 113 );

	//Ok, Cancel (x, y, ?, ?, ?)
			AddButton( 30, 365, 247, 249, 1, GumpButtonType.Reply, 0 );
			AddButton( 100, 365, 241, 243, 0, GumpButtonType.Reply, 0 );

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

						from.Say( "CREATING WORLD..." );

						// Now use any selected command
						if( Selections.Contains( 101 ) == true )
						{
							from.Say( "Generating moongates..." );
							CommandSystem.Handle( from, String.Format( "{0}moongen", prefix ) );
						}

						if( Selections.Contains( 102 ) == true )
						{
							from.Say( "Generating doors..." );
							CommandSystem.Handle( from, String.Format( "{0}doorgen", prefix ) );
						}

						if( Selections.Contains( 103 ) == true )
						{
							from.Say( "Decorating world..." );
							CommandSystem.Handle( from, String.Format( "{0}decorate", prefix ) );
						}

						if( Selections.Contains( 104 ) == true )
						{
							from.Say( "Generating signs..." );
							CommandSystem.Handle( from, String.Format( "{0}signgen", prefix ) );
						}

						if( Selections.Contains( 105 ) == true )
						{
							from.Say( "Generating teleporters..." );
							CommandSystem.Handle( from, String.Format( "{0}telgen", prefix ) );
						}

						if( Selections.Contains( 106 ) == true )
						{
							from.Say( "Generating Gauntlet spawners..." );
							CommandSystem.Handle( from, String.Format( "{0}gengauntlet", prefix ) );
						}

						if( Selections.Contains( 107 ) == true )
						{
							// champions message in champions script
							CommandSystem.Handle( from, String.Format( "{0}genchampions", prefix ) );
						}

						if( Selections.Contains( 108 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}genkhaldun", prefix ) );
						}

						if( Selections.Contains( 109 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}generatefactions", prefix ) );
							from.Say( "Factions Generated!" );
						}

						if( Selections.Contains( 110 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}genstealarties", prefix ) );
							from.Say( "Stealable Artifacts Generated!" );
						}

						if( Selections.Contains( 111 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}shtelgen", prefix ) );
						}

						if( Selections.Contains( 112 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}secretlocgen", prefix ) );
						}

						if( Selections.Contains( 113 ) == true )
						{
							CommandSystem.Handle( from, String.Format( "{0}decorateml", prefix ) );
						}
					}

					from.Say( "World generation completed!" );

					break;
				} 
			} 
		}
	}
}