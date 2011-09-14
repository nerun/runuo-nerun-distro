using Server;
using System;
using System.Collections;
using Server.Items;
using Server.Spells;
using Server.Mobiles;

namespace Server.Regions
{
    public class CustomRegion : GuardedRegion
    {               
        private RegionControl m_Controller;

        public RegionControl Controller
        {
            get { return m_Controller; }
        }

        public CustomRegion(RegionControl control): base(control.RegionName, control.Map, control.RegionPriority, control.RegionArea)
        {
            Disabled = !control.IsGuarded;
            Music = control.Music;
            m_Controller = control;
        }

        private Timer m_Timer;

        // public override bool OnDeath( Mobile m )
        public override void OnDeath(Mobile m)
        {
			//bool toreturn = true;

            if ( m != null && !m.Deleted)
            {

                if (m is PlayerMobile && m_Controller.NoPlayerItemDrop)
                {
                    if (m.Female)
                    {
                        m.FixedParticles(0x374A, 10, 30, 5013, 1153, 2, EffectLayer.Waist);
                        m.Body = 403;
                        m.Hidden = true;
                    }
                    else
                    {
                        m.FixedParticles(0x374A, 10, 30, 5013, 1153, 2, EffectLayer.Waist);
                        m.Body = 402;
                        m.Hidden = true;
                    }
                    m.Hidden = false;
                    //toreturn = false;
                }
                else if ( !(m is PlayerMobile) && m_Controller.NoNPCItemDrop)
                {
                    if (m.Female)
                    {
                        m.FixedParticles(0x374A, 10, 30, 5013, 1153, 2, EffectLayer.Waist);
                        m.Body = 403;
                        m.Hidden = true;
                    }
                    else
                    {
                        m.FixedParticles(0x374A, 10, 30, 5013, 1153, 2, EffectLayer.Waist);
                        m.Body = 402;
                        m.Hidden = true;
                    }
                    m.Hidden = false;
                    //toreturn = false;
                }
                //else
					//toreturn = true;

                // Start a 1 second timer
                // The Timer will check if they need moving, corpse deleting etc.
                m_Timer = new MovePlayerTimer(m, m_Controller);
                m_Timer.Start();

                return;
            }

            return;

        }

        private class MovePlayerTimer : Timer
        {
            private Mobile m;
            private RegionControl m_Controller;

            public MovePlayerTimer(Mobile m_Mobile, RegionControl controller)
                : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
            {
                Priority = TimerPriority.FiftyMS;
                m = m_Mobile;
                m_Controller = controller;
            }

