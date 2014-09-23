using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using DPS_Diablo3.Properties;
using System.Configuration;
using System.Xml.Serialization;
using System.Xml;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using DPS_Diablo3.parse;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace DPS_Diablo3
{
    public partial class dps_diablo : Form
    {

        List<string> main500 = new List<string> { "Jade Harvester", "Vyr", "Marauder", "Inna", "of Akkhan", "Firebird", "Helltooth" };
        List<string> main250 = new List<string> { "Chantodo", "Manajuma", "Sage", "Shenlong", "Zunimassa", "Natalya" };
        List<string> storms_set = new List<string> { "Eight-Demon Boots", "Fists of Thunder", "Heart of the Crashing Wave", "Mantle of the Upside-Down Sinners", "Mask of the Searing Sky", "Scales of the Dancing Serpent" };
        public bool sunwuko = false;

        public void set_parse()
        {
            akkhan = false;
            int set = 0; int rorg = 0;

            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("Ring of Royal Grandeur")) rorg += 1;

            foreach (string m500 in main500)
            {
                for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains(m500)) set += 1;
                // 17.09.2014 // if (set > 1) mainstat = mainstat + 500;
                set = 0;
            }

            foreach (string m250 in main250)
            {
                for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains(m250)) set += 1;
                // 17.09.2014 // if (set > 1) mainstat = mainstat + 250;
                set = 0;
            }

            foreach (string ss in storms_set)
                for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains(ss)) set += 1;
            // 17.09.2014 // if (set > 1) mainstat = mainstat + 500;
            if ((set > 3) || (set > 2 && rorg != 0)) elem_all = elem_all + 15;
            set = 0;

            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("Sunwuko's")) set += 1;
            if (set > 1) sunwuko = true;
            set = 0;
            
            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("Born's")) set += 1;
            if ((set > 2) || (set > 1 && rorg != 0)) cdr[9] = 10;
            set = 0;

            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("Captain Crimson's")) set += 1;
            if (set > 1) cdr[10] = 10;
            set = 0;

            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("Tal Rasha")) set += 1;
            if (set > 1) elem_all = elem_all + 5;
            set = 0;

            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("Blackthorne")) set += 1;
            if (set > 1) elite_damage = elite_damage + 10;
            set = 0;

            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("T12_Legendary_Set_Hallowed")) set += 1;
            if (set > 3) aps_up = aps_up + 10;
            set = 0;

            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("T12_Legendary_Set_Cains")) set += 1;
            if (set > 3) aps_up = aps_up + 8;
            set = 0;

            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("T12_Legendary_Set_Aughild")) set += 1;
            if ((set > 5) || (set > 3 && rorg !=0)) elite_damage = elite_damage + 15;
            set = 0;

            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("Raekor")) set += 1;
            // 17.09.2014 // if ((set > 3) || (set > 2 && rorg != 0)) mainstat = mainstat + 500;
            set = 0;
            
            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("Natalya")) set += 1;
            if ((set > 2) || (set > 1 && rorg != 0)) off_crit_chance = off_crit_chance + 7;
            set = 0;
            
            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("Bul-Kathos's Solemn Vow") || pars_hero[i].Contains("Bul-Kathos's Warrior Blood")) set += 1;
            // 17.09.2014 // if (set > 1) mainstat = mainstat + 250;
            set = 0;

            for (int i = 0; i < pars_hero.Length; i++) if (pars_hero[i].Contains("of Akkhan")) set += 1;
            if ((set > 5) || (set > 4 && rorg != 0)) akkhan = true;
            set = 0;
        }

    }
}
