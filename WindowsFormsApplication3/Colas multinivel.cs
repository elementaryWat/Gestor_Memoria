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
    public partial class Colas_multinivel : Form
    {
        int Tipocola;
        const int Conretro=1;
        const int Sinretro = 2;
        int filasel;
        int cantcolas;
        int[] algplan;
        int[] quancolas;
        string[] nombrescolas;
        bool[] esrr;
        string[] nombpol;
        const int FCFS = 1;
        const int SJF = 2;
        const int SRTF = 3;
        const int RR = 4;
        ConfColas configC;
        Computador ordenador;
        public Colas_multinivel(ConfColas miconf, Computador miord)
        {
            ordenador = miord;
            nombpol = new string[4];
            nombpol[0] = "FCFS";
            nombpol[1] = "SJF";
            nombpol[2] = "SRTF";
            nombpol[3] = "Round Robin";
            InitializeComponent();
            configC = miconf;
            cantcolas = configC.cantcolas;
        }
        private void definirceldas()
        {
            Datoscolas.Rows.Clear();
            if (Tipocola == Conretro)
            {
                DataGridViewComboBoxColumn cel = Datoscolas.Columns[2] as DataGridViewComboBoxColumn;
                cel.Items.Clear();
                cel.Items.Add("Round Robin");
                for (int x = 0; x < cantcolas; x++)
                {
                    Datoscolas.Rows.Add(x.ToString(), nombrescolas[x]);
                }
                //En caso de que sea 
                DataGridViewComboBoxCell ultcel = Datoscolas.Rows[cantcolas - 1].Cells[2] as DataGridViewComboBoxCell;
                ultcel.Items.Clear();
                ultcel.Items.Add("FCFS");
                ultcel.Items.Add("SJF");
                ultcel.Items.Add("SRTF");
                ultcel.Items.Add("Round Robin");
                for (int x = 0; x < (cantcolas - 1); x++)
                {
                    Datoscolas.Rows[x].Cells[2].Value = nombpol[3];
                    esrr[x] = true;
                    Datoscolas.Rows[x].Cells[3].ReadOnly = false;
                    Datoscolas.Rows[x].Cells[3].Value = 1;
                }
                Datoscolas.Rows[cantcolas - 1].Cells[2].Value = nombpol[0];
            }
            else if (Tipocola == Sinretro)
            {
                DataGridViewComboBoxColumn cel = Datoscolas.Columns[2] as DataGridViewComboBoxColumn;
                cel.Items.Clear();
                cel.Items.Add("FCFS");
                cel.Items.Add("SJF");
                cel.Items.Add("SRTF");
                cel.Items.Add("Round Robin");
                for (int x = 0; x < cantcolas; x++)
                {
                    Datoscolas.Rows.Add(x.ToString(), nombrescolas[x]);
                    //MessageBox.Show("El indice seleccionado es "+ algplan[x]);
                    Datoscolas.Rows[x].Cells[2].Value = nombpol[algplan[x] - 1];
                    if (algplan[x] == RR)
                    {
                        esrr[x] = true;
                        Datoscolas.Rows[x].Cells[3].ReadOnly = false;
                        Datoscolas.Rows[x].Cells[3].Value = 1;
                        if (!Datoscolas.Columns[3].Visible)
                        {
                            Datoscolas.Columns[3].Visible = true;
                        }
                    }
                }
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tipocola = TipoC.SelectedIndex + 1;
            definirceldas();
        }

        private void Est_Conf_Click(object sender, EventArgs e)
        {
            bool error=false;
            try {
                int bufern=0;
                string bufert;
                for (int i = 0; i < cantcolas; i++)
                {
                    if (esrr[i])
                    {
                        bufern=Int32.Parse(Datoscolas.Rows[i].Cells[3].Value.ToString());
                        if (bufern <= 0)
                        {
                            MessageBox.Show("Debe ingresar valores mayores que 0 en los tiempos de quantum");
                            error = true;
                        }
                    }
                    bufert = Datoscolas.Rows[i].Cells[1].Value.ToString();
                    if (bufert == "")
                    {
                        MessageBox.Show("Debe completar los nombres de las colas para poder identificarlas");
                        error = true;
                    }
                    
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Debe ingresar valores numericos en los quantum");
                error = true;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Debe completar los valores de los quantum en las colas con politica RR");
                error = true;
            }
            if (!error)
            {
                configC.cantcolas = cantcolas;
                configC.politicasColas = (int[])algplan.Clone();
                configC.quantumcolas = (int[])quancolas.Clone();
                configC.nombrescolas = (string[])nombrescolas.Clone();
                if (TipoC.SelectedIndex==0)
                {
                    configC.CRealimentada = true;
                }
                else
                {
                    configC.CRealimentada = false;
                }
                if (AColas.SelectedIndex == 0)
                {
                    configC.CApropiativa = true;
                }
                else
                {
                    configC.CApropiativa = false;
                }
            }
        }
        private void Datoscolas_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (Datoscolas.CurrentCell.ColumnIndex==2 && e.Control is ComboBox)
            {
                ComboBox myalgor = e.Control as ComboBox;
                myalgor.SelectedIndexChanged += DetermAlg;
            }
        }
        private void DetermAlg(object sender, EventArgs e)
        {
            ComboBox mycom = (ComboBox)sender;
            algplan[filasel] = mycom.SelectedIndex + 1;
            if (algplan[filasel] == RR || (Tipocola == Conretro && filasel<(cantcolas-1)))
            {
                esrr[filasel] = true;
                Datoscolas.Rows[filasel].Cells[3].ReadOnly = false;
                Datoscolas.Rows[filasel].Cells[3].Value = 1;
                if (!Datoscolas.Columns[3].Visible)
                {
                    Datoscolas.Columns[3].Visible = true;
                }
            }
            else
            {
                esrr[filasel] = false;
                Datoscolas.Rows[filasel].Cells[3].ReadOnly = true;
                Datoscolas.Rows[filasel].Cells[3].Value = "";
                bool enc = false;
                for (int i=0;i<cantcolas;i++)
                {
                    if (esrr[i])
                    {
                        enc = true;
                        break;
                    }
                }
                if (!enc)
                {
                    if (Datoscolas.Columns[3].Visible)
                    {
                        Datoscolas.Columns[3].Visible = false;

                    }
                }
                quancolas[filasel] = -1;
            }
        }

        private void Datoscolas_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            filasel = e.RowIndex;
        }

        private void CantidadC_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combocanc = sender as ComboBox;
            cantcolas = combocanc.SelectedIndex + 1;
            algplan=new int[cantcolas];
            quancolas= new int[cantcolas];
            esrr= new bool[cantcolas];
            nombrescolas = new string[cantcolas];
            for (int i=0;i<cantcolas;i++)
            {
                algplan[i] = 1;
                quancolas[i] = 0;
                esrr[i] = false;
                nombrescolas[i] = "Cola "+i;
            }
            definirceldas();
        }

        private void Colas_multinivel_Load(object sender, EventArgs e)
        {
            CantidadC.SelectedIndex = cantcolas - 1;
            algplan = configC.politicasColas;
            quancolas = configC.quantumcolas;
            nombrescolas = (string[])configC.nombrescolas.Clone();
            if (configC.CRealimentada)
            {
                TipoC.SelectedIndex = 0;
            }
            else
            {
                TipoC.SelectedIndex = 1;
            }
            if (configC.CApropiativa)
            {
                AColas.SelectedIndex = 0;
            }
            else
            {
                AColas.SelectedIndex = 1;
            }
        }
    }
}
