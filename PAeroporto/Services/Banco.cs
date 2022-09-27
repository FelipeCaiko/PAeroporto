using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Services
{
    internal class Banco
    {
        public string Conexao = "Data Source=localhost;Initial Catalog=OnTheFly;User Id=sa;Password=LiipeCaiko3030;";
        SqlConnection conn;

        public Banco()
        {
            conn = new SqlConnection(Conexao);
        }

        public void Add(string sql)
        {
            conn.Open();
            SqlCommand sqlInsert = new SqlCommand(sql, conn);
            sqlInsert.ExecuteNonQuery();
            conn.Close();
        }

        public void Update(string sql)
        {
            conn.Open();
            SqlCommand sqlInsert = new SqlCommand(sql, conn);
            sqlInsert.ExecuteNonQuery();
            conn.Close();
        }

        public void Select(string sql, int op)
        {
            conn.Open();

            SqlCommand sqlSelectOne = new SqlCommand(sql, conn);

            switch (op)
            {
                case 1:
                    using (SqlDataReader reader = sqlSelectOne.ExecuteReader())
                    {
                        Console.WriteLine("\nCompanhia Aérea Localizada: ");
                        while (reader.Read())
                        {
                            Console.WriteLine("CNPJ: {0}", reader.GetString(0));
                            Console.WriteLine("Razão Social: {0}", reader.GetString(1));
                            Console.WriteLine("Data de Abertura: {0}", reader.GetDateTime(2));
                            Console.WriteLine("Data do Cadastro: {0}", reader.GetDateTime(3));
                            Console.WriteLine("Ultimo Voo: {0}", reader.GetDateTime(4));
                            Console.WriteLine("Situação da Companhia: {0}\n", reader.GetString(5));
                        }
                    }
                    conn.Close();
                    break;
                case 2:
                    Console.WriteLine("Passageiro Localizado: ");
                    break;
                case 3:
                    Console.WriteLine("Aeronave Localizada: ");
                    break;
                case 4:
                    Console.WriteLine("Voo Localizado: ");
                    break;
                case 5:
                    Console.WriteLine("Passagem Localizada: ");
                    break;
                case 6:
                    Console.WriteLine("Venda Localizada: ");
                    break;
                case 7:
                    Console.WriteLine("Item venda Localizado: ");
                    break;
            }
        }
        public int Verify(string sql)
        {
            conn.Open();
            int count = 0;

            SqlCommand sqlVerify = conn.CreateCommand();
            sqlVerify.CommandText = sql;
            sqlVerify.Connection = conn;

            using (SqlDataReader reader = sqlVerify.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                }
            }
            if (count != 0)
            {
                conn.Close();
                return 1;
            }
            conn.Close();
            return 0;
        }
    }
}
