using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace db_Film.Model
{
    class FilmSQL : MySQL
    {
        static string defaultSelect = $"SELECT tbl_film.id, tbl_film.`Name`, tbl_producer.`Name` AS Producer, tbl_producer.id As ProducerID,  tbl_genre.`Name` AS Genre, tbl_genre.id as GenreID, tbl_film.`Year`, tbl_country.`Name` AS Country, tbl_country.id as CountryID, tbl_agerate.`Name` AS AgeRate, tbl_agerate.id As AgeRateID, tbl_film.Score, tbl_film.`Comment`, tbl_type.`Name` AS Type, tbl_type.id As TypeID";
        static string defaultFrom = $" FROM tbl_film, tbl_type, tbl_producer, tbl_genre, tbl_country, tbl_agerate";
        static string defaultWhereFirst = $" WHERE tbl_film.Producer = tbl_producer.id AND tbl_film.Type = tbl_type.id AND tbl_film.Type = ";
        static string defaultWhereSecond = $" AND tbl_film.Country = tbl_country.id AND tbl_film.Genre = tbl_genre.id AND tbl_film.AgeRate = tbl_agerate.id";

       static public List<Film> GetFilm(string type)
       {
            List<Film> result = new List<Film>();
            string queryMain = defaultSelect + defaultFrom + defaultWhereFirst + type + defaultWhereSecond;
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(queryMain, connection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                    while (dr.Read())
                        result.Add(new Film
                        {
                            Id = dr.GetInt32("id"),
                            Name = dr.GetString("Name"),
                            Producer = new Dick() {ID = dr.GetInt32("ProducerID"),Name = dr.GetString("Producer") },
                            Genre = new Dick() { ID = dr.GetInt32("GenreID"), Name = dr.GetString("Genre") },
                            Year= dr.GetString("Year"),
                            Country = new Dick() { ID = dr.GetInt32("CountryID"), Name = dr.GetString("Country") },
                            AgeRate = new Dick() { ID = dr.GetInt32("AgeRateID"), Name = dr.GetString("AgeRate") },
                            Score = dr.GetString("Score"),
                            Comment = dr.GetString("Comment"),
                            Type = new Dick() { ID = dr.GetInt32("TypeID"), Name = dr.GetString("Type") }

                        });
                CloseConnection();
            }
            return result;
       }

    }
}
