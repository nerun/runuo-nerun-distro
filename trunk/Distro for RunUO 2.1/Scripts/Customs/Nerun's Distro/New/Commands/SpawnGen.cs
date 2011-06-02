/////////////////////////
//       By Nerun      //
// Engine r12 (v5.3.7) //
/////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Regions;


namespace Server
{
	public class SpawnGenerator
	{
		private static int m_Count;
		private static int m_MapOverride = -1;
		private static int m_IDOverride = -1;
		private static double m_MinTimeOverride = -1;
		private static double m_MaxTimeOverride = -1;
		private const bool TotalRespawn = true;
		private const int Team = 0;

		public static void Initialize()
		{
			CommandSystem.Register( "SpawnGen", AccessLevel.Administrator, new CommandEventHandler( SpawnGen_OnCommand ) );
		}

		[Usage( "SpawnGen [<filename>]|[unload <id>]|[remove <region>|<rect>]|[save <region>|<rect>]" )]
		[Description( "Generates spawners from Data/Monsters/*.map" )]
		private static void SpawnGen_OnCommand( CommandEventArgs e )
		{
			if ( e.ArgString == null || e.ArgString == "" )
			{
				e.Mobile.SendMessage( "Usage: SpawnGen [<filename>]|[remove <region>|<rect>|<ID>]|[save <region>|<rect>|<ID>]" );
			}
			else if ( e.Arguments[0].ToLower() == "remove" && e.Arguments.Length == 2 )
			{
				Remove( e.Mobile, e.Arguments[1].ToLower() );
			}
			else if ( e.Arguments[0].ToLower() == "remove" && e.Arguments.Length == 5 )
			{
				int x1 = Utility.ToInt32( e.Arguments[1] );
				int y1 = Utility.ToInt32( e.Arguments[2] );
				int x2 = Utility.ToInt32( e.Arguments[3] );
				int y2 = Utility.ToInt32( e.Arguments[4] );
				Remove( e.Mobile, x1, y1, x2, y2 );
			}
			else if ( e.ArgString.ToLower() == "remove" )
			{
				Remove( e.Mobile, ""  );
			}
			else if ( e.Arguments[0].ToLower() == "save" && e.Arguments.Length == 2 )
			{
				Save( e.Mobile, e.Arguments[1].ToLower() );
			}
			else if ( e.Arguments[0].ToLower() == "unload" && e.Arguments.Length == 2 )
			{
				int ID = Utility.ToInt32( e.Arguments[1] );
				Remove( ID );
			}
			else if ( e.Arguments[0].ToLower() == "savebyhand" )
			{
				SaveByHand();
			}
			else if ( e.Arguments[0].ToLower() == "save" && e.Arguments.Length == 5 )
			{
				int x1 = Utility.ToInt32( e.Arguments[1] );
				int y1 = Utility.ToInt32( e.Arguments[2] );
				int x2 = Utility.ToInt32( e.Arguments[3] );
				int y2 = Utility.ToInt32( e.Arguments[4] );
				Save( e.Mobile, x1, y1, x2, y2 );
			}
			else if ( e.ArgString.ToLower() == "save" )
			{
				Save( e.Mobile, "" );
			}
			else
			{
				Parse( e.Mobile, e.ArgString );
			}
		}

		private static void Remove( Mobile from, string region )
		{
			World.Broadcast( 0x35, true, "Spawns are being removed, please wait." );
			DateTime startTime = DateTime.Now;
			int count = 0;
			ArrayList itemsdel = new ArrayList();
			string prefix = Server.Commands.CommandSystem.Prefix;

			if ( region == null || region == "" )
			{
				CommandSystem.Handle( from, String.Format( "{0}Global remove where premiumspawner", prefix ) );
			}
			else
			{
				foreach ( Item itemdel in World.Items.Values )
				{
					Region re = Region.Find(itemdel.Location, itemdel.Map);
					string regname = re.ToString().ToLower();

					if ( itemdel is PremiumSpawner && itemdel.Map == from.Map )
					{
						if (regname == region )
						{
							itemsdel.Add(itemdel);
							count += 1;
						}
					}
				}

				foreach ( Item itemdel2 in itemsdel )
				{
					itemdel2.Delete();
				}

				DateTime endTime = DateTime.Now;
				World.Broadcast( 0x35, true, "{0} spawns have been removed in {1:F1} seconds.", count, (endTime - startTime).TotalSeconds );
			}
		}

