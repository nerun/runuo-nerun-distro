using System; 
using System.IO; 
using Server; 
using System.Text; 
using System.Collections; 
using System.Net; 
using Server.Mobiles; 
using Server.Network;
using Server.Commands;

namespace Server.Commands 
{ 
	public class WorldItemWipeCommand 
	{ 
		public static void Initialize() 
		{ 
			Register( "Clearall", AccessLevel.Administrator, new CommandEventHandler( Clearall_OnCommand ) ); 
		} 

		public static void Register( string command, AccessLevel access, CommandEventHandler handler ) 
		{ 
			CommandSystem.Register( command, access, handler ); 
		} 

		[Usage( "ClearAll" )] 
		[Description( "Clear all facets." )] 
		public static void Clearall_OnCommand( CommandEventArgs e ) 
		{ 
			Mobile from = e.Mobile; 
			DateTime time = DateTime.Now;

			int countItems = 0;
			int countMobs = 0;
			ArrayList itemsdel = new ArrayList();

			foreach ( Item itemdel in World.Items.Values )
			{
				if ( itemdel.Parent == null )
				{
					itemsdel.Add( itemdel );
					countItems +=1;
				}
			}

			foreach ( Mobile mobdel in World.Mobiles.Values )
			{
				if ( !mobdel.Player )
				{
					itemsdel.Add( mobdel );
					countMobs +=1;
				}
			}

			foreach ( object itemdel2 in itemsdel )
			{
				if(itemdel2 is Item) ((Item)itemdel2).Delete();
				if(itemdel2 is Mobile) ((Mobile)itemdel2).Delete();

			}

			double totalTime = ( ( DateTime.Now - time ).TotalSeconds );
			from.SendMessage( "{0} items removed. {1} mobs removed. Took {2} seconds!", countItems, countMobs, totalTime );
		} 
	} 
}