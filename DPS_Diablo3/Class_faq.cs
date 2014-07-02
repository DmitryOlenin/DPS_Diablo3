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
        public int faq_flag = 0;

        public LinkLabel dot_net = new LinkLabel();
        public LinkLabel virustotal = new LinkLabel();
        public LinkLabel github = new LinkLabel();
        public Panel pan_faq = new Panel();

        private void LinkedLabelClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var web = (LinkLabel)sender;
            if (web.Name == "dot_net")
            {
                dot_net.LinkVisited = true;
                System.Diagnostics.Process.Start("http://www.microsoft.com/en-US/download/details.aspx?id=17718");
            }
            if (web.Name == "virustotal")
            {
                virustotal.LinkVisited = true;
                System.Diagnostics.Process.Start("https://www.virustotal.com");
            }
            if (web.Name == "github")
            {
                github.LinkVisited = true;
                System.Diagnostics.Process.Start("http://github.com/DmitryOlenin/DPS_Diablo3");
            }
        }

        private void faq(object sender, MouseEventArgs e)
        {
            if (faq_flag == 1)
            {
                this.ControlBox = true;
                pan_faq.Dispose();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                faq_flag = 0;
                foreach (Control contr in this.Controls)
                {
                    if (contr.Name == "Close") this.Controls.Remove(contr);
                }
                //b_faq.Location = new Point(395, 199);
                //b_faq.Text = "FAQ";
                return;
            }

            faq_flag = 1;
            int pos = 0;

            this.ControlBox = false;

            dot_net = new LinkLabel();
            virustotal = new LinkLabel();
            github = new LinkLabel();
            pan_faq = new Panel();

            pan_faq.AutoScroll = true;

            Label lab_faq_qu1 = new Label();
            Label lab_faq_an1 = new Label();
            Label lab_faq_q1 = new Label();
            Label lab_faq_a1 = new Label();

            Label lab_faq_qu2 = new Label();
            Label lab_faq_an2 = new Label();
            Label lab_faq_q2 = new Label();
            Label lab_faq_a2 = new Label();

            Label lab_faq_qu3 = new Label();
            Label lab_faq_an3 = new Label();
            Label lab_faq_q3 = new Label();
            Label lab_faq_a3 = new Label();

            Label lab_faq_qu4 = new Label();
            Label lab_faq_an4 = new Label();
            Label lab_faq_q4 = new Label();
            Label lab_faq_a4 = new Label();

            Label lab_faq_qu5 = new Label();
            Label lab_faq_an5 = new Label();
            Label lab_faq_q5 = new Label();
            Label lab_faq_a5 = new Label();

            Label lab_faq_qu6 = new Label();
            Label lab_faq_an6 = new Label();
            Label lab_faq_q6 = new Label();
            Label lab_faq_a6 = new Label();

            Label lab_faq_qu7 = new Label();
            Label lab_faq_an7 = new Label();
            Label lab_faq_q7 = new Label();
            Label lab_faq_a7 = new Label();

            Label lab_faq_qu8 = new Label();
            Label lab_faq_an8 = new Label();
            Label lab_faq_q8 = new Label();
            Label lab_faq_a8 = new Label();

            Label lab_faq_qu9 = new Label();
            Label lab_faq_an9 = new Label();
            Label lab_faq_q9 = new Label();
            Label lab_faq_a9 = new Label();

            Label lab_faq_qu10 = new Label();
            Label lab_faq_an10 = new Label();
            Label lab_faq_q10 = new Label();
            Label lab_faq_a10 = new Label();

            Label lab_faq_qu11 = new Label();
            Label lab_faq_an11 = new Label();
            Label lab_faq_q11 = new Label();
            Label lab_faq_a11 = new Label();

            Label lab_faq_qu12 = new Label();
            Label lab_faq_an12 = new Label();
            Label lab_faq_q12 = new Label();
            Label lab_faq_a12 = new Label();

            pan_faq.Location = new Point(0, 0);
            pan_faq.Size = new Size(this.Size.Width - 5, this.Size.Height - 28);//new Size(675, 368);
            this.Controls.Add(pan_faq);
            pan_faq.BringToFront();

            // ---------------------------------------------------- //

            lab_faq_q1.Text = lng.qu;
            lab_faq_q1.AutoSize = true;
            lab_faq_q1.Location = new Point(0, pos);
            lab_faq_q1.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a1.Text = lng.an;
            lab_faq_a1.AutoSize = true;
            lab_faq_a1.Location = new Point(0, pos + 15);
            lab_faq_a1.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu1.AutoSize = true;
            lab_faq_an1.AutoSize = true;
            lab_faq_qu1.Location = new Point(60, pos + 1);
            lab_faq_qu1.Text = lng.q1;
            pan_faq.Controls.Add(lab_faq_q1);
            pan_faq.Controls.Add(lab_faq_qu1);
            pan_faq.Controls.Add(lab_faq_a1);
            lab_faq_an1.Location = new Point(60, pos + 16);
            lab_faq_an1.Text = lng.a1_1 + "\n" + lng.a1_2;
            pan_faq.Controls.Add(lab_faq_an1);

            // ---------------------------------------------------- //

            pos += 60;

            lab_faq_q2.Text = lng.qu;
            lab_faq_q2.AutoSize = true;
            lab_faq_q2.Location = new Point(0, pos);
            lab_faq_q2.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a2.Text = lng.an;
            lab_faq_a2.AutoSize = true;
            lab_faq_a2.Location = new Point(0, pos + 15);
            lab_faq_a2.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu2.AutoSize = true;
            lab_faq_an2.AutoSize = true;
            lab_faq_qu2.Location = new Point(60, pos + 1);
            lab_faq_qu2.Text = lng.q2;
            pan_faq.Controls.Add(lab_faq_q2);
            pan_faq.Controls.Add(lab_faq_qu2);
            pan_faq.Controls.Add(lab_faq_a2);
            lab_faq_an2.Location = new Point(60, pos + 16);
            lab_faq_an2.Text = lng.a2_1;
            pan_faq.Controls.Add(lab_faq_an2);

            // ---------------------------------------------------- //

            pos += 45;

            lab_faq_q3.Text = lng.qu;
            lab_faq_q3.AutoSize = true;
            lab_faq_q3.Location = new Point(0, pos);
            lab_faq_q3.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a3.Text = lng.an;
            lab_faq_a3.AutoSize = true;
            lab_faq_a3.Location = new Point(0, pos + 15);
            lab_faq_a3.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu3.AutoSize = true;
            lab_faq_an3.AutoSize = true;
            lab_faq_qu3.Location = new Point(60, pos + 1);
            lab_faq_qu3.Text = lng.q3;
            pan_faq.Controls.Add(lab_faq_q3);
            pan_faq.Controls.Add(lab_faq_qu3);
            pan_faq.Controls.Add(lab_faq_a3);
            //lab_faq_an3.Location = new Point(60, 96);

            virustotal.AutoSize = true;
            virustotal.Text = lng.a3_1;
            virustotal.Location = new Point(60, pos + 16);
            virustotal.Name = "virustotal";
            if (bt_lang.Text == "ENG") virustotal.Links.Add(84, 16, "https://www.virustotal.com");
            else virustotal.Links.Add(93, 16, "https://www.virustotal.com");
            virustotal.LinkClicked += new LinkLabelLinkClickedEventHandler(this.LinkedLabelClicked);

            github.AutoSize = true;
            github.Text = lng.a3_2;
            github.Location = new Point(60, pos + 31);
            github.Name = "github";
            if (bt_lang.Text == "ENG") github.Links.Add(35, 35, "http://github.com/DmitryOlenin/DPS_Diablo3");
            else github.Links.Add(46, 35, "http://github.com/DmitryOlenin/DPS_Diablo3");
            github.LinkClicked += new LinkLabelLinkClickedEventHandler(this.LinkedLabelClicked);

            pan_faq.Controls.Add(virustotal);
            pan_faq.Controls.Add(github);

            //lab_faq_an3.Text = "Программа не содержит ничего, что могло бы повредить компьютеру. Проверить её вы можете на сайте virustotal.\nТакже есть исходные коды на сайте: github.com/DmitryOlenin/DPS_Diablo3";
            //pan_faq.Controls.Add(lab_faq_an3);

            // ---------------------------------------------------- //

            pos += 60;

            lab_faq_q4.Text = lng.qu;
            lab_faq_q4.AutoSize = true;
            lab_faq_q4.Location = new Point(0, pos);
            lab_faq_q4.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a4.Text = lng.an;
            lab_faq_a4.AutoSize = true;
            lab_faq_a4.Location = new Point(0, pos + 15);
            lab_faq_a4.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu4.AutoSize = true;
            lab_faq_an4.AutoSize = true;
            lab_faq_qu4.Location = new Point(60, pos + 1);
            lab_faq_qu4.Text = lng.q4;
            pan_faq.Controls.Add(lab_faq_q4);
            pan_faq.Controls.Add(lab_faq_qu4);
            pan_faq.Controls.Add(lab_faq_a4);
            //lab_faq_an4.Location = new Point(60, 141);
            //lab_faq_an4.Text = "";
            dot_net.AutoSize = true;
            dot_net.Text = lng.a4_1;
            dot_net.Location = new Point(60, pos + 16);
            dot_net.Name = "dot_net";
            if (bt_lang.Text == "ENG") dot_net.Links.Add(24, 35, "http://www.microsoft.com/en-US/download/details.aspx?id=17718");
            else dot_net.Links.Add(19, 35, "http://www.microsoft.com/en-US/download/details.aspx?id=17718");
            dot_net.LinkClicked += new LinkLabelLinkClickedEventHandler(this.LinkedLabelClicked);
            //dot_net.LinkClicked += LinkedLabelClicked();
            //pan_faq.Controls.Add(lab_faq_an4);
            pan_faq.Controls.Add(dot_net);

            // ---------------------------------------------------- //

            pos += 45;

            lab_faq_q5.Text = lng.qu;
            lab_faq_q5.AutoSize = true;
            lab_faq_q5.Location = new Point(0, pos);
            lab_faq_q5.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a5.Text = lng.an;
            lab_faq_a5.AutoSize = true;
            lab_faq_a5.Location = new Point(0, pos + 15);
            lab_faq_a5.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu5.AutoSize = true;
            lab_faq_an5.AutoSize = true;
            lab_faq_qu5.Location = new Point(60, pos + 1);
            lab_faq_qu5.Text = lng.q5;
            pan_faq.Controls.Add(lab_faq_q5);
            pan_faq.Controls.Add(lab_faq_qu5);
            pan_faq.Controls.Add(lab_faq_a5);
            lab_faq_an5.Location = new Point(60, pos + 16);
            lab_faq_an5.Text = lng.a5_1;
            pan_faq.Controls.Add(lab_faq_an5);

            // ---------------------------------------------------- //

            pos += 45;

            lab_faq_q6.Text = lng.qu;
            lab_faq_q6.AutoSize = true;
            lab_faq_q6.Location = new Point(0, pos);
            lab_faq_q6.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a6.Text = lng.an;
            lab_faq_a6.AutoSize = true;
            lab_faq_a6.Location = new Point(0, pos + 15);
            lab_faq_a6.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu6.AutoSize = true;
            lab_faq_an6.AutoSize = true;
            lab_faq_qu6.Location = new Point(60, pos + 1);
            lab_faq_qu6.Text = lng.q6;
            pan_faq.Controls.Add(lab_faq_q6);
            pan_faq.Controls.Add(lab_faq_qu6);
            pan_faq.Controls.Add(lab_faq_a6);
            lab_faq_an6.Location = new Point(60, pos + 16);
            lab_faq_an6.Text = lng.a6_1;
            pan_faq.Controls.Add(lab_faq_an6);

            // ---------------------------------------------------- //

            pos += 45;

            lab_faq_q7.Text = lng.qu;
            lab_faq_q7.AutoSize = true;
            lab_faq_q7.Location = new Point(0, pos);
            lab_faq_q7.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a7.Text = lng.an;
            lab_faq_a7.AutoSize = true;
            lab_faq_a7.Location = new Point(0, pos + 15);
            lab_faq_a7.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu7.AutoSize = true;
            lab_faq_an7.AutoSize = true;
            lab_faq_qu7.Location = new Point(60, pos + 1);
            lab_faq_qu7.Text = lng.q7;
            pan_faq.Controls.Add(lab_faq_q7);
            pan_faq.Controls.Add(lab_faq_qu7);
            pan_faq.Controls.Add(lab_faq_a7);
            lab_faq_an7.Location = new Point(60, pos + 16);
            lab_faq_an7.Text = lng.a7_1 + "\n" + lng.a7_2;
            pan_faq.Controls.Add(lab_faq_an7);

            // ---------------------------------------------------- //

            pos += 60;

            lab_faq_q8.Text = lng.qu;
            lab_faq_q8.AutoSize = true;
            lab_faq_q8.Location = new Point(0, pos);
            lab_faq_q8.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a8.Text = lng.an;
            lab_faq_a8.AutoSize = true;
            lab_faq_a8.Location = new Point(0, pos + 15);
            lab_faq_a8.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu8.AutoSize = true;
            lab_faq_an8.AutoSize = true;
            lab_faq_qu8.Location = new Point(60, pos + 1);
            lab_faq_qu8.Text = lng.q8;
            pan_faq.Controls.Add(lab_faq_q8);
            pan_faq.Controls.Add(lab_faq_qu8);
            pan_faq.Controls.Add(lab_faq_a8);
            lab_faq_an8.Location = new Point(60, pos + 16);
            lab_faq_an8.Text = lng.a8_1 + "\n" + lng.a8_2;
            pan_faq.Controls.Add(lab_faq_an8);

            // ---------------------------------------------------- //

            pos += 60;

            lab_faq_q9.Text = lng.qu;
            lab_faq_q9.AutoSize = true;
            lab_faq_q9.Location = new Point(0, pos);
            lab_faq_q9.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a9.Text = lng.an;
            lab_faq_a9.AutoSize = true;
            lab_faq_a9.Location = new Point(0, pos + 15);
            lab_faq_a9.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu9.AutoSize = true;
            lab_faq_an9.AutoSize = true;
            lab_faq_qu9.Location = new Point(60, pos + 1);
            lab_faq_qu9.Text = lng.q9;
            pan_faq.Controls.Add(lab_faq_q9);
            pan_faq.Controls.Add(lab_faq_qu9);
            pan_faq.Controls.Add(lab_faq_a9);
            lab_faq_an9.Location = new Point(60, pos + 16);
            lab_faq_an9.Text = lng.a9_1;
            pan_faq.Controls.Add(lab_faq_an9);

            // ---------------------------------------------------- //

            pos += 45;

            lab_faq_q10.Text = lng.qu;
            lab_faq_q10.AutoSize = true;
            lab_faq_q10.Location = new Point(0, pos);
            lab_faq_q10.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a10.Text = lng.an;
            lab_faq_a10.AutoSize = true;
            lab_faq_a10.Location = new Point(0, pos + 15);
            lab_faq_a10.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu10.AutoSize = true;
            lab_faq_an10.AutoSize = true;
            lab_faq_qu10.Location = new Point(60, pos + 1);
            lab_faq_qu10.Text = lng.q10;
            pan_faq.Controls.Add(lab_faq_q10);
            pan_faq.Controls.Add(lab_faq_qu10);
            pan_faq.Controls.Add(lab_faq_a10);
            lab_faq_an10.Location = new Point(60, pos + 16);
            lab_faq_an10.Text = lng.a10_1;
            pan_faq.Controls.Add(lab_faq_an10);

            // ---------------------------------------------------- //

            pos += 45;

            lab_faq_q11.Text = lng.qu;
            lab_faq_q11.AutoSize = true;
            lab_faq_q11.Location = new Point(0, pos);
            lab_faq_q11.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a11.Text = lng.an;
            lab_faq_a11.AutoSize = true;
            lab_faq_a11.Location = new Point(0, pos + 15);
            lab_faq_a11.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu11.AutoSize = true;
            lab_faq_an11.AutoSize = true;
            lab_faq_qu11.Location = new Point(60, pos + 1);
            lab_faq_qu11.Text = lng.q11;
            pan_faq.Controls.Add(lab_faq_q11);
            pan_faq.Controls.Add(lab_faq_qu11);
            pan_faq.Controls.Add(lab_faq_a11);
            lab_faq_an11.Location = new Point(60, pos + 16);
            lab_faq_an11.Text = lng.a11_1 + "\n" + lng.a11_2;
            pan_faq.Controls.Add(lab_faq_an11);

            // ---------------------------------------------------- //

            pos += 60;

            lab_faq_q12.Text = lng.qu;
            lab_faq_q12.AutoSize = true;
            lab_faq_q12.Location = new Point(0, pos);
            lab_faq_q12.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a12.Text = lng.an;
            lab_faq_a12.AutoSize = true;
            lab_faq_a12.Location = new Point(0, pos + 15);
            lab_faq_a12.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu12.AutoSize = true;
            lab_faq_an12.AutoSize = true;
            lab_faq_qu12.Location = new Point(60, pos + 1);
            lab_faq_qu12.Text = lng.q12;
            pan_faq.Controls.Add(lab_faq_q12);
            pan_faq.Controls.Add(lab_faq_qu12);
            pan_faq.Controls.Add(lab_faq_a12);
            lab_faq_an12.Location = new Point(60, pos + 16);
            lab_faq_an12.Text = lng.a12_1 + "\n\n";
            pan_faq.Controls.Add(lab_faq_an12);

            // ---------------------------------------------------- //

            pan_faq.Focus();
        }

    }
}
