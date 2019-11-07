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
        static string defaultWhereFirst = $" WHERE tbl_film.Type = tbl_type.id AND tbl_film.Producer = tbl_producer.id AND tbl_film.Country = tbl_country.id AND tbl_film.Genre = tbl_genre.id AND tbl_film.AgeRate = tbl_agerate.id AND tbl_film.Type = ";

        static public List<Film> GetFilm(string type)
        {
            List<Film> result = new List<Film>();
            string queryMain = defaultSelect + defaultFrom + defaultWhereFirst + type;
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(queryMain, connection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                    while (dr.Read())
                        result.Add(new Film
                        {
                            Id = dr.GetInt32("id"),
                            Name = dr.GetString("Name"),
                            Producer = new Dick() { ID = dr.GetInt32("ProducerID"), Name = dr.GetString("Producer") },
                            Genre = new Dick() { ID = dr.GetInt32("GenreID"), Name = dr.GetString("Genre") },
                            Year = dr.GetString("Year"),
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

        public void IncertFilm(Film film)
        {
            film.Id = ShowNextID("tbl_film");
            string query = "insert into tbl_film value(0,@b,@c,@d,@e,@f,@g,@h,@i,@j)";
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, connection))
                {
                    MySqlParameter p = new MySqlParameter("@b", MySqlDbType.String);
                    p.Value = film.Name;
                    mc.Parameters.Add(p);

                    p = new MySqlParameter("@c", MySqlDbType.Int32);
                    p.Value = film.Producer.ID;
                    mc.Parameters.Add(p);


                    p = new MySqlParameter("@d", MySqlDbType.String);
                    p.Value = film.Year;
                    mc.Parameters.Add(p);

                    p = new MySqlParameter("@e", MySqlDbType.Int32);
                    p.Value = film.Genre.ID;
                    mc.Parameters.Add(p);


                    p = new MySqlParameter("@f", MySqlDbType.Int32);
                    p.Value = film.Country.ID;
                    mc.Parameters.Add(p);


                    p = new MySqlParameter("@g", MySqlDbType.Int32);
                    p.Value = film.AgeRate.ID;
                    mc.Parameters.Add(p);

                    p = new MySqlParameter("@h", MySqlDbType.String);
                    p.Value = film.Score;
                    mc.Parameters.Add(p);

                    p = new MySqlParameter("@i", MySqlDbType.String);
                    p.Value = film.Comment;
                    mc.Parameters.Add(p);

                    p = new MySqlParameter("@j", MySqlDbType.Int32);
                    p.Value = film.Type.ID;
                    mc.Parameters.Add(p);

                    mc.ExecuteNonQuery();
                }
            }
            CloseConnection();
        }

        public int UpdateFilm(Film film)
        {
            int id = -1;
            string check = $"select tbl_film.id from tbl_film where tbl_film.id ={film.Id}";
            string query = $"update tbl_film set Name = @b, Producer = @c,Year = @d, Genre = @e, Country = @f, AgeRate = @g,Score = @h,Comment = @i,Type = @j where id={film.Id}";
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(check, connection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                    while (dr.Read())
                        id = dr.GetInt32("id");

                if (id > -1)
                    using (MySqlCommand mc = new MySqlCommand(query, connection))
                    {
                        MySqlParameter p = new MySqlParameter("@b", MySqlDbType.String);
                        p.Value = film.Name;
                        mc.Parameters.Add(p);

                        p = new MySqlParameter("@c", MySqlDbType.Int32);
                        p.Value = film.Producer.ID;
                        mc.Parameters.Add(p);


                        p = new MySqlParameter("@d", MySqlDbType.String);
                        p.Value = film.Year;
                        mc.Parameters.Add(p);

                        p = new MySqlParameter("@e", MySqlDbType.Int32);
                        p.Value = film.Genre.ID;
                        mc.Parameters.Add(p);


                        p = new MySqlParameter("@f", MySqlDbType.Int32);
                        p.Value = film.Country.ID;
                        mc.Parameters.Add(p);


                        p = new MySqlParameter("@g", MySqlDbType.Int32);
                        p.Value = film.AgeRate.ID;
                        mc.Parameters.Add(p);

                        p = new MySqlParameter("@h", MySqlDbType.String);
                        p.Value = film.Score;
                        mc.Parameters.Add(p);

                        p = new MySqlParameter("@i", MySqlDbType.String);
                        p.Value = film.Comment;
                        mc.Parameters.Add(p);

                        p = new MySqlParameter("@j", MySqlDbType.Int32);
                        p.Value = film.Type.ID;
                        mc.Parameters.Add(p);

                        mc.ExecuteNonQuery();

                        CloseConnection();
                        return 0;
                    }
                else
                {
                    System.Windows.MessageBox.Show("Freeman YOU FOOL \nPlease reload db");
                    CloseConnection();
                    return 1;
                }

            }


            return 1;


        }


        public void RemoveFilm(Film film)
        {
            string query = $"delete from tbl_film where id={film.Id}";
            ExecureNonQuery(query);
        }


        static public List<Film> FindFilm(string type, string where)
        {
            List<Film> result = new List<Film>();
            string queryMain = defaultSelect + defaultFrom + defaultWhereFirst + type + where;
            if (OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(queryMain, connection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                    while (dr.Read())
                        result.Add(new Film
                        {
                            Id = dr.GetInt32("id"),
                            Name = dr.GetString("Name"),
                            Producer = new Dick() { ID = dr.GetInt32("ProducerID"), Name = dr.GetString("Producer") },
                            Genre = new Dick() { ID = dr.GetInt32("GenreID"), Name = dr.GetString("Genre") },
                            Year = dr.GetString("Year"),
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
