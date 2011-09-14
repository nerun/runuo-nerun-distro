using System;
using Server;
using Server.Gumps;
using Server.Spells;
using Server.Network;
using System.Collections;

public enum RestrictType
{
	Spells,
	Skills
}

namespace Server.Gumps
{
	public abstract class RestrictGump : Gump
	{
		BitArray m_Restricted;

		RestrictType m_type;

		public RestrictGump( BitArray ba, RestrictType t ) : base( 50, 50 )
		{
			m_Restricted = ba;
			m_type = t;

			Closable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(10, 10, 225, 425, 9380);
			AddLabel(73, 15, 1152, (t == RestrictType.Spells) ? "Restrict Spells" : "Restrict Skills" );
			AddButton(91, 411, 247, 248, 1, GumpButtonType.Reply, 0);
			//Okay Button ->  # 1



			int itemsThisPage = 0;
			int nextPageNumber = 1;
		    
			Object[] ary;// = (t == RestrictType.Skills) ? SkillInfo.Table : SpellRegistry.Types;

			if( t == RestrictType.Skills )
				ary = SkillInfo.Table;
			else
				ary = SpellRegistry.Types;


			for( int i = 0; i < ary.Length; i++ )
			{
				if( ary[i] != null )
				{
					if( itemsThisPage >= 8 || itemsThisPage == 0)
					{
						itemsThisPage = 0;

						if( nextPageNumber != 1)
						{
							AddButton(190, 412, 4005, 4007, 2, GumpButtonType.Page, nextPageNumber);
							//Forward button -> #2
						}

						AddPage( nextPageNumber++ );

						if( nextPageNumber != 2)
						{
							AddButton(29, 412, 4014, 4016, 3, GumpButtonType.Page, nextPageNumber-2);
							//Back Button -> #3
						}
					}

					AddCheck(40, 55 + ( 45 * itemsThisPage ), 210, 211, ba[i], i + ((t == RestrictType.Spells) ? 100 : 500) );
					//checkbox -> ID = 100 + i for spells,    500 + i for skills
					//Console.WriteLine( ary[i].GetType().ToString() );
					AddLabel(70, 55 + ( 45 * itemsThisPage ) , 0, ((t == RestrictType.Spells) ? ((Type)(ary[i])).Name : ((SkillInfo)(ary[i])).Name ));
	
					itemsThisPage++;                    
				}
			}	
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if( info.ButtonID == 1 )
			{
				for( int i = 0; i < m_Restricted.Length; i++ )
				{
					m_Restricted[ i ] = info.IsSwitched( i + ((m_type == RestrictType.Spells) ? 100 : 500 ));
					//This way is faster after looking at decompiled BitArray.SetAll( bool )
				}
			}
		}
	}

	public class SpellRestrictGump : RestrictGump
	{
		public SpellRestrictGump( BitArray ba ) : base( ba, RestrictType.Spells )
		{

		}
	}

	public class SkillRestrictGump : RestrictGump
	{
		public SkillRestrictGump( BitArray ba ) : base( ba, RestrictType.Skills )
		{

		}
	}
}