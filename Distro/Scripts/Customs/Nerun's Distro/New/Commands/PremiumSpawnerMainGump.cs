// Engine r29
#define RunUo2_0
using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class PremiumSpawnerMainGump : Gump
    {
        Mobile caller;

        public static void Initialize()
        {
#if(RunUo2_0)
            CommandSystem.Register("PremiumSpawner", AccessLevel.Administrator, new CommandEventHandler(PremiumSpawner_OnCommand));
			CommandSystem.Register("Spawner", AccessLevel.Administrator, new CommandEventHandler(PremiumSpawner_OnCommand));
#else
            Register("PremiumSpawner", AccessLevel.Administrator, new CommandEventHandler(PremiumSpawner_OnCommand));
            Register("Spawner", AccessLevel.Administrator, new CommandEventHandler(PremiumSpawner_OnCommand));
#endif
        }

        [Usage("PremiumSpawner")]
		[Aliases( "Spawner" )]
        [Description("PremiumSpawner main gump.")]
        public static void PremiumSpawner_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (from.HasGump(typeof(PremiumSpawnerMainGump)))
                from.CloseGump(typeof(PremiumSpawnerMainGump));
            from.SendGump(new PremiumSpawnerMainGump(from));
        }

        public PremiumSpawnerMainGump(Mobile from) : this()
        {
            caller = from;
        }
		
		public void AddBlackAlpha( int x, int y, int width, int height )
		{
			AddImageTiled( x, y, width, height, 2624 );
			AddAlphaRegion( x, y, width, height );
		}

        public PremiumSpawnerMainGump() : base( 0, 0 )
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			//PAGE 1
			AddPage(1);
			AddBackground(93, 68, 256, 423, 9200);
			AddHtml( 98, 75, 244, 44, "       PREMIUM SPAWNER<BR>" + "by Nerun                 Rev.151", (bool)true, (bool)false);
			AddBlackAlpha(100, 124, 241, 71);
			AddLabel(109, 126, 52, @"WORLD CREATION");
			AddLabel(126, 148, 52, @"Let there be light (Create World)");
			AddLabel(126, 170, 52, @"Apocalypse now (Clear All Facets)");
			AddButton(109, 151, 1210, 1209, 101, GumpButtonType.Reply, 0);
			AddButton(109, 173, 1210, 1209, 102, GumpButtonType.Reply, 0);
			AddBlackAlpha(100, 200, 241, 89);
			AddLabel(109, 202, 52, @"SELECT SPAWNS BY EXPANSION");
			AddLabel(126, 224, 52, @"UO Classic spawns (pre-T2A)");
			AddLabel(126, 244, 52, @"UO Mondain's Legacy spawns");
			AddLabel(126, 264, 52, @"UO KR, SA and HS spawns");
			//AddLabel(238, 224, 52, @"UO:ML spawns");
			//AddLabel(238, 244, 52, @"UO:KR, SA and HS spawns");
			//AddLabel(238, 264, 52, @"teste");
			AddButton(109, 227, 1210, 1209, 103, GumpButtonType.Reply, 0);
			AddButton(109, 247, 1210, 1209, 104, GumpButtonType.Reply, 0);
			AddButton(109, 267, 1210, 1209, 105, GumpButtonType.Reply, 0);
			//AddButton(221, 227, 1210, 1209, 106, GumpButtonType.Reply, 0);
			//AddButton(221, 247, 1210, 1209, 107, GumpButtonType.Reply, 0);
			//AddButton(221, 267, 1210, 1209, 108, GumpButtonType.Reply, 0);
			AddBlackAlpha(100, 294, 241, 89);
			AddLabel(109, 296, 52, @"REMOVE SPAWNS BY EXPANSION");
			AddLabel(126, 318, 52, @"UO Classic spawns (pre-T2A)");
			AddLabel(126, 338, 52, @"UO Mondain's Legacy spawns");
			AddLabel(126, 358, 52, @"UO KR, SA and HS spawns");
			//AddLabel(238, 318, 52, @"Ter Mur");
			//AddLabel(238, 338, 52, @"Tokuno");
			//AddLabel(238, 358, 52, @"Trammel");
			AddButton(109, 321, 1210, 1209, 109, GumpButtonType.Reply, 0);
			AddButton(109, 341, 1210, 1209, 110, GumpButtonType.Reply, 0);
			AddButton(109, 361, 1210, 1209, 111, GumpButtonType.Reply, 0);
			//AddButton(221, 321, 1210, 1209, 112, GumpButtonType.Reply, 0);
			//AddButton(221, 341, 1210, 1209, 113, GumpButtonType.Reply, 0);
			//AddButton(221, 361, 1210, 1209, 114, GumpButtonType.Reply, 0);
			AddBlackAlpha(100, 388, 241, 68);
			AddLabel(109, 391, 52, @"SMART PLAYER RANGE SENSITIVE");
			AddLabel(126, 413, 52, @"Generate Spawns' Overseer");
			AddLabel(126, 432, 52, @"Remove Spawns' Overseer");
			AddButton(109, 416, 1210, 1209, 115, GumpButtonType.Reply, 0);
			AddButton(109, 435, 1210, 1209, 116, GumpButtonType.Reply, 0);
			//Page change
			AddLabel(207, 463, 200, @"1/3");
			AddButton(235, 465, 5601, 5605, 0, GumpButtonType.Page, 2); //advance
			
			// PAGE 2
			AddPage(2);
			AddBackground(93, 68, 256, 423, 9200);
			AddHtml( 98, 75, 244, 44, "       PREMIUM SPAWNER<BR>" + "by Nerun                 Rev.151", (bool)true, (bool)false);
			AddBlackAlpha(100, 124, 241, 114);
			AddLabel(109, 126, 52, @"SAVE SPAWNERS");
			AddLabel(126, 148, 52, @"All spawns (spawns.map)");
			AddLabel(126, 170, 52, @"'By hand' spawns (byhand.map)");
			AddLabel(126, 192, 52, @"Spawns inside region (region.map)");
			AddLabel(126, 214, 52, @"Spawns inside coordinates");
			AddButton(109, 151, 1210, 1209, 117, GumpButtonType.Reply, 0);
			AddButton(109, 173, 1210, 1209, 118, GumpButtonType.Reply, 0);
			AddButton(109, 195, 1210, 1209, 119, GumpButtonType.Reply, 0);
			AddButton(109, 217, 1210, 1209, 120, GumpButtonType.Reply, 0);
			AddBlackAlpha(100, 244, 241, 134);
			AddLabel(109, 246, 52, @"REMOVE SPAWNERS");
			AddLabel(126, 268, 52, @"All spawners in ALL facets");
			AddLabel(126, 290, 52, @"All spawners in THIS facet");
			AddLabel(126, 312, 52, @"Remove spawners by SpawnID");
			AddLabel(126, 334, 52, @"Remove inside coordinates");
			AddLabel(126, 355, 52, @"Remove spawners inside region");
			AddButton(109, 271, 1210, 1209, 121, GumpButtonType.Reply, 0);
			AddButton(109, 293, 1210, 1209, 122, GumpButtonType.Reply, 0);
			AddButton(109, 315, 1210, 1209, 123, GumpButtonType.Reply, 0);
			AddButton(109, 337, 1210, 1209, 124, GumpButtonType.Reply, 0);
			AddButton(109, 358, 1210, 1209, 125, GumpButtonType.Reply, 0);
			AddBlackAlpha(100, 385, 241, 71);
			AddLabel(109, 387, 52, @"EDITOR");
			AddLabel(126, 408, 52, @"Spawn Editor (edit, find and list");
			AddLabel(126, 427, 52, @"all PremiumSpawners in the world)");
			AddButton(109, 411, 1210, 1209, 126, GumpButtonType.Reply, 0);
			//Page change
			AddLabel(207, 463, 200, @"2/3");
			AddButton(189, 465, 5603, 5607, 0, GumpButtonType.Page, 1); //back
			AddButton(235, 465, 5601, 5605, 0, GumpButtonType.Page, 3); //advance
			
			//PAGE 3
			AddPage(3);
			AddBackground(93, 68, 256, 423, 9200);
			AddHtml( 98, 75, 244, 44, "       PREMIUM SPAWNER<BR>" + "by Nerun                 Rev.151", (bool)true, (bool)false);
			AddBlackAlpha(101, 124, 241, 47);
			AddLabel(109, 126, 52, @"CONVERSION UTILITY");
			AddLabel(127, 148, 52, @"RunUO Spawners to Premium");
			AddButton(110, 151, 1210, 1209, 127, GumpButtonType.Reply, 0);
			AddBlackAlpha(101, 177, 241, 134);
			AddLabel(109, 179, 52, @"CUSTOM REGIONS IN A BOX");
			AddLabel(127, 201, 52, @"Add a Region Controler");
			AddLabel(127, 222, 52, @"(double-click the Region");
			AddLabel(127, 243, 52, @"Controller to configure it region.");
			AddLabel(127, 264, 52, @"Every Controller control one");
			AddLabel(127, 286, 52, @"region. Don't forget to prop)");
			AddButton(110, 204, 1210, 1209, 128, GumpButtonType.Reply, 0);
			//Page change
			AddLabel(207, 463, 200, @"3/3");
			AddButton(189, 465, 5603, 5607, 0, GumpButtonType.Page, 2); //back
        }
		
		public static void DoThis( Mobile from, string command)
		{
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( from, String.Format( "{0}{1}", prefix, command ) );
			CommandSystem.Handle( from, String.Format( "{0}spawner", prefix ) );
		}

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch(info.ButtonID)
            {
                case 0:
				{
					//Quit
					break;
				}
				case 101:
				{
					DoThis( from, "createworld" );
					break;
				}
				case 102:
				{
					DoThis( from, "clearall" );
					break;
				}
				case 103:
				{
					from.Say( "SPAWNING UO Classic..." );
					DoThis( from, "spawngen uoclassic/UOClassic.map" );
					break;
				}
				case 104:
				{
					DoThis( from, "SpawnUOML" );
					break;
				}
				case 105:
				{
					DoThis( from, "SpawnCurrent" );
					break;
				}
					//DoThis( from106, "" );
					//DoThis( from107, "" );
					//DoThis( from108, "" );
				case 109:
				{
					DoThis( from, "spawngen unload 1000" );
					break;
				}
				case 110:
				{
					DoThis( from, "UnloadUOML" );
					break;
				}
				case 111:
				{
					DoThis( from, "UnloadCurrent" );
					break;
				}
					//DoThis( from112, "" );
					//DoThis( from113, "" );
					//DoThis( from114, "" );
				case 115:
				{
					DoThis( from, "GenSeers" );
					break;
				}
				case 116:
				{
					DoThis( from, "RemSeers" );
					break;
				}
				case 117:
				{
					DoThis( from, "spawngen save" );
					break;
				}
				case 118:
				{
					DoThis( from, "spawngen savebyhand" );
					break;
				}
				case 119:
				{
					DoThis( from, "GumpSaveRegion" );
					break;
				}
				case 120:
				{
					DoThis( from, "GumpSaveCoordinate" );
					break;
				}
				case 121:
				{
					DoThis( from, "spawngen remove" );
					break;
				}
				case 122:
				{
					DoThis( from, "spawngen cleanfacet" );
					break;
				}
				case 123:
				{
					DoThis( from, "GumpRemoveID" );
					break;
				}
				case 124:
				{
					DoThis( from, "GumpRemoveCoordinate" );
					break;
				}
				case 125:
				{
					DoThis( from, "GumpRemoveRegion" );
					break;
				}
				case 126:
				{
					DoThis( from, "SpawnEditor" );
					break;
				}
				case 127:
				{
					DoThis( from, "RunUOSpawnerExporter" );
					break;
				}
				case 128:
				{
					DoThis( from, "Add RegionControl" );
					break;
				}
            }
        }
    }
}
