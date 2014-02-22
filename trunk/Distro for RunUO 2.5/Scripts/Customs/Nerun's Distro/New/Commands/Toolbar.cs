/**************************************
*Script Name: Toolbar                 *
*Author: Joeku AKA Demortris          *
*For use with RunUO 2.0               *
*Client Tested with: 5.0.7.1          *
*Version: 1.3                         *
*Initial Release: 08/23/06            *
*Revision Date: 01/22/07              *
***************************************
*Changed by Nerun                     *
*For use with RunUO 2.1 to 2.5        *
*Client Tested with: from 7.0.8.2 up  *
*                    to 7.0.33.1      *
*Last update: Nerun's Distro r137     *
**************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Accounting; 
using Server.Commands;
using Server.Gumps;
using Server.Network;

namespace Joeku
{
	public class ToolbarHelper
	{
		public static int Version = 130;						// Version identification
		public static string ReleaseDate = "January 22, 2007";	// release date =D
		public static ToolbarInfos Infos = null;				// DO NOT CHANGE THIS! Used for the persistance item...

		public static void Initialize()
		{
			if( Infos == null )
				Infos = new ToolbarInfos();

			CommandHandlers.Register("Toolbar", AccessLevel.Counselor, new CommandEventHandler(Toolbar_OnCommand));
			EventSink.Login += new LoginEventHandler(OnLogin);
			// Talow and AlphaDragon fix 1/3
			// http://www.runuo.com/community/threads/joeku-toolbar-after-gm-death.477771/#post-3722174
			EventSink.PlayerDeath += new PlayerDeathEventHandler(OnPlayerDeath);
		}

		/// <summary>
        /// Sends a toolbar to staff members upon death.
        /// </summary>
		// Talow and AlphaDragon fix 2/3
        public static void OnPlayerDeath(PlayerDeathEventArgs e)
        {
            if (e.Mobile.AccessLevel >= AccessLevel.Counselor)
            {
                e.Mobile.CloseGump(typeof(Toolbar));
                object[] arg = new object[] {e.Mobile};
                Timer.DelayCall( TimeSpan.FromSeconds( 2.0 ), new TimerStateCallback( SendToolbar ), arg);
            }
        }
		
		/// <summary>
		/// Sends a toolbar to staff members upon login.
		/// </summary>
		private static void OnLogin(LoginEventArgs e)
		{
			if (e.Mobile.AccessLevel >= AccessLevel.Counselor)
			{
				e.Mobile.CloseGump(typeof(Toolbar));
				SendToolbar(e.Mobile);
			}
		}
		
		[Usage("Toolbar")]
		public static void Toolbar_OnCommand(CommandEventArgs e)
		{
			e.Mobile.CloseGump(typeof(Toolbar));
			SendToolbar(e.Mobile);
		}

		/// <summary>
		/// Sends a toolbar to the mobile
		/// </summary>
		public static void SendToolbar(Mobile mob)
		{
			ToolbarInfo info;
			ReadInfo(mob, out info);

			mob.SendGump(new Toolbar(info));
		}

		// Talow and AlphaDragon fix 3/3
        public static void SendToolbar(object state)
        {
            object[] states = (object[])state;

            Mobile m = (Mobile)states[0];
            SendToolbar(m);
        }

		/// <summary>
		/// Reads the information in the persistance item and exports it into a new ToolbarInfo class.
		/// </summary>
		public static void ReadInfo(Mobile mob, out ToolbarInfo info)
		{
			EnsureMaxed( mob );
			Account acc = mob.Account as Account;
			info = null;
			for(int i = 0; i < ToolbarHelper.Infos.Info.Count; i++)
			{
				if(ToolbarHelper.Infos.Info[i].Account == acc.Username)
					info = ToolbarHelper.Infos.Info[i];
			}
			if(info == null)
				info = ToolbarInfo.CreateNew(mob, acc);
		}

		public static void EnsureMaxed( Mobile mob )
		{
			//AccessLevel level = (AccessLevel)ToolbarInfo.GetAccess( mob.Account as Account );

			if( mob.AccessLevel > ((Account)mob.Account).AccessLevel )
			{
				mob.Account.AccessLevel = mob.AccessLevel;
			//else if( mob.AccessLevel < acc.AccessLevel )
				//mob.AccessLevel = level;
				Console.WriteLine("***TOOLBAR*** Account {0}, Mobile {1}: AccessLevel resolved to {2}.", ((Account)mob.Account).Username, mob, mob.AccessLevel );
			}
		}
	}

	public class ToolbarInfos : Item
	{
		private List<ToolbarInfo> p_ToolbarInfo = new List<ToolbarInfo>();
		public List<ToolbarInfo> Info{ get{ return p_ToolbarInfo; } set{ p_ToolbarInfo = value; }}

		public ToolbarInfos(){}

		public ToolbarInfos(Serial serial) : base( serial ){}

