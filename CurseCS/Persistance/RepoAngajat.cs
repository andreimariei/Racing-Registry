using curse.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Persistance
{

    public class RepoAngajat : IRepoAngajat
    {
        public RepoAngajat()
        {
            Console.WriteLine("Creating RepoAngajat");

        }

        public void Add(Angajat entity)
        {
            {
                Console.WriteLine("Entering save with object {0}", entity);
                IDbConnection connection = DBUtils.getConnection();
                using (var comm = connection.CreateCommand())
                {
                    comm.CommandText = "insert into Angajati(ID,PasswordA,UsernameA) values (@id,@password,@username)";
                    IDbDataParameter param1 = comm.CreateParameter();
                    param1.ParameterName = "@id";
                    param1.Value = entity.Id;

                    comm.Parameters.Add(param1);


                    IDbDataParameter param2 = comm.CreateParameter();
                    param2.ParameterName = "@password";
                    param2.Value = entity.Password;

                    comm.Parameters.Add(param2);

                    IDbDataParameter param3 = comm.CreateParameter();
                    param3.ParameterName = "@username";
                    param3.Value = entity.Username;
                    comm.Parameters.Add(param3);

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
                    comm.CommandText = "delete from Angajati where ID=@id";
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


        public Angajat FindByUsername(string username)
        {
            Console.Write("Entering findbyUsername with value {0}", username);
            IDbConnection connection = DBUtils.getConnection();
            using (var comm = connection.CreateCommand())

            {
                comm.CommandText = "select * from Angajati where UsernameA=@username";
                IDbDataParameter user = comm.CreateParameter();
                user.ParameterName = "@username";
                user.Value = username;
                comm.Parameters.Add(user);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        string Password = dataR.GetString(2);
                        Angajat u = new Angajat(username, Password);
                        u.Id = id;
                        Console.WriteLine("Entering findbyUsername with value {0}", u);
                        return u;
                    }
                }

            }
            Console.WriteLine("Exiting FindByUsername with value {0}", null);

            connection.Close();
            return null;


        }

        public Angajat FindOne(long id)
        {
            Console.WriteLine("Entering findOne with value {0}", id);
            IDbConnection connection = DBUtils.getConnection();
            List<Angajat> users = new List<Angajat>();
            using (var comm = connection.CreateCommand())

            {
                comm.CommandText = "select * from Angajati where id=@id";
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
                        Angajat u = new Angajat(username, password);
                        u.Id = idd;
                        Console.WriteLine("Entering findOne with value {0}", u);
                        return u;
                    }
                }
            }
            Console.WriteLine("Exiting findOne with value {0}", null);
            return null;
        }

        public IEnumerable<Angajat> GetAll()
        {
            Console.WriteLine("Entering findAll with value {0}");
            IDbConnection connection = DBUtils.getConnection();
            List<Angajat> users = new List<Angajat>();
            using (var comm = connection.CreateCommand())

            {
                comm.CommandText = "select * from Angajati";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        string username = dataR.GetString(1);
                        string password = dataR.GetString(2);
                        Angajat u = new Angajat(username, password);
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
