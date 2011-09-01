/*
INCLUDED AT v4.0 FOR RC2:

@JeremyMCC
I've toyed around with the RegionControl.cs and have a fix that seems to work.
I've tested it pretty thoroughly and haven't had any crashes and the custom go location still
seems to work fine. Here's the fix, let me know how it works for everyone.
http://www.runuo.com/community/threads/runuo-2-0-rc1-custom-regions-in-a-box-v4-0.70170/page-6#post-543450

	@oii88
	Reported JeremyMCC changes did not works.
	http://www.runuo.com/community/threads/runuo-2-0-rc1-custom-regions-in-a-box-v4-0.70170/page-6#post-543461

@Datguy
Removed Custom Go Location
If you want no Crash/error, remove CustomGo in RegionControl.cs.
You'll not have the Region Go command for the custom regions you make but you'll not have crashes either.
http://www.runuo.com/community/threads/runuo-2-0-rc1-custom-regions-in-a-box-v4-0.70170/page-8#post-543495

INCLUDED AT v4.0 FOR NERUN'S DISTRO

@Lord Dio
Custom Regions in a Box 2.0 Final: Z value fix
http://www.runuo.com/community/threads/custom-regions-in-a-box-2-0-final-z-value-fix.102663/

@Nerun
Lines 873-877
RemoveAreaGump will be refreshed after added a new area
*/
using System;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Spells;
using Server.Items;
using Server.Regions;
using System.Collections;
using Server.SkillHandlers;
using Server.Gumps;

namespace Server.Items 
{
    public enum RegionFlag : uint
    {
        None                =   0x00000000,
        AllowBenefitPlayer  =   0x00000001,
        AllowHarmPlayer     =   0x00000002,
        AllowHousing        =   0x00000004,
        AllowSpawn          =   0x00000008,

        CanBeDamaged        =   0x00000010,
        CanHeal             =   0x00000020,
        CanRessurect        =   0x00000040,
        CanUseStuckMenu     =   0x00000080,
        ItemDecay           =   0x00000100,

        ShowEnterMessage    =   0x00000200,
        ShowExitMessage     =   0x00000400,

        AllowBenefitNPC     =   0x00000800,
        AllowHarmNPC        =   0x00001000,

        CanMountEthereal    =   0x000002000,
        // ToDo: Change to "CanEnter"
        CanEnter            =   0x000004000,

        CanLootPlayerCorpse =   0x000008000,
        CanLootNPCCorpse    =   0x000010000,
        // ToDo: Change to "CanLootOwnCorpse"
        CanLootOwnCorpse    =   0x000020000,

        CanUsePotions       =   0x000040000,

        IsGuarded           =   0x000080000,

        // Obsolete, needed for old versions for DeSer.
        NoPlayerCorpses     =   0x000100000,
		NoItemDrop          =   0x000200000,
        //

        EmptyNPCCorpse      =   0x000400000,
        EmptyPlayerCorpse   =   0x000800000,
        DeleteNPCCorpse     =   0x001000000,
        DeletePlayerCorpse  =   0x002000000,
        ResNPCOnDeath       =   0x004000000,
        ResPlayerOnDeath    =   0x008000000,
        MoveNPCOnDeath      =   0x010000000,
        MovePlayerOnDeath   =   0x020000000,
        
        NoPlayerItemDrop    =   0x040000000,
        NoNPCItemDrop       =   0x080000000
    }

	public class RegionControl : Item
	{
        private static List<RegionControl> m_AllControls = new List<RegionControl>();

        public static List<RegionControl> AllControls
        {
            get { return m_AllControls; }
        }


        #region Region Flags

        private RegionFlag m_Flags;

        public RegionFlag Flags
        {
            get { return m_Flags; }
            set { m_Flags = value; }
        }
        
        public bool GetFlag(RegionFlag flag)
        {
            return ((m_Flags & flag) != 0);
        }

