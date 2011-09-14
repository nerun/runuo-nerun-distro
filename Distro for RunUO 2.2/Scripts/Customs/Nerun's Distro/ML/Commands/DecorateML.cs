using System;
using Server;
using System.IO;
using Server.Commands;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server
{
	public static class MondainsLegacy
	{	
		public static void Initialize()
		{
			CommandSystem.Register( "DecorateML", AccessLevel.Administrator, new CommandEventHandler( DecorateML_OnCommand ) );
		}

		[Usage( "DecorateML" )]
		[Description( "Generates Mondain's Legacy world decoration." )]
		private static void DecorateML_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendMessage( "Generating Mondain's Legacy world decoration, please wait." );
			
			Decorate.Generate( "Data/Decoration/Mondain's Legacy/Trammel", Map.Trammel );
			Decorate.Generate( "Data/Decoration/Mondain's Legacy/Felucca", Map.Felucca );
			Decorate.Generate( "Data/Decoration/Mondain's Legacy/Ilshenar", Map.Ilshenar );
			Decorate.Generate( "Data/Decoration/Mondain's Legacy/Malas", Map.Malas );
			Decorate.Generate( "Data/Decoration/Mondain's Legacy/Tokuno", Map.Tokuno );
			
			e.Mobile.SendMessage( "Mondain's Legacy world generation complete." );
		}
	}
}