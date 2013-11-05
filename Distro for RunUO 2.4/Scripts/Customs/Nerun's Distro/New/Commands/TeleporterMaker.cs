// Engine r129
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.Commands
{
	public class TeleporterMaker
	{
		private static int m_Count;

		public TeleporterMaker()
		{
		}
		
		public static void Initialize()
		{
			CommandSystem.Register( "TelMake", AccessLevel.Administrator, new CommandEventHandler( TeleporterMaker_OnCommand ) );
		}

		[Usage( "TelMake" )]
		[Description( "Generates world/dungeon teleporters for all facets." )]
		public static void TeleporterMaker_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendMessage( "Generating teleporters, please wait." );
			
			//[TelMake SE (or ML, KR1, KR2, SA, HS1, HS2)
			if ( Lib.IsValidExpansion( e.Arguments[0] ) == true  )
			{
				string args = e.Arguments[0].ToUpper();
				string tele = "Teleporters" + args + ".cfg";
				Generate( tele, e.Mobile );
			}
			//wrong use
			else
			{
				e.Mobile.SendMessage( "Usage: 'TelMake SE' (or ML, KR1, KR2, SA, HS1, HS2)" );
			}
		}
		
		private static Queue m_Queue = new Queue();
		
		private static bool FindTeleporter( Map map, Point3D p )
		{
			IPooledEnumerable eable = map.GetItemsInRange( p, 0 );

			foreach ( Item item in eable )
			{
				if ( item is Teleporter && !(item is KeywordTeleporter) && !(item is SkillTeleporter) )
				{
					int delta = item.Z - p.Z;

					if ( delta >= -12 && delta <= 12 )
						m_Queue.Enqueue( item );
				}
			}

			eable.Free();

			while ( m_Queue.Count > 0 )
				((Item)m_Queue.Dequeue()).Delete();

			return false;
		}

		public static void Generate( string filename, Mobile from )
		{
			m_Count = 0; // Need to restart the counter each time this command is used
			
			List<string> line = new List<string>( Lib.ListOfLines( "Data/Nerun's Distro/Teleporters", filename ) );
			
			if ( line.Count > 1 ) // File Exists
			{
				for ( int i = 0; i < line.Count; ++i )
				{
					string lineA = Convert.ToString( line[i] );

					if ( lineA != "" && lineA.Contains("|") && Lib.IsNumber( Convert.ToString( lineA[0] ) ) )
					{
						string[] split = lineA.Split( '|' );

						if ( split.Length == 9 )
						{
							PlaceTeleporter( Convert.ToString( split[0] ), Convert.ToString( split[1] ), Convert.ToString( split[2] ), Convert.ToString( split[3] ), Convert.ToString( split[4] ), Convert.ToString( split[5] ), Convert.ToString( split[6] ), Convert.ToString( split[7] ), Convert.ToString( split[8][0] ), "do" );
						}
						
						if ( split.Length == 10 && Convert.ToString( split[9] ) == "remove" )
						{
							PlaceTeleporter( Convert.ToString( split[0] ), Convert.ToString( split[1] ), Convert.ToString( split[2] ), Convert.ToString( split[3] ), Convert.ToString( split[4] ), Convert.ToString( split[5] ), Convert.ToString( split[6] ), Convert.ToString( split[7] ), Convert.ToString( split[8][0] ), "undo" );
						}
					}
				}
				
				line.Clear();
				
				from.SendMessage( "Teleporter generating complete. {0} teleporters were generated.", m_Count );
			}
			else
			{
				from.SendMessage( "{0} not found!", line[0] ); // line[0] = path + file name
			}
		}

		public static void PlaceTeleporter( string xLoc, string yLoc, string zLoc, string xDest, string yDest, string zDest, string mapLoc, string mapDest, string back, string DoOrNot )
		{
			// IDENTIFY OPERATION

			Point3D pointLocation = new Point3D( Utility.ToInt32( xLoc ), Utility.ToInt32( yLoc ), Utility.ToInt32( zLoc ) );
			Point3D pointDestination = new Point3D( Utility.ToInt32( xDest ), Utility.ToInt32( yDest ), Utility.ToInt32( zDest ) );
			
			/*	MAP OF CHARACTERS
				0 = Felucca + Trammel
				1 = Felucca
				2 = Trammel
				3 = Ilshenar
				4 = Malas
				5 = Tokuno
				6 = Ter Mur	*/
			
			int mLoc = Utility.ToInt32( mapLoc );
			int mDest = Utility.ToInt32( mapDest );
			
			bool BackOrNot;

			if ( back.ToLower() == "t" )
				BackOrNot = true;
			else
				BackOrNot = false;
			
			bool FelTram;
			
			Map mapLocation;
			
			if ( mLoc != 0 )
			{
				FelTram = false;
				mapLocation = Map.Maps[mLoc-1];
			}
			else
			{
				FelTram = true;
				mapLocation = Map.Maps[0]; //Felucca in Scripts\Misc\MapDefinitions.cs
			}
			
			Map mapDestination;
			
			if ( mDest != 0 )
			{
				FelTram = false;
				mapDestination = Map.Maps[mDest-1];
			}
			else
			{
				FelTram = true;
				mapDestination = Map.Maps[1]; //Trammel in Scripts\Misc\MapDefinitions.cs
			}
			
			// PLACE TELEPORTERS OPERATION
			
			if ( !FelTram ) // Not Felucca + Trammel
			{
				if ( !BackOrNot ) // Don't put way back
				{
					Make( mapLocation, mapDestination, pointLocation, pointDestination, DoOrNot );
				}
				else
				{
					Make( mapLocation, mapDestination, pointLocation, pointDestination, DoOrNot ); // Go
					Make( mapDestination, mapLocation, pointDestination, pointLocation, DoOrNot ); // Back
				}
			}
			else // Felucca + Trammel
			{
				if ( !BackOrNot ) // Don't put way back
				{
					Make( mapDestination, mapDestination, pointLocation, pointDestination, DoOrNot ); // Trammel
					Make( mapLocation, mapLocation, pointLocation, pointDestination, DoOrNot ); // Felucca
				}
				else
				{
					// Trammel
					Make( mapDestination, mapDestination, pointLocation, pointDestination, DoOrNot ); // Go
					Make( mapDestination, mapDestination, pointDestination, pointLocation, DoOrNot ); // Back
					// Felucca
					Make( mapLocation, mapLocation, pointLocation, pointDestination, DoOrNot ); // Go
					Make( mapLocation, mapLocation, pointDestination, pointLocation, DoOrNot ); // Back
				}
			}
		}

		public static void Make( Map mapA, Map mapB, Point3D ptA, Point3D ptB, string DoOrNot )
		{
			if ( !FindTeleporter( mapA, ptA ) ) // remove old teleporters in that spot first
			{
				if ( DoOrNot == "do" ) // then create new ones if not marked as "undo only" (...|remove termination)
				{
					Teleporter tel = new Teleporter( ptB, mapB );
					tel.MoveToWorld( ptA, mapA );
					m_Count++;
				}
			}
		}
	}
}