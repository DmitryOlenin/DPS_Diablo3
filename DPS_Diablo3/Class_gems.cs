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

        public Form frm_gems_up = new Form();
        public Label lb_gem_lvl = new Label();
        public Label lb_rift_lvl = new Label();
        public Label lb_desc = new Label();
        public Label lb_up1 = new Label();
        public Label lb_up2 = new Label();
        public Label lb_up3 = new Label();
        public Label lb_up1_desc = new Label();
        public Label lb_up2_desc = new Label();
        public Label lb_up3_desc = new Label();
        public Label lb_pr1 = new Label();
        public Label lb_pr2 = new Label();
        public Label lb_pr3 = new Label();
        public NumericUpDown nud_gem_lvl = new NumericUpDown();
        public NumericUpDown nud_rift_lvl = new NumericUpDown();

        public void gems_up_create()
        {
            frm_gems_up.StartPosition = FormStartPosition.Manual;
            //frm_para.Owner = this;
            this.Move += Form_gems_up_Move;
            frm_gems_up.ControlBox = false;
            frm_gems_up.Name = "Gems leveling";
            //frm_para.Opacity = 1;

            //По центру внизу
            //frm_gems_up.Size = new Size(216, 210);
            //frm_gems_up.MaximumSize = new Size(216, 210);
            //frm_gems_up.MinimumSize = new Size(216, 210);

            //Возле меню
            frm_gems_up.Size = new Size(235, 160);
            frm_gems_up.MaximumSize = new Size(235, 160);
            frm_gems_up.MinimumSize = new Size(235, 160);

            frm_gems_up.ShowInTaskbar = false;
            frm_gems_up.Visible = false;
            frm_gems_up.KeyPreview = true;
            frm_gems_up.KeyDown += new KeyEventHandler(frm_gems_up_KeyDown);

            frm_gems_up.Deactivate += new EventHandler(frm_gems_up_Deactivate);
            //frm_para.Activated += new EventHandler(frm_para_Activated);
            frm_gems_up.FormBorderStyle = FormBorderStyle.SizableToolWindow;

            int ypos = 1;
            ypos += 5;

            lb_gem_lvl.Location = new Point(25, ypos + 2);
            lb_gem_lvl.AutoSize = true;
            lb_gem_lvl.Text = lng.lb_gem_lvl;
            frm_gems_up.Controls.Add(lb_gem_lvl);

            nud_gem_lvl.Location = new Point(143, ypos);
            nud_gem_lvl.Size = new Size(50, 20);
            nud_gem_lvl.Maximum = 100;
            nud_gem_lvl.Increment = 1M;
            nud_gem_lvl.DecimalPlaces = 0;
            nud_gem_lvl.Name = "gem_lvl";
            //nud_gem_lvl.MouseClick += new MouseEventHandler(nud_clear);
            frm_gems_up.Controls.Add(nud_gem_lvl);

            ypos += 25;
            lb_rift_lvl.Location = new Point(25, ypos + 2);
            lb_rift_lvl.AutoSize = true;
            lb_rift_lvl.Text = lng.lb_rift_lvl;
            frm_gems_up.Controls.Add(lb_rift_lvl);

            nud_rift_lvl.Location = new Point(143, ypos);
            nud_rift_lvl.Size = new Size(50, 20);
            nud_rift_lvl.Maximum = 100;
            nud_rift_lvl.Increment = 1M;
            nud_rift_lvl.DecimalPlaces = 0;
            nud_rift_lvl.Name = "rift_lvl";
            //nud_rift_lvl.MouseClick += new MouseEventHandler(nud_clear);
            frm_gems_up.Controls.Add(nud_rift_lvl);

            ypos += 25;

            lb_desc.Location = new Point(30, ypos);
            lb_desc.AutoSize = true;
            lb_desc.Text = lng.lb_desc;
            frm_gems_up.Controls.Add(lb_desc);

            ypos += 20;

            lb_up1_desc.Location = new Point(45, ypos);
            lb_up1_desc.AutoSize = true;
            lb_up1_desc.Text = lng.lb_up1_desc;
            frm_gems_up.Controls.Add(lb_up1_desc);

            lb_up1.Location = new Point(120, ypos);
            lb_up1.AutoSize = true;
            frm_gems_up.Controls.Add(lb_up1);

            lb_pr1.Location = new Point(143, ypos);
            lb_pr1.Text = "%";
            lb_pr1.Visible = false;
            frm_gems_up.Controls.Add(lb_pr1);

            ypos += 25;

            lb_up2_desc.Location = new Point(45, ypos);
            lb_up2_desc.AutoSize = true;
            lb_up2_desc.Text = lng.lb_up2_desc;
            frm_gems_up.Controls.Add(lb_up2_desc);

            lb_up2.Location = new Point(120, ypos);
            lb_up2.AutoSize = true;
            frm_gems_up.Controls.Add(lb_up2);

            lb_pr2.Location = new Point(143, ypos);
            lb_pr2.Text = "%";
            lb_pr2.Visible = false;
            frm_gems_up.Controls.Add(lb_pr2);

            ypos += 25;

            lb_up3_desc.Location = new Point(45, ypos);
            lb_up3_desc.AutoSize = true;
            lb_up3_desc.Text = lng.lb_up3_desc;
            frm_gems_up.Controls.Add(lb_up3_desc);

            lb_up3.Location = new Point(120, ypos);
            lb_up3.AutoSize = true;
            frm_gems_up.Controls.Add(lb_up3);

            lb_pr3.Location = new Point(143, ypos);
            lb_pr3.Text = "%";
            lb_pr3.Visible = false;
            frm_gems_up.Controls.Add(lb_pr3);

            foreach (NumericUpDown nud in frm_gems_up.Controls.OfType<NumericUpDown>())
            {
                nud.ValueChanged += new EventHandler(nud_gems_up_ValueChanged);
                nud.TextChanged += new EventHandler(nud_gems_up_ValueChanged);
            }
        }

        private void gemsGreaterRiftsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gems_up();
        }

        public void gems_up()
        {

            //if (!frm_gems_up.Controls.Contains(lb_rift_lvl))
            //{

            //}

            frm_gems_up.Visible = true;
            frm_gems_up.TopMost = true;
            //frm_gems_up.Show();
        }

        private void Form_gems_up_Move(object sender, EventArgs e)
        {

            frm_gems_up.DesktopLocation = new Point(this.Location.X + 5, this.Location.Y + 50);

            Graphics g = this.CreateGraphics();
            if (g.DpiX >= 120)
            {
                frm_gems_up.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
                frm_gems_up.DesktopLocation = new Point(this.Location.X + 5, this.Location.Y + 60);
            }
            //frm_gems_up.DesktopLocation = new Point(this.Location.X + 228, this.Location.Y + 238);
            //По центру пункта
            //if (Environment.OSVersion.Version.Major >= 6 && DwmIsCompositionEnabled())
            //frm_gems_up.DesktopLocation = new Point(this.Location.X + 5, this.Location.Y + 50);
            //else frm_gems_up.DesktopLocation = new Point(this.Location.X + 75, this.Location.Y + 50);
        }

        private void frm_gems_up_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7 && frm_gems_up.Visible)
            {
                foreach (NumericUpDown nud in frm_gems_up.Controls.OfType<NumericUpDown>()) nud.Value = 0;
                lb_up1.Text = "";
                lb_up2.Text = "";
                lb_up3.Text = "";
            }
            if (e.KeyCode == Keys.F2 && frm_gems_up.Visible)
            {
                frm_gems_up.Visible = false;
            }
        }

        private void frm_gems_up_Deactivate(object sender, EventArgs e)
        {
            frm_gems_up.Visible = false;
        }

        void nud_gems_up_ValueChanged(object sender, EventArgs e)
        {
            var nud = (NumericUpDown)sender;
            //if (nud_ps_prev.Text == "" && nud.Name != "ps_prev") nud_ps_prev.Value = 1;
            //if (nud_ps_next.Text == "" && nud.Name != "ps_next") nud_ps_next.Value = 1;
            nud_gems_calc(null, null);
            nud.Focus();
        }

        public void nud_gems_calc(object sender, EventArgs e)
        {
            int[] gem_up = new int[] { 0, 0, 0 };
            int gem_lvl = 0, rift_lvl=0, chance=0;
            gem_lvl = Convert.ToInt16(nud_gem_lvl.Value);
            rift_lvl = Convert.ToInt16(nud_rift_lvl.Value);

            for (int i = 0; i < 3; i++)
            {
                if ((gem_lvl - rift_lvl) <= -10) chance = 100;
                if ((gem_lvl - rift_lvl) == -9) chance = 90;
                if ((gem_lvl - rift_lvl) == -8) chance = 80;
                if ((gem_lvl - rift_lvl) == -7) chance = 70;
                if ((gem_lvl - rift_lvl) >= -6 && (gem_lvl - rift_lvl) <= 0) chance = 60;
                if ((gem_lvl - rift_lvl) == 1) chance = 30;
                if ((gem_lvl - rift_lvl) == 2) chance = 15;
                if ((gem_lvl - rift_lvl) == 3) chance = 8;
                if ((gem_lvl - rift_lvl) == 4) chance = 4;
                if ((gem_lvl - rift_lvl) == 5) chance = 2;
                if ((gem_lvl - rift_lvl) == 6) chance = 1;
                if ((gem_lvl - rift_lvl) >= 7) chance = 0;
                gem_lvl += 1;

                if (i == 0) lb_up1.Text = chance.ToString();
                if (i == 1) lb_up2.Text = chance.ToString();
                if (i == 2) lb_up3.Text = chance.ToString();

                lb_pr1.Visible = true; lb_pr2.Visible = true; lb_pr3.Visible = true;
                //gem_up[i] = chance;
            }



        }

    }
}
