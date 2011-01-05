using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a sir patrick corpse" )]
	public class SirPatrick : BaseCreature
	{
		[Constructable]
		public SirPatrick() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.015, 0.075 )
		{
			Name = "a sir patrick";
			Hue = 0x47E;
			Body = 0x93;
			BaseSoundID = 0x1C3;

			SetStr( 208, 319 );
			SetDex( 98, 132 );
			SetInt( 45, 91 );

			SetHits( 616, 884 );

			SetDamage( 15, 25 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Cold, 60 );

			SetResistance( ResistanceType.Physical, 55, 62 );
			SetResistance( ResistanceType.Fire, 40, 48 );
			SetResistance( ResistanceType.Cold, 71, 80 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.Wrestling, 126.3, 136.5 );
			SetSkill( SkillName.Tactics, 128.5, 143.8 );
			SetSkill( SkillName.MagicResist, 102.8, 117.9 );
			SetSkill( SkillName.Anatomy, 127.5, 137.2 );
			
			AddItem( new PlateGloves() );
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 3 );
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
			if ( Utility.RandomDouble() < 0.15 )
				c.DropItem( new DisintegratingThesisNotes() );
				
			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new AssassinChest() );
*/
		}
		
//OFF		public override bool GivesMinorArtifact{ get{ return true; } }
	
		public SirPatrick( Serial serial ) : base( serial )
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

