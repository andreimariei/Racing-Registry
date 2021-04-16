using curse.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Persistance
{
    public class RepoCursa : IRepoCursa
    {
        public RepoCursa()
        {
            Console.WriteLine("Creating RepoCursa");

        }

        public void Add(Cursa entity)
        {
            {
                Console.WriteLine("Entering save with object {0}", entity);
                IDbConnection connection = DBUtils.getConnection();
                using (var comm = connection.CreateCommand())
                {
                    comm.CommandText = "insert into Cursa(ID,Capacitate) values (@ID,@Capacitate)";
                    IDbDataParameter param1 = comm.CreateParameter();
                    param1.ParameterName = "@ID";
                    param1.Value = entity.Id;

                    comm.Parameters.Add(param1);


                    IDbDataParameter param2 = comm.CreateParameter();
                    param2.ParameterName = "@Capacitate";
                    param2.Value = entity.capacitate;

                    comm.Parameters.Add(param2);


                    var result = comm.ExecuteNonQuery();
                    if (result == 0)
                        Console.WriteLine("save exited with value {0}", result);
                    else
                        Console.WriteLine("save exited with value {1}", result);


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
                    comm.CommandText = "delete from Cursa where ID=@id";
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

        public Cursa FindByCapacitate(int capacitate)
        {
            Console.WriteLine("Entering findbycapacitate with value {0}", capacitate);
            IDbConnection connection = DBUtils.getConnection();
            List<Cursa> users = new List<Cursa>();
            using (var comm = connection.CreateCommand())

            {
                comm.CommandText = "select * from Cursa where Capacitate=@capacitate";
                IDbDataParameter user = comm.CreateParameter();
                user.ParameterName = "@capacitate";
                user.Value = capacitate;

                comm.Parameters.Add(user);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        int Capacitate = dataR.GetInt32(1);
                        Cursa u = new Cursa(Capacitate);
                        u.Id = id;
                        return u;
                    }
                }
               
            }
            Console.WriteLine("Exiting findbycapacitate with value {0}", capacitate);
            return null;
        }

        public Cursa FindOne(long id)
        {
            Console.WriteLine("Entering findOne with value {0}", id);
            IDbConnection connection = DBUtils.getConnection();
            List<Cursa> users = new List<Cursa>();
            using (var comm = connection.CreateCommand())

            {
                comm.CommandText = "select * from Cursa where ID=@id";
                IDbDataParameter idi = comm.CreateParameter();
                idi.ParameterName = "@id";
                idi.Value = id;
                comm.Parameters.Add(idi);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {

                        int idd = dataR.GetInt32(0);
                        int Capacitate = dataR.GetInt32(1);
                        Cursa u = new Cursa(Capacitate);
                        u.Id = idd;
                        Console.WriteLine("Entering findOne with value {0}", u);
                        return u;
                    }
                }
            }
            Console.WriteLine("Exiting findOne with value {0}", null);
            return null;
        }

        public IEnumerable<Cursa> GetAll()
        {
            Console.WriteLine("Entering findAll with value {0}");
            IDbConnection connection = DBUtils.getConnection();
            List<Cursa> users = new List<Cursa>();
            using (var comm = connection.CreateCommand())

            {
                comm.CommandText = "select * from Cursa";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        int Capacitate = dataR.GetInt32(1);
                        Cursa u = new Cursa(Capacitate);
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