            protected override void OnTick()
            {
                // Emptys the corpse and places items on ground
                if ( m is PlayerMobile )
                {
                    if (m_Controller.EmptyPlayerCorpse)
                    {
                        if (m != null && m.Corpse != null)
                        {
                            ArrayList corpseitems = new ArrayList(m.Corpse.Items);

                            foreach (Item item in corpseitems)
                            {
                                if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair) && (item.Layer != Layer.Mount))
                                {
                                    if ((item.LootType != LootType.Blessed))
                                    {
                                        item.MoveToWorld(m.Corpse.Location, m.Corpse.Map);
                                    }
                                }
                            }
                        }
                    }
                }
                else if ( m_Controller.EmptyNPCCorpse )
                {
                    if (m != null && m.Corpse != null)
                    {
                        ArrayList corpseitems = new ArrayList(m.Corpse.Items);

                        foreach (Item item in corpseitems)
                        {
                            if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack) && (item.Layer != Layer.Hair) && (item.Layer != Layer.FacialHair) && (item.Layer != Layer.Mount))
                            {
                                if ((item.LootType != LootType.Blessed))
                                {
                                    item.MoveToWorld(m.Corpse.Location, m.Corpse.Map);
                                }
                            }
                        }
                    }
                }

                Mobile newnpc = null;   

                // Resurrects Players
                if (m is PlayerMobile)
                {
                    if (m_Controller.ResPlayerOnDeath)
                    {
                        if (m != null)
                        {
                            m.Resurrect();
                            m.SendMessage("You have been Resurrected");
                        }
                    }
                }
                else if (m_Controller.ResNPCOnDeath)
                {
                    if (m != null && m.Corpse != null)
                    {
                        Type type = m.GetType();
                        newnpc = Activator.CreateInstance(type) as Mobile;
                        if (newnpc != null)
                        {
                            newnpc.Location = m.Corpse.Location;
                            newnpc.Map = m.Corpse.Map;
                        }
                    }
                }

                // Deletes the corpse 
                if ( m is PlayerMobile )
                {
                    if (m_Controller.DeletePlayerCorpse)
                    {
                        if (m != null && m.Corpse != null)
                        {
                            m.Corpse.Delete();
                        }
                    }
                }
                else if ( m_Controller.DeleteNPCCorpse )
                {
                    if (m != null && m.Corpse != null)
                    {
                        m.Corpse.Delete();
                    }
                }           

                // Move Mobiles
                if ( m is PlayerMobile )
                {
                    if (m_Controller.MovePlayerOnDeath)
                    {
                        if (m != null)
                        {
                            m.Map = m_Controller.MovePlayerToMap;
                            m.Location = m_Controller.MovePlayerToLoc;
                        }
                    }
                }
                else if ( m_Controller.MoveNPCOnDeath )
                {
                    if (newnpc != null)
                    {
                        newnpc.Map = m_Controller.MoveNPCToMap;
                        newnpc.Location = m_Controller.MoveNPCToLoc;
                    }
                }
                 
                Stop();

            }
        }

        public override bool IsDisabled()
        {
            if (!m_Controller.IsGuarded != Disabled)
                m_Controller.IsGuarded = !Disabled;

			return Disabled;
        }

        public override bool AllowBeneficial(Mobile from, Mobile target)
        {
            if ((!m_Controller.AllowBenefitPlayer && target is PlayerMobile) || (!m_Controller.AllowBenefitNPC && target is BaseCreature))
            {
                from.SendMessage("You cannot perform benificial acts on your target.");
                return false;
            }

            return base.AllowBeneficial(from, target);
        }

        public override bool AllowHarmful(Mobile from, Mobile target)
        {
            if ((!m_Controller.AllowHarmPlayer && target is PlayerMobile) || (!m_Controller.AllowHarmNPC && target is BaseCreature))
            {
                from.SendMessage("You cannot perform harmful acts on your target.");
                return false;
            }

            return base.AllowHarmful(from, target);
        }

        public override bool AllowHousing(Mobile from, Point3D p)
        {
            return m_Controller.AllowHousing;
        }

        public override bool AllowSpawn()
        {
            return m_Controller.AllowSpawn;
        }

        public override bool CanUseStuckMenu(Mobile m)
        {
            if (!m_Controller.CanUseStuckMenu)
                m.SendMessage("You cannot use the Stuck menu here.");
            return m_Controller.CanUseStuckMenu;
        }

        public override bool OnDamage(Mobile m, ref int Damage)
        {
            if (!m_Controller.CanBeDamaged)
            {
                m.SendMessage("You cannot be damaged here.");
            }

            return m_Controller.CanBeDamaged;
        }
        public override bool OnResurrect(Mobile m)
        {
            if (!m_Controller.CanRessurect && m.AccessLevel == AccessLevel.Player)
                m.SendMessage("You cannot ressurect here.");
            return m_Controller.CanRessurect;
        }

        public override bool OnBeginSpellCast(Mobile from, ISpell s)
        {
            if (from.AccessLevel == AccessLevel.Player)
            {
                bool restricted = m_Controller.IsRestrictedSpell(s);
                if (restricted)
                {
                    from.SendMessage("You cannot cast that spell here.");
                    return false;
                }

                //if ( s is EtherealSpell && !CanMountEthereal ) Grr, EthereealSpell is private :<
                if (!m_Controller.CanMountEthereal && ((Spell)s).Info.Name == "Ethereal Mount") //Hafta check with a name compare of the string to see if ethy
                {
                    from.SendMessage("You cannot mount your ethereal here.");
                    return false;
                }
            }

            //Console.WriteLine( m_Controller.GetRegistryNumber( s ) );

            //return base.OnBeginSpellCast( from, s );
            return true;	//Let users customize spells, not rely on weather it's guarded or not.
        }

        public override bool OnDecay(Item item)
        {
            return m_Controller.ItemDecay;
        }

        public override bool OnHeal(Mobile m, ref int Heal)
        {
            if (!m_Controller.CanHeal)
            {
                m.SendMessage("You cannot be healed here.");
            }

            return m_Controller.CanHeal;
        }

        public override bool OnSkillUse(Mobile m, int skill)
        {
            bool restricted = m_Controller.IsRestrictedSkill(skill);
            if (restricted && m.AccessLevel == AccessLevel.Player)
            {
                m.SendMessage("You cannot use that skill here.");
                return false;
            }

            return base.OnSkillUse(m, skill);
        }

        public override void OnExit(Mobile m)
        {
            if (m_Controller.ShowExitMessage)
                m.SendMessage("You have left {0}", this.Name);

            base.OnExit(m);

        }

        public override void OnEnter(Mobile m)
        {
            if (m_Controller.ShowEnterMessage)
                m.SendMessage("You have entered {0}", this.Name);

            base.OnEnter(m);
        }

        public override bool OnMoveInto(Mobile m, Direction d, Point3D newLocation, Point3D oldLocation)
        {
            if (!m_Controller.CanEnter && !this.Contains(oldLocation))
            {
                m.SendMessage("You cannot enter this area.");
                return false;
            }

            return true;
        }

        public override TimeSpan GetLogoutDelay(Mobile m)
        {
            if (m.AccessLevel == AccessLevel.Player)
                return m_Controller.PlayerLogoutDelay;

            return base.GetLogoutDelay(m);
        }

        public override bool OnDoubleClick(Mobile m, object o)
        {
            if (o is BasePotion && !m_Controller.CanUsePotions)
            {
                m.SendMessage("You cannot drink potions here.");
                return false;
            }

            if (o is Corpse)
            {
                Corpse c = (Corpse)o;

                bool canLoot;

                if (c.Owner == m)
                    canLoot = m_Controller.CanLootOwnCorpse;
                else if (c.Owner is PlayerMobile)
                    canLoot = m_Controller.CanLootPlayerCorpse;
                else
                    canLoot = m_Controller.CanLootNPCCorpse;

                if (!canLoot)
                    m.SendMessage("You cannot loot that corpse here.");

                if (m.AccessLevel >= AccessLevel.GameMaster && !canLoot)
                {
                    m.SendMessage("This is unlootable but you are able to open that with your Godly powers.");
                    return true;
                }

                return canLoot;
            }

            return base.OnDoubleClick(m, o);
        }

        public override void AlterLightLevel(Mobile m, ref int global, ref int personal)
        {
            if (m_Controller.LightLevel >= 0)
                global = m_Controller.LightLevel;
            else
                base.AlterLightLevel(m, ref global, ref personal);
        }

        /*public override bool CheckAccessibility(Item item, Mobile from)
        {

            if (item is BasePotion && !m_Controller.CanUsePotions)
            {
                from.SendMessage("You cannot drink potions here.");
                return false;
            }

            if (item is Corpse)
            {
                Corpse c = item as Corpse;

                bool canLoot;

                if (c.Owner == from)
                    canLoot = m_Controller.CanLootOwnCorpse;
                else if (c.Owner is PlayerMobile)
                    canLoot = m_Controller.CanLootPlayerCorpse;
                else
                    canLoot = m_Controller.CanLootNPCCorpse;

                if (!canLoot)
                    from.SendMessage("You cannot loot that corpse here.");

                if (from.AccessLevel >= AccessLevel.GameMaster && !canLoot)
                {
                    from.SendMessage("This is unlootable but you are able to open that with your Godly powers.");
                    return true;
                }

                return canLoot;
            }

            return base.CheckAccessibility(item, from);
        }*/

    }
}
