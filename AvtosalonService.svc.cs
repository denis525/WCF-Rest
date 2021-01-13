using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Serialization;
using WcfAvtosalonRest.Model;

namespace WcfAvtosalonRest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AvtosalonService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AvtosalonService.svc or AvtosalonService.svc.cs at the Solution Explorer and start debugging.
    public class AvtosalonService : IAvtosalonService
    {
        SqlConnection con = new SqlConnection("");

  
        List<Avto> avteki;
        List<Narocnik> narocniki;
        List<Izposoja> izposoje;
      
        
        string connstring = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\denis\\Desktop\\ORA_2\\WcfAvtosalonRest\\WcfAvtosalonRest\\App_Data\\DB.mdf;Integrated Security=True";

          public void addAvto(Avto avto)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connstring);
                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "INSERT INTO Avto (Id, Znamka, Model) VALUES (@Id, @Znamka, @Model)";
                comm.Parameters.Add("Id", System.Data.SqlDbType.VarChar).Value = avto.Id;
                comm.Parameters.Add("Znamka", System.Data.SqlDbType.VarChar).Value = avto.Ime;
                comm.Parameters.Add("Model", System.Data.SqlDbType.Int).Value = avto.Letnik;
                
                conn.Open();

                int dodanih = comm.ExecuteNonQuery();
                /*if (dodanih > 0) {
                    UriTemplateMatch match = WebOperationContext.Current.IncomingRequest.UriTemplateMatch;

                    UriTemplate template = new UriTemplate("/avto/{id}");
                    Uri novAvtoUri = template.BindByPosition(match.BaseUri, avto.Id);
                    WebOperationContext.Current.OutgoingResponse.SetStatusAsCreated(novAvtoUri);

                }*/
                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }



        public void updateAvto(string id, Avto avto)
        {
            /*XmlSerializer s = new XmlSerializer(typeof(List<Avto>));
            TextReader r = new StreamReader(xmlPath);
            avteki = (List<Avto>)s.Deserialize(r);
            r.Close();*/


            SqlConnection conn = new SqlConnection(connstring);
            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "UPDATE Avto SET Ime = @Ime, Letnik = @Letnik, StPrevozenihKm = @StPrevozenihKm WHERE Id = @Id";
            comm.Parameters.Add("Id", System.Data.SqlDbType.VarChar).Value = id;
            comm.Parameters.Add("Ime", System.Data.SqlDbType.VarChar).Value = avto.Ime;
            comm.Parameters.Add("Letnik", System.Data.SqlDbType.Int).Value = avto.Letnik;
            comm.Parameters.Add("StPrevozenihKm", System.Data.SqlDbType.Decimal).Value = avto.StPrevozenihKm;

            conn.Open();

            int dodanih = comm.ExecuteNonQuery();



            //avteki.Add(podatek);
        }

        public void deleteAvto(string id)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connstring);
                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "DELETE FROM Avto WHERE Id=@Id";
                comm.Parameters.Add("Id", System.Data.SqlDbType.VarChar).Value = id;

                conn.Open();

                int spremenjenih = comm.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Avto getAvto(string id)
        {
            return vrniAvto(int.Parse(id));
        }

        public List<Avto> getAllAvte()
        {
            List<Avto> avti = new List<Avto>();
            SqlConnection conn = new SqlConnection(connstring);
            using (conn)
            {
                SqlCommand comm = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * FROM Avto"
                };
                conn.Open();
                using (comm)
                {
                    SqlDataReader reader = comm.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            avti.Add(new Avto()
                            {
                                Id = reader["Id"].ToString(),
                                Ime = reader["Ime"].ToString(),
                                Letnik = int.Parse(reader["Letnik"].ToString()),
                                StPrevozenihKm = double.Parse(reader["StPrevozenihKm"].ToString())
                            });
                        }
                    }
                }
            }
            return avti;
        }

        /*public void updateAvto(string id, Avto avto)
        {
            setAvto(avto);
        }*/

        public bool CreateAvto(Avto avto)
        {
            SqlConnection conn = new SqlConnection(connstring);
            using (conn)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;

                comm.CommandText = "INSERT INTO Avto (Ime, Letnik, StPrevozenihKm) VALUES" +
                    "(@Ime, @Letnik, @StPrevozenihKm)";

                comm.Parameters.Add("Ime", System.Data.SqlDbType.VarChar).Value = avto.Ime;
                comm.Parameters.Add("Letnik", System.Data.SqlDbType.Int).Value = avto.Letnik;
                comm.Parameters.Add("StPrevozenihKm", System.Data.SqlDbType.Decimal).Value = avto.StPrevozenihKm;
           
            
                conn.Open();
                using (comm)
                {
                    int dodanih = comm.ExecuteNonQuery();
                    if (dodanih > 0)
                        return true;
                }
            }
            return false;
        }

        public Avto vrniAvto(int id)
        {
            SqlConnection conn = new SqlConnection(connstring);
            using (conn)
            {
                SqlCommand comm = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * FROM Avto WHERE Id = @id;"
                };
                comm.Parameters.Add("id", System.Data.SqlDbType.Int).Value = id;
                conn.Open();
                using (comm)
                {
                    SqlDataReader reader = comm.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            return new Avto()
                            {
                                Id = reader["Id"].ToString(),
                                Ime = reader["Ime"].ToString(),
                                Letnik = int.Parse(reader["Letnik"].ToString()),
                                StPrevozenihKm = double.Parse(reader["StPrevozenihKm"].ToString())
                            };
                        }
                    }
                }
                return new Avto();
            }
        }

        public Izposoja vrniIzposoje(int id)
        {
            SqlConnection conn = new SqlConnection(connstring);
            using (conn)
            {
                SqlCommand comm = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * FROM Izposoja WHERE Id = @id;"
                };
                comm.Parameters.Add("id", System.Data.SqlDbType.Int).Value = id;
                conn.Open();
                using (comm)
                {
                    SqlDataReader reader = comm.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            return new Izposoja()
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                DatumIzposoje = Convert.ToDateTime(reader["DatumIzposoje"]), // .ToString("yyyy-MM-dd"))
                                DatumVrnitve = Convert.ToDateTime(reader["DatumVrnitve"]),
                                StPrevozenihNarocnika = double.Parse(reader["StPrevozenihNarocnika"].ToString())
                            };
                        }
                    }
                }
                return new Izposoja();
            }
        }

        public void deleteIzposoja(string id)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connstring);
                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "DELETE FROM Izposoja WHERE Id=@Id";
                comm.Parameters.Add("Id", System.Data.SqlDbType.VarChar).Value = id;

                conn.Open();

                int spremenjenih = comm.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
        

        public bool createIzposoja(Izposoja izposoja)
        {
            SqlConnection conn = new SqlConnection(connstring);
            using (conn)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;

                comm.CommandText = "INSERT INTO Izposoja (Id, StPrevozenihNarocnika, DatumIzposoje, DatumVrnitve) VALUES" +
                    "(@Id, @StPrevozenihNarocnika, @DatumIzposoje, @DatumVrnitve)";

                comm.Parameters.Add("Id", System.Data.SqlDbType.Int).Value = izposoja.Id;
                comm.Parameters.Add("StPrevozenihNarocnika", System.Data.SqlDbType.Float).Value = izposoja.StPrevozenihNarocnika;
                comm.Parameters.Add("DatumIzposoje", System.Data.SqlDbType.Date).Value = izposoja.DatumIzposoje;
                comm.Parameters.Add("DatumVrnitve", System.Data.SqlDbType.Date).Value = izposoja.DatumVrnitve;


                conn.Open();
                using (comm)
                {
                    int dodanih = comm.ExecuteNonQuery();
                    if (dodanih > 0)
                        return true;
                }
            }
            return false;
        }

        public List<Izposoja> vrniIzposoje()
        {
            List<Izposoja> list = new List<Izposoja>();
            SqlConnection conn = new SqlConnection(connstring);
            using (conn)
            {
                SqlCommand comm = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * FROM Izposoja;"
                };
                conn.Open();
                using (comm)
                {
                    SqlDataReader reader = comm.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            list.Add(new Izposoja()
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                DatumIzposoje = Convert.ToDateTime(reader["DatumIzposoje"]), // .ToString("yyyy-MM-dd"))
                                DatumVrnitve = Convert.ToDateTime(reader["DatumVrnitve"]),
                                StPrevozenihNarocnika = double.Parse(reader["StPrevozenihNarocnika"].ToString())
                            });
                        }
                    }
                }
            }
            return list;
        }

        public Narocnik vrniNarocnike(int id)
        {
            SqlConnection conn = new SqlConnection(connstring);
            using (conn)
            {
                SqlCommand comm = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * FROM Narocnik WHERE Id = @id;"
                };
                comm.Parameters.Add("id", System.Data.SqlDbType.Int).Value = id;
                conn.Open();
                using (comm)
                {
                    SqlDataReader reader = comm.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            return new Narocnik()
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Ime = reader["Ime"].ToString(),
                                Priimek = reader["Priimek"].ToString()
                            };
                        }
                    }
                }
                return new Narocnik();
            }
        }


        public void deleteNarocnik(string id)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connstring);
                SqlCommand comm = conn.CreateCommand();
                comm.CommandText = "DELETE FROM Narocnik WHERE Id=@Id";
                comm.Parameters.Add("Id", System.Data.SqlDbType.VarChar).Value = id;

                conn.Open();

                int spremenjenih = comm.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Narocnik getNarocnik(string id)
        {
            return vrniNarocnike(int.Parse(id));
        }

        public void updateNarocnik(string id, Narocnik narocnik)
        {
            SqlConnection conn = new SqlConnection(connstring);

            SqlCommand comm = conn.CreateCommand();
            comm.CommandText = "UPDATE Narocnik SET Ime = @Ime, Priimek = @Priimek WHERE Id = @Id";
            comm.Parameters.Add("Id", System.Data.SqlDbType.Int).Value = id;
            comm.Parameters.Add("Ime", System.Data.SqlDbType.VarChar).Value = narocnik.Ime;
            comm.Parameters.Add("Priimek", System.Data.SqlDbType.VarChar).Value = narocnik.Priimek;

            conn.Open();

            int dodanih = comm.ExecuteNonQuery();
        }

        public bool createNarocnik(Narocnik narocnik)
        {
            SqlConnection conn = new SqlConnection(connstring);
            using (conn)
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;

                comm.CommandText = "INSERT INTO Narocnik (id, ime, priimek) VALUES" +
                    "(@id, @ime, @priimek)";

                comm.Parameters.Add("Id", System.Data.SqlDbType.Int).Value = narocnik.Id;
                comm.Parameters.Add("Ime", System.Data.SqlDbType.VarChar).Value = narocnik.Ime;
                comm.Parameters.Add("Priimek", System.Data.SqlDbType.VarChar).Value = narocnik.Priimek;


                conn.Open();
                using (comm)
                {
                    int dodanih = comm.ExecuteNonQuery();
                    if (dodanih > 0)
                        return true;
                }
            }
            return false;
        }

        public List<Narocnik> vrniNarocnike()
        {
            List<Narocnik> list = new List<Narocnik>();
            SqlConnection conn = new SqlConnection(connstring);
            using (conn)
            {
                SqlCommand comm = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT * FROM Narocnik WHERE Id = @id;"
                };
                comm.Parameters.Add("id", System.Data.SqlDbType.Int).Value = id;
                conn.Open();
                using (comm)
                {
                    SqlDataReader reader = comm.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            list.Add(new Narocnik()
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Ime = reader["Ime"].ToString(),
                                Priimek = reader["Priimek"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
        


        /*public void createAvto(string id)
        {
            throw new NotImplementedException();
        }*/
    }
}
