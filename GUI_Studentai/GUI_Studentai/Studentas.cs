﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_Studentai
{
    class Studentas
    {
        public string PavVrd { get; set; }
        public int Pazym { get; set; }
        public Studentas(string p, int pp)
        {
            PavVrd = p;
            Pazym = pp;
        }
        public override string ToString()
        {
            string eilute = string.Format("{0, -20}   {1, 2}", PavVrd, Pazym);
            return eilute;
        }
    }
}
