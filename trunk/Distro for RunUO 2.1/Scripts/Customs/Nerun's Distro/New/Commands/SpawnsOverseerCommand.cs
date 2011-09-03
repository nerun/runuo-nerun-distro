using System;
using Server;
using System.IO;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Text;
using System.Collections.Generic;
using System.Net;

namespace Server.Commands
{
	public static class GenOverseer
	{	
		public static void Initialize()
		{
			CommandSystem.Register( "GenOverseers", AccessLevel.Administrator, new CommandEventHandler( GenOverseers_OnCommand ) );
			CommandSystem.Register( "GenSeers", AccessLevel.Administrator, new CommandEventHandler( GenOverseers_OnCommand ) );
		}

		[Usage( "GenOverseers" )]
		[Aliases( "GenSeers" )]
		[Description( "Generates Spawns' Overseers around the world." )]
		private static void GenOverseers_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendMessage( "Generating Spawns' Overseers, please wait." );
			
			Decorate.Generate( "Data/Monsters/overseers/Trammel", Map.Trammel );
			Decorate.Generate( "Data/Monsters/overseers/Felucca", Map.Felucca );
			Decorate.Generate( "Data/Monsters/overseers/Ilshenar", Map.Ilshenar );
			Decorate.Generate( "Data/Monsters/overseers/Malas", Map.Malas );
			Decorate.Generate( "Data/Monsters/overseers/Tokuno", Map.Tokuno );
			Decorate.Generate( "Data/Monsters/overseers/Termur", Map.TerMur );
			
			e.Mobile.SendMessage( "Spawns' Overseers generation complete." );
		}
	}

	public class RemOverseer
	{
		public static void Initialize()
		{
			CommandSystem.Register( "RemOverseers", AccessLevel.Administrator, new CommandEventHandler( RemOverseers_OnCommand ) );
			CommandSystem.Register( "RemSeers", AccessLevel.Administrator, new CommandEventHandler( RemOverseers_OnCommand ) );
		}

		[Usage( "RemOverseers" )]
		[Aliases( "RemSeers" )]
		[Description( "Removes all the Spawns' Overseers in all facets." )]
		public static void RemOverseers_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			World.Broadcast( 0x35, true, "Spawns' Overseers are being removed, please wait." );
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

			DateTime endTime = DateTime.Now;
			World.Broadcast( 0x35, true, "{0} Spawns' Overseers has been removed in {1:F1} seconds.", count, (endTime - startTime).TotalSeconds );
		}
	}

}