using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace pSHA_Keccak_Form_
{
    public partial class About : Form
    {
        ProcessStartInfo p = new ProcessStartInfo();
        
        public About()
        {
            InitializeComponent();
        }

        private void linkLabe1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            p.FileName = "https://wiki2.org/en/SHA-3";
            p.UseShellExecute = true;
            Process.Start(p);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            p.FileName = "https://www.nist.gov/publications/sha-3-standard-permutation-based-hash-and-extendable-output-functions?pub_id=919061";
            p.UseShellExecute = true;
            Process.Start(p);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            p.FileName = "http://masters.donntu.org/ivanitsa/index.html";
            p.UseShellExecute = true;
            Process.Start(p);
        }

       

    }
}
