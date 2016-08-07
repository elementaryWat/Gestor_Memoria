using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3  
{

    public partial class Form1 : Form
    {
        DialogoSimple dialogoS;
        DialogoPartIgtam DialogoP;
        ConfPartFijas2 dialogoConfPF;
        Colas_multinivel DialogoColas;
        ConfColas MiconfColas;
        Memoria Gestormemoria;
        List<int[]> usosmem;
        List<int[]> particiones;
        List<int> cantpartins;
        List<int> fragmentaciones;
        public Form1()
        {
            InitializeComponent();
            dialogoS = new DialogoSimple();
            MiconfColas = new ConfColas();
            Gestormemoria = new Memoria(128);
            usosmem = new List<int[]>();
            particiones = new List<int[]>();
            cantpartins = new List<int>();
            fragmentaciones = new List<int>();
            tiempoquantum = 1;
            tiempoquantumES = 1;
        }
        bool[] exProceso;
        bool cerosdet;
        int[] pend1CPU;
        int[] pendentrada;
        int[] pend2CPU;
        int[] pendsalida;
        int[] pend3CPU;
        int politica;
        int politicaES;

        const int FCFS = 1;
        const int SJF = 2;
        const int SRTF = 3;
        const int RR = 4;
        const int CM = 5;
        public int tiempoquantum;
        public int tiempoquantumES;
        public int[] tamaniosproc;
        public int[] naturalezasproc;
        public bool haydemasiadogrande;
        public bool naturnodef;
        private void inicializar(int cantidad)
        {
            naturnodef = false;
            haydemasiadogrande = false;
            //Lista de arreglos con las rafagas de los procesos
            List<int[]> configuraciones = new List<int[]>();
            tamaniosproc = new int[cantidad];
            naturalezasproc = new int[cantidad];
            exProceso = new bool[cantidad];
            pend1CPU = new int[cantidad];
            pendentrada = new int[cantidad];
            pend2CPU = new int[cantidad];
            pendsalida = new int[cantidad];
            pend3CPU = new int[cantidad];
            cerosdet = false;
            for (int x = 0; x < cantidad; x++)
            {
                exProceso[x] = false;
                tamaniosproc[x] = (Int32.Parse(DatosFlow.Rows[x].Cells[3].Value.ToString()));
                if (!Gestormemoria.tamanioadecuado(tamaniosproc[x]))
                {
                    haydemasiadogrande = true;
                }
                pend1CPU[x] = (Int32.Parse(DatosFlow.Rows[x].Cells[4].Value.ToString()));
                pendentrada[x] = (Int32.Parse(DatosFlow.Rows[x].Cells[5].Value.ToString()));
                pend2CPU[x] = (Int32.Parse(DatosFlow.Rows[x].Cells[6].Value.ToString()));
                pendsalida[x] = (Int32.Parse(DatosFlow.Rows[x].Cells[7].Value.ToString()));
                pend3CPU[x] = (Int32.Parse(DatosFlow.Rows[x].Cells[8].Value.ToString()));
                if (pend1CPU[x]==0)
                {
                    cerosdet = true;
                }
            }
            if (cerosdet)
            {
                MessageBox.Show("La primer rafaga de CPU debe ser distinta de 0");
            }
            Gestormemoria.definirtamproc(tamaniosproc);
            configuraciones.Add(pend1CPU);
            configuraciones.Add(pendentrada);
            configuraciones.Add(pend2CPU);
            configuraciones.Add(pendsalida);
            configuraciones.Add(pend3CPU);
            ordenador = new Computador(cantidad,configuraciones.ToArray(),MiconfColas);
            Gestormemoria.definirordenador(ordenador);
            ordenador.definirmemoria(Gestormemoria);
            if (politica == CM && !MiconfColas.CRealimentada)
            {
                for (int i = 0; i < cantidad; i++)
                {
                    string texto = DatosFlow.Rows[i].Cells[1].Value.ToString();
                    if (texto != "")
                    {
                        int indice = Array.IndexOf(MiconfColas.nombrescolas, DatosFlow.Rows[i].Cells[1].Value.ToString());
                        ordenador.naturalezasprocesos[i] = indice;
                    }
                    else
                    {
                        naturnodef = true;
                        break;
                    }
                }
            }
            
            
        }
        private bool haynoarribados()
        {
            int cantidad = (DatosFlow.RowCount) - 1;
            for (int x=0;x<cantidad;x++)
            {
                if (!exProceso[x])
                {
                    return true;
                }
            }
            return false;
        }
        private void DatosEjer7_Inicio(object sender, EventArgs e)
        {
            DatosFlow.Rows.Clear();
            //{Id_Proceso,Tiempo_de_arribo,1er_R_CPU,Entrada,2da_R_CPU,Salida,3er_R_CPU}
            string[] row1 = { "1","", "0", "6", "1", "3", "4", "3", "1" };
            DatosFlow.Rows.Add(row1);
            string[] row2 = { "2", "", "3", "5", "1", "2", "3", "2", "1" };
            DatosFlow.Rows.Add(row2);
            string[] row3 = { "3", "", "5", "4", "1", "3", "2", "1", "1" };
            DatosFlow.Rows.Add(row3);
            string[] row4 = { "4", "", "8", "6", "1", "2", "4", "3", "1" };
            DatosFlow.Rows.Add(row4);
            string[] row5 = { "5", "", "10", "7", "1", "4", "5", "3", "1" };
            DatosFlow.Rows.Add(row5);
            string[] row6 = { "6", "", "11", "4", "1", "2", "2", "2", "1" };
            DatosFlow.Rows.Add(row6);
        }
        private void ejercicio8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatosFlow.Rows.Clear();
            //{Id_Proceso,Tiempo_de_arribo,1er_R_CPU,Entrada,2da_R_CPU,Salida,3er_R_CPU}
            string[] row1 = { "1", "", "1", "20", "2", "1", "1", "0", "0" };
            DatosFlow.Rows.Add(row1);
            string[] row2 = { "2", "", "2", "14", "1", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row2);
            string[] row3 = { "3", "", "3", "18", "1", "2", "1", "0", "0" };
            DatosFlow.Rows.Add(row3);
            string[] row4 = { "4", "", "5", "8", "2", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row4);
            string[] row5 = { "5", "", "7", "14", "1", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row5);
        }
        private void ejer9Guia1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatosFlow.Rows.Clear();
            //{Id_Proceso,Tiempo_de_arribo,1er_R_CPU,Entrada,2da_R_CPU,Salida,3er_R_CPU}
            string[] row1 = { "1", "", "0", "10", "10", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row1);
            string[] row2 = { "2", "", "0", "10", "4", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row2);
            string[] row3 = { "3", "", "0", "10", "2", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row3);
            string[] row4 = { "4", "", "0", "10", "1", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row4);
            string[] row5 = { "5", "", "1", "10", "6", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row5);
            string[] row6 = { "6", "", "3", "10", "2", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row6);
            string[] row7 = { "7", "", "4", "10", "2", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row7);
        }
        private void ejer5Guia4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatosFlow.Rows.Clear();
            //{Id_Proceso,Tiempo_de_arribo,1er_R_CPU,Entrada,2da_R_CPU,Salida,3er_R_CPU}
            string[] row1 = { "1", "", "0", "15", "5", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row1);
            string[] row2 = { "2", "", "0", "20", "4", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row2);
            string[] row3 = { "3", "", "0", "12", "10", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row3);
            string[] row4 = { "4", "", "1", "5", "3", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row4);
            string[] row5 = { "5", "", "2", "3", "2", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row5);
            string[] row6 = { "6", "", "3", "70", "10", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row6);
            string[] row7 = { "7", "", "4", "25", "5", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row7);
            string[] row8 = { "8", "", "5", "10", "5", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row8);
        }
        private void ejerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatosFlow.Rows.Clear();
            //{Id_Proceso,Tiempo_de_arribo,1er_R_CPU,Entrada,2da_R_CPU,Salida,3er_R_CPU}
            string[] row1 = { "T1", "", "0", "15", "8", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row1);
            string[] row2 = { "T2", "", "1", "20", "2", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row2);
            string[] row3 = { "T3", "", "4", "10", "3", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row3);
            string[] row4 = { "T4", "", "3", "30", "10", "0", "0", "0", "0" };
            DatosFlow.Rows.Add(row4);
        }
        //Determina si hay un proceso
        int cantidad;
        public int[] verificaraarribar(int instante)
        {
            //Agrega todos los procesos que arriban en un instante de tiempo
            bool ninguno = true;
            int cantidada = 0;
            List<int> respuesta=new List<int>();
            cantidad = (DatosFlow.RowCount) - 1;
            for (int x = 0; x < cantidad; x++)
            {
                int arribo = (Int32.Parse(DatosFlow.Rows[x].Cells[2].Value.ToString()));
                if (!exProceso[x])
                {
                    if (arribo == instante)
                    {
                        exProceso[x] = true;
                        respuesta.Add(x);
                        ninguno = false;
                        cantidada++;
                    }
                    
                }
            }
            if (ninguno)
            {
                respuesta.Add(-1);
                return  respuesta.ToArray();
            }
            else
            {
                //MessageBox.Show("En el instante "+instante+" arriban "+cantidada+" procesos");
                return respuesta.ToArray();
            }
            
        }
        Computador ordenador;
        private bool notermi()
        {
            if (!haynoarribados())
            {
                if (ordenador.noterminado())
                {
                    return true;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        int reloj;
        int instanteseleccionado;
        private void estinst(object sender, DataGridViewCellEventArgs e)
        {
           instanteseleccionado = e.RowIndex;
        }
        public void limpiarseleccombo()
        {
            int cantidadc = (DatosFlow.RowCount) - 1;
            for (int i=0;i<cantidadc;i++)
            {
                DatosFlow.Rows[i].Cells[1].Value = "";
            }
        }
        Usomemoria miusomemoria;
        private void verMapaDeMemoriaEnEsteInstanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (miusomemoria != null)
            {
                miusomemoria.Dispose();
            }
            miusomemoria = new Usomemoria();
            miusomemoria.Show();
            int[] usomemactual = usosmem[instanteseleccionado];
            int[] particionesactual = particiones[instanteseleccionado];
            int cantpartactual = cantpartins[instanteseleccionado];
            int fragmentactual = fragmentaciones[instanteseleccionado];
            int tamanio = cantpartactual;
            int alturatotal = 40 * tamanio;
            int mentamanio;
            mentamanio = 0;
            int cantidadc = (DatosFlow.RowCount) - 1;
            int[] tamaniosproc = new int[cantidadc];
            string textoa = "";
            if (Gestormemoria.organizacionmem == Memoria.Tiposorgmem.PAGINADO || Gestormemoria.organizacionmem == Memoria.Tiposorgmem.PARTFIJO)
            {
                textoa = "Fragmentacion interna = " + fragmentactual;
            }
            else
            {
                textoa = "Fragmentacion externa = " + fragmentactual;
            }
            miusomemoria.FragT.Text = textoa;
            for (int i=0;i<cantidad;i++)
            {
                tamaniosproc[i] = Int32.Parse(DatosFlow.Rows[i].Cells[3].Value.ToString());
            }
            if ((Gestormemoria.Tampart == Memoria.Opcionestam.DIFTAM && Gestormemoria.organizacionmem == Memoria.Tiposorgmem.PARTFIJO) || Gestormemoria.organizacionmem == Memoria.Tiposorgmem.PARTDIN)
            {
                mentamanio = particionesactual[0];
                for (int i = 0; i < tamanio; i++)
                {
                    int tamaniopart = particionesactual[i];
                    if (tamaniopart < mentamanio)
                    {
                        mentamanio = tamaniopart;
                    }
                }
            }
            
            for (int i=0;i<tamanio;i++)
            {
                int tamparticion=0;
                if ((Gestormemoria.Tampart == Memoria.Opcionestam.DIFTAM && Gestormemoria.organizacionmem == Memoria.Tiposorgmem.PARTFIJO) || Gestormemoria.organizacionmem == Memoria.Tiposorgmem.PARTDIN)
                {
                    tamparticion = particionesactual[i];
                }
                else if (Gestormemoria.Tampart == Memoria.Opcionestam.MISMTAM && Gestormemoria.organizacionmem == Memoria.Tiposorgmem.PARTFIJO)
                {
                    tamparticion = Gestormemoria.tam1part;
                }
                else if (Gestormemoria.organizacionmem == Memoria.Tiposorgmem.PAGINADO)
                {
                    tamparticion = Gestormemoria.tampag;
                }
                if (usomemactual[i] != -1)
                {
                    int idproceso = Int32.Parse(DatosFlow.Rows[usomemactual[i]].Cells[0].Value.ToString());
                    int tamanioac = tamaniosproc[usomemactual[i]];
                    if (tamanioac < tamparticion)
                    {
                        string[] filaac = { idproceso.ToString(), "(" + tamanioac + "/" + tamparticion + ")", (tamparticion - tamanioac) + "/" + tamparticion };
                        miusomemoria.Bloquesmem.Rows.Add(filaac);
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.BackColor = Color.BlueViolet;
                        miusomemoria.Bloquesmem.Rows[miusomemoria.Bloquesmem.Rows.Count - 1].Cells[2].Style = style;
                    }
                    else
                    {
                        tamaniosproc[usomemactual[i]] -= tamparticion;
                        string[] filaac = { idproceso.ToString(), "(" + tamparticion + "/" + tamparticion + ")", 0 + "/" + tamparticion };
                        miusomemoria.Bloquesmem.Rows.Add(filaac);
                    }

                }
                else
                {
                    string[] filaac = { "-","0", tamparticion.ToString() };
                    miusomemoria.Bloquesmem.Rows.Add(filaac);
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.BackColor = Color.BlueViolet;
                    for (int h = 0; h < 3; h++)
                    {
                        miusomemoria.Bloquesmem.Rows[miusomemoria.Bloquesmem.Rows.Count - 1].Cells[h].Style = style;
                    }
                }
                if ((Gestormemoria.Tampart == Memoria.Opcionestam.DIFTAM && Gestormemoria.organizacionmem == Memoria.Tiposorgmem.PARTFIJO) || Gestormemoria.organizacionmem == Memoria.Tiposorgmem.PARTDIN)
                {
                    int porcentaje = (particionesactual[i] * 100) / Gestormemoria.tamañomemoria;
                    //miusoprueba.Bloquesmem.Rows[miusoprueba.Bloquesmem.Rows.Count - 1].Height = (30 * porcentaje) / mentamanio;
                }
                
            }
        }
        private void imprimirestadisticas()
        {
            Promediosrafagas.Rows.Clear();
            estadisticasEjec.Rows.Clear();
            Estadisticaspromedio.Rows.Clear();
            estadisticasEjecE.Rows.Clear();
            EstadisticaspromedioE.Rows.Clear();
            estadisticasEjecS.Rows.Clear();
            EstadisticaspromedioS.Rows.Clear();
            int[] tiemposretorno=new int[cantidad];
            int[] tiemposirrupcion = new int[cantidad];
            int[] tiemposarribo = new int[cantidad];
            int[] tiemposespera = new int[cantidad];
            int[] tiemposfinalizacion = new int[cantidad];
            int[] tiemposprimerrespuesta = new int[cantidad];
            int[] tiemposrespuesta = new int[cantidad];
            int sumaretorno = 0;
            int promretorno = 0;
            int sumarespuesta = 0;
            int promrespuesta = 0;
            int sumarespera = 0;
            int promespera = 0;
            int sumadiferearribo = 0;
            int promdiferearribo = 0;
            int sumapend1CPU = 0;
            int prompend1CPU = 0;
            int sumapendEntrada = 0;
            int prompendEntrada = 0;
            int sumapend2CPU = 0;
            int prompend2CPU = 0;
            int sumapendSalida = 0;
            int prompendSalida = 0;
            int sumapend3CPU = 0;
            int prompend3CPU = 0;
            //Obtiene estadisticas de ordenador
            tiemposfinalizacion = ordenador.tiemposfinalizacion;
            tiemposprimerrespuesta = ordenador.tiemposprimerrespuesta;

            int[] tiemposarriboE = new int[cantidad];
            int[] tiemposretornoE = new int[cantidad];
            int[] tiemposirrupcionE = new int[cantidad];
            int[] tiemposesperaE = new int[cantidad];
            int[] tiemposfinalizacionE = new int[cantidad];
            int[] tiemposprimerrespuestaE = new int[cantidad];
            int[] tiemposrespuestaE = new int[cantidad];
            int sumaretornoE = 0;
            int promretornoE = 0;
            int sumarespuestaE = 0;
            int promrespuestaE = 0;
            int sumaresperaE = 0;
            int promesperaE = 0;
            //Obtiene estadisticas de ordenador
            tiemposfinalizacionE = ordenador.tiemposfinalizacionE;
            tiemposprimerrespuestaE = ordenador.tiemposprimerrespuestaE;
            tiemposarriboE = ordenador.tiemposarriboE;

            int[] tiemposarriboS = new int[cantidad];
            int[] tiemposretornoS = new int[cantidad];
            int[] tiemposirrupcionS = new int[cantidad];
            int[] tiemposesperaS = new int[cantidad];
            int[] tiemposfinalizacionS = new int[cantidad];
            int[] tiemposprimerrespuestaS = new int[cantidad];
            int[] tiemposrespuestaS = new int[cantidad];
            int sumaretornoS = 0;
            int promretornoS = 0;
            int sumarespuestaS = 0;
            int promrespuestaS = 0;
            int sumaresperaS = 0;
            int promesperaS = 0;
            //Obtiene estadisticas de ordenador
            tiemposfinalizacionS = ordenador.tiemposfinalizacionS;
            tiemposprimerrespuestaS = ordenador.tiemposprimerrespuestaS;
            tiemposarriboS = ordenador.tiemposarriboS;
            //Obtiene datos originales de procesos
            for (int x = 0; x < cantidad; x++)
            {
                //-----------------------Obtiene datos orginales-----------------------------
                pend1CPU[x] = (Int32.Parse(DatosFlow.Rows[x].Cells[4].Value.ToString()));
                sumapend1CPU += pend1CPU[x];
                pendentrada[x] = (Int32.Parse(DatosFlow.Rows[x].Cells[5].Value.ToString()));
                sumapendEntrada += pendentrada[x];
                pend2CPU[x] = (Int32.Parse(DatosFlow.Rows[x].Cells[6].Value.ToString()));
                sumapend2CPU += pend2CPU[x];
                pendsalida[x] = (Int32.Parse(DatosFlow.Rows[x].Cells[7].Value.ToString()));
                sumapendSalida += pendsalida[x];
                pend3CPU[x] = (Int32.Parse(DatosFlow.Rows[x].Cells[8].Value.ToString()));
                sumapend3CPU += pend3CPU[x];
                //--------------------------------------------------------------------------
                //Estadisticas para CPU
                tiemposarribo[x] = (Int32.Parse(DatosFlow.Rows[x].Cells[2].Value.ToString()));
                if (x>0)
                {
                    sumadiferearribo += tiemposarribo[x]- tiemposarribo[x-1];
                }
                tiemposretorno[x] = tiemposfinalizacion[x] - tiemposarribo[x];
                sumaretorno += tiemposretorno[x];
                //MessageBox.Show("Las rafagas de CPU son "+ pend1CPU[x] +"/"+ pend2CPU[x] + "/" + pend3CPU[x]);
                tiemposirrupcion[x] = pend1CPU[x] + pend2CPU[x] + pend3CPU[x];
                tiemposespera[x] = tiemposretorno[x] - tiemposirrupcion[x];
                sumarespera += tiemposespera[x];
                tiemposrespuesta[x] = tiemposprimerrespuesta[x] - tiemposarribo[x];
                sumarespuesta += tiemposrespuesta[x];
                string idproceso = DatosFlow.Rows[x].Cells[0].Value.ToString();
                string[] estadistica = {idproceso, tiemposretorno[x].ToString(), tiemposirrupcion[x].ToString(), tiemposespera[x].ToString(), tiemposfinalizacion[x].ToString(), tiemposarribo[x].ToString(), tiemposretorno[x].ToString(), tiemposprimerrespuesta[x].ToString(), tiemposrespuesta[x].ToString() };
                estadisticasEjec.Rows.Add(estadistica);
                //Estadisticas para Entrada
                tiemposretornoE[x] = tiemposfinalizacionE[x] - tiemposarriboE[x];
                sumaretornoE += tiemposretornoE[x];
                tiemposirrupcionE[x] = pendentrada[x];
                tiemposesperaE[x] = tiemposretornoE[x] - tiemposirrupcionE[x];
                sumaresperaE += tiemposesperaE[x];
                tiemposrespuestaE[x] = tiemposprimerrespuestaE[x] - tiemposarriboE[x];
                sumarespuestaE += tiemposrespuestaE[x];
                string[] estadisticaE = { idproceso, tiemposretornoE[x].ToString(), tiemposirrupcionE[x].ToString(), tiemposesperaE[x].ToString(), tiemposfinalizacionE[x].ToString(), tiemposarriboE[x].ToString(), tiemposretornoE[x].ToString(), tiemposprimerrespuestaE[x].ToString(), tiemposrespuestaE[x].ToString() };
                estadisticasEjecE.Rows.Add(estadisticaE);
                //Estadisticas para Salida
                tiemposretornoS[x] = tiemposfinalizacionS[x] - tiemposarriboS[x];
                sumaretornoS += tiemposretornoS[x];
                tiemposirrupcionS[x] = pendsalida[x];
                tiemposesperaS[x] = tiemposretornoS[x] - tiemposirrupcionS[x];
                sumaresperaS += tiemposesperaS[x];
                tiemposrespuestaS[x] = tiemposprimerrespuestaS[x] - tiemposarriboS[x];
                sumarespuestaS += tiemposrespuestaS[x];
                string[] estadisticaS = { idproceso, tiemposretornoS[x].ToString(), tiemposirrupcionS[x].ToString(), tiemposesperaS[x].ToString(), tiemposfinalizacionS[x].ToString(), tiemposarriboS[x].ToString(), tiemposretornoS[x].ToString(), tiemposprimerrespuestaS[x].ToString(), tiemposrespuestaS[x].ToString() };
                estadisticasEjecS.Rows.Add(estadisticaS);
            }
            //Promedios CPU
            promespera = sumarespera / cantidad;
            promrespuesta = sumarespuesta / cantidad;
            promretorno = sumaretorno / cantidad;
            string[] estadistprom = { promespera.ToString(), promretorno.ToString(),promrespuesta.ToString()};
            Estadisticaspromedio.Rows.Add(estadistprom);
            //Promedios Entrada
            promesperaE = sumaresperaE / cantidad;
            promrespuestaE = sumarespuestaE / cantidad;
            promretornoE = sumaretornoE / cantidad;
            string[] estadistpromE = { promesperaE.ToString(), promretornoE.ToString(), promrespuestaE.ToString() };
            EstadisticaspromedioE.Rows.Add(estadistpromE);
            //Promedios Salida
            promesperaS = sumaresperaS / cantidad;
            promrespuestaS = sumarespuestaS / cantidad;
            promretornoS = sumaretornoS / cantidad;
            string[] estadistpromS = { promesperaS.ToString(), promretornoS.ToString(), promrespuestaS.ToString() };
            EstadisticaspromedioS.Rows.Add(estadistpromS);
            if (cantidad > 1)
            {
                promdiferearribo = sumadiferearribo / (cantidad - 1);
            }
            else
            {
                promdiferearribo = 0;
            }
            prompend1CPU = sumapend1CPU / cantidad;
            prompendEntrada = sumapendEntrada / cantidad;
            prompend2CPU = sumapend2CPU / cantidad;
            prompendSalida = sumapendSalida / cantidad;
            prompend3CPU = sumapend3CPU / cantidad;
            string[] rafagaspr = { promdiferearribo.ToString(), prompend1CPU.ToString(), prompendEntrada.ToString(), prompend2CPU.ToString(), prompendSalida.ToString(), prompend3CPU.ToString() };
            Promediosrafagas.Rows.Add(rafagaspr);
        }
        string backcolanuevos;
        string backcolacpu;
        string backcolaBE;
        string backcolaBS;
        string backcolaE;
        string backcolaS;
        string backusomem;
        string backenejc;
        string backenejcent;
        string backenejcsal;
        Queue<int> filasgantt= new Queue<int>();
        Queue<int> filasganttE = new Queue<int>();
        Queue<int> filasganttS = new Queue<int>();
        private void imprimir()
        {
            //usosmem.Clear();
            //Imprime datos ejecucion 
            //Estados procesos
            /*---------------------------Bloqueado Entrada-----------------------------------------*/
            string colaBE = "";
            int[] colBentrada = ordenador.BEntrada.ToArray();
            int cantidadBentrada = colBentrada.Length;
            if (cantidadBentrada == 0)
            {
                colaBE = "-";
            }
            else {
                for (int x = 0; x < cantidadBentrada; x++)
                {
                    colaBE += DatosFlow.Rows[colBentrada[x]].Cells[0].Value.ToString();
                    if (x != (cantidadBentrada - 1))
                    {
                        colaBE += ", ";
                    }
                }
            }
            /*-------------------------------------------------------------------------------------*/
            /*---------------------------Bloqueado Salida-----------------------------------------*/
            string colaBS = "";
            int[] colBsalida = ordenador.BSalida.ToArray();
            int cantidadBsalida = colBsalida.Length;
            if (cantidadBsalida == 0)
            {
                colaBS = "-";
            }
            else {
                for (int x = 0; x < cantidadBsalida; x++)
                {
                    colaBS += DatosFlow.Rows[colBsalida[x]].Cells[0].Value.ToString();
                    if (x != (cantidadBsalida - 1))
                    {
                        colaBS += ", ";
                    }
                }
            }
            /*-------------------------------------------------------------------------------------*/
            //Colas de recursos
            /*---------------------------Cola de memoria-----------------------------------------------------*/
            string colamemoria = "";
            int[] colanuevos = Gestormemoria.obtenercolanuevos();
            int cantidadmemoria = colanuevos.Length;
            if (cantidadmemoria == 0)
            {
                colamemoria = "-";
            }
            else {
                for (int x = 0; x < cantidadmemoria; x++)
                {
                    colamemoria += DatosFlow.Rows[colanuevos[x]].Cells[0].Value.ToString();
                    //+"("+(ordenador.rafagas[(ordenador.rafagas_actuales[x] - 1)][x])+")"
                    if (x != (cantidadmemoria - 1))
                    {
                        colamemoria += ", ";
                    }
                }
            }
            /*-------------------------------------------------------------------------------------*/
            /*---------------------------Cola de CPU-----------------------------------------------------*/
            string colacpu = "";
            int[] colalistos;
            int cantidadcpu;
            if (ordenador.politica == CM)
            {
                colacpu = "";
                for (int i=0;i<ordenador.cantcolas;i++)
                {
                    colalistos = ordenador.Colasmultinivel[i].ToArray();
                    cantidadcpu = colalistos.Length;
                    if (cantidadcpu == 0)
                    {
                        colacpu += "-";
                    }
                    else
                    {
                        for (int x = 0; x < cantidadcpu; x++)
                        {
                            colacpu += DatosFlow.Rows[colalistos[x]].Cells[0].Value.ToString();
                            //+"("+(ordenador.rafagas[(ordenador.rafagas_actuales[x] - 1)][x])+")"
                            if (x != (cantidadcpu - 1))
                            {
                                colacpu += ", ";
                            }
                        }
                    }
                    if (i!=(ordenador.cantcolas-1))
                    {
                        colacpu += "/";
                    }
                }
            }
            else
            {
                colalistos = ordenador.CPU.ToArray();
                cantidadcpu = colalistos.Length;
                if (cantidadcpu == 0)
                {
                    colacpu = "-";
                }
                else
                {
                    for (int x = 0; x < cantidadcpu; x++)
                    {
                        colacpu += DatosFlow.Rows[colalistos[x]].Cells[0].Value.ToString();
                        //+"("+(ordenador.rafagas[(ordenador.rafagas_actuales[x] - 1)][x])+")"
                        if (x != (cantidadcpu - 1))
                        {
                            colacpu += ", ";
                        }
                    }
                }
            }
            
            /*-------------------------------------------------------------------------------------*/
            /*---------------------------Cola de Entrada-------------------------------------------*/
            string colaE = "";
            int[] colentrada = ordenador.Entrada.ToArray();
            int cantidadentrada = colentrada.Length;
            if (cantidadentrada == 0)
            {
                colaE = "-";
            }
            else {
                for (int x = 0; x < cantidadentrada; x++)
                {
                    colaE += DatosFlow.Rows[colentrada[x]].Cells[0].Value.ToString();
                    if (x != (cantidadentrada - 1))
                    {
                        colaE += ", ";
                    }
                }
            }
            /*-------------------------------------------------------------------------------------*/
            /*---------------------------Cola de  Salida-------------------------------------------*/
            string colaS = "";
            int[] colsalida = ordenador.Salida.ToArray();
            int cantidadsalida = colsalida.Length;
            if (cantidadsalida == 0)
            {
                colaS = "-";
            }
            else {
                for (int x = 0; x < cantidadsalida; x++)
                {
                    colaS += DatosFlow.Rows[colsalida[x]].Cells[0].Value.ToString();
                    if (x != (cantidadsalida - 1))
                    {
                        colaS += ", ";
                    }
                }
            }
            /*-------------------------------------------------------------------------------------*/
            //Uso de recursos
            /*---------------------------Uso de memoria-----------------------------------------------------*/
            int[] usomemactual= (int[])Gestormemoria.mapamemoria.Clone();
            string usomemoria = "";
            int[] usandomemoria = Gestormemoria.obtenerusomemoria();
            int cantidadusandom = usandomemoria.Length;
            if (cantidadusandom == 0)
            {
                usomemoria = "-";
            }
            else {
                for (int x = 0; x < cantidadusandom; x++)
                {
                    usomemoria += DatosFlow.Rows[usandomemoria[x]].Cells[0].Value.ToString();
                    //+"("+(ordenador.rafagas[(ordenador.rafagas_actuales[x] - 1)][x])+")"
                    if (x != (cantidadusandom - 1))
                    {
                        usomemoria += ", ";
                    }
                }
            }
            usosmem.Add((int[])usomemactual.Clone());
            particiones.Add((int[])Gestormemoria.particionesmem.Clone());
            cantpartins.Add(Gestormemoria.cantpart);
            fragmentaciones.Add(Gestormemoria.obtenerfragmem());
            /*-------------------------------------------------------------------------------------*/
            /*-------------------------------Uso de CPU-------------------------------------------*/
            string enejc = "";
            if (ordenador.uCPU == -1)
            {
                enejc = "-";
            } else
            {
                enejc = DatosFlow.Rows[ordenador.uCPU].Cells[0].Value.ToString() ;
            }
            /*-------------------------------------------------------------------------------------*/
            /*---------------------------Uso de entrada-------------------------------------------*/
            string enejcent = "";
            if (ordenador.UEntrada == -1)
            {
                enejcent = "-";
            }
            else
            {
                enejcent = DatosFlow.Rows[ordenador.UEntrada].Cells[0].Value.ToString();
            }
            /*-------------------------------------------------------------------------------------*/
            /*-----------------------------Uso de salida-------------------------------------------*/
            string enejcsal = "";
            if (ordenador.USalida == -1)
            {
                enejcsal = "-";
            }
            else
            {
                enejcsal = DatosFlow.Rows[ordenador.USalida].Cells[0].Value.ToString();
            }
            /*-------------------------------------------------------------------------------------*/
            //Para no imprimir filas con datos redundantes se verifica que por lo menos algun valor sea diferente de la fila anterior
            if ((colamemoria != backcolanuevos || colacpu != backcolacpu || colaBE != backcolaBE || colaBS != backcolaBS || colaE != backcolaE || colaS != backcolaS || usomemoria != backusomem || enejc != backenejc || enejcent != backenejcent || enejcsal != backenejcsal) || !NoDatosrep.Checked)
            {
                bool cambiarcolor = false;
                bool cambiarcolorE = false;
                bool cambiarcolorS = false;
                if (enejc != backenejc && (backenejc != "" || enejc != "-"))
                {
                    cambiarcolor = true;
                }
                if (enejcent != backenejcent && (backenejcent != "" || enejcent != "-"))
                {
                    cambiarcolorE = true;
                }
                if (enejcsal != backenejcsal && (backenejcsal != "" || enejcsal!="-"))
                {
                    cambiarcolorS = true;
                }

                backcolanuevos = colamemoria;
                backcolacpu = colacpu;
                backcolaBE = colaBE;
                backcolaBS = colaBS;
                backcolaE = colaE;
                backcolaS = colaS;
                backenejc = enejc;
                backusomem = usomemoria;
                backenejcent = enejcent;
                backenejcsal = enejcsal;
                string[] row = { reloj.ToString(),colamemoria, colacpu, enejc, colaBE + "/" + colaBS, colacpu, colaE, colaS,usomemoria, enejc, enejcent, enejcsal };
                FlujoEjec.Rows.Add(row);
                FlujoEjec.Rows[FlujoEjec.Rows.Count - 1].ContextMenuStrip = MenuConMem;
                FlujoEjec.CellClick += new DataGridViewCellEventHandler(estinst);
                //Resalta filas para GANTT CPU
                if (cambiarcolor && InstGant.Checked)
                {
                    filasgantt.Enqueue((FlujoEjec.Rows.Count - 1));
                }
                //Resalta filas para GANTT Entrada
                if (cambiarcolorE && InstGant.Checked)
                {
                    filasganttE.Enqueue((FlujoEjec.Rows.Count - 1));
                }
                //Resalta filas para GANTT Salida
                if (cambiarcolorS && InstGant.Checked)
                {
                    filasganttS.Enqueue((FlujoEjec.Rows.Count - 1));
                }

            }
        }
        private void Iniciarejecucion(object sender, EventArgs e)
        {
            backcolacpu = "";
            backcolaBE = "";
            backcolaBS = "";
            backcolaE = "";
            backcolaS = "";
            backenejc = "";
            backenejcent = "";
            backenejcsal = "";
            bool noerror = true;
            if (Politica1.Checked)
            {
                politica = FCFS;
            }
            if (Politica2.Checked)
            {
                politica = SJF;
            }
            if (Politica3.Checked)
            {
                politica = SRTF;
            }
            if (Politica4.Checked)
            {
                politica = RR;
            }
            if (politicaCM.Checked)
            {
                politica = CM;
            }
            if (politicaESFCFS.Checked)
            {
                politicaES = FCFS;
            }
            if (politicaESSJF.Checked)
            {
                politicaES = SJF;
            }
            FlujoEjec.Rows.Clear();
            usosmem.Clear();
            particiones.Clear();
            cantpartins.Clear();
            fragmentaciones.Clear();
            reloj =0;
            int cantidad = (DatosFlow.RowCount) - 1;
            if (cantidad==0)
            {
                MessageBox.Show("No hay ningun proceso en la lista");
            }
            else {
                try
                {
                    if (noerror)
                    {
                        inicializar(cantidad);
                        if (haydemasiadogrande)
                        {
                            MessageBox.Show("Existe uno o mas procesos que no caben en la memoria");
                        }
                        else if (naturnodef)
                        {
                            MessageBox.Show("No se han definido los tipos de algunos procesos");
                        }
                        else if(!cerosdet)
                        {
                            ordenador.politica = politica;
                            ordenador.politicaES = politicaES;
                            ordenador.tiempoquantum = tiempoquantum;
                            ordenador.tiempoquantumES = tiempoquantumES;
                            filasgantt.Clear();
                            filasganttE.Clear();
                            filasganttS.Clear();
                            //MessageBox.Show("La organizacion de mem es " + Gestormemoria.particionesmem);
                            while (notermi())
                            {
                                int[] respuesta = verificaraarribar(reloj);
                                //Si hay algun proceso para arribar
                                if (respuesta[0] != -1)
                                {
                                    for (int x = 0; x < respuesta.Length; x++)
                                    {
                                        //MessageBox.Show("Se agrega");
                                        Gestormemoria.agregaracolanuevos(respuesta[x]);
                                    }
                                }
                                Gestormemoria.asignarmemoria();
                                ordenador.ejecutar(reloj);
                                Gestormemoria.asignarmemoria();
                                imprimir();
                                /*if (reloj>10)
                                {
                                    imprimir();
                                    MessageBox.Show("Hola");
                                }*/
                                reloj++;
                            }
                            imprimirestadisticas();
                            int[] filagant = filasgantt.ToArray();
                            DataGridViewCellStyle style = new DataGridViewCellStyle();
                            style.BackColor = Color.Aquamarine;
                            int[] filagantE = filasganttE.ToArray();
                            DataGridViewCellStyle styleE = new DataGridViewCellStyle();
                            styleE.BackColor = Color.LightGreen;
                            int[] filagantS = filasganttS.ToArray();
                            DataGridViewCellStyle styleS = new DataGridViewCellStyle();
                            styleS.BackColor = Color.Orchid;

                            for (int y = 0; y < filagant.Length; y++)
                            {
                                FlujoEjec.Rows[filagant[y]].Cells[3].Style = style;
                                FlujoEjec.Rows[filagant[y]].Cells[9].Style = style;
                            }
                            for (int y = 0; y < filagantE.Length; y++)
                            {
                                FlujoEjec.Rows[filagantE[y]].Cells[10].Style = styleE;
                            }
                            for (int y = 0; y < filagantS.Length; y++)
                            {
                                FlujoEjec.Rows[filagantS[y]].Cells[11].Style = styleS;
                            }
                        }
                        
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Se encontro caracteres no numericos en los datos de los procesos");
                }
                /*
                catch (NullReferenceException)
                {
                    MessageBox.Show("Se encontro valores nulos en los datos de los procesos");
                }
                */
            }
            
        }

        private void Limpiardatos(object sender, EventArgs e)
        {
            DatosFlow.Rows.Clear();
        }

        //Muestra el cuadro de texto del quantum cuando se selecciona politica R



        private void DatosFlow_KeyUp(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("Se ha cambiado el valor de una celda");
        }


        private void OnSelPolESCPU_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            if (!ts.Checked)
            {
                ts.Checked = true;
            }
            if (politicaESFCFS != ts)
            {
                politicaESFCFS.Checked = false;
            }
            if (politicaESSJF != ts)
            {
                politicaESSJF.Checked = false;
            }
        }
        public void definirquantumCPU()
        {
            int temporal = dialogoS.bufer;
            if (temporal > 0)
            {
                tiempoquantum = temporal;
                Politica4.Text = "Round Robin (q=" + tiempoquantum + ")";
            }
            else
            {
                dialogoS.error = true;
                MessageBox.Show("Debe ingresar un tiempo de quantum mayor que 0");
            }
            dialogoS.Texto.Text = tiempoquantum.ToString();
        }
        private void cambiarTiempoDeQuantumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogoS = new DialogoSimple();
            Politica1.Checked = false;
            Politica2.Checked = false;
            Politica3.Checked = false;
            Politica4.Checked = true;
            dialogoS.Text = "Quantum CPU";
            dialogoS.Texto.Text = tiempoquantum.ToString();
            dialogoS.Etiqueta.Text = "Quantum CPU";
            dialogoS.BotonEst.Text = "Establecer este quantum";
            dialogoS.mensaje = "Debe ingresar un numero para el tiempo de quantum";
            //Establece la funcion de retorno
            dialogoS.manejador = definirquantumCPU;
            dialogoS.bufer = tiempoquantum;
            dialogoS.Show();
        }

        private void OnSelPolCPU_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            if (!ts.Checked)
            {
                ts.Checked = true;
            }
            if (Politica1 != ts)
            {
                Politica1.Checked = false;
            }
            if (Politica2 != ts)
            {
                Politica2.Checked = false;
            }
            if (Politica3 != ts)
            {
                Politica3.Checked = false;
            }
            if (Politica4 != ts)
            {
                Politica4.Checked = false;
            }
            if (politicaCM!=ts)
            {
                Naturaleza.Visible = false;
                politicaCM.Checked = false;
            }
        }

        /*---------------------Configuraciones de gestion de memoria-------------------------------*/
        public void definirtamaño()
        {
            int temporal = dialogoS.bufer;
            if (temporal > 0)
            {
                if((temporal % 2)==0)
                {
                    Gestormemoria.tamañomemoria = temporal;
                    Gestormemoria.cantpartdif = 2;
                    Gestormemoria.cantpartig = 2;
                    Gestormemoria.cantpartfij = 2;
                    Gestormemoria.tam1part = Gestormemoria.tamañomemoria / 2;
                    Gestormemoria.tampag = Gestormemoria.tamañomemoria / 2;
                    Gestormemoria.cantpag = 2;
                    Gestormemoria.particionesmemfij = new int[2];
                    Gestormemoria.particionesmemfij[0] = (Gestormemoria.tamañomemoria / 2) + 1;
                    Gestormemoria.particionesmemfij[1] = (Gestormemoria.tamañomemoria / 2) - 1;
                    switch (Gestormemoria.organizacionmem)
                    {
                        case Memoria.Tiposorgmem.PARTDIN:
                            Gestormemoria.cantpart = 1;
                            Gestormemoria.particionesmem = new int[1];
                            Gestormemoria.particionesmem[0] = Gestormemoria.tamañomemoria;
                            Gestormemoria.mapamemoria = new int[1];
                            Gestormemoria.vaciarmemoria();
                            break;
                        case Memoria.Tiposorgmem.PARTFIJO:
                            Gestormemoria.cantpart = 2;
                            Gestormemoria.mapamemoria= new int[2];
                            Gestormemoria.particionesmem = (int[])Gestormemoria.particionesmemfij.Clone();
                            Gestormemoria.vaciarmemoria();
                            break;
                        case Memoria.Tiposorgmem.PAGINADO:
                            Gestormemoria.cantpart = Gestormemoria.cantpag;
                            Gestormemoria.mapamemoria = new int[Gestormemoria.cantpart];
                            Gestormemoria.vaciarmemoria();
                            break;
                    }

                    BotTamMem.Text = "Tamaño de memoria (" + Gestormemoria.tamañomemoria + " palabras)";
                }
                else
                {
                    MessageBox.Show("Debe ingresar un tamaño de memoria par");
                }
            }
            else
            {
                dialogoS.error = true;
                MessageBox.Show("Debe ingresar un tamaño de memoria mayor que 0");
            }
            dialogoS.Texto.Text = Gestormemoria.tamañomemoria.ToString();
        } 
        private void cambiarTamañoMemoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dialogoS = new DialogoSimple();
            dialogoS.Text = "Tamaño de memoria";
            dialogoS.Texto.Text = Gestormemoria.tamañomemoria.ToString();
            dialogoS.Etiqueta.Text = "Tamaño de memoria (palabras)";
            dialogoS.BotonEst.Text = "Establecer este tamaño";
            dialogoS.mensaje = "Debe ingresar un numero para el tamaño de memoria";
            dialogoS.manejador = definirtamaño;
            dialogoS.bufer = Gestormemoria.tamañomemoria;
            dialogoS.Show();
        }

        private void organizarParticionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Gestormemoria.Tampart = Memoria.Opcionestam.DIFTAM;
            dialogoConfPF = new ConfPartFijas2(Gestormemoria);
            dialogoConfPF.manejadormem = Gestormemoria.ConfigurarConPartFijo;
            dialogoConfPF.Show();
        }
        private void organizarPaginasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PartFijas.Checked = false;
            PartDin.Checked = false;
            Paginado.Checked = true;
            Gestormemoria.organizacionmem =  Memoria.Tiposorgmem.PAGINADO;
            ConfPaginado dialogocon = new ConfPaginado(Gestormemoria);
            dialogocon.Show();
        }
        private void TipoPart_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            if (!ts.Checked)
            {
                ts.Checked = true;
            }
            if (PartFijas != ts)
            {
                PartFijas.Checked = false;
            }
            if (PartDin != ts)
            {
                PartDin.Checked = false;
            }
            if (Paginado != ts)
            {
                Paginado.Checked = false;
            }
            if (ts == PartFijas)
            {
                Gestormemoria.organizacionmem = Memoria.Tiposorgmem.PARTFIJO;
                Gestormemoria.particionesmem = Gestormemoria.particionesmemfij;
                Gestormemoria.cantpart = Gestormemoria.cantpartfij;
                Gestormemoria.mapamemoria = new int[Gestormemoria.cantpart];
                Gestormemoria.vaciarmemoria();  
            }
            if (ts == PartDin)
            {
                Gestormemoria.organizacionmem = Memoria.Tiposorgmem.PARTDIN;
                Gestormemoria.cantpart = 1;
                Gestormemoria.particionesmem = new int[1];
                Gestormemoria.particionesmem[0] = Gestormemoria.tamañomemoria;
                Gestormemoria.mapamemoria = new int[1];
                Gestormemoria.vaciarmemoria();
            }
            if (ts == Paginado)
            {
                Gestormemoria.organizacionmem = Memoria.Tiposorgmem.PAGINADO;
                Gestormemoria.cantpart = Gestormemoria.cantpag;
                Gestormemoria.mapamemoria = new int[Gestormemoria.cantpag];
                Gestormemoria.vaciarmemoria();
            }
        }
        private void TipoPart_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            if (ts.Checked)
            {
                if (ts == PartFijas)
                {
                    Gestormemoria.organizacionmem = Memoria.Tiposorgmem.PARTFIJO;
                    Gestormemoria.particionesmem = Gestormemoria.particionesmemfij;
                    Gestormemoria.cantpart = Gestormemoria.cantpartfij;
                    Gestormemoria.mapamemoria = new int[Gestormemoria.cantpart];
                    Gestormemoria.vaciarmemoria();
                }
                if (ts == PartDin)
                {
                    Gestormemoria.organizacionmem = Memoria.Tiposorgmem.PARTDIN;
                    Gestormemoria.cantpart = 1;
                    Gestormemoria.particionesmem = new int[1];
                    Gestormemoria.particionesmem[0] = Gestormemoria.tamañomemoria;
                    Gestormemoria.mapamemoria = new int[1];
                    Gestormemoria.vaciarmemoria();
                }
                if (ts == Paginado)
                {
                    Gestormemoria.organizacionmem = Memoria.Tiposorgmem.PAGINADO;
                    Gestormemoria.cantpart = Gestormemoria.cantpag;
                    Gestormemoria.mapamemoria = new int[Gestormemoria.cantpag];
                    Gestormemoria.vaciarmemoria();
                }
            }
        }
        private void TamPart_Click(object sender, EventArgs e)
        {
            PartFijas.Checked = true;
            PartDin.Checked = false;
            Paginado.Checked = false;
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            if (!ts.Checked)
            {
                ts.Checked = true;
            }
            if (DifTamPart != ts)
            {
                DifTamPart.Checked = false;
            }
            if (IgTamPart != ts)
            {
                IgTamPart.Checked = false;
            }
            if (ts== IgTamPart)
            {
                Gestormemoria.Tampart = Memoria.Opcionestam.MISMTAM;
                Gestormemoria.cantpart = Gestormemoria.cantpartig;
            }
            if (ts == DifTamPart)
            {
                Gestormemoria.Tampart = Memoria.Opcionestam.DIFTAM;
                Gestormemoria.cantpart = Gestormemoria.cantpartdif;
            }
        }
        private void TamPart_CheckedChanged(object sender, EventArgs e)
        {
            PartFijas.Checked = true;
            PartDin.Checked = false;
            Paginado.Checked = false;
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            if (ts.Checked)
            {
                if (ts == IgTamPart)
                {
                    Gestormemoria.Tampart = Memoria.Opcionestam.MISMTAM;
                    Gestormemoria.cantpart = Gestormemoria.cantpartig;
                }
                if (ts == DifTamPart)
                {
                    Gestormemoria.Tampart = Memoria.Opcionestam.DIFTAM;
                    Gestormemoria.cantpart = Gestormemoria.cantpartdif;
                }
            }
            
        }
        private void Cantidadcolas_Click(object sender, EventArgs e)
        {
            IgTamPart.Checked = false;
            DifTamPart.Checked = true;
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            //Solo permite la seleccion de una opcion
            if (!ts.Checked)
            {
                ts.Checked = true;
            }
            if (UnaCola != ts)
            {
                UnaCola.Checked = false;
            }
            if (UnaColaPP != ts)
            {
                UnaColaPP.Checked = false;
            }
            //Configura la memoria
            if (ts==UnaCola)
            {
                Gestormemoria.CantCol = Memoria.OpcionesCol.UNA;
            }
            if (ts == UnaColaPP)
            {
                Gestormemoria.CantCol = Memoria.OpcionesCol.UNAxPART;
            }
            Gestormemoria.cambiarcolas();
        }
        private void Unacola_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            if (ts.Checked)
            {
                if (ts == UnaCola)
                {
                    Gestormemoria.CantCol = Memoria.OpcionesCol.UNA;
                }
                if (ts == UnaColaPP)
                {
                    Gestormemoria.CantCol = Memoria.OpcionesCol.UNAxPART;
                }
                IgTamPart.Checked = false;
                DifTamPart.Checked = true;
                Gestormemoria.cambiarcolas();
            } 
        }

        private void configurarMismT(object sender, EventArgs e)
        {
            DialogoP = new DialogoPartIgtam(Gestormemoria);
            IgTamPart.Checked = true;
            DifTamPart.Checked = false;
            Gestormemoria.Tampart = Memoria.Opcionestam.MISMTAM;
            DialogoP.Show();
        }
        private void Tiporecorridocolas(object sender, EventArgs e)
        {
            UnaCola.Checked = true;
            UnaColaPP.Checked = false;
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            //Solo permite la seleccion de una opcion
            if (!ts.Checked)
            {
                ts.Checked = true;
            }
            if (ConrecorPF != ts)
            {
                ConrecorPF.Checked = false;
            }
            if (SinrecorPF != ts)
            {
                SinrecorPF.Checked = false;
            }
            if (ConrecorPF == ts)
            {
                Gestormemoria.RecorColPF = Memoria.TiposrecCol.Conrecor;
            }
            if (SinrecorPF == ts)
            {
                Gestormemoria.RecorColPF = Memoria.TiposrecCol.Sinrecor;
            }
        }
        private void TiporecorPD_Click(object sender, EventArgs e)
        {
            PartDin.Checked = true;
            PartFijas.Checked = false;
            Paginado.Checked = false;
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            //Solo permite la seleccion de una opcion
            if (!ts.Checked)
            {
                ts.Checked = true;
            }
            if (conRecorPD != ts)
            {
                conRecorPD.Checked = false;
            }
            if (sinRecorPD != ts)
            {
                sinRecorPD.Checked = false;
            }
            if (conRecorPD == ts)
            {
                Gestormemoria.RecorColPD = Memoria.TiposrecCol.Conrecor;
            }
            if (sinRecorPD == ts)
            {
                Gestormemoria.RecorColPD = Memoria.TiposrecCol.Sinrecor;
            }
        }

        private void AlgoritCol_Click(object sender, EventArgs e)
        {
            PartDin.Checked = true;
            PartFijas.Checked = false;
            Paginado.Checked = false;
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            //Solo permite la seleccion de una opcion
            if (!ts.Checked)
            {
                ts.Checked = true;
            }
            if (bESTFITcOL != ts)
            {
                bESTFITcOL.Checked = false;
            }
            if (wORSTFITcOL != ts)
            {
                wORSTFITcOL.Checked = false;
            }
            if (fIRSTFITcOL != ts)
            {
                fIRSTFITcOL.Checked = false;
            }
            if (fIRSTFITcOL == ts)
            {
                Gestormemoria.AlgorCol = Memoria.AlgsCol.FIRSTFIT;
            }
            if (bESTFITcOL == ts)
            {
                Gestormemoria.AlgorCol = Memoria.AlgsCol.BESTFIT;
            }
            if (wORSTFITcOL == ts)
            {
                Gestormemoria.AlgorCol = Memoria.AlgsCol.WORSTFIT;
            }
        }
        private void Menu_MouseEnter(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            ts.ForeColor = Color.RoyalBlue;
            ts.BackColor = Color.Cyan;
        }

        private void Menu_MouseLeave(object sender, EventArgs e)
        {
            ToolStripMenuItem ts = (ToolStripMenuItem)sender;
            ts.BackColor = Color.RoyalBlue;
            ts.ForeColor = Color.Cyan;
        }

        private void colasMultinivelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogoColas = new Colas_multinivel(MiconfColas,ordenador,this);
            DialogoColas.Show();
        }
    }
    /*-----------------------------------------------------------------------------------------*/
}
