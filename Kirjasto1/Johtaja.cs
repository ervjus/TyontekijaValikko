using System;
using System.Collections.Generic;
using System.Text;

namespace Kirjasto1
{
    public class Johtaja : Duunari
    {
        private string _rooli;

        public string Rooli
        {
            get { return _rooli; }
            set { _rooli = value; }
        }




        public Johtaja()
        {

        }

 
    }
}
