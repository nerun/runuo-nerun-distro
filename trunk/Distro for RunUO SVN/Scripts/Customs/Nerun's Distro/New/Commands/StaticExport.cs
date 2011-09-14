/*************************
 * StaticExport by Nerun *
 *      Version 2.3      *
 *************************
*/
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Commands;
using Server.Targeting;

namespace Server.Commands
{
	public class StaticExport
	{
		public static void Initialize()
		{
			CommandSystem.Register( "StaticExport" , AccessLevel.Administrator, new CommandEventHandler( StaticExport_OnCommand ) );
			CommandSystem.Register( "StaEx" , AccessLevel.Administrator, new CommandEventHandler( StaticExport_OnCommand ) );
		}

		[Usage( "staticexport [string filename]" )]
		[Aliases( "staex" )]
		[Description( "Convert Statics in a cfg decoration file." )]
		public static void StaticExport_OnCommand( CommandEventArgs e )
		{
			if ( e.Arguments.Length == 5 )
			{
				string file = e.Arguments[0];
				int x1 = Utility.ToInt32( e.Arguments[1] );
				int y1 = Utility.ToInt32( e.Arguments[2] );
				int x2 = Utility.ToInt32( e.Arguments[3] );
				int y2 = Utility.ToInt32( e.Arguments[4] );
				Export( e.Mobile, file, x1, y1, x2, y2 );
			}
			else 
			{
				if ( e.Arguments.Length == 1 )
				{
					string file = e.Arguments[0];
					BeginStaEx( e.Mobile, file );
				}
				else				
				{
					e.Mobile.SendMessage( "Usage: StaEx filename" );
				}
			}
		}

		public static void BeginStaEx( Mobile from, string file )
		{
	    		BoundingBoxPicker.Begin( from, new BoundingBoxCallback( StaExBox_Callback ), new object[]{ file } );
		}

		private static void StaExBox_Callback( Mobile from, Map map, Point3D start, Point3D end, object state )
		{
			object[] states = (object[])state;
			string file = (string)states[0];

			Export( from, file, start.X, start.Y, end.X, end.Y );
		}

