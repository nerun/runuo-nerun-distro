/************************
 * Talow's Stairs Addon *
 * version 1.0          *
 ************************/

using System;
using Server;

namespace Server.Items
{
	public enum Facing {
		South,
		West,
		North,
		East
	}
	
	public class BaseStairAddon : BaseAddon
	{
		public BaseStairAddon(Facing f, int block, int step, int levels)
		{
			AddComponent( new AddonComponent( step ), 0, 0, 0 );
			
			switch(f){
				case Facing.South: {
					for ( int i = 0; i < levels; i++ ){
						for ( int j = 0; j < i; j++ ){
							AddComponent( new AddonComponent( block ), 0, i * -1, j * 5);
						}
						AddComponent( new AddonComponent( step ), 0, i * -1, i * 5);
					}
					
					break;
				}
				case Facing.West: {
					for ( int i = 0; i < levels; i++ ){
						for ( int j = 0; j < i; j++ ){
							AddComponent( new AddonComponent( block ), i * -1, 0, j * 5);
						}
						AddComponent( new AddonComponent( step ), i * -1, 0, i * 5);
					}
					
					break;
				}
				case Facing.North: {
					for ( int i = 0; i < levels; i++ ){
						for ( int j = 0; j < i; j++ ){
							AddComponent( new AddonComponent( block ), 0, i, j * 5);
						}
						AddComponent( new AddonComponent( step ), 0, i, i * 5);
					}
					
					break;
				}
				case Facing.East: {
					for ( int i = 0; i < levels; i++ ){
						for ( int j = 0; j < i; j++ ){
							AddComponent( new AddonComponent( block ), i, 0, j * 5);
						}
						AddComponent( new AddonComponent( step ), i, 0, i * 5);
					}
					
					break;
				}
			}
		}

		public BaseStairAddon( Serial serial ) : base( serial )
		{
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
	
	public class WornSandStoneStairs : BaseStairAddon
	{
		[Constructable]
		public WornSandStoneStairs(Facing f, int levels) : base(f, 0x03EE, 0x03EF + (int)f, levels)
		{}
		
		public WornSandStoneStairs( Serial serial ) : base( serial )
		{
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
	
	public class MarbleStairs : BaseStairAddon
	{
		[Constructable]
		public MarbleStairs(Facing f, int levels) : base(f, 0x0709, 0x070A + (int)f, levels)
		{}
		
		public MarbleStairs( Serial serial ) : base( serial )
		{
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
	
	public class StoneStairs : BaseStairAddon
	{
		[Constructable]
		public StoneStairs(Facing f, int levels) : base(f, 0x071E, f == Facing.South ? 0x071F : f == Facing.West ? 0x0736 : f == Facing.North ? 0x0737 : 0x0749, levels)
		{}
		
		public StoneStairs( Serial serial ) : base( serial )
		{
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
	
	public class LightWoodStairs : BaseStairAddon
	{
		[Constructable]
		public LightWoodStairs(Facing f, int levels) : base(f, 0x0721, 0x0722 + (int)f, levels )
		{}
		
		public LightWoodStairs( Serial serial ) : base( serial )
		{
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
	
	public class WoodStairs : BaseStairAddon
	{
		[Constructable]
		public WoodStairs(Facing f, int levels) : base(f, 0x0738, 0x0739 + (int)f, levels )
		{}
		
		public WoodStairs( Serial serial ) : base( serial )
		{
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
	
	public class LightStoneStairs : BaseStairAddon
	{
		[Constructable]
		public LightStoneStairs(Facing f, int levels) : base(f, 0x0750, 0x0751 + (int)f, levels)
		{}
		
		public LightStoneStairs( Serial serial ) : base( serial )
		{
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
	
	public class SandStoneStairs : BaseStairAddon
	{
		[Constructable]
		public SandStoneStairs(Facing f, int levels) : base(f, 0x076C, 0x076D + (int)f, levels)
		{}
		
		public SandStoneStairs( Serial serial ) : base( serial )
		{
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
	
	public class DarkStoneStairs : BaseStairAddon
	{
		[Constructable]
		public DarkStoneStairs(Facing f, int levels) : base(f, 0x0788, 0x0789 + (int)f, levels)
		{}
		
		public DarkStoneStairs( Serial serial ) : base( serial )
		{
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
	
	public class BrickStairs : BaseStairAddon
	{
		[Constructable]
		public BrickStairs(Facing f, int levels) : base(f, 0x07A3, 0x07A4 + (int)f, levels)
		{}
		
		public BrickStairs( Serial serial ) : base( serial )
		{
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