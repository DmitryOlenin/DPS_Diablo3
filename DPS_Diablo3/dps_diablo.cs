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

namespace Dps_Diablo3
{

    //public delegate void ChangedEventHandler(object sender, EventArgs e);

    public partial class dps_diablo : Form
    {

        public double wepdamage, otherdamage, cleardamage, critdamage, overdamage, speedtotal, result, result_profile,
            wepdamage_wd, otherdamage_wd, cleardamage_wd, critdamage_wd, overdamage_wd, speedtotal_wd, result_wd, petdamage_wd, skill_wd,
              damage1, damage2, ac1, ac2, main, acincr, cc, cd, off_min, off_max, am_min, am_max,
              r1_min, r1_max, r2_min, r2_max, other_min, other_max, fromskills, elem, toskill, elite, skill,
              result_old, result_old_wd,
              dmg1_1, dmg1_2, dmg2_1, dmg2_2, ac1_a, ac2_a, dmg1_p, dmg2_p, ac1_p, ac2_p, dmg1_1_a, dmg1_2_a, dmg2_1_a, dmg2_2_a,
              skill1_usage,skill2_usage, skill1, skill2, elem1, elem2, toskill1, toskill2,
              coold, cdr_all, skill_base, skill_other
              ;

        public dps_diablo()
        {
            InitializeComponent();

            foreach (Control control in this.Controls.OfType<TextBox>()) control.KeyDown += Key_Test;
            
            foreach (Control control in this.Controls.OfType<Panel>())
                foreach (Control control1 in control.Controls.OfType<TextBox>()) control1.KeyDown += Key_Test;
            
            foreach (Control control in this.Controls.OfType<TextBox>()) control.ContextMenu = new ContextMenu();

            foreach (Control control in this.Controls.OfType<Panel>())
                foreach (Control control1 in control.Controls.OfType<TextBox>()) control1.ContextMenu = new ContextMenu();
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
            //Settings.Default.tb_other_min = tb_other_min.Text;
            //Settings.Default.tb_other_max = tb_other_max.Text;
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
            Settings.Default.Save();
        }

        

        private void b_save_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Хотите сохранить?\nПредыдущие данные будут перезаписаны!", "Внимание",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Save_settings();
        }

