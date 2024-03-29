﻿using Homework4.Charts;
using Homework4.CustomForm;
using Homework4.DataObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework4
{
    public partial class Form1 : Form
    {

        public IEnumerable<DataPoint> sample;
        Scatterplot sp;
        ContingentyTable cTable;

        public bool histogram = false;

        public Form1()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            DoubleBuffered = true;
            this.UpdateStyles();

            sample = null;
            sp = null;
            cTable = null;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            sample = File.ReadAllLines(@"CSV\data.csv")
                   .Skip(1) // Skips the column names
                   .Select(x => x.Split(','))
                   .Select(x => new DataPoint(Int32.Parse(x[2]), Int32.Parse(x[1])));
            sp = new Scatterplot(sample);
            cTable = new ContingentyTable(sample);
        }

        //Scatterplot button
        private void button2_Click(object sender, EventArgs e)
        {
            myPictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.Controls.Add(this.myPictureBox1);
            this.myPictureBox1.Refresh();
        }

        //Draw Histograms inside scatterplot
        private void button3_Click(object sender, EventArgs e) { this.histogram = !this.histogram; this.myPictureBox1.Refresh(); }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) { sp.Draw(e.Graphics); if (histogram) sp.DrawHistogram(e.Graphics); }

        private void button4_Click(object sender, EventArgs e)
        {
            myPictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            this.Controls.Add(this.myPictureBox2);
            this.myPictureBox2.Refresh();
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e) { cTable.Draw(e.Graphics); }
    }
}
