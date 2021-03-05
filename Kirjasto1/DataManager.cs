using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Kirjasto1;

namespace Kirjasto1
{
    public class DataManager
    {
       // Otetaan tietokanta metodit mukaan
        private SqlPyorittaja sqlpyorittaja;

        public DataManager(SqlPyorittaja sqlpyorittaja)
        {
            this.sqlpyorittaja = sqlpyorittaja;
        }

        // Valikko mistä valitaan vaihtoehtoja numeroiden avulla
        public void AlkuMenu()
        {
            bool naytaMenu = true;

            while (naytaMenu)
            {
                Console.Clear();
                string otsikot = "\n |Alkuvalikko|";
                otsikot += "\n1.Luo Työntekijä";
                otsikot += "\n2 Näytä työntekijät";
                otsikot += "\n3 Poista työntekijä listalta";
                otsikot += "\n4 Exit";
                otsikot += "\nValitse toiminto numerolla";

                Console.WriteLine(otsikot);
                ConsoleKey valinta = Console.ReadKey(true).Key;

                switch (valinta)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Console.WriteLine("Anna työntekijän tiedot");
                        Duunari duunari = LuoTyontekija();
                        sqlpyorittaja.LisaaTyontekija(duunari);
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        Console.WriteLine("|Kaikkien työntekijöiden tiedot|");
                        PrinttaaNumeroillaTyontekijat(sqlpyorittaja.KaikkiTyontekijat());
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        Console.WriteLine("\n Valitse poistettava työntekijä Id numerolla");
                        var k = ValitsePoistettavaTyontekija(sqlpyorittaja.KaikkiTyontekijat());
                        NaytaYksiTyontekija(k);
                        sqlpyorittaja.Poistatyontekija(k.Id);
                        Console.WriteLine("Työntekijä on poistettu tietokannasta");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D4:
                        naytaMenu = false;
                        break;


                }
            }

        }

        // Tulostetaan numeroiden avulla työntekijät
        public void PrinttaaNumeroillaTyontekijat(List<Duunari> duunari)
        {
            for (int i = 0; i < duunari.Count; i++)
            {
                Console.WriteLine($"ID: {i + 1}. Etunimi: {duunari[i].Etunimi} Sukunimi: {duunari[i].Sukunimi} Työtunnit: {duunari[i].Tyotunnit} Palkka:{duunari[i].Palkka}");
            }

        }

        // Luodaan uusi työntekijä
        public Duunari LuoTyontekija()
        {
            Duunari duunari = new Duunari();
            Console.WriteLine("Valitse duunarin tyyppi");
            Console.WriteLine("1.Duunari");
            Console.WriteLine("2.Johtaja");
            Console.WriteLine("3 Takaisin valikkoon");


            int select; // Kysytään ja valitaan onko työntekijä vai johtaja

            bool valintaCheck = Int32.TryParse(Console.ReadLine(), out select);
            while (!valintaCheck)
            {
                Console.WriteLine("Syötä valinta");
                valintaCheck = Int32.TryParse(Console.ReadLine(), out select);
            }

            if (select == 1)
            {
                duunari = new Duunari();
            }
            if (select == 2)
            {
                duunari = new Johtaja();
            }
            if (select == 3)
            {
                AlkuMenu();
            }
            if (duunari.GetType() == typeof(Johtaja)) // Jos Johtaja, niin annetaan esimiehen rooli
            {
                Console.WriteLine("Anna esimiehen rooli:");
                string rooli = Console.ReadLine();
                (duunari as Johtaja).Rooli = rooli;

            }


            // Annetaan etunimi
            string etunimi = null;

           
            while (String.IsNullOrWhiteSpace(etunimi))
            {
                Console.WriteLine("Syötä työntekijän etunimi:");
                etunimi = Console.ReadLine();
            }


            duunari.Etunimi = etunimi;
            //Annetaan sukunimi
            string sukunimi = null;


            while (String.IsNullOrWhiteSpace(sukunimi))
            {
                Console.WriteLine("Syötä työntekijän sukunimi:");
                sukunimi = Console.ReadLine();
            }


            duunari.Sukunimi = sukunimi;
            // Annetaan työtunnit
            int tyotunnit;

            Console.WriteLine("Anna työtunnit");
            bool palkkaCheck = Int32.TryParse(Console.ReadLine(), out tyotunnit);
            while (!palkkaCheck)
            {
                Console.WriteLine("Syötä työtunnit numeroina");
                palkkaCheck = Int32.TryParse(Console.ReadLine(), out tyotunnit);
            }

            duunari.Tyotunnit = tyotunnit;
            // Muodostetaan työtunneista palkka tyypin mukaan 
            float palkka;
           
            if (duunari.GetType() == typeof(Johtaja)) 
            {
                palkka = tyotunnit * 10;

            } else 
            {
                palkka = tyotunnit * 5;
            }
           
            duunari.Palkka = palkka;
            NaytaYksiTyontekija(duunari);
            return duunari;



        }

        // Luodaan numerotaulukko for loopilla
        public Duunari ValitsePoistettavaTyontekija(List<Duunari> duunari)
        {
            for (int i = 0; i < duunari.Count; i++)
            {
                Console.WriteLine($"ID:{i + 1}. Etunimi: {duunari[i].Etunimi} Sukunimi: {duunari[i].Sukunimi} Työtunnit: {duunari[i].Tyotunnit} Palkka:{duunari[i].Palkka}");
            }
            Console.WriteLine("Valitse poistettavan työntekijän ID");
            
            int valinta;

            bool poistoCheck = Int32.TryParse(Console.ReadLine(), out valinta);
            while (!poistoCheck)
            {
                Console.WriteLine("Syötä poistettavan id numeroina");
                poistoCheck = Int32.TryParse(Console.ReadLine(), out valinta);
            }
         
            return duunari[valinta - 1];


        }

        // Tulostetaan yhden työntekijän tiedot
        public void NaytaYksiTyontekija(Duunari duunari)
        {
            Console.WriteLine($"Etunimi:{duunari.Etunimi} Sukunimi:{duunari.Sukunimi} Työtunnit:{duunari.Tyotunnit} Palkka:{duunari.Palkka}");
            Type type = duunari.GetType();
            if (type == typeof(Johtaja))
            {
                Johtaja boss = (Johtaja)duunari;
                Console.WriteLine($"Rooli: {boss.Rooli}");
            }

        }
    }
}
