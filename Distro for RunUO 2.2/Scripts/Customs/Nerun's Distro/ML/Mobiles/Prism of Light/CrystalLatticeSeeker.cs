using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "an crystal lattice seeker corpse" )] 
	public class CrystalLatticeSeeker : BaseCreature 
	{ 
		[Constructable] 
		public CrystalLatticeSeeker() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{		
			Name = "a crystal lattice seeker";
			Body = 0x7B;
			Hue = 0x47E;
			
			SetStr( 609, 843 );
			SetDex( 191, 243 );
			SetInt( 351, 458 );

			SetHits( 358, 527 );

			SetDamage( 13, 19 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Anatomy, 51.1, 74.2 );
			SetSkill( SkillName.EvalInt, 90.3, 99.8 );
			SetSkill( SkillName.Magery, 99.1, 100.0 );
			SetSkill( SkillName.Meditation, 90.1, 99.6 );
			SetSkill( SkillName.MagicResist, 90.6, 99.5 );
			SetSkill( SkillName.Tactics, 90.1, 99.5 );
			SetSkill( SkillName.Wrestling, 97.7, 100.0 );
			
			PackGem();
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosFilthyRich, 3 );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
/*
			if ( Utility.RandomDouble() < 0.3 )
				c.DropItem( new CrystallineFragments() );
				
			if ( Utility.RandomDouble() < 0.1 )
				c.DropItem( new PiecesOfCrystal() );

			if ( Utility.RandomDouble() < 0.1 )				
				c.DropItem( new ParrotItem() );
*/
		}
		
		public override int Feathers{ get{ return 100; } }		
		public override int TreasureMapLevel{ get{ return 5; } }

		public override int GetAttackSound() { return Utility.Random( 0x2F5, 2 ); }
		public override int GetDeathSound()	{ return 0x2F7;	}
		public override int GetAngerSound() { return 0x2F8; }
		public override int GetHurtSound() { return 0x2F9; }
		public override int GetIdleSound() { return 0x2FA; }	
				
		public CrystalLatticeSeeker( Serial serial ) : base( serial ) 
		{ 
		} 

		public virtual void DrainLife()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );

				m.FixedParticles( 0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist );
				m.PlaySound( 0x231 );

				m.SendMessage( "You feel the life drain out of you!" );

				int toDrain = Utility.RandomMinMax( 10, 40 );

				Hits += toDrain;
				m.Damage( toDrain, this );
			}
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