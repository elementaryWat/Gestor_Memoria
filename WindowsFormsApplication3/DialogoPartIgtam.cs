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
    public partial class DialogoPartIgtam : Form
    {
        public int bufer;
        public bool error;
        public delegate void Del();
        int tamp;
        public Del manejador;
        Memoria memoriactual;
        public DialogoPartIgtam(Memoria memac)
        {
            memoriactual = memac;
            InitializeComponent();
            TextoCP.Text = memoriactual.cantpartig.ToString();
            Tampart.Text = "Tamaño de particion "+memoriactual.tam1part+memoriactual.textounidad;
            EtTamMemoria.Text = "Tamaño de memoria = "+memoriactual.tamañomemoria+memoriactual.textounidad;
            EtSumaPart.BackColor = Color.Lime;
            EtSumaPart.ForeColor = Color.Black;
            EtSumaPart.Text = "Suma de tamaños de particiones=" + memoriactual.cantpartig * memoriactual.tam1part ;
        }

        private void DialogoPlus_Load(object sender, EventArgs e)
        {

        }

        private void BotonEst_Click(object sender, EventArgs e)
        {
            //Si no hay error en el procesamiento del numero llama a la funcion encapsulada por el delegado
            if (!error)
            {
                memoriactual.tam1part = tamp;
                memoriactual.cantpartig = bufer;
                memoriactual.cantpart = bufer;
                memoriactual.cantpartfij = bufer;
                memoriactual.mapamemoria = new int[bufer];
                memoriactual.vaciarmemoria();
                Hide();
            }
            else
            {
                MessageBox.Show("Debe corregir los errores para poder continuar");
            }
        }

        private void Texto_TextChanged(object sender, EventArgs e)
        {
            error = false;
            try
            {
                bufer = Int32.Parse(TextoCP.Text);
                if (bufer>memoriactual.tamañomemoria)
                {
                    MessageBox.Show("La cantidad de particiones no puede ser mayor que el tamaño de memoria!");
                    error = true;
                }else
                {
                    tamp = memoriactual.tamañomemoria / bufer;
                    Tampart.Text = "Tamaño de particion " + tamp + memoriactual.textounidad;
                    if ((tamp * bufer) == memoriactual.tamañomemoria)
                    {

                        EtSumaPart.BackColor = Color.Lime;
                        EtSumaPart.ForeColor = Color.Black;
                        EtSumaPart.Text = "Suma de tamaños de particiones=" + (tamp * bufer);
                    }
                    else
                    {
                        EtSumaPart.BackColor = Color.Red;
                        EtSumaPart.ForeColor = Color.White;
                        EtSumaPart.Text = "Suma de tamaños de particiones=" + (tamp * bufer);
                        error = true;
                    }
                }
                
            }
            catch (FormatException)
            {
                MessageBox.Show("Debe ingresar un numero para la cantidad de particiones");
                error = true;
            }
        }
    }
}
