using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public class Memoria
    {
        public int tamañomemoria;
        public int cantpart;
        public int cantpag;
        public int cantpartig;
        public int cantpartdif;
        public int cantpartfij;
        public int cantpartdin;
        public int[] particionesmem;
        public int[] particionesmemfij;
        public int[] particionesmemdin;
        public int[] mapamemoria;
        public int tam1part;
        public int tampag;
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
        public bool hayfragin;
        public bool hayfragex;
        public int fragmentacion;
        List<Queue<int>> ListaColas;
        Computador compactual;
        public int[] tamaniosproc;
        public string textounidad;
        public Memoria(int tammem)
        {
            ListaColas = new List<Queue<int>>();
            organizacionmem = Tiposorgmem.PARTFIJO;
            Tampart = Opcionestam.DIFTAM;
            CantCol = OpcionesCol.UNA;
            AlgorCol = AlgsCol.BESTFIT;
            RecorColPD = TiposrecCol.Conrecor;
            RecorColPF = TiposrecCol.Conrecor;
            cantpag = 2;
            cantpart = 2;
            cantpartfij = 2;
            cantpartdif = 2;
            cantpartig = 2;
            tamañomemoria = tammem;
            textounidad = " KB";
            tampag= tamañomemoria / cantpart;
            tam1part = tamañomemoria / cantpart;
            int tamp = tamañomemoria / cantpart;
            particionesmem = new int[cantpart];
            particionesmemfij = new int[cantpart];
            mapamemoria = new int[cantpart];
            vaciarmemoria();
            Queue<int> cola = new Queue<int>();
            ListaColas.Add(cola);
            particionesmem[0] = 48;
            particionesmem[1] = 80;
            particionesmemfij = (int[])particionesmem.Clone();
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
            particionesmem = (int[])newconf.particionesnu.Clone();
            particionesmemfij = (int[])newconf.particionesnu.Clone();
            cantpart = newconf.cantpart;
            cantpartdif= newconf.cantpart;
            cantpartfij = newconf.cantpart;
            mapamemoria = new int[cantpart];
            ordenarparticiones();
            vaciarmemoria();
        }
        /*--------------------------------------------------------------------*/
        /*--------------------------Colas de particiones----------------------*/
        public void cambiarcolas()
        {
            ListaColas.Clear();
            if (CantCol== OpcionesCol.UNA || Tampart== Opcionestam.MISMTAM)
            {
                Queue<int> cola = new Queue<int>();
                ListaColas.Add(cola);
            }
            if (CantCol==OpcionesCol.UNAxPART)
            {
                for (int x = 0; x < cantpart; x++)
                {
                    Queue<int> cola = new Queue<int>();
                    ListaColas.Add(cola);
                }
            }
            vaciarmemoria();
        }
        private void quitarprocesoCola(int id_proceso,int idcola)
        {
            int[] colamem = ListaColas[0].ToArray();
            int longcolm = colamem.Length;
            ListaColas[idcola].Clear();
            for (int i = 0; i < longcolm; i++)
            {
                if (colamem[i] != id_proceso)
                {
                    ListaColas[idcola].Enqueue(colamem[i]);
                }
            }
        }
        /*--------------------------------------------------------------------*/
        /*--------------------------Asignacion de memoria---------------------*/
        public void compactarmemoria()
        {
            int sumalibres = 0;
            int cantlibres = 0;
            int[] backmapamemoria = (int[])mapamemoria.Clone();
            int[] backparticiones = (int[])particionesmem.Clone();
            for (int i = 0; i < cantpart; i++)
            {
                if (mapamemoria[i] == -1)
                {
                    sumalibres += particionesmem[i];
                    cantlibres++;
                }
            }
            if (cantlibres>1)
            {
                //MessageBox.Show("Se compactara la memoria");
                mapamemoria = new int[cantpart - (cantlibres-1)];
                particionesmem = new int[cantpart - (cantlibres - 1)];
                //Desplaza los contenidos del anterior mapa de memoria al tope de la misma
                int h = 0;
                for (int i=0;i<cantpart;i++)
                {
                    if (backmapamemoria[i] != -1)
                    {
                        mapamemoria[h] = backmapamemoria[i];
                        particionesmem[h] = backparticiones[i];
                        h++;
                    }
                }
                mapamemoria[h] = -1;
                particionesmem[h] = sumalibres;
                cantpart -= (cantlibres - 1);
            }
        }
        public int obtenerfragmem()
        {
            switch (organizacionmem)
            {
                case Tiposorgmem.PAGINADO:
                    return obtenerfraginternapag();
                case Tiposorgmem.PARTFIJO:
                    return obtenerfraginternafij();
                case Tiposorgmem.PARTDIN:
                    if (hayfragex)
                    {
                        return fragmentacion;
                    }
                    break;
            }
            return 0;
        }
        public int obtenerfraginternafij()
        {
            int sumafrag=0;
            int tamparticion = tam1part;
            for (int i=0;i<cantpart;i++)
            {
                if (Tampart== Opcionestam.DIFTAM)
                {
                    tamparticion = particionesmem[i];
                }
                if (mapamemoria[i]!=-1)
                {
                    sumafrag += tamparticion-tamaniosproc[mapamemoria[i]];
                }
            }
            return sumafrag;
        }
        public int obtenerfraginternapag()
        {
            int sumafrag = 0;
            int[] tamprocesos = (int[] )tamaniosproc.Clone();
            for (int i = 0; i < cantpart; i++)
            {
                if (mapamemoria[i] != -1)
                {
                    if (tamprocesos[mapamemoria[i]] <= tampag)
                    {
                        sumafrag += tampag - tamprocesos[mapamemoria[i]];
                    }
                    else
                    {
                        tamprocesos[mapamemoria[i]] -= tampag;
                    }
                }
            }
            return sumafrag;
        }
        public bool tamanioadecuado(int tamanio)
        {
            switch (organizacionmem)
            {
                case Tiposorgmem.PARTDIN:
                case Tiposorgmem.PAGINADO:
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
            }   
        }
        public int[] obtenerusomemoria()
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
                case Tiposorgmem.PARTDIN:
                case Tiposorgmem.PAGINADO:
                    colanac = ListaColas[0].ToArray();
                    for (int h = 0; h < colanac.Length; h++)
                    {
                        colanuevoscom.Add(colanac[h]);
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
                case Tiposorgmem.PARTDIN:
                    ListaColas[0].Enqueue(id_proceso);
                    break;
                case Tiposorgmem.PAGINADO:
                    ListaColas[0].Enqueue(id_proceso);
                    break;
            }
        }
        public void liberarmemoria(int id_proceso)
        {
            int indice;
            indice = Array.IndexOf(mapamemoria, id_proceso);
            //Busca la ubicacion del proceso en memoria y libera ese espacio
            if (indice!=-1)
            {
                switch (organizacionmem)
                {
                    case Tiposorgmem.PARTDIN:
                        mapamemoria[indice] = -1;
                        //Cantidad de espacio sin ocupar adyacente a bloque a eliminar
                        int cantidades=particionesmem[indice];
                        //Bloques inicial y final actual de nuevo bloque de mapa de memoria
                        int bloqueina = indice;
                        int bloqueins = indice;
                        //Cantidad de bloques a combinar en uno nuevo
                        int cantael = 0;
                        //Verifica si los bloques adyacentes estan libres o no para unirlos dentro de un mismo bloque
                        //En primer lugar verifica bloques antes del bloque a liberar
                        for (int i = (indice - 1); i >= 0; i--)
                        {
                            if (mapamemoria[i] == -1)
                            {
                                cantidades += particionesmem[i];
                                bloqueina = i;
                                cantael++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        for (int i = (indice + 1); i < cantpart; i++)
                        {
                            if (mapamemoria[i] == -1)
                            {
                                cantidades += particionesmem[i];
                                bloqueins = i;
                                cantael++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (cantael > 0)
                        {
                            int[] backmapamemoria = (int[])mapamemoria.Clone();
                            int[] backmaparticionesmem = (int[])particionesmem.Clone();
                            cantpart -= cantael;
                            mapamemoria = new int[cantpart];
                            particionesmem = new int[cantpart];
                            vaciarmemoria();
                            for (int h = 0; h < bloqueina; h++)
                            {
                                mapamemoria[h] = backmapamemoria[h];
                                particionesmem[h] = backmaparticionesmem[h];
                            }
                            mapamemoria[bloqueina] = -1;
                            particionesmem[bloqueina] = cantidades;
                            for (int h = bloqueina + 1; h < cantpart; h++)
                            {
                                mapamemoria[h] = backmapamemoria[h + cantael];
                                particionesmem[h] = backmaparticionesmem[h + cantael];
                            }
                        }
                        break;
                    case Tiposorgmem.PARTFIJO:
                        mapamemoria[indice] = -1;
                        break;
                    case Tiposorgmem.PAGINADO:
                        while (indice != -1)
                        {
                            mapamemoria[indice] = -1;
                            indice = Array.IndexOf(mapamemoria, id_proceso, indice + 1);
                        }
                        break;
                }
            }
        }
        public void asignarmemoria()
        {
            switch (organizacionmem)
            {
                case Tiposorgmem.PARTFIJO:
                    asignarparticion_fija();
                    break;
                case Tiposorgmem.PARTDIN:
                    hayfragex = false;
                    asignarbloque();
                    break;
                case Tiposorgmem.PAGINADO:
                    asignarpaginas();
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
        private void asignarpaginas()
        {
            int[] colamem = ListaColas[0].ToArray();
            int longcolm = colamem.Length;
            for (int i = 0; i < longcolm; i++)
            {
                int tamanioproc = tamaniosproc[colamem[i]];
                int cantpagreq = (int)Math.Ceiling((double)tamanioproc / (double)tampag);
                int resto = tamanioproc % tampag;
                int r = 0;
                int cantlibres = 0;
                for (int h = 0; h < cantpart; h++)
                {
                    if (mapamemoria[h] == -1)
                    {
                        cantlibres++;
                    }
                }
                if (cantlibres>=cantpagreq)
                {
                    if (resto != 0)
                    {
                        r = 1;
                        asigmarco(colamem[i], resto);
                    }
                    for (int h = 0; h < (cantpagreq - r); h++)
                    {
                        asigmarco(colamem[i], tampag);
                    }
                    quitarprocesoCola(colamem[i],0);
                    compactual.agregarproceso(colamem[i],false,0);
                }
            }
        }
        private void asigmarco(int id_proceso, int tamanio)
        {
            for (int h = 0; h < cantpart; h++)
            {
                if (mapamemoria[h] == -1)
                {
                    mapamemoria[h] = id_proceso;
                    break;
                }
            }
        }
        private void asignarbloque()
        {
            if (RecorColPD == TiposrecCol.Sinrecor)
            {
                if (ListaColas[0].Count!=0)
                {
                    asigbloqaproc(ListaColas[0].Peek());
                } 
            }
            else if (RecorColPD == TiposrecCol.Conrecor)
            {
                int[] colamem = ListaColas[0].ToArray();
                int longcolm = colamem.Length;
                for (int i = 0; i < longcolm; i++)
                {
                    asigbloqaproc(colamem[i]);
                }
            }
        }
        private void asigbloqaproc(int id_proceso)
        {
            bool asignado = false;
            int[] backmapamemoria = (int[])mapamemoria.Clone();
            int[] backmaparticionesmem = (int[])particionesmem.Clone();
            int mentamanio = -1;
            int indment = 0;
            int maxtamanio = -1;
            int indmaxt = 0;
            int primerlibre=-1;
            bool enclibre=false;
            int sumalibres=0;//Para fragmentacion externa
            for (int h=0;h<cantpart;h++)
            {
                if (mapamemoria[h] == -1)
                {
                    sumalibres += particionesmem[h];
                }
                if (mapamemoria[h] == -1 && tamaniosproc[id_proceso] <=particionesmem[h])
                {
                    enclibre = true;
                    if (particionesmem[h]< mentamanio || mentamanio==-1)
                    {
                        mentamanio = particionesmem[h];
                        indment = h;
                    }
                    if (particionesmem[h] > maxtamanio)
                    {
                        maxtamanio = particionesmem[h];
                        indmaxt = h;
                    }
                    if (primerlibre==-1)
                    {
                        primerlibre = h;
                    }
                }
            }
            if (enclibre)
            {
                int i = 0;
                switch (AlgorCol)
                {
                    case AlgsCol.BESTFIT:
                        i = indment;
                        break;
                    case AlgsCol.WORSTFIT:
                        i = indmaxt;
                        break;
                    case AlgsCol.FIRSTFIT:
                        i = primerlibre;
                        break;
                }
                if (particionesmem[i] == tamaniosproc[id_proceso])
                {
                    mapamemoria[i] = id_proceso;
                }
                else
                {
                    cantpart++;
                    mapamemoria = new int[cantpart];
                    particionesmem = new int[cantpart];
                    vaciarmemoria();
                    for (int h = 0; h < i; h++)
                    {
                        mapamemoria[h] = backmapamemoria[h];
                        particionesmem[h] = backmaparticionesmem[h];
                    }
                    mapamemoria[i] = id_proceso;
                    mapamemoria[i + 1] = backmapamemoria[i];
                    particionesmem[i] = tamaniosproc[id_proceso];
                    particionesmem[i + 1] = backmaparticionesmem[i] - tamaniosproc[id_proceso];
                    for (int h = i + 2; h < cantpart; h++)
                    {
                        mapamemoria[h] = backmapamemoria[h - 1];
                        particionesmem[h] = backmaparticionesmem[h - 1];
                    }

                }
                quitarprocesoCola(id_proceso, 0);
                compactual.agregarproceso(id_proceso, false, 0);
            }
            else
            {
                if (sumalibres>= tamaniosproc[id_proceso])
                {
                    hayfragex = true;
                    fragmentacion = sumalibres;
                }
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
                if (ListaColas[0].Count != 0)
                {
                    asigpartaproc(ListaColas[0].Peek());
                }
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
                    quitarprocesoCola(id_proceso, 0);
                    compactual.agregarproceso(id_proceso, false, 0);
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
                        quitarprocesoCola(procesonuevo, i);
                        mapamemoria[i] = procesonuevo;
                        compactual.agregarproceso(procesonuevo, false, 0);
                    }
                }
            }
        }
    }
}