        private void b_load_Click(object sender, EventArgs e)
        {
            if (tb_damage1.Text == "") tb_damage1.Text = Settings.Default.tb_damage1;
            if ((tb_damage2.Text == "" || tb_damage2.Text == "Оружие 2") && (Settings.Default.tb_damage2 != "" && Settings.Default.tb_damage2 != "Оружие 2")) tb_damage2.Text = Settings.Default.tb_damage2;
            if (tb_ac1.Text == "") tb_ac1.Text = Settings.Default.tb_ac1;
            if ((tb_ac2.Text == "" || tb_ac2.Text == "Оружие 2") && (Settings.Default.tb_ac2 != "" && Settings.Default.tb_ac2 != "Оружие 2")) tb_ac2.Text = Settings.Default.tb_ac2;
            if (tb_main.Text == "") tb_main.Text = Settings.Default.tb_main;
            if (tb_acincr.Text == "") tb_acincr.Text = Settings.Default.tb_acincr;
            if (tb_cc.Text == "") tb_cc.Text = Settings.Default.tb_cc;
            if (tb_cd.Text == "") tb_cd.Text = Settings.Default.tb_cd;
            if (tb_off_min.Text == "") tb_off_min.Text = Settings.Default.tb_off_min;
            if (tb_off_max.Text == "") tb_off_max.Text = Settings.Default.tb_off_max;
            if (tb_am_min.Text == "") tb_am_min.Text = Settings.Default.tb_am_min;
            if (tb_am_max.Text == "") tb_am_max.Text = Settings.Default.tb_am_max;
            if (tb_r1_min.Text == "") tb_r1_min.Text = Settings.Default.tb_r1_min;
            if (tb_r1_max.Text == "") tb_r1_max.Text = Settings.Default.tb_r1_max;
            if (tb_r2_min.Text == "") tb_r2_min.Text = Settings.Default.tb_r2_min;
            if (tb_r2_max.Text == "") tb_r2_max.Text = Settings.Default.tb_r2_max;
            //if (tb_other_min.Text == "") tb_other_min.Text = Settings.Default.tb_other_min;
            //if (tb_other_max.Text == "") tb_other_max.Text = Settings.Default.tb_other_max;
            if (tb_fromskills.Text == "") tb_fromskills.Text = Settings.Default.tb_fromskills;
            if (tb_elem.Text == "") tb_elem.Text = Settings.Default.tb_elem;
            if (tb_toskill.Text == "") tb_toskill.Text = Settings.Default.tb_toskill;
            if (tb_elite.Text == "") tb_elite.Text = Settings.Default.tb_elite;
            if (tb_skill.Text == "") tb_skill.Text = Settings.Default.tb_skill;
            if (nud_garg.Value == 0) nud_garg.Value = Settings.Default.nud_garg;
            if (nud_dogs.Value == 0) nud_dogs.Value = Settings.Default.nud_dogs;
            if (nud_fet.Value == 0) nud_fet.Value = Settings.Default.nud_fet;
            if (nud_elemp.Value == 0) nud_elemp.Value = Settings.Default.nud_elemp;
            if (nud_damp.Value == 0) nud_damp.Value = Settings.Default.nud_damp;
            if (nud_speedp.Value == 0) nud_speedp.Value = Settings.Default.nud_speedp;
            if (nud_garg.Value != 0 || nud_dogs.Value != 0 || nud_fet.Value != 0 || nud_elemp.Value != 0 || nud_damp.Value != 0 || nud_speedp.Value != 0)
            {
                //b_wd.Enabled = true;
                pan_wd.Visible = true;
                pan_roll.Visible = false;
                pan_wep.Visible = false;
                pan_wddam.Visible = true;
                b_wep.Text = "Расчёт изменения параметров";
                cb_wd.Checked = true;
                //b_wd.Text = "Изменение статов и DPS";
            }
            if (tb_dmg1_1_a.Text == "") tb_dmg1_1_a.Text = Settings.Default.tb_dmg1_1_a;
            if (tb_dmg1_2_a.Text == "") tb_dmg1_2_a.Text = Settings.Default.tb_dmg1_2_a;
            if (tb_skill1.Text == "") tb_skill1.Text = Settings.Default.tb_skill1;
            if ((tb_dmg2_1_a.Text == "" || tb_dmg2_1_a.Text == "min2") && (Settings.Default.tb_dmg2_1_a != "" && Settings.Default.tb_dmg2_1_a != "min2"))
            {
                tb_dmg2_1_a.Text = Settings.Default.tb_dmg2_1_a;
                tb_dmg2_1_a.ReadOnly = false;
            }
            if ((tb_dmg2_2_a.Text == "" || tb_dmg2_2_a.Text == "max2") && (Settings.Default.tb_dmg2_2_a != "" && Settings.Default.tb_dmg2_2_a != "max2")) 
            {
                tb_dmg2_2_a.Text = Settings.Default.tb_dmg2_2_a;
                tb_dmg2_2_a.ReadOnly = false;
            }
            if ((tb_dmg1_p.Text == "" || tb_dmg1_p.Text == "Оружие 1") && (Settings.Default.tb_dmg1_p != "" && Settings.Default.tb_dmg1_p != "Оружие 1")) 
            {
                tb_dmg1_p.Text = Settings.Default.tb_dmg1_p;
                tb_dmg1_p.ReadOnly = false;
            }
            if ((tb_dmg2_p.Text == "" || tb_dmg2_p.Text == "Оружие 2") && (Settings.Default.tb_dmg2_p != "" && Settings.Default.tb_dmg2_p != "Оружие 2")) 
            {
                tb_dmg2_p.Text = Settings.Default.tb_dmg2_p;
                tb_dmg2_p.ReadOnly = false;
            }
            if ((tb_skill2.Text == "" || tb_skill2.Text == "skill2") && (Settings.Default.tb_skill2 != "" && Settings.Default.tb_skill2 != "skill2")) 
            {
                tb_skill2.Text = Settings.Default.tb_skill2;
                tb_skill2.ReadOnly = false;
            }
            if ((tb_skill1_usage.Text == "" || tb_skill1_usage.Text == "100%") && (Settings.Default.tb_skill1_usage != "" && Settings.Default.tb_skill1_usage != "100%")) 
            {
                tb_skill1_usage.Text = Settings.Default.tb_skill1_usage;
                tb_skill1_usage.ReadOnly = false;
            }
            if ((tb_skill2_usage.Text == "" || tb_skill2_usage.Text == "skill2") && (Settings.Default.tb_skill2_usage != "" && Settings.Default.tb_skill2_usage != "skill2")) 
            {
                tb_skill2_usage.Text = Settings.Default.tb_skill2_usage;
                tb_skill2_usage.ReadOnly = false;
            }
            if (tb_elem1.Text == "") tb_elem1.Text = Settings.Default.tb_elem1;
            if ((tb_elem2.Text == "" || tb_elem2.Text == "skill2") && (Settings.Default.tb_elem2 != "" && Settings.Default.tb_elem2 != "skill2"))
            {
                tb_elem2.Text = Settings.Default.tb_elem2;
                tb_elem2.ReadOnly = false;
            }
            if (tb_toskill1.Text == "") tb_toskill1.Text = Settings.Default.tb_toskill1;
            if ((tb_toskill2.Text == "" || tb_toskill2.Text == "skill2") && (Settings.Default.tb_toskill2 != "" && Settings.Default.tb_toskill2 != "skill2"))
            {
                tb_toskill2.Text = Settings.Default.tb_toskill2;
                tb_toskill2.ReadOnly = false;
            }
            if ((tb_ac1_p.Text == "" || tb_ac1_p.Text == "Оружие 1") && (Settings.Default.tb_ac1_p != "" && Settings.Default.tb_ac1_p != "Оружие 1"))
            {
                tb_ac1_p.Text = Settings.Default.tb_ac1_p;
                tb_ac1_p.ReadOnly = false;
            }
            if ((tb_ac2_p.Text == "" || tb_ac2_p.Text == "Оружие 2") && (Settings.Default.tb_ac2_p != "" && Settings.Default.tb_ac2_p != "Оружие 2"))
            {
                tb_ac2_p.Text = Settings.Default.tb_ac2_p;
                tb_ac2_p.ReadOnly = false;
            }
            if (tb_dmg1_w.Text == "") tb_dmg1_w.Text = Settings.Default.tb_dmg1_w;
            if (tb_dmg2_w.Text == "") tb_dmg2_w.Text = Settings.Default.tb_dmg2_w;
            if (nud_main_w.Value == 0) nud_main_w.Value = Settings.Default.nud_main_w;
            if (nud_damp_w.Value == 0) nud_damp_w.Value = Settings.Default.nud_damp_w;
            if (nud_acp_w.Value == 0) nud_acp_w.Value = Settings.Default.nud_acp_w;
            if (nud_cd_w.Value == 0) nud_cd_w.Value = Settings.Default.nud_cd_w;
            if (nud_elem_w.Value == 0) nud_elem_w.Value = Settings.Default.nud_elem_w;
            if (nud_cooldown .Value == 0) nud_cooldown.Value = Settings.Default.nud_cooldown;
            if (nud_cdr1 .Value == 0) nud_cdr1.Value = Settings.Default.nud_cdr1;
            if (nud_cdr2 .Value == 0) nud_cdr2.Value = Settings.Default.nud_cdr2;
            if (nud_cdr3 .Value == 0) nud_cdr3.Value = Settings.Default.nud_cdr3;
            if (nud_cdr4 .Value == 0) nud_cdr4.Value = Settings.Default.nud_cdr4;
            if (nud_cdr5 .Value == 0) nud_cdr5.Value = Settings.Default.nud_cdr5;
            if (nud_cdr6 .Value == 0) nud_cdr6.Value = Settings.Default.nud_cdr6;
            if (nud_cdr7 .Value == 0) nud_cdr7.Value = Settings.Default.nud_cdr7;
            if (nud_cdr8 .Value == 0) nud_cdr8.Value = Settings.Default.nud_cdr8;
            if (nud_cdr9 .Value == 0) nud_cdr9.Value = Settings.Default.nud_cdr9;
            if (nud_cdr10 .Value == 0) nud_cdr10.Value = Settings.Default.nud_cdr10;
            White();
            if ((tb_dmg1_1_a.Text != "" || tb_dmg1_2_a.Text != "" || tb_skill1.Text != "") && (b_adv.Text == "Высокая\r\nточность")) b_adv.PerformClick();
            b_start.PerformClick();
        }

