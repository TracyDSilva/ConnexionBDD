using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ConnexionBDD
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connexion
            string sCnx = "user=root; password=siojjr; database=basePersonne; host=localhost";
            MySqlConnection connexion = new MySqlConnection(sCnx);

            //Ouverture
            connexion.Open();

            //Insert
            Console.WriteLine("Insertion d'une personne");
            Console.WriteLine("Saisissez l'id, le nom et l'age");
            int id = Convert.ToInt32(Console.ReadLine());
            string nom = Console.ReadLine();
            int age = Convert.ToInt32(Console.ReadLine());

            string sCmd = "insert into personne(id,nom,age) values(@id,@nom,@age)";
            MySqlCommand cmd = new MySqlCommand(sCmd, connexion);

            MySqlParameter pId = new MySqlParameter("@id", id);
            MySqlParameter pNom = new MySqlParameter("@nom", nom);
            MySqlParameter pAge = new MySqlParameter("@age", age);

            cmd.Parameters.Add(pId);
            cmd.Parameters.Add(pNom);
            cmd.Parameters.Add(pAge);

            cmd.ExecuteNonQuery();

            //Update
            Console.WriteLine();
            Console.WriteLine("Modifiation d'une personne:");
            Console.WriteLine("Saisissez l'id de la personne à modifier:");

            int idUpdate = Convert.ToInt32(Console.ReadLine());
            string nomUpdate = Console.ReadLine();
            int ageUpdate = Convert.ToInt32(Console.ReadLine());

            string sCmdUpdate = "update personne set nom=@nom, age=@age where id=@id";
            MySqlCommand cmdUpdate = new MySqlCommand(sCmdUpdate, connexion);

            MySqlParameter pIdUpdate = new MySqlParameter("@id", idUpdate);
            MySqlParameter pNomUpdate = new MySqlParameter("@nom", nomUpdate);
            MySqlParameter pAgeUpdate = new MySqlParameter("@age", ageUpdate);

            cmdUpdate.Parameters.Add(pIdUpdate);
            cmdUpdate.Parameters.Add(pNomUpdate);
            cmdUpdate.Parameters.Add(pAgeUpdate);

            cmdUpdate.ExecuteNonQuery();

            //Delete
            Console.WriteLine();
            Console.WriteLine("Suppression d'une personne");
            Console.WriteLine("Saisissez l'id à supprimer:");
            int idDelete = Convert.ToInt32(Console.ReadLine());

            string sCmdDelete = "delete from personne where @id=id";
            MySqlCommand cmdDelete = new MySqlCommand(sCmdDelete, connexion);
            MySqlParameter pIdDelete = new MySqlParameter("@id", idDelete);
            cmdDelete.Parameters.Add(pIdDelete);

            cmdDelete.ExecuteNonQuery();

            //Count
            Console.WriteLine();
            string sCmdCount = "select count(*) from personne";

            MySqlCommand cmdCount = new MySqlCommand(sCmdCount, connexion);
            int count = Convert.ToInt32(cmdCount.ExecuteScalar());
            Console.WriteLine("Il y a {0} enregistrements dans la table personne.", count);

            cmdCount.ExecuteNonQuery();

            //Select
            Console.WriteLine();
            MySqlCommand cmdSelect = new MySqlCommand("select id,nom,age from personne", connexion);
            MySqlDataReader reader = cmdSelect.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(reader["id"]);
                Console.WriteLine(reader["nom"]);
                Console.WriteLine(reader["age"]);
            }
            reader.Close();

            Console.ReadLine();
            //Fermeture
            connexion.Close();
        }
    }
}
