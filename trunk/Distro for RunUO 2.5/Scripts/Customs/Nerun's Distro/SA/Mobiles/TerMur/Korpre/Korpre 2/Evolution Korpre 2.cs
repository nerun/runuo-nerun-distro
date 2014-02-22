using System;
using System.Collections; 
using Server.Mobiles;
using Server.Items;
using Server.Network; 
using Server.Targeting;
using Server.Gumps;

namespace Server.Mobiles
{
	[CorpseName( "a Korpre corpse" )]
	public class EvolutionKorpre2 : BaseCreature
	{
		private Timer m_Korpre2MatingTimer;
		private DateTime m_EndMating;
		
		public int m_Stage;
		public int m_KPKorpre2;
		
		public DateTime EndMating{ get{ return m_EndMating; } set{ m_EndMating = value; } }
		
		public bool m_AllowMating;
		public bool m_HasEgg;
		public bool m_Pregnant;
		
		public bool m_S1;
		public bool m_S2;

		public bool S1
		{
			get{ return m_S1; }
			set{ m_S1 = value; }
		}
		public bool S2
		{
			get{ return m_S2; }
			set{ m_S2 = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public bool AllowMating
		{
			get{ return m_AllowMating; }
			set{ m_AllowMating = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool HasEgg
		{
			get{ return m_HasEgg; }
			set{ m_HasEgg = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public bool Pregnant
		{
			get{ return m_Pregnant; }
			set{ m_Pregnant = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int KPKorpre2
		{
			get{ return m_KPKorpre2; }
			set{ m_KPKorpre2 = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage
		{
			get{ return m_Stage; }
			set{ m_Stage = value; }
		}

		[Constructable]
		public EvolutionKorpre2() : this( "a Korpre" )
		{
		}

		[Constructable]
		public EvolutionKorpre2( string name ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a Korpre";
			Body = 51;
			Hue = 1759;
			BaseSoundID = 456;
			Stage = 1;

			S1 = true;
			S2 = true;

			SetStr( 26 );
			SetDex( 19 );
			SetInt( 17 );

			SetHits( 58 );

			SetDamage( 1, 5 );

			SetDamageType( ResistanceType.Physical, 45 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 45 );
			SetResistance( ResistanceType.Fire, 25 );
			SetResistance( ResistanceType.Cold, 5 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 1 );
			
			SetSkill( SkillName.Wrestling, 16.0, 16.5 );

			Fame = 9000;
			Karma = -10000;

			VirtualArmor = 30;

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 80.0;

			PackItem( new Korpre2Dust( 1 ) );
		}
		
		public EvolutionKorpre2(Serial serial) : base(serial)
		{
		}
		
		public override void Damage( int amount, Mobile defender )
		
		{

			int KPKorpre2gainmin, KPKorpre2gainmax;

			if ( this.Stage == 1 )
			{
				if ( defender is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)defender;

					if ( bc.Controlled != true )
					{
						KPKorpre2gainmin = 5 + ( bc.HitsMax ) / 10;
						KPKorpre2gainmax = 5 + ( bc.HitsMax ) / 5;

						this.KPKorpre2 += Utility.RandomList( KPKorpre2gainmin, KPKorpre2gainmax );
					}
				}

				if ( this.KPKorpre2 >= 50000 )
				{
					if ( this.S1 == true )
					{
						this.S1 = false;
						int va;

						va = ( this.VirtualArmor + 10 );

						//this.AI = AIType.AI_Mage;
						this.Warmode = false;
						this.Say( "*"+ this.Name +" is now a anlorzen*");
						this.Title = "a anlorzen";
						
						this.BodyValue = 0x9D;
            this.BaseSoundID = 0x388;
						this.VirtualArmor = va;
						this.Stage = 2;

						this.SetDamageType( ResistanceType.Physical, 20 );
						this.SetDamageType( ResistanceType.Fire, 20 );
						this.SetDamageType( ResistanceType.Cold, 20 );
						this.SetDamageType( ResistanceType.Poison, 20 );
						this.SetDamageType( ResistanceType.Energy, 20 );

						this.SetResistance( ResistanceType.Physical, 47, 49 );
						this.SetResistance( ResistanceType.Fire, 43, 45 );
						this.SetResistance( ResistanceType.Cold, 36, 40 );
						this.SetResistance( ResistanceType.Poison, 100 );
						this.SetResistance( ResistanceType.Energy, 41, 45 );

						this.SetSkill( SkillName.Anatomy, 30.3, 75.0 );
						this.SetSkill( SkillName.Poisoning, 60.1, 80.0 );
						this.SetSkill( SkillName.MagicResist, 34.7, 48.3 );
						this.SetSkill( SkillName.Tactics, 37.0, 50.5 );
            this.SetSkill( SkillName.Wrestling, 50.7, 61.0 );
            
						this.SetStr( 612, 622 );
            this.SetDex( 702, 758 );
            this.SetInt( 876, 964 );
            this.SetHits( 349, 370 );
            
            this.SetDamage( 15, 18 );
					}
				}
			}

			else if ( this.Stage == 2 )
			{
				if ( defender is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)defender;

					if ( bc.Controlled != true )
					{
						KPKorpre2gainmin = 5 + ( bc.HitsMax ) / 30;
						KPKorpre2gainmax = 5 + ( bc.HitsMax ) / 20;

						this.KPKorpre2 += Utility.RandomList( KPKorpre2gainmin, KPKorpre2gainmax );
					}
				}

				if ( this.KPKorpre2 >= 250000 )
				{
					if ( this.S2 == true )
					{
						this.S2 = false;
						int va;

						va = ( this.VirtualArmor + 10 );

						this.AllowMating = true;
						this.Warmode = false;
						this.Say( "*"+ this.Name +" is now a Anlorzaglem*");
						this.Title = "a anlorzaglem";
						this.BodyValue = 71;
						this.BaseSoundID = 594;
						this.VirtualArmor = va;
						this.Stage = 3;

						this.SetDamageType( ResistanceType.Physical, 30 );
						this.SetDamageType( ResistanceType.Fire, 30 );
						this.SetDamageType( ResistanceType.Cold, 30 );
						this.SetDamageType( ResistanceType.Poison, 30 );
						this.SetDamageType( ResistanceType.Energy, 30 );

						this.SetResistance( ResistanceType.Physical, 49 );
						this.SetResistance( ResistanceType.Fire, 41 );
						this.SetResistance( ResistanceType.Cold, 45 );
						this.SetResistance( ResistanceType.Poison, 100 );
						this.SetResistance( ResistanceType.Energy, 38 );

						this.SetSkill( SkillName.Anatomy, 65.3, 95.0 );
						this.SetSkill( SkillName.Poisoning, 100.0 );
						this.SetSkill( SkillName.MagicResist, 84.5 );
						this.SetSkill( SkillName.Tactics, 97.2 );
            this.SetSkill( SkillName.Wrestling, 82.5 );
						
						this.SetStr( 1036 );
            this.SetDex( 1177 );
            this.SetInt( 1049 );
            this.SetHits( 3134 );
            
            this.SetDamage( 11, 13 );
					}
				}
			}
			base.Damage( amount, defender );
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
   		{
   			PlayerMobile player = from as PlayerMobile;
   
   			if ( player != null )
   			{
   				if ( dropped is Korpre2Dust )
   				{
 		 		    Korpre2Dust dust = ( Korpre2Dust )dropped;
   
 					int amount = ( dust.Amount * 1000 );
   
 					this.PlaySound( 665 );
 					this.KPKorpre2 += amount;

 					dust.Delete();

 		 		    this.Say( "*"+ this.Name +" absorbs the Korpre2 dust*" );
   
 					return false;
   				}
   			}
   			return base.OnDragDrop( from, dropped );
   		}
   		
   		private void MatingTarget_Callback( Mobile from, object obj ) 
        {
            if ( obj is EvolutionKorpre2 && obj is BaseCreature ) 
            { 
			    BaseCreature bc = (BaseCreature)obj;
			    EvolutionKorpre2 ed = (EvolutionKorpre2)obj;

				if ( ed.Controlled == true && ed.ControlMaster == from )
				{
				    if ( ed.Female == false )
					{
					    if ( ed.AllowMating == true )
						{
						    this.Blessed = true;
							this.Pregnant = true;

							m_Korpre2MatingTimer = new Korpre2MatingTimer( this, TimeSpan.FromDays( 0.0 ) );
							m_Korpre2MatingTimer.Start();

							m_EndMating = DateTime.Now + TimeSpan.FromDays( 0.0 );
						}
						else
						{
						    from.SendMessage( "This male Korpre is not old enough to mate!" );
						}
					}
					else
					{
					    from.SendMessage( "This Korpre is not male!" );
					}
				}
				else if ( ed.Controlled == true )
				{
				    if ( ed.Female == false )
					{
						if ( ed.AllowMating == true )
						{
						    if ( ed.ControlMaster != null )
							{
								ed.ControlMaster.SendGump( new Korpre2MatingGump( from, ed.ControlMaster, this, ed ) );
								from.SendMessage( "You ask the owner of the Korpre if they will let your female mate with their male." );
							}
                           	else
                           	{
                              	from.SendMessage( "This Korpre is wild." );
			   				}
						}
						else
						{
							from.SendMessage( "This male Korpre is not old enough to mate!" );
						}
					}
					else
					{
						from.SendMessage( "This Korpre is not male!" );
					}
				}
                else 
                { 
                    from.SendMessage( "This Korpre is wild." );
			   	}
            } 
            else 
            { 
                from.SendMessage( "That is not a Korpre!" );
			}
        }

	    /*public override bool HandlesOnSpeech( Mobile from )
        {
            if (from.InRange(this, 1))
                return true;
            else
                return false;
        }*/

        public override void OnSpeech(SpeechEventArgs args)
        {
            base.OnSpeech (args);
            if (args.Speech.ToLower() == "mate Korpre2")
            {
                if (this.Controlled == true && this.ControlMaster == args.Mobile)
                {
                    //if (this is EvolutionDragon) // Always true ;)
                    //{
                        if (this.Female == true)
                        {
                             if (this.AllowMating == true)
                            {
                                if (this.Pregnant == false)
                                {
                                    if (this.HasEgg == true)
                                    {
                                        this.HasEgg = false;
                                        this.Pregnant = false;
                                        this.Blessed = false;

                                        if (args.Mobile.Backpack != null)
                                        {
                                            args.Mobile.AddToBackpack(new Korpre2Egg());
                                            args.Mobile.SendMessage("A Korpre's egg has been placed in your backpack");
                                        }
                                        else
                                            args.Mobile.SendMessage("You broke the egg."); // If the Mobile has no backpack, the Egg is lost.
                                    }
                                    else
                                    {
                                        args.Mobile.SendMessage("Target a male Korpre to mate with this female.");
                                        args.Mobile.BeginTarget(-1, false, TargetFlags.Harmful, new TargetCallback(MatingTarget_Callback));
                                    }
                                }
                            }
                            else
                            {
                                args.Mobile.SendMessage("This female Korpre is not old enough to mate!");
                            }
                        }
                    //}
                }
            }
        } 
     
		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 1);

            writer.Write( m_AllowMating ); 
            writer.Write( m_HasEgg ); 
            writer.Write( m_Pregnant );			
            writer.Write( m_S1 );
            writer.Write( m_S2 );  
            writer.Write( (int) m_KPKorpre2 );
			writer.Write( (int) m_Stage );
			writer.WriteDeltaTime( m_EndMating );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
                    m_AllowMating = reader.ReadBool(); 
                    m_HasEgg = reader.ReadBool();  
                    m_Pregnant = reader.ReadBool();
                    m_S1 = reader.ReadBool(); 
                    m_S2 = reader.ReadBool(); 
					m_KPKorpre2 = reader.ReadInt();
					m_Stage = reader.ReadInt();
					m_EndMating = reader.ReadDeltaTime();
					m_Korpre2MatingTimer = new Korpre2MatingTimer( this, m_EndMating - DateTime.Now );
					m_Korpre2MatingTimer.Start();

					break;
		        }
				case 0:
				{
					TimeSpan durationmating = TimeSpan.FromDays( 0.0 );
					
					m_Korpre2MatingTimer = new Korpre2MatingTimer( this, durationmating );
					m_Korpre2MatingTimer.Start();
					m_EndMating = DateTime.Now + durationmating;

					break;
				}
			}
		}
	}
  
  public class Korpre2MatingTimer : Timer
	{ 
		private EvolutionKorpre2 ed;

		public Korpre2MatingTimer( EvolutionKorpre2 owner, TimeSpan duration ) : base( duration ) 
		{ 
			Priority = TimerPriority.OneSecond;
			ed = owner;
		}

		protected override void OnTick() 
		{
			ed.Blessed = false;
			ed.HasEgg = true;
			ed.Pregnant = false;
			Stop();
		}
	}
	public class Korpre2MatingGump : Gump
	{
		private Mobile m_From;
		private Mobile m_Mobile;
		private EvolutionKorpre2 m_ED1;
		private EvolutionKorpre2 m_ED2;

		public Korpre2MatingGump( Mobile from, Mobile mobile, EvolutionKorpre2 ed1, EvolutionKorpre2 ed2 ) : base( 25, 50 )
		{
			Closable = false; 
			Dragable = false; 

			m_From = from;
			m_Mobile = mobile;
			m_ED1 = ed1;
			m_ED2 = ed2;

			AddPage( 0 );

			AddBackground( 25, 10, 420, 200, 5054 );

			AddImageTiled( 33, 20, 401, 181, 2624 );
			AddAlphaRegion( 33, 20, 401, 181 );

			AddLabel( 125, 148, 1152, m_From.Name +" would like to mate "+ m_ED1.Name +" with" );
			AddLabel( 125, 158, 1152, m_ED2.Name +"." );

			AddButton( 100, 50, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddLabel( 130, 50, 1152, "Allow them to mate." );
			AddButton( 100, 75, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddLabel( 130, 75, 1152, "Do not allow them to mate." );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

			if ( info.ButtonID == 0 )
			{
				m_From.SendMessage( m_Mobile.Name +" declines your request to mate the two Korpres." );
				m_Mobile.SendMessage( "You decline "+ m_From.Name +"'s request to mate the two Korpres." );
			}
			if ( info.ButtonID == 1 )
			{
				m_ED1.Blessed = true;
				m_ED1.Pregnant = true;

				Korpre2MatingTimer mt = new Korpre2MatingTimer( m_ED1, TimeSpan.FromDays( 0.0 ) );
				mt.Start();
				m_ED1.EndMating = DateTime.Now + TimeSpan.FromDays( 0.0 );

				m_From.SendMessage( m_Mobile.Name +" accepts your request to mate the two Korpres." );
				m_Mobile.SendMessage( "You accept "+ m_From.Name +"'s request to mate the two Korpres." );
			}
		}
	}
}