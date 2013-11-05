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
using System.Collections.Generic;
using Server;

namespace Joeku.SR
{
	public class SR_RuneAccount
	{
		public string Username;
		public List<SR_Rune> Runes;
		public int Count{ get{ return Runes.Count; } }
		public int RunebookCount, RuneCount;
		public int PageIndex = -1;

		public SR_Rune ChildRune
		{
			get
			{
				SR_Rune rune = null;

				if( PageIndex > -1 )
					rune = Runes[PageIndex];

				if( rune != null )
				{
					while( rune.PageIndex > -1 )
						rune = rune.Runes[rune.PageIndex];
				}

				return rune;
			}
		}

		public SR_RuneAccount( string username ) : this( username, new List<SR_Rune>() ){}
		public SR_RuneAccount( string username, List<SR_Rune> runes )
		{
			Username = username;
			Runes = runes;
			FindCounts();

			SR_Main.AddInfo( this );
		}

		public void ResetPageIndex()
		{
			Runes[PageIndex].ResetPageIndex();
			PageIndex = -1;
		}

		public void Clear()
		{
			Runes.Clear();
			RunebookCount = 0;
			RuneCount = 0;
			PageIndex = -1;
		}

		public void AddRune( SR_Rune rune )
		{
			for( int i = 0; i < Count; i++ )
				if( Runes[i] == rune )
					Runes.RemoveAt(i);

			if( rune.IsRunebook )
			{
				Runes.Insert( RunebookCount, rune );
				RunebookCount++;
			}
			else
			{
				Runes.Add( rune );
				RuneCount++;
			}
		}

		public void RemoveRune( int index ){ RemoveRune( index, false ); }
		public void RemoveRune( int index, bool pageIndex )
		{
			if( Runes[index].IsRunebook )
				RunebookCount--;
			else
				RuneCount--;

			if( pageIndex && PageIndex == index )
				PageIndex = -1;

			Runes.RemoveAt( index );
		}

		public void FindCounts()
		{
			int runebookCount = 0, runeCount = 0;
			for( int i = 0; i < Runes.Count; i++ )
				if( Runes[i].IsRunebook )
					runebookCount++;
				else
					runeCount++;

			RunebookCount = runebookCount;
			RuneCount = runeCount;
		}

		// Legacy... binary serialization only used in v1.00, deserialization preserved to migrate data.
		public static void Deserialize( GenericReader reader, int version )
		{
			List<SR_Rune> runes = new List<SR_Rune>();

			string username = reader.ReadString();
			Console.Write("  Account: {0}... ", username);
			int count = reader.ReadInt();
			for( int i = 0; i < count; i++ )
				runes.Add( SR_Rune.Deserialize( reader, version ) );
			new SR_RuneAccount( username, runes );
			Console.WriteLine("done.");
		}
	}
}