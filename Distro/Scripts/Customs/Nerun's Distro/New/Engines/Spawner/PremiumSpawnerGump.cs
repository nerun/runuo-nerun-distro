//Engine r120
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

			AddButton( 260, 40, 0xFB7, 0xFB9, 1000, GumpButtonType.Reply, 0 );
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
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, (1 + i), GumpButtonType.Reply, 0 ); // > (spawn this creature)
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, (91 + i), GumpButtonType.Reply, 0 ); // X (remove this creature)

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.CreaturesName.Count )
				{
					str = (string)spawner.CreaturesName[i];
					int count = m_Spawner.CountCreatures( m_Spawner.Creatures, str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 101 + i, str );
			}

			AddPage( 2 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 2" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 1000, GumpButtonType.Reply, 0 );
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
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, (16 + i), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, (106 + i), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerA.Count )
				{
					str = (string)spawner.SubSpawnerA[i];
					int count = m_Spawner.CountCreatures( m_Spawner.CreaturesA, str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 201 + i, str );
			}

			AddPage( 3 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 3" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 1000, GumpButtonType.Reply, 0 );
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
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, (31 + i), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, (121 + i), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerB.Count )
				{
					str = (string)spawner.SubSpawnerB[i];
					int count = m_Spawner.CountCreatures( m_Spawner.CreaturesB, str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 301 + i, str );
			}

			AddPage( 4 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 4" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 1000, GumpButtonType.Reply, 0 );
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
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, (46 + i), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, (136 + i), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerC.Count )
				{
					str = (string)spawner.SubSpawnerC[i];
					int count = m_Spawner.CountCreatures( m_Spawner.CreaturesC, str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 401 + i, str );
			}

			AddPage( 5 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 5" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 1000, GumpButtonType.Reply, 0 );
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
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, (61 + i), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, (151 + i), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerD.Count )
				{
					str = (string)spawner.SubSpawnerD[i];
					int count = m_Spawner.CountCreatures( m_Spawner.CreaturesD, str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 501 + i, str );
			}

			AddPage( 6 );

			AddBackground( 0, 0, 350, 360, 5054 );

			AddLabel( 80, 1, 52, "Creatures List 6" );

			AddLabel( 215, 3, 52, "PREMIUM SPAWNER" );
			AddBlackAlpha( 213, 23, 125, 270 );

			AddButton( 260, 40, 0xFB7, 0xFB9, 1000, GumpButtonType.Reply, 0 );
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
				AddButton( 5, ( 22 * i ) + 20, 0xFA5, 0xFA7, (76 + i), GumpButtonType.Reply, 0 );
				AddButton( 38, ( 22 * i ) + 20, 0xFA2, 0xFA4, (166 + i), GumpButtonType.Reply, 0 );

				AddImageTiled( 71, ( 22 * i ) + 20, 119, 23, 0xA40 );
				AddImageTiled( 72, ( 22 * i ) + 21, 117, 21, 0xBBC );

				string str = "";

				if ( i < spawner.SubSpawnerE.Count )
				{
					str = (string)spawner.SubSpawnerE[i];
					int count = m_Spawner.CountCreatures( m_Spawner.CreaturesE, str );

					AddLabel( 192, ( 22 * i ) + 20, 0, count.ToString() );
				}

				AddTextEntry( 75, ( 22 * i ) + 21, 114, 21, 0, 601 + i, str );
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
		
		public string GetEntry( int Type, RelayInfo info )
		{
			TextRelay entry = info.GetTextEntry( Type );
			return entry.Text;
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( m_Spawner.Deleted )
				return;

			switch ( info.ButtonID )
			{
				case 0: // Cancel (mouse's right button click anywhere on the gump)
				{
					break;
				}
				case 200: // Bring everything home
				{
					m_Spawner.BringToHome();
					break;
				}
				case 300: // Total respawn
				{
					// 1st save changes
					m_Spawner.CreaturesName = CreateArray( info, state.Mobile, 100 );
					m_Spawner.SubSpawnerA = CreateArray( info, state.Mobile, 200 );
					m_Spawner.SubSpawnerB = CreateArray( info, state.Mobile, 300 );
					m_Spawner.SubSpawnerC = CreateArray( info, state.Mobile, 400 );
					m_Spawner.SubSpawnerD = CreateArray( info, state.Mobile, 500 );
					m_Spawner.SubSpawnerE = CreateArray( info, state.Mobile, 600 );
					// then respwan
					m_Spawner.Respawn();
					m_Spawner.Running = true;
					break;
				}
				case 400: // Props
				{
					state.Mobile.SendGump( new PropertiesGump( state.Mobile, m_Spawner ) );
					state.Mobile.SendGump( new PremiumSpawnerGump( m_Spawner ) );
					break;
				}
				case 500: // Cancel button
				{
					break;
				}
				case 1000: // Okay
				{
					m_Spawner.CreaturesName = CreateArray( info, state.Mobile, 100 );
					m_Spawner.SubSpawnerA = CreateArray( info, state.Mobile, 200 );
					m_Spawner.SubSpawnerB = CreateArray( info, state.Mobile, 300 );
					m_Spawner.SubSpawnerC = CreateArray( info, state.Mobile, 400 );
					m_Spawner.SubSpawnerD = CreateArray( info, state.Mobile, 500 );
					m_Spawner.SubSpawnerE = CreateArray( info, state.Mobile, 600 );
					m_Spawner.Running = true;
					break;
				}
				default:
				{ //ButtonID: 1-90 spawn; 91-180 remove
					int ID = info.ButtonID;
					
					int Type = 0;
					
					// Spawn creature
					if ( (ID >= 1) && (ID <= 15) )
					{
						Type += 100 + ID;
						m_Spawner.SpawnFromGump( m_Spawner.CreaturesName, m_Spawner.Creatures, m_Spawner.Count, m_Spawner.CreaturesNameCount, GetEntry(Type, info) );
					}
					else if ( (ID >= 16) && (ID <= 30) )
					{
						Type += 200 + ID - 15;
						m_Spawner.SpawnFromGump( m_Spawner.SubSpawnerA, m_Spawner.CreaturesA, m_Spawner.CountA, m_Spawner.CreaturesNameCountA, GetEntry(Type, info) );
					}
					else if ( (ID >= 31) && (ID <= 45) )
					{
						Type += 300 + ID - 30;
						m_Spawner.SpawnFromGump( m_Spawner.SubSpawnerB, m_Spawner.CreaturesB, m_Spawner.CountB, m_Spawner.CreaturesNameCountB, GetEntry(Type, info) );
					}
					else if ( (ID >= 46) && (ID <= 60) )
					{
						Type += 400 + ID - 45;
						m_Spawner.SpawnFromGump( m_Spawner.SubSpawnerC, m_Spawner.CreaturesC, m_Spawner.CountC, m_Spawner.CreaturesNameCountC, GetEntry(Type, info) );
					}
					else if ( (ID >= 61) && (ID <= 75) )
					{
						Type += 500 + ID - 60;
						m_Spawner.SpawnFromGump( m_Spawner.SubSpawnerD, m_Spawner.CreaturesD, m_Spawner.CountD, m_Spawner.CreaturesNameCountD, GetEntry(Type, info) );
					}
					else if ( (ID >= 76) && (ID <= 90) )
					{
						Type += 600 + ID - 75;
						m_Spawner.SpawnFromGump( m_Spawner.SubSpawnerE, m_Spawner.CreaturesE, m_Spawner.CountE, m_Spawner.CreaturesNameCountE, GetEntry(Type, info) );
					}
					// Remove creature
					else if ( (ID >= 91) && (ID <= 105) )
					{
						Type += 100 + ID - 90;
						m_Spawner.RemoveCreaturesFromGump( m_Spawner.Creatures, GetEntry(Type, info) );
					}
					else if ( (ID >= 106) && (ID <= 120) )
					{
						Type += 200 + ID - 105;
						m_Spawner.RemoveCreaturesFromGump( m_Spawner.CreaturesA, GetEntry(Type, info) );
					}
					else if ( (ID >= 121) && (ID <= 135) )
					{
						Type += 300 + ID - 120;
						m_Spawner.RemoveCreaturesFromGump( m_Spawner.CreaturesB, GetEntry(Type, info) );
					}
					else if ( (ID >= 136) && (ID <= 150) )
					{
						Type += 400 + ID - 135;
						m_Spawner.RemoveCreaturesFromGump( m_Spawner.CreaturesC, GetEntry(Type, info) );
					}
					else if ( (ID >= 151) && (ID <= 165) )
					{
						Type += 500 + ID - 150;
						m_Spawner.RemoveCreaturesFromGump( m_Spawner.CreaturesD, GetEntry(Type, info) );
					}
					else if ( (ID >= 166) && (ID <= 180) )
					{
						Type += 600 + ID - 165;
						m_Spawner.RemoveCreaturesFromGump( m_Spawner.CreaturesE, GetEntry(Type, info) );
					}

					string entry = GetEntry(Type, info);
					
					if ( entry != null && entry.Length > 0 )
					{
						m_Spawner.CreaturesName = CreateArray( info, state.Mobile, 100 );
						m_Spawner.SubSpawnerA = CreateArray( info, state.Mobile, 200 );
						m_Spawner.SubSpawnerB = CreateArray( info, state.Mobile, 300 );
						m_Spawner.SubSpawnerC = CreateArray( info, state.Mobile, 400 );
						m_Spawner.SubSpawnerD = CreateArray( info, state.Mobile, 500 );
						m_Spawner.SubSpawnerE = CreateArray( info, state.Mobile, 600 );
						m_Spawner.Running = true;
					}

					break;
				}
			}
		}
	}
}