		private static void Save( Mobile from, string region )
		{
			World.Broadcast( 0x35, true, "Spawns are being saved, please wait." );
			DateTime startTime = DateTime.Now;
			int count = 0;
			ArrayList itemssave = new ArrayList();
			string mapanome = "spawns";

			foreach ( Item itemsave in World.Items.Values )
			{
				Region re = Region.Find(itemsave.Location, itemsave.Map);
				string regname = re.ToString().ToLower();

				if ( itemsave is PremiumSpawner && ( region == null || region == "" ) )
				{
					itemssave.Add( itemsave );
					count +=1;
				}

				else if ( itemsave is PremiumSpawner && itemsave.Map == from.Map )
				{
					if ( regname == region )
					{
						itemssave.Add( itemsave );
						count += 1;
					}
				}
			}

			GenericSave( itemssave, mapanome, count, startTime );
		}

		private static void Remove( int ID )
		{
			World.Broadcast( 0x35, true, "Spawns are being removed, please wait." );
			DateTime startTime = DateTime.Now;
			int count = 0;
			ArrayList itemsremove = new ArrayList();

			foreach ( Item itemremove in World.Items.Values )
			{ 
				if ( itemremove is PremiumSpawner && ((PremiumSpawner)itemremove).SpawnID == ID )
				{
					itemsremove.Add( itemremove );
					count +=1;
				}
			}

			foreach ( Item itemremove2 in itemsremove )
			{
				itemremove2.Delete();
			}

			DateTime endTime = DateTime.Now;
			World.Broadcast( 0x35, true, "{0} spawns have been removed in {1:F1} seconds.", count, (endTime - startTime).TotalSeconds );
		}

		private static void SaveByHand()
		{
			World.Broadcast( 0x35, true, "Spawns are being saved, please wait." );
			DateTime startTime = DateTime.Now;
			int count = 0;
			ArrayList itemssave = new ArrayList();
			string mapanome = "byhand";

			foreach ( Item itemsave in World.Items.Values )
			{ 
				if ( itemsave is PremiumSpawner && ((PremiumSpawner)itemsave).SpawnID == 1 )
				{
					itemssave.Add( itemsave );
					count +=1;
				}
			}

			GenericSave( itemssave, mapanome, count, startTime );
		}

		private static void Remove( Mobile from, int x1, int y1, int x2, int y2 )
		{
			World.Broadcast( 0x35, true, "Spawns are being removed, please wait." );
			DateTime startTime = DateTime.Now;
			int count = 0;
			ArrayList itemsremove = new ArrayList();

			foreach ( Item itemremove in World.Items.Values )
			{ 
				if ( itemremove is PremiumSpawner && ( ( itemremove.X >= x1 && itemremove.X <= x2 ) && ( itemremove.Y >= y1 && itemremove.Y <= y2 ) && itemremove.Map == from.Map ) )
				{
					itemsremove.Add( itemremove );
					count +=1;
				}
			}

			foreach ( Item itemremove2 in itemsremove )
			{
				itemremove2.Delete();
			}

			DateTime endTime = DateTime.Now;
			World.Broadcast( 0x35, true, "{0} spawns have been removed. The entire process took {1:F1} seconds.", count, (endTime - startTime).TotalSeconds );
		}

		private static void Save( Mobile from, int x1, int y1, int x2, int y2 )
		{
			World.Broadcast( 0x35, true, "Spawns are being saved, please wait." );
			DateTime startTime = DateTime.Now;
			int count = 0;
			ArrayList itemssave = new ArrayList();
			string mapanome = "spawns";

			foreach ( Item itemsave in World.Items.Values )
			{ 
				if ( itemsave is PremiumSpawner && ( ( itemsave.X >= x1 && itemsave.X <= x2 ) && ( itemsave.Y >= y1 && itemsave.Y <= y2 ) && itemsave.Map == from.Map ) )
				{
					itemssave.Add( itemsave );
					count +=1;
				}
			}

			GenericSave( itemssave, mapanome, count, startTime );
		}

