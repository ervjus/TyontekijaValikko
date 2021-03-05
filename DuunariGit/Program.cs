using System;
using Kirjasto1;

namespace DuunariGit
{
    class Program
    {
        static void Main(string[] args)
        {

            SqlPyorittaja sp = new SqlPyorittaja(); // Luodaan uusi sqlpyörittäjä
            DataManager pyorittaja = new DataManager(sp); // Luodaan uusi datamanageri joka ottaa sql metodit mukaan
            pyorittaja.AlkuMenu(); // Käynnistetään AlkuMenu
 
        }
    }
}
