using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a succubus corpse" )]
	public class Szavetra : Succubus
	{
		[Constructable]
		public Szavetra () : base()
		{
			Name = "Szavetra";

			SetStr( 627, 641 );
			SetDex( 164, 193 );
			SetInt( 566, 595 );

			SetHits( 312, 353 );

			SetDamage( 18, 28 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 83, 90 );
			SetResistance( ResistanceType.Fire, 72, 75 );
			SetResistance( ResistanceType.Cold, 43, 49 );
			SetResistance( ResistanceType.Poison, 51, 59 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.EvalInt, 90.3, 99.8 );
			SetSkill( SkillName.Magery, 100.1, 100.6 );
			SetSkill( SkillName.Meditation, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 112.2, 127.2 );
			SetSkill( SkillName.Tactics, 91.5, 92.8 );
			SetSkill( SkillName.Wrestling, 80.2, 86.4 );

			Fame = 24000;
			Karma = -24000;
		}

		public Szavetra( Serial serial ) : base( serial )
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