using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a lady jennifyr corpse" )]
	public class LadyJennifyr : BaseCreature
	{
		[Constructable]
		public LadyJennifyr() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.015, 0.075 )
		{
			Name = "a lady jennifyr";
			Hue = 0x76D;
			Body = 0x93;
			BaseSoundID = 0x1C3;

			SetStr( 208, 309 );
			SetDex( 91, 118 );
			SetInt( 44, 101 );

			SetHits( 1113, 1285 );

			SetDamage( 15, 25 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Cold, 60 );

			SetResistance( ResistanceType.Physical, 56, 65 );
			SetResistance( ResistanceType.Fire, 41, 49 );
			SetResistance( ResistanceType.Cold, 71, 80 );
			SetResistance( ResistanceType.Poison, 41, 50 );
			SetResistance( ResistanceType.Energy, 50, 58 );

			SetSkill( SkillName.Wrestling, 127.9, 137.1 );
			SetSkill( SkillName.Tactics, 128.4, 141.9 );
			SetSkill( SkillName.MagicResist, 102.1, 119.5 );
			SetSkill( SkillName.Anatomy, 129.0, 137.5 );
			
			AddItem( new PlateLegs() );
		}
		
		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosUltraRich, 4 );
		}
		
		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			
			if ( m_Table == null )
				m_Table = new Hashtable();
		
			if ( Combatant != null && m_Table[ Combatant ] == null )
			{
				ResistanceMod mod = new ResistanceMod( ResistanceType.Fire, -10 );
				Combatant.AddResistanceMod( mod );
				m_Table[ Combatant ] = mod;
				Timer.DelayCall( TimeSpan.FromSeconds( 30 ), new TimerStateCallback( EndMod_Callback ), Combatant );
			}
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );		
/*
			if ( Utility.RandomDouble() < 0.15 )
				c.DropItem( new DisintegratingThesisNotes() );

			if ( Utility.RandomDouble() < 0.1 )
				c.DropItem( new ParrotItem() );
*/
		}
		
//OFF		public override bool GivesMinorArtifact{ get{ return true; } }
	
		public LadyJennifyr( Serial serial ) : base( serial )
		{
		}
		
		private static Hashtable m_Table;
		
		private void EndMod_Callback( object state )
		{
			if ( state is Mobile )
				RemoveResistanceMod( (Mobile) state );
		}
		
		public virtual void RemoveResistanceMod( Mobile from )
		{			
			if ( m_Table == null )
				m_Table = new Hashtable();
				
			if ( m_Table[ from ] != null )
			{
				from.RemoveResistanceMod( (ResistanceMod) m_Table[ from ] );
				m_Table[ from ] = null;
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

