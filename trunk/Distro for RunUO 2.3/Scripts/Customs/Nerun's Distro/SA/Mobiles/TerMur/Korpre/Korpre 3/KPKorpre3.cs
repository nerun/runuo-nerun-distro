using System;
using System.Collections; 
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Network;
using Server.Targeting;


namespace Server.Commands
{ 
	public class KPKorpre3System
	{ 

		public static void Initialize()
		{
			CommandSystem.Register( "KPKorpre3", AccessLevel.Player, new CommandEventHandler( KPKorpre3_OnCommand ) );    
		} 

		public static void KPKorpre3_OnCommand( CommandEventArgs e )
		{ 
			PlayerMobile from = e.Mobile as PlayerMobile; 
          
			if( from != null ) 
			{  
				from.Target = new InternalTarget( from );
			} 
		} 

		private class InternalTarget : Target
		{
			public InternalTarget( Mobile from ) : base( 8, false, TargetFlags.None )
			{
				from.SendMessage ( "Target an animal that you own to check their level." );
			}

			protected override void OnTarget( Mobile from, object obj )
			{
        if ( !from.Alive )
          {
					from.SendMessage( "You may not do that while dead." );
          }
          else if ( obj is EvolutionKorpre3 && obj is BaseCreature )//Korpre
          { 
					BaseCreature bc = (BaseCreature)obj;
					EvolutionKorpre3 ed = (EvolutionKorpre3)obj;

					if ( ed.Controlled == true && ed.ControlMaster == from )
					{
						ed.PublicOverheadMessage( MessageType.Regular, ed.SpeechHue, true, ed.Name +" has "+ ed.KPKorpre3 +" kill points.", false );
					}
					else
					{
						from.SendMessage( "You do not control this Korpre!" );
					}
				}
			}
		}
	}
}