/**************************************
*Script Name: Automatic Speed Booster
*Author: Joeku
*For use with RunUO 2.0 RC2
*Client Tested with: 6.0.9.1
*Version: 1.00
*Initial Release: 08/06/08
*Revision Date: 08/06/08
**************************************/

using System;
using Server;
using Server.Accounting;
using Server.Commands;

namespace Joeku.ASB
{
	public class ASB_Main
	{
		public const int Version = 100; // Script version (do not change)
		public static AccessLevel HasAccess; // Who has access to the ASB? Mirrors that of the SpeedBoost command (default Counselor)
		public static bool Initialized = false; // Has the script been initialized yet? (activated upon first player login)
		public static bool Running = true; // Is the script active? (deactivates if SpeedBoost command is not found)

		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler(EventSink_OnLogin);
		}

		// Cannot fully initialize until shard is completely loaded, due to conflicts with the CommandSystem
		public static void Start()
		{
			CommandEntry entry = CommandSystem.Entries["SpeedBoost"]; // Find the SpeedBoost CommandEntry
			if( entry != null ) //  The SpeedBoost command exists...
			{
				HasAccess = entry.AccessLevel; // Set the access to the system to the AccessLevel required to use the SpeedBoost command
				CommandSystem.Register("AutoSpeedBooster", HasAccess, new CommandEventHandler(EventSink_OnCommand)); // Register the ASB command in the CommandSystem
				HelpInfo.FillTable(); // Refresh the HelpInfo dictionary to include the ASB command
				Initialized = true; // The script has now been initialized
			}
			else // The SpeedBoost command does not exist...
				Running = false; // Deactivate the script
		}

		public static void EventSink_OnLogin(LoginEventArgs e)
		{
			if( !Running ) // The script has been deactivated
				return;

			if( !Initialized ) // The script has not yet been initialized...
				Start(); // Initialize the script

			e.Mobile.SendMessage(String.Empty); // Skip a line
			if( e.Mobile.AccessLevel >= HasAccess ) // Does the player have access to the ASB?
			{
				if( GetASB(e.Mobile) ) // The player's ASB is enabled (default)...
				{
					CommandSystem.Handle(e.Mobile, String.Format("{0}SpeedBoost", CommandSystem.Prefix)); // Force the player to invoke the SpeedBoost command
					e.Mobile.SendMessage("Automatic speed boost is enabled for this account. To disable it, use the \"AutoSpeedBooster\" command.");
				}
				else // The player's ASB is disabled
					e.Mobile.SendMessage("Automatic speed boost is disabled for this account. To enable it, use the \"AutoSpeedBooster\" command.");
			}
			e.Mobile.SendMessage(String.Empty); // Skip a line
		}

		[Usage( "AutoSpeedBooster [true|false]" )]
		[Description( "Toggles automatic speed boost upon login for the invoker." )]
		public static void EventSink_OnCommand(CommandEventArgs e)
		{
			if( e.Length == 1 ) // There is one argument present...
			{
				if( Insensitive.Equals(e.Arguments[0], "true") ) // The argument is "true"...
					SetASB(e.Mobile, true); // Enable the player's ASB
				else if( Insensitive.Equals(e.Arguments[0], "false") ) // The argument is "false"...
					SetASB(e.Mobile, false); // Disable the player's ASB
				else // The argument is not a boolean value...
					e.Mobile.SendMessage("Format: AutoSpeedBooster [true|false]");
			}
			else if( e.Length > 1 ) // There is more than one argument present...
				e.Mobile.SendMessage("Format: AutoSpeedBooster [true|false]");
			else // There are no arguments present
				SetASB(e.Mobile, !GetASB(e.Mobile)); // Toggle the player's SpeedBooster
		}

		public static bool GetASB(Mobile mob){ return GetASB((Account)mob.Account); }
		public static bool GetASB(Account acc)
		{
			if( acc.GetTag("ASB") != null ) // Since the account has a SpeedBooster tag, the player's ASB has been disabled
				return false;
			return true;
		}

		public static void SetASB(Mobile mob, bool set)
		{
			Account acc = (Account)mob.Account;

			if( set ) // Enable the player's ASB...
			{
				acc.RemoveTag("ASB"); // Remove any ASB tags the player's account might have
				mob.SendMessage("Automatic speed boost has been enabled for this account.");
			}
			else // Disable the player's ASB...
			{
				acc.SetTag("ASB", "Automatic speed boost has been disabled for this account."); // Add an ASB tag to the player's account
				mob.SendMessage("Automatic speed boost has been disabled for this account.");
			}
		}
	}
}
