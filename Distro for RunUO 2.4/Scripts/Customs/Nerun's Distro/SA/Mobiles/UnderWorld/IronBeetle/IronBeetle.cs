using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Menus;
using Server.ContextMenus;
using Server.Menus.Questions; 
using Server.Targeting;
using Server.Engines.Harvest;
using Server.Regions;
using Server.Mobiles;
using System.Runtime.Serialization;
using System.IO; 
using System.Diagnostics; 
using System.Text;  
using Server.Misc; 
using System.Text.RegularExpressions; 
using Server.Commands;

namespace Server.Mobiles
{
    [CorpseName("an Iron beetle corpse")]
    public class IronBeetle : BaseCreature
    {
        private static int[] m_MountainAndCaveTiles = new int[]
		{
			113, 114, 115, 116, 117, 118, 119, 120,
			220, 221, 222, 223, 224, 225, 226, 227, 228, 229,
			230, 231, 236, 237, 238, 239, 240, 241, 242, 243,
			244, 245, 246, 247, 252, 253, 254, 255, 256, 257,
			258, 259, 260, 261, 262, 263, 268, 269, 270, 271,
			272, 273, 274, 275, 276, 277, 278, 279, 286, 287,
			288, 289, 290, 291, 292, 293, 294, 296, 296, 297,
			321, 322, 323, 324, 467, 468, 469, 470, 471, 472,
			473, 474, 476, 477, 478, 479, 480, 481, 482, 483,
			484, 485, 486, 487, 492, 493, 494, 495, 543, 544,
			545, 546, 547, 548, 549, 550, 551, 552, 553, 554,
			555, 556, 557, 558, 559, 560, 561, 562, 563, 564,
			565, 566, 567, 568, 569, 570, 571, 572, 573, 574,
			575, 576, 577, 578, 579, 581, 582, 583, 584, 585,
			586, 587, 588, 589, 590, 591, 592, 593, 594, 595,
			596, 597, 598, 599, 600, 601, 610, 611, 612, 613,

			1010, 1741, 1742, 1743, 1744, 1745, 1746, 1747, 1748, 1749,
			1750, 1751, 1752, 1753, 1754, 1755, 1756, 1757, 1771, 1772,
			1773, 1774, 1775, 1776, 1777, 1778, 1779, 1780, 1781, 1782,
			1783, 1784, 1785, 1786, 1787, 1788, 1789, 1790, 1801, 1802,
			1803, 1804, 1805, 1806, 1807, 1808, 1809, 1811, 1812, 1813,
			1814, 1815, 1816, 1817, 1818, 1819, 1820, 1821, 1822, 1823,
			1824, 1831, 1832, 1833, 1834, 1835, 1836, 1837, 1838, 1839,
			1840, 1841, 1842, 1843, 1844, 1845, 1846, 1847, 1848, 1849,
			1850, 1851, 1852, 1853, 1854, 1861, 1862, 1863, 1864, 1865,
			1866, 1867, 1868, 1869, 1870, 1871, 1872, 1873, 1874, 1875,
			1876, 1877, 1878, 1879, 1880, 1881, 1882, 1883, 1884, 1981,
			1982, 1983, 1984, 1985, 1986, 1987, 1988, 1989, 1990, 1991,
			1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 2000, 2001,
			2002, 2003, 2004, 2028, 2029, 2030, 2031, 2032, 2033, 2100,
			2101, 2102, 2103, 2104, 2105,

			0x453B, 0x453C, 0x453D, 0x453E, 0x453F, 0x4540, 0x4541,
			0x4542, 0x4543, 0x4544, 0x4545, 0x4546, 0x4547, 0x4548,
			0x4549, 0x454A, 0x454B, 0x454C, 0x454D, 0x454E, 0x454F
		};

        public bool m_Mine;
        private DateTime m_NextUse;
        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime NextUse { get { return m_NextUse; } set { m_NextUse = value; } }

        public override bool SubdueBeforeTame { get { return true; } }

        [Constructable]
        public IronBeetle()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an Iron beetle";
            Body = 714;
            BaseSoundID = 397;

            SetStr(760, 886);
            SetDex(66, 75);
            SetInt(36, 51);

            SetHits(765, 880);
            SetMana(36, 51);

            SetDamage(15, 20);

            SetDamageType(ResistanceType.Physical, 90);