        public void SetFlag(RegionFlag flag, bool value)
        {
            if (value)
                m_Flags |= flag;
            else
            {     
                m_Flags &= ~flag;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowBenefitPlayer
        {
            get { return GetFlag(RegionFlag.AllowBenefitPlayer); }
            set { SetFlag(RegionFlag.AllowBenefitPlayer, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowHarmPlayer
        {
            get { return GetFlag(RegionFlag.AllowHarmPlayer); }
            set { SetFlag(RegionFlag.AllowHarmPlayer, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowHousing
        {
            get { return GetFlag(RegionFlag.AllowHousing); }
            set { SetFlag(RegionFlag.AllowHousing, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowSpawn
        {
            get { return GetFlag(RegionFlag.AllowSpawn); }
            set { SetFlag(RegionFlag.AllowSpawn, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CanBeDamaged
        {
            get { return GetFlag(RegionFlag.CanBeDamaged); }
            set { SetFlag(RegionFlag.CanBeDamaged, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CanMountEthereal
        {
            get { return GetFlag(RegionFlag.CanMountEthereal); }
            set { SetFlag(RegionFlag.CanMountEthereal, value); }
        }

        // ToDo: Change to "CanEnter"
        [CommandProperty(AccessLevel.GameMaster)]
        public bool CanEnter
        {
            get { return GetFlag(RegionFlag.CanEnter); }
            set { SetFlag(RegionFlag.CanEnter, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CanHeal
        {
            get { return GetFlag(RegionFlag.CanHeal); }
            set { SetFlag(RegionFlag.CanHeal, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CanRessurect
        {
            get { return GetFlag(RegionFlag.CanRessurect); }
            set { SetFlag(RegionFlag.CanRessurect, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CanUseStuckMenu
        {
            get { return GetFlag(RegionFlag.CanUseStuckMenu); }
            set { SetFlag(RegionFlag.CanUseStuckMenu, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ItemDecay
        {
            get { return GetFlag(RegionFlag.ItemDecay); }
            set { SetFlag(RegionFlag.ItemDecay, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowBenefitNPC
        {
            get { return GetFlag(RegionFlag.AllowBenefitNPC); }
            set { SetFlag(RegionFlag.AllowBenefitNPC, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowHarmNPC
        {
            get { return GetFlag(RegionFlag.AllowHarmNPC); }
            set { SetFlag(RegionFlag.AllowHarmNPC, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ShowEnterMessage
        {
            get { return GetFlag(RegionFlag.ShowEnterMessage); }
            set { SetFlag(RegionFlag.ShowEnterMessage, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ShowExitMessage
        {
            get { return GetFlag(RegionFlag.ShowExitMessage); }
            set { SetFlag(RegionFlag.ShowExitMessage, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CanLootPlayerCorpse
        {
            get { return GetFlag(RegionFlag.CanLootPlayerCorpse); }
            set { SetFlag(RegionFlag.CanLootPlayerCorpse, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CanLootNPCCorpse
        {
            get { return GetFlag(RegionFlag.CanLootNPCCorpse); }
            set { SetFlag(RegionFlag.CanLootNPCCorpse, value); }
        }

        // ToDo: Change to "CanLootOwnCorpse"
        [CommandProperty(AccessLevel.GameMaster)]
        public bool CanLootOwnCorpse
        {
            get { return GetFlag(RegionFlag.CanLootOwnCorpse); }
            set { SetFlag(RegionFlag.CanLootOwnCorpse, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool CanUsePotions
        {
            get { return GetFlag(RegionFlag.CanUsePotions); }
            set { SetFlag(RegionFlag.CanUsePotions, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsGuarded
        {
            get
            { return GetFlag(RegionFlag.IsGuarded); }
            set
            {
                SetFlag(RegionFlag.IsGuarded, value);
                if (m_Region != null)
                    m_Region.Disabled = !value;

                Timer.DelayCall(TimeSpan.FromSeconds(2.0), new TimerCallback(UpdateRegion));
            }
        }

        // OBSOLETE, needed for old Deser
        public bool NoPlayerCorpses
        {
            get { return GetFlag(RegionFlag.NoPlayerCorpses); }
            set { SetFlag(RegionFlag.NoPlayerCorpses, value); }
        }

        public bool NoItemDrop
        {
            get { return GetFlag(RegionFlag.NoItemDrop); }
            set { SetFlag(RegionFlag.NoItemDrop, value); }
        }
        // END OBSOLETE

        [CommandProperty(AccessLevel.GameMaster)]
        public bool EmptyNPCCorpse
        {
            get { return GetFlag(RegionFlag.EmptyNPCCorpse); }
            set { SetFlag(RegionFlag.EmptyNPCCorpse, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool EmptyPlayerCorpse
        {
            get { return GetFlag(RegionFlag.EmptyPlayerCorpse); }
            set { SetFlag(RegionFlag.EmptyPlayerCorpse, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool DeleteNPCCorpse
        {
            get { return GetFlag(RegionFlag.DeleteNPCCorpse); }
            set { SetFlag(RegionFlag.DeleteNPCCorpse, value); }
        }
        
        [CommandProperty(AccessLevel.GameMaster)]
        public bool DeletePlayerCorpse
        {
            get { return GetFlag(RegionFlag.DeletePlayerCorpse); }
            set { SetFlag(RegionFlag.DeletePlayerCorpse, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ResNPCOnDeath
        {
            get { return GetFlag(RegionFlag.ResNPCOnDeath); }
            set { SetFlag(RegionFlag.ResNPCOnDeath, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ResPlayerOnDeath
        {
            get { return GetFlag(RegionFlag.ResPlayerOnDeath); }
            set { SetFlag(RegionFlag.ResPlayerOnDeath, value); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool MoveNPCOnDeath
        {
            get { return GetFlag(RegionFlag.MoveNPCOnDeath); }
            set
            {
                if (MoveNPCToMap == null || MoveNPCToMap == Map.Internal || MoveNPCToLoc == Point3D.Zero)
                    SetFlag(RegionFlag.MoveNPCOnDeath, false);
                else
                    SetFlag(RegionFlag.MoveNPCOnDeath, value);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool MovePlayerOnDeath
        {
            get { return GetFlag(RegionFlag.MovePlayerOnDeath); }
            set
            {
                if (MovePlayerToMap == null || MovePlayerToMap == Map.Internal || MovePlayerToLoc == Point3D.Zero)
                    SetFlag(RegionFlag.MovePlayerOnDeath, false);
                else
                    SetFlag(RegionFlag.MovePlayerOnDeath, value);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool NoPlayerItemDrop
        {
            get { return GetFlag(RegionFlag.NoPlayerItemDrop); }
            set { SetFlag(RegionFlag.NoPlayerItemDrop, value); }
        }
        
        
        [CommandProperty(AccessLevel.GameMaster)]
        public bool NoNPCItemDrop
        {
            get { return GetFlag(RegionFlag.NoNPCItemDrop); }
            set { SetFlag(RegionFlag.NoNPCItemDrop, value); }
        }
        
        # endregion


        #region Region Restrictions

        private BitArray m_RestrictedSpells;
        private BitArray m_RestrictedSkills;

        public BitArray RestrictedSpells
        {
            get { return m_RestrictedSpells; }
        }

        public BitArray RestrictedSkills
        {
            get { return m_RestrictedSkills; }
        }

        # endregion


        # region Region Related Objects

        private CustomRegion m_Region;
        private Rectangle3D[] m_RegionArea;

        public CustomRegion Region
        {
            get { return m_Region; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Rectangle3D[] RegionArea
        {
            get { return m_RegionArea; }
            set { m_RegionArea = value; }
        }

        # endregion


        # region Control Properties

        private bool m_Active = true;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Active
        {
            get { return m_Active; }
            set
            {
                if (m_Active != value)
                {
                    m_Active = value;
                    UpdateRegion();
                }
            }

        }

        # endregion


        # region Region Properties

        private string m_RegionName;
        private int m_RegionPriority;
        private MusicName m_Music;
        private TimeSpan m_PlayerLogoutDelay;
        private int m_LightLevel;

        private Map m_MoveNPCToMap;
        private Point3D m_MoveNPCToLoc;
        private Map m_MovePlayerToMap;
        private Point3D m_MovePlayerToLoc;

        [CommandProperty(AccessLevel.GameMaster)]
        public string RegionName
        {
            get { return m_RegionName; }
            set 
            {
                if (Map != null && !RegionNameTaken(value))
                    m_RegionName = value;
                else if (Map != null)
                    Console.WriteLine("RegionName not changed for {0}, {1} already has a Region with the name of {2}", this, Map, value);
                else if(Map == null)
                    Console.WriteLine("RegionName not changed for {0} to {1}, it's Map value was null", this, value);

                UpdateRegion();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int RegionPriority
        {
            get { return m_RegionPriority; }
            set 
            { 
                m_RegionPriority = value;
                UpdateRegion();           
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public MusicName Music
        {
            get { return m_Music; }
            set
            {
                m_Music = value;
                UpdateRegion();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
		public TimeSpan PlayerLogoutDelay
		{
			get{ return m_PlayerLogoutDelay; }
			set
            { 
                m_PlayerLogoutDelay = value;
                UpdateRegion();
            }
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public int LightLevel
        {
            get { return m_LightLevel; }
            set 
            { 
                m_LightLevel = value;
                UpdateRegion();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Map MoveNPCToMap
        {
            get { return m_MoveNPCToMap; }
            set
            {
                if (value != Map.Internal)
                    m_MoveNPCToMap = value;
                else
                    SetFlag(RegionFlag.MoveNPCOnDeath, false);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D MoveNPCToLoc
        {
            get { return m_MoveNPCToLoc; }
            set
            {
                if (value != Point3D.Zero)
                    m_MoveNPCToLoc = value;
                else
                    SetFlag(RegionFlag.MoveNPCOnDeath, false);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Map MovePlayerToMap
        {
            get { return m_MovePlayerToMap; }
            set
            {
                if (value != Map.Internal)
                    m_MovePlayerToMap = value;
                else
                    SetFlag(RegionFlag.MovePlayerOnDeath, false);
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D MovePlayerToLoc
        {
            get { return m_MovePlayerToLoc; }
            set
            {
                if (value != Point3D.Zero)
                    m_MovePlayerToLoc = value;
                else
                    SetFlag(RegionFlag.MovePlayerOnDeath, false);
            }
        }

       // REMOVED
        /*
        private Point3D m_CustomGoLocation;

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D CustomGoLocation
        {
            get { return m_Region.GoLocation; }
            set 
            { 
                m_Region.GoLocation = value;
                m_CustomGoLocation = value;
                UpdateRegion();           
            }
        }
         */

        # endregion


        [Constructable]
		public RegionControl() : base ( 5609 )
		{
			Visible = false;
			Movable = false;
			Name = "Region Controller";

            if (m_AllControls == null)
                m_AllControls = new List<RegionControl>();
            m_AllControls.Add(this);

            m_RegionName = FindNewName("Custom Region");
            m_RegionPriority = CustomRegion.DefaultPriority;

            m_RestrictedSpells = new BitArray(SpellRegistry.Types.Length);
            m_RestrictedSkills = new BitArray(SkillInfo.Table.Length);
		}

        [Constructable]
        public RegionControl(Rectangle2D rect): base(5609)
        {
            Visible = false;
            Movable = false;
            Name = "Region Controller";

            if (m_AllControls == null)
                m_AllControls = new List<RegionControl>();
            m_AllControls.Add(this);

            m_RegionName = FindNewName("Custom Region");
            m_RegionPriority = CustomRegion.DefaultPriority;

            m_RestrictedSpells = new BitArray(SpellRegistry.Types.Length);
            m_RestrictedSkills = new BitArray(SkillInfo.Table.Length);

            Rectangle3D newrect = Server.Region.ConvertTo3D(rect);
            DoChooseArea(null, this.Map, newrect.Start, newrect.End, this);

            UpdateRegion();
        }

        [Constructable]
        public RegionControl(Rectangle3D rect): base(5609)
        {
            Visible = false;
            Movable = false;
            Name = "Region Controller";

            if (m_AllControls == null)
                m_AllControls = new List<RegionControl>();
            m_AllControls.Add(this);

            m_RegionName = FindNewName("Custom Region");
            m_RegionPriority = CustomRegion.DefaultPriority;

            m_RestrictedSpells = new BitArray(SpellRegistry.Types.Length);
            m_RestrictedSkills = new BitArray(SkillInfo.Table.Length);

            DoChooseArea(null, this.Map, rect.Start, rect.End, this);

            UpdateRegion();
        }

        [Constructable]
        public RegionControl(Rectangle2D[] rects): base(5609)
        {
            Visible = false;
            Movable = false;
            Name = "Region Controller";

            if (m_AllControls == null)
                m_AllControls = new List<RegionControl>();
            m_AllControls.Add(this);

            m_RegionName = FindNewName("Custom Region");
            m_RegionPriority = CustomRegion.DefaultPriority;

            m_RestrictedSpells = new BitArray(SpellRegistry.Types.Length);
            m_RestrictedSkills = new BitArray(SkillInfo.Table.Length);

            foreach (Rectangle2D rect2d in rects)
            {
                Rectangle3D newrect = Server.Region.ConvertTo3D(rect2d);
                DoChooseArea(null, this.Map, newrect.Start, newrect.End, this);
            }

            UpdateRegion();
        }

        [Constructable]
        public RegionControl(Rectangle3D[] rects): base(5609)
        {
            Visible = false;
            Movable = false;
            Name = "Region Controller";

            if (m_AllControls == null)
                m_AllControls = new List<RegionControl>();
            m_AllControls.Add(this);

            m_RegionName = FindNewName("Custom Region");
            m_RegionPriority = CustomRegion.DefaultPriority;

            m_RestrictedSpells = new BitArray(SpellRegistry.Types.Length);
            m_RestrictedSkills = new BitArray(SkillInfo.Table.Length);

            foreach (Rectangle3D rect3d in rects)
            {
                DoChooseArea(null, this.Map, rect3d.Start, rect3d.End, this);
            }

            UpdateRegion();
        }

		public RegionControl( Serial serial ) : base( serial )
		{
        }


        #region Control Special Voids

        public bool RegionNameTaken(string testName)
        {

            if (m_AllControls != null)
            {
                foreach (RegionControl control in m_AllControls)
                {
                    if (control.RegionName == testName && control != this)
                        return true;
                }
            }

            return false;
        }

        public string FindNewName(string oldName)
        {
            int i = 1;

            string newName = oldName;
            while( RegionNameTaken(newName) )
            {
                newName = oldName;
                newName += String.Format(" {0}", i);
                i++;
            }

            return newName;
        }

        public void UpdateRegion()
        {
            if (m_Region != null)
                m_Region.Unregister();

            if (this.Map != null && this.Active)
            {
                if (this != null && this.RegionArea != null && this.RegionArea.Length > 0)
                {
                    m_Region = new CustomRegion(this);
                    // m_Region.GoLocation = m_CustomGoLocation;  // REMOVED
                    m_Region.Register();
                }
                else
                    m_Region = null;
            }
            else
                m_Region = null;
        }

        public void RemoveArea(int index, Mobile from)
        {
            try
            {
                List<Rectangle3D> rects = new List<Rectangle3D>();
                foreach (Rectangle3D rect in m_RegionArea)
                    rects.Add(rect);

                rects.RemoveAt(index);
                m_RegionArea = rects.ToArray();

                UpdateRegion();
                from.SendMessage("Area Removed!");
            }
            catch
            {
                from.SendMessage("Removing of Area Failed!");
            }
        }
        public static int GetRegistryNumber(ISpell s)
        {
            Type[] t = SpellRegistry.Types;

            for (int i = 0; i < t.Length; i++)
            {
                if (s.GetType() == t[i])
                    return i;
            }

            return -1;
        }


        public bool IsRestrictedSpell(ISpell s)
        {

            if (m_RestrictedSpells.Length != SpellRegistry.Types.Length)
            {

                m_RestrictedSpells = new BitArray(SpellRegistry.Types.Length);

                for (int i = 0; i < m_RestrictedSpells.Length; i++)
                    m_RestrictedSpells[i] = false;

            }

            int regNum = GetRegistryNumber(s);


            if (regNum < 0)	//Happens with unregistered Spells
                return false;

            return m_RestrictedSpells[regNum];
        }

        public bool IsRestrictedSkill(int skill)
        {
            if (m_RestrictedSkills.Length != SkillInfo.Table.Length)
            {

                m_RestrictedSkills = new BitArray(SkillInfo.Table.Length);

                for (int i = 0; i < m_RestrictedSkills.Length; i++)
                    m_RestrictedSkills[i] = false;

            }

            if (skill < 0)
                return false;


            return m_RestrictedSkills[skill];
        }

        public void ChooseArea(Mobile m)
        {
            BoundingBoxPicker.Begin(m, new BoundingBoxCallback(CustomRegion_Callback), this);
        }

        public void CustomRegion_Callback(Mobile from, Map map, Point3D start, Point3D end, object state)
        {
            DoChooseArea(from, map, start, end, state);
        }

        public void DoChooseArea(Mobile from, Map map, Point3D start, Point3D end, object control)
        {
            if (this != null)
            {
                List<Rectangle3D> areas = new List<Rectangle3D>();
                
                if (this.m_RegionArea != null)
                {
                    foreach (Rectangle3D rect in this.m_RegionArea)
                        areas.Add(rect);
                }
// Added Lord Dio's Z Value Fix
                if (start.Z == end.Z || start.Z < end.Z)
                {
                    if (start.Z != Server.Region.MinZ)
                        start.Z = Server.Region.MinZ;
                    if (end.Z != Server.Region.MaxZ)
                        end.Z = Server.Region.MaxZ;
                }
                else
                {
					if (start.Z != Server.Region.MaxZ)
                        start.Z = Server.Region.MaxZ;
                    if (end.Z != Server.Region.MinZ)
                    	end.Z = Server.Region.MinZ;
                }

                Rectangle3D newrect = new Rectangle3D(start, end);
                areas.Add(newrect);

                this.m_RegionArea = areas.ToArray();

                this.UpdateRegion();
// Added by nerun, so the RemoveAreaGump will be refreshed after added a new area
				from.CloseGump( typeof( RegionControlGump ) );
				from.SendGump( new RegionControlGump( this ) );
				from.CloseGump( typeof( RemoveAreaGump ) );
				from.SendGump( new RemoveAreaGump( this ) );
            }
        }

        # endregion


        #region Control Overrides

        public override void OnDoubleClick(Mobile m)
        {
            if (m.AccessLevel >= AccessLevel.GameMaster)
            {
                if (m_RestrictedSpells.Length != SpellRegistry.Types.Length)
                {
                    m_RestrictedSpells = new BitArray(SpellRegistry.Types.Length);

                    for (int i = 0; i < m_RestrictedSpells.Length; i++)
                        m_RestrictedSpells[i] = false;

                    m.SendMessage("Resetting all restricted Spells due to Spell change");
                }

                if (m_RestrictedSkills.Length != SkillInfo.Table.Length)
                {

                    m_RestrictedSkills = new BitArray(SkillInfo.Table.Length);

                    for (int i = 0; i < m_RestrictedSkills.Length; i++)
                        m_RestrictedSkills[i] = false;

                    m.SendMessage("Resetting all restricted Skills due to Skill change");

                }

                m.CloseGump(typeof(RegionControlGump));
                m.SendGump(new RegionControlGump(this));
                m.SendMessage("Don't forget to props this object for more options!");
                m.CloseGump(typeof(RemoveAreaGump));
                m.SendGump(new RemoveAreaGump(this));
            }
        }

		public override void OnMapChange()
		{
			UpdateRegion();
			base.OnMapChange();
		}

        public override void OnDelete()
        {
            if (m_Region != null)
                m_Region.Unregister();

            if (m_AllControls != null)
                m_AllControls.Remove(this);

            base.OnDelete();
        }

        # endregion


        #region Ser/Deser Helpers

        public static void WriteBitArray(GenericWriter writer, BitArray ba)
        {
            writer.Write(ba.Length);

            for (int i = 0; i < ba.Length; i++)
            {
                writer.Write(ba[i]);
            }
            return;
        }

        public static BitArray ReadBitArray(GenericReader reader)
        {
            int size = reader.ReadInt();

            BitArray newBA = new BitArray(size);

            for (int i = 0; i < size; i++)
            {
                newBA[i] = reader.ReadBool();
            }

            return newBA;
        }


        public static void WriteRect3DArray(GenericWriter writer, Rectangle3D[] ary)
        {
            if (ary == null)
            {
                writer.Write(0);
                return;
            }

            writer.Write(ary.Length);

            for (int i = 0; i < ary.Length; i++)
            {
                Rectangle3D rect = ((Rectangle3D)ary[i]);
                writer.Write((Point3D)rect.Start);
                writer.Write((Point3D)rect.End);
            }
            return;
        }

        public static List<Rectangle2D> ReadRect2DArray(GenericReader reader)
        {
            int size = reader.ReadInt();
            List<Rectangle2D> newAry = new List<Rectangle2D>();

            for (int i = 0; i < size; i++)
            {
                newAry.Add(reader.ReadRect2D());
            }

            return newAry;
        }

        public static Rectangle3D[] ReadRect3DArray(GenericReader reader)
        {
            int size = reader.ReadInt();
            List<Rectangle3D> newAry = new List<Rectangle3D>();

            for (int i = 0; i < size; i++)
            {
                Point3D start = reader.ReadPoint3D();
                Point3D end = reader.ReadPoint3D();
                newAry.Add(new Rectangle3D(start,end));
            }

            return newAry.ToArray();
        }

        # endregion


        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 5 ); // version

            // writer.Write((Point3D)CustomGoLocation);   // REMOVED

            WriteRect3DArray(writer, m_RegionArea);
            
            writer.Write((int)m_Flags);

            WriteBitArray(writer, m_RestrictedSpells);
            WriteBitArray(writer, m_RestrictedSkills);

            writer.Write((bool)m_Active);

            writer.Write((string)m_RegionName);
            writer.Write((int)m_RegionPriority);
            writer.Write((int)m_Music);
            writer.Write((TimeSpan)m_PlayerLogoutDelay);
            writer.Write((int)m_LightLevel);

            writer.Write((Map)m_MoveNPCToMap);
            writer.Write((Point3D)m_MoveNPCToLoc);
            writer.Write((Map)m_MovePlayerToMap);
            writer.Write((Point3D)m_MovePlayerToLoc); 
		}

		public override void Deserialize( GenericReader reader )
		{
            base.Deserialize(reader);

            int version = reader.ReadInt();

            // Point3D customGoLoc = new Point3D(0,0,0); // REMOVED
            switch (version)
            {
                // New RunUO 2.0 Version (case 5 and 4)
                case 5:
                {
                    // customGoLoc = reader.ReadPoint3D(); // REMOVED
                    goto case 4;
                }
                case 4:
                {
                    m_RegionArea = ReadRect3DArray(reader);
                    
                    m_Flags = (RegionFlag)reader.ReadInt();

                    m_RestrictedSpells = ReadBitArray(reader);
                    m_RestrictedSkills = ReadBitArray(reader);

                    m_Active = reader.ReadBool();

                    m_RegionName = reader.ReadString();
                    m_RegionPriority = reader.ReadInt();
                    m_Music = (MusicName)reader.ReadInt();
                    m_PlayerLogoutDelay = reader.ReadTimeSpan();
                    m_LightLevel = reader.ReadInt();

                    m_MoveNPCToMap = reader.ReadMap();
                    m_MoveNPCToLoc = reader.ReadPoint3D();
                    m_MovePlayerToMap = reader.ReadMap();
                    m_MovePlayerToLoc = reader.ReadPoint3D();

                    break;
                }

                // Old RunUO 1.0 Version (cases 3-0)
                case 3:
                {
                    m_LightLevel = reader.ReadInt();
                    goto case 2;
                }
                case 2:
                {
                    m_Music = (MusicName)reader.ReadInt();
                    goto case 1;
                }
                case 1:
                {
                    List<Rectangle2D> rects2d = ReadRect2DArray(reader);
                    foreach (Rectangle2D rect in rects2d)
                    {
                        Rectangle3D newrect = Server.Region.ConvertTo3D(rect);
                        DoChooseArea(null, this.Map, newrect.Start, newrect.End, this);
                    }

                    m_RegionPriority = reader.ReadInt();
                    m_PlayerLogoutDelay = reader.ReadTimeSpan();

                    m_RestrictedSpells = ReadBitArray(reader);
                    m_RestrictedSkills = ReadBitArray(reader);

                    m_Flags = (RegionFlag)reader.ReadInt();
                    if (NoPlayerCorpses)
                    {
                        DeleteNPCCorpse = true;
                        DeletePlayerCorpse = true;                          
                    }
                    if (NoItemDrop)
                    {
                        NoPlayerItemDrop = true;
                        NoNPCItemDrop = true;
                    }
                    // Invert because of change from "Cannot" to "Can"
                    if (CanLootOwnCorpse)
                    {
                        CanLootOwnCorpse = false;
                    }
                    if (CanEnter)
                    {
                        CanEnter = false;
                    }

                    m_RegionName = reader.ReadString();
                    break;
                }
                case 0:
                {
                    List<Rectangle2D> rects2d = ReadRect2DArray(reader);
                    foreach (Rectangle2D rect in rects2d)
                    {
                        Rectangle3D newrect = Server.Region.ConvertTo3D(rect);
                        DoChooseArea(null, this.Map, newrect.Start, newrect.End, this);
                    }

                    m_RestrictedSpells = ReadBitArray(reader);
                    m_RestrictedSkills = ReadBitArray(reader);

                    m_Flags = (RegionFlag)reader.ReadInt();
                    if (NoPlayerCorpses)
                    {
                        DeleteNPCCorpse = true;
                        DeletePlayerCorpse = true;
                    }
                    if (NoItemDrop)
                    {
                        NoPlayerItemDrop = true;
                        NoNPCItemDrop = true;
                    }
                    // Invert because of change from "Cannot" to "Can"
                    if (CanLootOwnCorpse)
                    {
                        CanLootOwnCorpse = false;
                    }
                    if (CanEnter)
                    {
                        CanEnter = false;
                    }

                    m_RegionName = reader.ReadString();
                    break;
                }
            }

            m_AllControls.Add(this);

            if(RegionNameTaken(m_RegionName))
                m_RegionName = FindNewName(m_RegionName);

            UpdateRegion();
           // m_CustomGoLocation = customGoLoc;  // REMOVED
           // CustomGoLocation = customGoLoc;   // REMOVED
            UpdateRegion();
		}
	}
}
