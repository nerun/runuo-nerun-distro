/*
using System;
using Server;

namespace Server.Items
{
	public class TormentedMinotaurStatuette : MonsterStatuette
	{
	      

		[Constructable]
		public TormentedMinotaurStatuette() : base( MonsterStatuetteType.TormentedMinotaur )
 
		{
			Name = "Tormented Minotaur Statuette";
			ItemID = 0x2D88;
                  Weight = 1.0;			
		      LootType = LootType.Blessed;
            }

		public TormentedMinotaurStatuette( Serial serial ) : base( serial )
		{
		}
		
		private static int[] m_Sounds = new int[]
		{
			0x597, 0x598, 0x599, 0x59A, 0x59B, 0x59C, 0x59D		
		};

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( TurnedOn && IsLockedDown && (!m.Hidden || m.AccessLevel == AccessLevel.Player) && Utility.InRange( m.Location, this.Location, 2 ) && !Utility.InRange( oldLocation, this.Location, 2 ) )
				Effects.PlaySound( Location, Map, m_Sounds[ Utility.Random( m_Sounds.Length ) ] );
				
			base.OnMovement( m, oldLocation );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}
*/