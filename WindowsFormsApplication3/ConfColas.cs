using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    public class ConfColas
    {
        public int cantcolas;
        List<Queue<int>> Colasmultinivel;
        public bool CRealimentada;
        public bool CApropiativa;
        public int[] politicasColas;
        //Usado para las colas con algoritmo de planificacion RR
        public int[] quantumcolas;
        public int[] maxquantum;
        public string[] nombrescolas;
        const int FCFS = 1;
        const int SJF = 2;
        const int SRTF = 3;
        const int RR = 4;
        public ConfColas()
        {
            cantcolas = 3;
            quantumcolas = new int[cantcolas];
            maxquantum = new int[cantcolas];
            politicasColas = new int[cantcolas];
            nombrescolas = new string[cantcolas];
            CApropiativa = true;
            CRealimentada = false;
            Colasmultinivel = new List<Queue<int>>();
            maxquantum[0] = 1;
            for (int i = 0; i < cantcolas; i++)
            {
                Colasmultinivel.Add(new Queue<int>());
                quantumcolas[i] = 1;
                if (i>0)
                {
                    maxquantum[i] = maxquantum[i-1]+1;
                } 
            }
            politicasColas[0] = FCFS;
            politicasColas[1] = RR;
            quantumcolas[1] = 2;
            politicasColas[2] = SJF;
            nombrescolas[0] = "Sistema";
            nombrescolas[1] = "Interactivo";
            nombrescolas[2] = "Lotes";
        }
    }
}
