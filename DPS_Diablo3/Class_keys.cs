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
        private void Readonly_clear(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).ReadOnly = false;
            ((TextBox)sender).BackColor = Color.White;
            if (((TextBox)sender).Text == "" || ((TextBox)sender).Text == "min2" || ((TextBox)sender).Text == "weapon2" || ((TextBox)sender).Text == "weapon1" || ((TextBox)sender).Text == "max2" || ((TextBox)sender).Text == "skill2" || ((TextBox)sender).Text == "100%" || ((TextBox)sender).Text.Contains("http"))
                ((TextBox)sender).Text = "";
            if (((TextBox)sender).Name == "tb_pers") ((TextBox)sender).Text = "";
        }

        private void Readonly_insert(object sender, string settings, decimal sett_dec, string fl)
        {
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
