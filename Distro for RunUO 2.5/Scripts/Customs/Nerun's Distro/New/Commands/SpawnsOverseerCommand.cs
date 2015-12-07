// Engine r117
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Commands
{
	public class GenOverseer
	{
		public static void Initialize()
		{
			CommandSystem.Register( "GenSeers", AccessLevel.Administrator, new CommandEventHandler( GenOverseer_OnCommand ) );
			CommandSystem.Register( "GenOverseers", AccessLevel.Administrator, new CommandEventHandler( GenOverseer_OnCommand ) );
			CommandSystem.Register( "GenSeer", AccessLevel.Administrator, new CommandEventHandler( GenOverseer_OnCommand ) );
			CommandSystem.Register( "GenOverseer", AccessLevel.Administrator, new CommandEventHandler( GenOverseer_OnCommand ) );
		}

		[Usage( "GenSeers" )]
		[Aliases( "GenSeer, GenOverseer and GenOverseers" )]
		[Description( "Generates Spawns' Overseers around the world." )]
		private static void GenOverseer_OnCommand( CommandEventArgs e )
		{
			m_Mobile = e.Mobile;
			m_Count = 0;

			m_Mobile.SendMessage( "Generating Spawns' Overseers, please wait." );

			Generate( "Data/Nerun's Distro/Spawns/overseers/trammel", Map.Trammel );
			Generate( "Data/Nerun's Distro/Spawns/overseers/felucca", Map.Felucca );
			Generate( "Data/Nerun's Distro/Spawns/overseers/ilshenar", Map.Ilshenar );
			Generate( "Data/Nerun's Distro/Spawns/overseers/malas", Map.Malas );
			Generate( "Data/Nerun's Distro/Spawns/overseers/tokuno", Map.Tokuno );
			Generate( "Data/Nerun's Distro/Spawns/overseers/termur", Map.TerMur );
			
			m_Mobile.SendMessage( "Spawns' Overseers generation complete. {0} seers were generated.", m_Count );
		}

		public static void Generate( string folder, params Map[] maps )
		{
			if ( !Directory.Exists( folder ) )
				return;

			string[] files = Directory.GetFiles( folder, "*.cfg" );

			for ( int i = 0; i < files.Length; ++i )
			{
				ArrayList list = DecorationListSeers.ReadAll( files[i] );

				for ( int j = 0; j < list.Count; ++j )
					m_Count += ((DecorationListSeers)list[j]).Generate( maps );
			}
		}

		private static Mobile m_Mobile;
		private static int m_Count;
	}

	public class DecorationListSeers
	{
		private Type m_Type;
		private int m_ItemID;
		private string[] m_Params;
		private ArrayList m_Entries;

		public DecorationListSeers()
		{
		}

		private static Type typeofStatic = typeof( Static );
		private static Type typeofLocalizedStatic = typeof( LocalizedStatic );

		public Item Construct()
		{
			Item item;

			try
			{
				if ( m_Type == typeofStatic )
				{
					item = new Static( m_ItemID );
				}
				else if ( m_Type == typeofLocalizedStatic )
				{
					int labelNumber = 0;

					for ( int i = 0; i < m_Params.Length; ++i )
					{
						if ( m_Params[i].StartsWith( "LabelNumber" ) )
						{
							int indexOf = m_Params[i].IndexOf( '=' );

							if ( indexOf >= 0 )
							{
								labelNumber = Utility.ToInt32( m_Params[i].Substring( ++indexOf ) );
								break;
							}
						}
					}

					item = new LocalizedStatic( m_ItemID, labelNumber );
				}
				else
				{
					item = (Item)Activator.CreateInstance( m_Type );
				}
			}
			catch ( Exception e )
			{
				throw new Exception( String.Format( "Bad type: {0}", m_Type ), e );
			}

			if ( item is Server.Items.SpawnsOverseer )
			{
				Server.Items.SpawnsOverseer sp = (Server.Items.SpawnsOverseer)item;

				for ( int i = 0; i < m_Params.Length; ++i )
				{
					if ( m_Params[i].StartsWith( "Range" ) )
					{
						int indexOf = m_Params[i].IndexOf( '=' );

						if ( indexOf >= 0 )
							sp.Range = Utility.ToInt32( m_Params[i].Substring( ++indexOf ) );
					}
					else if ( m_Params[i].StartsWith( "InRangeDelay" ) )
					{
						int indexOf = m_Params[i].IndexOf( '=' );

						if ( indexOf >= 0 )
							sp.InRangeDelay = Utility.ToInt32( m_Params[i].Substring( ++indexOf ) );
					}
					else if ( m_Params[i].StartsWith( "OutRangeDelay" ) )
					{
						int indexOf = m_Params[i].IndexOf( '=' );

						if ( indexOf >= 0 )
							sp.OutRangeDelay = Utility.ToInt32( m_Params[i].Substring( ++indexOf ) );
					}
				}
			}

			item.Movable = false;

			for ( int i = 0; i < m_Params.Length; ++i )
			{
				if ( m_Params[i].StartsWith( "Light" ) )
				{
					int indexOf = m_Params[i].IndexOf( '=' );

					if ( indexOf >= 0 )
						item.Light = (LightType)Enum.Parse( typeof( LightType ), m_Params[i].Substring( ++indexOf ), true );
				}
				else if ( m_Params[i].StartsWith( "Hue" ) )
				{
					int indexOf = m_Params[i].IndexOf( '=' );

					if ( indexOf >= 0 )
					{
						int hue = Utility.ToInt32( m_Params[i].Substring( ++indexOf ) );

						if ( item is DyeTub )
							((DyeTub)item).DyedHue = hue;
						else
							item.Hue = hue;
					}
				}
				else if ( m_Params[i].StartsWith( "Name" ) )
				{
					int indexOf = m_Params[i].IndexOf( '=' );

					if ( indexOf >= 0 )
						item.Name = m_Params[i].Substring( ++indexOf );
				}
				else if ( m_Params[i].StartsWith( "Amount" ) )
				{
					int indexOf = m_Params[i].IndexOf( '=' );

					if ( indexOf >= 0 )
					{
						// Must supress stackable warnings

						bool wasStackable = item.Stackable;

						item.Stackable = true;
						item.Amount = Utility.ToInt32( m_Params[i].Substring( ++indexOf ) );
						item.Stackable = wasStackable;
					}
				}
			}

			return item;
		}

		private static Queue m_DeleteQueue = new Queue();

		private static bool FindItem( int x, int y, int z, Map map, Item srcItem )
		{
			int itemID = srcItem.ItemID;

			bool res = false;

			IPooledEnumerable eable;

			if ( (TileData.ItemTable[itemID & TileData.MaxItemValue].Flags & TileFlag.LightSource) != 0 )
			{
				eable = map.GetItemsInRange( new Point3D( x, y, z ), 0 );

				LightType lt = srcItem.Light;
				string srcName = srcItem.ItemData.Name;

				foreach ( Item item in eable )
				{
					if ( item.Z == z )
					{
						if ( item.ItemID == itemID )
						{
							if ( item.Light != lt )
								m_DeleteQueue.Enqueue( item );
							else
								res = true;
						}
						else if ( (item.ItemData.Flags & TileFlag.LightSource) != 0 && item.ItemData.Name == srcName )
						{
							m_DeleteQueue.Enqueue( item );
						}
					}
				}
			}
			else
			{
				eable = map.GetItemsInRange( new Point3D( x, y, z ), 0 );

				foreach ( Item item in eable )
				{
					if ( item.Z == z && item.ItemID == itemID )
					{
						eable.Free();
						return true;
					}
				}
			}

			eable.Free();

			while ( m_DeleteQueue.Count > 0 )
				((Item)m_DeleteQueue.Dequeue()).Delete();

			return res;
		}

		public int Generate( Map[] maps )
		{
			int count = 0;

			Item item = null;

			for ( int i = 0; i < m_Entries.Count; ++i )
			{
				DecorationEntrySeers entry = (DecorationEntrySeers)m_Entries[i];
				Point3D loc = entry.Location;
				string extra = entry.Extra;

				for ( int j = 0; j < maps.Length; ++j )
				{
					if ( item == null )
						item = Construct();

					if ( item == null )
						continue;

					if ( FindItem( loc.X, loc.Y, loc.Z, maps[j], item ) )
					{
					}
					else
					{
						item.MoveToWorld( loc, maps[j] );
						++count;

						item = null;
					}
				}
			}

			if ( item != null )
				item.Delete();

			return count;
		}

		public static ArrayList ReadAll( string path )
		{
			using ( StreamReader ip = new StreamReader( path ) )
			{
				ArrayList list = new ArrayList();

				for ( DecorationListSeers v = Read( ip ); v != null; v = Read( ip ) )
					list.Add( v );

				return list;
			}
		}

		private static string[] m_EmptyParams = new string[0];

		public static DecorationListSeers Read( StreamReader ip )
		{
			string line;

			while ( (line = ip.ReadLine()) != null )
			{
				line = line.Trim();

				if ( line.Length > 0 && !line.StartsWith( "#" ) )
					break;
			}

			if ( string.IsNullOrEmpty( line ) )
				return null;

			DecorationListSeers list = new DecorationListSeers();

			int indexOf = line.IndexOf( ' ' );

			list.m_Type = ScriptCompiler.FindTypeByName( line.Substring( 0, indexOf++ ), true );

			if ( list.m_Type == null )
				throw new ArgumentException( String.Format( "Type not found for header: '{0}'", line ) );

			line = line.Substring( indexOf );
			indexOf = line.IndexOf( '(' );
			if ( indexOf >= 0 )
			{
				list.m_ItemID = Utility.ToInt32( line.Substring( 0, indexOf - 1 ) );

				string parms = line.Substring( ++indexOf );

				if ( line.EndsWith( ")" ) )
					parms = parms.Substring( 0, parms.Length - 1 );

				list.m_Params = parms.Split( ';' );

				for ( int i = 0; i < list.m_Params.Length; ++i )
					list.m_Params[i] = list.m_Params[i].Trim();
			}
			else
			{
				list.m_ItemID = Utility.ToInt32( line );
				list.m_Params = m_EmptyParams;
			}

			list.m_Entries = new ArrayList();

			while ( (line = ip.ReadLine()) != null )
			{
				line = line.Trim();

				if ( line.Length == 0 )
					break;

				if ( line.StartsWith( "#" ) )
					continue;

				list.m_Entries.Add( new DecorationEntrySeers( line ) );
			}

			return list;
		}
	}

	public class DecorationEntrySeers
	{
		private Point3D m_Location;
		private string m_Extra;

		public Point3D Location{ get{ return m_Location; } }
		public string Extra{ get{ return m_Extra; } }

		public DecorationEntrySeers( string line )
		{
			string x, y, z;

			Pop( out x, ref line );
			Pop( out y, ref line );
			Pop( out z, ref line );

			m_Location = new Point3D( Utility.ToInt32( x ), Utility.ToInt32( y ), Utility.ToInt32( z ) );
			m_Extra = line;
		}

		public void Pop( out string v, ref string line )
		{
			int space = line.IndexOf( ' ' );

			if ( space >= 0 )
			{
				v = line.Substring( 0, space++ );
				line = line.Substring( space );
			}
			else
			{
				v = line;
				line = "";
			}
		}
	}

	public class RemOverseer
	{
		public static void Initialize()
		{
			CommandSystem.Register( "RemOverseers", AccessLevel.Administrator, new CommandEventHandler( RemOverseers_OnCommand ) );
			CommandSystem.Register( "RemSeers", AccessLevel.Administrator, new CommandEventHandler( RemOverseers_OnCommand ) );
			CommandSystem.Register( "RemOverseer", AccessLevel.Administrator, new CommandEventHandler( RemOverseers_OnCommand ) );
			CommandSystem.Register( "RemSeer", AccessLevel.Administrator, new CommandEventHandler( RemOverseers_OnCommand ) );
		}

		[Usage( "RemSeers" )]
		[Aliases( "RemSeer, RemOverseer, RemOverseers" )]
		[Description( "Remove all Overseers in all facets." )]
		public static void RemOverseers_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			World.Broadcast( 0x35, true, "Overseers are being removed, please wait." );
			DateTime startTime = DateTime.Now;
			int count = 0;
			List<Item> itemsremove = new List<Item>();

			foreach ( Item itemremove in World.Items.Values )
			{ 
				if ( itemremove is SpawnsOverseer && itemremove.Parent == null )
				{
					itemsremove.Add( itemremove );
					count +=1;
				}
			}

			foreach ( Item itemremove2 in itemsremove )
			{
				itemremove2.Delete();
			}
			
			foreach ( Item premiums in World.Items.Values )
			{ 
				if ( premiums is PremiumSpawner && premiums.Parent == null && ((PremiumSpawner)premiums).Running == false )
				{
					((PremiumSpawner)premiums).Running = true;
					((PremiumSpawner)premiums).NextSpawn = TimeSpan.FromSeconds( 1 );
				}
			}

			DateTime endTime = DateTime.Now;
			World.Broadcast( 0x35, true, "{0} Overseers has been removed in {1:F1} seconds.", count, (endTime - startTime).TotalSeconds );
		}
	}
}