		private static void Export( Mobile from, string file, int X1, int Y1, int X2, int Y2 )
		{
				int x1 = X1;
				int y1 = Y1;
				int x2 = X2;
				int y2 = Y2;

				if(X1 > X2)
				{
					x1 = X2;
					x2 = X1;
				}

				if(Y1 < Y2)
				{
					y1 = Y2;
					y2 = Y1;
				}

			Map map = from.Map;
			List<Item> list = new List<Item>();
			Dictionary<string, List<string>> DicOfItemIDs = new Dictionary<string, List<string>>();
			List<Item> Signs = new List<Item>();

			if ( !Directory.Exists( @".\Export\" ) )
				Directory.CreateDirectory( @".\Export\" );

			using ( StreamWriter op = new StreamWriter( String.Format( @".\Export\{0}.cfg", file ) ) )
			{

				from.SendMessage( "Saving Statics..." );

				op.WriteLine( "# Saved By Static Exporter" );
				op.WriteLine( "#  StaticExport by Nerun" );
				op.WriteLine( "#       Version 2.3" );
				op.WriteLine( "" );

				foreach ( Item item in World.Items.Values )
				{
					if ( item.Decays == false && item.Movable == false && item.Parent == null && ( ( item.X >= x1 && item.X <= x2 ) && ( item.Y <= y1 && item.Y >= y2 ) && item.Map == map ) )
					{
						list.Add( item );
					}
				}

				foreach ( Item item in list )
				{
					Map MapDestFinal = item.Map;
				
					if (item is PublicMoongate)
					{
						op.WriteLine( "PublicMoongate 3948" );
						op.WriteLine( "{0} {1} {2}", item.X, item.Y, item.Z );
						op.WriteLine( "" );
					}
					else if (item is Moongate)
					{
						op.WriteLine( "Moongate 3948 (Target={0}; TargetMap={1}; Hue={2})", ((Moongate)item).Target, ((Moongate)item).TargetMap, ((Moongate)item).Hue );
						op.WriteLine( "{0} {1} {2}", item.X, item.Y, item.Z );
						op.WriteLine( "" );
					}
					else if (item is Teleporter)
					{
						if ( ((Teleporter)item).MapDest != null )
						{
							MapDestFinal = ((Teleporter)item).MapDest;
						}
						else
						{
							MapDestFinal = ((Teleporter)item).Map;
						}
						op.WriteLine( "Teleporter 7107 (PointDest={0}; MapDestination={1})", ((Teleporter)item).PointDest, MapDestFinal );
						op.WriteLine( "{0} {1} {2}", item.X, item.Y, item.Z );
						op.WriteLine( "" );
					}
					else if (item is KeywordTeleporter)
					{
						if ( ((KeywordTeleporter)item).MapDest != null )
						{
							MapDestFinal = ((KeywordTeleporter)item).MapDest;
						}
						else
						{
							MapDestFinal = ((KeywordTeleporter)item).Map;
						}
						op.WriteLine( "KeywordTeleporter 7107 (PointDest={0}; MapDestination={1}; Range={2}; Substring={3})", ((KeywordTeleporter)item).PointDest, MapDestFinal, ((KeywordTeleporter)item).Range, ((KeywordTeleporter)item).Substring );
						op.WriteLine( "{0} {1} {2}", item.X, item.Y, item.Z );
						op.WriteLine( "" );
					}
					else if (item is Spawner)
					{
						op.WriteLine( "Spawner 0x1F13 (Spawn={0}; Count={1}; HomeRange={2}; WalkingRange={3})", ((Spawner)item).SpawnNames[0], ((Spawner)item).Count, ((Spawner)item).HomeRange, ((Spawner)item).WalkingRange );
						op.WriteLine( "{0} {1} {2}", item.X, item.Y, item.Z );
						op.WriteLine( "" );
					}
					else
					{
						/* How would Jack the Ripper said: divided into parts!
						 *
						 * 1.- Some items are statics other are not. So lets get the "real" item name, that is
						 *     something like: 0x40098345 "LampPost1". The real hame has hex number plus a
						 *     name inside quotes. So lets remove the 12 first caracters fromk the beginning
						 *     (the hex number plus the space and the first quote before the name). Now we just
						 *     need to remove the last caracter (the last quote) to get our "real name".
						 *     The end is just: LampPost1, without quotes.
						 */

						string itemname = item.ToString();
						itemname = itemname.Remove(0, 12); //remove 12 caracters from the beginning of the string
						itemname = itemname.Remove(itemname.Length - 1); // remove the last caracter (the ")

						/*
						 * 2.- Some items are Addons. Addons are collections of static items, called
						 *     AddonComponent. We don't need the AddonComponent, but just the Addon item.
						 *     The "real name" of the Addon will have an ItemID equal to 1. But this is not good
						 *     for us, because if the configuration file has, example:
						 *
						 *		StoneFountainAddon 1
						 *		1437 1678 10
						 *     
						 *     The [decorate command will place the Addon in the right place, but will forget
						 *     one static item of that collection: the one at coordinates 1437 1678 10.
						 *
						 *     So lets change the ItemID from 1 to 0, that will generate the Addon correctly.
						 */

						int itemid = item.ItemID;

						if ( itemid == 1 )
						{
							itemid = 0; // if it is Addon "main", change the ID to 0, because 1 forget one static
						}

						string itemidhex = itemid.ToString("X");

						/*
						 * 3.- Now lets begin the work. Decoration files starts with an item name plus a number
						 *     (ItemID) in the same line. In the line bellow, there are all events of that
						 *     exactly item translated in coordinates. We will simplify, creating a string that
						 *     sums item "real name" and ItemID. Plus a string that sums all the coordinates
						 *     X, Y and Z in one line string.
						 */

						string HexConstruct = " 0x";

						if ( itemidhex.Length == 1 )
						{
							HexConstruct = " 0x000";
						}
						else if ( itemidhex.Length == 2 )
						{
							HexConstruct = " 0x00";
						}
						else if ( itemidhex.Length == 3 )
						{
							HexConstruct = " 0x0";
						}

						string NamePlusID = itemname + HexConstruct + itemidhex;
						string Coord = (item.X).ToString() + " " + (item.Y).ToString() + " " + (item.Z).ToString();

						/*
						 * 4.- Lets customize the NamePlusID and Coord strings, because some decorations has
						 *     custom Names and Hues (custom properties).
						 */

						if ( item.Name == null && item.Hue != 0 )
						{
							NamePlusID = NamePlusID + " (Hue=" + item.Hue + ")";
						}

						else if ( item.Hue == 0 && item.Name != null )
						{
							NamePlusID = NamePlusID + " (Name=" + item.Name + ")";
						}

						else if ( item.Name != null && item.Hue != 0 )
						{
							NamePlusID = NamePlusID + " (Name=" + item.Name + "; Hue=" + item.Hue + ")";
						}

						else if ( item is BaseBeverage && item.Hue == 0 && item.Name == null)
						{
							string sContent = " (Content=" + ((BaseBeverage)item).Content + ")";
							NamePlusID = NamePlusID + sContent;
						}
						else if ( item is LocalizedSign )
						{
							NamePlusID = "LocalizedSign " + HexConstruct + itemidhex + " (LabelNumber=" + ((LocalizedSign)item).LabelNumber + ")";
						}
						else if ( itemname == "Cannon" )
						{
							NamePlusID = "Cannon 0";
						}

						/*
						 * 5.- Now, the main job. Above, we created a Dictionary, that holds collections of
						 *     Keys and values. Each item in a decoration file has a Key (NamePlusID string)
						 *     plus a lot of Values (all the coordinates where that item appear). Because of
						 *     it we create a Dictionary of String plus ArrayList as value!
						 *     The code bellow will add the NamePlusID (the key) to the Dictionary if that
						 *     key was not added yet. If was, it will add the new coordinates to the
						 *     ArrayList of values, and update the key with more one coordinate!
						 */

						if (DicOfItemIDs.ContainsKey(NamePlusID))
						{
							List<string> CoordXYZupdated = DicOfItemIDs[NamePlusID];
							CoordXYZupdated.Add(Coord);
							DicOfItemIDs.Remove(NamePlusID);
							DicOfItemIDs.Add(NamePlusID, CoordXYZupdated);
						}
						else
						{
							if ( !itemname.Contains("Component") && itemname != "InternalItem" && itemname != "PremiumSpawner" )
							{ // ignore AddonComponent, CannonComponent, InternalItem and PremiumSpawner
								List<string> CoordXYZ = new List<string>();
								CoordXYZ.Add(Coord);
								DicOfItemIDs.Add(NamePlusID, CoordXYZ);
							}
						}
					}
				}

				/*
				 * 6.- Final job. Lets analyze the Dictionary and write id up in
				 *     the configuration file.
				 */

				foreach ( KeyValuePair<string, List<string>> pair in DicOfItemIDs )
				{
					op.WriteLine("{0}", pair.Key);

					foreach (string ElementInArray in DicOfItemIDs[pair.Key])
					{
						op.WriteLine(ElementInArray);
					}

					op.WriteLine( "" );
				}
				
				from.SendMessage( String.Format( "You exported {0} Statics from this facet.", list.Count ) );
			}
		}
	}
}