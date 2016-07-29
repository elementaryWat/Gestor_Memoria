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
    public partial class ConfPartFijas2 : Form
    {
        Memoria memoriaactual;
        public int cantpart;
        public int[] particionesor;
        public int[] particionesnu;
        int temporal;
        int tamp;
        bool error;
        public int sumatamor;
        public int sumatamnu;
        public delegate void Del(ConfPartFijas2 newconf);
        public Del manejadormem;
        public ConfPartFijas2(Memoria memac)
        {
            sumatamor = 0;  
            memoriaactual = memac;
            InitializeComponent();
            particionesor = memoriaactual.particionesmemfij;
            cantpart = memoriaactual.cantpartdif;
            Cantpart.Text = cantpart.ToString();
            Tamactual.Text = "Tamaño actual de memoria = " + memoriaactual.tamañomemoria + " KB";
            Tampartic.Rows.Clear();
            for (int x = 0; x < cantpart; x++)
            {
                string[] particion = { (x + 1).ToString(), particionesor[x].ToString() };
                Tampartic.Rows.Add(particion);
                sumatamor += particionesor[x];
            }
            SumaAct.Text = "Suma de particiones = " + sumatamor + " KB";
            if (sumatamor == memoriaactual.tamañomemoria)
            {
                SumaAct.BackColor = Color.Lime;
                SumaAct.ForeColor = Color.Black;
            }
            else
            {
                SumaAct.BackColor = Color.Red;
                SumaAct.ForeColor = Color.White;
            }
        }

        private void Cantpart_TextChanged(object sender, EventArgs e)
        {
            Tampartic.Rows.Clear();
            error = false;
            sumatamnu = 0;
            try
            {
                temporal = Int32.Parse(Cantpart.Text);
                if (temporal>0)
                {
                    cantpart = temporal;
                    particionesnu = new int[temporal];
                    tamp = memoriaactual.tamañomemoria / cantpart;
                    for (int i=0;i<temporal;i++)
                    {
                        sumatamnu += tamp;
                        particionesnu[i] = tamp;
                        string[] particion= {(i+1).ToString() , tamp.ToString() };
                        Tampartic.Rows.Add(particion);
                    }
                    if (temporal>memoriaactual.tamañomemoria)
                    {
                        MessageBox.Show("La cantidad de particiones no puede ser mayor que el tamaño de memoria!");
                        error = true;
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar una cantidad mayor que 0");
                    error = true;
                }
            }
            catch(FormatException)
            {
                MessageBox.Show("Debe ingresar un numero en la cantidad de particiones");
                error = true;
            }
            if (error)
            {
                cantpart = memoriaactual.cantpartdif;
                Cantpart.Text = cantpart.ToString();
                Tampartic.Rows.Clear();
                for (int x = 0; x < cantpart; x++)
                {
                    string[] particion = { (x + 1).ToString(), particionesor[x].ToString() };
                    Tampartic.Rows.Add(particion);
                }
                SumaAct.Text = "Suma de particiones = "+sumatamor + " KB";
                if (sumatamor==memoriaactual.tamañomemoria)
                {
                    SumaAct.BackColor = Color.Lime;
                    SumaAct.ForeColor = Color.Black;
                    error = false;
                }
                else
                {
                    SumaAct.BackColor = Color.Red;
                    SumaAct.ForeColor = Color.White;
                }
            }
            else
            {
                SumaAct.Text = "Suma de particiones = " + sumatamnu + " KB";
                if (sumatamnu == memoriaactual.tamañomemoria)
                {
                    SumaAct.BackColor = Color.Lime;
                    SumaAct.ForeColor = Color.Black;
                }
                else
                {
                    error = true;
                    SumaAct.BackColor = Color.Red;
                    SumaAct.ForeColor = Color.White;
                }
            }
        }

        private void Est_tamanios_Click(object sender, EventArgs e)
        {
            bool partig = false;
            for (int h = 0; h < cantpart; h++)
            {
                for (int i = (h + 1); i < cantpart; i++)
                {
                    if (particionesnu[i] == particionesnu[h])
                    {
                        MessageBox.Show("Existen dos o mas particiones del mismo tamaño!");
                        error = true;
                        partig = true;
                        break;
                    }
                }
                if (partig)
                {
                    break;
                }
            }
            if (!error)
            {
                manejadormem(this);
                Dispose();
            }
            else
            {
                MessageBox.Show("Debe corregir los errores para poder continuar");
            }
        }

        private void Tampartic_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            sumatamnu = 0;
            error = false;
            for (int x = 0; x < cantpart; x++)
            {
                try
                {
                    temporal = Int32.Parse(Tampartic.Rows[x].Cells[1].Value.ToString());
                    if (temporal > 0)
                    {
                        particionesnu[x] = temporal;
                        sumatamnu += temporal;
                    }
                    else
                    {
                        MessageBox.Show("Los tamaños deben ser mayores que 0!");
                        error = true;
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Debe ingresar valores numericos en los tamaños!");
                    error = true;
                }
            }
            SumaAct.Text = "Suma de particiones = " + sumatamnu + " KB";
            if (sumatamnu == memoriaactual.tamañomemoria)
            {
                SumaAct.BackColor = Color.Lime;
                SumaAct.ForeColor = Color.Black;
            }
            else
            {
                error = true;
                SumaAct.BackColor = Color.Red;
                SumaAct.ForeColor = Color.White;
            }
        }

        private void ConfPartFijas2_Load(object sender, EventArgs e)
        {

        }
    }
}
