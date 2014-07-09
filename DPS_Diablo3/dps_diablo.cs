﻿using System;
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
using System.Runtime.InteropServices;
//using Ranslant.JSON.Linq;

namespace DPS_Diablo3
{

    public partial class dps_diablo : Form
    {

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        public string[] pars_hero;
        public List<string> passive_skill, active_skill, active_skill_rune;

        public string 
             hero_parse, head, torso, feet, hands, shoulders, legs, bracers, mainHand, offHand, waist, rightFinger, leftFinger, neck
            , head_parse, torso_parse, feet_parse, hands_parse, shoulders_parse, legs_parse, bracers_parse, mainHand_parse, offHand_parse, waist_parse, rightFinger_parse, leftFinger_parse, neck_parse
            , item_parse
            , sep = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).ToString()
            , skill_damage = "", phys_up = "", dps_min = "", aps_item = "", dps_min_off = "", phys_up_off = "", aps_item_off = ""
            , pers_name, first_skill, weapon_id, weapon_2h, para_lvl
            , version, p_lvl = ""
            ;
        
        public double wepdamage, otherdamage, cleardamage, critdamage, overdamage, speedtotal, result, result_profile,
            wepdamage_wd, otherdamage_wd, cleardamage_wd, critdamage_wd, overdamage_wd, speedtotal_wd, result_wd, petdamage_wd, skill_wd,
              damage1, damage2, ac1, ac2, main, acincr, cc, cd, off_min, off_max, am_min, am_max,
              r1_min, r1_max, r2_min, r2_max, other_min, other_max, fromskills, elem, toskill, elite, skill,
              result_old, result_old_wd,
              dmg1_1, dmg1_2, dmg2_1, dmg2_2, ac1_a, ac2_a, dmg1_p, dmg2_p, ac1_p, ac2_p, dmg1_1_a, dmg1_2_a, dmg2_1_a, dmg2_2_a,
              skill1_usage, skill2_usage, skill1, skill2, elem1, elem2, toskill1, toskill2, ac1w, elite1, elite1w,
              coold, cdr_all, skill_base, skill_other
              , del_Physical = 0, del_X1_Physical = 0, del_Fire = 0, del_Cold = 0, del_Lightning = 0, del_Poison = 0, del_Arcane = 0, del_Holy = 0
              , min_Physical = 0, min_X1_Physical = 0, min_Fire = 0, min_Cold = 0, min_Lightning = 0, min_Poison = 0, min_Arcane = 0, min_Holy = 0
              , elem_Physical = 0, elem_Fire = 0, elem_Cold = 0, elem_Lightning = 0, elem_Poison = 0, elem_Arcane = 0, elem_Holy = 0
              , damage_min = 0, damage_max = 0, elem_all = 0, aps = 0, aps_up = 0, aps_up_off = 0, aps_off = 0, damage_min_off = 0, damage_max_off = 0
              , crit_damage = 0, crit_chance = 0, elite_damage = 0, off_crit_chance = 0, off_del_Physical = 0, off_min_Physical = 0
              , rings_count = 0, r1_del_Physical = 0, r1_min_Physical = 0, r2_del_Physical = 0, r2_min_Physical = 0, am_del_Physical = 0, am_min_Physical = 0
              , cooldown
              , ver = 2.1
              ;

        public Double[] cdr = new Double[] {0,0,0,0,0,0,0,0,0,0,0};

        public Class_lang lng = new Class_lang();
        
        public int mainstat = 0, to_skl = 0, cnt = 0, from_skl, cdr_n=0, para_level = 0, level1 = 0, level2 = 0;

        decimal lev2 = 0, curr_para = 0, prev_para = 0, coold_pass=0;

        public static SettingsTable overview;

        public Form frm_para = new Form();
        public Label lb_para_main = new Label();
        public Label lb_para_as = new Label();
        public Label lb_para_cdr = new Label();
        public Label lb_para_cc = new Label();
        public Label lb_para_cd = new Label();
        public Label lb_para = new Label();
        public Label lb_para_lvl = new Label();
        public NumericUpDown nud_para_main = new NumericUpDown();
        public NumericUpDown nud_para_as = new NumericUpDown();
        public NumericUpDown nud_para_cdr = new NumericUpDown();
        public NumericUpDown nud_para_cc = new NumericUpDown();
        public NumericUpDown nud_para_cd = new NumericUpDown();
        public Button b_para_save = new Button();
        public Button b_para_load = new Button();

        public dps_diablo()
        {

            InitializeComponent();

            pan_divider_Paint(null,null);

            version = "DPS - Diablo3 ver. " + string.Format("{0:F1}", ver).ToString().Replace(sep, ".");
            this.Text = version;

            //faq();
            para_form_create();

            //MessageBox.Show(CultureInfo.CurrentCulture.EnglishName);
            if (lb_adv.Text == "def") tsmi_adv_Click(null, null);
            
            #if DEBUG
            b_parse.Visible = true;
            #endif

            //comb_region.SelectedIndex = 0;
            this.KeyPreview = true;

            if (Settings.Default.bt_lang == "")
            {
                if (CultureInfo.CurrentCulture.EnglishName.Contains("Russia")) Settings.Default.bt_lang = "ENG";
                else Settings.Default.bt_lang = "RUS";
                Settings.Default.Save();
            }
            
            bt_lang.Text = Settings.Default.bt_lang;
            if (bt_lang.Text == "ENG") lng.Lang_rus(); else lng.Lang_eng();
            Lang();
            cb_choice.SelectedIndex = 0;

            if (Settings.Default.UpdateSettings)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpdateSettings = false;
                Settings.Default.Save();
            }

            //foreach (SettingsPropertyValue set in Settings.Default.PropertyValues)
            //{
            //    if (set.Name == "tb_toskill") MessageBox.Show(set.Name.ToString() + " = " + set.PropertyValue.ToString());
            //}

            List<string> ro = new List<string> { "tb_dmg2_1_a", "tb_dmg2_p", "tb_dmg1_p", "tb_dmg2_2_a", "tb_skill2", "tb_skill1_usage", "tb_skill2_usage", "tb_damage2", "tb_ac2", "tb_toskill2", "tb_elem2", "tb_ac1_p", "tb_ac2_p", "tb_pers" };
            List<string> click_dot = new List<string> { "tb_damage1", "tb_damage2", "tb_ac1", "tb_ac2", "tb_acincr", "tb_cc", "tb_cd" };
            List<string> click = new List<string> { "tb_fromskills", "tb_elem", "tb_toskill", "tb_elite", "tb_skill", "tb_main", "tb_off_min", "tb_off_max", "tb_am_min", "tb_am_max", "tb_r1_min", "tb_r1_max", "tb_r2_min", "tb_r2_max", "tb_dmg1_1_a", "tb_dmg1_2_a", "tb_dmg2_1_a", "tb_dmg2_2_a", "tb_dmg1_p", "tb_dmg2_p", "tb_skill_usage", "tb_skill2_usage", "tb_skill1", "tb_skill2", "tb_toskill2", "tb_elem2", "tb_toskill1", "tb_elem1", "tb_ac1_p", "tb_ac2_p", "tb_dmg1_w", "tb_dmg2_w" };

            foreach (Control control in this.Controls.OfType<TextBox>())
            {
                if (ro.Contains(control.Name)) control.MouseClick += Readonly_clear;
                if (click_dot.Contains(control.Name)) control.KeyPress += Input_dot;
                if (click.Contains(control.Name)) control.KeyPress += Input_norm;
                control.KeyDown += Key_Test;
                if (control.Name != "tb_pers") control.ContextMenu = new ContextMenu();
            }

            foreach (Control control in this.Controls.OfType<Panel>())
                foreach (Control control1 in control.Controls.OfType<TextBox>())
                {
                    if (ro.Contains(control1.Name.ToString().Trim())) control1.MouseClick += Readonly_clear;
                    if (click_dot.Contains(control1.Name)) control1.KeyPress += Input_dot;
                    if (click.Contains(control1.Name)) control1.KeyPress += Input_norm;
                    control1.KeyDown += Key_Test;
                    control1.ContextMenu = new ContextMenu();
                }

            foreach (Control control in this.Controls.OfType<Panel>())
                foreach (NumericUpDown nud in control.Controls.OfType<NumericUpDown>())
                    nud.TextChanged += new EventHandler(nud_TextChanged);

