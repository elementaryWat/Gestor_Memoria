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
    public partial class DialogoSimple : Form
    {
        public int bufer;
        public bool error;
        public string mensaje;
        public delegate void Del();
        public Del manejador;
        public DialogoSimple()
        {
            InitializeComponent();
        }
        private void BotonEst_Click(object sender, EventArgs e)
        {
            error = false;
            try
            {
                bufer= Int32.Parse(Texto.Text);
            }
            catch(FormatException)
            {
                MessageBox.Show(mensaje);
                error = true;
            }
            //Si no hay error en el procesamiento del numero llama a la funcion encapsulada por el delegado
            if (!error)
            {
                manejador();
                Hide();
            }
            
        }
    }
}
