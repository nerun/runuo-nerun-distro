// Engine r114
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Engines.Quests;
using Server.Commands;

namespace Server.Commands
{
    public static class DecorateSE
    {
        public static void Initialize()
        {
            CommandSystem.Register("DecorateSE", AccessLevel.Administrator, new CommandEventHandler(DecorateSE_OnCommand));
            CommandSystem.Register("DecorateDeleteSE", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteSE_OnCommand));
			CommandSystem.Register("DecoDelSE", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteSE_OnCommand));
        }

        [Usage("DecorateSE")]
        [Description("Generates world decorations for a Samurai Empire shard.")]
        private static void DecorateSE_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}Decorate", prefix ) );
			//Remove some default decorations
			DecorateDelete.RemoveOne( "Data/Decoration/Britannia", "britain.cfg", Map.Trammel, Map.Felucca );
			//SE
			Decorate.Generate( "Data/Nerun's Distro/Decoration/06-SE/Britannia", Map.Trammel, Map.Felucca );

            e.Mobile.SendMessage("Samurai Empire decorated.");
        }

        [Usage("DecorateDeleteSE")]
		[Aliases( "DecoDelSE" )]
        [Description("Removes world decorations of a Samurai Empire shard.")]
        private static void DecorateDeleteSE_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateDelete", prefix ) );
			
			//Nerun's Distro custom decoration
			DecorateDelete.Remove( "Data/Nerun's Distro/Decoration/06-SE/Britannia", Map.Trammel, Map.Felucca );

            e.Mobile.SendMessage("Decoration removal completed.");
        }
    }

    public static class DecorateML
    {
        public static void Initialize()
        {
            CommandSystem.Register("DecorateML", AccessLevel.Administrator, new CommandEventHandler(DecorateML_OnCommand));
            CommandSystem.Register("DecorateDeleteML", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteML_OnCommand));
			CommandSystem.Register("DecoDelML", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteML_OnCommand));
        }

        [Usage("DecorateML")]
        [Description("Generates world decorations for a Mondain's Legacy shard.")]
        private static void DecorateML_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateSE", prefix ) );
			//ML
			Decorate.Generate( "Data/Nerun's Distro/Decoration/07-ML/Britannia", Map.Trammel, Map.Felucca );
			Decorate.Generate( "Data/Nerun's Distro/Decoration/07-ML/Felucca", Map.Felucca );
			Decorate.Generate( "Data/Nerun's Distro/Decoration/07-ML/Trammel", Map.Trammel );
			Decorate.Generate( "Data/Nerun's Distro/Decoration/07-ML/Ilshenar", Map.Ilshenar );
			Decorate.Generate( "Data/Nerun's Distro/Decoration/07-ML/Malas", Map.Malas );

            e.Mobile.SendMessage("Mondain's Legacy decorated.");
        }

        [Usage("DecorateDeleteML")]
		[Aliases( "DecoDelML" )]
        [Description("Removes world decorations of a Mondain's Legacy shard.")]
        private static void DecorateDeleteML_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateDeleteSE", prefix ) );
			//Nerun's Distro custom decoration
			DecorateDelete.Remove( "Data/Nerun's Distro/Decoration/07-ML/Britannia", Map.Trammel, Map.Felucca );
			DecorateDelete.Remove( "Data/Nerun's Distro/Decoration/07-ML/Felucca", Map.Felucca );
			DecorateDelete.Remove( "Data/Nerun's Distro/Decoration/07-ML/Trammel", Map.Trammel );
			DecorateDelete.Remove( "Data/Nerun's Distro/Decoration/07-ML/Ilshenar", Map.Ilshenar );
			DecorateDelete.Remove( "Data/Nerun's Distro/Decoration/07-ML/Malas", Map.Malas );
        }
    }

    public static class DecorateKRfirstAge
    {
        public static void Initialize()
        {
            CommandSystem.Register("DecorateKRfirstAge", AccessLevel.Administrator, new CommandEventHandler(DecorateKRfirstAge_OnCommand));
            CommandSystem.Register("DecorateDeleteKRfirstAge", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteKRfirstAge_OnCommand));
			CommandSystem.Register("DecoDelKRfa", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteKRfirstAge_OnCommand));
        }

        [Usage("DecorateKRfirstAge")]
        [Description("Generates world decorations for a Kingdom's Reborn First Age shard.")]
        private static void DecorateKRfirstAge_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateML", prefix ) );
			//Remove some default decorations
			DecorateDelete.RemoveOne( "Data/Decoration/Trammel", "_haven additions.cfg", Map.Trammel );
			DecorateDelete.RemoveOne( "Data/Decoration/Trammel", "collector quest.cfg", Map.Trammel );
			DecorateDelete.RemoveOne( "Data/Decoration/Trammel", "haven.cfg", Map.Trammel );
			DecorateDelete.RemoveOne( "Data/Decoration/Trammel", "uzeraan turmoil quest.cfg", Map.Trammel );
			//KR
			Decorate.Generate( "Data/Nerun's Distro/Decoration/08-KR/Trammel", Map.Trammel );

            e.Mobile.SendMessage("Kingdom's Reborn - I decorated.");
        }

        [Usage("DecorateDeleteKRfirstAge")]
		[Aliases( "DecoDelKRfa" )]
        [Description("Removes world decorations of a Kingdom's Reborn First Age shard.")]
        private static void DecorateDeleteKRfirstAge_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateDeleteML", prefix ) );
			//Nerun's Distro custom decoration
			DecorateDelete.Remove( "Data/Nerun's Distro/Decoration/08-KR/Trammel", Map.Trammel );
        }
    }

    public static class DecorateKRsecondAge
    {
        public static void Initialize()
        {
            CommandSystem.Register("DecorateKRsecondAge", AccessLevel.Administrator, new CommandEventHandler(DecorateKRsecondAge_OnCommand));
            CommandSystem.Register("DecorateDeleteKRsecondAge", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteKRsecondAge_OnCommand));
			CommandSystem.Register("DecoDelKRsa", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteKRsecondAge_OnCommand));
        }

        [Usage("DecorateKRsecondAge")]
        [Description("Generates world decorations for a Kingdom's Reborn Second Age shard.")]
        private static void DecorateKRsecondAge_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateKRfirstAge", prefix ) );
			//Decorate.Generate( "Data/Decoration/RuinedMaginciaTram", Map.Trammel );
			//Decorate.Generate( "Data/Decoration/RuinedMaginciaFel", Map.Felucca );
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateMag", prefix ) );
			//Remove some default decorations
			DecorateDelete.RemoveOne( "Data/Decoration/Britannia", "magincia.cfg", Map.Trammel, Map.Felucca );

            e.Mobile.SendMessage("Kingdom's Reborn - II decorated.");
        }

        [Usage("DecorateDeleteKRsecondAge")]
		[Aliases( "DecoDelKRsa" )]
        [Description("Removes world decorations of a Kingdom's Reborn Second Age shard.")]
        private static void DecorateDeleteKRsecondAge_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateDeleteKRfirstAge", prefix ) );
			DecorateDelete.Remove( "Data/Decoration/RuinedMaginciaTram", Map.Trammel );
			DecorateDelete.Remove( "Data/Decoration/RuinedMaginciaFel", Map.Felucca );
        }
    }

    public static class DecorateSA
    {
        public static void Initialize()
        {
            CommandSystem.Register("DecorateSA", AccessLevel.Administrator, new CommandEventHandler(DecorateSA_OnCommand));
            CommandSystem.Register("DecorateDeleteSA", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteSA_OnCommand));
			CommandSystem.Register("DecoDelSA", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteSA_OnCommand));
        }

        [Usage("DecorateSA")]
        [Description("Generates world decorations for a Stygian Abyss shard.")]
        private static void DecorateSA_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateKRsecondAge", prefix ) );
			//Nerun's Distro custom decoration
			Decorate.Generate( "Data/Nerun's Distro/Decoration/09-SA/TerMur", Map.TerMur );
			Decorate.Generate( "Data/Nerun's Distro/Decoration/09-SA/Trammel", Map.Trammel );

            e.Mobile.SendMessage("Stygian Abyss decorated.");
        }

        [Usage("DecorateDeleteSA")]
		[Aliases( "DecoDelSA" )]
        [Description("Removes world decorations of a Stygian Abyss shard.")]
        private static void DecorateDeleteSA_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateDeleteKRsecondAge", prefix ) );
			DecorateDelete.Remove( "Data/Nerun's Distro/Decoration/09-SA/TerMur", Map.TerMur );
			DecorateDelete.Remove( "Data/Nerun's Distro/Decoration/09-SA/Trammel", Map.Trammel );
        }
    }

    public static class DecorateHSfirstAge
    {
        public static void Initialize()
        {
            CommandSystem.Register("DecorateHSfirstAge", AccessLevel.Administrator, new CommandEventHandler(DecorateHSfirstAge_OnCommand));
            CommandSystem.Register("DecorateDeleteHSfirstAge", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteHSfirstAge_OnCommand));
			CommandSystem.Register("DecoDelHSfa", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteHSfirstAge_OnCommand));
        }

        [Usage("DecorateHSfirstAge")]
        [Description("Generates world decorations for a High Seas First Age shard.")]
        private static void DecorateHSfirstAge_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateSA", prefix ) );
			//Nerun's Distro custom decoration
			Decorate.Generate( "Data/Nerun's Distro/Decoration/10-HS/Britannia", Map.Trammel, Map.Felucca );

            e.Mobile.SendMessage("High Seas - I decorated.");
        }

        [Usage("DecorateDeleteHSfirstAge")]
		[Aliases( "DecoDelHSfa" )]
        [Description("Removes world decorations of a High Seas First Age shard.")]
        private static void DecorateDeleteHSfirstAge_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateDeleteSA", prefix ) );
			DecorateDelete.Remove( "Data/Nerun's Distro/Decoration/10-HS/Britannia", Map.Trammel, Map.Felucca );
        }
    }

    public static class DecorateHSsecondAge
    {
        public static void Initialize()
        {
            CommandSystem.Register("DecorateHSsecondAge", AccessLevel.Administrator, new CommandEventHandler(DecorateHSsecondAge_OnCommand));
            CommandSystem.Register("DecorateDeleteHSsecondAge", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteHSsecondAge_OnCommand));
			CommandSystem.Register("DecoDelHSsa", AccessLevel.Administrator, new CommandEventHandler(DecorateDeleteHSsecondAge_OnCommand));
        }

        [Usage("DecorateHSsecondAge")]
        [Description("Generates world decorations for a High Seas Second Age shard.")]
        private static void DecorateHSsecondAge_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateHSfirstAge", prefix ) );
			DecorateDelete.Remove( "Data/Decoration/RuinedMaginciaTram", Map.Trammel );
			DecorateDelete.Remove( "Data/Decoration/RuinedMaginciaFel", Map.Felucca );

            e.Mobile.SendMessage("High Seas - II decorated.");
        }

        [Usage("DecorateDeleteHSsecondAge")]
		[Aliases( "DecoDelHSsa" )]
        [Description("Removes world decorations of a High Seas Second Age shard.")]
        private static void DecorateDeleteHSsecondAge_OnCommand(CommandEventArgs e)
        {
			string prefix = Server.Commands.CommandSystem.Prefix;
			CommandSystem.Handle( e.Mobile, String.Format( "{0}DecorateDeleteHSfirstAge", prefix ) );
        }
    }
}