		private static void GenericSave( ArrayList colecao, string mapa, int count, DateTime startTime )
		{
			ArrayList itemssave = new ArrayList( colecao );
			string mapanome = mapa;

			if ( !Directory.Exists( "Data/Monsters" ) )
				Directory.CreateDirectory( "Data/Monsters" );

			string escreva = "Data/Monsters/" + mapanome + ".map";

			using ( StreamWriter op = new StreamWriter( escreva ) )
			{
				foreach ( PremiumSpawner itemsave2 in itemssave )
				{
					int mapnumber = 0;
					switch ( itemsave2.Map.ToString() )
					{
						case "Felucca":
							mapnumber = 1;
							break;
						case "Trammel":
							mapnumber = 2;
							break;
						case "Ilshenar":
							mapnumber = 3;
							break;
						case "Malas":
							mapnumber = 4;
							break;
						case "Tokuno":
							mapnumber = 5;
							break;
						case "TerMur":
							mapnumber = 6;
							break;
						default:
							mapnumber = 7;
							Console.WriteLine( "Monster Parser: Warning, unknown map {0}", itemsave2.Map );
							break;
					}

					string timer1a = itemsave2.MinDelay.ToString();
					string[] timer1b = timer1a.Split( ':' );
					int timer1c = ( Utility.ToInt32( timer1b[0] ) * 60 ) + Utility.ToInt32( timer1b[1] );

					string timer2a = itemsave2.MaxDelay.ToString();
					string[] timer2b = timer2a.Split( ':' );
					int timer2c = ( Utility.ToInt32( timer2b[0] ) * 60 ) + Utility.ToInt32( timer2b[1] );
					string towrite = "";
					string towriteA = "";
					string towriteB = "";
					string towriteC = "";
					string towriteD = "";
					string towriteE = "";

					if ( itemsave2.CreaturesName.Count > 0 )
					{
						towrite = itemsave2.CreaturesName[0].ToString();
					}

					if ( itemsave2.SubSpawnerA.Count > 0 )
					{
						towriteA = itemsave2.SubSpawnerA[0].ToString();
					}

					if ( itemsave2.SubSpawnerB.Count > 0 )
					{
						towriteB = itemsave2.SubSpawnerB[0].ToString();
					}

					if ( itemsave2.SubSpawnerC.Count > 0 )
					{
						towriteC = itemsave2.SubSpawnerC[0].ToString();
					}

					if ( itemsave2.SubSpawnerD.Count > 0 )
					{
						towriteD = itemsave2.SubSpawnerD[0].ToString();
					}

					if ( itemsave2.SubSpawnerE.Count > 0 )
					{
						towriteE = itemsave2.SubSpawnerE[0].ToString();
					}

					for ( int i = 1; i < itemsave2.CreaturesName.Count; ++i )
					{
						if ( itemsave2.CreaturesName.Count > 0 )
						{
							towrite = towrite + ":" + itemsave2.CreaturesName[i].ToString();
						}
					}

					for ( int i = 1; i < itemsave2.SubSpawnerA.Count; ++i )
					{
						if ( itemsave2.SubSpawnerA.Count > 0 )
						{
							towriteA = towriteA + ":" + itemsave2.SubSpawnerA[i].ToString();
						}
					}

					for ( int i = 1; i < itemsave2.SubSpawnerB.Count; ++i )
					{
						if ( itemsave2.SubSpawnerB.Count > 0 )
						{
							towriteB = towriteB + ":" + itemsave2.SubSpawnerB[i].ToString();
						}
					}

					for ( int i = 1; i < itemsave2.SubSpawnerC.Count; ++i )
					{
						if ( itemsave2.SubSpawnerC.Count > 0 )
						{
							towriteC = towriteC + ":" + itemsave2.SubSpawnerC[i].ToString();
						}
					}

					for ( int i = 1; i < itemsave2.SubSpawnerD.Count; ++i )
					{
						if ( itemsave2.SubSpawnerD.Count > 0 )
						{
							towriteD = towriteD + ":" + itemsave2.SubSpawnerD[i].ToString();
						}
					}

					for ( int i = 1; i < itemsave2.SubSpawnerE.Count; ++i )
					{
						if ( itemsave2.SubSpawnerE.Count > 0 )
						{
							towriteE = towriteE + ":" + itemsave2.SubSpawnerE[i].ToString();
						}
					}

					op.WriteLine( "*|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}", towrite, towriteA, towriteB, towriteC, towriteD, towriteE, itemsave2.X, itemsave2.Y, itemsave2.Z, mapnumber, timer1c, timer2c, itemsave2.WalkingRange, itemsave2.HomeRange, itemsave2.SpawnID, itemsave2.Count, itemsave2.CountA, itemsave2.CountB, itemsave2.CountC, itemsave2.CountD, itemsave2.CountE );
				}
			}

			DateTime endTime = DateTime.Now;
			World.Broadcast( 0x35, true, "{0} spawns had been saved. The entire process took {1:F1} seconds.", count, (endTime - startTime).TotalSeconds );
		}

