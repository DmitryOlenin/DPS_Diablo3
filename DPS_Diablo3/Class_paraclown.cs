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

        public int[] bp_lvl = new int[] { 60, 70, 73, 74, 149, 250, 350, 449, 500, 550, 600, 650, 700, 750, 1000, 10000 };
        public UInt64[] bp_inc = new UInt64[] { 2880000, 5040000, 3660000, 1020000, 2040000, 4080000, 6120000, 8160000, 20400000, 40800000, 61200000, 81600000, 102000000, 122400000, 122400000 };
        public UInt64[] bp_sum = new UInt64[] { 2980800000, 4060800000, 4453920000, 4593660000, 17981160000, 50329440000, 113161440000, 225653160000, 309717480000, 428343480000, 623979480000, 947625480000, 1450281480000, 2182947480000, 10311327480000, 1031132748000000 };
        public UInt64[] bp_val = new UInt64[] { 92160000, 120960000, 136080000, 139740000, 216240000, 422280000, 830280000, 1436160000, 1852320000, 2872320000, 4912320000, 7972320000, 12052320000, 17152320000, 47752320000 };
        public Form frm_para_seasons = new Form();
        public Label lb_para_seasons_prev = new Label();
        public Label lb_para_seasons_next = new Label();
        public Label lb_para_seasons_exp_prev = new Label();
        public Label lb_para_seasons_exp_next = new Label();
        public Label lb_para_seasons_exp_sum = new Label();
        public Label lb_para_seasons_exp_lvl = new Label();
        public Label lb_para_seasons_exp_prev_val = new Label();
        public Label lb_para_seasons_exp_next_val = new Label();
        public Label lb_para_seasons_exp_sum_val = new Label();
        public Label lb_para_seasons_exp_lvl_val = new Label();
        public NumericUpDown nud_ps_prev = new NumericUpDown();
        public NumericUpDown nud_ps_next = new NumericUpDown();
        public Button b_ps_start = new Button();

        private void paragonSeasonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paragon_seasons();
            //frm_para_seasons.Visible = true;
        }

        private void Form_seasons_Move(object sender, EventArgs e)
        {

            frm_para_seasons.DesktopLocation = new Point(this.Location.X + 5, this.Location.Y + 50);

            Graphics g = this.CreateGraphics();
            if (g.DpiX >= 120)
            {
                frm_para_seasons.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
                frm_para_seasons.DesktopLocation = new Point(this.Location.X + 5, this.Location.Y + 60);
            }
            //frm_para_seasons.DesktopLocation = new Point(this.Location.X + 228, this.Location.Y + 238);
            //По центру пункта
            //if (Environment.OSVersion.Version.Major >= 6 && DwmIsCompositionEnabled())
                //frm_para_seasons.DesktopLocation = new Point(this.Location.X + 5, this.Location.Y + 50);
            //else frm_para_seasons.DesktopLocation = new Point(this.Location.X + 75, this.Location.Y + 50);
        }

        public void para_seasons_create()
        {
            frm_para_seasons.StartPosition = FormStartPosition.Manual;
            //frm_para.Owner = this;
            this.Move += Form_seasons_Move;
            frm_para_seasons.ControlBox = false;
            frm_para_seasons.Name = "Paragon Seasons";
            //frm_para.Opacity = 1;

            //По центру внизу
            //frm_para_seasons.Size = new Size(216, 210);
            //frm_para_seasons.MaximumSize = new Size(216, 210);
            //frm_para_seasons.MinimumSize = new Size(216, 210);

            //Возле меню
            frm_para_seasons.Size = new Size(235, 160);
            frm_para_seasons.MaximumSize = new Size(235, 160);
            frm_para_seasons.MinimumSize = new Size(235, 160);

            frm_para_seasons.ShowInTaskbar = false;
            frm_para_seasons.Visible = false;
            frm_para_seasons.KeyPreview = true;
            frm_para_seasons.KeyDown += new KeyEventHandler(frm_para_seasons_KeyDown);

            frm_para_seasons.Deactivate += new EventHandler(frm_para_seasons_Deactivate);
            //frm_para.Activated += new EventHandler(frm_para_Activated);
            frm_para_seasons.FormBorderStyle = FormBorderStyle.SizableToolWindow;


            int ypos = 1;
            ypos += 5;

            lb_para_seasons_prev.Location = new Point(15, ypos + 2);
            lb_para_seasons_prev.AutoSize = true;
            //lb_para_seasons_prev.Text = lng.lb_as + ":";
            lb_para_seasons_prev.Text = lng.lb_para_seasons_prev;
            frm_para_seasons.Controls.Add(lb_para_seasons_prev);

            nud_ps_prev.Location = new Point(143, ypos);
            nud_ps_prev.Size = new Size(50, 20);
            //nud_ps_prev.Value = 1;
            nud_ps_prev.Maximum = 1000;
            nud_ps_prev.Increment = 1M;
            nud_ps_prev.DecimalPlaces = 0;
            nud_ps_prev.Name = "ps_prev";
            nud_ps_prev.MouseClick += new MouseEventHandler(nud_clear);
            frm_para_seasons.Controls.Add(nud_ps_prev);

            ypos += 25;
            lb_para_seasons_next.Location = new Point(15, ypos + 2);
            lb_para_seasons_next.AutoSize = true;
            //lb_para_seasons_next.Text = lng.lb_cdr + ":";
            lb_para_seasons_next.Text = lng.lb_para_seasons_next;
            frm_para_seasons.Controls.Add(lb_para_seasons_next);

            nud_ps_next.Location = new Point(143, ypos);
            nud_ps_next.Size = new Size(50, 20);
            //nud_ps_next.Value = 1;
            nud_ps_next.Maximum = 1000;
            nud_ps_next.Increment = 1M;
            nud_ps_next.DecimalPlaces = 0;
            nud_ps_next.Name = "ps_next";
            nud_ps_next.MouseClick += new MouseEventHandler(nud_clear);
            frm_para_seasons.Controls.Add(nud_ps_next);

            //ypos += 25;
            //b_ps_start.Location = new Point(25, ypos);
            //b_ps_start.Name = "para_seasons_start";
            //b_ps_start.Text = "Start";
            //b_ps_start.Click += new EventHandler(para_seasons_calc);
            //frm_para_seasons.Controls.Add(b_ps_start);


            ypos += 30;
            lb_para_seasons_exp_prev.Location = new Point(15, ypos + 2);
            lb_para_seasons_exp_prev.AutoSize = true;
            //lb_para_seasons_prev.Text = lng.lb_as + ":";
            lb_para_seasons_exp_prev.Text = lng.lb_para_seasons_exp_prev;
            frm_para_seasons.Controls.Add(lb_para_seasons_exp_prev);

            lb_para_seasons_exp_prev_val.Location = new Point(105, ypos + 2);
            lb_para_seasons_exp_prev_val.AutoSize = true;
            //lb_para_seasons_prev.Text = lng.lb_as + ":";
            lb_para_seasons_exp_prev_val.Text = "";
            frm_para_seasons.Controls.Add(lb_para_seasons_exp_prev_val);

            ypos += 20;
            lb_para_seasons_exp_next.Location = new Point(15, ypos + 2);
            lb_para_seasons_exp_next.AutoSize = true;
            //lb_para_seasons_prev.Text = lng.lb_as + ":";
            lb_para_seasons_exp_next.Text = lng.lb_para_seasons_exp_next;
            frm_para_seasons.Controls.Add(lb_para_seasons_exp_next);

            lb_para_seasons_exp_next_val.Location = new Point(105, ypos + 2);
            lb_para_seasons_exp_next_val.AutoSize = true;
            //lb_para_seasons_prev.Text = lng.lb_as + ":";
            lb_para_seasons_exp_next_val.Text = "";
            frm_para_seasons.Controls.Add(lb_para_seasons_exp_next_val);

            ypos += 20;
            lb_para_seasons_exp_sum.Location = new Point(15, ypos + 2);
            lb_para_seasons_exp_sum.AutoSize = true;
            //lb_para_seasons_prev.Text = lng.lb_as + ":";
            lb_para_seasons_exp_sum.Text = lng.lb_para_seasons_exp_sum;
            frm_para_seasons.Controls.Add(lb_para_seasons_exp_sum);

            lb_para_seasons_exp_sum_val.Location = new Point(105, ypos + 2);
            lb_para_seasons_exp_sum_val.AutoSize = true;
            //lb_para_seasons_prev.Text = lng.lb_as + ":";
            lb_para_seasons_exp_sum_val.Text = "";
            frm_para_seasons.Controls.Add(lb_para_seasons_exp_sum_val);

            ypos += 20;
            lb_para_seasons_exp_lvl.Location = new Point(15, ypos + 2);
            lb_para_seasons_exp_lvl.AutoSize = true;
            //lb_para_seasons_prev.Text = lng.lb_as + ":";
            lb_para_seasons_exp_lvl.Text = lng.lb_para_seasons_exp_lvl;
            frm_para_seasons.Controls.Add(lb_para_seasons_exp_lvl);

            lb_para_seasons_exp_lvl_val.Location = new Point(105, ypos + 2);
            lb_para_seasons_exp_lvl_val.AutoSize = true;
            //lb_para_seasons_prev.Text = lng.lb_as + ":";
            lb_para_seasons_exp_lvl_val.Text = "";
            frm_para_seasons.Controls.Add(lb_para_seasons_exp_lvl_val);

            foreach (NumericUpDown nud in frm_para_seasons.Controls.OfType<NumericUpDown>())
            {
                nud.ValueChanged += new EventHandler(nud_seasons_inc_ValueChanged);
                nud.TextChanged += new EventHandler(nud_seasons_inc_ValueChanged);
            }
        }

        private void frm_para_seasons_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7 && frm_para_seasons.Visible)
            {
                foreach (NumericUpDown nud in frm_para_seasons.Controls.OfType<NumericUpDown>()) nud.Value = 0;
                lb_para_seasons_exp_prev_val.Text = "";
                lb_para_seasons_exp_next_val.Text = "";
                lb_para_seasons_exp_sum_val.Text = "";
                lb_para_seasons_exp_lvl_val.Text = "";
            }
            if (e.KeyCode == Keys.F2 && frm_para_seasons.Visible)
            {
                frm_para_seasons.Visible = false;
            }
        }

        private void frm_para_seasons_Deactivate(object sender, EventArgs e)
        {
            frm_para_seasons.Visible = false;
        }

        public void paragon_seasons()
        {

            if (!frm_para_seasons.Controls.Contains(lb_para_seasons_next))
            {
                int ypos = 1;
                ypos += 5;

                lb_para_seasons_prev.Location = new Point(15, ypos + 2);
                lb_para_seasons_prev.AutoSize = true;
                //lb_para_seasons_prev.Text = lng.lb_as + ":";
                lb_para_seasons_prev.Text = lng.lb_para_seasons_prev;
                frm_para_seasons.Controls.Add(lb_para_seasons_prev);

                nud_ps_prev.Location = new Point(143, ypos);
                nud_ps_prev.Size = new Size(50, 20);
                //nud_ps_prev.Value = 1;
                nud_ps_prev.Maximum = 1000;
                nud_ps_prev.Increment = 1M;
                nud_ps_prev.DecimalPlaces = 0;
                nud_ps_prev.Name = "ps_prev";
                nud_ps_prev.MouseClick += new MouseEventHandler(nud_clear);
                frm_para_seasons.Controls.Add(nud_ps_prev);

                ypos += 25;
                lb_para_seasons_next.Location = new Point(15, ypos + 2);
                lb_para_seasons_next.AutoSize = true;
                //lb_para_seasons_next.Text = lng.lb_cdr + ":";
                lb_para_seasons_next.Text = lng.lb_para_seasons_next;
                frm_para_seasons.Controls.Add(lb_para_seasons_next);

                nud_ps_next.Location = new Point(143, ypos);
                nud_ps_next.Size = new Size(50, 20);
                //nud_ps_next.Value = 1;
                nud_ps_next.Maximum = 1000;
                nud_ps_next.Increment = 1M;
                nud_ps_next.DecimalPlaces = 0;
                nud_ps_next.Name = "ps_next";
                nud_ps_next.MouseClick += new MouseEventHandler(nud_clear);
                frm_para_seasons.Controls.Add(nud_ps_next);

                //ypos += 25;
                //b_ps_start.Location = new Point(25, ypos);
                //b_ps_start.Name = "para_seasons_start";
                //b_ps_start.Text = "Start";
                //b_ps_start.Click += new EventHandler(para_seasons_calc);
                //frm_para_seasons.Controls.Add(b_ps_start);


                ypos += 30;
                lb_para_seasons_exp_prev.Location = new Point(15, ypos + 2);
                lb_para_seasons_exp_prev.AutoSize = true;
                //lb_para_seasons_prev.Text = lng.lb_as + ":";
                lb_para_seasons_exp_prev.Text = lng.lb_para_seasons_exp_prev;
                frm_para_seasons.Controls.Add(lb_para_seasons_exp_prev);

                lb_para_seasons_exp_prev_val.Location = new Point(105, ypos + 2);
                lb_para_seasons_exp_prev_val.AutoSize = true;
                //lb_para_seasons_prev.Text = lng.lb_as + ":";
                lb_para_seasons_exp_prev_val.Text = "";
                frm_para_seasons.Controls.Add(lb_para_seasons_exp_prev_val);

                ypos += 20;
                lb_para_seasons_exp_next.Location = new Point(15, ypos + 2);
                lb_para_seasons_exp_next.AutoSize = true;
                //lb_para_seasons_prev.Text = lng.lb_as + ":";
                lb_para_seasons_exp_next.Text = lng.lb_para_seasons_exp_next;
                frm_para_seasons.Controls.Add(lb_para_seasons_exp_next);

                lb_para_seasons_exp_next_val.Location = new Point(105, ypos + 2);
                lb_para_seasons_exp_next_val.AutoSize = true;
                //lb_para_seasons_prev.Text = lng.lb_as + ":";
                lb_para_seasons_exp_next_val.Text = "";
                frm_para_seasons.Controls.Add(lb_para_seasons_exp_next_val);

                ypos += 20;
                lb_para_seasons_exp_sum.Location = new Point(15, ypos + 2);
                lb_para_seasons_exp_sum.AutoSize = true;
                //lb_para_seasons_prev.Text = lng.lb_as + ":";
                lb_para_seasons_exp_sum.Text = lng.lb_para_seasons_exp_sum;
                frm_para_seasons.Controls.Add(lb_para_seasons_exp_sum);

                lb_para_seasons_exp_sum_val.Location = new Point(105, ypos + 2);
                lb_para_seasons_exp_sum_val.AutoSize = true;
                //lb_para_seasons_prev.Text = lng.lb_as + ":";
                lb_para_seasons_exp_sum_val.Text = "";
                frm_para_seasons.Controls.Add(lb_para_seasons_exp_sum_val);

                ypos += 20;
                lb_para_seasons_exp_lvl.Location = new Point(15, ypos + 2);
                lb_para_seasons_exp_lvl.AutoSize = true;
                //lb_para_seasons_prev.Text = lng.lb_as + ":";
                lb_para_seasons_exp_lvl.Text = lng.lb_para_seasons_exp_lvl;
                frm_para_seasons.Controls.Add(lb_para_seasons_exp_lvl);

                lb_para_seasons_exp_lvl_val.Location = new Point(105, ypos + 2);
                lb_para_seasons_exp_lvl_val.AutoSize = true;
                //lb_para_seasons_prev.Text = lng.lb_as + ":";
                lb_para_seasons_exp_lvl_val.Text = "";
                frm_para_seasons.Controls.Add(lb_para_seasons_exp_lvl_val);

                foreach (NumericUpDown nud in frm_para_seasons.Controls.OfType<NumericUpDown>())
                {
                    nud.ValueChanged += new EventHandler(nud_seasons_inc_ValueChanged);
                    nud.TextChanged += new EventHandler(nud_seasons_inc_ValueChanged);
                }
            
            }

            frm_para_seasons.Visible = true;
            frm_para_seasons.TopMost = true;
            //frm_para_seasons.Show();
        }

        private void nud_clear(object sender, MouseEventArgs e)
        {
            //((NumericUpDown)sender).Text = "";
            ((NumericUpDown)sender).Select(0,4);
        }

        void nud_seasons_inc_ValueChanged(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            //if (nud_ps_prev.Text == "" && nud.Name != "ps_prev") nud_ps_prev.Value = 1;
            //if (nud_ps_next.Text == "" && nud.Name != "ps_next") nud_ps_next.Value = 1;
            para_seasons_calc(null, null);
            nud.Focus();
        }

        //private void b_test_Click(object sender, EventArgs e)
        //{
        //    para_seasons_calc(null,null);
        //}

        public void para_seasons_calc(object sender, EventArgs e)
        {
            UInt64 sum_prev = 0;
            UInt64 sum_next = 0;
            UInt64 sum_all = 0;
            UInt64 sum_find = 0;
            int num_prev = 0;
            int num_next = 0;
            int num_start = 0;
            int num_start_next = 0;
            int num_find = 0;
            int lvl_start = 1;
            int lvl_end = 60;
            int num_more = 0;

            //if (nud_ps_prev.Text == "") nud_ps_prev.Text = "1";
            //if (nud_ps_next.Text == "") nud_ps_next.Text = "1";
            if (nud_ps_prev.Value < 1) nud_ps_prev.Value = 1;
            if (nud_ps_next.Value < 1) nud_ps_next.Value = 1;

            for (int i = 0; i < 16; i++)
            {
                if (bp_lvl[i] > nud_ps_prev.Value)
                {
                    num_prev = i - 1;
                    break;
                }
            }

            for (int i = 0; i < 16; i++)
            {
                if (bp_lvl[i] > nud_ps_next.Value)
                {
                    num_next = i - 1;
                    break;
                }
            }

            num_start = num_prev;
            if (num_next > num_prev) num_start = num_next;
            if (num_start < 0) num_start = 0;

            //Сумма опыта на уровне = (2 * (ближайший брекпоинт) + (прирост * (нужный уровень - уровень брекпоинта))) / 2 * (нужный уровень - уровень брекпоинта + 1) - (ближайший брекпоинт) + (сумма на предыдущем брекпоинте)

            if (num_prev > 0) sum_prev = (2 * bp_val[num_prev] + (bp_inc[num_prev] * Convert.ToUInt64(nud_ps_prev.Value - bp_lvl[num_prev]))) / 2 * Convert.ToUInt64(nud_ps_prev.Value - bp_lvl[num_prev] + 1) - bp_val[num_prev] + bp_sum[num_prev];
            else sum_prev = (2 * 7200000 + (1440000 * Convert.ToUInt64(nud_ps_prev.Value - 1))) / 2 * Convert.ToUInt64(nud_ps_prev.Value);

            if (num_next > 0) sum_next = (2 * bp_val[num_next] + (bp_inc[num_next] * Convert.ToUInt64(nud_ps_next.Value - bp_lvl[num_next]))) / 2 * Convert.ToUInt64(nud_ps_next.Value - bp_lvl[num_next] + 1) - bp_val[num_next] + bp_sum[num_next];
            else sum_next = (2 * 7200000 + (1440000 * Convert.ToUInt64(nud_ps_next.Value - 1))) / 2 * Convert.ToUInt64(nud_ps_next.Value);

            sum_all = sum_prev + sum_next;

            //MessageBox.Show(bp_val[num+1].ToString());
            //MessageBox.Show(bp_inc[num+1].ToString());
            //MessageBox.Show(Convert.ToUInt64(nud_prev.Value - bp_lvl[num]).ToString());
            //MessageBox.Show(bp_sum[num].ToString());

            //MessageBox.Show("Текущая сумма опыта: " + sum_prev.ToString("N0") + "\nСледующая сумма опыта: " + sum_next.ToString("N0"));

            for (int i = num_start; i < 16; i++)
            {
                if (bp_sum[i] > sum_all)
                {
                    if (i > 0) num_more = 1;
                    num_start_next = i - 1;
                    if (num_start_next < 1) num_start_next = 0;
                    //MessageBox.Show("Старт: " + bp_lvl[num_start_next].ToString() + " Конец: " + bp_lvl[num_start_next + 1].ToString() + " Сумма брекпоинта: " + bp_sum[i].ToString() + " Общая сумма: " + sum_all.ToString());
                    //MessageBox.Show(num_start_next.ToString());
                    break;
                }
            }

            if (num_more > 0) lvl_start = bp_lvl[num_start_next];
            if (num_more > 0) lvl_end = bp_lvl[num_start_next + 1];
            //if (sum_all > 2797920000) MessageBox.Show("Старт " + lvl_start.ToString() + " Уровень до чего считать: " + lvl_end.ToString());

            //MessageBox.Show(lvl_start.ToString() + " " + lvl_end.ToString());
            //MessageBox.Show(sum_all.ToString());

            for (int i = lvl_start; i < lvl_end+1; i++)
            {
                //MessageBox.Show(i.ToString("N0"));
                //if (sum_all > 2825920000) MessageBox.Show("Старт " + i.ToString() + " Сумма найденная: " + sum_find.ToString() + " Сумма общая: " + sum_all.ToString());
                if (i > 59) sum_find = (2 * bp_val[num_start_next] + (bp_inc[num_start_next] * Convert.ToUInt64(i - bp_lvl[num_start_next]))) / 2 * Convert.ToUInt64(i - bp_lvl[num_start_next] + 1) - bp_val[num_start_next] + bp_sum[num_start_next];
                else sum_find = (2 * 7200000 + (1440000 * Convert.ToUInt64(i - 1))) / 2 * Convert.ToUInt64(i);
                if (sum_find > sum_all)
                {
                    //MessageBox.Show(sum_find.ToString());
                    num_find = i - 1;
                    break;
                }

            }

            //49 + 29
            //MessageBox.Show("Суммарный опыт: " + sum_all.ToString("N0") + "\nУровень для этой суммы: " + num_find.ToString("N0"));

            lb_para_seasons_exp_prev_val.Text = sum_prev.ToString("N0");
            lb_para_seasons_exp_next_val.Text = sum_next.ToString("N0");
            lb_para_seasons_exp_sum_val.Text = sum_all.ToString("N0");
            lb_para_seasons_exp_lvl_val.Text = num_find.ToString("N0");

        }

    }
}
