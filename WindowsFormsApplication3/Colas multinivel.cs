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
        int cantidadColas;
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
        public Colas_multinivel(ConfColas miconf)
        {
            nombpol = new string[4];
            nombpol[0] = "FCFS";
            nombpol[1] = "SJF";
            nombpol[2] = "SRTF";
            nombpol[3] = "Round Robin";
            InitializeComponent();
            configC = miconf;
            cantcolas = configC.cantcolas;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Datoscolas.Rows.Clear();
            if (CantidadC.SelectedIndex!=-1)
            {
                cantidadColas = CantidadC.SelectedIndex + 1;
                Tipocola = TipoC.SelectedIndex + 1;
                if (Tipocola == Conretro)
                {
                    DataGridViewComboBoxColumn cel = Datoscolas.Columns[2] as DataGridViewComboBoxColumn;
                    cel.Items.Clear();
                    cel.Items.Add("Round Robin");
                    for (int x = 0; x < cantidadColas; x++)
                    {
                        Datoscolas.Rows.Add(x.ToString(), nombrescolas[x]);
                    }
                    //En caso de que sea 
                    DataGridViewComboBoxCell ultcel = Datoscolas.Rows[cantidadColas - 1].Cells[2] as DataGridViewComboBoxCell;
                    ultcel.Items.Clear();
                    ultcel.Items.Add("FCFS");
                    ultcel.Items.Add("SJF");
                    ultcel.Items.Add("SRTF");
                    ultcel.Items.Add("Round Robin");
                } else if (Tipocola==Sinretro)
                {
                    for (int x = 0; x < cantidadColas; x++)
                    {
                        Datoscolas.Rows.Add(x.ToString(), nombrescolas[x]);
                        //MessageBox.Show("El indice seleccionado es "+ algplan[x]);
                        Datoscolas.Rows[x].Cells[2].Value = nombpol[algplan[x] - 1];
                    }
                }
            }
        }

        private void Est_Conf_Click(object sender, EventArgs e)
        {

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
            if (algplan[filasel] == RR)
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
                algplan[i] = -1;
                quancolas[i] = -1;
                esrr[i] = false;
                nombrescolas[i] = "Cola "+i;
            }
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
