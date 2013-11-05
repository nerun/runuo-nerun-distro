using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.Targeting;
using Server.Targets;
using Server.Mobiles;
using Server.Items;
using Server.Misc; 

namespace Server.Commands
{
	public class GMbody
	{
		public static void Initialize()
		{
			Register( "GMbody", AccessLevel.Counselor, new CommandEventHandler( GM_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
			CommandSystem.Register( command, access, handler );
		}
		
		[Usage( "GMbody" )]
		[Description( "Helps senior staff members set their body to GM style." )]
		public static void GM_OnCommand( CommandEventArgs e )
		{
			e.Mobile.Target = new GMmeTarget();
		}

		private class GMmeTarget : Target
		{
			public GMmeTarget() : base( -1, false, TargetFlags.None )
			{
			}

			private static void DisRobe( Mobile m_from, Container cont, Layer layer ) 
			{ 
				if ( m_from.FindItemOnLayer( layer ) != null )
				{
					Item item = m_from.FindItemOnLayer( layer );
					cont.AddItem( item );
				}
			}

			private static Mobile m_Mobile;

			private static void EquipItem( Item item )
			{
				EquipItem( item, false );
			}

			private static void EquipItem( Item item, bool mustEquip )
			{
				if ( !Core.AOS )
					item.LootType = LootType.Newbied;

				if ( m_Mobile != null && m_Mobile.EquipItem( item ) )
					return;

				Container pack = m_Mobile.Backpack;

				if ( !mustEquip && pack != null )
					pack.DropItem( item );
				else
					item.Delete();
			}

			private static void PackItem( Item item )
			{
				if ( !Core.AOS )
					item.LootType = LootType.Newbied;

				Container pack = m_Mobile.Backpack;

				if ( pack != null )
					pack.DropItem( item );
				else
					item.Delete();
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile )
				{
					Mobile targ = (Mobile)targeted;
					if ( from != targ ) 
						from.SendMessage( "You may only set your own body to GM style." );

					else 
					{
						m_Mobile = from;

						CommandLogging.WriteLine( from, "{0} {1} is assuming a GM body", from.AccessLevel, CommandLogging.Format( from ) );
						
						string prefix = Server.Commands.CommandSystem.Prefix;
						CommandSystem.Handle( from, String.Format( "{0}AutoSpeedBooster true", prefix ) );

						Container pack = from.Backpack;

						List<Item> ItemsToDelete = new List<Item>();

						foreach (Item item in from.Items)
						{
							if (item.Layer != Layer.Bank && item.Layer != Layer.Hair && item.Layer != Layer.FacialHair && item.Layer != Layer.Mount && item.Layer != Layer.Backpack)
							{
								ItemsToDelete.Add(item);
							}
						}
						foreach (Item item in ItemsToDelete)
						{
							item.Delete();
						}
						
						if ( pack == null )
						{
							pack = new Backpack();
							pack.Movable = false;

							from.AddItem( pack );
						}
						else
						{
							pack.Delete();
							pack = new Backpack();
							pack.Movable = false;

							from.AddItem( pack );
						}

						from.Hunger = 20;
						from.Thirst = 20;
						from.Fame = 0;
						from.Karma = 0;
						from.Kills = 0;
						from.Hidden = true;
						from.Blessed = true;
						from.Hits = from.HitsMax;
						from.Mana = from.ManaMax;
						from.Stam = from.StamMax;

						if(from.AccessLevel >= AccessLevel.Counselor)
						{
							EquipItem( new StaffRing() );
							Spellbook book1 = new Spellbook( (ulong)18446744073709551615 );
							Spellbook book2 = new NecromancerSpellbook( (ulong)0xffff );
							Spellbook book3 = new BookOfChivalry();
							Spellbook book4 = new BookOfBushido();
							Spellbook book5 = new BookOfNinjitsu();

							PackItem( book1 );
							PackItem( book2 );
							PackItem( book3 );
							PackItem( book4 );
							PackItem( book5 );

							from.RawStr = 100;
							from.RawDex = 100;
							from.RawInt = 100;
							from.Hits = from.HitsMax;
							from.Mana = from.ManaMax;
							from.Stam = from.StamMax;

							for ( int i = 0; i < targ.Skills.Length; ++i )
								targ.Skills[i].Base = 120;
						}

						if(from.AccessLevel == AccessLevel.Counselor)
						{
							EquipItem( new CounselorRobe() );
							EquipItem( new ThighBoots( 3 ) );
							from.Title = "[Counselor]";
						}

						if(from.AccessLevel == AccessLevel.GameMaster)
						{
							EquipItem( new GMRobe() );
							EquipItem( new ThighBoots( 39 ) );
							from.Title = "[GM]";
						}

						if(from.AccessLevel == AccessLevel.Seer)
						{
							EquipItem( new SeerRobe() );
							EquipItem( new ThighBoots( 467 ) );
							from.Title = "[Seer]";
						}

						if(from.AccessLevel >= AccessLevel.Administrator)
						{
							PackItem( new StaffOrb() );
							EquipItem( new ThighBoots( 1001 ) );
							EquipItem( new AdminRobe() );
						}
						
						if(from.AccessLevel == AccessLevel.Administrator)
							from.Title = "[Admin]";

						if(from.AccessLevel == AccessLevel.Developer)
							from.Title = "[Developer]";

						if(from.AccessLevel == AccessLevel.Owner)
							from.Title = "[Owner]";
					}
				}
			}
		}
	}
}