using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalAlignment.Utilities
{
    public class Alignment
    {
        public string Sequence1;
        public string Sequence2;

        public Alignment()
        {
            Sequence1 = null;
            Sequence2 = null;
        }

        public Alignment(string s1, string s2)
        {
            Sequence1 = s1;
            Sequence2 = s2;
        }
    }
}