            SetResistance(ResistanceType.Physical, 55, 65);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 20, 30);
            SetResistance(ResistanceType.Poison, 20, 40);
            SetResistance(ResistanceType.Energy, 45, 55);

            SetSkill(SkillName.Anatomy, 80, 89);
            SetSkill(SkillName.MagicResist, 120, 129);
            SetSkill(SkillName.Tactics, 82.6, 97);
            SetSkill(SkillName.Wrestling, 94.8, 108);
            SetSkill(SkillName.Blacksmith, 120.0);
            SetSkill(SkillName.Mining, 70);
            Skills.Mining.Cap = 120;

            Fame = 40000;
            Karma = -40000;

            VirtualArmor = 38;

            Tamable = true;
            ControlSlots = 4;
            MinTameSkill = 112.1;

            Container pack = Backpack;
            //if (pack != null) pack.Delete();
            pack = new StrongBackpack();
            pack.Movable = false;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
            AddLoot(LootPack.Gems);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

//            if (Utility.RandomDouble() < 0.1)
//                c.DropItem(new UndamagedIronBeetleScale());

            if (Utility.RandomDouble() < 0.15)
                c.DropItem(new IBShovel());

        }

        public override bool CanRummageCorpses { get { return true; } }
        public override int Meat { get { return 1; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override DeathMoveResult GetInventoryMoveResultFor(Item item) { return DeathMoveResult.MoveToCorpse; }
        public override bool AllowEquipFrom(Mobile from) { return (ControlMaster != null && ControlMaster == from); }
        public override bool CanBeRenamedBy(Mobile from) { return (ControlMaster != null && ControlMaster == from); }
        public override bool CanPaperdollBeOpenedBy(Mobile from) { return false; }
        public override bool CheckNonlocalDrop(Mobile from, Item item, Item target) { return PackAnimal.CheckAccess(this, from); }
        public override bool CheckNonlocalLift(Mobile from, Item item) { return PackAnimal.CheckAccess(this, from); }
       
        public override bool OnBeforeDeath()
        {
            if (!base.OnBeforeDeath()) return false;
            PackAnimal.CombineBackpacks(this);
            return base.OnBeforeDeath();
        }

        public override bool IsSnoop(Mobile from)
        {
            if (PackAnimal.CheckAccess(this, from)) return false;
            return base.IsSnoop(from);
        }

        public override bool OnDragDrop(Mobile from, Item item)
        {
            this.Skills.Mining.Cap = 120;
            if (CheckFeed(from, item)) return true;
            if (PackAnimal.CheckAccess(this, from))
            {
                if (item is BaseOre)
                {
                    BaseOre m_Ore = item as BaseOre;

                    int toConsume = m_Ore.Amount;

                    if (toConsume > 30000)
                    {
                        toConsume = 30000;
                    }
                    else if (m_Ore.Amount < 2)
                    {
                        from.SendLocalizedMessage(501989); // You burn away the impurities but are left with no useable metal.
                        m_Ore.Delete();
                        return true;
                    }

                    BaseIngot ingot = m_Ore.GetIngot();
                    ingot.Amount = toConsume * 2;

                    m_Ore.Consume(toConsume);

                    this.Hue = m_Ore.Hue;

                    from.PlaySound(0x57);

                    AddToBackpack(item);
                    AddToBackpack(ingot);


                    return true;
                }
            }

            return base.OnDragDrop(from, item);
        }

        public override void OnSpeech(SpeechEventArgs args)
        {
            string said = args.Speech.ToLower();
            Mobile from = args.Mobile;
            string bmine = this.Name + " " + "mine";
            string bmine2 = bmine.ToLower();
            string bbreak = this.Name + " " + "takebreak";
            string bbreak2 = bbreak.ToLower();
            string bbreak3 = this.Name + " " + "take break";
            string bbreak4 = bbreak3.ToLower();
            if (said == this.Name.ToLower())
            {
                if (from == this.ControlMaster)
                {
                    this.Say("Yes?");
                }
                else
                {
                   
                        this.Say("You are not my master.");
                       
                    }
                }
            
            else if ((said == "mine" || said == bmine2) && from == this.ControlMaster) { m_Mine = true; ControlOrder = OrderType.Stay; }
            else if ((said == "takebreak" || said == "take break" || said == bbreak2 || said == bbreak4) && from == this.ControlMaster)  { m_Mine = false; ControlOrder = OrderType.Follow; this.Hue = 0; }
            base.OnSpeech(args);
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            PackAnimal.GetContextMenuEntries(this, from, list);
        }

        public override void OnThink()
        {
            if (this.Alive && this.m_Mine && this.m_NextUse <= DateTime.Now)
            {
                Container backpack = this.Backpack;
                 
                if ( backpack == null) { m_NextUse = DateTime.Now + TimeSpan.FromSeconds(2.5); base.OnThink(); return; }
                if ( backpack.TotalWeight > (backpack.MaxWeight - 100) ) { this.Say("I'm full !! I'm AutoConverting Ore in Ingot !"); m_Mine = true; this.Hue = 0; ControlOrder = OrderType.Stay; ConvertOretoIngot( this );  base.OnThink(); return; } 
               
                Shovel shovel1 = (Shovel)backpack.FindItemByType(typeof(Shovel));
		Pickaxe shovel2 = (Pickaxe)backpack.FindItemByType(typeof(Pickaxe));
                IBShovel shovel3 = (IBShovel)backpack.FindItemByType(typeof(IBShovel));
		
                
                if (shovel1 != null)
                { 
                    if (this.DoDisposeOre(this.X, this.Y, this.Z, this.Map, this))
                    {
                        
                        shovel1.UsesRemaining -= 1;
                        if (shovel1 != null && !shovel1.Deleted && shovel1.UsesRemaining <= 0) shovel1.Delete();
                    }
                }
               
                if (shovel2 != null)
                { 
                    if (this.DoDisposeOre(this.X, this.Y, this.Z, this.Map, this))
                    {
                        
                        shovel2.UsesRemaining -= 1;
                        if (shovel2 != null && !shovel2.Deleted && shovel2.UsesRemaining <= 0) shovel2.Delete();
                    }
                }
		
		if (shovel3 != null)
                { 
                    if (this.DoDisposeOre(this.X, this.Y, this.Z, this.Map, this))
                    {
                        
                        shovel3.UsesRemaining -= 1;
                        if (shovel3 != null && !shovel3.Deleted && shovel3.UsesRemaining <= 0) shovel3.Delete();
                    }
                }

                m_NextUse = DateTime.Now + TimeSpan.FromSeconds(2.5);

                if ( (shovel1 == null) && (shovel2 == null) && (shovel3 == null) )
                {
                     this.Say("I require a tool to dig"); 
                     m_Mine = false;
                     this.Hue = 0;
                     ControlOrder = OrderType.Follow;                    
                }

                IronIngot iron1 = (IronIngot)backpack.FindItemByType(typeof(IronIngot));
                if( iron1 is BaseIngot )
                {
                    BaseIngot m_Ore1 = iron1 as BaseIngot;
                
                    if (m_Ore1.Amount > 1000)
                    {
                        SendIronIngot( this );
                    }
                }

                DullCopperIngot dull1 = (DullCopperIngot)backpack.FindItemByType(typeof(DullCopperIngot));
                if( dull1 is BaseIngot )
                {
                    BaseIngot m_Ore2 = dull1 as BaseIngot;
                
                    if (m_Ore2.Amount > 1000)
                    {
                        SendDullCopperIngot( this );
                    }
                }

                ShadowIronIngot shad1 = (ShadowIronIngot)backpack.FindItemByType(typeof(ShadowIronIngot));
                if( shad1 is BaseIngot )
                {
                    BaseIngot m_Ore3 = shad1 as BaseIngot;
                
                    if (m_Ore3.Amount > 1000)
                    {
                        SendShadowIronIngot( this );
                    }
                }

                BronzeIngot bron1 = (BronzeIngot)backpack.FindItemByType(typeof(BronzeIngot));
                if( bron1 is BaseIngot )
                {
                    BaseIngot m_Ore4 = bron1 as BaseIngot;
                
                    if (m_Ore4.Amount > 1000)
                    {
                        SendBronzeIngot( this );
                    }
                }

                GoldIngot gold1 = (GoldIngot)backpack.FindItemByType(typeof(GoldIngot));
                if( gold1 is BaseIngot )
                {
                    BaseIngot m_Ore5 = gold1 as BaseIngot;
                
                    if (m_Ore5.Amount > 1000)
                    {
                        SendGoldIngot( this );
                    }
                }  
    
                CopperIngot copp1 = (CopperIngot)backpack.FindItemByType(typeof(CopperIngot));
                if( copp1 is BaseIngot )
                {
                    BaseIngot m_Ore6 = copp1 as BaseIngot;
                
                    if (m_Ore6.Amount > 1000)
                    {
                        SendCopperIngot( this );
                    }
                }

                AgapiteIngot agap1 = (AgapiteIngot)backpack.FindItemByType(typeof(AgapiteIngot));
                if( agap1 is BaseIngot )
                {
                    BaseIngot m_Ore7 = agap1 as BaseIngot;
                
                    if (m_Ore7.Amount > 1000)
                    {
                        SendAgapiteIngot( this );
                    }
                }    
                
                VeriteIngot veri1 = (VeriteIngot)backpack.FindItemByType(typeof(VeriteIngot));
                if( veri1 is BaseIngot )
                {
                    BaseIngot m_Ore8 = veri1 as BaseIngot;
                
                    if (m_Ore8.Amount > 1000)
                    {
                        SendVeriteIngot( this );
                    }
                }                

                ValoriteIngot valo1 = (ValoriteIngot)backpack.FindItemByType(typeof(ValoriteIngot));
                if( valo1 is BaseIngot )
                {
                    BaseIngot m_Ore9 = valo1 as BaseIngot;
                
                    if (m_Ore9.Amount > 1000)
                    {
                        SendValoriteIngot( this );
                    }
                }
    
               
            }
            base.OnThink();
        }

        public bool DoDisposeOre(int x, int y, int z, Map map, Mobile from)
        {
            if (!IsMiningTile(x, y, map))
            {
                this.Say("I can not mine here !"); 
                m_Mine = false;
                this.Hue = 0;
                ControlOrder = OrderType.Follow;  
                return false;
            }

            HarvestBank bank = Mining.System.OreAndStone.GetBank(map, x, y);

            if (bank == null)
            {
                this.Say("I can not mine here !");
                m_Mine = false;
                this.Hue = 0;
                ControlOrder = OrderType.Follow;  
                return false;
            }

            if (bank.Current <= 0) 
            {
                this.Say("No Ore remains !"); 
                m_Mine = false;
                this.Hue = 0;
                ControlOrder = OrderType.Follow;  
                return false;
            }

            HarvestVein vein = bank.DefaultVein;

            if (vein == null)
            {
                this.Say("I can not mine here !");
                m_Mine = false;
                this.Hue = 0;
                ControlOrder = OrderType.Follow;  
                return false;
            }

            HarvestDefinition def = Mining.System.OreAndStone;
            HarvestResource res = vein.PrimaryResource;
            BaseOre ore = Mining.System.Construct(res.Types[0], null) as BaseOre;

            if (ore == null) 
            {
                this.Say("I can not mine here !");
                m_Mine = false;
                this.Hue = 0;
                ControlOrder = OrderType.Follow;  
                return false;
            } 

            if (ore.Resource > CraftResource.Iron)
            {
                double minskill = 0.0;
                double minskill2 = 0.0;
                double maxskill = 0.0;
                double skillbase = this.Skills.Mining.Base;
                
                switch (ore.Resource)
                {
                    case CraftResource.Iron: { minskill = 00.0; minskill2 = 00.0; maxskill = 100.0; } break;
                    case CraftResource.DullCopper: { minskill = 60.0; minskill2 = 25.0; maxskill = 105.0; } break;
                    case CraftResource.ShadowIron: { minskill = 65.0; minskill2 = 30.0; maxskill = 110.0; } break;
                    case CraftResource.Copper: { minskill = 70.0; minskill2 = 35.0; maxskill = 115.0; } break;
                    case CraftResource.Gold: { minskill = 75.0; minskill2 = 40.0; maxskill = 120.0; } break;
                    case CraftResource.Agapite: { minskill = 80.0; minskill2 = 45.0; maxskill = 120.0; } break;
                    case CraftResource.Verite: { minskill = 85.0; minskill2 = 50.0; maxskill = 120.0; } break;
                    case CraftResource.Valorite: { minskill = 90.0; minskill2 = 55.0; maxskill = 120.0; } break;
                }

                if (Utility.RandomDouble() <= 0.30 || skillbase < minskill) { ore = new IronOre(); minskill = 00.0; minskill2 = 00.0; maxskill = 100.0; }
                if (!(from.CheckSkill(SkillName.Mining, minskill2, maxskill)))
                {
                    ore.Delete();
                    return false;
                }
            }
           
            ore.Amount = (map == Map.Felucca ? 2 : 1);
            if (from != null) from.AddToBackpack(ore);
            else ore.Delete();
            bank.Consume( ore.Amount, this);
            this.Hue = ore.Hue;            
            return true;
            
        }

        public void SendIronIngot( Mobile from )
        { 
            Container backpack = this.Backpack;
            from = this.ControlMaster;     
            BankBox bank1 = from.BankBox;
            IronIngot iron = (IronIngot)backpack.FindItemByType(typeof(IronIngot)); 
            if( iron is BaseIngot )
            {
                BaseIngot m_Ore1 = iron as BaseIngot;
                
                if (m_Ore1.Amount > 1000)
                {
                
                  PlaceItemIn( bank1, 18, 169, iron );
                }
            }   
        }  
     
        public void SendDullCopperIngot( Mobile from )
        { 
            Container backpack = this.Backpack;
            from = this.ControlMaster;     
            BankBox bank1 = from.BankBox;
            DullCopperIngot dull = (DullCopperIngot)backpack.FindItemByType(typeof(DullCopperIngot)); 
            if( dull is BaseIngot )
            {
                BaseIngot m_Ore2 = dull as BaseIngot;
                
                if (m_Ore2.Amount > 1000)
                {
                
                  PlaceItemIn( bank1, 18, 169, dull);
                }
            }   
        }  
        
        public void SendShadowIronIngot( Mobile from )
        { 
            Container backpack = this.Backpack;
            from = this.ControlMaster;     
            BankBox bank1 = from.BankBox;
            ShadowIronIngot shad = (ShadowIronIngot)backpack.FindItemByType(typeof(ShadowIronIngot)); 
            if( shad is BaseIngot )
            {
                BaseIngot m_Ore3 = shad as BaseIngot;
                
                if (m_Ore3.Amount > 1000)
                {
                
                  PlaceItemIn( bank1, 18, 169, shad);
                }
            }   
        }  

        public void SendCopperIngot( Mobile from )
        { 
            Container backpack = this.Backpack;
            from = this.ControlMaster;     
            BankBox bank1 = from.BankBox;
            CopperIngot copp = (CopperIngot)backpack.FindItemByType(typeof(CopperIngot)); 
            if( copp is BaseIngot )
            {
                BaseIngot m_Ore4 = copp as BaseIngot;
                
                if (m_Ore4.Amount > 1000)
                {
                
                  PlaceItemIn( bank1, 18, 169, copp );
                }
            }   
        }  
     
        public void SendGoldIngot( Mobile from )
        { 
            Container backpack = this.Backpack;
            from = this.ControlMaster;   
            BankBox bank1 = from.BankBox;
            GoldIngot gold = (GoldIngot)backpack.FindItemByType(typeof(GoldIngot)); 
            if( gold is BaseIngot )
            {
                BaseIngot m_Ore5 = gold as BaseIngot;
                
                if (m_Ore5.Amount > 1000)
                {
                
                  PlaceItemIn( bank1, 18, 169, gold);
                }
            }   
        }  

        public void SendBronzeIngot( Mobile from )
        { 
            Container backpack = this.Backpack;
            from = this.ControlMaster;     
            BankBox bank1 = from.BankBox;
            BronzeIngot bron = (BronzeIngot)backpack.FindItemByType(typeof(BronzeIngot)); 
            if( bron is BaseIngot )
            {
                BaseIngot m_Ore6 = bron as BaseIngot;
                
                if (m_Ore6.Amount > 1000)
                {
                
                  PlaceItemIn( bank1, 18, 169, bron);
                }
            }   
        }  

        public void SendAgapiteIngot( Mobile from )
        { 
            Container backpack = this.Backpack;
            from = this.ControlMaster;     
            BankBox bank1 = from.BankBox;
            AgapiteIngot agap = (AgapiteIngot)backpack.FindItemByType(typeof(AgapiteIngot)); 
            if( agap is BaseIngot )
            {
                BaseIngot m_Ore7 = agap as BaseIngot;
                
                if (m_Ore7.Amount > 1000)
                {
                
                  PlaceItemIn( bank1, 18, 169, agap);
                }
            }   
        }  

         public void SendVeriteIngot( Mobile from )
        { 
            Container backpack = this.Backpack;
            from = this.ControlMaster;     
            BankBox bank1 = from.BankBox;
            VeriteIngot veri = (VeriteIngot)backpack.FindItemByType(typeof(VeriteIngot)); 
            if( veri is BaseIngot )
            {
                BaseIngot m_Ore8 = veri as BaseIngot;
                
                if (m_Ore8.Amount > 1000)
                {
                
                  PlaceItemIn( bank1, 18, 169, veri);
                }
            }   
        }  

        public void SendValoriteIngot( Mobile from )
        { 
            Container backpack = this.Backpack;
            from = this.ControlMaster;     
            BankBox bank1 = from.BankBox;
            ValoriteIngot valo = (ValoriteIngot)backpack.FindItemByType(typeof(ValoriteIngot)); 
            if( valo is BaseIngot )
            {
                BaseIngot m_Ore9 = valo as BaseIngot;
                
                if (m_Ore9.Amount > 1000)
                {
                
                  PlaceItemIn( bank1, 18, 169, valo);
                }
            }   
        }  
 
        public void ConvertOretoIngot( Mobile from )
        {
              this.Skills.Mining.Cap = 120;
              Container backpack = this.Backpack;
              BankBox bank = from.BankBox;   
              IronOre item = (IronOre)backpack.FindItemByType(typeof(IronOre));
                         
              if( item is BaseOre )
              {
                    BaseOre m_Ore = item as BaseOre;

                    int toConsume = m_Ore.Amount;

                    if (toConsume > 30000)
                    {
                        toConsume = 30000;
                    }
                    else if (m_Ore.Amount < 2)
                    {
                        m_Ore.Delete();
                    }

                    BaseIngot ingot = m_Ore.GetIngot();
                    ingot.Amount = toConsume * 2;

                    m_Ore.Consume(toConsume);

                    this.PlaySound(0x57);

                    this.AddToBackpack(item);
                    this.AddToBackpack(ingot);

              } 
 
              ShadowIronOre item1 = (ShadowIronOre)backpack.FindItemByType(typeof(ShadowIronOre));
                         
              if( item1 is BaseOre )
              {
                    BaseOre m_Ore = item1 as BaseOre;

                    int toConsume = m_Ore.Amount;

                    if (toConsume > 30000)
                    {
                        toConsume = 30000;
                    }
                    else if (m_Ore.Amount < 2)
                    {
                        m_Ore.Delete();
                    }

                    BaseIngot ingot = m_Ore.GetIngot();
                    ingot.Amount = toConsume * 2;

                    m_Ore.Consume(toConsume);

                    this.PlaySound(0x57);

                    this.AddToBackpack(item);
                    this.AddToBackpack(ingot);                 
              }  

              BronzeOre item2 = (BronzeOre)backpack.FindItemByType(typeof(BronzeOre));
                         
              if( item2 is BaseOre )
              {
                    BaseOre m_Ore = item2 as BaseOre;

                    int toConsume = m_Ore.Amount;

                    if (toConsume > 30000)
                    {
                        toConsume = 30000;
                    }
                    else if (m_Ore.Amount < 2)
                    {
                        m_Ore.Delete();
                    }

                    BaseIngot ingot = m_Ore.GetIngot();
                    ingot.Amount = toConsume * 2;

                    m_Ore.Consume(toConsume);

                    this.PlaySound(0x57);

                    this.AddToBackpack(item);
                    this.AddToBackpack(ingot);
              }  

              CopperOre item3 = (CopperOre)backpack.FindItemByType(typeof(CopperOre));
                         
              if( item3 is BaseOre )
              {
                    BaseOre m_Ore = item3 as BaseOre;

                    int toConsume = m_Ore.Amount;

                    if (toConsume > 30000)
                    {
                        toConsume = 30000;
                    }
                    else if (m_Ore.Amount < 2)
                    {
                        m_Ore.Delete();
                    }

                    BaseIngot ingot = m_Ore.GetIngot();
                    ingot.Amount = toConsume * 2;

                    m_Ore.Consume(toConsume);

                    this.PlaySound(0x57);

                    this.AddToBackpack(item);
                    this.AddToBackpack(ingot);
              }    

              GoldOre item4 = (GoldOre)backpack.FindItemByType(typeof(GoldOre));
                         
              if( item4 is BaseOre )
              {
                    BaseOre m_Ore = item4 as BaseOre;

                    int toConsume = m_Ore.Amount;

                    if (toConsume > 30000)
                    {
                        toConsume = 30000;
                    }
                    else if (m_Ore.Amount < 2)
                    {
                        m_Ore.Delete();
                    }

                    BaseIngot ingot = m_Ore.GetIngot();
                    ingot.Amount = toConsume * 2;

                    m_Ore.Consume(toConsume);

                    this.PlaySound(0x57);

                    this.AddToBackpack(item);
                    this.AddToBackpack(ingot);
              }    

              DullCopperOre item5 = (DullCopperOre)backpack.FindItemByType(typeof(DullCopperOre));
                         
              if( item5 is BaseOre )
              {
                    BaseOre m_Ore = item5 as BaseOre;

                    int toConsume = m_Ore.Amount;

                    if (toConsume > 30000)
                    {
                        toConsume = 30000;
                    }
                    else if (m_Ore.Amount < 2)
                    {
                        m_Ore.Delete();
                    }

                    BaseIngot ingot = m_Ore.GetIngot();
                    ingot.Amount = toConsume * 2;

                    m_Ore.Consume(toConsume);

                    this.PlaySound(0x57);

                    this.AddToBackpack(item);
                    this.AddToBackpack(ingot);
             }
            
             AgapiteOre item6 = (AgapiteOre)backpack.FindItemByType(typeof(AgapiteOre));
                         
              if( item6 is BaseOre )
              {
                    BaseOre m_Ore = item6 as BaseOre;

                    int toConsume = m_Ore.Amount;

                    if (toConsume > 30000)
                    {
                        toConsume = 30000;
                    }
                    else if (m_Ore.Amount < 2)
                    {
                        m_Ore.Delete();
                    }

                    BaseIngot ingot = m_Ore.GetIngot();
                    ingot.Amount = toConsume * 2;

                    m_Ore.Consume(toConsume);

                    this.PlaySound(0x57);

                    this.AddToBackpack(item);
                    this.AddToBackpack(ingot);
              }
            
              VeriteOre item7 = (VeriteOre)backpack.FindItemByType(typeof(VeriteOre));
                         
              if( item7 is BaseOre )
              {
                    BaseOre m_Ore = item7 as BaseOre;

                    int toConsume = m_Ore.Amount;

                    if (toConsume > 30000)
                    {
                        toConsume = 30000;
                    }
                    else if (m_Ore.Amount < 2)
                    {
                        m_Ore.Delete();
                    }

                    BaseIngot ingot = m_Ore.GetIngot();
                    ingot.Amount = toConsume * 2;

                    m_Ore.Consume(toConsume);

                    this.PlaySound(0x57);

                    this.AddToBackpack(item);
                    this.AddToBackpack(ingot);
              }

              ValoriteOre item8 = (ValoriteOre)backpack.FindItemByType(typeof(ValoriteOre));
                         
              if( item8 is BaseOre )
              {
                    BaseOre m_Ore = item8 as BaseOre;

                    int toConsume = m_Ore.Amount;

                    if (toConsume > 30000)
                    {
                        toConsume = 30000;
                    }
                    else if (m_Ore.Amount < 2)
                    {
                        m_Ore.Delete();
                    }

                    BaseIngot ingot = m_Ore.GetIngot();
                    ingot.Amount = toConsume * 2;

                    m_Ore.Consume(toConsume);

                    this.PlaySound(0x57);

                    this.AddToBackpack(item);
                    this.AddToBackpack(ingot);
             } 
        } 

        private bool IsMiningTile(int X, int Y, Map map)
        {
            LandTile list = map.Tiles.GetLandTile( X, Y );
            
            for (int l = 0; l < m_MountainAndCaveTiles.Length; l++)
            {
              if (m_MountainAndCaveTiles[l] == list.ID) return true;
            }
            return false;
        }

        private static void PlaceItemIn( Container bank1, int x, int y, Item item ) 
        { 
           
           bank1.AddItem( item ); 
           item.Location = new Point3D( x, y, 0 ); 
        } 
  
        public IronBeetle(Serial serial): base(serial)
        {
        }
            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write((int)0);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();
            }
        }
    }