		public static void Parse( Mobile from, string filename )
		{
			string monster_path1 = Path.Combine( Core.BaseDirectory, "Data/Monsters" );
			string monster_path = Path.Combine( monster_path1, filename );
			m_Count = 0;

			if ( File.Exists( monster_path ) )
			{
				from.SendMessage( "Spawning {0}...", filename );
				m_MapOverride = -1;
				m_IDOverride = -1;
				m_MinTimeOverride = -1;
				m_MaxTimeOverride = -1;

				using ( StreamReader ip = new StreamReader( monster_path ) )
				{
					string line;

					while ( (line = ip.ReadLine()) != null )
					{
						string[] split = line.Split( '|' );
						string[] splitA = line.Split( ' ' );

						if ( splitA.Length == 2  )
						{
							if ( splitA[0].ToLower() == "overridemap" )
								m_MapOverride = Utility.ToInt32( splitA[1] );
							if ( splitA[0].ToLower() == "overrideid" )
								m_IDOverride = Utility.ToInt32( splitA[1] );
							if ( splitA[0].ToLower() == "overridemintime" )
								m_MinTimeOverride = Utility.ToDouble( splitA[1] );
							if ( splitA[0].ToLower() == "overridemaxtime" )
								m_MaxTimeOverride = Utility.ToDouble( splitA[1] );
						}

						if ( split.Length < 19 )
							continue;

						switch( split[0].ToLower() ) 
						{
							//Comment Line

							case "##":
								break;

							//Place By class

							case "*":
								PlaceNPC( split[2].Split(':'), split[3].Split(':'), split[4].Split(':'), split[5].Split(':'), split[6].Split(':'), split[7], split[8], split[9], split[10], split[11], split[12], split[14], split[13], split[15], split[16], split[17], split[18], split[19], split[20], split[21], split[1].Split(':') );
								break;

							//Place By Type

							case "r":
								PlaceNPC( split[2].Split(':'), split[3].Split(':'), split[4].Split(':'), split[5].Split(':'), split[6].Split(':'), split[7], split[8], split[9], split[10], split[11], split[12], split[14], split[13], split[15], split[16], split[17], split[18], split[19], split[20], split[1], "bloodmoss", "sulfurousash", "spiderssilk", "mandrakeroot", "gravedust", "nightshade", "ginseng", "garlic", "batwing", "pigiron", "noxcrystal", "daemonblood", "blackpearl");
								break;
							}
					}
				}

				m_MapOverride = -1;
				m_IDOverride = -1;
				m_MinTimeOverride = -1;
				m_MaxTimeOverride = -1;

				from.SendMessage( "Done, added {0} spawners",m_Count );
			}
			else
			{
				from.SendMessage( "{0} not found!", monster_path );
			}
		}