        private void b_clear_Click(object sender, EventArgs e)
        {
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
            tb_damage2.Text = "Оружие 2";
            tb_damage2.ReadOnly = true;
            tb_ac2.Text = "Оружие 2";
            tb_ac2.ReadOnly = true;
            pan_stat.Visible = false;
            pan_info.Visible = true;
            b_stat.Text = "Прирост DPS / Skill cooldown";
            //b_stat.Enabled = false;
            lb_result.Text = "Результаты";
            lb_result_profile.Text = "(приблизительно)";
            lb_changed.Text = "Урон после изменений";
            //b_wd.Enabled = false;
            pan_wd.Visible = false;
            pan_wddam.Visible = false;
            //cb_wd.CheckState = 0;
            //b_wd.Text = "Расчёт урона от питомцев (WD)";
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
            tb_dmg1_p.Text = "Оружие 1";
            tb_dmg1_p.ReadOnly = true;
            tb_dmg2_p.Text = "Оружие 2";
            tb_dmg2_p.ReadOnly = true;
            tb_dmg2_1_a.Text = "min2";
            tb_dmg2_1_a.ReadOnly = true;
            tb_dmg2_2_a.Text = "max2";
            tb_dmg2_2_a.ReadOnly = true;
            tb_ac1_p.Text = "Оружие 1";
            tb_ac1_p.ReadOnly = true;
            tb_ac2_p.Text = "Оружие 2";
            tb_ac2_p.ReadOnly = true;
            //b_wep.Enabled = false;
            lb_state.Text = "roll";
            pan_list.Visible = false;
            pan_wep.Visible = false;
            b_wep.Text = "Расчёт изменения оружия";
            pan_cdr.Visible = false;
            coold = 0;
            if (b_adv.Text != "Высокая\r\nточность") b_adv.PerformClick();
        }

