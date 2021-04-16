using curse.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Persistance
{
    public class RepoPilot : IRepoPilot
    {
        public RepoPilot()
        {

            Console.WriteLine("Creating RepoPilot");
        }

        public void Add(Pilot entity)
        {
            {

                IDbConnection connection = DBUtils.getConnection();
                using (var comm = connection.CreateCommand())
                {
                    comm.CommandText = "insert into Pilot(ID,Nume,NumeEchipa) values (@id,@nume,@numeEchipa)";
                    IDbDataParameter param1 = comm.CreateParameter();
                    param1.ParameterName = "@id";
                    param1.Value = entity.Id;

                    comm.Parameters.Add(param1);


                    IDbDataParameter param2 = comm.CreateParameter();
                    param2.ParameterName = "@nume";
                    param2.Value = entity.nume;

                    comm.Parameters.Add(param2);

                    IDbDataParameter param3 = comm.CreateParameter();
                    param3.ParameterName = "@numeEchipa";
                    param3.Value = entity.numeEchipa;
                    comm.Parameters.Add(param3);

                    var result = comm.ExecuteNonQuery();
                    if (result == 0)
                        Console.WriteLine("save exited with value {0}", result);
                    else
                        Console.WriteLine("save exited with value {0}", result);


                }
            }
        }

        public void Delete(long id)
        {
            {
                Console.WriteLine("Entering delete with value {0}", id);
                IDbConnection connection = DBUtils.getConnection();
                using (var comm = connection.CreateCommand())
                {
                    comm.CommandText = "delete from Pilot where id=@id";
                    IDbDataParameter paramId = comm.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = id;
                    comm.Parameters.Add(paramId);

                    var result = comm.ExecuteNonQuery();
                    if (result == 0)
                        Console.WriteLine("delete exited with value {0}", result);
                    else
                        Console.WriteLine("delete exited with value {1}", result);
                }
                Console.WriteLine("Exiting delete");
            }
        }

        public IEnumerable<Pilot> FindByNume(string nume)
        {
            Console.WriteLine("Entering findbyUsername with value {0}", nume);
            IDbConnection connection = DBUtils.getConnection();
            List<Pilot> users = new List<Pilot>();
            using (var comm = connection.CreateCommand())

            {
                comm.CommandText = "select * from Pilot where Nume=@nume";
                IDbDataParameter user = comm.CreateParameter();
                user.ParameterName = "@nume";
                user.Value = nume;
                comm.Parameters.Add(user);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        string Nume = dataR.GetString(1);
                        string numeEchipa = dataR.GetString(2);
                       Pilot u = new Pilot(Nume, numeEchipa);
                        u.Id = id;
                        users.Add(u);
                    }
                }

            }
            Console.WriteLine("Entering findbyUsername with value {0}", users);
            return users;
        }

        public IEnumerable<Pilot> FindByNumeEchipa(string numeEchipa)
        {
            Console.WriteLine("Entering findbyechipa with value {0}", numeEchipa);
            IDbConnection connection = DBUtils.getConnection();
            List<Pilot> users = new List<Pilot>();
            using (var comm = connection.CreateCommand())

            {
                comm.CommandText = "select * from Pilot where numeEchipa=@numeEchipa";
                IDbDataParameter user = comm.CreateParameter();
                user.ParameterName = "@numeEchipa";
                user.Value = numeEchipa;
                comm.Parameters.Add(user);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        string Nume = dataR.GetString(1);
                        string NumeEchipa = dataR.GetString(2);
                        Pilot u = new Pilot(Nume, NumeEchipa);
                        u.Id = id;
                        users.Add(u);
                    }
                }

            }
            Console.WriteLine("Entering findbyechipa with value {0}", users);
            return users;
        }

        public Pilot FindOne(long id)
        {
            Console.WriteLine("Entering findOne with value {0}", id);
            IDbConnection connection = DBUtils.getConnection();
            List<Pilot> users = new List<Pilot>();
            using (var comm = connection.CreateCommand())

            {
                comm.CommandText = "select * from Pilot where id=@id";
                IDbDataParameter idi = comm.CreateParameter();
                idi.ParameterName = "@id";
                idi.Value = id;
                comm.Parameters.Add(idi);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {

                        int idd = dataR.GetInt32(0);
                        string username = dataR.GetString(1);
                        string password = dataR.GetString(2);
                        Pilot u = new Pilot(username, password);
                        u.Id = idd;
                        Console.WriteLine("Entering findOne with value {0}", u);
                        return u;
                    }
                }
            }
            Console.WriteLine("Exiting findOne with value {0}", null);
            return null;
        }

        public IEnumerable<Pilot> GetAll()
        {
            Console.WriteLine("Entering findAll with value {0}");
            IDbConnection connection = DBUtils.getConnection();
            List<Pilot> users = new List<Pilot>();
            using (var comm = connection.CreateCommand())

            {
                comm.CommandText = "select * from Pilot";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        string username = dataR.GetString(1);
                        string password = dataR.GetString(2);
                        Pilot u = new Pilot(username, password);
                        u.Id = id;
                        users.Add(u);
                    }
                }

            }
            Console.WriteLine("Entering findAll with value {0}", users);
            return users;

        }
    }
    }

