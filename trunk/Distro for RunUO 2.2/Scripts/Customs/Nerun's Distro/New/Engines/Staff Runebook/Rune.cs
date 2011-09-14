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
	public class SR_Rune
	{
		public string Name;
		public bool IsRunebook = false;
		public List<SR_Rune> Runes;
		public int Count{ get{ return Runes.Count; } }
		public int RunebookCount, RuneCount;
		public int PageIndex = -1;
		public SR_Rune ParentRune;

		public int Tier
		{
			get
			{
				if( this.ParentRune != null )
					return ParentRune.Tier + 1;

				return 0;
			}
		}

		public Map TargetMap = Map.Felucca;
		public Point3D TargetLoc = new Point3D( 0, 0, 0 );

		public SR_Rune( string name, Map map, Point3D loc ) : this( name, false )
		{
			TargetMap = map;
			TargetLoc = loc;
		}
		public SR_Rune( string name, bool isRunebook ) : this( name, isRunebook, new List<SR_Rune>() ){}
		public SR_Rune( string name, bool isRunebook, List<SR_Rune> runes )
		{
			Name = name;
			IsRunebook = isRunebook;
			Runes = runes;
			FindCounts();
		}

		public void ResetPageIndex()
		{
			if( !IsRunebook || PageIndex == -1 )
				return;

			if( Runes[PageIndex] != null )
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

			rune.ParentRune = this;
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
		public static SR_Rune Deserialize( GenericReader reader, int version )
		{
			SR_Rune rune = null;

			string name = reader.ReadString();
			bool isRunebook = reader.ReadBool();

			Map targetMap = reader.ReadMap();
			Point3D targetLoc = reader.ReadPoint3D();

			if( isRunebook )
				rune = new SR_Rune( name, isRunebook );
			else
				rune = new SR_Rune( name, targetMap, targetLoc );

			int count = reader.ReadInt();
			for( int i = 0; i < count; i++ )
				rune.AddRune( SR_Rune.Deserialize( reader, version ) );

			return rune;
		}
	}
}