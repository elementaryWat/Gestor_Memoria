using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    public class Memoria
    {
        public int tamañomemoria;
        public int cantpart;
        public int[] particionesmem;
        public int tam1part;
        public enum Tiposorgmem { PARTFIJO = 1, PARTDIN, PAGINADO }
            public enum Opcionestam {MISMTAM = 1, DIFTAM };
                public enum OpcionesCol { UNA = 1, UNAxPART };
        public Tiposorgmem organizacionmem;
        public Opcionestam Tampart;
        public OpcionesCol CantCol;
        List<Queue<int>> ListaColas;
        public Memoria(int tammem)
        {
            ListaColas = new List<Queue<int>>();
            organizacionmem = Tiposorgmem.PARTFIJO;
            Tampart = Opcionestam.DIFTAM;
            CantCol = OpcionesCol.UNA;
            cantpart = 2;
            tamañomemoria = tammem;
            tam1part = tamañomemoria / cantpart;
            int tamp = tamañomemoria / cantpart;
            particionesmem = new int[cantpart];
            Queue<int> cola = new Queue<int>();
            ListaColas.Add(cola);
            for (int x=0;x< cantpart; x++)
            {
                particionesmem[x] = tamp;
            }
        }
        /*--------------------------Particiones-------------------------------*/
        private void ordenarparticiones()
        {
            int temporal;
            for (int x=0;x<(cantpart-1);x++)
            {
                for (int y=(x+1);y<cantpart;y++)
                {
                    if (particionesmem[x]>particionesmem[y])
                    {
                        temporal = particionesmem[x];
                        particionesmem[x] = particionesmem[y];
                        particionesmem[y] = temporal;
                    }
                }
            }
        }
        public void ConfigurarConPartFijo(ConfPartFijas2 newconf)
        {
            particionesmem = newconf.particionesnu;
            cantpart = newconf.cantpart;
            ordenarparticiones();
        }
        /*--------------------------------------------------------------------*/
        /*--------------------------Colas de particiones----------------------*/
        public void cambiarcolas()
        {
            ListaColas.Clear();
            Queue<int> cola = new Queue<int>();
            switch (CantCol)
            {
                case OpcionesCol.UNA:
                    ListaColas.Add(cola);
                    break;
                case OpcionesCol.UNAxPART:
                        for (int x=0;x<cantpart;x++)
                        {
                            ListaColas.Add(cola);
                        }
                    break;
            }
        }
        /*--------------------------------------------------------------------*/
    }
}