        private void dps_diablo_Load(object sender, EventArgs e)
        {
            this.Icon = DPS_Diablo3.Properties.Resources.icon;
            tt_save.SetToolTip(b_save, "Сохраняет данные, прошлые будут перезаписаны.");
            tt_load.SetToolTip(b_load, "Загружает сохранённые данные.");
            tt_clear.SetToolTip(b_clear, "Очищает поля ввода.");
            //tt_start.SetToolTip(b_start, "Запуск.");
            toolTip1.SetToolTip(lb_idmg1, "DPS, написанный на оружии в профиле.");
            toolTip2.SetToolTip(lb_iac1, "Скорость оружия, на оружии в профиле.");
            toolTip3.SetToolTip(lb_imain, "Основной параметр персонажа.");
            toolTip4.SetToolTip(lb_iasi, "Сумма увеличения скорости атаки с вещей (кроме оружия).\n+15% скорости, если два оружия, не забываем.");
            toolTip5.SetToolTip(lb_icc, "Критшанс, в профиле персонажа.");
            toolTip6.SetToolTip(lb_icd, "Критурон, в профиле персонажа.");
            toolTip7.SetToolTip(lb_ioff, "Средний урон на сфере/обереге/колчане.");
            toolTip8.SetToolTip(lb_iamu, "Средний урон на амулете.");
            toolTip9.SetToolTip(lb_ir1, "Средний урон на кольце слева.");
            toolTip10.SetToolTip(lb_ir2, "Средний урон на кольце справа.");
            toolTip12.SetToolTip(lb_ifromskills, "Общее увеличение урона от пассивок, активных скиллов.");
            toolTip13.SetToolTip(lb_ielem, "Увеличение элементального урона стихии скилла, который считаем.");
            toolTip14.SetToolTip(lb_itoskill, "Увеличение урона скилла (+% урона скилла такого-то).");
            toolTip15.SetToolTip(lb_ielite, "Увеличение урона по элите.");
            toolTip16.SetToolTip(lb_iskill, "Процент урона скилла, написан на самом скилле.");
            toolTip17.SetToolTip(lb_dmg_a, "Минимум и максимум урона оружия (второе опционально).");
            toolTip18.SetToolTip(lb_perc_a, "+% урона на оружии, если есть (второе опционально).");
            toolTip19.SetToolTip(lb_skill_a, "Процент урона скилла, написан на самом скилле (второе опционально).");
            toolTip20.SetToolTip(lb_skill_usage_a, "Процент времени пользования 1 и 2 скиллом (примерно).");
            toolTip11.SetToolTip(lb_speed_a, "Увеличение скорости на оружии, если есть (второе опционально).");
            toolTip21.SetToolTip(lb_toskill_a, "Увеличение урона скиллов (+% урона скилла такого-то).");
            toolTip22.SetToolTip(lb_elem_a, "Увеличение элементального урона стихии скиллов.");
            toolTip23.SetToolTip(lb_dam_w, "Увеличение урона оружия (разница между новыми и старыми показателями).");
            toolTip24.SetToolTip(lb_main_w, "Новые показатели основного параметра на оружии.");
            toolTip25.SetToolTip(lb_damp_w, "Изменение % урона на оружии.");
            toolTip26.SetToolTip(lb_acp_w, "Изменение % скорости на оружии.");
            toolTip27.SetToolTip(lb_cd_w, "Изменение критического урона на оружии.");
            toolTip28.SetToolTip(lb_elem_w, "Изменение элементального урона на оружии.");
            toolTip28.SetToolTip(lb_auth, "Ссылка на форум с описанием");
            toolTip29.SetToolTip(lb_cooldown, "Время отката скилла в секундах");
            toolTip30.SetToolTip(lb_cdr12, "Скоращение времени отката с источников 1/2");
            toolTip31.SetToolTip(lb_cdr34, "Скоращение времени отката с источников 3/4");
            toolTip32.SetToolTip(lb_cdr56, "Скоращение времени отката с источников 5/6");
            toolTip33.SetToolTip(lb_cdr78, "Скоращение времени отката с источников 7/8");
            toolTip34.SetToolTip(lb_cdr910, "Скоращение времени отката с источников 9/10");
        }

        public void Check_lines()
        {
            string s = ".";
            int flag = 0;
            int count = 0;

            foreach (Control control in this.Controls.OfType<TextBox>())
            {
                count = control.Text.ToCharArray().Where(c => c == s[0]).Count();
                if (count > 1) flag++;
                if (control.Text == ".") flag++;
            }

            
            if (flag > 0) lb_result.Text = "Проблемы с точками";
            else
            {
                if (b_adv.Text == "Высокая\r\nточность")
                {
                    if (tb_damage1.Text == "" || tb_ac1.Text == "" || tb_main.Text == "" || tb_cc.Text == "" || tb_cd.Text == "" || tb_skill.Text == "")
                    {
                        lb_result.Text = "Не введены значения";
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
                        lb_result.Text = "Результаты";
                        White();
                    }
                }
                else
                {
                    if (tb_dmg1_1_a.Text == "" || tb_dmg1_2_a.Text == "" || tb_ac1.Text == "" || tb_main.Text == "" || tb_cc.Text == "" || tb_cd.Text == "" || tb_skill1.Text == "")
                    {
                        lb_result.Text = "Не введены значения";
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
                        lb_result.Text = "Результаты";
                        White();
                    }
                }
            }
        }

