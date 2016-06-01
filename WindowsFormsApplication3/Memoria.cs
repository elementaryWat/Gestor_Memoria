﻿using System;
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
        public enum Opcionestam { MISMTAM = 1, DIFTAM };
        public enum AlgsCol {BESTFIT = 1, WORSTFIT,FIRSTFIT };
                public enum OpcionesCol { UNA = 1, UNAxPART };
                    public enum TiposrecCol { Conrecor = 1, Sinrecor };
        public Tiposorgmem organizacionmem;
        public AlgsCol AlgorCol;
        public Opcionestam Tampart;
        public OpcionesCol CantCol;
        public TiposrecCol RecorColPF;
        public TiposrecCol RecorColPD;
        List<Queue<int>> ListaColas;
        Computador compactual;
        int[] tamaniosproc;
        public Memoria(int tammem)
        {
            ListaColas = new List<Queue<int>>();
            organizacionmem = Tiposorgmem.PARTFIJO;
            Tampart = Opcionestam.DIFTAM;
            CantCol = OpcionesCol.UNA;
            AlgorCol = AlgsCol.BESTFIT;
            RecorColPD = TiposrecCol.Conrecor;
            RecorColPF = TiposrecCol.Conrecor;
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
        public void definirtamproc(int[] tamproc)
        {
            tamaniosproc = tamproc;
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
            mapamemoria = new int[cantpart];
            espocupados = new int[cantpart];
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
            vaciarmemoria();
        }
        /*--------------------------------------------------------------------*/
        /*--------------------------Asignacion de memoria---------------------*/
        public bool tamanioadecuado(int tamanio)
        {
            switch (organizacionmem)
            {
                case Tiposorgmem.PARTDIN:
                    if (tamanio <= tamañomemoria)
                    {
                        return true;
                    }
                    break;
                case Tiposorgmem.PARTFIJO:
                    switch (Tampart)
                    {
                        case Opcionestam.DIFTAM:
                            if (tamanio<=particionesmem[cantpart-1])
                            {
                                return true;
                            }
                            break;
                        case Opcionestam.MISMTAM:
                            if (tamanio <= tam1part)
                            {
                                return true;
                            }
                            break;
                    }
                    break;
            }           
            return false;
        }
        public void vaciarmemoria()
        {
            //Vacia todas las particiones de memoria
            for (int x = 0; x < cantpart; x++)
            {
                mapamemoria[x] = -1;
                espocupados[x] = 0;
            }
        }
        public int[]obtenerusomemoria()
        {
            List<int> usandomemoria = new List<int>();
            switch (organizacionmem)
            {
                case Tiposorgmem.PARTFIJO:
                    for (int i=0;i<cantpart;i++)
                    {
                        if (mapamemoria[i]!=-1)
                        {
                            usandomemoria.Add(mapamemoria[i]);
                        }
                    }
                    break;
            }
            return usandomemoria.ToArray();
        }
        public int[] obtenercolanuevos()
        {
            List<int> colanuevoscom= new List<int>();
            int[] colanac;
            switch (organizacionmem)
            {
                case Tiposorgmem.PARTFIJO:
                    switch(CantCol)
                    {
                        case OpcionesCol.UNA:
                            colanac = ListaColas[0].ToArray();
                            for (int h = 0; h < colanac.Length; h++)
                            {
                                colanuevoscom.Add(colanac[h]);
                            }
                            break;
                        case OpcionesCol.UNAxPART:
                                for (int i=0;i<cantpart;i++)
                                {
                                    colanac = ListaColas[i].ToArray();
                                    for (int h=0;h<colanac.Length;h++)
                                    {
                                        colanuevoscom.Add(colanac[h]);
                                    }
                                }
                            break; 
                    }
                    break;
            }
            return colanuevoscom.ToArray();
        }
        public void agregaracolanuevos(int id_proceso)
        {
            switch (organizacionmem)
            {
                case Tiposorgmem.PARTFIJO:
                    agregaraparticion_fija(id_proceso);
                    break;
            }
        }
        public void liberarmemoria(int id_proceso)
        {
            int indice;
            indice = Array.IndexOf(mapamemoria, id_proceso);
            //Busca la ubicacion del proceso en memoria y libera ese espacio
            while (indice!=-1)
            {
                mapamemoria[indice] = -1;
                espocupados[indice] = 0;
                indice = Array.IndexOf(mapamemoria,id_proceso,indice + 1);
            }
        }
        public void asignarmemoria()
        {
            switch (organizacionmem)
            {
                case Tiposorgmem.PARTFIJO:
                    asignarparticion_fija();
                    break;
            }
        }
        private void agregaraparticion_fija(int id_proceso)
        {
            switch (CantCol)
            {
                case OpcionesCol.UNA:
                    ListaColas[0].Enqueue(id_proceso);
                    break;
                case OpcionesCol.UNAxPART:
                    bool asignado = false;
                    //Insercion del proceso en cola adecuada
                    for (int i = 0; i < cantpart; i++)
                    {
                        //Si la partcion actual tiene el tamaño suficiente para contener al proceso
                        if (tamaniosproc[id_proceso] <= particionesmem[i])
                        {
                            ListaColas[i].Enqueue(id_proceso);
                            asignado = true;
                        }
                        if (asignado)
                        {
                            break;
                        }
                    }
                    break;
            }
        }
        private void asignarparticion_fija()
        {
            switch (CantCol)
            {
                case OpcionesCol.UNA:
                    asignarconunacola();
                    break;
                case OpcionesCol.UNAxPART:
                    asignarconvarcolas();
                    break;
            }
        }
        private void asignarconunacola()
        {
            if (RecorColPF == TiposrecCol.Sinrecor)
            {
                asigpartaproc(ListaColas[0].Peek());
            }
            else if (RecorColPF == TiposrecCol.Conrecor)
            {
                int[] colamem = ListaColas[0].ToArray();
                int longcolm = colamem.Length;
                for (int i = 0; i < longcolm; i++)
                {
                    asigpartaproc(colamem[i]);
                }
            }

        }
        private void asigpartaproc(int id_proceso)
        {
            bool asignado = false;
            int tampar = tam1part;
            //Recorre la memoria en busca de particiones libres para el proceso
            for (int h = 0; h < cantpart; h++)
            {
                if (Tampart == Opcionestam.DIFTAM)
                {
                    tampar = particionesmem[h];
                }
                //Si encuentra un hueco libre en memoria lo asigna al proceso y lo agrega a la cola de listos
                if (mapamemoria[h] == -1 && tamaniosproc[id_proceso] <= tampar)
                {
                    mapamemoria[h] = id_proceso;
                    ListaColas[0].Dequeue();
                    compactual.agregarproceso(id_proceso);
                    espocupados[h] = tamaniosproc[id_proceso];
                    asignado = true;
                }
                if (asignado)
                {
                    break;
                }
            }
        }
        private void asignarconvarcolas()
        {
            //Asignacion a memoria mirando colas de cada partcion
            for (int i = 0; i < cantpart; i++)
            {
                if (mapamemoria[i] == -1)
                {
                    if (ListaColas[i].Count != 0)
                    {
                        int procesonuevo = ListaColas[i].Peek();
                        ListaColas[i].Dequeue();
                        mapamemoria[i] = procesonuevo;
                        compactual.agregarproceso(procesonuevo);
                        espocupados[i] = tamaniosproc[procesonuevo];
                    }
                }
            }
        }
        

    }
}