		public override void Delete(){ return; }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) ToolbarHelper.Version);

			writer.Write((int) Info.Count);

			for(int i = 0; i < Info.Count; i++)
			{
				ToolbarInfo t = Info[i] as ToolbarInfo;

			// Version 1.3
				writer.Write((int)t.Font );
				writer.Write((bool)t.Phantom );
				writer.Write((bool)t.Stealth );
				writer.Write((bool)t.Reverse );
				writer.Write((bool)t.Lock );

			// Version 1.0
				writer.Write((string) t.Account);

				writer.Write((int) t.Dimensions.Count);
				for(int j = 0; j < t.Dimensions.Count; j++)
					writer.Write((int) t.Dimensions[j]);

				writer.Write((int) t.Entries.Count);
				for(int k = 0; k < t.Entries.Count; k++)
					writer.Write((string) t.Entries[k]);

				writer.Write((int) t.Skin);

				writer.Write((int) t.Points.Count);
				for(int l = 0; l < t.Points.Count; l++)
					writer.Write((Point3D) t.Points[l]);
			}
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			ToolbarHelper.Infos = this;

			int count = reader.ReadInt();

		// Version 1.3
			int font = 0;
			bool phantom = true, stealth = false, reverse = false, locked = true; 

		// Version 1.0
			string account;
			List<int> dimensions;
			List<string> entries;
			int subcount, skin;
			List<Point3D> points;
			for(int i = 0; i < count; i++)
			{	
				switch(version)
				{
					case 130:
					{
						font = reader.ReadInt();
						phantom = reader.ReadBool();
						stealth = reader.ReadBool();
						reverse = reader.ReadBool();
						locked = reader.ReadBool();
						goto case 100;
					}
					default:
					case 100:
					{
						account = reader.ReadString();

						dimensions = new List<int>();

						subcount = reader.ReadInt();
						for(int j = 0; j < subcount; j++)
							dimensions.Add(reader.ReadInt());

						entries = new List<string>();

						subcount = reader.ReadInt();
						for(int k = 0; k < subcount; k++)
							entries.Add(reader.ReadString());

						skin = reader.ReadInt();

						points = new List<Point3D>();

						subcount = reader.ReadInt();
						for(int l = 0; l < subcount; l++)
							points.Add(reader.ReadPoint3D());

						break;
					}
				}
				
				ToolbarInfo info = new ToolbarInfo(account, dimensions, entries, skin, points, font, new bool[]{ phantom, stealth, reverse, locked });
			}
		}
	}

	public class ToolbarInfo
	{
	// Version 1.3
		private int p_Font;
		public int Font{ get{ return p_Font; } set{ p_Font = value; }}
		private bool p_Phantom;
		public bool Phantom{ get{ return p_Phantom; } set{ p_Phantom = value; }}
		private bool p_Stealth;
		public bool Stealth{ get{ return p_Stealth; } set{ p_Stealth = value; }}
		private bool p_Reverse;
		public bool Reverse{ get{ return p_Reverse; } set{ p_Reverse = value; }}
		private bool p_Lock;
		public bool Lock{ get{ return p_Lock; } set{ p_Lock = value; }}

	// Version 1.0
		private string p_Account;
		public string Account{ get{ return p_Account; } set{ p_Account = value; }}

		private List<int> p_Dimensions;	
		public List<int> Dimensions{ get{ return p_Dimensions; } set{ p_Dimensions = value; }}

		private List<string> p_Entries;
		public List<string> Entries{ get{ return p_Entries; } set{ p_Entries = value; }}

		private int p_Skin;
		public int Skin{ get{ return p_Skin; } set{ p_Skin= value; }}

		private List<Point3D> p_Points;	// Marker
		public List<Point3D> Points{ get{ return p_Points; } set{ p_Points = value; }}
		
		public ToolbarInfo(string account, List<int> dimensions, List<string> entries, int skin, List<Point3D> points, int font, bool[] switches )
		{
			p_Dimensions = new List<int>();
			p_Entries = new List<string>();
			p_Points = new List<Point3D>();

			SetAttributes(account, dimensions, entries, skin, points, font, switches);
			ToolbarHelper.Infos.Info.Add(this);
		}

		/// <summary>
		/// Sets the attributes of a ToolbarInfo.
		/// </summary>
		public void SetAttributes(string account, List<int> dimensions, List<string> entries, int skin, List<Point3D> points, int font, bool[] switches )
		{
		// Version 1.3
			p_Font = font;
			p_Phantom = switches[0];
			p_Stealth = switches[1];
			p_Reverse = switches[2];
			p_Lock = switches[3];

		// Version 1.0
			p_Account = account;
			p_Dimensions = dimensions;
			p_Entries = entries;
			p_Skin = skin;

			p_Points = points;
		}

		/// <summary>
		/// Creates a new ToolbarInfo...
		/// </summary>
		public static ToolbarInfo CreateNew(Mobile mob, Account acc)
		{
			string account = acc.Username;
			int access = (int)mob.AccessLevel;
			List<int> dimensions = DefaultDimensions( access );
			List<string> entries = DefaultEntries( access );
			int skin = 0;
			List<Point3D> points = new List<Point3D>();
			
			for( int i = entries.Count; i <= 135; i++ )
				entries.Add("(?) to edit");

			return new ToolbarInfo(account, dimensions, entries, skin, points, 0, new bool[]{ true, false, false, true } );
		}
		
		/// <summary>
		/// Gets the highest accesslevel of a character on an account.
		/// </summary>
		/*public static int GetAccess( Account acc )
		{
			int access = 0;
			for( int i = 0; i < 6; i++ )
			{
				if( ((Mobile)acc[i]) == null )
					break;
				if( (int)((Mobile)acc[i]).AccessLevel > access )
					access = (int)((Mobile)acc[i]).AccessLevel;
			}
			return access;
		}*/

		/// <summary>
		/// Calculates the default command entries based on one's AccessLevel.
		/// </summary>
		public static List<string> DefaultEntries( int i )
		{
			List<string> entries = new List<string>();
			switch(i)
			{
				case 0: // Player
					break;
				case 1: // Counselor
					entries.Add("GMBody"); entries.Add("StaffRunebook"); entries.Add("Stuck"); entries.Add("M Tele"); for( int j = 0; j < 5; j++ ){entries.Add("-");}
					entries.Add("Where"); entries.Add("Who"); entries.Add("Self Hide"); entries.Add("Self Unhide");
					break;
				case 2: // GameMaster
					entries.Add("GMBody"); entries.Add("StaffRunebook"); entries.Add("M Tele"); entries.Add("Where"); entries.Add("Who"); entries.Add("Self Hide"); entries.Add("Self Unhide"); entries.Add("Recover"); for( int j = 0; j < 1; j++ ){entries.Add("-");}
					entries.Add("SpawnEditor"); entries.Add("Move"); entries.Add("M Remove"); entries.Add("Wipe"); entries.Add("Props"); entries.Add("Get Location"); entries.Add("Get ItemID"); entries.Add("AddStairs"); for( int j = 0; j < 1; j++ ){entries.Add("-");}
					entries.Add("AddDoor"); entries.Add("ViewEquip"); entries.Add("Skills"); entries.Add("Kick"); entries.Add("Kill");
					break;
				case 3: // Seer
					goto case 2;
				case 4: // Administrator
					entries.Add("Admin"); entries.Add("GMBody"); entries.Add("StaffRunebook"); entries.Add("StaEx MyDeco"); entries.Add("M Tele"); entries.Add("Where"); entries.Add("Who"); entries.Add("Self Hide"); for( int j = 0; j < 1; j++ ){entries.Add("-");}
					entries.Add("PremiumSpawner"); entries.Add("SpawnEditor"); entries.Add("Move"); entries.Add("M Remove"); entries.Add("Wipe"); entries.Add("Props"); entries.Add("Recover"); entries.Add("Self Unhide"); for( int j = 0; j < 1; j++ ){entries.Add("-");}
					entries.Add("AddonGen"); entries.Add("Get Location"); entries.Add("Get ItemID"); entries.Add("AddDoor"); entries.Add("AddStairs");
					break;
				case 5: // Developer
					goto case 4;
				case 6: // Owner
					goto case 4;
			}
			return entries;
		}

		public static List<int> DefaultDimensions( int i )
		{
			List<int> dimensions = new List<int>();
			switch(i)
			{
				case 0: // Player
					dimensions.Add(0); dimensions.Add(0);
					break;
				case 1: // Counselor
					dimensions.Add(4); dimensions.Add(2);
					break;
				case 2: // GameMaster
					dimensions.Add(7); dimensions.Add(2);
					break;
				case 3: // Seer
					goto case 2;
				case 4: // Administrator
					dimensions.Add(8); dimensions.Add(3);
					break;
				case 5: // Developer
					goto case 4;
				case 6: // Owner
					goto case 4;
			}

			return dimensions;
		}
	}

	public class Toolbar : Gump
	{
		/*******************
		*	BUTTON ID'S
		* 0 - Close
		* 1 - Edit
		*******************/

		private ToolbarInfo p_Info;

	// Version 1.3
		private int p_Font;
		public int Font{ get{ return p_Font; } set{ p_Font = value; } }
		private bool p_Phantom, p_Stealth, p_Reverse, p_Lock;
		public bool Phantom{ get{ return p_Phantom; } set{ p_Phantom = value; } }
		public bool Stealth{ get{ return p_Stealth; } set{ p_Stealth = value; } }
		public bool Reverse{ get{ return p_Reverse; } set{ p_Reverse = value; } }
		public bool Lock{ get{ return p_Lock; } set{ p_Lock = value; } }

	// Version 1.0
		private List<string> p_Commands;
		private int p_Skin, p_Columns, p_Rows;

		public List<string> Commands{ get{ return p_Commands; } set{ p_Commands = value; } }
		public int Skin{ get{ return p_Skin; } set{ p_Skin = value; } }
		public int Columns{ get{ return p_Columns; } set{ p_Columns = value; } }
		public int Rows{ get{ return p_Rows; } set{ p_Rows = value; } }

		public int InitOptsW, InitOptsH;

		public Toolbar(ToolbarInfo info) : base(0, 28)
		{
			p_Info = info;

		// Version 1.3
			p_Font = info.Font;
			p_Phantom = info.Phantom;
			p_Stealth = info.Stealth;
			p_Reverse = info.Reverse;
			p_Lock = info.Lock;

		// Version 1.0
			p_Commands = info.Entries;
			p_Skin = info.Skin;
			p_Columns = info.Dimensions[0];
			p_Rows = info.Dimensions[1];

//original
//            if( Lock )
//                Closable = false;
//AlphaDragon's mod:
            if( Lock )
            {
                Closable = false;
                Disposable = false;
            }
//I moded end so that will remain even when editing house

			int offset = GumpIDs.Misc[(int)GumpIDs.MiscIDs.ButtonOffset].Content[p_Skin,0];
			int bx = ((offset * 2) + (Columns * 110)), by = ((offset * 2) + (Rows * 24)), byx = by, cy = 0;

			SetCoords( offset );

			if( Reverse )
			{
				cy = InitOptsH;
				by = 0;
			}

			AddPage( 0 );
			AddPage( 1 );
			if( Stealth )
			{
				AddMinimized( by, offset );
				AddPage( 2 );
			}

			AddInitOpts( by, offset );

			AddBackground(0, cy, bx, byx, GumpIDs.Misc[(int)GumpIDs.MiscIDs.Background].Content[p_Skin,0]); 

			string font = GumpIDs.Fonts[Font];
			if( Phantom )
				font += "<BASEFONT COLOR=#FFFFFF>";

			int temp = 0, x = 0, y = 0;
			for(int i = 0; i < Columns*Rows; i++)
			{
				x = offset + ((i % Columns) * 110);
				y = offset + (int)(Math.Floor((double)(i / Columns)) * 24) + cy;
				AddButton(x + 1, y, 2445, 2445, temp + 10, GumpButtonType.Reply, 0);
				AddBackground(x, y, 110, 24, GumpIDs.Misc[(int)GumpIDs.MiscIDs.Buttonground].Content[p_Skin,0]);
				
				if( Phantom )
				{
					AddImageTiled( x + 2, y + 2, 106, 20, 2624 ); // Alpha Area 1_1
					AddAlphaRegion( x + 2, y + 2, 106, 20 ); // Alpha Area 1_2
				}

				AddHtml( x + 5, y + 3, 100, 20, String.Format( "<center>{0}{1}", font, Commands[temp]), false, false );
				//AddLabelCropped(x + 5, y + 3, 100, 20, GumpIDs.Misc[(int)GumpIDs.MiscIDs.Color].Content[p_Skin,0], Commands[temp]); 

				if( i%Columns == Columns-1 )
					temp += 9-Columns;
				temp++;
			}

			/*TEST---
			0%5 == 0
			1%5 == 1
			2%5 == 2
			3%5 == 3
			4%5 == 4
			5%5 == 0
			END TEST---*/

			if( !Stealth )
			{
				AddPage(2);
				AddMinimized( by, offset );
			}
		}
 
		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile mob = sender.Mobile;
			string prefix = Server.Commands.CommandSystem.Prefix;

			switch(info.ButtonID)
			{
				default:	// Command
					mob.SendGump(this);
					mob.SendMessage( Commands[info.ButtonID - 10]);
					CommandSystem.Handle( mob, String.Format( "{0}{1}", prefix, Commands[info.ButtonID - 10] ) );
					break;
				case 0:		// Close
					break;
				case 1:		// Edit
					mob.SendGump(this);
					mob.CloseGump( typeof( ToolbarEdit ) );
					mob.SendGump(new ToolbarEdit( p_Info ));
					break;
			}
		}

		/// <summary>
		/// Sets coordinates and sizes.
		/// </summary>
		public void SetCoords( int offset )
		{
			InitOptsW = 50 + (offset * 2) + GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Minimize].Content[p_Skin,2] + 5 + GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Customize].Content[p_Skin,2];
			InitOptsH = (offset * 2) + GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Minimize].Content[p_Skin,3];
			if(GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Customize].Content[p_Skin,3] + (offset * 2) > InitOptsH)
				InitOptsH = GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Customize].Content[p_Skin,3] + (offset * 2);
		}

		/// <summary>
		/// Adds initial options.
		/// </summary>
		public void AddInitOpts( int y, int offset )
		{
			AddBackground(0, y, InitOptsW, InitOptsH, GumpIDs.Misc[(int)GumpIDs.MiscIDs.Background].Content[p_Skin,0]); 
			AddButton(offset, y + offset, GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Minimize].Content[p_Skin,0], GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Minimize].Content[p_Skin,1], 0, GumpButtonType.Page, Stealth ? 1 : 2); 
			AddButton(offset + GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Minimize].Content[p_Skin,2] + 5, y + offset, GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Customize].Content[p_Skin,0], GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Customize].Content[p_Skin,1], 1, GumpButtonType.Reply, 0);	// 1 Edit
		}
		
		/// <summary>
		/// Adds minimized page.
		/// </summary>
		public void AddMinimized( int y, int offset )
		{
			AddBackground(0, y, InitOptsW, InitOptsH, GumpIDs.Misc[(int)GumpIDs.MiscIDs.Background].Content[p_Skin,0]); 
			AddButton(offset, y + offset, GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Maximize].Content[p_Skin,0], GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Maximize].Content[p_Skin,1], 0, GumpButtonType.Page, Stealth ? 2 : 1); 
			AddButton(offset + GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Minimize].Content[p_Skin,2] + 5, y + offset, GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Customize].Content[p_Skin,0], GumpIDs.Buttons[(int)GumpIDs.ButtonIDs.Customize].Content[p_Skin,1], 1, GumpButtonType.Reply, 0);	// 1 Edit
		}
	}

	public class ToolbarEdit : Gump
	{
		public static string HTML = String.Format( "<center><u>Command Toolbar v{0}</u><br>Made by Joeku AKA Demortris<br>{1}<br>- Customized to Nerun's Distro -</center><br>   This toolbar is extremely versatile. You can switch skins and increase or decrease columns or rows. The toolbar operates like a spreadsheet; you can use the navigation menu to scroll through different commands and bind them. Enjoy!<br><p>If you have questions, concerns, or bug reports, please <A HREF=\"mailto:demortris@adelphia.net\">e-mail me</A>.", ((double)ToolbarHelper.Version) / 100, ToolbarHelper.ReleaseDate);
		private bool p_Expanded;
		private int p_ExpandedInt;
	
		private int p_Font;
		public int Font{ get{ return p_Font; } set{ p_Font = value; } }

		private bool p_Phantom, p_Stealth, p_Reverse, p_Lock;
		public bool Phantom{ get{ return p_Phantom; } set{ p_Phantom = value; } }
		public bool Stealth{ get{ return p_Stealth; } set{ p_Stealth = value; } }
		public bool Reverse{ get{ return p_Reverse; } set{ p_Reverse = value; } }
		public bool Lock{ get{ return p_Lock; } set{ p_Lock = value; } }

		private List<string> p_Commands;
		private int p_Skin, p_Columns, p_Rows;
		private ToolbarInfo p_Info;
		private List<TextRelay> TextRelays = null;

		public List<string> Commands{ get{ return p_Commands; } set{ p_Commands = value; } }
		public int Skin{ get{ return p_Skin; } set{ p_Skin = value; } }
		public int Columns{ get{ return p_Columns; } set{ p_Columns = value; } }
		public int Rows{ get{ return p_Rows; } set{ p_Rows = value; } }

		public ToolbarEdit( ToolbarInfo info ) : this( info, false ){}
		public ToolbarEdit( ToolbarInfo info, bool expanded ) : this( info, expanded, info.Entries, info.Skin, info.Dimensions[0], info.Dimensions[1], info.Font, new bool[]{ info.Phantom, info.Stealth, info.Reverse, info.Lock } ){}

		public ToolbarEdit( ToolbarInfo info, List<string> commands, int skin, int columns, int rows, int font, bool[] switches ) : this( info, false, commands, skin, columns, rows, font, switches ){}
		public ToolbarEdit( ToolbarInfo info, bool expanded, List<string> commands, int skin, int columns, int rows, int font, bool[] switches )  : base(0, 28)
		{
			p_Info = info;
			p_Expanded = expanded;
			p_ExpandedInt = expanded ? 2 : 1;

			p_Font = font;
			p_Phantom = switches[0];
			p_Stealth = switches[1];
			p_Reverse = switches[2];
			p_Lock = switches[3];

			p_Commands = commands;
			p_Skin = skin;
			p_Columns = columns;
			p_Rows = rows;

			AddInit();
			AddControls();
			AddNavigation();
			AddResponses();
			AddEntries();
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile m = sender.Mobile;
			TextRelays = CreateList( info.TextEntries );

			
			bool[] switches = new bool[4]
				{
					info.IsSwitched( 21 ),
					info.IsSwitched( 23 ),
					info.IsSwitched( 25 ),
					info.IsSwitched( 27 )
				};

			switch( info.ButtonID )
			{
				case 0: break;
				case 1: m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin + 1, Columns, Rows, Font, switches )); break;
				case 2: m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin - 1, Columns, Rows, Font, switches )); break;
				case 3: m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin, Columns, Rows + 1, Font, switches )); break;
				case 4: m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin, Columns, Rows - 1, Font, switches )); break;
				case 5: m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin, Columns + 1, Rows, Font, switches )); break;
				case 6: m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin, Columns - 1, Rows, Font, switches )); break;
				//case 7:
					//m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin, Columns, Rows, Font, switches ));
					//m.SendMessage( 32, "The Marker utility is not an active feature yet; please be patient." );
					//goto case 0;
				case 9:		// Default
					List<string> toolbarinfo = ToolbarInfo.DefaultEntries( (int)m.AccessLevel );
					CombineEntries( toolbarinfo );
					toolbarinfo.AddRange( AnalyzeEntries( toolbarinfo.Count ) );
					m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, toolbarinfo, Skin, Columns, Rows, Font, switches ));
					break;
				case 10:	// Okay
					goto case 12;
				case 11:	// Cancel
					goto case 0;
				case 12:	// Apply
					Account acc = m.Account as Account;
					ToolbarInfo infos = null;
					for(int i = 0; i < ToolbarHelper.Infos.Info.Count; i++)
					{
						if(ToolbarHelper.Infos.Info[i].Account == acc.Username)
							infos = ToolbarHelper.Infos.Info[i];
					}
					if(infos == null)
						infos = ToolbarInfo.CreateNew(m, acc);
					List<int> dims = new List<int>();
						dims.Add( Columns );
						dims.Add( Rows );

					infos.SetAttributes(acc.Username, dims, AnalyzeEntries(), Skin, infos.Points, Font, switches );

					if( info.ButtonID == 12 )
						m.SendGump( new ToolbarEdit( infos, this.p_Expanded ) );
					m.CloseGump( typeof( Toolbar ) );
					m.SendGump( new Toolbar( infos ) );
					break;
				case 18: m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin, Columns, Rows, Font + 1, switches )); break;
				case 19: m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin, Columns, Rows, Font - 1, switches )); break;
				case 20:
					m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin, Columns, Rows, Font, switches ));
					m.SendMessage( 2101, "Phantom mode turns the toolbar semi-transparent." );
					break;
				case 22:
					m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin, Columns, Rows, Font, switches ));
					m.SendMessage( 2101, "Stealth mode makes the toolbar automatically minimize when you click a button." );
					break;
				case 24:
					m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin, Columns, Rows, Font, switches ));
					m.SendMessage( 2101, "Reverse mode puts the minimize bar above the toolbar instead of below." );
					break;
				case 26:
					m.SendGump( new ToolbarEdit( p_Info, this.p_Expanded, AnalyzeEntries(), Skin, Columns, Rows, Font, switches ));
					m.SendMessage( 2101, "Lock mode disables closing the toolbar with right-click." );
					break;
				case 28:	// Expand
					m.SendGump( new ToolbarEdit( p_Info, !this.p_Expanded, AnalyzeEntries(), Skin, Columns, Rows, Font, switches ));
					m.SendMessage( 2101, "Expanded view {0}activated.", this.p_Expanded ? "de" : "" );
					break;
			}
		}

		/// <summary>
		/// Takes the gump relay entries and converts them from an Array into a List.
		/// </summary>
		public static List<TextRelay> CreateList( TextRelay[] entries )
		{
			List<TextRelay> list = new List<TextRelay>();
			for( int i = 0; i < entries.Length; i++ )
				list.Add( entries[i] );

			return list;
		}

		public void CombineEntries( List<string> list )
		{
			string temp;
			for( int i = 0; i < list.Count; i++ )
			{
				if( list[i] == "-*UNUSED*-" && (temp = GetEntry( i+13, this ).Text) != "" )
					list[i] = temp;
			}
		}

		public List<string> AnalyzeEntries()
		{ return AnalyzeEntries( 0 ); }

		/// <summary>
		/// Organizes the gump relay entries into a usable collection.
		/// </summary>
		public List<string> AnalyzeEntries( int i )
		{
			List<string> list = new List<string>();

			string temp;
			for( int j = i; j < 135; j++ )
			{
				if( (temp = GetEntry( j+13, this ).Text) == "" )
					list.Add( "-*UNUSED*-" );
				else
					list.Add( temp );
			}

			return list;
		}

		/// <summary>
		/// Gets entry # in the gump relay.
		/// </summary>
		public static TextRelay GetEntry( int entryID, ToolbarEdit gump )
		{
			int temp = 0;
			TextRelay relay = null;
			for( int i = 0; i < gump.TextRelays.Count; i++ )
			{
				if( gump.TextRelays[i].EntryID == entryID )
				{
					temp = i;
					relay = gump.TextRelays[i];
				}
			}
			gump.TextRelays.RemoveAt( temp );
			return relay;
		}

		/// <summary>
		/// Adds the skeleton gump.
		/// </summary>
		public void AddInit()
		{
			AddPage(0);
			AddBackground(0, 0, 620, 120, 9200); 
			AddHtml(10, 10, 240, 100, HTML, true, true); 
		}

		/// <summary>
		/// Adds other dynamic options.
		/// </summary>
		public void AddControls()
		{
			AddBackground(260, 0, 240, 120, 9200); 
			AddLabel(274, 11, 0, String.Format("Skin - {0}", p_Skin + 1 )); 
			if( p_Skin < GumpIDs.Skins - 1 )
				AddButton(359, 10, 2435, 2436, 1, GumpButtonType.Reply, 0); 
			if( p_Skin > 0 )
				AddButton(359, 21, 2437, 2438, 2, GumpButtonType.Reply, 0); 
			AddLabel(274, 36, 0, String.Format("Rows - {0}", p_Rows )); 
			if( p_Rows < 15 )
				AddButton(359, 35, 2435, 2436, 3, GumpButtonType.Reply, 0); 
			if( p_Rows > 1 )
				AddButton(359, 46, 2437, 2438, 4, GumpButtonType.Reply, 0); 
			AddLabel(274, 61, 0, String.Format("Columns - {0}", p_Columns )); 
			if( p_Columns < 9 )
				AddButton(359, 60, 2435, 2436, 5, GumpButtonType.Reply, 0); 
			if( p_Columns > 1 )
				AddButton(359, 71, 2437, 2438, 6, GumpButtonType.Reply, 0); 

			AddHtml( 276, 87, 100, 20, String.Format("{0}Font - {1}", GumpIDs.Fonts[p_Font], p_Font+1), false, false );
			if( p_Font < GumpIDs.Fonts.Length-1 )
				AddButton(359, 85, 2435, 2436, 18, GumpButtonType.Reply, 0); 
			if( p_Font > 0 )
				AddButton(359, 96, 2437, 2438, 19, GumpButtonType.Reply, 0); 


			/*AddLabel(274, 86, 0, "Marker"); 
			AddButton(326, 88, 22153, 22155, 7, GumpButtonType.Reply, 0); 
			AddCheck(349, 86, 210, 211, true, 8); */

		// Version 1.3
			AddLabel(389, 11, 0, "Phantom"); 
				AddButton(446, 13, 22153, 22155, 20, GumpButtonType.Reply, 0); 
				AddCheck(469, 11, 210, 211, Phantom, 21); 
			AddLabel(389, 36, 0, "Stealth"); 
				AddButton(446, 38, 22153, 22155, 22, GumpButtonType.Reply, 0); 
				AddCheck(469, 36, 210, 211, Stealth, 23); 
			AddLabel(389, 61, 0, "Reverse"); 
				AddButton(446, 63, 22153, 22155, 24, GumpButtonType.Reply, 0); 
				AddCheck(469, 61, 210, 211, Reverse, 25); 
			AddLabel(389, 86, 0, "Lock"); 
				AddButton(446, 88, 22153, 22155, 26, GumpButtonType.Reply, 0); 
				AddCheck(469, 86, 210, 211, Lock, 27); 
		}

		/// <summary>
		/// Adds the skeleton navigation section.
		/// </summary>
		public void AddNavigation()
		{
			AddBackground(500, 0, 120, 120, 9200); 
			AddHtml(500, 10, 120, 20, @"<center><u>Navigation</u></center>", false, false); 
			AddLabel( 508, 92, 0, "Expanded View" );
			AddButton( 595, 95, this.p_Expanded ? 5603 : 5601, this.p_Expanded ? 5607 : 5605, 28, GumpButtonType.Reply, 0 );
		}

		/// <summary>
		/// Adds response buttons.
		/// </summary>
		public void AddResponses()
		{
			int temp = 170 + (p_ExpandedInt * 100);
			AddBackground(0, temp, 500, 33, 9200);
			AddButton( 50, temp + 5, 246, 244, 9, GumpButtonType.Reply, 0 );		// Default
			AddButton( 162, temp + 5, 249, 248, 10, GumpButtonType.Reply, 0 );	// Okay
			AddButton( 275, temp + 5, 243, 241, 11, GumpButtonType.Reply, 0 );	// Cancel
			AddButton( 387, temp + 5, 239, 240, 12, GumpButtonType.Reply, 0 );	// Apply
		}

		/// <summary>
		/// Adds the actual command/phrase entries.
		/// </summary>
		public void AddEntries()
		{
			int buffer = 2;
			// CALC
			int entryX = p_ExpandedInt * 149, entryY = p_ExpandedInt * 20; 
			int bgX = 10 + 4 + (buffer * 3) + (entryX * 3), bgY = 10 + 8 + (entryY * 5);
			int divX = bgX - 10, divY = bgY - 10;
			// ENDCALC

			AddBackground(0, 120, 33 + bgX, 32 + bgY, 9200); 
			
				AddBackground(33, 152, bgX, bgY, 9200);
				
				// Add vertical dividers
				for( int m = 1; m <= 2; m++ )
					AddImageTiled( 38 + ( m * entryX ) + buffer + ((m-1)*4), 157, 2, divY, 10004); 

				// Add horizontal dividers
				for( int n = 1; n <= 4; n++ )
					AddImageTiled( 38, 155 +  (n * ( entryY + 2)), divX, 2, 10001); 

			int start = -3, temp = 0;

			for( int i = 1; i <= 9; i++ )
			{
				start += 3;
				if( i == 4 )
					start = 45;
				else if( i == 7 )
					start = 90;
				temp = start;
				/********
				* PAGES *
				*-------*
				* 1 2 3 *
				* 4 5 6 *
				* 7 8 9 *
				********/

				AddPage(i);
				CalculatePages(i);

				// Add column identifiers
				for( int j = 0; j < 3; j++ )
					AddHtml( 38 + buffer + ((j % 3) * (buffer + entryX + 2)), 128, entryX, 20, String.Format("<center>Column {0}</center>", (j+1) + CalculateColumns(i) ), false, false); 

				AddHtml( 2, 128, 30, 20, "<center>Row</center>", false, false );

				int tempInt = 0;
				if( p_Expanded )
					tempInt = 11;
				// Add row identifiers
				for( int k = 0; k < 5; k++ )
					AddHtml(0, 157 + (k * (entryY + 2)) + tempInt, 32, 20, String.Format("<center>{0}</center>", (k+1) +  CalculateRows(i) ), false, false); 

				// Add command entries
				for( int l = 0; l < 15; l++ )
				{
					AddTextEntry( 38 + buffer + ((l % 3) * ((buffer*2) + entryX )), 157 + ((int)Math.Floor((double)l / 3) * (entryY + 2)), entryX-1, entryY, 2101, temp+13, Commands[temp] /*,int size*/);

					if( l%3 == 2 )
						temp += 6;
					temp++;
				}
			}
		}
		
		/// <summary>
		/// Calculates what navigation button takes you to what page.
		/// </summary>
		public void CalculatePages( int i )
		{
			int up = 0, down = 0, left = 0, right = 0;
			switch( i )
			{
				case 1: down = 4; right = 2; break;
				case 2: down = 5; left = 1; right = 3; break;
				case 3: down = 6; left = 2; break;
				case 4: up = 1; down = 7; right = 5; break;
				case 5: up = 2; down = 8; left = 4; right = 6; break;
				case 6: up = 3; down = 9; left = 5; break;
				case 7: up = 4; right = 8; break;
				case 8: up = 5; left = 7; right = 9; break;
				case 9: up = 6; left = 8; break;
			}

			AddNavigation( up, down, left, right );
		}

		/// <summary>
		/// Adds navigation buttons for each page.
		/// </summary>
		public void AddNavigation( int up, int down, int left, int right )
		{
			if( up > 0 )
				AddButton(549, 34, 9900, 9902, 0, GumpButtonType.Page, up); 
			if( down > 0 )
				AddButton(549, 65, 9906, 9908, 0, GumpButtonType.Page, down); 
			if( left > 0 )
				AddButton(523, 50, 9909, 9911, 0, GumpButtonType.Page, left); 
			if( right > 0 )
				AddButton(575, 50, 9903, 9905, 0, GumpButtonType.Page, right); 
		}

		/// <summary>
		/// Damn I've forgotten...
		/// </summary>
		public static int CalculateColumns( int i )
		{
			if( i == 1 || i == 4 || i == 7 )
				return 0;
			else if( i == 2 || i == 5 || i == 8 )
				return 3;
			else
				return 6;
		}

		/// <summary>
		/// Same as above! =(
		/// </summary>
		public static int CalculateRows( int i )
		{
			if( i >= 1 && i <= 3 )
				return 0;
			else if( i >= 4 && i <= 6 )
				return 5;
			else
				return 10;
		}
	}

	public class GumpIDs
	{
		public enum MiscIDs
		{
			Background = 0,
			Color = 1,
			Buttonground = 2,
			ButtonOffset = 3,
		}

		public enum ButtonIDs
		{
			Minimize = 0,
			Maximize = 1,
			Customize = 2,
			SpecialCommand = 3,

			Send = 4,
			Teleport = 5,
			Gate = 6,
		}

		private int p_ID;
		public int ID{ get{ return p_ID; } set{ p_ID = value; }}
		private int[,] p_Content;
		public int[,] Content{ get{ return p_Content; } set{ p_Content = value; }}
		private string p_Name;
		public string Name{ get{ return p_Name; } set{ p_Name = value; }}

		public GumpIDs(int iD, string name, int[,] content)
		{
			p_ID = iD;
			p_Content = content;
			p_Name = name;
		}

		private static string[] p_Fonts = new string[]
		{ "", "<b>", "<i>", "<b><i>", "<small>", "<b><small>", "<i><small>", "<b><i><small>", "<big>", "<b><big>", "<i><big>", "<b><i><big>" };
		public static string[] Fonts{ get{ return p_Fonts; } set{ p_Fonts = value; }}

		public static int Skins = 2;
		private static GumpIDs[] m_Misc = new GumpIDs[]
			{
				new GumpIDs( 0, "Background",		new int[2,1]{{ 9200 }, { 9270 }}),
				new GumpIDs( 1, "Color",			new int[2,1]{{ 0 }, { 0 }}),
				new GumpIDs( 2, "Buttonground",		new int[2,1]{{ 9200 }, { 9350 }}),
				new GumpIDs( 3, "ButtonOffset",		new int[2,1]{{ 5 }, { 7 }}),
			};
		public static GumpIDs[] Misc{ get{ return m_Misc; } set{ m_Misc = value; }}

		private static GumpIDs[] m_Buttons = new GumpIDs[]
			{
				new GumpIDs( 0, "Minimize",			new int[2,4]{{ 5603, 5607, 16, 16 }, { 5537, 5539, 19, 21 }}),
				new GumpIDs( 1, "Maximize",			new int[2,4]{{ 5601, 5605, 16, 16 }, { 5540, 5542, 19, 21 }}),
				new GumpIDs( 2, "Customize",		new int[2,4]{{ 22153, 22155, 16, 16 }, { 5525, 5527, 62, 24 }}),
				new GumpIDs( 3, "SpecialCommand",	new int[2,4]{{ 2117, 2118, 15, 15 }, { 9720, 9722, 29, 29 }}),

				new GumpIDs( 4, "Send",				new int[2,4]{{ 2360, 2360, 11, 11 }, { 2360, 2360, 11, 11 }}),
				new GumpIDs( 5, "Teleport",			new int[2,4]{{ 2361, 2361, 11, 11 }, { 2361, 2361, 11, 11 }}),
				new GumpIDs( 6, "Gate",				new int[2,4]{{ 2362, 2362, 11, 11 }, { 2361, 2361, 11, 11 }}),
			};
		public static GumpIDs[] Buttons{ get{ return m_Buttons; } set{ m_Buttons = value; }}
	}
}
