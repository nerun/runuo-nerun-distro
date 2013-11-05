using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "an elite ninja corpse" )] 
      public class EliteNinjaWarrior : BaseCreature
	{
		private DateTime m_DecayTime;
		private Timer m_Timer;

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool BardImmune { get { return true; } }
		public override bool CanRummageCorpses { get { return true; } }


		[Constructable]
		public EliteNinjaWarrior() : base( AIType.AI_Ninja, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an elite ninja";
			Body = 0x190;
			Hue = Utility.RandomSkinHue();

			SetStr( 125, 175 );
			SetDex( 175, 275 );
			SetInt( 85, 105 );

			SetHits( 250, 350 );

			SetDamage( 8, 22 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 60 );
			SetResistance( ResistanceType.Fire, 45, 65 );
			SetResistance( ResistanceType.Cold, 25, 45 );
			SetResistance( ResistanceType.Poison, 40, 60 );
			SetResistance( ResistanceType.Energy, 40, 65 );

			Fame = 5000;
			Karma = -5000;

			SetSkill( SkillName.MagicResist, 80.0, 100.0 );
			SetSkill( SkillName.Tactics, 115.0, 130.0 );
			SetSkill( SkillName.Wrestling, 95.0, 120.0 );
			SetSkill( SkillName.Anatomy, 105.0, 120.0 );
			SetSkill( SkillName.Fencing, 78.0, 100.0 );

			SetSkill( SkillName.Ninjitsu, 90.0, 120.0 );
			SetSkill( SkillName.Hiding, 100.0, 120.0 );
			SetSkill( SkillName.Stealth, 100.0, 120.0 );

			
			AddItem( new LeatherNinjaHood() );
			AddItem( new LeatherNinjaJacket() );
			AddItem( new LeatherNinjaPants() );
			AddItem( new LeatherNinjaBelt() );
			AddItem( new LeatherNinjaMitts() );
			AddItem( new NinjaTabi() );
                  
                  if( Utility.RandomDouble() < 0.33 )
				AddItem( new SmokeBomb() );

			switch ( Utility.Random( 8 ))
			{
				case 0: AddItem( new Tessen() ); break;
				case 1: AddItem( new Wakizashi() ); break;
				case 2: AddItem( new Nunchaku() ); break;
				case 3: AddItem( new Daisho() ); break;
				case 4: AddItem( new Sai() ); break;
				case 5: AddItem( new Tekagi() ); break;
				case 6: AddItem( new Kama() ); break;
				case 7: AddItem( new Katana() ); break;
			}

			Utility.AssignRandomHair( this );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			c.DropItem( new BookOfNinjitsu() );
		}

		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems, 2 );
		//}
		
			m_DecayTime = DateTime.Now + TimeSpan.FromMinutes( 10.0 );

			m_Timer = new InternalTimer( this, m_DecayTime );
			m_Timer.Start();
		}

		

		public EliteNinjaWarrior( Serial serial ) : base( serial )
		{
		}
           /* public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 4 );
		}*/
		
		

		public override void OnAfterDelete()
		{
			if ( m_Timer != null )
				m_Timer.Stop();

			base.OnAfterDelete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.WriteDeltaTime( m_DecayTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_DecayTime = reader.ReadDeltaTime();

					m_Timer = new InternalTimer( this, m_DecayTime );
					m_Timer.Start();

					break;
				}
			}
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Mob;

			public InternalTimer( Mobile mob, DateTime end ) : base( end - DateTime.Now )
			{
				m_Mob = mob;
			}

			protected override void OnTick()
			{
				m_Mob.FixedParticles( 14120, 10, 15, 5012, EffectLayer.Waist );
				m_Mob.PlaySound( 510 );
				m_Mob.Delete();
				Stop();
			}
		}
	}
}
			

			
			

			   
