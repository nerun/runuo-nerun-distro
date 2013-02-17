// Engine r121
using System;
using System.Collections.Generic;
using System.IO;
using Server;
using Server.Items;

namespace Server.Commands
{
	public class SignPut
	{
		private static int m_AddCount;
		private static int m_DelCount;

		private class SignEntry
		{
			public string m_Text;
			public Point3D m_Location;
			public int m_ItemID;
			public int m_Map;

			public SignEntry( string text, Point3D pt, int itemID, int mapLoc )
			{
				m_Text = text;
				m_Location = pt;
				m_ItemID = itemID;
				m_Map = mapLoc;
			}
		}

		public static void Initialize()
		{
			CommandSystem.Register( "SignPut", AccessLevel.Administrator, new CommandEventHandler( SignPut_OnCommand ) );
		}

		[Usage( "SignPut" )]
		[Description( "Places shop signs as specified in a config file." )]
		public static void SignPut_OnCommand( CommandEventArgs c )
		{
			//[SignPut
			if ( c.ArgString.Length == 0 )
			{
				Parse( c.Mobile, "", false ); // Data/signs.cfg
			}
			//[SignPut SE (or ML, KR1, KR2, SA, HS1, HS2)
			else if ( Lib.IsValidExpansion( c.Arguments[0] ) == true  )
			{
				Parse( c.Mobile, c.Arguments[0], false );
			}
			//wrong use
			else
			{
				c.Mobile.SendMessage( "Usage: 'SignPut' or 'SignPut SE' (or ML, KR1, KR2, SA, HS1, HS2)" );
			}
		}
		
		public static void Parse( Mobile from, string filename, bool AddOrDel )
		{
			string ThisFile;
			
			if ( filename == null || filename == "" )
			{
				ThisFile = "Signs.cfg";
			}
			else
			{
				ThisFile = "Signs" + filename.ToUpper() + ".cfg";
			}
			
			List<string> line = new List<string>( Lib.ListOfLines( "Data/Nerun's Distro/Signs", ThisFile ) );

			if ( line.Count > 1 ) // File Exists
			{
				List<SignEntry> list = new List<SignEntry>();

				if ( AddOrDel == true )
				{
					from.SendMessage( "Removing signs, please wait." );
				}
				else
				{
					from.SendMessage( "Generating signs, please wait." );
				}

				for ( int i = 0; i < line.Count; ++i )
				{
					string lineA = Convert.ToString( line[i] );
					
					if ( !lineA.StartsWith("#") && Lib.IsNumber( Convert.ToString( lineA[0] ) ) && lineA != null && lineA != "" && lineA != " " ) // If not comment or blank Line
					{
						string[] split = lineA.Split( ' ' );

						SignEntry e = new SignEntry( lineA.Substring( split[0].Length + 1 + split[1].Length + 1 + split[2].Length + 1 + split[3].Length + 1 + split[4].Length + 1 ), new Point3D( Utility.ToInt32( split[2] ), Utility.ToInt32( split[3] ), Utility.ToInt32( split[4] ) ), Utility.ToInt32( split[1] ), Utility.ToInt32( split[0] ) );

						list.Add( e );
					}
				}

				Map[] brit = new Map[]{ Map.Felucca, Map.Trammel };
				Map[] fel = new Map[]{ Map.Felucca };
				Map[] tram = new Map[]{ Map.Trammel };
				Map[] ilsh = new Map[]{ Map.Ilshenar };
				Map[] malas = new Map[]{ Map.Malas };
				Map[] tokuno = new Map[]{ Map.Tokuno };
				
				for ( int i = 0; i < list.Count; ++i )
				{
					SignEntry e = list[i];
					Map[] maps = null;

					switch ( e.m_Map )
					{
						case 0: maps = brit; break; // Trammel and Felucca
						case 1: maps = fel; break;  // Felucca
						case 2: maps = tram; break; // Trammel
						case 3: maps = ilsh; break; // Ilshenar
						case 4: maps = malas; break; // Malas
						case 5: maps = tokuno; break; // Tokuno Islands
					}

					for ( int j = 0; maps != null && j < maps.Length; ++j )
					{
						if ( AddOrDel == true )
						{
							Del_Static( e.m_ItemID, e.m_Location, maps[j] );
						}
						else
						{
							Add_Static( e.m_ItemID, e.m_Location, maps[j], e.m_Text );
							m_AddCount++;
						}
					}
				}

				if ( AddOrDel == true )
				{
					from.SendMessage( "{0} signs removed.", m_DelCount );
				}
				else
				{
					from.SendMessage( "{0} signs created.", m_AddCount  );
				}

				m_AddCount = 0;
				m_DelCount = 0;
			}
			else
			{
				from.SendMessage( "{0} not found!", line[0] ); // line[0] = path + file name
			}
		}

		private static Queue<Item> m_ToDelete = new Queue<Item>();

		public static void Add_Static( int itemID, Point3D location, Map map, string name )
		{
			Del_Static( itemID, location, map );

			Item sign;

			if ( name.StartsWith( "#" ) )
			{
				sign = new LocalizedSign( itemID, Utility.ToInt32( name.Substring( 1 ) ) );
			}
			else
			{
				sign = new Sign( itemID );
				sign.Name = name;
			}

			if ( map == Map.Malas )
			{
				if ( location.X >= 965 && location.Y >= 502 && location.X <= 1012 && location.Y <= 537 )
					sign.Hue = 0x47E;
				else if ( location.X >= 1960 && location.Y >= 1278 && location.X < 2106 && location.Y < 1413 )
					sign.Hue = 0x44E;
			}

			sign.MoveToWorld( location, map );
		}

		public static void Del_Static( int itemID, Point3D location, Map map )
		{
			IPooledEnumerable eable = map.GetItemsInRange( location, 0 );
			
			foreach ( Item item in eable )
			{
				if ( item is Sign && item.Z == location.Z && item.ItemID == itemID )
				{
					m_ToDelete.Enqueue( item );
					m_DelCount++;
				}
			}

			eable.Free();

			while ( m_ToDelete.Count > 0 )
				m_ToDelete.Dequeue().Delete();
		}
	}
	
	public class SignDel
	{
		public SignDel()
		{
		}

		public static void Initialize() 
		{ 
			CommandSystem.Register( "SignDel", AccessLevel.Administrator, new CommandEventHandler( SignDel_OnCommand ) ); 
		} 

		[Usage( "[SignDel" )]
		[Description( "Removes shop signs as specified in a config file." )]
		private static void SignDel_OnCommand( CommandEventArgs c )
		{
			//[SignDel
			if ( c.ArgString.Length == 0 )
			{
				SignPut.Parse( c.Mobile, "", true ); // Data/signs.cfg
			}
			//[SignDel SE (or ML, KR1, KR2, SA, HS1, HS2)
			else if ( Lib.IsValidExpansion( c.Arguments[0] ) == true  )
			{
				SignPut.Parse( c.Mobile, c.Arguments[0], true );
			}
			//wrong use
			else
			{
				c.Mobile.SendMessage( "Usage: 'SignDel' or 'SignDel SE' (or ML, KR1, KR2, SA, HS1, HS2)" );
			}
		}
	}
}