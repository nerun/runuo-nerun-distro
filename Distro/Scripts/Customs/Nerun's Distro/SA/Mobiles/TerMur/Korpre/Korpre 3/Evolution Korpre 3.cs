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
	public class EvolutionKorpre3 : BaseCreature
	{
		private Timer m_Korpre3MatingTimer;
		private DateTime m_EndMating;
		
		public int m_Stage;
		public int m_KPKorpre3;
		
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
		public int KPKorpre3
		{
			get{ return m_KPKorpre3; }
			set{ m_KPKorpre3 = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Stage
		{
			get{ return m_Stage; }
			set{ m_Stage = value; }
		}

		[Constructable]
		public EvolutionKorpre3() : this( "a Korpre" )
		{
		}

		[Constructable]
		public EvolutionKorpre3( string name ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
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

			PackItem( new Korpre3Dust( 1 ) );
		}
		
		public EvolutionKorpre3(Serial serial) : base(serial)
		{
		}
		
		public override void Damage( int amount, Mobile defender )
		
		{

			int KPKorpre3gainmin, KPKorpre3gainmax;

			if ( this.Stage == 1 )
			{
				if ( defender is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)defender;

					if ( bc.Controlled != true )
					{
						KPKorpre3gainmin = 5 + ( bc.HitsMax ) / 10;
						KPKorpre3gainmax = 5 + ( bc.HitsMax ) / 5;

						this.KPKorpre3 += Utility.RandomList( KPKorpre3gainmin, KPKorpre3gainmax );
					}
				}

				if ( this.KPKorpre3 >= 50000 )
				{
					if ( this.S1 == true )
					{
						this.S1 = false;
						int va;

						va = ( this.VirtualArmor + 10 );

						this.AI = AIType.AI_Mage;
						this.Warmode = false;
						this.Say( "*"+ this.Name +" is now a Anzuanord*");
						this.Title = "a anzuanord ";
						
						this.BodyValue = 22;
            this.BaseSoundID = 377;
						this.VirtualArmor = va;
						this.Stage = 2;

						this.SetDamageType( ResistanceType.Physical, 20 );
						this.SetDamageType( ResistanceType.Fire, 20 );
						this.SetDamageType( ResistanceType.Cold, 20 );
						this.SetDamageType( ResistanceType.Poison, 20 );
						this.SetDamageType( ResistanceType.Energy, 20 );

						this.SetResistance( ResistanceType.Physical, 39 );
						this.SetResistance( ResistanceType.Fire, 15 );
						this.SetResistance( ResistanceType.Cold, 15 );
						this.SetResistance( ResistanceType.Poison, 10 );
						this.SetResistance( ResistanceType.Energy, 100 );

						this.SetSkill( SkillName.EvalInt, 85.6 );
						this.SetSkill( SkillName.Magery, 76.9 );
						this.SetSkill( SkillName.MagicResist, 47.5 );
						this.SetSkill( SkillName.Tactics, 85.6 );
            this.SetSkill( SkillName.Wrestling, 41.9 );
            
						this.SetStr( 705 );
            this.SetDex( 909 );
            this.SetInt( 983 );
            this.SetHits( 180 );
            
            this.SetDamage( 8, 10 );
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
						KPKorpre3gainmin = 5 + ( bc.HitsMax ) / 30;
						KPKorpre3gainmax = 5 + ( bc.HitsMax ) / 20;

						this.KPKorpre3 += Utility.RandomList( KPKorpre3gainmin, KPKorpre3gainmax );
					}
				}

				if ( this.KPKorpre3 >= 250000 )
				{
					if ( this.S2 == true )
					{
						this.S2 = false;
						int va;

						va = ( this.VirtualArmor + 10 );

						this.AllowMating = true;
						this.Warmode = false;
						this.Say( "*"+ this.Name +" is now a Vasanord*");
						this.Title = "a vasanord";
						this.BodyValue = 780;
						this.BaseSoundID = 0x482;
						this.VirtualArmor = va;
						this.Stage = 3;

						this.SetDamageType( ResistanceType.Physical, 30 );
						this.SetDamageType( ResistanceType.Fire, 30 );
						this.SetDamageType( ResistanceType.Cold, 30 );
						this.SetDamageType( ResistanceType.Poison, 30 );
						this.SetDamageType( ResistanceType.Energy, 30 );

						this.SetResistance( ResistanceType.Physical, 20, 47 );
						this.SetResistance( ResistanceType.Fire, 20, 35 );
						this.SetResistance( ResistanceType.Cold, 20, 33 );
						this.SetResistance( ResistanceType.Poison, 100 );
						this.SetResistance( ResistanceType.Energy, 20, 29 );

						this.SetSkill( SkillName.Magery, 100.0 );
						this.SetSkill( SkillName.MagicResist, 100.0 );
						this.SetSkill( SkillName.Tactics, 70.0 );
            this.SetSkill( SkillName.Wrestling, 80.0 );
						
						this.SetStr( 880 );
            this.SetDex( 49 );
            this.SetInt( 40 );
            this.SetHits( 5079 );
            
            this.SetDamage( 10, 23 );
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
   				if ( dropped is Korpre3Dust )
   				{
 		 		    Korpre3Dust dust = ( Korpre3Dust )dropped;
   
 					int amount = ( dust.Amount * 1000 );
   
 					this.PlaySound( 665 );
 					this.KPKorpre3 += amount;

 					dust.Delete();

 		 		    this.Say( "*"+ this.Name +" absorbs the Korpre3 dust*" );
   
 					return false;
   				}
   			}
   			return base.OnDragDrop( from, dropped );
   		}
   		
   		private void MatingTarget_Callback( Mobile from, object obj ) 
        {
            if ( obj is EvolutionKorpre3 && obj is BaseCreature ) 
            { 
			    BaseCreature bc = (BaseCreature)obj;
			    EvolutionKorpre3 ed = (EvolutionKorpre3)obj;

				if ( ed.Controlled == true && ed.ControlMaster == from )
				{
				    if ( ed.Female == false )
					{
					    if ( ed.AllowMating == true )
						{
						    this.Blessed = true;
							this.Pregnant = true;

							m_Korpre3MatingTimer = new Korpre3MatingTimer( this, TimeSpan.FromDays( 0.0 ) );
							m_Korpre3MatingTimer.Start();

							m_EndMating = DateTime.Now + TimeSpan.FromDays( 0.0 );
						}
						else
						{
						    from.SendMessage( "This male Korpre3 is not old enough to mate!" );
						}
					}
					else
					{
					    from.SendMessage( "This Korpre3 is not male!" );
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
								ed.ControlMaster.SendGump( new Korpre3MatingGump( from, ed.ControlMaster, this, ed ) );
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
            if (args.Speech.ToLower() == "mate Korpre3")
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
                                            args.Mobile.AddToBackpack(new Korpre3Egg());
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
                                args.Mobile.SendMessage("This female Hen is not old enough to mate!");
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
            writer.Write( (int) m_KPKorpre3 );
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
					m_KPKorpre3 = reader.ReadInt();
					m_Stage = reader.ReadInt();
					m_EndMating = reader.ReadDeltaTime();
					m_Korpre3MatingTimer = new Korpre3MatingTimer( this, m_EndMating - DateTime.Now );
					m_Korpre3MatingTimer.Start();

					break;
		        }
				case 0:
				{
					TimeSpan durationmating = TimeSpan.FromDays( 0.0 );
					
					m_Korpre3MatingTimer = new Korpre3MatingTimer( this, durationmating );
					m_Korpre3MatingTimer.Start();
					m_EndMating = DateTime.Now + durationmating;

					break;
				}
			}
		}
	}
  
  public class Korpre3MatingTimer : Timer
	{ 
		private EvolutionKorpre3 ed;

		public Korpre3MatingTimer( EvolutionKorpre3 owner, TimeSpan duration ) : base( duration ) 
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
	public class Korpre3MatingGump : Gump
	{
		private Mobile m_From;
		private Mobile m_Mobile;
		private EvolutionKorpre3 m_ED1;
		private EvolutionKorpre3 m_ED2;

		public Korpre3MatingGump( Mobile from, Mobile mobile, EvolutionKorpre3 ed1, EvolutionKorpre3 ed2 ) : base( 25, 50 )
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

				Korpre3MatingTimer mt = new Korpre3MatingTimer( m_ED1, TimeSpan.FromDays( 0.0 ) );
				mt.Start();
				m_ED1.EndMating = DateTime.Now + TimeSpan.FromDays( 0.0 );

				m_From.SendMessage( m_Mobile.Name +" accepts your request to mate the two Korpres." );
				m_Mobile.SendMessage( "You accept "+ m_From.Name +"'s request to mate the two Korpres." );
			}
		}
	}
}