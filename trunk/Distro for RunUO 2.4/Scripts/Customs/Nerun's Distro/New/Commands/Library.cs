/*********************************
* Nerun's Library - Version 0.41 *
* Collection of usefull methods  *
**********************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Server;

namespace Server
{
	public class Lib
	{
		// IS LETTER
		public static bool IsLetter( string r )
		{
			string s = r.ToLower();
			
			string[] Letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
			
			bool exists = Array.Exists( Letters, delegate(string t) { return t.Equals(s); } );
			
			return exists;
		}

		// IS NUMBER
		public static bool IsNumber( string r )
		{
			string s = r.ToLower();
			
			string[] Numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
			
			bool exists = Array.Exists( Numbers, delegate(string t) { return t.Equals(s); } );
			
			return exists;
		}
		
		// IS VALID EXPANSION
		public static bool IsValidExpansion( string args )
		{
			string s = args.ToLower();
			
			string[] exp = { "se", "ml", "kr1", "kr2", "sa", "hs1", "hs2" };
			
			bool exists = Array.Exists( exp, delegate(string t) { return t.Equals(s); } );
			
			return exists;
		}
		
		// LIST OF LINES
		public static List<string> ListOfLines( string WorkingFolder, string FileName ) // = "Data/MyExamples" and "Example.cfg"
		{
			string folders = WorkingFolder + "/" + FileName; // = "Data/MyExamples/Example.cfg"
			string path = Path.Combine( Core.BaseDirectory, folders ); // = "C:/RunUO/Data/MyExamples/Example.cfg"
			
			if ( File.Exists( path ) )
			{
				using ( StreamReader ip = new StreamReader( path ) )
				{
					string file = ip.ReadToEnd();
					List<string> lines = new List<string>( file.Split(new char[] {'\n'}) );
					lines.Add( path );
					file = null;
					return lines; // n1, n2, n3, ..., ni, path
				}
			}
			else
			{
				List<string> nothing = new List<string>();
				nothing.Add( path );
				return nothing; // path
			}
		}
	}
}