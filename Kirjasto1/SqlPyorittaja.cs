using System;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using Kirjasto1;

namespace Kirjasto1
{
   public class SqlPyorittaja : DataAcces
    {
       
        // Lisätään tietokantaan uusi työntekijä
        public int LisaaTyontekija(Duunari duunari)
        {
            string str = DataAcces.CnnVal(DataAcces.currentDBname);
            int duunariId;
            using (IDbConnection connection = new SqlConnection(str))
            {
                duunariId = connection.QuerySingle<int>("INSERT dbo.Duunari  (etunimi,sukunimi,palkka,tyotunnit) OUTPUT inserted.id VALUES (@Etunimi, @Sukunimi,@Palkka,@Tyotunnit)", new { etunimi = duunari.Etunimi, sukunimi = duunari.Sukunimi, palkka = duunari.Palkka, tyotunnit = duunari.Tyotunnit });

                return duunariId;
            }

        }
        // Näytetään tietokannasta kaikki työntekijät
        public List<Duunari> KaikkiTyontekijat()
        {
            
            string str = DataAcces.CnnVal(DataAcces.currentDBname);
            using (IDbConnection connection = new SqlConnection(str))
            {
                var toReturn = connection.Query<Duunari>("SELECT * FROM dbo.Duunari").ToList();
                return toReturn;
            }
        }

        // Poistetaan tietokannasta työntekijä
        public void Poistatyontekija(int id)
        {
            string str = DataAcces.CnnVal(DataAcces.currentDBname);
            using (IDbConnection connection = new SqlConnection(str))
            {
                connection.Execute("DELETE from dbo.Duunari WHERE Id=@id", new { id = id });
            }


        }

    }
}



