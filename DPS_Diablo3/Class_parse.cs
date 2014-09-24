using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace DPS_Diablo3.parse
{
    public class Parse
    {

        public string code { get; set; }
        public string name { get; set; }
        public string paragonLevel { get; set; }

        public Stats stats { get; set; }
        public class Stats
        {
            public string strength { get; set; }
            public string dexterity { get; set; }
            public string intelligence { get; set; }
        }

        public Items items { get; set; }
        public class Items
        {
            public Head head { get; set; }
            public Torso torso { get; set; }
            public Feet feet { get; set; }
            public Hands hands { get; set; }
            public Shoulders shoulders { get; set; }
            public Legs legs { get; set; }
            public Bracers bracers { get; set; }
            public MainHand mainHand { get; set; }
            public OffHand offHand { get; set; }
            public Waist waist { get; set; }
            public RightFinger rightFinger { get; set; }
            public LeftFinger leftFinger { get; set; }
            public Neck neck { get; set; }

            public class Head
            {
                public string tooltipParams { get; set; }
            }

            public class Torso
            {
                public string tooltipParams { get; set; }
            }

            public class Feet
            {
                public string tooltipParams { get; set; }
            }

            public class Hands
            {
                public string tooltipParams { get; set; }
            }

            public class Shoulders
            {
                public string tooltipParams { get; set; }
            }

            public class Legs
            {
                public string tooltipParams { get; set; }
            }


            public class Bracers
            {
                public string tooltipParams { get; set; }
            }
      
            public class MainHand
            {
                    public string tooltipParams { get; set; }
            }


            public class OffHand
            {
                public string tooltipParams { get; set; }
            }


            public class Waist
            {
                public string tooltipParams { get; set; }
            }

            public class RightFinger
            {
                public string tooltipParams { get; set; }
            }

            public class LeftFinger
            {
                public string tooltipParams { get; set; }
            }

            public class Neck
            {
                public string tooltipParams { get; set; }
            }
            
        }
    }

    public class MainHand
    {

        public string tooltipParams { get; set; }

        public Type type { get; set; }
        public class Type
        {
            public string id { get; set; }
            public string twoHanded { get; set; }
        }

        public AttacksPerSecond attacksPerSecond { get; set; }
        public class AttacksPerSecond
        {
            public string min { get; set; }
        }

        public Dps dps { get; set; }
        public class Dps
        {
            public string min { get; set; }
        }

        public MinDamage minDamage { get; set; }
        public class MinDamage
        {
            public string min { get; set; }
        }

        public MaxDamage maxDamage { get; set; }
        public class MaxDamage
        {
            public string min { get; set; }
        }

        public AttributesRaw attributesRaw { get; set; }
        public class AttributesRaw
        {

            public attacks_Per_Second_Percent Attacks_Per_Second_Percent { get; set; }
            public class attacks_Per_Second_Percent
            {
                public string min { get; set; }
            }

            public crit_Percent_Bonus_Capped Crit_Percent_Bonus_Capped { get; set; }
            public class crit_Percent_Bonus_Capped
            {
                public string min { get; set; }
            }

            public damage_Percent_Bonus_Vs_Elites Damage_Percent_Bonus_Vs_Elites { get; set; }
            public class damage_Percent_Bonus_Vs_Elites
            {
                public string min { get; set; }
            }

            public crit_Damage_Percent Crit_Damage_Percent { get; set; }
            public class crit_Damage_Percent
            {
                public string min { get; set; }
            }

            public damage_Weapon_Percent_Bonus_Physical Damage_Weapon_Percent_Bonus_Physical { get; set; }
            public class damage_Weapon_Percent_Bonus_Physical
            {
                public string min { get; set; }
            }

            public attacks_Per_Second_Item_Percent Attacks_Per_Second_Item_Percent { get; set; }
            public class attacks_Per_Second_Item_Percent
            {
                public string min { get; set; }
            }

            //----------+DPS %----------//
            public damage_Weapon_Percent_All Damage_Weapon_Percent_All { get; set; }
            public class damage_Weapon_Percent_All
            {
                public string min { get; set; }
            }

            //----------Physical----------//
            public damage_Delta_Physical Damage_Delta_Physical { get; set; }
            public class damage_Delta_Physical
            {
                public string min { get; set; }
            }
            public damage_Min_Physical Damage_Min_Physical { get; set; }
            public class damage_Min_Physical
            {
                public string min { get; set; }
            }
            public damage_Weapon_Delta_Physical Damage_Weapon_Delta_Physical { get; set; }
            public class damage_Weapon_Delta_Physical
            {
                public string min { get; set; }
            }
            public damage_Weapon_Min_Physical Damage_Weapon_Min_Physical { get; set; }
            public class damage_Weapon_Min_Physical
            {
                public string min { get; set; }
            }
            public damage_Weapon_Bonus_Delta_X1_Physical Damage_Weapon_Bonus_Delta_X1_Physical { get; set; }
            public class damage_Weapon_Bonus_Delta_X1_Physical
            {
                public string min { get; set; }
            }
            public damage_Weapon_Bonus_Min_X1_Physical Damage_Weapon_Bonus_Min_X1_Physical { get; set; }
            public class damage_Weapon_Bonus_Min_X1_Physical
            {
                public string min { get; set; }
            }
            public damage_Dealt_Percent_Bonus_Physical Damage_Dealt_Percent_Bonus_Physical { get; set; }
            public class damage_Dealt_Percent_Bonus_Physical
            {
                public string min { get; set; }
            }

            //----------Fire----------//

            public damage_Weapon_Delta_Fire Damage_Weapon_Delta_Fire { get; set; }
            public class damage_Weapon_Delta_Fire
            {
                public string min { get; set; }
            }
            public damage_Weapon_Min_Fire Damage_Weapon_Min_Fire { get; set; }
            public class damage_Weapon_Min_Fire
            {
                public string min { get; set; }
            }
            public damage_Dealt_Percent_Bonus_Fire Damage_Dealt_Percent_Bonus_Fire { get; set; }
            public class damage_Dealt_Percent_Bonus_Fire
            {
                public string min { get; set; }
            }

            //----------Cold----------//

            public damage_Weapon_Delta_Cold Damage_Weapon_Delta_Cold { get; set; }
            public class damage_Weapon_Delta_Cold
            {
                public string min { get; set; }
            }
            public damage_Weapon_Min_Cold Damage_Weapon_Min_Cold { get; set; }
            public class damage_Weapon_Min_Cold
            {
                public string min { get; set; }
            }
            public damage_Dealt_Percent_Bonus_Cold Damage_Dealt_Percent_Bonus_Cold { get; set; }
            public class damage_Dealt_Percent_Bonus_Cold
            {
                public string min { get; set; }
            }

            //----------Lightning----------//

            public damage_Weapon_Delta_Lightning Damage_Weapon_Delta_Lightning { get; set; }
            public class damage_Weapon_Delta_Lightning
            {
                public string min { get; set; }
            }
            public damage_Weapon_Min_Lightning Damage_Weapon_Min_Lightning { get; set; }
            public class damage_Weapon_Min_Lightning
            {
                public string min { get; set; }
            }
            public damage_Dealt_Percent_Bonus_Lightning Damage_Dealt_Percent_Bonus_Lightning { get; set; }
            public class damage_Dealt_Percent_Bonus_Lightning
            {
                public string min { get; set; }
            }

            //----------Poison----------//

            public damage_Weapon_Delta_Poison Damage_Weapon_Delta_Poison { get; set; }
            public class damage_Weapon_Delta_Poison
            {
                public string min { get; set; }
            }
            public damage_Weapon_Min_Poison Damage_Weapon_Min_Poison { get; set; }
            public class damage_Weapon_Min_Poison
            {
                public string min { get; set; }
            }
            public damage_Dealt_Percent_Bonus_Poison Damage_Dealt_Percent_Bonus_Poison { get; set; }
            public class damage_Dealt_Percent_Bonus_Poison
            {
                public string min { get; set; }
            }

            //----------Arcane----------//

            public damage_Weapon_Delta_Arcane Damage_Weapon_Delta_Arcane { get; set; }
            public class damage_Weapon_Delta_Arcane
            {
                public string min { get; set; }
            }
            public damage_Weapon_Min_Arcane Damage_Weapon_Min_Arcane { get; set; }
            public class damage_Weapon_Min_Arcane
            {
                public string min { get; set; }
            }
            public damage_Dealt_Percent_Bonus_Arcane Damage_Dealt_Percent_Bonus_Arcane { get; set; }
            public class damage_Dealt_Percent_Bonus_Arcane
            {
                public string min { get; set; }
            }

            //----------Holy----------//

            public damage_Weapon_Delta_Holy Damage_Weapon_Delta_Holy { get; set; }
            public class damage_Weapon_Delta_Holy
            {
                public string min { get; set; }
            }
            public damage_Weapon_Min_Holy Damage_Weapon_Min_Holy { get; set; }
            public class damage_Weapon_Min_Holy
            {
                public string min { get; set; }
            }
            public damage_Dealt_Percent_Bonus_Holy Damage_Dealt_Percent_Bonus_Holy { get; set; }
            public class damage_Dealt_Percent_Bonus_Holy
            {
                public string min { get; set; }
            }
        }


    }

        [Serializable]
        public class MyCustomDict : ISerializable
        {
            public Dictionary<string, object[]> dict;
            public MyCustomDict()
            {
                dict = new Dictionary<string, object[]>();
            }
            protected MyCustomDict(SerializationInfo info, StreamingContext context)
            {
                dict = new Dictionary<string, object[]>();
                foreach (var entry in info)
                {
                    Debug.Assert(entry.ObjectType.IsArray);
                    object[] array = entry.Value as object[];
                    dict.Add(entry.Name, array);
                }
            }
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                foreach (string key in dict.Keys)
                {
                    info.AddValue(key, dict[key]);
                }
            }
        }

        public class Primary
        {
                public string text { get; set; }
        }

 

}

