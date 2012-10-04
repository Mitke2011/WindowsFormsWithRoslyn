using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Testing.Core;
using RoslynClassGenerator;

namespace WindowsFormsWithRoslyn
{
    public partial class Form1 : Form
    {
        //private FormTemplate ft;
        private RoslynGen genMacro;
        public Form1()
        {
            InitializeComponent();
            genMacro = new RoslynGen();
            //ft = new FormTemplate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ft.Show();
            Form fgen = genMacro.GetGenerator();
            fgen.Show();
            //this.textBox1.Text = formCodeFrontEnd.ToString();
        }
       
    }
}
