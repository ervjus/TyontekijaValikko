using System;

namespace Kirjasto1
{
    public class Duunari
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string _etunimi;

        public string Etunimi
        {
            get { return _etunimi; }
            set { _etunimi = value; }
        }

        private string _sukunimi;

        public string Sukunimi
        {
            get { return _sukunimi; }
            set { _sukunimi = value; }
        }


        private float _palkka;

        public float Palkka
        {
            get { return _palkka; }
            set { _palkka = value; }
        }

        private int _tyotunnit;

        public int Tyotunnit
        {
            get { return _tyotunnit; }
            set { _tyotunnit = value; }
        }


        public Duunari()
        {

        }
    
    }
}
