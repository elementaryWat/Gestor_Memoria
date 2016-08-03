using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    public class Computador
    {
        public Computador(int cantproces, int[][] configuraciones, ConfColas conac)
        {
            esmultinivel = false;
            Confactcolas = conac;
            naturalezasprocesos = new int[cantproces];
            cantcolas = Confactcolas.cantcolas;
            politicasColas = Confactcolas.politicasColas;
            quantumcolas = Confactcolas.quantumcolas;
            nombrescolas = Confactcolas.nombrescolas;
            CApropiativa = Confactcolas.CApropiativa;
            CRealimentada = Confactcolas.CRealimentada;
            Colasmultinivel = new List<Queue<int>>();
            for (int i=0;i<cantcolas;i++)
            {
                Queue<int> colaac = new Queue<int>();
                Colasmultinivel.Add(colaac);
            }
            // configuraciones[rafaga][num_proceso]
            rafagas = configuraciones;
            esprimerarespuesta = new bool[cantproces];
            esprimerarespuestaE = new bool[cantproces];
            esprimerarespuestaS = new bool[cantproces];
            rafagas_actuales = new int[cantproces];
            rafagas_anteriores = new int[cantproces];
            tiemposfinalizacion = new int[cantproces];
            tiemposprimerrespuesta = new int[cantproces];
            tiemposarriboE = new int[cantproces];
            tiemposfinalizacionE = new int[cantproces];
            tiemposprimerrespuestaE = new int[cantproces];
            tiemposarriboS = new int[cantproces];
            tiemposfinalizacionS = new int[cantproces];
            tiemposprimerrespuestaS = new int[cantproces];
            hayarribo = false;
            hayarriboE = false;
            hayarriboS = false;
            for (int x = 0; x < cantproces; x++)
            {
                //Recuerda la rafaga de CPU que tiene que ejecutar
                rafagas_actuales[x] = 1;
                rafagas_anteriores[x] = 0;
                esprimerarespuesta[x] = true;
                esprimerarespuestaE[x] = true;
                esprimerarespuestaS[x] = true;
            }
            cantidad_procesos = cantproces;
            uCPU = UEntrada = USalida = -1;
            TRestanteCPU = TRestanteEntrada = TRestanteSalida = 0;
            CPU = new Queue<int>();
            BEntrada = new Queue<int>();
            BSalida = new Queue<int>();
            Entrada = new Queue<int>();
            Salida = new Queue<int>();
        }
        public void definirmemoria(Memoria memac)
        {
            memoriaactual = memac;
        }
        Memoria memoriaactual;
        //Contadores auxiliares
        public int[] rafagas_actuales;
        public int[] rafagas_anteriores;
        public int[][] rafagas;
        private int cantidad_procesos;
        public int TRestanteCPU;
        public int TRestanteEntrada;
        public int TRestanteSalida;
        bool hayarribo;
        bool hayarriboE;
        bool hayarriboS;
        public int politica, politicaES, instante;
        int politicaA;
        //Datos para estadisticas de procesos
        public int[] tiemposfinalizacion;
        public int[] tiemposprimerrespuesta;
        public int[] tiemposarriboE;
        public int[] tiemposfinalizacionE;
        public int[] tiemposprimerrespuestaE;
        public int[] tiemposarriboS;
        public int[] tiemposfinalizacionS;
        public int[] tiemposprimerrespuestaS;
        bool[] esprimerarespuesta;
        bool[] esprimerarespuestaE;
        bool[] esprimerarespuestaS;
        public int[] naturalezasprocesos;
        ConfColas Confactcolas;
        public int cantcolas;
        public List<Queue<int>> Colasmultinivel;
        Queue<int> actual = null;
        public bool CRealimentada;
        public bool CApropiativa;
        public int[] politicasColas;
        bool esmultinivel;
        //Usado para las colas con algoritmo de planificacion RR
        public int[] quantumcolas;
        public string[] nombrescolas;
        public int colaenejec;
        const int FCFS = 1;
        const int SJF = 2;
        const int SRTF = 3;
        const int RR = 4;
        const int CM = 5;
        public int tiempoquantum;
        public int tiempoquantumES;
        //Estados
        //Listo = Cola de CPU
        //Ejecucion=uCPU
        public Queue<int> BEntrada;
        public Queue<int> BSalida;
        //COLA de recursos
        public Queue<int> CPU;
        public Queue<int> Entrada;
        public Queue<int> Salida;
        //Uso recursos
        public int uCPU;
        public int UEntrada;
        public int USalida;
        public void agregarproceso(int num_proceso, bool buscaradecuada, int colaains)
        {
            //Se define la cola en la que sera agregada el proceso
            Queue<int> actual=null;
            if (politica == CM)
            {
                //Si la politica actua
                if (CRealimentada)
                {
                    //Inserta el proceso en la primera cola cuando es la primera vez ejecutado y en la correspondiente cuando ya habia sido ejecutado previamente
                    if (buscaradecuada)
                    {
                        for (int i=0;i<cantcolas;i++)
                        {
                            if (rafagas[(rafagas_actuales[num_proceso] - 1)][num_proceso]<quantumcolas[i])
                            {
                                actual = Colasmultinivel[i];
                                politicaA = politicasColas[i];
                                break;
                            }
                        }
                    }
                    else {
                        actual = Colasmultinivel[colaains];
                        politicaA = politicasColas[colaains];
                    }
                    
                }
                else
                {
                    actual = Colasmultinivel[naturalezasprocesos[num_proceso]];
                    politicaA = politicasColas[naturalezasprocesos[num_proceso]];
                }  
            }
            else
            {
                actual = CPU;
                politicaA = politica;
            }
            //Agrega a la cola solo a procesos con irrupciones distintas de 0 en sus rafagas actuales
            if (rafagas[(rafagas_actuales[num_proceso] - 1)][num_proceso] != 0)
            {
                //Tendra en cuenta el valor del arreglo politica para definir el criterio de ordenacion de la cola de listos
                actual.Enqueue(num_proceso);
                hayarribo = true;
                bool modificada = false;
                int temporal = -1;
                //Si se implementa politica SJF o SRTF se ordena la cola de listos
                if (politicaA == SJF || politicaA == SRTF)
                {
                    int[] colaaccpu = actual.ToArray();
                    int cantidadcola = colaaccpu.Length;
                    //Ordena la cola de listos teniendo en cuenta la rafaga actual de cada proceso
                    for (int x = 0; x < (cantidadcola - 1); x++)
                    {
                        for (int y = (x + 1); y < cantidadcola; y++)
                        {
                            if (rafagas[(rafagas_actuales[colaaccpu[x]] - 1)][colaaccpu[x]] > rafagas[(rafagas_actuales[colaaccpu[y]] - 1)][colaaccpu[y]])
                            {
                                temporal = colaaccpu[x];
                                colaaccpu[x] = colaaccpu[y];
                                colaaccpu[y] = temporal;
                                modificada = true;
                            }
                        }
                    }
                    if (modificada)
                    {
                        actual.Clear();
                        for (int x = 0; x < cantidadcola; x++)
                        {
                            actual.Enqueue(colaaccpu[x]);
                        }
                    }
                }
            }
        }
        public bool agregarprocesoE(int num_proceso)
        {
            if (rafagas[1][num_proceso] != 0)
            {
                //Tendra en cuenta el valor del arreglo politica para definir el criterio de ordenacion de la cola de listos
                Entrada.Enqueue(num_proceso);
                hayarriboE = true;
                bool modificada = false;
                bool modificadab = false;
                int temporal = -1;
                int temporal2 = -1;
                //Si se implementa politica SJF o SRTF se ordena la cola de listos
                if (politicaES == SJF)
                {
                    int[] colaentrada = Entrada.ToArray();
                    int[] colabentrada = BEntrada.ToArray();
                    int cantidadcola = colaentrada.Length;
                    int cantidadcolab = colabentrada.Length;
                    //Ordena la cola de listos teniendo en cuenta la primera rafaga de cada proceso
                    for (int x = 0; x < (cantidadcola - 1); x++)
                    {
                        for (int y = (x + 1); y < cantidadcola; y++)
                        {
                            if (rafagas[1][colaentrada[x]] > rafagas[1][colaentrada[y]])
                            {
                                temporal = colaentrada[x];
                                colaentrada[x] = colaentrada[y];
                                colaentrada[y] = temporal;
                                modificada = true;
                            }
                        }
                    }
                    for (int x = 0; x < (cantidadcolab - 1); x++)
                    {
                        for (int y = (x + 1); y < cantidadcolab; y++)
                        {
                            if (rafagas[1][colabentrada[x]] > rafagas[1][colabentrada[y]])
                            {
                                temporal2 = colabentrada[x];
                                colabentrada[x] = colabentrada[y];
                                colabentrada[y] = temporal2;
                                modificadab = true;
                            }
                        }
                    }
                    if (modificada)
                    {
                        Entrada.Clear();
                        for (int x = 0; x < cantidadcola; x++)
                        {
                            Entrada.Enqueue(colaentrada[x]);
                        }
                    }
                    if (modificadab)
                    {
                        BEntrada.Clear();
                        for (int x = 0; x < cantidadcolab; x++)
                        {
                            BEntrada.Enqueue(colabentrada[x]);
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool agregarprocesoS(int num_proceso)
        {
            if (rafagas[3][num_proceso] != 0)
            {
                //Tendra en cuenta el valor del arreglo politica para definir el criterio de ordenacion de la cola de listos
                Salida.Enqueue(num_proceso);
                hayarriboS = true;
                bool modificada = false;
                bool modificadab = false;
                int temporal = -1;
                int temporal2 = -1;
                //Si se implementa politica SJF o SRTF se ordena la cola de listos
                if (politicaES == SJF)
                {
                    int[] colasalida = Salida.ToArray();
                    int[] colabsalida = BSalida.ToArray();
                    int cantidadcola = colasalida.Length;
                    int cantidadcolab = colabsalida.Length;
                    //Ordena la cola de listos teniendo en cuenta la primera rafaga de cada proceso
                    for (int x = 0; x < (cantidadcola - 1); x++)
                    {
                        for (int y = (x + 1); y < cantidadcola; y++)    
                        {
                            if (rafagas[3][colasalida[x]] > rafagas[3][colasalida[y]])
                            {
                                temporal = colasalida[x];
                                colasalida[x] = colasalida[y];
                                colasalida[y] = temporal;
                                modificada = true;
                            }
                        }
                    }
                    for (int x = 0; x < (cantidadcolab - 1); x++)
                    {
                        for (int y = (x + 1); y < cantidadcolab; y++)
                        {
                            if (rafagas[3][colabsalida[x]] > rafagas[3][colabsalida[y]])
                            {
                                temporal2 = colabsalida[x];
                                colabsalida[x] = colabsalida[y];
                                colabsalida[y] = temporal2;
                                modificadab = true;
                            }
                        }
                    }
                    if (modificada)
                    {
                        Salida.Clear();
                        for (int x = 0; x < cantidadcola; x++)
                        {
                            Salida.Enqueue(colasalida[x]);
                        }
                    }
                    if (modificadab)
                    {
                        BSalida.Clear();
                        for (int x = 0; x < cantidadcolab; x++)
                        {
                            BSalida.Enqueue(colabsalida[x]);
                        }
                    }
                }
                return true;
            }
            else { return false; }
        }
        int rafaga;
        int proceso;
        int trafaga;
        private void ejecutandoCPU()
        {
            if (politica == CM)
            {
                //Busca alguna cola con procesos desde la mayor a la menor prioridad
                for (int i=0;i<cantcolas;i++)
                {
                    actual = Colasmultinivel[i];
                    politicaA = politicasColas[i];
                    tiempoquantum = quantumcolas[i];
                    if (actual.Count!=0)
                    {
                        colaenejec = i;
                        break;
                    }
                }
            }
            else
            {
                actual = CPU;
            }
            proceso = actual.Peek();
            actual.Dequeue();
            trafaga = rafagas[(rafagas_actuales[proceso] - 1)][proceso];
            // MessageBox.Show("Se inicia la ejecucion de " +proceso+" en instante "+instante);
            uCPU = proceso;
            //Descuenta el ciclo de reloj en curso
            //Si la rafaga es la primera almacena recuerda el instante de primer respuesta
            if (esprimerarespuesta[proceso])
            {
                esprimerarespuesta[proceso] = false;
                tiemposprimerrespuesta[proceso] = instante;
            }
            rafagas_anteriores[proceso] = rafagas_actuales[proceso];
            rafaga = rafagas_actuales[proceso];

            if (politicaA == RR)
            {
                if (tiempoquantum < trafaga)
                {
                    TRestanteCPU = tiempoquantum - 1;
                    rafagas[(rafagas_actuales[proceso] - 1)][proceso] = trafaga - tiempoquantum;
                    //Recuerda que rafaga de CPU debera ejecutar en la proxima
                }
                else
                {
                    TRestanteCPU = trafaga - 1;
                    rafagas[(rafagas_actuales[proceso] - 1)][proceso] = 0;
                    //Recuerda que rafaga de CPU debera ejecutar en la proxima
                    rafagas_actuales[proceso] += 2;
                }
            }
            else
            {
                TRestanteCPU = trafaga - 1;
                rafagas[(rafagas_actuales[proceso] - 1)][proceso] = 0;
                //Recuerda que rafaga de CPU debera ejecutar en la proxima
                rafagas_actuales[proceso] += 2;
            }
        }

        private void ejecutarcpu()
        {
            if (uCPU == -1)
            {
                Queue<int> actuallocal = null;
                if (politica == CM)
                {
                    esmultinivel = true;
                    for (int i = 0; i < cantcolas; i++)
                    {
                        actuallocal = Colasmultinivel[i];
                        if (actuallocal.Count != 0)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    actuallocal = CPU;
                }
                //Si hay algun proceso en la cola de Listos lo pone en ejecucion
                if (actuallocal.Count != 0)
                {
                    ejecutandoCPU();
                }
            }
            else {//Si ya hay algun proceso en ejecucion
                //Determina si ya ha terminado su ejecucion 
                if (TRestanteCPU == 0)
                {
                    rafaga = rafagas_actuales[uCPU];
                    //Si no termino de ejecutar la rafaga actual y se usa politica Round Robin
                    if (rafaga == rafagas_anteriores[uCPU] && politicaA == RR)
                    {
                        if (esmultinivel && Confactcolas.CRealimentada && colaenejec == 0)
                        {
                            agregarproceso(uCPU, true, colaenejec);
                        }
                        else
                        {
                            agregarproceso(uCPU, false, colaenejec);
                        }     
                    }
                    else
                    {
                        //Si se termino de ejecutar la ultima rafaga del proceso recuerda el instante de finalizacion
                        if (rafaga == 7)
                        {
                            tiemposfinalizacion[uCPU] = instante;
                            memoriaactual.liberarmemoria(uCPU);
                        }
                        //Lo agrega a la cola de entrada o salida dependiendo de la rafaga de cpu que deba ejecutar luego
                        else if (rafaga == 3)
                        {
                            //Determinael instante de finalizacion en CPU del proceso dependiendo del valor de las siguientes rafagas
                            if (rafagas[2][uCPU] == 0 && rafagas[4][uCPU] == 0)
                            {
                                tiemposfinalizacion[uCPU] = instante;
                                memoriaactual.liberarmemoria(uCPU);
                            }
                            BEntrada.Enqueue(uCPU);
                            if (agregarprocesoE(uCPU))
                            {
                                tiemposarriboE[uCPU] = instante;
                            }
                            else
                            {
                                BEntrada.Dequeue();
                                if (rafagas[2][uCPU] == 0)
                                {
                                    BSalida.Enqueue(uCPU);
                                    if (agregarprocesoS(uCPU))
                                    {
                                        tiemposarriboS[uCPU] = instante;
                                    }
                                    else
                                    {
                                        BSalida.Dequeue();
                                        if (rafagas[4][uCPU] != 0)
                                        {
                                            rafagas_actuales[uCPU] += 2;
                                            agregarproceso(uCPU,false,0);
                                        }
                                    }
                                }
                                else
                                {
                                    agregarproceso(uCPU,false,0);
                                }
                            }

                            //Informa de un nuevo arribo a la cola de entrada
                        }
                        else if (rafaga == 5)
                        {
                            //Determinael instante de finalizacion en CPU del proceso dependiendo del valor de las siguientes rafagas
                            if (rafagas[4][uCPU] == 0)
                            {
                                tiemposfinalizacion[uCPU] = instante;
                                memoriaactual.liberarmemoria(uCPU);
                            }
                            BSalida.Enqueue(uCPU);
                            if (agregarprocesoS(uCPU))
                            {
                                tiemposarriboS[uCPU] = instante;
                            }
                            else
                            {
                                BSalida.Dequeue();
                                if (rafagas[4][uCPU] != 0)
                                {
                                    agregarproceso(uCPU,false,0);
                                }
                            }
                            //Informa de un nuevo arribo a la cola de salida
                        }
                    }
                    //Si hay algun proceso en la cola de Listos lo pone en ejecucion
                    //Orden FIFO
                    if (actual.Count != 0)
                    {
                        ejecutandoCPU();
                    }
                    else
                    {//Si no hay ningun proceso en la cola restablece el valor del uso de CPU a 0
                        uCPU = -1;
                    }
                }
                else
                {//En caso de que se este usando politica SRTF o colas multinivel con prioridad entre colas apropiativa
                 //Si no termino la ejecucion verifica si el primer proceso en la cola de listos arribo un proceso con tiempo de irrupcion menor
                 //que el actualmente en ejecucion o que haya arribado algun proceso en alguna cola de mayor prioridad
                    if ((politica == SRTF || (politica==CM && CApropiativa)) && hayarribo)
                    {
                        bool verificarc = false;
                        bool verificars = false;
                        hayarribo = false;
                        int colanueva = 0;
                        if (politica == CM)
                        {
                            for (int i = 0; i < colaenejec; i++)
                            {
                                actual = Colasmultinivel[i];
                                if (actual.Count != 0)
                                {
                                    colanueva = i;
                                    verificarc = true;
                                    verificars = true;
                                    break;
                                }
                            }
                            if (!verificarc && politicasColas[colaenejec]==SRTF)
                            {
                                verificarc = true;
                                actual = Colasmultinivel[colaenejec];
                            }
                        }
                        else if (politica == SRTF)
                        {
                            actual = CPU;
                            verificarc = true;
                        }
                        // MessageBox.Show("Hola");
                        if (actual.Count != 0 && verificarc)
                        {
                            proceso = actual.Peek();
                            rafaga = rafagas[(rafagas_actuales[proceso] - 1)][proceso];
                            if ((rafaga < TRestanteCPU) || verificars)
                            {
                                //Debera indicar que no termino la rafaga actual del proceso actualizando la cantidad de irrupciones pendientes
                                rafagas_actuales[uCPU] -= 2;
                                //MessageBox.Show("El proceso " + uCPU + " sale de ejecucion en el instante " + instante + " con la rafaga " + rafagas_actuales[uCPU]+"pendiente de terminar con "+ TRestanteCPU+" irrupciones");
                                rafagas[(rafagas_actuales[uCPU] - 1)][uCPU] = TRestanteCPU;
                                //Saca el nuevo proceso a ejecutar
                                actual.Dequeue();
                                //Ordena la cola de listos 
                                agregarproceso(uCPU,false,colaenejec);
                                colaenejec = colanueva;
                                //Inicia la ejecucion del nuevo proceso
                                uCPU = proceso;
                                //Descuenta el ciclo de reloj en curso
                                rafaga = rafagas_actuales[proceso];
                                //Si la rafaga es la primera almacena recuerda el instante de primer respuesta
                                if (rafaga == 1)
                                {
                                    tiemposprimerrespuesta[proceso] = instante;
                                }
                                TRestanteCPU = rafagas[(rafagas_actuales[proceso] - 1)][proceso] - 1;
                                rafagas[(rafagas_actuales[proceso] - 1)][proceso] = 0;
                                //Recuerda que rafaga de CPU debera ejecutar en la proxima
                                rafagas_actuales[proceso] += 2;

                            }
                            else
                            {
                                TRestanteCPU--;
                            }
                        }
                        //MessageBox.Show("Chau.El proceso actualmente en ejecucion es "+(uCPU+1)+".La rafaga actual es "+ rafagas_actuales[uCPU]+" con " + rafagas[(rafagas_actuales[uCPU] - 1)][uCPU]+"irrupciones pendientes");
                        //MessageBox.Show("Existen "+CPU.Count+" en la cola de listos");
                        else
                        {
                            TRestanteCPU--;
                        }

                    }
                    else
                    {
                        //Sino simplemente continua con la ejecucion
                        TRestanteCPU--;
                        //MessageBox.Show("El proceso " + uCPU + " en el instante " + instante + "le quedan " + TRestanteCPU + " irrupciones");
                    }
                }
            }
        }
        private void ejecutandoEntrada()
        {
            proceso = Entrada.Peek();
            //Determina si es la primera respuesta
            //Condicional util para politica RR
            if (esprimerarespuestaE[proceso])
            {
                esprimerarespuestaE[proceso] = false;
                tiemposprimerrespuestaE[proceso] = instante;
            }
            trafaga = rafagas[1][proceso];
            //MessageBox.Show("Se inicia la ejecucion en entrada de " + proceso + " en instante " + instante);
            Entrada.Dequeue();
            UEntrada = proceso;
            TRestanteEntrada = trafaga - 1;
            rafagas[1][proceso] = 0;
        }
        private void ejecutarEntrada()
        {
            //Si no hay ningun proceso en ejecucion de entrada
            if (UEntrada == -1)
            {
                //Si hay algun proceso en la cola de Entrada lo pone en ejecucion
                if (Entrada.Count != 0)
                {
                    ejecutandoEntrada();
                }
            }
            else {
                if (TRestanteEntrada == 0)
                {
                    tiemposfinalizacionE[UEntrada] = instante;
                    //Desbloquea el proceso
                    BEntrada.Dequeue();
                    //Lo agrega a la cola de CPU
                    agregarproceso(UEntrada,false,0);
                    //Si hay algun proceso en la cola de Entrada lo pone en ejecucion
                    //Orden FIFO
                    if (Entrada.Count != 0)
                    {
                        ejecutandoEntrada();
                    }
                    else
                    {//Si no hay ningun proceso en la cola restablece el valor del uso de Entrada a 0
                        UEntrada = -1;
                    }
                }
                else
                {
                    TRestanteEntrada--;
                }
            }
        }
        private void ejecutandoSalida()
        {
            proceso = Salida.Peek();
            if (esprimerarespuestaS[proceso])
            {
                esprimerarespuestaS[proceso] = false;
                tiemposprimerrespuestaS[proceso] = instante;
            }
            trafaga = rafagas[3][proceso];
            //MessageBox.Show("Se inicia la ejecucion en salida de " + proceso + " en instante " + instante);
            Salida.Dequeue();
            USalida = proceso;
            TRestanteSalida = trafaga - 1;
            rafagas[3][proceso] = 0;
        }
        private void ejecutarSalida()
        {
            //Si no hay ningun proceso en ejecucion de salida
            if (USalida == -1)
            {
                //Si hay algun proceso en la cola de Salida lo pone en ejecucion
                //Orden FIFO
                if (Salida.Count != 0)
                {
                    ejecutandoSalida();
                }
            }
            else { 
                if (TRestanteSalida == 0)
                {
                    tiemposfinalizacionS[USalida] = instante;
                    //Desbloquea el proceso
                    BSalida.Dequeue();
                    //Lo agrega a la cola de CPU
                    agregarproceso(USalida,false,0);
                    //Si hay algun proceso en la cola de Salida lo pone en ejecucion
                    //Orden FIFO
                    if (Salida.Count != 0)
                    {
                        ejecutandoSalida();
                    }
                    else
                    {//Si no hay ningun proceso en la cola restablece el valor del uso de Salida a 0
                        USalida = -1;
                    }
                }
                else
                {
                    TRestanteSalida--;
                }
            }
        }

        public void ejecutar(int instant)
        {// rafagas[rafaga][num_proceso]
            instante = instant;
            ejecutarcpu();
            ejecutarEntrada();
            ejecutarSalida();
            //Verifica si la CPU esta ociosa para atender procesos desbloqueados
            if (uCPU == -1)
            {
                ejecutarcpu();
            }
        }
        public bool noterminado()
        {
            Queue<int> actual = null;
            bool terminado = true;
            if (politica == CM)
            {
                for (int i = 0; i < cantcolas; i++)
                {
                    actual = Colasmultinivel[i];
                    if (actual.Count != 0)
                    {
                        terminado = false;
                        break;
                    }
                }
            }
            else
            {
                if (CPU.Count!=0)
                {
                    terminado = false;
                }
            }
            if (terminado && Entrada.Count == 0 && Salida.Count == 0 && uCPU == -1 && UEntrada == -1 && USalida == -1)
            {
                return false;
            }
            return true;
        }
    }
}
