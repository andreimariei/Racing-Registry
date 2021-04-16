using curse.Model;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse.Persistance
{
    public class RepoInscriere : IRepoInscriere

    {

        public RepoInscriere()
        {
            Console.WriteLine("Creating RepoInscriere");


        }
        public void Add(Inscriere entity)
        {
            {
                Console.WriteLine("Entering save with object {0}", entity);
                IDbConnection connection = DBUtils.getConnection();
                Console.WriteLine("udfjskgbksjdhkljhfkdsa");
                using (var comm = connection.CreateCommand())
                {
                    comm.CommandText = "insert into Inscrieri(IDPilot,IDCursa) values (@IDPilot,@IDCursa)";


                    IDbDataParameter param2 = comm.CreateParameter();
                    param2.ParameterName = "@IDPilot";
                    param2.Value = entity.Pilot;
                    comm.Parameters.Add(param2);

                    IDbDataParameter param3 = comm.CreateParameter();
                    param3.ParameterName = "@IDCursa";
                    param3.Value = entity.Cursa;
                    comm.Parameters.Add(param3);

                    Console.WriteLine("udfjskgbksjdhkljhfkdsa");

                    var result = comm.ExecuteNonQuery();
                    if (result == 0)
                        Console.WriteLine("save exited with value {0}", result);
                    else
                        Console.WriteLine("save exited with value {0}", result);


                }
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Inscriere FindOne(int id)
        {
            Console.WriteLine("Entering findOne with value {0}", id);
            IDbConnection connection = DBUtils.getConnection();
            using (var comm = connection.CreateCommand())

            {
                comm.CommandText = "select * from Inscrieri where IDPilot=@id";
                IDbDataParameter idi = comm.CreateParameter();
                idi.ParameterName = "@id";
                idi.Value = id;
                comm.Parameters.Add(idi);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {

                        int idd = dataR.GetInt32(0);
                        int IDPilot = dataR.GetInt32(1);
                        int IDCursa = dataR.GetInt32(2);
                        Inscriere u =new Inscriere(idd,IDPilot,IDCursa);
                        Console.WriteLine("Entering findOne with value {0}", u);
                        return u;
                    }
                }
            }
            Console.WriteLine("Exiting findOne with value {0}", null);
            return null;
        }

        public IEnumerable<Inscriere> GetAll()
        {
            Console.WriteLine("Entering findAll with value {0}");
            IDbConnection connection = DBUtils.getConnection();
            List<Inscriere> users = new List<Inscriere>();
            using (var comm = connection.CreateCommand())

            {
                comm.CommandText = "select * from Inscrieri";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        int pilot = dataR.GetInt32(1);
                        int cursa = dataR.GetInt32(2);
                        Inscriere u = new Inscriere(id, pilot, cursa);
                        users.Add(u);
                    }
                }

            }
            Console.WriteLine("Entering findAll with value {0}", users);
            return users;
        }
    }
}
