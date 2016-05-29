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
        public Memoria(int tammem)
        {
            cantpart = 4;
            tamañomemoria = tammem;
            int tamp = tamañomemoria / cantpart;
            particionesmem = new int[cantpart];
            for(int x=0;x< cantpart; x++)
            {
                particionesmem[x] = tamp;
            }
        }
        public void ConfigurarConPartFijo(ConfPartFijas2 newconf)
        {
            particionesmem = newconf.particionesnu;
            cantpart = newconf.cantpart;
        }
    }
}
