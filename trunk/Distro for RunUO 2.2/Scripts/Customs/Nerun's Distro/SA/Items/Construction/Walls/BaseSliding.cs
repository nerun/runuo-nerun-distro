using System;

namespace Server.Items
{
	public class BaseSliding : Item
	{
		private int m_OpenedID;
		private int m_ClosedID;
		
		[Constructable]
		public BaseSliding( int closedID, int openedID ) : base( closedID )
		{
			Movable = false;
			m_OpenedID = openedID;
			m_ClosedID = closedID;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			this.ItemID = this.m_OpenedID;
			Timer.DelayCall( TimeSpan.FromSeconds( 1.6 ), delegate()
			{
				this.Z += -22;
				this.Visible = false;
				Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), delegate()
				{
					this.ItemID = this.m_ClosedID;
					this.Visible = true;
					this.Z += 22;
				} );
			} );
		}

		public BaseSliding( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_OpenedID );
			writer.Write( m_ClosedID );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_OpenedID = reader.ReadInt();
			m_ClosedID = reader.ReadInt();
		}
	}
}