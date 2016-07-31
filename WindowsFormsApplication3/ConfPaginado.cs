using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class ConfPaginado : Form
    {
        public int bufer;
        public bool error;
        public delegate void Del();
        double cantpag;
        public Del manejador;
        Memoria memoriactual;
        public ConfPaginado(Memoria memac)
        {
            InitializeComponent();
            memoriactual = memac;
            int pot = 1;
            for (int i = 1; i <= 10; i++)
            {
                pot *= 2;
                Tampag.Items.Add(pot.ToString());
            }
            Tampag.SelectedIndex =(int)Math.Log((double)memac.tampag, (double)2)-1;
            Cantpag.Text = "Cantidad de paginas " + memoriactual.cantpag;
            EtSumaPag.Text = "Suma de tamaños de paginas " + memoriactual.tampag*memoriactual.cantpag;
            EtTamMemoria.Text = "Tamaño de memoria = " + memoriactual.tamañomemoria;
            Cantpag.BackColor = Color.Lime;
            Cantpag.ForeColor = Color.Black;
            EtSumaPag.BackColor = Color.Lime;
            EtSumaPag.ForeColor = Color.Black;
        }

        private void BotonEst_Click(object sender, EventArgs e)
        {
            if (!error)
            {
                memoriactual.tampag = bufer;
                memoriactual.cantpag = (int)cantpag;
                memoriactual.cantpart = (int)cantpag;
                memoriactual.mapamemoria = new int[(int)cantpag];
                memoriactual.vaciarmemoria();
                Hide();
            }
            else
            {
                MessageBox.Show("Debe corregir los errores para poder continuar");
            }
        }

        private void Tampag_SelectedIndexChanged(object sender, EventArgs e)
        {
            error = false;
            bufer = Int32.Parse(Tampag.SelectedItem.ToString());
            if (bufer > memoriactual.tamañomemoria)
            {
                MessageBox.Show("El tamaño de pagina no puede ser mayor que el tamaño de memoria");
                error = true;
            }
            else
            {
                cantpag = memoriactual.tamañomemoria / bufer;
                double sumaact = cantpag * bufer;
                Cantpag.Text = "Cantidad de paginas " + cantpag;
                EtSumaPag.Text = "Suma de paginas " + sumaact + " palabras";
                if (sumaact == memoriactual.tamañomemoria)
                {
                    Cantpag.BackColor = Color.Lime;
                    Cantpag.ForeColor = Color.Black;
                    EtSumaPag.BackColor = Color.Lime;
                    EtSumaPag.ForeColor = Color.Black;
                }
                else
                {
                    Cantpag.BackColor = Color.Red;
                    Cantpag.ForeColor = Color.White;
                    EtSumaPag.BackColor = Color.Red;
                    EtSumaPag.ForeColor = Color.White;
                    error = true;
                }
            }
        }
    }
  }