            foreach (NumericUpDown nud in pan_cdr.Controls.OfType<NumericUpDown>())
                //if (nud.Name != "nud_cooldown")
                    nud.ValueChanged += nud_cdr_ValueChanged;
        }

        public void White()
        {
            foreach (Control control in this.Controls.OfType<TextBox>())
                if (control.BackColor == Color.Red) control.BackColor = Color.White;

            foreach (Control control in this.Controls.OfType<Panel>())
                foreach (Control control1 in control.Controls.OfType<TextBox>())
                    if (control1.BackColor == Color.Red) control1.BackColor = Color.White;
        }

        public void Save_settings()
        {
            Settings.Default.tb_damage1 = tb_damage1.Text;
            Settings.Default.tb_damage2 = tb_damage2.Text;
            Settings.Default.tb_ac1 = tb_ac1.Text;
            Settings.Default.tb_ac2 = tb_ac2.Text;
            Settings.Default.tb_main = tb_main.Text;
            Settings.Default.tb_acincr = tb_acincr.Text;
            Settings.Default.tb_cc = tb_cc.Text;
            Settings.Default.tb_cd = tb_cd.Text;
            Settings.Default.tb_off_min = tb_off_min.Text;
            Settings.Default.tb_off_max = tb_off_max.Text;
            Settings.Default.tb_am_min = tb_am_min.Text;
            Settings.Default.tb_am_max = tb_am_max.Text;
            Settings.Default.tb_r1_min = tb_r1_min.Text;
            Settings.Default.tb_r1_max = tb_r1_max.Text;
            Settings.Default.tb_r2_min = tb_r2_min.Text;
            Settings.Default.tb_r2_max = tb_r2_max.Text;
            Settings.Default.tb_fromskills = tb_fromskills.Text;
            Settings.Default.tb_elem = tb_elem.Text;
            Settings.Default.tb_toskill = tb_toskill.Text;
            Settings.Default.tb_elite = tb_elite.Text;
            Settings.Default.tb_skill = tb_skill.Text;
            Settings.Default.nud_garg = nud_garg.Value;
            Settings.Default.nud_dogs = nud_dogs.Value;
            Settings.Default.nud_fet = nud_fet.Value;
            Settings.Default.nud_elemp = nud_elemp.Value;
            Settings.Default.nud_damp = nud_damp.Value;
            Settings.Default.nud_speedp = nud_speedp.Value;
            Settings.Default.tb_dmg1_1_a = tb_dmg1_1_a.Text;
            Settings.Default.tb_dmg1_2_a = tb_dmg1_2_a.Text;
            Settings.Default.tb_dmg2_1_a = tb_dmg2_1_a.Text;
            Settings.Default.tb_dmg2_2_a = tb_dmg2_2_a.Text;
            Settings.Default.tb_dmg1_p = tb_dmg1_p.Text;
            Settings.Default.tb_dmg2_p = tb_dmg2_p.Text;
            Settings.Default.tb_skill1 = tb_skill1.Text;
            Settings.Default.tb_skill2 = tb_skill2.Text;
            Settings.Default.tb_skill1_usage = tb_skill1_usage.Text;
            Settings.Default.tb_skill2_usage = tb_skill2_usage.Text;
            Settings.Default.tb_toskill1 = tb_toskill1.Text;
            Settings.Default.tb_toskill2 = tb_toskill2.Text;
            Settings.Default.tb_elem1 = tb_elem1.Text;
            Settings.Default.tb_elem2 = tb_elem2.Text;
            Settings.Default.tb_ac1_p = tb_ac1_p.Text;
            Settings.Default.tb_ac2_p = tb_ac2_p.Text;
            Settings.Default.tb_dmg1_w = tb_dmg1_w.Text;
            Settings.Default.tb_dmg2_w = tb_dmg2_w.Text;
            Settings.Default.nud_main_w = nud_main_w.Value;
            Settings.Default.nud_damp_w = nud_damp_w.Value;
            Settings.Default.nud_acp_w = nud_acp_w.Value;
            Settings.Default.nud_cd_w = nud_cd_w.Value;
            Settings.Default.nud_elem_w = nud_elem_w.Value;
            Settings.Default.nud_cooldown = nud_cooldown.Value;
            Settings.Default.nud_cdr1 = nud_cdr1.Value;
            Settings.Default.nud_cdr2 = nud_cdr2.Value;
            Settings.Default.nud_cdr3 = nud_cdr3.Value;
            Settings.Default.nud_cdr4 = nud_cdr4.Value;
            Settings.Default.nud_cdr5 = nud_cdr5.Value;
            Settings.Default.nud_cdr6 = nud_cdr6.Value;
            Settings.Default.nud_cdr7 = nud_cdr7.Value;
            Settings.Default.nud_cdr8 = nud_cdr8.Value;
            Settings.Default.nud_cdr9 = nud_cdr9.Value;
            Settings.Default.nud_cdr10 = nud_cdr10.Value;
            Settings.Default.bt_lang = bt_lang.Text;

            overview = new SettingsTable();

            DataTable Strings = overview.dataset.Tables.Add("Strings");
            Strings.Columns.Add("Key");
            Strings.Columns.Add("Value", typeof(String));

            DataTable Decimals = overview.dataset.Tables.Add("Decimals");
            Decimals.Columns.Add("Key");
            Decimals.Columns.Add("Value", typeof(Decimal));

            DataTable Booleans = overview.dataset.Tables.Add("Booleans");
            Booleans.Columns.Add("Key");
            Booleans.Columns.Add("Value", typeof(Boolean));

            foreach (SettingsProperty currentProperty in Settings.Default.Properties)
            {
                if (Settings.Default[currentProperty.Name].GetType().ToString() == "System.String")
                {
                    Strings.Rows.Add(currentProperty.Name.ToString(), Settings.Default[currentProperty.Name].ToString());
                }
                if (Settings.Default[currentProperty.Name].GetType().ToString() == "System.Decimal")
                {
                    Decimals.Rows.Add(currentProperty.Name.ToString(), Convert.ToDecimal(Settings.Default[currentProperty.Name]));
                }
                if (Settings.Default[currentProperty.Name].GetType().ToString() == "System.Boolean")
                {
                    Booleans.Rows.Add(currentProperty.Name.ToString(), Convert.ToBoolean(Settings.Default[currentProperty.Name]));
                }
            }

            Settings.Default.nud_para_main = nud_para_main.Value;
            Settings.Default.nud_para_as = nud_para_as.Value;
            Settings.Default.nud_para_cdr = nud_para_cdr.Value;
            Settings.Default.nud_para_cc = nud_para_cc.Value;
            Settings.Default.nud_para_cd = nud_para_cd.Value;
            Settings.Default.lb_para_lvl = lb_para_lvl.Text;

            Settings.Default.Save();

            //WriteXML(overview);
            //ReadXML();
        }

        public class SettingsTable
        {
            public DataSet dataset = new DataSet();
        }


        public static void WriteXML(string path)
        {

            XmlSerializer writer = new XmlSerializer(typeof(SettingsTable));
            //var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            System.IO.FileStream file = System.IO.File.Create(path);
            writer.Serialize(file, overview);
            file.Close();
            overview.dataset.Dispose();
        }

        public void ReadXML(string path)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(SettingsTable));
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            SettingsTable overview = new SettingsTable();
            overview = (SettingsTable)reader.Deserialize(file);

            for (int i = 0; i < overview.dataset.Tables[0].Rows.Count; i++)
            {
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_damage1") Settings.Default.tb_damage1 = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_damage2") Settings.Default.tb_damage2 = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_ac1") Settings.Default.tb_ac1 = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_ac2") Settings.Default.tb_ac2 = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_main") Settings.Default.tb_main = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_acincr") Settings.Default.tb_acincr = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_cc") Settings.Default.tb_cc = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_cd") Settings.Default.tb_cd = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_off_min") Settings.Default.tb_off_min = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_off_max") Settings.Default.tb_off_max = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_am_min") Settings.Default.tb_am_min = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_am_max") Settings.Default.tb_am_max = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_r1_min") Settings.Default.tb_r1_min = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_r1_max") Settings.Default.tb_r1_max = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_r2_min") Settings.Default.tb_r2_min = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_r2_max") Settings.Default.tb_r2_max = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_fromskills") Settings.Default.tb_fromskills = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_elem") Settings.Default.tb_elem = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_toskill") Settings.Default.tb_toskill = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_elite") Settings.Default.tb_elite = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_skill") Settings.Default.tb_skill = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_dmg1_1_a") Settings.Default.tb_dmg1_1_a = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_dmg1_2_a") Settings.Default.tb_dmg1_2_a = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_dmg2_1_a") Settings.Default.tb_dmg2_1_a = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_dmg2_2_a") Settings.Default.tb_dmg2_2_a = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_dmg1_p") Settings.Default.tb_dmg1_p = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_dmg2_p") Settings.Default.tb_dmg2_p = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_skill1") Settings.Default.tb_skill1 = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_skill2") Settings.Default.tb_skill2 = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_skill1_usage") Settings.Default.tb_skill1_usage = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_skill2_usage") Settings.Default.tb_skill2_usage = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_toskill1") Settings.Default.tb_toskill1 = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_toskill2") Settings.Default.tb_toskill2 = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_elem1") Settings.Default.tb_elem1 = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_elem2") Settings.Default.tb_elem2 = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_ac1_p") Settings.Default.tb_ac1_p = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_ac2_p") Settings.Default.tb_ac2_p = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_dmg1_w") Settings.Default.tb_dmg1_w = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
                if (overview.dataset.Tables["Strings"].Rows[i][0].ToString() == "tb_dmg2_w") Settings.Default.tb_dmg2_w = overview.dataset.Tables["Strings"].Rows[i][1].ToString();
            }
            for (int j = 0; j < overview.dataset.Tables[1].Rows.Count; j++)
            {

                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_garg") Settings.Default.nud_garg = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_dogs") Settings.Default.nud_dogs = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_fet") Settings.Default.nud_fet = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_elemp") Settings.Default.nud_elemp = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_damp") Settings.Default.nud_damp = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_speedp") Settings.Default.nud_speedp = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_main_w") Settings.Default.nud_main_w = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_damp_w") Settings.Default.nud_damp_w = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_acp_w") Settings.Default.nud_acp_w = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_cd_w") Settings.Default.nud_cd_w = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_elem_w") Settings.Default.nud_elem_w = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_cooldown") Settings.Default.nud_cooldown = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_cdr1") Settings.Default.nud_cdr1 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_cdr2") Settings.Default.nud_cdr2 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_cdr3") Settings.Default.nud_cdr3 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_cdr4") Settings.Default.nud_cdr4 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_cdr5") Settings.Default.nud_cdr5 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_cdr6") Settings.Default.nud_cdr6 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_cdr7") Settings.Default.nud_cdr7 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_cdr8") Settings.Default.nud_cdr8 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_cdr9") Settings.Default.nud_cdr9 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
                if (overview.dataset.Tables[1].Rows[j][0].ToString() == "nud_cdr10") Settings.Default.nud_cdr10 = Convert.ToDecimal(overview.dataset.Tables[1].Rows[j][1]);
            }

            Settings.Default.Save();
            file.Close();
            //file.Dispose();
            //overview.dataset.Dispose();

        }

        private void dps_diablo_Load(object sender, EventArgs e)
        {
            this.Icon = DPS_Diablo3.Properties.Resources.icon;
            tooltips();
        }

        public void Check_lines()
        {
            string s = ".";
            int flag = 0;
            int count = 0;

            foreach (Control control in this.Controls.OfType<TextBox>())
            {
                if (control.Name != "tb_pers")
                {
                    count = control.Text.ToCharArray().Where(c => c == s[0]).Count();
                    if (count > 1) flag++;
                    if (control.Text == ".") flag++;
                }
            }


            if (flag > 0) lb_result.Text = lng.lb_resultt_dots;
            else
            {
                if (lb_adv.Text == "def")
                {
                    if (tb_damage1.Text == "" || tb_ac1.Text == "" || tb_main.Text == "" || tb_cc.Text == "" || tb_cd.Text == "" || tb_skill.Text == "")
                    {
                        lb_result.Text = lng.lb_resultt_none;
                        White();
                        if (tb_damage1.Text == "") tb_damage1.BackColor = Color.Red;
                        if (tb_ac1.Text == "") tb_ac1.BackColor = Color.Red;
                        if (tb_main.Text == "") tb_main.BackColor = Color.Red;
                        if (tb_cc.Text == "") tb_cc.BackColor = Color.Red;
                        if (tb_cd.Text == "") tb_cd.BackColor = Color.Red;
                        if (tb_skill.Text == "") tb_skill.BackColor = Color.Red;
                    }
                    else
                    {
                        lb_result.Text = lng.lb_resultt;
                        White();
                    }
                }
                else
                {
                    if (tb_dmg1_1_a.Text == "" || tb_dmg1_2_a.Text == "" || tb_ac1.Text == "" || tb_main.Text == "" || tb_cc.Text == "" || tb_cd.Text == "" || tb_skill1.Text == "")
                    {
                        lb_result.Text = lng.lb_resultt_none;
                        White();
                        if (tb_dmg1_1_a.Text == "") tb_dmg1_1_a.BackColor = Color.Red;
                        if (tb_dmg1_2_a.Text == "") tb_dmg1_2_a.BackColor = Color.Red;
                        if (tb_ac1.Text == "") tb_ac1.BackColor = Color.Red;
                        if (tb_main.Text == "") tb_main.BackColor = Color.Red;
                        if (tb_cc.Text == "") tb_cc.BackColor = Color.Red;
                        if (tb_cd.Text == "") tb_cd.BackColor = Color.Red;
                        if (tb_skill1.Text == "") tb_skill1.BackColor = Color.Red;
                    }
                    else
                    {
                        lb_result.Text = lng.lb_resultt;
                        White();
                    }
                }
            }
        }

        private void b_start_Click(object sender, EventArgs e)
        {
            pan_roll.Visible = true;

            Check_lines();
            if (lb_result.Text == lng.lb_resultt)
            {
                foreach (NumericUpDown numud in this.Controls.OfType<NumericUpDown>()) if (numud.Text == "") numud.Value = 0;
                foreach (Control control in this.Controls.OfType<Panel>())
                    foreach (NumericUpDown numud in control.Controls.OfType<NumericUpDown>()) if (numud.Text == "") numud.Value = 0;

                lb_start.Text = "res";
                cdr_all = 100;
                coold = 0;
                if (nud_cooldown.Value != 0)
                {
                    nud_cdr_ValueChanged(null, null);
                    lb_cdr.Visible = true;
                }

                //Считаем DPS при продвинутом расчёте

                if (lb_adv.Text == "adv")
                {
                    //b_wep.Enabled = true;
                    pan_list.Visible = true;
                    ac1 = Convert.ToSingle(tb_ac1.Text.Replace(".", sep));
                    if (tb_dmg1_p.Text != "" && tb_dmg1_p.Text.ToLower().Replace(" ", "") != "weapon1") { dmg1_p = Convert.ToSingle(tb_dmg1_p.Text.Replace(".", sep)); } else { dmg1_p = 0; }
                    if (tb_dmg2_p.Text != "" && tb_dmg2_p.Text.ToLower().Replace(" ", "") != "weapon2") { dmg2_p = Convert.ToSingle(tb_dmg2_p.Text.Replace(".", sep)); } else { dmg2_p = 0; }
                    if (tb_ac1_p.Text != "" && tb_ac1_p.Text.ToLower().Replace(" ", "") != "weapon1")
                    {
                        ac1_p = Convert.ToSingle(tb_ac1_p.Text.Replace(".", sep));
                        ac1 = Math.Round((ac1 * 100 / (100 + ac1_p)), 1) * ((100 + ac1_p) / 100);
                    }
                    dmg1_1 = Math.Round(Convert.ToSingle(tb_dmg1_1_a.Text.Replace(".", sep)) / (dmg1_p / 100 + 1));
                    dmg1_2 = Math.Round(Convert.ToSingle(tb_dmg1_2_a.Text.Replace(".", sep)) / (dmg1_p / 100 + 1));
                    damage1 = (dmg1_1 + dmg1_2) / 2 * ac1 * (dmg1_p / 100 + 1);
                    damage2 = damage1;
                    ac2 = ac1;
                    if (tb_dmg2_1_a.Text != "" && tb_dmg2_1_a.Text != "min2" && tb_dmg2_2_a.Text != "" && tb_dmg2_2_a.Text != "max2" && tb_ac2.Text != "" && tb_ac2.Text != "weapon2" && tb_ac2.Text != "0" && tb_dmg2_1_a.Text != "0" && tb_dmg2_2_a.Text != "0")
                    {
                        dmg2_1 = Math.Round(Convert.ToSingle(tb_dmg2_1_a.Text.Replace(".", sep)) / (dmg2_p / 100 + 1));
                        dmg2_2 = Math.Round(Convert.ToSingle(tb_dmg2_2_a.Text.Replace(".", sep)) / (dmg2_p / 100 + 1));
                        ac2 = Convert.ToSingle(tb_ac2.Text.Replace(".", sep));
                        if (tb_ac2_p.Text != "" && tb_ac2_p.Text.ToLower().Replace(" ", "") != "weapon2")
                        {
                            ac2_p = Convert.ToSingle(tb_ac2_p.Text.Replace(".", sep));
                            ac2 = Math.Round((ac2 * 100 / (100 + ac2_p)), 1) * ((100 + ac2_p) / 100);
                        }
                        damage2 = (dmg2_1 + dmg2_2) / 2 * ac2 * (dmg2_p / 100 + 1);

                    }
                    if (tb_skill1_usage.Text == "100%") skill1_usage = 100;
                    if (tb_skill1_usage.Text != "100%" && tb_skill1_usage.Text != "") skill1_usage = Convert.ToSingle(tb_skill1_usage.Text.Replace(".", sep));
                    skill1 = Convert.ToSingle(tb_skill1.Text.Replace(".", sep));
                    skill2 = 0; skill2_usage = 0;
                    if (tb_skill2_usage.Text != "" && tb_skill2.Text != "" && tb_skill2_usage.Text != "skill2" && tb_skill2.Text != "skill2")
                    {
                        skill2 = Convert.ToSingle(tb_skill2.Text.Replace(".", sep));
                        skill2_usage = Convert.ToSingle(tb_skill2_usage.Text.Replace(".", sep));
                    }
                    //(elem / 100 + 1) * (toskill / 100 + 1)
                    if (tb_elem1.Text != "") elem1 = Convert.ToSingle(tb_elem1.Text.Replace(".", sep)); else elem1 = 0;
                    if (tb_elem2.Text != "" && tb_elem2.Text != "skill2") elem2 = Convert.ToSingle(tb_elem2.Text.Replace(".", sep)); else elem2 = 0;
                    if (tb_toskill1.Text != "") toskill1 = Convert.ToSingle(tb_toskill1.Text.Replace(".", sep)); else toskill1 = 0;
                    if (tb_toskill2.Text != "" && tb_toskill2.Text != "skill2") toskill2 = Convert.ToSingle(tb_toskill2.Text.Replace(".", sep)); else toskill2 = 0;

                    skill_base = (skill1 * skill1_usage / 100 * (elem1 / 100 + 1) * (toskill1 / 100 + 1));
                    skill_other = (skill2 * skill2_usage / 100 * (elem2 / 100 + 1) * (toskill2 / 100 + 1));
                    skill = skill_base + skill_other;
                }
                //Считаем DPS при обычном расчёте
                else
                {
                    //lb_Debug.Items.Add("Тест норма");
                    damage1 = Convert.ToSingle(tb_damage1.Text.Replace(".", sep));
                    ac1 = Convert.ToSingle(tb_ac1.Text.Replace(".", sep));
                    damage2 = damage1;
                    ac2 = ac1;
                    if (tb_damage2.Text != "" && tb_damage2.Text.ToLower().Replace(" ", "") != "weapon2" && tb_ac2.Text != "" && tb_ac2.Text.ToLower() != "weapon2" && tb_ac2.Text != "0" && tb_damage2.Text != "0")
                    {
                        damage2 = Convert.ToSingle(tb_damage2.Text.Replace(".", sep));
                        ac2 = Convert.ToSingle(tb_ac2.Text.Replace(".", sep));
                    }
                    skill = Convert.ToSingle(tb_skill.Text.Replace(".", sep));
                }
                main = Convert.ToSingle(tb_main.Text.Replace(".", sep)) + Convert.ToSingle(nud_para_main.Value);
                acincr = Convert.ToSingle(nud_para_as.Value);
                if (tb_acincr.Text != "") acincr += Convert.ToSingle(tb_acincr.Text.Replace(".", sep));
                cc = Convert.ToSingle(tb_cc.Text.Replace(".", sep)) + Convert.ToSingle(nud_para_cc.Value);
                cd = Convert.ToSingle(tb_cd.Text.Replace(".", sep)) + Convert.ToSingle(nud_para_cd.Value);
                off_min = 0; off_max = 0; am_min = 0; am_max = 0; r1_min = 0; r1_max = 0; r2_min = 0; r2_max = 0; other_min = 0; other_max = 0;
                if (tb_off_min.Text != "") off_min = Convert.ToSingle(tb_off_min.Text.Replace(".", sep));
                if (tb_off_max.Text != "") off_max = Convert.ToSingle(tb_off_max.Text.Replace(".", sep));
                if (tb_am_min.Text != "") am_min = Convert.ToSingle(tb_am_min.Text.Replace(".", sep));
                if (tb_am_max.Text != "") am_max = Convert.ToSingle(tb_am_max.Text.Replace(".", sep));
                if (tb_r1_min.Text != "") r1_min = Convert.ToSingle(tb_r1_min.Text.Replace(".", sep));
                if (tb_r1_max.Text != "") r1_max = Convert.ToSingle(tb_r1_max.Text.Replace(".", sep));
                if (tb_r2_min.Text != "") r2_min = Convert.ToSingle(tb_r2_min.Text.Replace(".", sep));
                if (tb_r2_max.Text != "") r2_max = Convert.ToSingle(tb_r2_max.Text.Replace(".", sep));
                other_min = 0; other_max = 0;

                fromskills = 0; elem = 0; toskill = 0; elite = 0;
                if (tb_fromskills.Text != "") fromskills = Convert.ToSingle(tb_fromskills.Text.Replace(".", sep));
                if (tb_elem.Text != "") elem = Convert.ToSingle(tb_elem.Text.Replace(".", sep));
                if (tb_toskill.Text != "") toskill = Convert.ToSingle(tb_toskill.Text.Replace(".", sep));
                if (tb_elite.Text != "") elite = Convert.ToSingle(tb_elite.Text.Replace(".", sep));

                Calculate();

                lb_result.Text = result.ToString("N0");
                lb_result_profile.Text = result_profile.ToString("N0");
                lb_result.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                lb_result_profile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                result_old = result;

                //if (cb_wd.Checked)
                //Считаем урон питомцев WD
                if (lb_state.Text == "wd")
                {
                    CalculateWD();
                    lb_result_wd.Text = (result + result_wd).ToString("N0");
                    result_old_wd = result + result_wd;
                    pan_wddam.Visible = true;
                    lb_start.Text = lb_start.Text + "wd";
                }

                acincr = acincr + 1; lb_ac.Visible = true; Calculate(); lb_ac.Text = (result - result_old).ToString("N0"); acincr = acincr - 1;
                main = main + 100; lb_main.Visible = true; Calculate(); lb_main.Text = (result - result_old).ToString("N0"); main = main - 100;
                elite = elite + 1; lb_elite.Visible = true; Calculate(); lb_elite.Text = (result - result_old).ToString("N0"); elite = elite - 1;
                cc = cc + 1; lb_cc.Visible = true; Calculate(); lb_cc.Text = (result - result_old).ToString("N0"); cc = cc - 1;
                cd = cd + 10; lb_cd.Visible = true; Calculate(); lb_cd.Text = (result - result_old).ToString("N0"); cd = cd - 10;
                if (lb_adv.Text == "adv")
                {
                    elem1 = elem1 + 1;
                    lb_elem.Visible = true;
                    skill_base = (skill1 * skill1_usage / 100 * (elem1 / 100 + 1) * (toskill1 / 100 + 1));
                    skill_other = (skill2 * skill2_usage / 100 * (elem2 / 100 + 1) * (toskill2 / 100 + 1));
                    skill = (skill1 * skill1_usage / 100 * (elem1 / 100 + 1) * (toskill1 / 100 + 1)) + (skill2 * skill2_usage / 100 * (elem2 / 100 + 1) * (toskill2 / 100 + 1));
                    Calculate();
                    lb_elem.Text = (result - result_old).ToString("N0");
                    elem1 = elem1 - 1;
                }
                else
                {
                    elem = elem + 1;
                    lb_elem.Visible = true;
                    Calculate();
                    lb_elem.Text = (result - result_old).ToString("N0");
                    elem = elem - 1;
                }
                other_min = other_min + 20; lb_dmg.Visible = true; Calculate(); lb_dmg.Text = (result - result_old).ToString("N0"); other_min = other_min - 20;

                if (lb_state.Text == "wep" && lb_adv.Text == "adv")
                {

                    if (tb_dmg1_w.Text == "") tb_dmg1_w.Text = "0";
                    if (tb_dmg2_w.Text == "") tb_dmg2_w.Text = "0";
                    ac1w = Math.Round((ac1 * 100 / (100 + ac1_p)), 1) * ((100 + ac1_p + Convert.ToSingle(nud_acp_w.Value)) / 100);
                    damage1 = (dmg1_1 + dmg1_2 + Convert.ToSingle(tb_dmg1_w.Text.Replace(".", sep)) + Convert.ToSingle(tb_dmg2_w.Text.Replace(".", sep))) / 2 * ac1w * ((dmg1_p + Convert.ToSingle(nud_damp_w.Value)) / 100 + 1);

                    main = main + Convert.ToSingle(nud_main_w.Value);
                    elite = elite + Convert.ToSingle(nud_elite_w.Value);
                    cd = cd + Convert.ToSingle(nud_cd_w.Value);
                    elem1 = elem1 + Convert.ToSingle(nud_elem_w.Value);
                    skill = (skill1 * skill1_usage / 100 * (elem1 / 100 + 1) * (toskill1 / 100 + 1)) + (skill2 * skill2_usage / 100 * (elem2 / 100 + 1) * (toskill2 / 100 + 1));
                    Calculate();
                    lb_changed.Text = result.ToString("N0") + " (" + (result * 100 / result_old - 100).ToString("N2") + "%)";
                    lb_start.Text = lb_start.Text + "changed";
                }
                else
                    if (nud_main.Value != 0 || nud_ac.Value != 0 || nud_cc.Value != 0 || nud_cd.Value != 0 || nud_elem.Value != 0 || nud_damage.Value != 0 || nud_elite.Value != 0)
                    {
                        main = main + Convert.ToSingle(nud_main.Value);
                        elite = elite + Convert.ToSingle(nud_elite.Value);
                        acincr = acincr + Convert.ToSingle(nud_ac.Value);
                        cc = cc + Convert.ToSingle(nud_cc.Value);
                        cd = cd + Convert.ToSingle(nud_cd.Value);
                        if (lb_adv.Text == "adv")
                        {
                            skill_base = (skill1 * skill1_usage / 100 * ((elem1 + Convert.ToSingle(nud_elem.Value)) / 100 + 1) * (toskill1 / 100 + 1));
                            skill_other = (skill2 * skill2_usage / 100 * (elem2 / 100 + 1) * (toskill2 / 100 + 1));
                            skill = skill_base + skill_other;
                        }

                        else elem = elem + Convert.ToSingle(nud_elem.Value);
                        other_min = other_min + Convert.ToSingle(nud_damage.Value);
                        other_max = other_max + Convert.ToSingle(nud_damage.Value);
                        //if (cb_wd.Checked)
                        if (lb_state.Text == "wd")
                        {
                            Calculate();
                            CalculateWD();
                            lb_changed.Text = (result + result_wd).ToString("N0") + " (" + ((result + result_wd) * 100 / result_old_wd - 100).ToString("N2") + "%)";
                        }
                        else
                        {
                            Calculate();
                            lb_changed.Text = result.ToString("N0") + " (" + (result * 100 / result_old - 100).ToString("N2") + "%)";
                        }
                        //main = main - Convert.ToSingle(nud_main.Value);
                        lb_start.Text = lb_start.Text + "changed";
                    }
                    else lb_changed.Text = lng.lb_changedt;
                //main_change=(((damage1 / ac1) + (damage2 / ac2)) / 2) + ((off_min + off_max + am_min + am_max + r1_min + r1_max + r2_min + r2_max + other_min + other_max) / 2)
                if (lb_changed.Text != lng.lb_changedt) lb_changed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                else lb_changed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        public void Calculate()
        {
            wepdamage = ((damage1 / ac1) + (damage2 / ac2)) / 2;
            otherdamage = (off_min + off_max + am_min + am_max + r1_min + r1_max + r2_min + r2_max + other_min + other_max) / 2;
            cleardamage = (wepdamage + otherdamage) * (main / 100 + 1);
            critdamage = cleardamage * (cc / 100 * cd / 100 + 1);

            if (lb_adv.Text == "adv") overdamage = critdamage * (fromskills / 100 + 1) * (elite / 100 + 1);
            else overdamage = critdamage * (fromskills / 100 + 1) * (elem / 100 + 1) * (toskill / 100 + 1) * (elite / 100 + 1);

            speedtotal = 2 / (1 / ac1 + 1 / ac2) * (acincr / 100 + 1);
            result = overdamage * skill / 100 * speedtotal;
            if (coold != 0)
                if (lb_adv.Text == "adv")
                {
                    result = (overdamage * skill_base / 100 / coold) + (overdamage * skill_other / 100 * speedtotal);
                }
                else result = overdamage * skill / 100 / coold;

            result_profile = critdamage * speedtotal;
        }

        public void CalculateWD()
        {
            wepdamage_wd = ((damage1 / ac1) + (damage2 / ac2)) / 2;
            otherdamage_wd = (off_min + off_max + am_min + am_max + r1_min + r1_max + r2_min + r2_max + other_min + other_max) / 2;
            cleardamage_wd = (wepdamage_wd + otherdamage_wd) * (main / 100 + 1);
            critdamage_wd = cleardamage_wd * (cc / 100 * cd / 100 + 1);
            overdamage_wd = critdamage_wd * (fromskills / 100 + 1) * (Convert.ToSingle(nud_elemp.Value) / 100 + 1) * (elite / 100 + 1);
            skill_wd = (1 * (Convert.ToSingle(nud_garg.Value))) + (12 * (Convert.ToSingle(nud_dogs.Value))) + (180 * (Convert.ToSingle(nud_fet.Value)));
            speedtotal_wd = (ac1 + ac2) / 2 * (acincr / 100 + 1) * (Convert.ToSingle(nud_speedp.Value) / 100 + 1);
            result_wd = overdamage_wd * skill_wd / 100 * speedtotal_wd * (Convert.ToSingle(nud_damp.Value) / 100 + 1);
        }


        private void b_stat_Click(object sender, EventArgs e)
        {
            //if (pan_stat.Visible == false)
            //{
            //    pan_stat.Visible = true;
            //    pan_cdr.Visible = false;
            //    b_stat.Text = lng.b_statt_cdr;
            //    lb_stat.Text = "dps";
            //}
            //else
            //{
            //    pan_stat.Visible = false;
            //    pan_cdr.Visible = true;
            //    b_stat.Text = lng.b_statt_dps;
            //    lb_stat.Text = "cdr";
            //}
        }


        private void Key_Test(object sender, KeyEventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            decimal n; var tb = (TextBox)sender;
            if (decimal.TryParse((String)iData.GetData(DataFormats.Text), out n) == false && tb.Name != "tb_pers") Clipboard.SetDataObject("");
            if (e.KeyData == (Keys.Control | Keys.V))
            {
                if (tb.Name != "tb_pers") (sender as TextBox).Paste();
                if (iData.GetData(DataFormats.Text).ToString().Length > 0 && tb.Name == "tb_pers") b_web_Click(null, null);
            }
            if (tb.Name == "tb_pers" && tb.Text != "" && e.KeyData == Keys.Enter) b_web_Click(null, null);
            //if (tb.Name == "tb_pers" && tb.Text != "" && e.KeyData == (Keys.Control | Keys.V)) b_web_Click(null, null);
            //if (tb.Name == "tb_pers" && tb.Text != "" && ((e.KeyData == Keys.Enter) || (tb.Text.Contains("battle.net") && tb.Text.Contains("profile")))) b_web_Click(null, null);
        }

        private void b_wep_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(b_wep.Text.ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (lb_state.Text == "wd" || (lb_state.Text == "wep" && cb_wd.Checked == false))
            {
                lb_state.Text = "roll";
                b_wep.Text = lng.b_wept;
                pan_wep.Visible = false;
                pan_wd.Visible = false;
                pan_roll.Visible = true;
            }
            else
                if (lb_state.Text == "roll")
                {
                    lb_state.Text = "wep";
                    if (cb_wd.Checked) b_wep.Text = lng.b_wept_wd; else b_wep.Text = lng.b_wept_skill;
                    pan_wep.Visible = true;
                    pan_wd.Visible = false;
                }
                else
                    if (lb_state.Text == "wep" && cb_wd.Checked == true)
                    {
                        lb_state.Text = "wd";
                        b_wep.Text = lng.b_wept_skill;
                        pan_wep.Visible = false;
                        pan_wd.Visible = true;
                    }

        }

        private void dps_diablo_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void lb_auth_Click(object sender, EventArgs e)
        {
            if (bt_lang.Text == "ENG") System.Diagnostics.Process.Start("http://glasscannon.ru/forum/viewtopic.php?f=5&t=149");
            else System.Diagnostics.Process.Start("http://www.diablofans.com/forums/diablo-iii-general-forums/diablo-iii-general-discussion/89743-offline-dps-calculator-diablo-3");
        }

        private void lb_auth_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void lb_auth_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        public void Lang()
        {
            gb_result.Text = lng.gb_result;
            gb_increase.Text = lng.gb_increase;
            lb_import.Text = lng.lb_import;
            //lb_changes.Text = lng.lb_changes;
            gb_roll.Text = lng.lb_changes;
            tb_pers.Text = lng.tb_pers;
            lb_dmg_a.Text = lng.lb_dmg_at;
            lb_perc_a.Text = lng.lb_perc_at;
            lb_speed_a.Text = lng.lb_speed_at;
            lb_toskill_a.Text = lng.lb_toskill_at;
            lb_elem_a.Text = lng.lb_elem_at;
            lb_skill_a.Text = lng.lb_skill_at;
            lb_skill_usage_a.Text = lng.lb_skill_usage_at;
            //lb_wep.Text = lng.lb_wept;
            lb_dam_w.Text = lng.lb_dam_wt;
            lb_main_w.Text = lng.lb_main_wt;
            lb_damp_w.Text = lng.lb_damp_wt;
            lb_acp_w.Text = lng.lb_acp_wt;
            lb_cd_w.Text = lng.lb_cd_wt;
            lb_elem_w.Text = lng.lb_elem_wt;
            //lb_wd.Text = lng.lb_wdt;
            lb_garg.Text = lng.lb_gargt;
            lb_dogs.Text = lng.lb_dogst;
            lb_fet.Text = lng.lb_fett;
            lb_fizp.Text = lng.lb_fizpt;
            lb_damp.Text = lng.lb_dampt;
            lb_spp.Text = lng.lb_sppt;
            //lb_statc.Text = lng.lb_statct;
            lb_acc.Text = lng.lb_acct;
            lb_mainc.Text = lng.lb_mainct;
            lb_damc.Text = lng.lb_damct;
            lb_ccc.Text = lng.lb_ccct;
            lb_cdc.Text = lng.lb_cdct;
            lb_elemc.Text = lng.lb_elemct;
            //lb_help1.Text = lng.lb_help1t;
            //lb_help2.Text = lng.lb_help2t;
            //lb_help3.Text = lng.lb_help3t;
            //lb_help4.Text = lng.lb_help4t;
            //lb_help5.Text = lng.lb_help5t;
            //lb_help6.Text = lng.lb_help6t;
            //lb_help7.Text = lng.lb_help7t;
            //lb_help8.Text = lng.lb_help8t;
            lb_idmg1.Text = lng.lb_idmg1t;
            lb_iac1.Text = lng.lb_iac1t;
            lb_icc.Text = lng.lb_icct;
            lb_icd.Text = lng.lb_icdt;
            lb_imain.Text = lng.lb_imaint;
            lb_iasi.Text = lng.lb_iasit;
            lb_ioff.Text = lng.lb_iofft;
            lb_iamu.Text = lng.lb_iamut;
            lb_ir1.Text = lng.lb_ir1t;
            lb_ir2.Text = lng.lb_ir2t;
            lb_ifromskills.Text = lng.lb_ifromskillst;
            lb_ielite.Text = lng.lb_ielitet;
            lb_itoskill.Text = lng.lb_itoskillt;
            lb_ielem.Text = lng.lb_ielemt;
            lb_iskill.Text = lng.lb_iskillt;
            lb_dpsp.Text = lng.lb_dpspt;
            lb_dps.Text = lng.lb_dpst;
            lb_dpsr.Text = lng.lb_dpsrt;
            lb_dpspets.Text = lng.lb_dpspetst;
            if (lb_start.Text == "calc") lb_result_profile.Text = lng.lb_result_profilet;
            if (lb_start.Text == "calc") lb_result.Text = lng.lb_resultt;
            if (lb_start.Text.IndexOf("changed") < 0) lb_changed.Text = lng.lb_changedt;
            if (lb_start.Text.IndexOf("wd") < 0) lb_result_wd.Text = lng.lb_result_wdt;
            if (lb_adv.Text == "def") b_adv.Text = lng.b_advt;
            if (lb_adv.Text == "adv") b_adv.Text = lng.b_advt_def;
            //if (lb_stat.Text == "hlp") b_stat.Text = lng.b_statt;
            if (lb_stat.Text == "dps") b_stat.Text = lng.b_statt_cdr;
            if (lb_stat.Text == "cdr") b_stat.Text = lng.b_statt_dps;
            if (lb_state.Text == "roll") b_wep.Text = lng.b_wept;
            if (lb_state.Text == "wd") b_wep.Text = lng.b_wept_skill;
            if (lb_state.Text == "wep") b_wep.Text = lng.b_wept_skill;
            if (lb_state.Text == "wep" && cb_wd.Checked == true) b_wep.Text = lng.b_wept_wd;
            cb_wd.Text = lng.cb_wdt;
            b_start.Text = lng.b_startt;
            //b_save.Text = lng.b_savet;
            //b_load.Text = lng.b_loadt;
            //b_clear.Text = lng.b_cleart;
            lb_auth.Text = lng.lb_autht;
            lb_as_dps.Text = lng.lb_as_dpst;
            lb_stat_dps.Text = lng.lb_stat_dpst;
            lb_cc_dps.Text = lng.lb_cc_dpst;
            lb_cd_dps.Text = lng.lb_cd_dpst;
            lb_elem_dps.Text = lng.lb_elem_dpst;
            lb_dmg_dps.Text = lng.lb_dmg_dpst;
            lb_elite_dps.Text = lng.lb_elite_dpst;
            lb_eliteс.Text = lng.lb_eliteс;
            lb_elite_w.Text = lng.lb_eliteс;

            lb_para_main.Text = lng.lb_imaint + ":";
            lb_para_as.Text = lng.lb_as + ":";
            lb_para_cdr.Text = lng.lb_cdr + ":";
            lb_para_cc.Text = lng.lb_icct + ":";
            lb_para_cd.Text = lng.lb_icdt + ":";
            lb_para.Text = lng.lb_paragon;

            action_choice();
            tooltips();

        }

        private void bt_lang_Click(object sender, EventArgs e)
        {
            if (bt_lang.Text == "RUS")
            {
                bt_lang.Text = "ENG";
                lng.Lang_rus();
            }
            else
            {
                bt_lang.Text = "RUS";
                lng.Lang_eng();
            }
            Lang();
            this.Refresh();
        }

        public void tooltips()
        {
            //tt_filesave.SetToolTip(b_filesave, lng.tt1t);
            //tt_fileload.SetToolTip(b_fileload, lng.tt2t);
            //tt_save.SetToolTip(b_save, lng.tt39t);
            //tt_load.SetToolTip(b_load, lng.tt40t);
            //tt_clear.SetToolTip(b_clear, lng.tt3t);
            toolTip1.SetToolTip(lb_idmg1, lng.tt4t);
            toolTip2.SetToolTip(lb_iac1, lng.tt5t);
            toolTip3.SetToolTip(lb_imain, lng.tt6t);
            toolTip4.SetToolTip(lb_iasi, (lng.tt7t + "\n" + lng.tt7_1t));
            toolTip5.SetToolTip(lb_icc, lng.tt8t);
            toolTip6.SetToolTip(lb_icd, lng.tt9t);
            toolTip7.SetToolTip(lb_ioff, lng.tt10t);
            toolTip8.SetToolTip(lb_iamu, lng.tt11t);
            toolTip9.SetToolTip(lb_ir1, lng.tt12t);
            toolTip10.SetToolTip(lb_ir2, lng.tt13t);
            toolTip12.SetToolTip(lb_ifromskills, lng.tt14t);
            toolTip13.SetToolTip(lb_ielem, lng.tt15t);
            toolTip14.SetToolTip(lb_itoskill, lng.tt16t);
            toolTip15.SetToolTip(lb_ielite, lng.tt17t);
            toolTip16.SetToolTip(lb_iskill, lng.tt18t);
            toolTip17.SetToolTip(lb_dmg_a, lng.tt19t);
            toolTip18.SetToolTip(lb_perc_a, lng.tt20t);
            toolTip19.SetToolTip(lb_skill_a, lng.tt21t);
            toolTip20.SetToolTip(lb_skill_usage_a, lng.tt22t);
            toolTip11.SetToolTip(lb_speed_a, lng.tt23t);
            toolTip21.SetToolTip(lb_toskill_a, lng.tt24t);
            toolTip22.SetToolTip(lb_elem_a, lng.tt25t);
            toolTip23.SetToolTip(lb_dam_w, lng.tt26t);
            toolTip24.SetToolTip(lb_main_w, lng.tt27t);
            toolTip25.SetToolTip(lb_damp_w, lng.tt28t);
            toolTip26.SetToolTip(lb_acp_w, lng.tt29t);
            toolTip27.SetToolTip(lb_cd_w, lng.tt30t);
            toolTip28.SetToolTip(lb_elem_w, lng.tt31t);
            toolTip28.SetToolTip(lb_auth, lng.tt32t);
            toolTip29.SetToolTip(lb_cooldown, lng.tt33t);
            toolTip30.SetToolTip(lb_cdr12, lng.tt34t);
            toolTip31.SetToolTip(lb_cdr34, lng.tt35t);
            toolTip32.SetToolTip(lb_cdr56, lng.tt36t);
            toolTip33.SetToolTip(lb_cdr78, lng.tt37t);
            toolTip34.SetToolTip(lb_cdr910, lng.tt38t);
            toolTip35.SetToolTip(b_web, lng.tt41t);
        }

        private void nud_cdr_ValueChanged(object sender, EventArgs e)
        {
            cdr_all = 100;
            lb_cdr.Visible = false;
            foreach (NumericUpDown numud in pan_cdr.Controls.OfType<NumericUpDown>())
                if (numud.Value != 0 && numud.Name != "nud_cooldown") cdr_all = cdr_all * (100 - Convert.ToSingle(numud.Value)) / 100;
            coold = Convert.ToSingle(nud_cooldown.Value) * cdr_all / 100;
            lb_cdr.Text = (cdr_all).ToString("N2") + "%";
            lb_cdt.Text = coold.ToString("N2").Replace(sep, ".") + "s";
            if (cdr_all != 100) lb_cdr.Visible = true;
            if (nud_cooldown.Value != 0) lb_cdt.Visible = true;
        }

        private void cooldown_import()
        {
            nud_cdr1.Value = Convert.ToDecimal(cdr[0]);
            nud_cdr2.Value = Convert.ToDecimal(cdr[1]);
            nud_cdr3.Value = Convert.ToDecimal(cdr[2]);
            nud_cdr4.Value = Convert.ToDecimal(cdr[3]);
            nud_cdr5.Value = Convert.ToDecimal(cdr[4]);
            nud_cdr6.Value = Convert.ToDecimal(cdr[5]);
            nud_cdr7.Value = Convert.ToDecimal(cdr[6]);
            nud_cdr8.Value = Convert.ToDecimal(cdr[7]);
            nud_cdr9.Value = Convert.ToDecimal(cdr[8]);
            nud_cdr10.Value = Convert.ToDecimal(cdr[9]);
            nud_cdr12.Value = coold_pass;
            int flag = 0;
            for (int i = 0; i < 10; i++)
            {
                if (cdr[i] != 0) flag = 1;
            }
            if (flag != 0 || coold_pass > 0) nud_cdr_ValueChanged(null, null);
        }

        private void b_filesave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Xml Files|*.xml";
            saveFileDialog1.Title = "Select a xml File to save";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //MessageBox.Show(openFileDialog1.FileName.ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);
                Save_settings();
                WriteXML(saveFileDialog1.FileName.ToString());
            }
        }

        private void dps_diablo_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F5)
            //{
            //    b_save.PerformClick();
            //}
            //if (e.KeyCode == Keys.F6)
            //{
            //    b_load.PerformClick();
            //}
            //if (e.KeyCode == Keys.F7)
            //{
            //    b_clear.PerformClick();
            //}
            if ((e.KeyCode == Keys.Escape || e.KeyCode == Keys.F1 || e.KeyCode == Keys.Enter) && faq_flag == 1)
            {
                faq(null, null);
                // b_faq1.PerformClick();
            }
        }


        private StreamReader webresp(string param)
        {
            string host = "eu.battle.net";
            if (tb_pers.Text.Contains("us.")) host = "us.battle.net";
            System.Net.WebRequest req_item = System.Net.WebRequest.Create(@"http://" + host + "/api/d3/data/" + param);

            if (b_parse.Visible)
            {
                WebProxy myProxy = new WebProxy("localhost", 4001);
                myProxy.BypassProxyOnLocal = true;
                req_item.Proxy = myProxy;
            }
            else
            {
                IWebProxy myProxy = WebRequest.DefaultWebProxy;
                myProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                req_item.Proxy = myProxy;
            }

            System.Net.WebResponse resp = req_item.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
           
            return sr;
        }

        private void web_req()
        {
            string host = "eu.battle.net";
            if (tb_pers.Text.Contains("us.battle.net")) host = "us.battle.net";

            string battletag_name = "";
            string battletag_code = "";
            string hero_id = "";

            int g = 0, h = 0;
            string[] prof_num;
            string[] pars_str = tb_pers.Text.Split('/');  //парсим строку и получаем стринговый массив
            for (int i = 0; i < pars_str.Length; i++)
            {
                if (pars_str[i].Contains("profile")) g = i + 1;
                if (pars_str[i].Contains("hero") && i <= pars_str.Length - 1) h = i + 1;

            }

            if (g != 0)
            {
                prof_num = pars_str[g].Split('-');
                battletag_name = prof_num[0];
                battletag_code = prof_num[1];
            }
            if (h != 0) hero_id = pars_str[h];

            if (battletag_name.ToString() == "" || battletag_code.ToString() == "" || hero_id.ToString() == "")
                MessageBox.Show(lng.mess_imp, lng.warn, MessageBoxButtons.OK, MessageBoxIcon.Question);

            else
            {
                System.Net.WebRequest req_hero = System.Net.WebRequest.Create(@"http://" + host + "/api/d3/profile/" + battletag_name + "-" + battletag_code + "/hero/" + hero_id);

                if (b_parse.Visible)
                {
                    WebProxy myProxy = new WebProxy("localhost", 4001);
                    myProxy.BypassProxyOnLocal = true;
                    req_hero.Proxy = myProxy;
                }
                else
                {
                    IWebProxy myProxy = WebRequest.DefaultWebProxy;
                    myProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                    req_hero.Proxy = myProxy;
                }

                System.Net.WebResponse resp = req_hero.GetResponse();
                System.IO.Stream stream = resp.GetResponseStream();
                System.IO.StreamReader sr = new System.IO.StreamReader(stream);
                hero_parse = sr.ReadToEnd();

                if (hero_parse.Contains("NOTFOUND")) MessageBox.Show(lng.mess_nopers, lng.warn, MessageBoxButtons.OK, MessageBoxIcon.Question);
                else if (!(hero_parse.Contains("completedQuests"))) MessageBox.Show(lng.mess_noint, lng.warn, MessageBoxButtons.OK, MessageBoxIcon.Question);
                else
                {
                    skill_damage = ""; phys_up = ""; dps_min = ""; aps_item = ""; dps_min_off = ""; phys_up_off = ""; aps_item_off = "";

                    pars_hero = hero_parse.Split('\n');  //парсим строку и получаем стринговый массив

                    int desc = 0;
                    first_skill = "";
                    cnt = 0; to_skl = 0;

                    for (int i = 0; i < pars_hero.Length; i++)
                    {
                        if (pars_hero[i].Contains("description"))
                        {
                            desc += 1;
                            if (pars_hero[i].Contains("% weapon damage") || pars_hero[i].Contains("% of your weapon"))
                            {
                                int kkk = pars_hero[i].IndexOf("% weapon damage");
                                if (!(kkk > 0)) kkk = pars_hero[i].IndexOf("% of your weapon");
                                if (pars_hero[i].Substring(kkk - 5, 1) == " ")
                                    skill_damage = pars_hero[i].Substring(kkk - 4, 4).Trim();
                                else
                                    skill_damage = pars_hero[i].Substring(kkk - 3, 3).Trim();
                            }
                            if (desc > 1 && skill_damage != "") break;
                        }
                    }

                    int skillnum = 0;
                    passive_skill = new List<string>() { };
                    active_skill = new List<string>() { };
                    active_skill_rune = new List<string>() { };

                    for (int i = 0; i < pars_hero.Length; i++)
                    {
                        if (pars_hero[i].Contains("name"))
                        {
                            skillnum += 1;
                            if (skillnum % 2 == 0 && skillnum < 14) active_skill.Add(pars_hero[i].Trim().Replace("\"name\" : \"", "").Replace("\",", ""));
                            if (skillnum % 2 != 0 && skillnum > 1 && skillnum < 14) active_skill_rune.Add(pars_hero[i].Trim().Replace("\"name\" : \"", "").Replace("\",", ""));
                            if (skillnum > 13 && skillnum < 18) passive_skill.Add(pars_hero[i].Trim().Replace("\"name\" : \"", "").Replace("\",", ""));
                        }
                    }


                    //MessageBox.Show(active_skill[0].Trim().Replace(" ","-"));

                    byte[] byteArray = Encoding.UTF8.GetBytes(hero_parse);
                    MemoryStream TheStream = new MemoryStream(byteArray);
                    DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Parse));
                    Parse pers = (Parse)json.ReadObject(TheStream);

                    mainstat = Convert.ToInt16(pers.stats.strength);
                    if (mainstat < Convert.ToInt16(pers.stats.intelligence)) mainstat = Convert.ToInt16(pers.stats.intelligence);
                    if (mainstat < Convert.ToInt16(pers.stats.dexterity)) mainstat = Convert.ToInt16(pers.stats.dexterity);
                    //tb_main.Text = mainstat.ToString();

                    del_Physical = 0; del_X1_Physical = 0; del_Fire = 0; del_Cold = 0; del_Lightning = 0; del_Poison = 0; del_Arcane = 0; del_Holy = 0
                    ; min_Physical = 0; min_X1_Physical = 0; min_Fire = 0; min_Cold = 0; min_Lightning = 0; min_Poison = 0; min_Arcane = 0; min_Holy = 0
                    ; elem_Physical = 0; elem_Fire = 0; elem_Cold = 0; elem_Lightning = 0; elem_Poison = 0; elem_Arcane = 0; elem_Holy = 0
                    ; damage_min = 0; damage_max = 0; elem_all = 0; aps = 0; aps_up = 0; aps_up_off = 0; aps_off = 0; damage_min_off = 0; damage_max_off = 0
                    ; crit_damage = 0; crit_chance = 0; elite_damage = 0; off_crit_chance = 0; off_del_Physical = 0; off_min_Physical = 0
                    ; rings_count = 0; r1_del_Physical = 0; r1_min_Physical = 0; r2_del_Physical = 0; r2_min_Physical = 0; am_del_Physical = 0; am_min_Physical = 0
                    ; from_skl = 0; coold_pass = 0;

                    //head_parse = ""; torso_parse = ""; feet_parse = ""; hands_parse = ""; shoulders_parse = ""; legs_parse = ""; bracers_parse = ""; mainHand_parse = ""; offHand_parse = ""; waist_parse = ""; rightFinger_parse = ""; leftFinger_parse = ""; neck_parse = "";

                    set_parse();
                    pers_name = pers.name + " - " + pers.paragonLevel;
                    para_lvl = pers.paragonLevel;

                    if (pers.items.mainHand != null)
                    {
                        mainHand = pers.items.mainHand.tooltipParams;
                        mainHand_parse = webresp(mainHand).ReadToEnd();
                        if (mainHand_parse.Contains("Critical Hit Damage Increased by 130")) crit_damage = crit_damage + 130;
                        if (mainHand_parse.Contains("Critical Hit Damage Increased by 125")) crit_damage = crit_damage + 125;
                        if (mainHand_parse.Contains("Critical Hit Damage Increased by 120")) crit_damage = crit_damage + 120;
                        if (mainHand_parse.Contains("Critical Hit Damage Increased by 115")) crit_damage = crit_damage + 115;
                        if (mainHand_parse.Contains("Critical Hit Damage Increased by 110")) crit_damage = crit_damage + 110;
                        if (mainHand_parse.Contains("Flawless Royal Diamond")) elite_damage = elite_damage + 20;
                        if (mainHand_parse.Contains("Royal Diamond")) elite_damage = elite_damage + 19;
                        if (mainHand_parse.Contains("Flawless Imperial Diamond")) elite_damage = elite_damage + 18;
                        if (mainHand_parse.Contains("Imperial Diamond")) elite_damage = elite_damage + 17;
                        if (mainHand_parse.Contains("Marquise Diamond")) elite_damage = elite_damage + 16;
                        mh();
                    }


                    foreach (string pass in passive_skill)
                    {
                        //Barbarian
                        if (pass == "Berserker Rage") from_skl += 25;
                        if (pass == "No Escape") from_skl += 25;
                        if (pass == "Brawler") from_skl += 20;
                        if (pass == "Weapons Master" && (weapon_id.Contains("Sword") || weapon_id.Contains("Dagger"))) from_skl += 8;
                        if (pass == "Weapons Master" && (weapon_id.Contains("Mace") || weapon_id.Contains("Axe"))) off_crit_chance += 5;
                        if (pass == "Weapons Master" && (weapon_id.Contains("Polearm") || weapon_id.Contains("Spear"))) aps_up += 8;
                        //Crusader
                        if (pass == "Blunt" || pass == "Towering Shield") from_skl += 20;
                        if (pass == "Holy Cause") from_skl += 10;
                        if (pass == "Fanaticism") aps_up += 15;
                        if (pass == "Fervor")
                        {
                            aps_up += 15;
                            coold_pass += 15;
                        }
                        if (pass == "Lord Commander") coold_pass += 35;
                        //Demon Hunter
                        if (pass == "Steady Aim" || pass == "Cull the Weak") from_skl += 20;
                        if (pass == "Archery" && weapon_id.Contains("Bow")) from_skl += 8;
                        if (pass == "Archery" && weapon_id.Contains("Crossbow") && weapon_2h.Contains("true")) crit_damage += 50;
                        if (pass == "Archery" && weapon_id.Contains("HandXbow") && !weapon_2h.Contains("true")) off_crit_chance += 5;
                        //Monk
                        if (pass == "Momentum") from_skl += 20;
                        if (pass == "Beacon of Ytar") coold_pass += 20;
                        //WD
                        if (pass == "Pierce the Veil") from_skl += 20;
                        //Wizard
                        if (pass == "Glass Cannon") from_skl += 15;
                        if (pass == "Audacity") from_skl += 15;
                        if (pass == "Cold Blooded") from_skl += 10;
                        if (pass == "Conflagration") off_crit_chance += 6;
                        if (pass == "Evocation") coold_pass += 20;
                    }

                    foreach (string act in active_skill)
                    {
                        //Barbarian
                        if (act == "Battle Rage")
                        {
                            int skl = 10;
                            off_crit_chance += 3;
                            foreach (string rune in active_skill_rune) if (rune == "Marauder's Rage") skl = 15;
                            from_skl += skl;
                        }
                        //Crusader
                        if (act == "Laws of Valor") aps_up += 8;
                        //Demon Hunter
                        if (act == "Marked for Death") from_skl += 20;
                        //Monk
                        if (act == "Mantra of Conviction")
                        {
                            int skl = 10;
                            foreach (string rune in active_skill_rune) if (rune == "Overawe") skl = 16;
                            from_skl += skl;
                        }
                        if (act == "Mystic Ally") foreach (string rune in active_skill_rune) if (rune == "Fire Ally") from_skl += 10;
                        //Wizard
                        if (act == "Magic Weapon")
                        {
                            int skl = 10;
                            foreach (string rune in active_skill_rune) if (rune == "Force Weapon") skl = 20;
                            from_skl += skl;
                        }
                        if (act == "Familiar") foreach (string rune in active_skill_rune) if (rune == "Sparkflint") from_skl += 10;
                        if (act == "Energy Armor") foreach (string rune in active_skill_rune) if (rune == "Pinpoint Barrier") off_crit_chance += 5;
                    }

                    if (pers.items.offHand != null)
                    {
                        offHand = pers.items.offHand.tooltipParams;
                        offHand_parse = webresp(offHand).ReadToEnd();
                        if (offHand_parse.Contains("Critical Hit Damage Increased by 130")) crit_damage = crit_damage + 130;
                        if (offHand_parse.Contains("Critical Hit Damage Increased by 125")) crit_damage = crit_damage + 125;
                        if (offHand_parse.Contains("Critical Hit Damage Increased by 120")) crit_damage = crit_damage + 120;
                        if (offHand_parse.Contains("Critical Hit Damage Increased by 115")) crit_damage = crit_damage + 115;
                        if (offHand_parse.Contains("Critical Hit Damage Increased by 110")) crit_damage = crit_damage + 110;
                        if (offHand_parse.Contains("Flawless Royal Diamond")) elite_damage = elite_damage + 20;
                        else if (offHand_parse.Contains("Royal Diamond")) elite_damage = elite_damage + 19;
                        if (offHand_parse.Contains("Flawless Imperial Diamond")) elite_damage = elite_damage + 18;
                        else if (offHand_parse.Contains("Imperial Diamond")) elite_damage = elite_damage + 17;
                        if (offHand_parse.Contains("Marquise Diamond")) elite_damage = elite_damage + 16;
                        offh(offHand_parse);
                    }

                    if (pers.items.head != null)
                    {
                        head = pers.items.head.tooltipParams;
                        head_parse = webresp(head).ReadToEnd();
                        offh(head_parse);
                    }

                    if (pers.items.torso != null)
                    {
                        torso = pers.items.torso.tooltipParams;
                        torso_parse = webresp(torso).ReadToEnd();
                        offh(torso_parse);
                    }

                    if (pers.items.feet != null)
                    {
                        feet = pers.items.feet.tooltipParams;
                        feet_parse = webresp(feet).ReadToEnd();
                        offh(feet_parse);
                    }

                    if (pers.items.hands != null)
                    {
                        hands = pers.items.hands.tooltipParams;
                        hands_parse = webresp(hands).ReadToEnd();
                        offh(hands_parse);
                    }

                    if (pers.items.shoulders != null)
                    {
                        shoulders = pers.items.shoulders.tooltipParams;
                        shoulders_parse = webresp(shoulders).ReadToEnd();
                        offh(shoulders_parse);
                    }

                    if (pers.items.legs != null)
                    {
                        legs = pers.items.legs.tooltipParams;
                        legs_parse = webresp(legs).ReadToEnd();
                        offh(legs_parse);
                    }

                    if (pers.items.bracers != null)
                    {
                        bracers = pers.items.bracers.tooltipParams;
                        bracers_parse = webresp(bracers).ReadToEnd();
                        offh(bracers_parse);
                    }

                    if (pers.items.waist != null)
                    {
                        waist = pers.items.waist.tooltipParams;
                        waist_parse = webresp(waist).ReadToEnd();
                        offh(waist_parse);
                    }

                    if (pers.items.rightFinger != null)
                    {
                        rightFinger = pers.items.rightFinger.tooltipParams;
                        rightFinger_parse = webresp(rightFinger).ReadToEnd();
                        offh(rightFinger_parse);
                    }

                    if (pers.items.leftFinger != null)
                    {
                        leftFinger = pers.items.leftFinger.tooltipParams;
                        leftFinger_parse = webresp(leftFinger).ReadToEnd();
                        offh(leftFinger_parse);
                    }

                    if (pers.items.neck != null)
                    {
                        neck = pers.items.neck.tooltipParams;
                        neck_parse = webresp(neck).ReadToEnd();
                        offh(neck_parse);
                    }

                    if (b_parse.Visible)
                    {
                        string path = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\WriteText1.txt";
                        System.IO.File.WriteAllText(path, hero_parse);

                        string path_head = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\head.txt";
                        string path_torso = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\torso.txt";
                        string path_feet = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\feet.txt";
                        string path_hands = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\hands.txt";
                        string path_shoulders = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\shoulders.txt";
                        string path_legs = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\legs.txt";
                        string path_bracers = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\bracers.txt";
                        string path_mainHand = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\mainHand.txt";
                        string path_offHand = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\offHand.txt";
                        string path_waist = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\waist.txt";
                        string path_rightFinger = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\rightFinger.txt";
                        string path_leftFinger = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\leftFinger.txt";
                        string path_neck = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\neck.txt";


                        System.IO.File.WriteAllText(path_head, head_parse);
                        System.IO.File.WriteAllText(path_torso, torso_parse);
                        System.IO.File.WriteAllText(path_feet, feet_parse);
                        System.IO.File.WriteAllText(path_hands, hands_parse);
                        System.IO.File.WriteAllText(path_shoulders, shoulders_parse);
                        System.IO.File.WriteAllText(path_legs, legs_parse);
                        System.IO.File.WriteAllText(path_bracers, bracers_parse);
                        System.IO.File.WriteAllText(path_mainHand, mainHand_parse);
                        System.IO.File.WriteAllText(path_offHand, offHand_parse);
                        System.IO.File.WriteAllText(path_waist, waist_parse);
                        System.IO.File.WriteAllText(path_rightFinger, rightFinger_parse);
                        System.IO.File.WriteAllText(path_leftFinger, leftFinger_parse);
                        System.IO.File.WriteAllText(path_neck, neck_parse);
                    }

                }

                //req_hero.GetResponse().Close();
                //resp.GetResponseStream().Close();
            }
        }

        public void mh()
        {

            byte[] byteArray = Encoding.UTF8.GetBytes(mainHand_parse.Replace('#', '_'));
            MemoryStream TheStream = new MemoryStream(byteArray);
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(MainHand));
            MainHand mh = (MainHand)json.ReadObject(TheStream);

            cdr = new Double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            cdr_n = 0;

            string[] pars_mh = mainHand_parse.Split('\n');  //парсим строку и получаем стринговый массив

            for (int i = 0; i < pars_mh.Length; i++)
            {
                if (pars_mh[i].Contains("Reduces cooldown of all skills by"))
                {
                    int start = pars_mh[i].IndexOf(@"Reduces cooldown of all skills by") + 34;
                    int end = pars_mh[i].IndexOf(@"%");
                    try { cdr[cdr_n] = Convert.ToDouble(pars_mh[i].Substring(start, end - start).Replace(".", sep)); } catch { }
                    break;
                }
            }

            weapon_id = mh.type.id;
            weapon_2h = mh.type.twoHanded;

            //----------Weapon(min/max)----------//

            if (mh.attributesRaw.Damage_Weapon_Min_Physical != null) min_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Physical.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Bonus_Min_X1_Physical != null) min_X1_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Bonus_Min_X1_Physical.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Min_Fire != null) min_Fire = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Fire.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Min_Cold != null) min_Cold = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Cold.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Min_Lightning != null) min_Lightning = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Lightning.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Min_Poison != null) min_Poison = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Poison.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Min_Arcane != null) min_Arcane = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Arcane.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Min_Holy != null) min_Holy = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Holy.min.Replace(".", sep)), 2);
            
            if (mh.attributesRaw.Damage_Weapon_Delta_Physical != null) del_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Physical.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Bonus_Delta_X1_Physical != null) del_X1_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Bonus_Delta_X1_Physical.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Delta_Fire != null) del_Fire = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Fire.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Delta_Cold != null) del_Cold = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Cold.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Delta_Lightning != null) del_Lightning = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Lightning.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Delta_Poison != null) del_Poison = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Poison.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Delta_Arcane != null) del_Arcane = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Arcane.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Delta_Holy != null) del_Holy = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Holy.min.Replace(".", sep)), 2);

            damage_min = Math.Round(min_Physical + min_X1_Physical + min_Fire + min_Cold + min_Lightning + min_Poison + min_Arcane + min_Holy);
            damage_max = Math.Round(del_Physical + del_X1_Physical + del_Fire + del_Cold + del_Lightning + del_Poison + del_Arcane + del_Holy + min_Physical + min_X1_Physical + min_Fire + min_Cold + min_Lightning + min_Poison + min_Arcane + min_Holy);

            if (mh.dps != null) dps_min = mh.dps.min;
            if (mh.attributesRaw.Damage_Weapon_Percent_Bonus_Physical != null)
            {
                phys_up = mh.attributesRaw.Damage_Weapon_Percent_Bonus_Physical.min;
                damage_min = Math.Round(damage_min * (1 + Math.Round(Convert.ToDouble(phys_up.Replace(".", sep)), 2)));
                damage_max = Math.Round(damage_max * (1 + Math.Round(Convert.ToDouble(phys_up.Replace(".", sep)), 2)));
            }

            //----------Elemental damage up----------//

            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Physical != null) elem_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Physical.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Fire != null) elem_Fire = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Fire.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Cold != null) elem_Cold = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Cold.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Lightning != null) elem_Lightning = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Lightning.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Poison != null) elem_Poison = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Poison.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Arcane != null) elem_Arcane = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Arcane.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Holy != null) elem_Holy = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Holy.min.Replace(".", sep)), 2);

            elem_all = elem_all + Math.Round((elem_Physical + elem_Fire + elem_Cold + elem_Lightning + elem_Poison + elem_Arcane + elem_Holy) * 100);

            //----------Attacks Per Second----------//

            if (mh.attacksPerSecond != null) aps = Math.Round(Convert.ToDouble(mh.attacksPerSecond.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Attacks_Per_Second_Item_Percent != null)  aps_item = mh.attributesRaw.Attacks_Per_Second_Item_Percent.min;

            //----------Critical damage----------//

            if (mh.attributesRaw.Crit_Damage_Percent != null) crit_damage = crit_damage + Math.Round(Convert.ToDouble(mh.attributesRaw.Crit_Damage_Percent.min.Replace(".", sep)), 2);

            //----------Damage vs elites----------//

            if (mh.attributesRaw.Damage_Percent_Bonus_Vs_Elites != null) elite_damage = elite_damage + Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Percent_Bonus_Vs_Elites.min.Replace(".", sep))*100, 2);

            del_Physical = 0; del_X1_Physical = 0; del_Fire = 0; del_Cold = 0; del_Lightning = 0; del_Poison = 0; del_Arcane = 0; del_Holy = 0;
            min_Physical = 0; min_X1_Physical = 0; min_Fire = 0; min_Cold = 0; min_Lightning = 0; min_Poison = 0; min_Arcane = 0; min_Holy = 0;

        }

        //--------------------------------------------//

        public void offh(string forparse)
        {

            //if (forparse == head_parse) MessageBox.Show(head_parse.Length.ToString());

            int cdr_flag = 0;

            string [] pars_item = forparse.Split('\n');  //парсим строку и получаем стринговый массив

            for (int i = 0; i < pars_item.Length; i++)
            {
                if (pars_item[i].Contains("Captain Crimson's") || pars_item[i].Contains("Born's")) cdr_flag = 1;
                if (pars_item[i].Contains("Reduces cooldown of all skills by") && (cdr_flag == 0 || !pars_item[i].Contains("10.0%")))
                {
                    cdr_n += 1;
                    int start = pars_item[i].IndexOf(@"Reduces cooldown of all skills by") + 34;
                    int end = pars_item[i].IndexOf(@"%");
                    try {cdr[cdr_n] = Convert.ToDouble(pars_item[i].Substring(start, end - start).Replace(".", sep));} catch { }
                }
                if (pars_item[i].Contains("Damage by"))
                {
                    foreach (string a_skill in active_skill)
                    {
                        if (pars_item[i].Contains(active_skill[0]) && pars_item[i].Contains(@"%"))
                        {
                            cnt += 1;
                            if (cnt == 1) first_skill = a_skill;
                            if (a_skill == first_skill)
                            {
                                int kkk = pars_item[i].IndexOf(@"%");
                                //MessageBox.Show(a_skill + " " + to_skl.ToString() + " --- " + pars_item[i].Substring(kkk - 3, 3).Trim());
                                to_skl = to_skl + Convert.ToInt16(pars_item[i].Substring(kkk - 2, 2).Trim());
                            }
                        }
                    }
                }
            }

            elem_Physical = 0; elem_Fire = 0; elem_Cold = 0; elem_Lightning = 0; elem_Poison = 0; elem_Arcane = 0; elem_Holy = 0; 

            byte[] byteArray = Encoding.UTF8.GetBytes(forparse.Replace('#', '_'));
            MemoryStream TheStream = new MemoryStream(byteArray);
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(MainHand));
            MainHand mh = (MainHand)json.ReadObject(TheStream);

            //----------Weapon(min/max)----------//

            if (mh.attributesRaw.Damage_Weapon_Min_Physical != null) min_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Physical.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Bonus_Min_X1_Physical != null) min_X1_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Bonus_Min_X1_Physical.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Min_Fire != null) min_Fire = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Fire.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Min_Cold != null) min_Cold = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Cold.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Min_Lightning != null) min_Lightning = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Lightning.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Min_Poison != null) min_Poison = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Poison.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Min_Arcane != null) min_Arcane = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Arcane.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Min_Holy != null) min_Holy = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Min_Holy.min.Replace(".", sep)), 2);

            if (mh.attributesRaw.Damage_Weapon_Delta_Physical != null) del_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Physical.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Bonus_Delta_X1_Physical != null) del_X1_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Bonus_Delta_X1_Physical.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Delta_Fire != null) del_Fire = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Fire.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Delta_Cold != null) del_Cold = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Cold.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Delta_Lightning != null) del_Lightning = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Lightning.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Delta_Poison != null) del_Poison = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Poison.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Delta_Arcane != null) del_Arcane = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Arcane.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Weapon_Delta_Holy != null) del_Holy = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Delta_Holy.min.Replace(".", sep)), 2);

            damage_min_off = Math.Round(min_Physical + min_X1_Physical + min_Fire + min_Cold + min_Lightning + min_Poison + min_Arcane + min_Holy);
            damage_max_off = Math.Round(del_Physical + del_X1_Physical + del_Fire + del_Cold + del_Lightning + del_Poison + del_Arcane + del_Holy + min_Physical + min_X1_Physical + min_Fire + min_Cold + min_Lightning + min_Poison + min_Arcane + min_Holy);

            if (mh.dps != null) dps_min_off = mh.dps.min;
            if (mh.attributesRaw.Damage_Weapon_Percent_Bonus_Physical != null)
            {
                phys_up_off = mh.attributesRaw.Damage_Weapon_Percent_Bonus_Physical.min;
                damage_min_off = Math.Round(damage_min * (1 + Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Percent_Bonus_Physical.min.Replace(".", sep)), 2)));
                damage_max_off = Math.Round(damage_max * (1 + Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Weapon_Percent_Bonus_Physical.min.Replace(".", sep)), 2)));
            }

            //----------Elemental damage up----------//

            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Physical != null) elem_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Physical.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Fire != null) elem_Fire = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Fire.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Cold != null) elem_Cold = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Cold.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Lightning != null) elem_Lightning = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Lightning.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Poison != null) elem_Poison = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Poison.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Arcane != null) elem_Arcane = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Arcane.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Damage_Dealt_Percent_Bonus_Holy != null) elem_Holy = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Dealt_Percent_Bonus_Holy.min.Replace(".", sep)), 2);

            if (Math.Round((elem_Physical + elem_Fire + elem_Cold + elem_Lightning + elem_Poison + elem_Arcane + elem_Holy) * 100) > 0 ) elem_all = elem_all + Math.Round((elem_Physical + elem_Fire + elem_Cold + elem_Lightning + elem_Poison + elem_Arcane + elem_Holy) * 100);

            //----------Attacks Per Second----------//

            if (mh.attacksPerSecond != null) aps_off = Math.Round(Convert.ToDouble(mh.attacksPerSecond.min.Replace(".", sep)), 2);
            if (mh.attributesRaw.Attacks_Per_Second_Item_Percent != null) aps_item_off = mh.attributesRaw.Attacks_Per_Second_Item_Percent.min;
            if (mh.attributesRaw.Attacks_Per_Second_Percent != null) aps_up = aps_up + Math.Round(Convert.ToDouble(mh.attributesRaw.Attacks_Per_Second_Percent.min.Replace(".", sep)), 2) * 100;

            //----------Critical damage----------//

            if (mh.attributesRaw.Crit_Damage_Percent != null) crit_damage = crit_damage + Math.Round(Convert.ToDouble(mh.attributesRaw.Crit_Damage_Percent.min.Replace(".", sep)), 2)*100;

            //----------Damage vs elites----------//

            if (mh.attributesRaw.Damage_Percent_Bonus_Vs_Elites != null) elite_damage = elite_damage + Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Percent_Bonus_Vs_Elites.min.Replace(".", sep)) * 100, 2);

            //----------OffHand Crit Chance----------//

            if (mh.attributesRaw.Crit_Percent_Bonus_Capped != null) off_crit_chance = off_crit_chance + Math.Round(Convert.ToDouble(mh.attributesRaw.Crit_Percent_Bonus_Capped.min.Replace(".", sep)) * 100, 2);

            //----------OffHand Damage----------//

            if (!(forparse.Contains("\"id\" : \"Ring\"") || forparse.Contains("\"id\" : \"Amulet\"")))
            {
                if (mh.attributesRaw.Damage_Min_Physical != null) off_min_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Min_Physical.min.Replace(".", sep)), 2);
                if (mh.attributesRaw.Damage_Delta_Physical != null) off_del_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Delta_Physical.min.Replace(".", sep)), 2);
            }
            if (forparse.Contains("\"id\" : \"Ring\"") && rings_count != 0)
            {
                if (mh.attributesRaw.Damage_Min_Physical != null) r2_min_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Min_Physical.min.Replace(".", sep)), 2);
                if (mh.attributesRaw.Damage_Delta_Physical != null) r2_del_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Delta_Physical.min.Replace(".", sep)), 2);
            }
            if (forparse.Contains("\"id\" : \"Ring\"") && rings_count == 0)
            {
                rings_count = rings_count + 1;
                if (mh.attributesRaw.Damage_Min_Physical != null) r1_min_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Min_Physical.min.Replace(".", sep)), 2);
                if (mh.attributesRaw.Damage_Delta_Physical != null) r1_del_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Delta_Physical.min.Replace(".", sep)), 2);
            }
            if (forparse.Contains("\"id\" : \"Amulet\""))
            {
                if (mh.attributesRaw.Damage_Min_Physical != null) am_min_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Min_Physical.min.Replace(".", sep)), 2);
                if (mh.attributesRaw.Damage_Delta_Physical != null) am_del_Physical = Math.Round(Convert.ToDouble(mh.attributesRaw.Damage_Delta_Physical.min.Replace(".", sep)), 2);
            }

        }

        //--------------------------------------------//

        public void import()
        {
            p_lvl = para_lvl;
            tsmi_clear_Click(null,null);
            //lb_para_lvl.Text = p_lvl;

            if (lb_adv.Text == "def") b_adv.PerformClick();
            
            if (skill_damage != "") tb_skill1.Text = skill_damage;
            if (skill_damage != "") tb_skill.Text = skill_damage;
            if (mainstat != 0) tb_main.Text = mainstat.ToString();
            if (phys_up != "") tb_dmg1_p.Text = (Math.Round(Convert.ToDouble(phys_up.Replace(".", sep)), 2) * 100).ToString().Replace(sep, ".");
            if (phys_up != "") Readonly_clear(tb_dmg1_p, null);//tb_dmg1_p_MouseClick_1(null, null);
            if (damage_min != 0) tb_dmg1_1_a.Text = damage_min.ToString().Replace(sep, ".");
            if (damage_max != 0) tb_dmg1_2_a.Text = damage_max.ToString().Replace(sep, ".");
            if (dps_min != "") tb_damage1.Text = Math.Round(Convert.ToDouble(dps_min.Replace(".", sep)), 2).ToString().Replace(sep, ".");
            if (elem_all != 0) tb_elem1.Text = elem_all.ToString().Replace(sep, ".");
            if (elem_all != 0) tb_elem.Text = elem_all.ToString().Replace(sep, ".");
            if (aps != 0) tb_ac1.Text = aps.ToString().Replace(sep, ".");
            if (aps_off != 0) tb_ac2.Text = aps_off.ToString().Replace(sep, ".");
            if (aps_off != 0) Readonly_clear(tb_ac2, null);//tb_ac2_MouseClick(null, null);
            if (aps_item != "") tb_ac1_p.Text = (Math.Round(Convert.ToDouble(aps_item.Replace(".", sep)), 2) * 100).ToString().Replace(sep, ".");
            if (aps_item != "") Readonly_clear(tb_ac1_p, null);//tb_ac1_p_MouseClick(null, null);
            if (elite_damage != 0) tb_elite.Text = elite_damage.ToString().Replace(sep, ".");
            if (phys_up_off != "") tb_dmg2_p.Text = (1 + Math.Round(Convert.ToDouble(phys_up_off.Replace(".", sep)), 2)).ToString().Replace(sep, ".");
            if (phys_up_off != "") Readonly_clear(tb_dmg2_p, null);//tb_dmg2_p_MouseClick(null, null);
            if (damage_min_off != 0) tb_dmg2_1_a.Text = damage_min_off.ToString().Replace(sep, ".");
            if (damage_max_off != 0) tb_dmg2_2_a.Text = damage_max_off.ToString().Replace(sep, ".");
            if (damage_min_off != 0) Readonly_clear(tb_dmg2_1_a, null);//tb_dmg2_1_a_MouseClick(null, null);
            if (damage_max_off != 0) Readonly_clear(tb_dmg2_2_a, null);//tb_dmg2_2_a_MouseClick(null, null);
            if (dps_min_off != "") tb_damage2.Text = Math.Round(Convert.ToDouble(dps_min_off.Replace(".", sep)), 2).ToString().Replace(sep, ".");
            if (dps_min_off != "") Readonly_clear(tb_damage2, null);//tb_damage2_MouseClick(null, null);
            if (aps_up != 0) tb_acincr.Text = aps_up.ToString().Replace(sep, ".");
            if (aps_up != 0 && damage_min_off != 0 && damage_max_off != 0 && aps_off != 0) tb_acincr.Text = (aps_up+15).ToString().Replace(sep, ".");
            if (aps_item_off != "") tb_ac2_p.Text = (Math.Round(Convert.ToDouble(aps_item_off.Replace(".", sep)), 2) * 100).ToString().Replace(sep, ".");
            if (aps_item_off != "") Readonly_clear(tb_ac2_p, null);//tb_ac2_p_MouseClick(null, null);
            tb_cc.Text = (off_crit_chance+5).ToString().Replace(sep, ".");
            tb_cd.Text = (crit_damage + 50).ToString().Replace(sep, ".");
            if (off_min_Physical != 0) tb_off_min.Text = off_min_Physical.ToString().Replace(sep, ".");
            if ((off_min_Physical + off_del_Physical) != 0) tb_off_max.Text = (off_min_Physical + off_del_Physical).ToString().Replace(sep, ".");
            if (r2_min_Physical != 0) tb_r2_min.Text = r2_min_Physical.ToString().Replace(sep, ".");
            if ((r2_min_Physical + r2_del_Physical) != 0) tb_r2_max.Text = (r2_min_Physical + r2_del_Physical).ToString().Replace(sep, ".");
            if (r1_min_Physical != 0) tb_r1_min.Text = r1_min_Physical.ToString().Replace(sep, ".");
            if ((r1_min_Physical + r1_del_Physical) != 0) tb_r1_max.Text = (r1_min_Physical + r1_del_Physical).ToString().Replace(sep, ".");
            if (am_min_Physical != 0) tb_am_min.Text = am_min_Physical.ToString().Replace(sep, ".");
            if ((am_min_Physical + am_del_Physical) != 0) tb_am_max.Text = (am_min_Physical + am_del_Physical).ToString().Replace(sep, ".");
            if (to_skl > 0)
            {
                tb_toskill1.Text = to_skl.ToString().Replace(sep, ".");
                tb_toskill.Text = to_skl.ToString().Replace(sep, ".");
            }
            if (from_skl > 0) tb_fromskills.Text = from_skl.ToString().Replace(sep, ".");

            this.Text = this.Text + "                                            " + pers_name;
            cooldown_import();
            b_start.PerformClick();
        }

        private void b_web_Click(object sender, EventArgs e)
        {
            b_web.Enabled = false;
            tb_pers.Enabled = false;
            bw.RunWorkerAsync();
            bw.WorkerReportsProgress = true;
            //pb_load.Visible = true;
            //web_req();
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // You are in the main thread
            // Update the UI here
            string data = (string)e.UserState;
            pb_load.Visible = true;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // You are in a worker thread
            (sender as BackgroundWorker).ReportProgress(0, "right");
            web_req();
            //pb_load.Visible = false;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pb_load.Visible = false;
            if (mainstat != 0)
            {
                import();
                paragon();
                if (frm_para.Visible != true) frm_para.Visible = true;
            }
            b_web.Enabled = true;
            tb_pers.Enabled = true;
        }

        private void b_Parse_Click(object sender, EventArgs e)
        {

            string host = "eu.battle.net";
            if (tb_pers.Text.Contains("us.")) host = "us.battle.net";
            //System.Net.WebRequest req_item = System.Net.WebRequest.Create(@"http://" + host + "/api/d3/data/skill/crusader/slash");
            System.Net.WebRequest req_item = System.Net.WebRequest.Create(@"http://" + host + "/d3/en/class/crusader/active/slash");

            if (b_parse.Visible)
            {
                WebProxy myProxy = new WebProxy("localhost", 4001);
                myProxy.BypassProxyOnLocal = true;
                req_item.Proxy = myProxy;
            }
            else
            {
                IWebProxy myProxy = WebRequest.DefaultWebProxy;
                myProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
                req_item.Proxy = myProxy;
            }

            System.Net.WebRequest req_ver = System.Net.WebRequest.Create(@"https://github.com/DmitryOlenin/DPS_Diablo3");
            System.Net.WebResponse resp = req_ver.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);

            string test = sr.ReadToEnd();


            string [] pars_ver = test.Split('\n');  //парсим строку и получаем стринговый массив

            for (int i = 0; i < pars_ver.Length; i++)
            {
                if (pars_ver[i].Contains("Version"))
                {
                    string vers = pars_ver[i].Substring(pars_ver[i].IndexOf("Version") + 8, 3).Trim();
                    //
                   // if (vers != ver.ToString()) pan_ver.Visible = true;
                        //MessageBox.Show(vers);
                    break;
                }
            }


            
            string path_test = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\test.txt";
            System.IO.File.WriteAllText(path_test, test);



            //MessageBox.Show(sr.ReadToEnd());
            
            //string mh_path = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\mainHand.txt";
            //string off_path = @"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\offHand.txt";
            ////hero_parse = System.IO.File.ReadAllText(@"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\WriteText1.txt");

            ////string json1 = "{\"p1\": [123, true], \"p2\":[456, false]}";

            //string json2 = System.IO.File.ReadAllText(mh_path);

            //string[] parsedArray = json2.Split('\n');  //парсим строку и получаем стринговый массив

            //for (int i = 0; i < parsedArray.Length; i++)
            //{
            //    //MessageBox.Show(parsedArray[i].Trim().ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);
            //}
            ////int fl=0;
            //Primary[] arr_primary = new Primary[3];

            //rings_count = 0; crit_damage = 0; off_crit_chance = 0; aps_up = 0;
            //mainHand_parse = System.IO.File.ReadAllText(mh_path);
            //offHand_parse = System.IO.File.ReadAllText(off_path);

            //if (mainHand_parse.Contains("Critical Hit Damage Increased by 130")) crit_damage = crit_damage + 130;
            //if (mainHand_parse.Contains("Critical Hit Damage Increased by 125")) crit_damage = crit_damage + 125;
            //if (mainHand_parse.Contains("Critical Hit Damage Increased by 120")) crit_damage = crit_damage + 120;
            //if (mainHand_parse.Contains("Critical Hit Damage Increased by 115")) crit_damage = crit_damage + 115;
            //if (mainHand_parse.Contains("Critical Hit Damage Increased by 110")) crit_damage = crit_damage + 110;

            //if (offHand_parse.Contains("Critical Hit Damage Increased by 130")) crit_damage = crit_damage + 130;
            //if (offHand_parse.Contains("Critical Hit Damage Increased by 125")) crit_damage = crit_damage + 125;
            //if (offHand_parse.Contains("Critical Hit Damage Increased by 120")) crit_damage = crit_damage + 120;
            //if (offHand_parse.Contains("Critical Hit Damage Increased by 115")) crit_damage = crit_damage + 115;
            //if (offHand_parse.Contains("Critical Hit Damage Increased by 110")) crit_damage = crit_damage + 110;

            //mh();
            //offh(offHand_parse);
            //offh(System.IO.File.ReadAllText(@"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\leftFinger.txt"));
            //offh(System.IO.File.ReadAllText(@"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\neck.txt"));
            //offh(System.IO.File.ReadAllText(@"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\rightFinger.txt"));
            //offh(System.IO.File.ReadAllText(@"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\waist.txt"));
            //offh(System.IO.File.ReadAllText(@"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\bracers.txt"));
            //offh(System.IO.File.ReadAllText(@"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\legs.txt"));
            //offh(System.IO.File.ReadAllText(@"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\shoulders.txt"));
            //offh(System.IO.File.ReadAllText(@"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\hands.txt"));
            //offh(System.IO.File.ReadAllText(@"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\feet.txt"));
            //offh(System.IO.File.ReadAllText(@"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\torso.txt"));
            //offh(System.IO.File.ReadAllText(@"c:\GitHub\DPS_Diablo3\DPS_Diablo3\bin\Debug\head.txt"));

        }

        private void Paste_Click(object sender, EventArgs e)
        {
            // Determine if there is any text in the Clipboard to paste into the text box. 
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true)
            {
                Readonly_clear(tb_pers, null);
                tb_pers.Paste();
                b_web_Click(null,null);
            }
        }

        private void tsm_faq_Click(object sender, EventArgs e)
        {
            if (faq_flag == 0)
            {
                //MessageBox.Show("123");
                Button b_faq1 = new Button();
                b_faq1.Location = new Point(605, 0);
                b_faq1.Name="Close";
                b_faq1.Size = new System.Drawing.Size (52, 23);
                b_faq1.FlatStyle = FlatStyle.Standard;
                b_faq1.BackColor = Color.Red;
                b_faq1.FlatAppearance.BorderColor = Color.Red;
                b_faq1.FlatAppearance.BorderSize = 1;
                b_faq1.Text = "Close";
                b_faq1.MouseClick += new MouseEventHandler(faq);
                faq(null,null);
                this.Controls.Add(b_faq1);
                b_faq1.BringToFront();
            }
            else faq(null, null); 
            //else
            //{
            //    b_faq.BackColor = SystemColors.Control;
            //    b_faq.FlatStyle = FlatStyle.System;
            //}

            //faq();
            //b_faq.BringToFront();
        }



        private void tsmi_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Dispose();
        }

        private void tsmi_qsave_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(lng.Save_dialog1t + "\n" + lng.Save_dialog3t, lng.Save_dialog1t,
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Save_settings();
        }

        private void tsmi_qload_Click(object sender, EventArgs e)
        {
            paragon();
            foreach (NumericUpDown nud in frm_para.Controls.OfType<NumericUpDown>()) nud.Value = 0;

            List<TextBox> form_box = new List<TextBox> { tb_damage1, tb_ac1, tb_main, tb_acincr, tb_cc, tb_cd, tb_off_min, tb_off_max, tb_am_min, tb_am_max, tb_r1_min, tb_r1_max, tb_r2_min, tb_r2_max, tb_fromskills, tb_elem, tb_toskill, tb_elite, tb_skill, tb_dmg1_1_a, tb_dmg1_2_a, tb_skill1, tb_elem1, tb_toskill1, tb_dmg1_w, tb_dmg2_w };
            List<string> set_box = new List<string> { Settings.Default.tb_damage1, Settings.Default.tb_ac1, Settings.Default.tb_main, Settings.Default.tb_acincr, Settings.Default.tb_cc, Settings.Default.tb_cd, Settings.Default.tb_off_min, Settings.Default.tb_off_max, Settings.Default.tb_am_min, Settings.Default.tb_am_max, Settings.Default.tb_r1_min, Settings.Default.tb_r1_max, Settings.Default.tb_r2_min, Settings.Default.tb_r2_max, Settings.Default.tb_fromskills, Settings.Default.tb_elem, Settings.Default.tb_toskill, Settings.Default.tb_elite, Settings.Default.tb_skill, Settings.Default.tb_dmg1_1_a, Settings.Default.tb_dmg1_2_a, Settings.Default.tb_skill1, Settings.Default.tb_elem1, Settings.Default.tb_toskill1, Settings.Default.tb_dmg1_w, Settings.Default.tb_dmg2_w };
            for (int i = 0; i < form_box.Count; i++)
            {
                Readonly_insert(form_box[i], set_box[i], 0, "Normal");
            }

            List<TextBox> form_box_ro = new List<TextBox> { tb_ac2_p, tb_ac1_p, tb_toskill2, tb_elem2, tb_skill2_usage, tb_skill1_usage, tb_dmg2_p, tb_dmg1_p, tb_dmg2_2_a, tb_dmg2_1_a, tb_skill2, tb_damage2, tb_ac2 };
            List<string> set_box_ro = new List<string> { Settings.Default.tb_ac2_p, Settings.Default.tb_ac1_p, Settings.Default.tb_toskill2, Settings.Default.tb_elem2, Settings.Default.tb_skill2_usage, Settings.Default.tb_skill1_usage, Settings.Default.tb_dmg2_p, Settings.Default.tb_dmg1_p, Settings.Default.tb_dmg2_2_a, Settings.Default.tb_dmg2_1_a, Settings.Default.tb_skill2, Settings.Default.tb_damage2, Settings.Default.tb_ac2 };
            for (int i = 0; i < form_box_ro.Count; i++)
            {
                Readonly_insert(form_box_ro[i], set_box_ro[i], 0, "ReadOnly");
            }

            List<NumericUpDown> form_nud = new List<NumericUpDown> { nud_garg, nud_dogs, nud_fet, nud_elemp, nud_damp, nud_speedp, nud_main_w, nud_damp_w, nud_acp_w, nud_cd_w, nud_elem_w, nud_cooldown, nud_cdr1, nud_cdr2, nud_cdr3, nud_cdr4, nud_cdr5, nud_cdr6, nud_cdr7, nud_cdr8, nud_cdr9, nud_cdr10, nud_para_main, nud_para_as, nud_para_cdr, nud_para_cc, nud_para_cd };
            List<decimal> set_nud = new List<decimal> { Settings.Default.nud_garg, Settings.Default.nud_dogs, Settings.Default.nud_fet, Settings.Default.nud_elemp, Settings.Default.nud_damp, Settings.Default.nud_speedp, Settings.Default.nud_main_w, Settings.Default.nud_damp_w, Settings.Default.nud_acp_w, Settings.Default.nud_cd_w, Settings.Default.nud_elem_w, Settings.Default.nud_cooldown, Settings.Default.nud_cdr1, Settings.Default.nud_cdr2, Settings.Default.nud_cdr3, Settings.Default.nud_cdr4, Settings.Default.nud_cdr5, Settings.Default.nud_cdr6, Settings.Default.nud_cdr7, Settings.Default.nud_cdr8, Settings.Default.nud_cdr9, Settings.Default.nud_cdr10, Settings.Default.nud_para_main, Settings.Default.nud_para_as, Settings.Default.nud_para_cdr, Settings.Default.nud_para_cc, Settings.Default.nud_para_cd };

            for (int i = 0; i < form_nud.Count; i++)
            {
                Readonly_insert(form_nud[i], null, set_nud[i], "NumericUpDown");
            }

            if (nud_garg.Value != 0 || nud_dogs.Value != 0 || nud_fet.Value != 0 || nud_elemp.Value != 0 || nud_damp.Value != 0 || nud_speedp.Value != 0)
            {
                //pan_wd.Visible = true;
                //pan_roll.Visible = false;
                //pan_wep.Visible = false;
                //pan_wddam.Visible = true;
                //b_wep.Text = lng.b_wept_skill;
                //cb_wd.Checked = true;
            }

            Readonly_insert(lb_para_lvl, Settings.Default.lb_para_lvl, 0, "Label");

            White();
            //if ((tb_dmg1_1_a.Text != "" || tb_dmg1_2_a.Text != "" || tb_skill1.Text != "") && (lb_adv.Text == "def")) b_adv.PerformClick();

            int leng = lb_para_lvl.Text.IndexOf("(");
            if (leng <= 0) leng = lb_para_lvl.Text.Length;
            p_lvl = lb_para_lvl.Text.Substring(0, leng).Trim();
            para_prep();

            nud_cdr11.Value = nud_para_cdr.Value;
           
            b_start.PerformClick();
        }

        private void tsmi_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Xml Files|*.xml";
            saveFileDialog1.Title = "Select a xml File to save";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //MessageBox.Show(openFileDialog1.FileName.ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);
                Save_settings();
                WriteXML(saveFileDialog1.FileName.ToString());
            }
        }

        private void tsmi_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Xml Files|*.xml";
            openFileDialog1.Title = "Select a xml File to load";
            //openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReadXML(openFileDialog1.FileName.ToString());
                tsmi_clear_Click(null,null);
                tsmi_qload_Click(null,null);
            }
        }

        private void tsmi_clear_Click(object sender, EventArgs e)
        {
            lb_changed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lb_result.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lb_result_profile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

            this.Text = version;
            White();
            foreach (Control control in this.Controls.OfType<TextBox>()) control.Text = "";
            foreach (NumericUpDown numud in this.Controls.OfType<NumericUpDown>()) numud.Value = 0;
            foreach (Control control in this.Controls.OfType<Panel>())
            {
                foreach (Control control1 in control.Controls.OfType<TextBox>()) control1.Text = "";
                foreach (NumericUpDown numud in control.Controls.OfType<NumericUpDown>())
                {
                    numud.Value = 1;
                    numud.Value = 0;
                }
            }
            tb_damage2.Text = "weapon2";
            tb_damage2.ReadOnly = true;
            tb_ac2.Text = "weapon2";
            tb_ac2.ReadOnly = true;
            //pan_stat.Visible = true;
            //pan_info.Visible = true;
            b_stat.Text = lng.b_statt_dps;
            lb_result.Text = lng.lb_resultt;
            lb_result_profile.Text = lng.lb_result_profilet;
            lb_changed.Text = lng.lb_changedt;
            //pan_wd.Visible = false;
            //pan_wddam.Visible = false;
            tb_skill1_usage.Text = "100%";
            tb_skill1_usage.ReadOnly = true;
            tb_skill2_usage.Text = "skill2";
            tb_skill2_usage.ReadOnly = true;
            tb_skill2.Text = "skill2";
            tb_skill2.ReadOnly = true;
            tb_toskill2.Text = "skill2";
            tb_toskill2.ReadOnly = true;
            tb_elem2.Text = "skill2";
            tb_elem2.ReadOnly = true;
            tb_dmg1_p.Text = "weapon1";
            tb_dmg1_p.ReadOnly = true;
            tb_dmg2_p.Text = "weapon2";
            tb_dmg2_p.ReadOnly = true;
            tb_dmg2_1_a.Text = "min2";
            tb_dmg2_1_a.ReadOnly = true;
            tb_dmg2_2_a.Text = "max2";
            tb_dmg2_2_a.ReadOnly = true;
            tb_ac1_p.Text = "weapon1";
            tb_ac1_p.ReadOnly = true;
            tb_ac2_p.Text = "weapon2";
            tb_ac2_p.ReadOnly = true;
            //tb_pers.Text = "Address \"http://\" from armory";
            //tb_pers.ReadOnly = true;
            //tb_pers.BackColor = System.Drawing.SystemColors.ControlLightLight;
            lb_state.Text = "roll";
            lb_stat.Text = "hlp";
            lb_start.Text = "calc";
            //pan_list.Visible = false;
            //pan_wep.Visible = false;
            //b_wep.Text = lng.b_wept;
            //pan_cdr.Visible = false;
            coold = 0;
            //if (lb_adv.Text == "adv") b_adv.PerformClick();

            lb_cdr.Visible = false;

            foreach (TextBox txt in this.Controls.OfType<TextBox>())
                if (txt.ReadOnly)
                {
                    txt.ReadOnly = false;
                    txt.BackColor = SystemColors.Control;
                    txt.ReadOnly = true;
                }
            //MessageBox.Show(txt.Name.ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);

            foreach (Control control in this.Controls.OfType<Panel>())
                foreach (TextBox txt in control.Controls.OfType<TextBox>())
                    if (txt.ReadOnly)
                    {
                        txt.ReadOnly = false;
                        txt.BackColor = SystemColors.Control;
                        txt.ReadOnly = true;
                    }

            cb_choice.SelectedIndex = 0;
            cb_choice_SelectionChangeCommitted(null, null);

            Lang();

            foreach (NumericUpDown numud in frm_para.Controls.OfType<NumericUpDown>()) numud.Value = 0;
            para_lvl = ""; lb_para_lvl.Text = "";
        }

        private void tsmi_adv_Click(object sender, EventArgs e)
        {
            if (pan_da.Visible == false)
            {
                pan_da.Visible = true;
                pan_wa.Visible = true;
                pan_skill.Visible = true;
                pan_skillusage.Visible = true;
                pan_skillup.Visible = true;
                //pan_list.Visible = true;
                //pan_wd.Visible = true;
                //b_adv.Text = lng.b_advt_def;
                lb_adv.Text = "adv";
            }
            else
            {
                pan_da.Visible = false;
                pan_wa.Visible = false;
                pan_skill.Visible = false;
                pan_skillusage.Visible = false;
                pan_skillup.Visible = false;
                //pan_list.Visible = false;
                //pan_wep.Visible = false;
                //pan_wd.Visible = false;
                //b_adv.Text = lng.b_advt;
                lb_adv.Text = "def";
            }
        }

        private void tsmi_about_Click(object sender, EventArgs e)
        {
            MessageBox.Show(lng.lb_help1t + "\n" + lng.lb_help2t + "\n" + lng.lb_help3t + "\n" + lng.lb_help4t + "\n" + lng.lb_help5t + "\n" + lng.lb_help6t + "\n" + lng.lb_help7t + "\n" + lng.lb_help8t);
            //Form f = new Form();
            //f.Location = new Point(0, 0);
            //f.Size = new Size(this.Size.Width - 5, this.Size.Height - 28);//new Size(675, 368);
            //f.Show();
        }

        private void tsmi_ver_Click(object sender, EventArgs e)
        {
            System.Net.WebRequest req_ver = System.Net.WebRequest.Create(@"https://github.com/DmitryOlenin/DPS_Diablo3");

            IWebProxy myProxy = WebRequest.DefaultWebProxy;
            myProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            req_ver.Proxy = myProxy;
            //req_ver.Proxy = null;

            System.Net.WebResponse resp1 = req_ver.GetResponse();
            System.IO.Stream stream1 = resp1.GetResponseStream();
            System.IO.StreamReader sr1 = new System.IO.StreamReader(stream1);

            string version = sr1.ReadToEnd();

            string[] pars_ver = version.Split('\n');  //парсим строку и получаем стринговый массив

            for (int i = 0; i < pars_ver.Length; i++)
            {
                if (pars_ver[i].Contains("Version"))
                {
                    string vers = pars_ver[i].Substring(pars_ver[i].IndexOf("title=\"Version") + 15, 3).Trim();
                    if (vers != string.Format("{0:F1}", ver).ToString().Trim().Replace(sep, "."))
                    {
                        if (MessageBox.Show(
                        "Download?", "New version: " + vers, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk
                        ) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start("https://dl.dropboxusercontent.com/u/14539335/DPS_Diablo3.zip");
                        }                        
                        
                        //pan_ver.Visible = true;
                        //b_ver.Visible = false;
                        //lb_version_check.Visible = false;
                    }
                    else MessageBox.Show("No new version!");
                    //MessageBox.Show(vers);
                    break;
                }
            }
        }

        private void tsmi_lang_rus_Click(object sender, EventArgs e)
        {
            bt_lang.Text = "ENG";
            lng.Lang_rus();
            Lang();
        }

        private void tsmi_lang_eng_Click(object sender, EventArgs e)
        {
            bt_lang.Text = "RUS";
            lng.Lang_eng();
            Lang();
        }

        public void action_choice ()
        {
            int i = cb_choice.SelectedIndex;
            cb_choice.Items.Clear();
            //cb_choice.Items.Add(lng.b_statt_dps);
            cb_choice.Items.Add(lng.b_wept_skill);
            cb_choice.Items.Add(lng.b_wept);
            cb_choice.Items.Add(lng.b_statt_cdr);
            cb_choice.Items.Add(lng.b_wept_wd);
            cb_choice.SelectedIndex = i;
        }

        private void cb_choice_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (cb_choice.SelectedIndex)
            {
                case 0:
                    lb_state.Text = "roll";
                    pan_roll.BringToFront();
                    break;
                case 1:
                    lb_state.Text = "wep";
                    pan_wep.BringToFront();
                    break;
                case 2:
                    lb_stat.Text = "cdr";
                    pan_cdr.BringToFront();
                    break;
                case 3:
                    lb_state.Text = "wd";
                    pan_wd.BringToFront();
                    break;
            }
        }

        private void pan_divider_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = pan_divider.CreateGraphics();
            Pen p = new Pen(System.Drawing.SystemColors.ControlDark, 1);// цвет линии и ширина
            Point p1 = new Point(0, 6);// первая точка
            Point p2 = new Point(this.Size.Width - 5, 6);// вторая точка
            gr.DrawLine(p, p1, p2);// рисуем линию
            gr.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
        }

        private void paragonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (Application.OpenForms["Paragon"] != null) frm_para.Visible = false;//frm_para.Dispose();
            if (frm_para.Visible == true) frm_para.Visible = false;//frm_para.Dispose();
            else
            {
                frm_para.Visible = true;
                paragon();
            }
        }

        public void para_form_create()
        {
            frm_para.StartPosition = FormStartPosition.Manual;
            frm_para.Owner = this;
            this.Move += Form_Move;
            frm_para.ControlBox = false;
            frm_para.Name = "Paragon";
            frm_para.Opacity = 1;
            frm_para.Size = new Size(200, 200);
            frm_para.MaximumSize = new Size(200, 200);
            frm_para.MinimumSize = new Size(200, 200);
            //frm_para.DesktopLocation = new Point(this.Location.X + this.Size.Width + 0, this.Location.Y - 0);
            //frm_para.Location = new Point(this.Location.X + this.Size.Width + 5, this.Location.Y - 5);
            frm_para.ShowInTaskbar = false;
            frm_para.Visible = false;
            frm_para.KeyPreview = true;
            frm_para.KeyDown +=new KeyEventHandler(frm_para_KeyDown);
        }

        private void frm_para_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7 && frm_para.Enabled)
            {
                foreach (NumericUpDown nud in frm_para.Controls.OfType<NumericUpDown>()) nud.Value = 0;
                lb_para_lvl.Text = "";
            }
        }

        public void paragon()
        {
            int ypos = 1;

            lb_para_main.Location = new Point(1, ypos + 2);
            lb_para_main.AutoSize = true;
            lb_para_main.Text = lng.lb_imaint + ":";
            frm_para.Controls.Add(lb_para_main);

            nud_para_main.Location = new Point(120, ypos);
            nud_para_main.Size = new Size(65, 20);
            nud_para_main.Maximum = 10000;
            nud_para_main.Increment = 5;
            nud_para_main.Name = "para_main";
            //ValueChanged += nud_para_ValueChanged
            frm_para.Controls.Add(nud_para_main);

            ypos += 25;
            lb_para_as.Location = new Point(1, ypos + 2);
            lb_para_as.AutoSize = true;
            lb_para_as.Text = lng.lb_as + ":";
            frm_para.Controls.Add(lb_para_as);

            nud_para_as.Location = new Point(120, ypos);
            nud_para_as.Size = new Size(65, 20);
            nud_para_as.Maximum = 10;
            nud_para_as.Increment = 0.2M;
            nud_para_as.DecimalPlaces = 1;
            nud_para_as.Name = "para_as";
            //ValueChanged += nud_para_ValueChanged
            frm_para.Controls.Add(nud_para_as);

            ypos += 25;
            lb_para_cdr.Location = new Point(1, ypos + 2);
            lb_para_cdr.AutoSize = true;
            lb_para_cdr.Text = lng.lb_cdr + ":";
            frm_para.Controls.Add(lb_para_cdr);

            nud_para_cdr.Location = new Point(120, ypos);
            nud_para_cdr.Size = new Size(65, 20);
            nud_para_cdr.Maximum = 10;
            nud_para_cdr.Increment = 0.2M;
            nud_para_cdr.DecimalPlaces = 1;
            nud_para_cdr.Name = "para_cdr";
            //ValueChanged += nud_para_ValueChanged
            frm_para.Controls.Add(nud_para_cdr);

            ypos += 25;
            lb_para_cc.Location = new Point(1, ypos + 2);
            lb_para_cc.AutoSize = true;
            lb_para_cc.Text = lng.lb_icct + ":";
            frm_para.Controls.Add(lb_para_cc);

            nud_para_cc.Location = new Point(120, ypos);
            nud_para_cc.Size = new Size(65, 20);
            nud_para_cc.Maximum = 5;
            nud_para_cc.Increment = 0.1M;
            nud_para_cc.DecimalPlaces = 1;
            nud_para_cc.Name = "para_cc";
            //ValueChanged += nud_para_ValueChanged
            frm_para.Controls.Add(nud_para_cc);

            ypos += 25;
            lb_para_cd.Location = new Point(1, ypos + 2);
            lb_para_cd.AutoSize = true;
            lb_para_cd.Text = lng.lb_icdt + ":";
            frm_para.Controls.Add(lb_para_cd);

            nud_para_cd.Location = new Point(120, ypos);
            nud_para_cd.Size = new Size(65, 20);
            nud_para_cd.Maximum = 50;
            nud_para_cd.Increment = 1;
            nud_para_cd.Name = "para_cd";
            //ValueChanged += nud_para_ValueChanged
            frm_para.Controls.Add(nud_para_cd);

            ypos += 30;
            lb_para.Location = new Point(1, ypos + 1);
            lb_para.AutoSize = true;
            lb_para.Text = lng.lb_paragon;
            frm_para.Controls.Add(lb_para);

            lb_para_lvl.Location = new Point(120, ypos);
            lb_para_lvl.AutoSize = true;
            lb_para_lvl.Text = p_lvl.Trim();
            //lb_para_lvl.Text = para_lvl;
            frm_para.Controls.Add(lb_para_lvl);

            ypos += 25;
            b_para_save.Location = new Point(10, ypos);
            b_para_save.Name = "para_save";
            b_para_save.Text = "Save";
            b_para_save.Click +=new EventHandler(b_para_save_Click);
            frm_para.Controls.Add(b_para_save);
            b_para_load.Location = new Point(100, ypos);
            b_para_load.Name = "para_load";
            b_para_load.Text = "Load";
            b_para_load.Click +=new EventHandler(b_para_load_Click);
            frm_para.Controls.Add(b_para_load);

            frm_para.Show();

            para_prep();

        }

        public void para_prep ()
        {
            para_level = 0; level1 = 0; level2 = 0;

            foreach (NumericUpDown nud in frm_para.Controls.OfType<NumericUpDown>())
                nud.TextChanged += new EventHandler(nud_TextChanged);

            foreach (NumericUpDown nud in frm_para.Controls.OfType<NumericUpDown>())
                if (nud.Name == "para_as" || nud.Name == "para_cdr" || nud.Name == "para_cc" || nud.Name == "para_cd") nud.ValueChanged += nud_para_ValueChanged;

            try { para_level = Convert.ToInt16(p_lvl); } catch { }
            //if (p_lvl.Length>0) para_level = Convert.ToInt16(p_lvl);
            if (para_level > 0)
            {
                level1 = para_level / 4;
                level2 = level1;
                if (para_level - (para_level / 4 * 4) > 0) level1 += 1;
                if (para_level - (para_level / 4 * 4) > 1) level2 += 1;
                //MessageBox.Show("Парагон1: " + level1.ToString() + " Парагон2: " + level2.ToString());

                nud_para_main.Maximum = level1 * 5;

                //MessageBox.Show(nud_para_main.Maximum.ToString());

                lev2 = Convert.ToDecimal(level2);
                curr_para = lev2; prev_para = lev2;

                if (level2 * nud_para_as.Increment < nud_para_as.Maximum) nud_para_as.Maximum = level2 * nud_para_as.Increment;
                if (level2 * nud_para_cdr.Increment < nud_para_cdr.Maximum) nud_para_cdr.Maximum = level2 * nud_para_cdr.Increment;
                if (level2 * nud_para_cc.Increment < nud_para_cc.Maximum) nud_para_cc.Maximum = level2 * nud_para_cc.Increment;
                if (level2 * nud_para_cd.Increment < nud_para_cd.Maximum) nud_para_cd.Maximum = level2 * nud_para_cd.Increment;
            }
        }

        private void b_para_save_Click(object sender, EventArgs e)
        {
            Paragon.Default.nud_para_main = nud_para_main.Value;
            Paragon.Default.nud_para_as = nud_para_as.Value;
            Paragon.Default.nud_para_cdr = nud_para_cdr.Value;
            Paragon.Default.nud_para_cc = nud_para_cc.Value;
            Paragon.Default.nud_para_cd = nud_para_cd.Value;
            Paragon.Default.lb_para_lvl = lb_para_lvl.Text;
            Paragon.Default.Save();

            //MessageBox.Show(Paragon.Default.lb_para_lvl.ToString());
        }

        private void b_para_load_Click(object sender, EventArgs e)
        {

            foreach (NumericUpDown nud in frm_para.Controls.OfType<NumericUpDown>()) nud.Value = 0;

            //MessageBox.Show(Paragon.Default.lb_para_lvl);

            Readonly_insert(lb_para_lvl, Paragon.Default.lb_para_lvl, 0, "Label");

            int leng = lb_para_lvl.Text.IndexOf("(");
            //int leng = Paragon.Default.lb_para_lvl.IndexOf("(");
            if (leng <= 0) leng = lb_para_lvl.Text.Length;
            p_lvl = lb_para_lvl.Text.Substring(0, leng).Trim();
            //p_lvl = Paragon.Default.lb_para_lvl.Substring(0, leng).Trim();

            //MessageBox.Show(Paragon.Default.lb_para_lvl);

            para_prep();
            
            List<NumericUpDown> para_nud = new List<NumericUpDown> { nud_para_main, nud_para_as, nud_para_cdr, nud_para_cc, nud_para_cd };
            List<decimal> set_para_nud = new List<decimal> { Paragon.Default.nud_para_main, Paragon.Default.nud_para_as, Paragon.Default.nud_para_cdr, Paragon.Default.nud_para_cc, Paragon.Default.nud_para_cd };

            for (int i = 0; i < para_nud.Count; i++)
            {
                Readonly_insert(para_nud[i], null, set_para_nud[i], "NumericUpDown");
            }

            nud_cdr11.Value = nud_para_cdr.Value;
        }

        private void nud_TextChanged(object sender, EventArgs e)
        {
            var nud_sender = (NumericUpDown)sender;
            if (nud_sender.Text == "") nud_sender.Value = 0;
        }

        private void nud_para_ValueChanged(object sender, EventArgs e)
        {
            var nud_sender = (NumericUpDown)sender;
            decimal val = nud_sender.Value;

            if (nud_sender.Value <= nud_sender.Maximum && level2 > 0)
            {
                curr_para = lev2;
                prev_para = lev2;
                foreach (NumericUpDown nud in frm_para.Controls.OfType<NumericUpDown>())
                    if (nud.Name == "para_as" || nud.Name == "para_cdr" || nud.Name == "para_cc" || nud.Name == "para_cd")
                    {
                        curr_para -= nud.Value / nud.Increment;
                    }

                foreach (NumericUpDown nud in frm_para.Controls.OfType<NumericUpDown>())
                    if (nud.Name != nud_sender.Name && nud.Name != "para_main") prev_para -= nud.Value / nud.Increment;


                if (curr_para < 0)
                {
                    nud_sender.Value = nud_sender.Increment * prev_para;
                    curr_para = 0;
                }

                lb_para_lvl.Text = p_lvl.Trim() + " (" + curr_para.ToString() + ")";

                //MessageBox.Show(nud_sender.Name);
                if (nud_sender.Name == "para_cdr") this.nud_cdr11.Value = nud_sender.Value;
            }
        }
        
        private void Form_Move(object sender, EventArgs e)
        {
            if (Environment.OSVersion.Version.Major >= 6 && DwmIsCompositionEnabled()) 
                frm_para.DesktopLocation = new Point(this.Location.X + this.Size.Width + 5, this.Location.Y - 5);
            else frm_para.DesktopLocation = new Point(this.Location.X + this.Size.Width + 0, this.Location.Y - 0);
        }

        }


    }
    

