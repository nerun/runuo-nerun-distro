using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Monstrous Interred Grizzle corpse" )]
	public class MonstrousInterredGrizzle : BaseCreature
	{
		[Constructable]
		public MonstrousInterredGrizzle() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a monstrous interred grizzle";
			Body = 0x103;			
			BaseSoundID = 589;

			SetStr( 1198, 1207 );
			SetDex( 127, 135 );
			SetInt( 595, 646 );

			SetHits( 50000 );

			SetDamage( 27, 31 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 48, 52 );
			SetResistance( ResistanceType.Fire, 77, 82 );
			SetResistance( ResistanceType.Cold, 56, 61 );
			SetResistance( ResistanceType.Poison, 32, 40 );
			SetResistance( ResistanceType.Energy, 69, 71 );

			SetSkill( SkillName.Wrestling, 112.6, 116.9 );
			SetSkill( SkillName.Tactics, 118.5, 119.2 );
			SetSkill( SkillName.MagicResist, 120 );
			SetSkill( SkillName.Anatomy, 111.0, 111.7 );
			SetSkill( SkillName.Magery, 100.0 );
			SetSkill( SkillName.EvalInt, 100 );
			SetSkill( SkillName.Meditation, 100 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 80;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 8 );
		}

		public override int Meat{ get{ return 1; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public void DrainLife()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
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

				m.SendMessage( "Your Life is Mine to feed on!" );

				int toDrain = Utility.RandomMinMax( 10, 40 );

				Hits += toDrain;
				m.Damage( toDrain, this );
			}
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

		public MonstrousInterredGrizzle( Serial serial ) : base( serial )
		{
		}		

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}