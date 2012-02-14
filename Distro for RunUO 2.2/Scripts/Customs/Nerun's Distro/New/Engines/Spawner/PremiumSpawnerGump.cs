//Engine r1
using System;
using System.Collections;
using Server.Network;
using Server.Gumps;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public class PremiumSpawnerGump : Gump
	{
		private PremiumSpawner m_Spawner;

		public void AddBlackAlpha( int x, int y, int width, int height )
		{
			AddImageTiled( x, y, width, height, 2624 );
			AddAlphaRegion( x, y, width, height );
		}

		public PremiumSpawnerGump( PremiumSpawner spawner ) : base( 50, 50 )
		{
			m_Spawner = spawner;

			AddPage( 1 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 1" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 1001, GumpButtonType.Reply, 0 );
			AddLabel( 260, 60, 52, "Okay" );

			AddButton( 260, 90, 0xFB4, 0xFB6, 200, GumpButtonType.Reply, 0 );
			AddLabel( 232, 110, 52, "Bring to Home" );

			AddButton( 260, 140, 0xFA8, 0xFAA, 300, GumpButtonType.Reply, 0 );
			AddLabel( 232, 160, 52, "Total Respawn" );

			AddButton( 260, 190, 0xFAB, 0xFAD, 400, GumpButtonType.Reply, 0 );
			AddLabel( 245, 210, 52, "Properties" );

			AddButton( 260, 240, 0xFB1, 0xFB3, 500, GumpButtonType.Reply, 0 );
			AddLabel( 256, 260, 52, "Cancel" );

			AddButton( 230, 320, 5603, 5607, 0, GumpButtonType.Page, 6 );
			AddButton( 302, 320, 5601, 5605, 0, GumpButtonType.Page, 2 );
			AddLabel( 258, 320, 52, "- 1 -" );

			for ( int i = 0;  i < 15; i++ )
			{
				// AddButton ( x, y, image, imageOnClick, ButtonID )
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, (0 + i), GumpButtonType.Reply, 0 ); // > (spawn this creature)
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, (90 + i), GumpButtonType.Reply, 0 ); // X (remove this creature)

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.CreaturesName.Count )
				{
					str = (string)spawner.CreaturesName[i];
					int count = m_Spawner.CountCreatures( str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 100 + i, str );
			}

			AddPage( 2 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 2" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 1002, GumpButtonType.Reply, 0 );
			AddLabel( 260, 60, 52, "Okay" );

			AddButton( 260, 90, 0xFB4, 0xFB6, 200, GumpButtonType.Reply, 0 );
			AddLabel( 232, 110, 52, "Bring to Home" );

			AddButton( 260, 140, 0xFA8, 0xFAA, 300, GumpButtonType.Reply, 0 );
			AddLabel( 232, 160, 52, "Total Respawn" );

			AddButton( 260, 190, 0xFAB, 0xFAD, 400, GumpButtonType.Reply, 0 );
			AddLabel( 245, 210, 52, "Properties" );

			AddButton( 260, 240, 0xFB1, 0xFB3, 500, GumpButtonType.Reply, 0 );
			AddLabel( 256, 260, 52, "Cancel" );

			AddButton( 230, 320, 5603, 5607, 0, GumpButtonType.Page, 1 );
			AddButton( 302, 320, 5601, 5605, 0, GumpButtonType.Page, 3 );
			AddLabel( 258, 320, 52, "- 2 -" );

			for ( int i = 0;  i < 15; i++ )
			{
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, (15 + i), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, (105 + i), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerA.Count )
				{
					str = (string)spawner.SubSpawnerA[i];
					int count = m_Spawner.CountCreaturesA( str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 200 + i, str );
			}

			AddPage( 3 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 3" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 1003, GumpButtonType.Reply, 0 );
			AddLabel( 260, 60, 52, "Okay" );

			AddButton( 260, 90, 0xFB4, 0xFB6, 200, GumpButtonType.Reply, 0 );
			AddLabel( 232, 110, 52, "Bring to Home" );

			AddButton( 260, 140, 0xFA8, 0xFAA, 300, GumpButtonType.Reply, 0 );
			AddLabel( 232, 160, 52, "Total Respawn" );

			AddButton( 260, 190, 0xFAB, 0xFAD, 400, GumpButtonType.Reply, 0 );
			AddLabel( 245, 210, 52, "Properties" );

			AddButton( 260, 240, 0xFB1, 0xFB3, 500, GumpButtonType.Reply, 0 );
			AddLabel( 256, 260, 52, "Cancel" );

			AddButton( 230, 320, 5603, 5607, 0, GumpButtonType.Page, 2 );
			AddButton( 302, 320, 5601, 5605, 0, GumpButtonType.Page, 4 );
			AddLabel( 258, 320, 52, "- 3 -" );

			for ( int i = 0;  i < 15; i++ )
			{
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, (30 + i), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, (120 + i), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerB.Count )
				{
					str = (string)spawner.SubSpawnerB[i];
					int count = m_Spawner.CountCreaturesB( str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 300 + i, str );
			}

			AddPage( 4 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 4" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 1004, GumpButtonType.Reply, 0 );
			AddLabel( 260, 60, 52, "Okay" );

			AddButton( 260, 90, 0xFB4, 0xFB6, 200, GumpButtonType.Reply, 0 );
			AddLabel( 232, 110, 52, "Bring to Home" );

			AddButton( 260, 140, 0xFA8, 0xFAA, 300, GumpButtonType.Reply, 0 );
			AddLabel( 232, 160, 52, "Total Respawn" );

			AddButton( 260, 190, 0xFAB, 0xFAD, 400, GumpButtonType.Reply, 0 );
			AddLabel( 245, 210, 52, "Properties" );

			AddButton( 260, 240, 0xFB1, 0xFB3, 500, GumpButtonType.Reply, 0 );
			AddLabel( 256, 260, 52, "Cancel" );

			AddButton( 230, 320, 5603, 5607, 0, GumpButtonType.Page, 3 );
			AddButton( 302, 320, 5601, 5605, 0, GumpButtonType.Page, 5 );
			AddLabel( 258, 320, 52, "- 4 -" );

			for ( int i = 0;  i < 15; i++ )
			{
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, (45 + i), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, (135 + i), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerC.Count )
				{
					str = (string)spawner.SubSpawnerC[i];
					int count = m_Spawner.CountCreaturesC( str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 400 + i, str );
			}

			AddPage( 5 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 5" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 1005, GumpButtonType.Reply, 0 );
			AddLabel( 260, 60, 52, "Okay" );

			AddButton( 260, 90, 0xFB4, 0xFB6, 200, GumpButtonType.Reply, 0 );
			AddLabel( 232, 110, 52, "Bring to Home" );

			AddButton( 260, 140, 0xFA8, 0xFAA, 300, GumpButtonType.Reply, 0 );
			AddLabel( 232, 160, 52, "Total Respawn" );

			AddButton( 260, 190, 0xFAB, 0xFAD, 400, GumpButtonType.Reply, 0 );
			AddLabel( 245, 210, 52, "Properties" );

			AddButton( 260, 240, 0xFB1, 0xFB3, 500, GumpButtonType.Reply, 0 );
			AddLabel( 256, 260, 52, "Cancel" );

			AddButton( 230, 320, 5603, 5607, 0, GumpButtonType.Page, 4 );
			AddButton( 302, 320, 5601, 5605, 0, GumpButtonType.Page, 6 );
			AddLabel( 258, 320, 52, "- 5 -" );

			for ( int i = 0;  i < 15; i++ )
			{
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, (60 + i), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, (150 + i), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerD.Count )
				{
					str = (string)spawner.SubSpawnerD[i];
					int count = m_Spawner.CountCreaturesD( str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 500 + i, str );
			}

			AddPage( 6 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 6" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 1006, GumpButtonType.Reply, 0 );
			AddLabel( 260, 60, 52, "Okay" );

			AddButton( 260, 90, 0xFB4, 0xFB6, 200, GumpButtonType.Reply, 0 );
			AddLabel( 232, 110, 52, "Bring to Home" );

			AddButton( 260, 140, 0xFA8, 0xFAA, 300, GumpButtonType.Reply, 0 );
			AddLabel( 232, 160, 52, "Total Respawn" );

			AddButton( 260, 190, 0xFAB, 0xFAD, 400, GumpButtonType.Reply, 0 );
			AddLabel( 245, 210, 52, "Properties" );

			AddButton( 260, 240, 0xFB1, 0xFB3, 500, GumpButtonType.Reply, 0 );
			AddLabel( 256, 260, 52, "Cancel" );

			AddButton( 230, 320, 5603, 5607, 0, GumpButtonType.Page, 5 );
			AddButton( 302, 320, 5601, 5605, 0, GumpButtonType.Page, 1 );
			AddLabel( 258, 320, 52, "- 6 -" );

			for ( int i = 0;  i < 15; i++ )
			{
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, (75 + i), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, (165 + i), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerE.Count )
				{
					str = (string)spawner.SubSpawnerE[i];
					int count = m_Spawner.CountCreaturesE( str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 600 + i, str );
			}

		}

		public List<string> CreateArray( RelayInfo info, Mobile from, int TextIndex )
		{
			List<string> creaturesName = new List<string>();

			for ( int i = 0;  i < 15; i++ )
			{
				TextRelay te = info.GetTextEntry( TextIndex + i );

				if ( te != null )
				{
					string str = te.Text;

					if ( str.Length > 0 )
					{
						str = str.Trim();

						string t = Spawner.ParseType( str );

						Type type = ScriptCompiler.FindTypeByName( t );

						if ( type != null )
							creaturesName.Add( str );
						else
							from.SendMessage( "{0} is not a valid type name.", t );
					}
				}
			}

			return creaturesName;
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( m_Spawner.Deleted )
				return;

			switch ( info.ButtonID )
			{
				case 200: // Bring everything home
				{
					m_Spawner.BringToHome();
					break;
				}
				case 300: // Complete respawn
				{
					m_Spawner.Respawn();
					break;
				}
				case 400: // Props
				{
					state.Mobile.SendGump( new PropertiesGump( state.Mobile, m_Spawner ) );
					state.Mobile.SendGump( new PremiumSpawnerGump( m_Spawner ) );
					break;
				}
				case 500: // Cancel
				{
					break;
				}
				case 1001: // Okay
				{
					m_Spawner.CreaturesName = CreateArray( info, state.Mobile, 100 );
					break;
				}
				case 1002: // Okay
				{
					m_Spawner.SubSpawnerA = CreateArray( info, state.Mobile, 200 );
					break;
				}
				case 1003: // Okay
				{
					m_Spawner.SubSpawnerB = CreateArray( info, state.Mobile, 300 );
					break;
				}
				case 1004: // Okay
				{
					m_Spawner.SubSpawnerC = CreateArray( info, state.Mobile, 400 );
					break;
				}
				case 1005: // Okay
				{
					m_Spawner.SubSpawnerD = CreateArray( info, state.Mobile, 500 );
					break;
				}
				case 1006: // Okay
				{
					m_Spawner.SubSpawnerE = CreateArray( info, state.Mobile, 600 );
					break;
				}
				default:
				{ //ButtonID: 1-90 spawn; 91-180 remove
					int buttonID = info.ButtonID;
					
					int Type = 0;
					
					// Choose the right TextEntry number
					if ( (buttonID >= 1) && (buttonID <= 15) )
						Type += 100 + buttonID;
					else if ( (buttonID >= 16) && (buttonID <= 30) )
						Type += 200 + buttonID - 15;
					else if ( (buttonID >= 31) && (buttonID <= 45) )
						Type += 300 + buttonID - 30;
					else if ( (buttonID >= 46) && (buttonID <= 60) )
						Type += 400 + buttonID - 45;
					else if ( (buttonID >= 61) && (buttonID <= 75) )
						Type += 500 + buttonID - 60;
					else if ( (buttonID >= 76) && (buttonID <= 90) )
						Type += 600 + buttonID - 75;
					// Remove creature
					else if ( (buttonID >= 91) && (buttonID <= 105) )
						Type += 100 + buttonID - 90;
					else if ( (buttonID >= 106) && (buttonID <= 120) )
						Type += 200 + buttonID - 105;
					else if ( (buttonID >= 121) && (buttonID <= 135) )
						Type += 300 + buttonID - 120;
					else if ( (buttonID >= 136) && (buttonID <= 150) )
						Type += 400 + buttonID - 135;
					else if ( (buttonID >= 151) && (buttonID <= 165) )
						Type += 500 + buttonID - 150;
					else if ( (buttonID >= 166) && (buttonID <= 180) )
						Type += 600 + buttonID - 165;

					TextRelay entry = info.GetTextEntry( Type );
					
					if ( entry != null && entry.Text.Length > 0 )
					{
						// Spawn creature
						if ( (buttonID >= 1) && (buttonID <= 15) )
							m_Spawner.Spawn( entry.Text );
						else if ( (buttonID >= 16) && (buttonID <= 30) )
							m_Spawner.SpawnA( entry.Text );
						else if ( (buttonID >= 31) && (buttonID <= 45) )
							m_Spawner.SpawnB( entry.Text );
						else if ( (buttonID >= 46) && (buttonID <= 60) )
							m_Spawner.SpawnC( entry.Text );
						else if ( (buttonID >= 61) && (buttonID <= 75) )
							m_Spawner.SpawnD( entry.Text );
						else if ( (buttonID >= 76) && (buttonID <= 90) )
							m_Spawner.SpawnE( entry.Text );
						// Remove creature
						else if ( (buttonID >= 91) && (buttonID <= 105) )
							m_Spawner.RemoveCreatures( entry.Text );
						else if ( (buttonID >= 106) && (buttonID <= 120) )
							m_Spawner.RemoveCreaturesA( entry.Text );
						else if ( (buttonID >= 121) && (buttonID <= 135) )
							m_Spawner.RemoveCreaturesB( entry.Text );
						else if ( (buttonID >= 136) && (buttonID <= 150) )
							m_Spawner.RemoveCreaturesC( entry.Text );
						else if ( (buttonID >= 151) && (buttonID <= 165) )
							m_Spawner.RemoveCreaturesD( entry.Text );
						else if ( (buttonID >= 166) && (buttonID <= 180) )
							m_Spawner.RemoveCreaturesE( entry.Text );

						m_Spawner.CreaturesName = CreateArray( info, state.Mobile, 100 );
						m_Spawner.SubSpawnerA = CreateArray( info, state.Mobile, 200 );
						m_Spawner.SubSpawnerB = CreateArray( info, state.Mobile, 300 );
						m_Spawner.SubSpawnerC = CreateArray( info, state.Mobile, 400 );
						m_Spawner.SubSpawnerD = CreateArray( info, state.Mobile, 500 );
						m_Spawner.SubSpawnerE = CreateArray( info, state.Mobile, 600 );
					}

					break;
				}
			}
		}
	}
}