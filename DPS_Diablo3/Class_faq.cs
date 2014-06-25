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
            if (web.Name == "ll_ver")
            {
                ll_ver.LinkVisited = true;
                System.Diagnostics.Process.Start("https://dl.dropboxusercontent.com/u/14539335/DPS_Diablo3.zip");
            }
        }

        public void faq()
        {
            if (faq_flag == 1)
            {
                pan_faq.Dispose();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                faq_flag = 0;
                b_faq.Location = new Point(395, 199);
                b_faq.Text = "FAQ";
                return;
            }

            faq_flag = 1;
            int pos = 0;

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
            pan_faq.Size = new Size(675, 368);
            this.Controls.Add(pan_faq);
            pan_faq.BringToFront();

            lab_faq_q1.Text = "Вопрос: ";
            lab_faq_q1.AutoSize = true;
            lab_faq_q1.Location = new Point(0, pos);
            lab_faq_q1.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a1.Text = "Ответ: ";
            lab_faq_a1.AutoSize = true;
            lab_faq_a1.Location = new Point(0, pos+15);
            lab_faq_a1.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu1.AutoSize = true;
            lab_faq_an1.AutoSize = true;
            lab_faq_qu1.Location = new Point(60, pos+1);
            lab_faq_qu1.Text = "Для чего эта программа нужна?";
            pan_faq.Controls.Add(lab_faq_q1);
            pan_faq.Controls.Add(lab_faq_qu1);
            pan_faq.Controls.Add(lab_faq_a1);
            lab_faq_an1.Location = new Point(60, pos+16);
            lab_faq_an1.Text = "Программа нужна для вычисления урона 1 или 2 скиллов, которыми пользуется персонаж.\nТакже она может быть полезна при изменении экипировки или вложении очков парагона.";
            pan_faq.Controls.Add(lab_faq_an1);

            // ---------------------------------------------------- //

            pos += 60;

            lab_faq_q2.Text = "Вопрос: ";
            lab_faq_q2.AutoSize = true;
            lab_faq_q2.Location = new Point(0, pos);
            lab_faq_q2.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a2.Text = "Ответ: ";
            lab_faq_a2.AutoSize = true;
            lab_faq_a2.Location = new Point(0, pos + 15);
            lab_faq_a2.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu2.AutoSize = true;
            lab_faq_an2.AutoSize = true;
            lab_faq_qu2.Location = new Point(60, pos + 1);
            lab_faq_qu2.Text = "Опасно ли пользоваться данной программой, не могут ли за это забанить?";
            pan_faq.Controls.Add(lab_faq_q2);
            pan_faq.Controls.Add(lab_faq_qu2);
            pan_faq.Controls.Add(lab_faq_a2);
            lab_faq_an2.Location = new Point(60, pos + 16);
            lab_faq_an2.Text = "Нет, не опасно, забанить не могут, программа никак не взаимодействует с игрой.";
            pan_faq.Controls.Add(lab_faq_an2);

            // ---------------------------------------------------- //

            pos += 45;

            lab_faq_q3.Text = "Вопрос: ";
            lab_faq_q3.AutoSize = true;
            lab_faq_q3.Location = new Point(0, pos);
            lab_faq_q3.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a3.Text = "Ответ: ";
            lab_faq_a3.AutoSize = true;
            lab_faq_a3.Location = new Point(0, pos + 15);
            lab_faq_a3.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu3.AutoSize = true;
            lab_faq_an3.AutoSize = true;
            lab_faq_qu3.Location = new Point(60, pos + 1);
            lab_faq_qu3.Text = "Я боюсь вирусов, кражи личной информации и призраков. Как мне быть?";
            pan_faq.Controls.Add(lab_faq_q3);
            pan_faq.Controls.Add(lab_faq_qu3);
            pan_faq.Controls.Add(lab_faq_a3);
            //lab_faq_an3.Location = new Point(60, 96);

            virustotal.AutoSize = true;
            virustotal.Text = "Программа не содержит ничего, что могло бы повредить компьютеру. Проверить можно на сайте virustotal.";
            virustotal.Location = new Point(60, pos + 16);
            virustotal.Name = "virustotal";
            virustotal.Links.Add(84, 16, "https://www.virustotal.com");
            virustotal.LinkClicked += new LinkLabelLinkClickedEventHandler(this.LinkedLabelClicked);

            github.AutoSize = true;
            github.Text = "Также есть исходные коды на сайте: github.com/DmitryOlenin/DPS_Diablo3.";
            github.Location = new Point(60, pos + 31);
            github.Name = "github";
            github.Links.Add(35, 35, "http://github.com/DmitryOlenin/DPS_Diablo3");
            github.LinkClicked += new LinkLabelLinkClickedEventHandler(this.LinkedLabelClicked);

            pan_faq.Controls.Add(virustotal);
            pan_faq.Controls.Add(github);

            //lab_faq_an3.Text = "Программа не содержит ничего, что могло бы повредить компьютеру. Проверить её вы можете на сайте virustotal.\nТакже есть исходные коды на сайте: github.com/DmitryOlenin/DPS_Diablo3";
            //pan_faq.Controls.Add(lab_faq_an3);

            // ---------------------------------------------------- //

            pos += 60;

            lab_faq_q4.Text = "Вопрос: ";
            lab_faq_q4.AutoSize = true;
            lab_faq_q4.Location = new Point(0, pos);
            lab_faq_q4.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a4.Text = "Ответ: ";
            lab_faq_a4.AutoSize = true;
            lab_faq_a4.Location = new Point(0, pos + 15);
            lab_faq_a4.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu4.AutoSize = true;
            lab_faq_an4.AutoSize = true;
            lab_faq_qu4.Location = new Point(60, pos + 1);
            lab_faq_qu4.Text = "Программа не запускается, что делать?";
            pan_faq.Controls.Add(lab_faq_q4);
            pan_faq.Controls.Add(lab_faq_qu4);
            pan_faq.Controls.Add(lab_faq_a4);
            //lab_faq_an4.Location = new Point(60, 141);
            //lab_faq_an4.Text = "";
            dot_net.AutoSize = true;
            dot_net.Text = "Вам следует установить .Net Framework 4.0 с сайта Microsoft.";
            dot_net.Location = new Point(60, pos + 16);
            dot_net.Name = "dot_net";
            dot_net.Links.Add(24, 35, "http://www.microsoft.com/en-US/download/details.aspx?id=17718");
            dot_net.LinkClicked += new LinkLabelLinkClickedEventHandler(this.LinkedLabelClicked);
            //dot_net.LinkClicked += LinkedLabelClicked();
            //pan_faq.Controls.Add(lab_faq_an4);
            pan_faq.Controls.Add(dot_net);

            // ---------------------------------------------------- //

            pos += 45;

            lab_faq_q5.Text = "Вопрос: ";
            lab_faq_q5.AutoSize = true;
            lab_faq_q5.Location = new Point(0, pos);
            lab_faq_q5.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a5.Text = "Ответ: ";
            lab_faq_a5.AutoSize = true;
            lab_faq_a5.Location = new Point(0, pos + 15);
            lab_faq_a5.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu5.AutoSize = true;
            lab_faq_an5.AutoSize = true;
            lab_faq_qu5.Location = new Point(60, pos + 1);
            lab_faq_qu5.Text = "Что отражает значение DPS - профиль?";
            pan_faq.Controls.Add(lab_faq_q5);
            pan_faq.Controls.Add(lab_faq_qu5);
            pan_faq.Controls.Add(lab_faq_a5);
            lab_faq_an5.Location = new Point(60, pos + 16);
            lab_faq_an5.Text = "Это тот DPS, который вам показывает игра. Обычно совпадает, если данные введены правильно.";
            pan_faq.Controls.Add(lab_faq_an5);

            // ---------------------------------------------------- //

            pos += 45;

            lab_faq_q6.Text = "Вопрос: ";
            lab_faq_q6.AutoSize = true;
            lab_faq_q6.Location = new Point(0, pos);
            lab_faq_q6.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a6.Text = "Ответ: ";
            lab_faq_a6.AutoSize = true;
            lab_faq_a6.Location = new Point(0, pos + 15);
            lab_faq_a6.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu6.AutoSize = true;
            lab_faq_an6.AutoSize = true;
            lab_faq_qu6.Location = new Point(60, pos + 1);
            lab_faq_qu6.Text = "Почему профильный DPS так сильно отличается от реального?";
            pan_faq.Controls.Add(lab_faq_q6);
            pan_faq.Controls.Add(lab_faq_qu6);
            pan_faq.Controls.Add(lab_faq_a6);
            lab_faq_an6.Location = new Point(60, pos + 16);
            lab_faq_an6.Text = "Профильный DPS не учитывает многих факторов, таких как элементальный урон, прибавка урона скилла.";
            pan_faq.Controls.Add(lab_faq_an6);

            // ---------------------------------------------------- //

            pos += 45;

            lab_faq_q7.Text = "Вопрос: ";
            lab_faq_q7.AutoSize = true;
            lab_faq_q7.Location = new Point(0, pos);
            lab_faq_q7.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a7.Text = "Ответ: ";
            lab_faq_a7.AutoSize = true;
            lab_faq_a7.Location = new Point(0, pos + 15);
            lab_faq_a7.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu7.AutoSize = true;
            lab_faq_an7.AutoSize = true;
            lab_faq_qu7.Location = new Point(60, pos + 1);
            lab_faq_qu7.Text = "Почему при импорте данные могут не совпасть как с профилем в игре, так и с профилем на сайте.";
            pan_faq.Controls.Add(lab_faq_q7);
            pan_faq.Controls.Add(lab_faq_qu7);
            pan_faq.Controls.Add(lab_faq_a7);
            lab_faq_an7.Location = new Point(60, pos + 16);
            lab_faq_an7.Text = "На сайте содержится неверная информация о части оффхендов, неверно считается % урона\n оружия с элементальным уроном, не учитываются прибавки от сетовых вещей, нет информации о парагоне.";
            pan_faq.Controls.Add(lab_faq_an7);

            // ---------------------------------------------------- //

            pos += 60;

            lab_faq_q8.Text = "Вопрос: ";
            lab_faq_q8.AutoSize = true;
            lab_faq_q8.Location = new Point(0, pos);
            lab_faq_q8.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a8.Text = "Ответ: ";
            lab_faq_a8.AutoSize = true;
            lab_faq_a8.Location = new Point(0, pos + 15);
            lab_faq_a8.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu8.AutoSize = true;
            lab_faq_an8.AutoSize = true;
            lab_faq_qu8.Location = new Point(60, pos + 1);
            lab_faq_qu8.Text = "Почему при вводе \"% урона от скилла\" не меняется профильный урон в программе?";
            pan_faq.Controls.Add(lab_faq_q8);
            pan_faq.Controls.Add(lab_faq_qu8);
            pan_faq.Controls.Add(lab_faq_a8);
            lab_faq_an8.Location = new Point(60, pos + 16);
            lab_faq_an8.Text = "Большая часть скиллов в игре не учитывается при расчёте профильного урона, однако есть исключения.\nВ расчёте реального урона всё учитывается как надо.";
            pan_faq.Controls.Add(lab_faq_an8);

            // ---------------------------------------------------- //

            pos += 60;

            lab_faq_q9.Text = "Вопрос: ";
            lab_faq_q9.AutoSize = true;
            lab_faq_q9.Location = new Point(0, pos);
            lab_faq_q9.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a9.Text = "Ответ: ";
            lab_faq_a9.AutoSize = true;
            lab_faq_a9.Location = new Point(0, pos + 15);
            lab_faq_a9.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu9.AutoSize = true;
            lab_faq_an9.AutoSize = true;
            lab_faq_qu9.Location = new Point(60, pos + 1);
            lab_faq_qu9.Text = "Чем отличается \"Обычная точность\" и \"Высокая точность\"?";
            pan_faq.Controls.Add(lab_faq_q9);
            pan_faq.Controls.Add(lab_faq_qu9);
            pan_faq.Controls.Add(lab_faq_a9);
            lab_faq_an9.Location = new Point(60, pos + 16);
            lab_faq_an9.Text = "В обычном режиме нужно вводить меньше параметров и может быть небольшая погрешность в измерениях.";
            pan_faq.Controls.Add(lab_faq_an9);

            // ---------------------------------------------------- //

            pos += 45;

            lab_faq_q10.Text = "Вопрос: ";
            lab_faq_q10.AutoSize = true;
            lab_faq_q10.Location = new Point(0, pos);
            lab_faq_q10.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a10.Text = "Ответ: ";
            lab_faq_a10.AutoSize = true;
            lab_faq_a10.Location = new Point(0, pos + 15);
            lab_faq_a10.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu10.AutoSize = true;
            lab_faq_an10.AutoSize = true;
            lab_faq_qu10.Location = new Point(60, pos + 1);
            lab_faq_qu10.Text = "Что делает переключатель WD в режиме высокой точности?";
            pan_faq.Controls.Add(lab_faq_q10);
            pan_faq.Controls.Add(lab_faq_qu10);
            pan_faq.Controls.Add(lab_faq_a10);
            lab_faq_an10.Location = new Point(60, pos + 16);
            lab_faq_an10.Text = "В правой части есть кнопка для расчёта изменений оружия или других вещей, переключающихся циклически.\nПри нажатом переключателе WD появляется третий пункт для расчёта урона от питомцев WD.";
            pan_faq.Controls.Add(lab_faq_an10);

            // ---------------------------------------------------- //

            pos += 60;

            lab_faq_q11.Text = "Вопрос: ";
            lab_faq_q11.AutoSize = true;
            lab_faq_q11.Location = new Point(0, pos);
            lab_faq_q11.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a11.Text = "Ответ: ";
            lab_faq_a11.AutoSize = true;
            lab_faq_a11.Location = new Point(0, pos + 15);
            lab_faq_a11.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu11.AutoSize = true;
            lab_faq_an11.AutoSize = true;
            lab_faq_qu11.Location = new Point(60, pos + 1);
            lab_faq_qu11.Text = "Как посчитать урон спелла со временем отката?";
            pan_faq.Controls.Add(lab_faq_q11);
            pan_faq.Controls.Add(lab_faq_qu11);
            pan_faq.Controls.Add(lab_faq_a11);
            lab_faq_an11.Location = new Point(60, pos + 16);
            lab_faq_an11.Text = "В левой части есть кнопка, позволяющая выбрать режим обсчёта кулдауна.\nВводите время отката и снижение времени с вещей.";
            pan_faq.Controls.Add(lab_faq_an11);

            // ---------------------------------------------------- //

            pos += 60;

            lab_faq_q12.Text = "Вопрос: ";
            lab_faq_q12.AutoSize = true;
            lab_faq_q12.Location = new Point(0, pos);
            lab_faq_q12.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_a12.Text = "Ответ: ";
            lab_faq_a12.AutoSize = true;
            lab_faq_a12.Location = new Point(0, pos + 15);
            lab_faq_a12.Font = new Font("Arial", 8.00F, System.Drawing.FontStyle.Bold);

            lab_faq_qu12.AutoSize = true;
            lab_faq_an12.AutoSize = true;
            lab_faq_qu12.Location = new Point(60, pos + 1);
            lab_faq_qu12.Text = "Как посчитать урон скилла, который используется время от времени и не зависит от скорости атаки?";
            pan_faq.Controls.Add(lab_faq_q12);
            pan_faq.Controls.Add(lab_faq_qu12);
            pan_faq.Controls.Add(lab_faq_a12);
            lab_faq_an12.Location = new Point(60, pos + 16);
            lab_faq_an12.Text = "В разделе для ввода кулдауна поставить 1 в пункт Skill 1 cooldown.\n\n";
            pan_faq.Controls.Add(lab_faq_an12);

            // ---------------------------------------------------- //

            pan_faq.Focus();
        }

    }
}
