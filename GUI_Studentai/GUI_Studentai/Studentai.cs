using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_Studentai
{
    class Studentai
    {
        const int cn = 500;
        public int kiek { get; set; }
        Studentas[] m;
        public Studentai()
        {
            m = new Studentas[cn];
            kiek = 0;
        }
        public void deti (Studentas s) { m[kiek++] = s; }
        public Studentas imti (int i) { return m[i]; }
    }
}
