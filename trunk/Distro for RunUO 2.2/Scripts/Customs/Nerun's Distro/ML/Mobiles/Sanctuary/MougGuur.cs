using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an ettins corpse" )]
	public class MougGuur : Ettin
	{
		[Constructable]
		public MougGuur() : base()
		{
			Name = "Moug-Guur";

			SetStr( 556, 560 );
			SetDex( 84, 88 );
			SetInt( 61, 73 );

			SetDamage( 12, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 61, 65 );
			SetResistance( ResistanceType.Fire, 18, 19 );
			SetResistance( ResistanceType.Cold, 41, 46 );
			SetResistance( ResistanceType.Poison, 24 );
			SetResistance( ResistanceType.Energy, 19, 25 );

			SetSkill( SkillName.MagicResist, 70.2, 73.7 );
			SetSkill( SkillName.Tactics, 80.8, 81.7 );
			SetSkill( SkillName.Wrestling, 93.9, 99.4 );

			Fame = 3000;
			Karma = -3000;
		}

		public MougGuur( Serial serial ) : base( serial )
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