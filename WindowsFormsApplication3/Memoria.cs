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
        public int[] espocupados;
        public int[] mapamemoria;
        public int tam1part;
        public enum Tiposorgmem { PARTFIJO = 1, PARTDIN, PAGINADO }
            public enum Opcionestam {MISMTAM = 1, DIFTAM };
                public enum OpcionesCol { UNA = 1, UNAxPART };
        public Tiposorgmem organizacionmem;
        public Opcionestam Tampart;
        public OpcionesCol CantCol;
        List<Queue<int>> ListaColas;
        Computador compactual;
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
            espocupados = new int[cantpart];
            mapamemoria = new int[cantpart];
            vaciarmemoria();
            Queue<int> cola = new Queue<int>();
            ListaColas.Add(cola);
            particionesmem[0] = 48;
            particionesmem[1] = 80;
        }
        public void definirordenador(Computador pcactual)
        {
            compactual = pcactual;
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
            vaciarmemoria();
        }
        /*--------------------------------------------------------------------*/
        /*--------------------------Colas de particiones----------------------*/
        public void cambiarcolas()
        {
            ListaColas.Clear();
            Queue<int> cola = new Queue<int>();
            if (CantCol== OpcionesCol.UNA || Tampart== Opcionestam.MISMTAM)
            {
                ListaColas.Add(cola);
            }
            if (CantCol==OpcionesCol.UNAxPART)
            {
                for (int x = 0; x < cantpart; x++)
                {
                    ListaColas.Add(cola);
                }
            }
        }
        /*--------------------------------------------------------------------*/
        /*--------------------------Asignacion de memoria---------------------*/
        public void vaciarmemoria()
        {
            //Vacia todas las particiones de memoria
            for (int x = 0; x < cantpart; x++)
            {
                mapamemoria[x] = -1;
            }
        }
        private void agregarconunacola(int id_proceso,int tamanio)
        {
            //Primero revisa la cola para ver si existe algun proceso esperando que pueda ser asignado
            //Si existe alguno
        }
        private void asignarparticion_fija(int id_proceso,int tamanio)
        {
            switch (CantCol)
            {
                case OpcionesCol.UNA:
                    agregarconunacola(id_proceso, tamanio);
                    break;
            }
        }
        
        public void asignarmemoria(int id_proceso,int tamanio)
        {
            switch (organizacionmem)
            {
                case Tiposorgmem.PARTFIJO:
                    asignarparticion_fija(id_proceso,tamanio);
                    break;
            }
        }
    }
}
