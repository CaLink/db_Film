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
        static string defaultSelect = $"SELECT tbl_film.id, tbl_film.`Name`, tbl_producer.`Name` AS Producer, tbl_genre.`Name` AS Genre, tbl_film.`Year`, tbl_country.`Name` AS Country, tbl_agerate.`Name` AS AgeRate, tbl_film.Score, tbl_film.`Comment`, tbl_type.`Name` AS Type";
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
                            Producer = dr.GetString("Producer"),
                            Genre = dr.GetString("Genre"),
                            Year= dr.GetString("Year"),
                            Country = dr.GetString("Country"),
                            AgeRate = dr.GetString("AgeRate"),
                            Score = dr.GetString("Score"),
                            Comment = dr.GetString("Comment"),
                            Type = dr.GetString("Type")

                        });
                CloseConnection();
            }
            return result;
       }

    }
}
