/**************************************
*Script Name: Staff Runebook          *
*Author: Joeku                        *
*For use with RunUO 2.0 RC2           *
*Client Tested with: 6.0.9.2          *
*Version: 1.10                        *
*Initial Release: 11/25/07            *
*Revision Date: 02/04/09              *
**************************************/

using System;
using Server;
using Server.Prompts;

namespace Joeku.SR
{
	public class SR_NewRunePrompt : Prompt
	{
		public SR_RuneAccount RuneAcc;
		public bool IsRunebook;
		public Point3D TargetLoc;
		public Map TargetMap;

		public SR_NewRunePrompt( SR_RuneAccount runeAcc )
		{
			RuneAcc = runeAcc;
			IsRunebook = true;
		}

		public SR_NewRunePrompt( SR_RuneAccount runeAcc, Point3D targetLoc, Map targetMap )
		{
			RuneAcc = runeAcc;
			TargetLoc = targetLoc;
			TargetMap = targetMap;
		}

		public override void OnResponse( Mobile mob, string text )
		{
			text = text.Trim();

			if ( text.Length > 40 )
				text = text.Substring( 0, 40 );

			if ( text.Length > 0 )
			{
				SR_Rune rune = null;
				if( IsRunebook )
					rune = new SR_Rune( text, true );
				else
					rune = new SR_Rune( text, TargetMap, TargetLoc );

				if( RuneAcc.ChildRune == null )
					RuneAcc.AddRune( rune );
				else
					RuneAcc.ChildRune.AddRune( rune );
			}

			SR_Gump.Send( mob, RuneAcc );
		}
	}
}