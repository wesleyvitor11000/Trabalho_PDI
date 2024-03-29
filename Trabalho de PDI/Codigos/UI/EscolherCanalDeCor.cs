﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trabalho_de_PDI
{
    public partial class EscolherCanalDeCor : Form
    {
        private Bitmap originalImageBitmap;
        private HSV[,] intensityEqHSVMatrix;
        private bool exibirHistograma;
        private SortedDictionary<double, int> intensityHistogram;
        private SortedDictionary<double, double> intensityEq;
        private bool equalize;
        private int[] choice;

        public EscolherCanalDeCor(String title, Bitmap image, SortedDictionary<double, int> intensityHistogram, bool exibirHistograma = true)
        {
            equalize = false;
            this.exibirHistograma = exibirHistograma;
            originalImageBitmap = image;
            this.intensityHistogram = intensityHistogram;

            InitializeComponent();
            
            textLabel.Text = title;

        }

        public EscolherCanalDeCor(String title, ref int[] choice)
        {
            equalize = true;
            this.choice = choice;
            InitializeComponent();
        }

        private void EscolherCanalDeCor_Load(object sender, EventArgs e)
        {
            String[] channels = { "Escala de cinza", "Vermelho", "Verde", "Azul", "Todos" };

            foreach (String s in channels)
            {
                comboBox1.Items.Add(s);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void escolherButton_Click(object sender, EventArgs e)
        {
            if (equalize)
            {
                choice[0] = comboBox1.SelectedIndex;
            }
            else
            {
                showHists();
            }

            Close();
            
        }

        private void showHists()
        {
            SortedDictionary<double, int> histogram;
            int choose = comboBox1.SelectedIndex;

            if (choose == 0)
            {
                new exibirHistograma(intensityHistogram, 0).Show();
            }
            else if (choose < 4)
            {
                histogram = HistogramProcessing.getHistogramFromImage(originalImageBitmap, choose);
                new exibirHistograma(histogram, choose).Show();
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    histogram = HistogramProcessing.getHistogramFromImage(originalImageBitmap, i);
                    new exibirHistograma(histogram, i + 1).Show();
                }
            }
        }
    }
}
