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
using System.Security;
using System.Threading;

namespace DPS_Diablo3
{
    public partial class dps_diablo : Form
    {
        private void Readonly_clear(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).ReadOnly = false;
            ((TextBox)sender).BackColor = Color.White;
            if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == "min2" || ((TextBox)sender).Text == "weapon2" || ((TextBox)sender).Text == "weapon1" || ((TextBox)sender).Text == "max2" || ((TextBox)sender).Text == "skill2" || ((TextBox)sender).Text == "100%" || ((TextBox)sender).Text.Contains("http"))
                ((TextBox)sender).Text = "";
            if (((TextBox)sender).Name == "tb_pers")
            {
                ((TextBox)sender).Text = "";

                //IDataObject idat = null;
                //Exception threadEx = null;
                //Thread staThread = new Thread(
                //    delegate()
                //    {
                //        try { idat = Clipboard.GetDataObject(); }
                //        catch (Exception ex) { threadEx = ex;}
                //    });
                //staThread.SetApartmentState(ApartmentState.STA);
                //staThread.Start();
                //staThread.Join();

                string inp = "Import string";
                if (Clipboard.ContainsText()) inp = (String)Clipboard.GetDataObject().GetData(DataFormats.Text);
                Uri nUrl = null;
                //if (Uri.TryCreate(inp, UriKind.Absolute, out nUrl))
                //{
                //    inp = nUrl.ToString();
                //}
                //Uri sch = new Uri(inp);
                //if (!(Uri.IsWellFormedUriString(inp, UriKind.Absolute) && (sch.Scheme == "http" || sch.Scheme == "https"))) MessageBox.Show("!@#");

                //try {
                    //if (Clipboard.ContainsText() && Clipboard.GetDataObject().GetDataPresent(DataFormats.Text)) inp = (String)Clipboard.GetDataObject().GetData(DataFormats.Text); 
                //}
                //catch { }
                //MessageBox.Show(inp+"длина строки: "); MessageBox.Show(inp.Length.ToString());

                //if (inp.IndexOf("battle.net") > 0 && inp.IndexOf("d3") > 0 && inp.IndexOf("profile") > 0 && inp.IndexOf("hero") > 0)
                if ((inp != null && inp.Length > 20 && inp.Contains("battle.net") && inp.Contains("d3") && inp.Contains("profile") && inp.Contains("hero"))
                    && (Uri.TryCreate(inp, UriKind.RelativeOrAbsolute, out nUrl)))
                {
                    tb_pers.Paste();
                    b_web_Click(null, null);
                }
                else
                {
                    if (inp != null && inp.Length>0) MessageBox.Show(lng.mess_imp, lng.warn, MessageBoxButtons.OK, MessageBoxIcon.Question);
                    if (Settings.Default.web1_www != "" && cb_web.Items.Count>0) cb_web.DroppedDown = true;
                    ((TextBox)sender).Focus();
                }
                //Clipboard.Clear();

            }
        }

        private void Readonly_insert(object sender, string settings, decimal sett_dec, string fl)
        {
            if (fl == "Label")
            {
                //if (((Label)sender).Text == "") 
                    ((Label)sender).Text = settings;
            }
            if (fl == "Normal")
            {
                if (((TextBox)sender).Text == "") ((TextBox)sender).Text = settings;
            }
            if (fl == "NumericUpDown")
            {
                if (((NumericUpDown)sender).Value == 0) ((NumericUpDown)sender).Value = sett_dec;
            }
            if (fl == "ReadOnly")
            {
                if (
                    (((TextBox)sender).Text == "" || ((TextBox)sender).Text == "min2" || ((TextBox)sender).Text == "weapon2" || ((TextBox)sender).Text == "weapon1" || ((TextBox)sender).Text == "max2" || ((TextBox)sender).Text == "skill2" || ((TextBox)sender).Text == "100%")
                    && (settings != "" || settings != "min2" || settings != "weapon2" || settings != "weapon1" || settings != "max2" || settings != "skill2" || settings != "100%")
                    && (settings != ((TextBox)sender).Text)
                    )
                {
                    ((TextBox)sender).Text = settings;
                    ((TextBox)sender).ReadOnly = false;
                    ((TextBox)sender).BackColor = Color.White;
                }
            }
        }

        private void Input_dot(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && e.KeyChar != 46 && (e.KeyChar < 48 || e.KeyChar > 57)) e.Handled = true;
        }

        private void Input_norm(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && (e.KeyChar < 48 || e.KeyChar > 57)) e.Handled = true;
        }
    }
}