		public static void PlaceNPC( string[] fakespawnsA, string[] fakespawnsB, string[] fakespawnsC, string[] fakespawnsD, string[] fakespawnsE, string sx, string sy, string sz, string sm, string smintime, string smaxtime, string swalkingrange, string shomerange, string sspawnid, string snpccount, string sfakecountA, string sfakecountB, string sfakecountC, string sfakecountD, string sfakecountE, params string[] types )
		{
			if ( types.Length == 0 )
				return;

			int x = Utility.ToInt32( sx );
			int y = Utility.ToInt32( sy );
			int z = Utility.ToInt32( sz );
			int map = Utility.ToInt32( sm );
			double dmintime = Utility.ToDouble( smintime );
			if ( m_MinTimeOverride != -1 )
				dmintime = m_MinTimeOverride;
			TimeSpan mintime = TimeSpan.FromMinutes( dmintime );
			double dmaxtime = Utility.ToDouble( smaxtime );

			if ( m_MaxTimeOverride != -1 )
			{
				if ( m_MaxTimeOverride < dmintime )
				{
					dmaxtime = dmintime;
				}
				else
				{
					dmaxtime = m_MaxTimeOverride;
				}
			}

			TimeSpan maxtime = TimeSpan.FromMinutes( dmaxtime );
			int homerange = Utility.ToInt32( shomerange );
		        int walkingrange = Utility.ToInt32( swalkingrange );
			int spawnid = Utility.ToInt32( sspawnid );
			int npccount = Utility.ToInt32( snpccount );
			int fakecountA = Utility.ToInt32( sfakecountA );
			int fakecountB = Utility.ToInt32( sfakecountB );
			int fakecountC = Utility.ToInt32( sfakecountC );
			int fakecountD = Utility.ToInt32( sfakecountD );
			int fakecountE = Utility.ToInt32( sfakecountE );

			if ( m_MapOverride != -1 )
				map = m_MapOverride;

			if ( m_IDOverride != -1 )
				spawnid = m_IDOverride;

			switch ( map )
			{
				case 0://Trammel and Felucca
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Felucca, mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Trammel, mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;
				case 1://Felucca
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Felucca, mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;
				case 2://Trammel
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Trammel, mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;
				case 3://Ilshenar
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Ilshenar, mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;
				case 4://Malas
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Malas, mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;
				case 5://Tokuno
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Maps[4], mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;
				case 6://TerMur
					MakeSpawner( types, fakespawnsA, fakespawnsB, fakespawnsC, fakespawnsD, fakespawnsE, x, y, z, Map.Maps[5], mintime, maxtime, walkingrange, homerange, spawnid, npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE );
					break;

				default:
					Console.WriteLine( "Spawn Parser: Warning, unknown map {0}", map );
					break;
			}
		}

		private static void MakeSpawner( string[] types, string[] fakespawnsA, string[] fakespawnsB, string[] fakespawnsC, string[] fakespawnsD, string[] fakespawnsE, int x, int y, int z, Map map, TimeSpan mintime, TimeSpan maxtime, int walkingrange, int homerange, int spawnid, int npccount, int fakecountA, int fakecountB, int fakecountC, int fakecountD, int fakecountE )
		{
			if ( types.Length == 0 )
				return;

			List<string> tipos = new List<string>( types );
			List<string> noneA = new List<string>();
			List<string> noneB = new List<string>();
			List<string> noneC = new List<string>();
			List<string> noneD = new List<string>();
			List<string> noneE = new List<string>();

			if ( fakespawnsA[0] != "" )
				noneA = new List<string>( fakespawnsA );

			if ( fakespawnsB[0] != "" )
				noneB = new List<string>( fakespawnsB );

			if ( fakespawnsC[0] != "" )
				noneC = new List<string>( fakespawnsC );

			if ( fakespawnsD[0] != "" )
				noneD = new List<string>( fakespawnsD );

			if ( fakespawnsE[0] != "" )
				noneE = new List<string>( fakespawnsE );

			PremiumSpawner spawner = new PremiumSpawner( npccount, fakecountA, fakecountB, fakecountC, fakecountD, fakecountE, spawnid, mintime, maxtime, Team, walkingrange, homerange, tipos, noneA, noneB, noneC, noneD, noneE );

			spawner.MoveToWorld( new Point3D( x, y, z ), map );

			if ( TotalRespawn )
			{
				spawner.Respawn();

				if ( ((PremiumSpawner)spawner).SpawnID == 132 ) // if is ChampionSpawn
				{
					spawner.BringToHome();
				}
			}
			
			m_Count++;
		}
	}
}