        private void b_start_Click(object sender, EventArgs e)
        {
            char a = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            string b = a.ToString();
            pan_roll.Visible = true;

            Check_lines();
            if (lb_result.Text == "Результаты")
            {
                foreach (NumericUpDown numud in this.Controls.OfType<NumericUpDown>()) if (numud.Text == "") numud.Value = 0;
                foreach (Control control in this.Controls.OfType<Panel>()) 
                    foreach (NumericUpDown numud in control.Controls.OfType<NumericUpDown>()) if (numud.Text == "") numud.Value = 0;

                cdr_all=100;
                coold = 0;
                if (nud_cooldown.Value != 0)
                {
                    foreach (NumericUpDown numud in pan_cdr.Controls.OfType<NumericUpDown>())
                        if (numud.Value != 0 && numud.Name!="nud_cooldown") cdr_all = cdr_all * (100 - Convert.ToSingle(numud.Value)) / 100;
                    coold = Convert.ToSingle(nud_cooldown.Value) * cdr_all / 100;
                }

                //MessageBox.Show(coold.ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);

                //Считаем DPS при продвинутом расчёте
                
                if (b_adv.Text != "Высокая\r\nточность")
                {
                    //b_wep.Enabled = true;
                    pan_list.Visible = true;
                    ac1 = Convert.ToSingle(tb_ac1.Text.Replace(".", b));
                    if (tb_dmg1_p.Text != "" && tb_dmg1_p.Text != "Оружие 1") { dmg1_p = Convert.ToSingle(tb_dmg1_p.Text.Replace(".", b)); } else { dmg1_p = 0; }
                    if (tb_dmg2_p.Text != "" && tb_dmg2_p.Text != "Оружие 2") { dmg2_p = Convert.ToSingle(tb_dmg2_p.Text.Replace(".", b)); } else { dmg2_p = 0; }
                    if (tb_ac1_p.Text != "" && tb_ac1_p.Text != "Оружие 1") { 
                        ac1_p = Convert.ToSingle(tb_ac1_p.Text.Replace(".", b));
                        ac1 = Math.Round((ac1 * 100 / (100 + ac1_p)), 1) * ((100 + ac1_p) / 100);
                    }
                    dmg1_1 = Math.Round(Convert.ToSingle(tb_dmg1_1_a.Text.Replace(".", b)) / (dmg1_p / 100 + 1));
                    dmg1_2 = Math.Round(Convert.ToSingle(tb_dmg1_2_a.Text.Replace(".", b)) / (dmg1_p / 100 + 1));
                    damage1 = (dmg1_1 + dmg1_2) / 2 * ac1 * (dmg1_p / 100 + 1);
                    damage2 = damage1;
                    //MessageBox.Show(damage1.ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    ac2 = ac1;
                    if (tb_dmg2_1_a.Text != "" && tb_dmg2_1_a.Text != "min2" && tb_dmg2_2_a.Text != "" && tb_dmg2_2_a.Text != "max2" && tb_ac2.Text != "" && tb_ac2.Text != "Оружие 2" && tb_ac2.Text != "0" && tb_dmg2_1_a.Text != "0" && tb_dmg2_2_a.Text != "0")
                    {
                        dmg2_1 = Math.Round(Convert.ToSingle(tb_dmg2_1_a.Text.Replace(".", b)) / (dmg2_p / 100 + 1));
                        dmg2_2 = Math.Round(Convert.ToSingle(tb_dmg2_2_a.Text.Replace(".", b)) / (dmg2_p / 100 + 1));
                        ac2 = Convert.ToSingle(tb_ac2.Text.Replace(".", b));
                        if (tb_ac2_p.Text != "" && tb_ac2_p.Text != "Оружие 2")
                        {
                            ac2_p = Convert.ToSingle(tb_ac2_p.Text.Replace(".", b));
                            ac2 = Math.Round((ac2 * 100 / (100 + ac2_p)), 1) * ((100 + ac2_p) / 100);
                        } 
                        damage2 = (dmg2_1 + dmg2_2) / 2 * ac2 * (dmg2_p / 100 + 1);

                    }
                    if (tb_skill1_usage.Text == "100%") skill1_usage = 100;
                    if (tb_skill1_usage.Text != "100%" && tb_skill1_usage.Text != "") skill1_usage = Convert.ToSingle(tb_skill1_usage.Text.Replace(".", b));
                    skill1 = Convert.ToSingle(tb_skill1.Text.Replace(".", b));
                    skill2=0; skill2_usage=0;
                    if (tb_skill2_usage.Text != "" && tb_skill2.Text != "" && tb_skill2_usage.Text != "skill2" && tb_skill2.Text != "skill2")
                    {
                        skill2=Convert.ToSingle(tb_skill2.Text.Replace(".", b));
                        skill2_usage=Convert.ToSingle(tb_skill2_usage.Text.Replace(".", b));
                    }
                    //(elem / 100 + 1) * (toskill / 100 + 1)
                    if (tb_elem1.Text != "") elem1 = Convert.ToSingle(tb_elem1.Text.Replace(".", b)); else elem1 = 0;
                    if (tb_elem2.Text != "" && tb_elem2.Text !="skill2") elem2 = Convert.ToSingle(tb_elem2.Text.Replace(".", b)); else elem2 = 0;
                    if (tb_toskill1.Text != "") toskill1 = Convert.ToSingle(tb_toskill1.Text.Replace(".", b)); else toskill1 = 0;
                    if (tb_toskill2.Text != "" && tb_toskill2.Text != "skill2") toskill2 = Convert.ToSingle(tb_toskill2.Text.Replace(".", b)); else toskill2 = 0;

                    skill_base = (skill1 * skill1_usage / 100 * (elem1 / 100 + 1) * (toskill1 / 100 + 1));
                    skill_other = (skill2 * skill2_usage / 100 * (elem2 / 100 + 1) * (toskill2 / 100 + 1));
                    skill = skill_base + skill_other;
                }
                //Считаем DPS при обычном расчёте
                else
                {
                    //lb_Debug.Items.Add("Тест норма");
                    damage1 = Convert.ToSingle(tb_damage1.Text.Replace(".", b));
                    ac1 = Convert.ToSingle(tb_ac1.Text.Replace(".", b));
                    damage2 = damage1;
                    ac2 = ac1;
                    if (tb_damage2.Text != "" && tb_damage2.Text != "Оружие 2" && tb_ac2.Text != "" && tb_ac2.Text != "Оружие 2" && tb_ac2.Text != "0" && tb_damage2.Text != "0")
                    {
                        damage2 = Convert.ToSingle(tb_damage2.Text.Replace(".", b));
                        ac2 = Convert.ToSingle(tb_ac2.Text.Replace(".", b));
                    }
                    skill = Convert.ToSingle(tb_skill.Text.Replace(".", b));
                }
                    main = Convert.ToSingle(tb_main.Text.Replace(".", b));
                    acincr = 0;
                    if (tb_acincr.Text != "") acincr = Convert.ToSingle(tb_acincr.Text.Replace(".", b));
                    cc = Convert.ToSingle(tb_cc.Text.Replace(".", b));
                    cd = Convert.ToSingle(tb_cd.Text.Replace(".", b));
                    off_min = 0; off_max = 0; am_min = 0; am_max = 0; r1_min = 0; r1_max = 0; r2_min = 0; r2_max = 0; other_min = 0; other_max = 0;
                    if (tb_off_min.Text != "") off_min = Convert.ToSingle(tb_off_min.Text.Replace(".", b));
                    if (tb_off_max.Text != "") off_max = Convert.ToSingle(tb_off_max.Text.Replace(".", b));
                    if (tb_am_min.Text != "") am_min = Convert.ToSingle(tb_am_min.Text.Replace(".", b));
                    if (tb_am_max.Text != "") am_max = Convert.ToSingle(tb_am_max.Text.Replace(".", b));
                    if (tb_r1_min.Text != "") r1_min = Convert.ToSingle(tb_r1_min.Text.Replace(".", b));
                    if (tb_r1_max.Text != "") r1_max = Convert.ToSingle(tb_r1_max.Text.Replace(".", b));
                    if (tb_r2_min.Text != "") r2_min = Convert.ToSingle(tb_r2_min.Text.Replace(".", b));
                    if (tb_r2_max.Text != "") r2_max = Convert.ToSingle(tb_r2_max.Text.Replace(".", b));
                    //if (tb_other_min.Text != "") other_min = Convert.ToSingle(tb_other_min.Text.Replace(".", b));
                    //if (tb_other_max.Text != "") other_max = Convert.ToSingle(tb_other_max.Text.Replace(".", b));
                    other_min = 0; other_max = 0;
                                       
                fromskills = 0; elem = 0; toskill = 0; elite = 0;
                if (tb_fromskills.Text != "") fromskills = Convert.ToSingle(tb_fromskills.Text.Replace(".", b));
                if (tb_elem.Text != "") elem = Convert.ToSingle(tb_elem.Text.Replace(".", b));
                if (tb_toskill.Text != "") toskill = Convert.ToSingle(tb_toskill.Text.Replace(".", b));
                if (tb_elite.Text != "") elite = Convert.ToSingle(tb_elite.Text.Replace(".", b));

                Calculate();

                lb_result.Text = result.ToString("N0");
                lb_result_profile.Text = result_profile.ToString("N0");
                result_old = result;

                //if (cb_wd.Checked)
                //Считаем урон питомцев WD
                if (lb_state.Text == "wd")
                {
                    CalculateWD();
                    lb_result_wd.Text = (result + result_wd).ToString("N0");
                    result_old_wd = result + result_wd;
                    pan_wddam.Visible = true;
                }


                b_stat.Enabled = true;
                if (pan_stat.Visible == false)
                {
                    pan_stat.Visible = true;
                    pan_info.Visible = false;
                    b_stat.Text = "Учёт умения с кулдауном";
                }

                acincr = acincr + 1; lb_ac.Visible = true; Calculate(); lb_ac.Text = (result - result_old).ToString("N0"); acincr = acincr - 1;
                main = main + 100; lb_main.Visible = true; Calculate(); lb_main.Text = (result - result_old).ToString("N0"); main = main - 100;
                cc = cc + 1; lb_cc.Visible = true; Calculate(); lb_cc.Text = (result - result_old).ToString("N0"); cc = cc - 1;
                cd = cd + 10; lb_cd.Visible = true; Calculate(); lb_cd.Text = (result - result_old).ToString("N0"); cd = cd - 10;
                if (b_adv.Text != "Высокая\r\nточность")
                {
                    elem1 = elem1 + 1; 
                    lb_elem.Visible = true;
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
                other_min = other_min + 100; lb_dmg.Visible = true; Calculate(); lb_dmg.Text = (result - result_old).ToString("N0"); other_min = other_min - 100;

                if (b_wep.Text != "Расчёт изменения оружия" && b_adv.Text != "Высокая\r\nточность")
                    {

                        if (tb_dmg1_w.Text == "") tb_dmg1_w.Text = "0";
                        if (tb_dmg2_w.Text == "") tb_dmg2_w.Text = "0";
                        ac1 = Math.Round((ac1 * 100 / (100 + ac1_p)), 1) * ((100 + ac1_p + Convert.ToSingle(nud_acp_w.Value)) / 100);
                        damage1 = (dmg1_1 + dmg1_2 + Convert.ToSingle(tb_dmg1_w.Text.Replace(".", b)) + Convert.ToSingle(tb_dmg2_w.Text.Replace(".", b))) / 2 * ac1 * (dmg1_p + Convert.ToSingle(nud_damp_w.Value) / 100 + 1);

                        main = main + Convert.ToSingle(nud_main_w.Value);
                        cd = cd + Convert.ToSingle(nud_cd_w.Value);
                        elem1 = elem1 + Convert.ToSingle(nud_elem_w.Value);
                        //MessageBox.Show(elem1.ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        skill = (skill1 * skill1_usage / 100 * (elem1 / 100 + 1) * (toskill1 / 100 + 1)) + (skill2 * skill2_usage / 100 * (elem2 / 100 + 1) * (toskill2 / 100 + 1));
                        Calculate();
                        lb_changed.Text = result.ToString("N0") + " (" + (result * 100 / result_old - 100).ToString("N2") + "%)";
                    }
                else
                if (nud_main.Value != 0 || nud_ac.Value != 0 || nud_cc.Value != 0 || nud_cd.Value != 0 || nud_elem.Value != 0 || nud_damage.Value != 0)
                {
                    main = main + Convert.ToSingle(nud_main.Value);
                    acincr = acincr + Convert.ToSingle(nud_ac.Value);
                    cc = cc + Convert.ToSingle(nud_cc.Value);
                    cd = cd + Convert.ToSingle(nud_cd.Value);
                    if (b_adv.Text != "Высокая\r\nточность") skill = (skill1 * skill1_usage / 100 * ((elem1 + Convert.ToSingle(nud_elem.Value)) / 100 + 1) * (toskill1 / 100 + 1)) + (skill2 * skill2_usage / 100 * (elem2 / 100 + 1) * (toskill2 / 100 + 1));
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
                }
                else lb_changed.Text = "Урон после изменений";
                //main_change=(((damage1 / ac1) + (damage2 / ac2)) / 2) + ((off_min + off_max + am_min + am_max + r1_min + r1_max + r2_min + r2_max + other_min + other_max) / 2)

            }
        }

        public void Calculate()
        {
            wepdamage = ((damage1 / ac1) + (damage2 / ac2)) / 2;
            otherdamage = (off_min + off_max + am_min + am_max + r1_min + r1_max + r2_min + r2_max + other_min + other_max) / 2;
            cleardamage = (wepdamage + otherdamage) * (main / 100 + 1);
            critdamage = cleardamage * (cc / 100 * cd / 100 + 1);
            
            if (b_adv.Text != "Высокая\r\nточность") overdamage = critdamage * (fromskills / 100 + 1) * (elite / 100 + 1); 
            else overdamage = critdamage * (fromskills / 100 + 1) * (elem / 100 + 1) * (toskill / 100 + 1) * (elite / 100 + 1);
            
            speedtotal = 2 / (1/ac1 + 1/ac2) * (acincr / 100 + 1);
            result = overdamage * skill / 100 * speedtotal;
            if (coold != 0)
                if (b_adv.Text != "Высокая\r\nточность")
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

        private void tb_damage1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_damage2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_ac1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_ac2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_main_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_acincr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_cc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_cd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_off_min_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_off_max_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_am_min_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_am_max_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_r1_min_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_r1_max_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_r2_min_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_r2_max_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_other_min_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tb_other_max_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tb_fromskills_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_elem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_toskill_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_elite_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_skill_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }


        private void tb_dmg1_1_a_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_dmg1_2_a_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_dmg2_1_a_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_dmg2_2_a_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_dmg1_p_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_dmg2_p_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_skill_usage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_skill2_usage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_skill1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_skill2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }


        private void tb_toskill2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_elem2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_toskill1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_elem1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_ac1_p_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_ac2_p_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_dmg1_w_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        private void tb_dmg2_w_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57))
                e.Handled = true;
        }

        //Закончили обрабатывать введённые в текстовые поля данные

        private void b_stat_Click(object sender, EventArgs e)
        {
            if (pan_stat.Visible == false)
            {
                pan_stat.Visible = true;
                pan_info.Visible = false;
                pan_cdr.Visible = false;
                b_stat.Text = "Учёт умения с кулдауном";
            }
            else
            {
                pan_stat.Visible = false;
                pan_info.Visible = false;
                pan_cdr.Visible = true;
                b_stat.Text = "Прирост DPS от характеристик";
            }
        }

        //private void cb_wd_Click(object sender, EventArgs e)
        //{
        //    if (cb_wd.Checked)
        //    {
        //        b_wd.Enabled = true;
        //        pan_wd.Visible = true;
        //        pan_wddam.Visible = true;
        //        b_wd.Text = "Изменение статов и DPS";
        //    }
        //    else
        //    {
        //        b_wd.Enabled = false;
        //        pan_wd.Visible = false;
        //        pan_wddam.Visible = false;
        //        b_wd.Text = "Расчёт урона от питомцев (WD)";
        //    }

        //}

        //private void b_wd_Click(object sender, EventArgs e)
        //{
        //    if (pan_wd.Visible == false)
        //    {
        //        pan_wd.Visible = true;
        //        b_wd.Text = "Изменение статов и DPS";
        //    }
        //    else
        //    {
        //        pan_wd.Visible = false;
        //        b_wd.Text = "Расчёт урона от питомцев (WD)";
        //    }
        //}

        private void Key_Test(object sender, KeyEventArgs e)
        {
            IDataObject iData = Clipboard.GetDataObject();
            decimal n;
            if (decimal.TryParse((String)iData.GetData(DataFormats.Text), out n) == false) Clipboard.SetDataObject("");
            if (e.KeyData == (Keys.Control | Keys.V))
                (sender as TextBox).Paste();
        }

        public void b_adv_Click(object sender, EventArgs e)
        {
            if (pan_da.Visible == false)
            {
                pan_da.Visible = true;
                pan_wa.Visible = true;
                pan_skill.Visible = true;
                pan_skillusage.Visible = true;
                pan_skillup.Visible = true;
                //b_wep.Enabled = true;
                pan_list.Visible = true;
                b_adv.Text = "Обычная\r\nточность";
            }
            else
            {
                pan_da.Visible = false;
                pan_wa.Visible = false;
                pan_skill.Visible = false;
                pan_skillusage.Visible = false;
                pan_skillup.Visible = false;
                //b_wep.Enabled = false;
                pan_list.Visible = false;
                pan_wep.Visible = false;
                pan_wd.Visible = false;
                b_adv.Text = "Высокая\r\nточность";
            }
        }

        private void tb_dmg2_1_a_MouseClick(object sender, MouseEventArgs e)
        {
            tb_dmg2_1_a.ReadOnly = false;
            if (tb_dmg2_1_a.Text == "" || tb_dmg2_1_a.Text == "min2") tb_dmg2_1_a.Text = "";
        }

        private void tb_dmg2_p_MouseClick(object sender, MouseEventArgs e)
        {
            tb_dmg2_p.ReadOnly = false;
            if (tb_dmg2_p.Text == "" || tb_dmg2_p.Text == "Оружие 2") tb_dmg2_p.Text = "";
        }

        private void tb_dmg1_p_MouseClick_1(object sender, MouseEventArgs e)
        {
            tb_dmg1_p.ReadOnly = false;
            if (tb_dmg1_p.Text == "" || tb_dmg1_p.Text == "Оружие 1") tb_dmg1_p.Text = "";
        }

        private void tb_dmg2_2_a_MouseClick(object sender, MouseEventArgs e)
        {
            tb_dmg2_2_a.ReadOnly = false;
            if (tb_dmg2_2_a.Text == "" || tb_dmg2_2_a.Text == "max2") tb_dmg2_2_a.Text = "";
        }

        private void tb_skill2_MouseClick(object sender, MouseEventArgs e)
        {
            tb_skill2.ReadOnly = false;
            if (tb_skill2.Text == "" || tb_skill2.Text == "skill2") tb_skill2.Text = "";
        }

        private void tb_skill_usage_MouseClick(object sender, MouseEventArgs e)
        {
            tb_skill1_usage.ReadOnly = false;
            if (tb_skill1_usage.Text == "" || tb_skill1_usage.Text == "100%") tb_skill1_usage.Text = "";
        }

        private void tb_skill2_usage_MouseClick(object sender, MouseEventArgs e)
        {
            tb_skill2_usage.ReadOnly = false;
            if (tb_skill2_usage.Text == "" || tb_skill2_usage.Text == "skill2") tb_skill2_usage.Text = "";
        }

        private void tb_damage2_MouseClick(object sender, MouseEventArgs e)
        {
            tb_damage2.ReadOnly = false;
            if (tb_damage2.Text == "" || tb_damage2.Text == "Оружие 2") tb_damage2.Text = "";
        }

        private void tb_ac2_MouseClick(object sender, MouseEventArgs e)
        {
            tb_ac2.ReadOnly = false;
            if (tb_ac2.Text == "" || tb_ac2.Text == "Оружие 2") tb_ac2.Text = "";
        }

        private void tb_toskill2_MouseClick(object sender, MouseEventArgs e)
        {
            tb_toskill2.ReadOnly = false;
            if (tb_toskill2.Text == "" || tb_toskill2.Text == "skill2") tb_toskill2.Text = "";
        }

        private void tb_elem2_MouseClick(object sender, MouseEventArgs e)
        {
            tb_elem2.ReadOnly = false;
            if (tb_elem2.Text == "" || tb_elem2.Text == "skill2") tb_elem2.Text = "";
        }

        private void tb_ac1_p_MouseClick(object sender, MouseEventArgs e)
        {
            tb_ac1_p.ReadOnly = false;
            if (tb_ac1_p.Text == "" || tb_ac1_p.Text == "Оружие 1") tb_ac1_p.Text = "";
        }

        private void tb_ac2_p_MouseClick(object sender, MouseEventArgs e)
        {
            tb_ac2_p.ReadOnly = false;
            if (tb_ac2_p.Text == "" || tb_ac2_p.Text == "Оружие 2") tb_ac2_p.Text = "";
        }

        private void b_wep_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(b_wep.Text.ToString(), "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (lb_state.Text == "wd" || (lb_state.Text == "wep" && cb_wd.Checked == false))
            {
                lb_state.Text = "roll";
                b_wep.Text = "Расчёт изменения оружия";
                pan_wep.Visible = false;
                pan_wd.Visible = false;
                pan_roll.Visible = true;
            }
            else
                if (lb_state.Text == "roll")
            {
                lb_state.Text = "wep";
                if (cb_wd.Checked) b_wep.Text = "Расчёт урона питомцев WD"; else b_wep.Text = "Расчёт изменения параметров";
                pan_wep.Visible = true;
                pan_wd.Visible = false;
            }
            else
            if (lb_state.Text == "wep" && cb_wd.Checked == true)
            {
                lb_state.Text = "wd";
                b_wep.Text = "Расчёт изменения параметров";
                pan_wep.Visible = false;
                pan_wd.Visible = true;
            }

        }

        //Закончили обрабатывать щелки по ReadOnly полям

        private void dps_diablo_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void lb_auth_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://glasscannon.ru/forum/viewtopic.php?f=5&t=149");
        }

        private void lb_auth_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            //lb_auth.Font = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Underline);
        }

        private void lb_auth_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }


    }

}
