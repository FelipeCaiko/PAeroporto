using PAeroporto.Models;
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
                    using (SqlDataReader reader = sqlSelectOne.ExecuteReader())
                    {
                        Console.WriteLine("\nPassageiro Localizado: ");
                        while (reader.Read())
                        {
                            Console.WriteLine("CPF: {0}", reader.GetString(0));
                            Console.WriteLine("Nome: {0}", reader.GetString(1));
                            Console.WriteLine("Data de Nascimento: {0}", reader.GetDateTime(2));
                            Console.WriteLine("Sexo: {0}", reader.GetString(3));
                            Console.WriteLine("Data da Última Compra: {0}", reader.GetDateTime(4));
                            Console.WriteLine("Data do Cadastro: {0}", reader.GetDateTime(5));
                            Console.WriteLine("Situação do Passageiro: {0}\n", reader.GetString(6));
                        }
                    }
                    conn.Close();
                    break;
                case 3:
                    using (SqlDataReader reader = sqlSelectOne.ExecuteReader())
                    {
                        Console.WriteLine("\nAeronave Localizada: ");
                        while (reader.Read())
                        {
                            Console.WriteLine("Inscrição: {0}", reader.GetString(0));
                            Console.WriteLine("CNPJ da Companhia da Aeronave: {0}", reader.GetString(1));
                            Console.WriteLine("Capacidade de Pessoas: {0}", reader.GetInt32(2));
                            Console.WriteLine("Data da Última Venda: {0}", reader.GetDateTime(3));
                            Console.WriteLine("Data do Cadastro: {0}", reader.GetDateTime(4));
                            Console.WriteLine("Situação da Aeronave: {0}\n", reader.GetString(5));
                        }
                    }
                    conn.Close();
                    break;
                case 4:
                    using (SqlDataReader reader = sqlSelectOne.ExecuteReader())
                    {
                        Console.WriteLine("\nVoo Localizado: ");
                        while (reader.Read())
                        {
                            Console.WriteLine("ID do Voo: {0}", reader.GetString(0));
                            Console.WriteLine("Inscrição da Aeronave: {0}", reader.GetString(1));
                            Console.WriteLine("Data do Cadastro: {0}", reader.GetDateTime(2));
                            Console.WriteLine("Data do Voo: {0}", reader.GetDateTime(3));
                            Console.WriteLine("Destino do Voo: {0}", reader.GetString(4));
                            Console.WriteLine("Assentos Ocupados: {0}", reader.GetInt32(5));
                            Console.WriteLine("Situação do Voo: {0}\n", reader.GetString(6));
                        }
                    }
                    conn.Close();
                    break;
                case 5:
                    using (SqlDataReader reader = sqlSelectOne.ExecuteReader())
                    {
                        Console.WriteLine("\nPassagem Localizada: ");
                        while (reader.Read())
                        {
                            Console.WriteLine("ID da Passagem: {0}", reader.GetString(0));
                            Console.WriteLine("ID do Voo: {0}", reader.GetString(1));
                            Console.WriteLine("Data da Ultima Operação: {0}", reader.GetDateTime(2));
                            Console.WriteLine("Valor da Passagem: {0}", reader.GetFloat(3));
                            Console.WriteLine("Situação da Passagem: {0}\n", reader.GetString(4));
                        }
                    }
                    conn.Close();
                    break;
                case 6:
                    using (SqlDataReader reader = sqlSelectOne.ExecuteReader())
                    {
                        Console.WriteLine("\nVenda Localizada: ");
                        while (reader.Read())
                        {
                            Console.WriteLine("ID da Venda: {0}", reader.GetInt32(0));
                            Console.WriteLine("Data da Venda: {0}", reader.GetDateTime(1));
                            Console.WriteLine("Valor Total da Venda: {0}", reader.GetFloat(2));
                            Console.WriteLine("CPF do Passageiro: {0}\n", reader.GetString(3));
                        }
                    }
                    conn.Close();
                    break;
                case 7:
                    using (SqlDataReader reader = sqlSelectOne.ExecuteReader())
                    {
                        Console.WriteLine("\nItem venda Localizado: ");
                        while (reader.Read())
                        {
                            Console.WriteLine("ID do Item Venda: {0}", reader.GetInt32(0));
                            Console.WriteLine("ID da Passagem: {0}", reader.GetString(1));
                            Console.WriteLine("ID da Venda: {0}", reader.GetInt32(2));
                            Console.WriteLine("Valor Unitário: {0}\n", reader.GetFloat(3));
                        }
                    }
                    conn.Close();
                    break;
                case 8:
                    using (SqlDataReader reader = sqlSelectOne.ExecuteReader())
                    {
                        Console.WriteLine("\nIatas Localizadas: ");
                        while (reader.Read())
                        {
                            Console.WriteLine("Sigla da Iata: {0}", reader.GetString(0));
                            Console.WriteLine("Descrição da Iata: {0}\n", reader.GetString(1));
                        }
                    }
                    conn.Close();
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

        public CompanhiaAerea VerifyReturnCA(string sql)
        {
            conn.Open();

            SqlCommand sqlVerify = conn.CreateCommand();
            sqlVerify.CommandText = sql;
            sqlVerify.Connection = conn;

            using (SqlDataReader reader = sqlVerify.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CompanhiaAerea companhiaAerea = new CompanhiaAerea();
                        companhiaAerea.CNPJ = reader.GetString(0);
                        companhiaAerea.RazaoSocial = reader.GetString(1);
                        companhiaAerea.DataAbertura = reader.GetDateTime(2);
                        companhiaAerea.DataCadastro = reader.GetDateTime(3);
                        companhiaAerea.UltimoVoo = reader.GetDateTime(4);
                        companhiaAerea.Situacao = reader.GetString(5);

                        conn.Close();
                        return companhiaAerea;
                    }
                }
                conn.Close();
                return null;
            }
        }
        public Aeronave VerifyReturnAN(string sql)
        {
            conn.Open();

            SqlCommand sqlVerify = conn.CreateCommand();
            sqlVerify.CommandText = sql;
            sqlVerify.Connection = conn;

            using (SqlDataReader reader = sqlVerify.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Aeronave aeronave = new Aeronave();
                        aeronave.Inscricao = reader.GetString(0);
                        aeronave.CNPJCompanhia.CNPJ = reader.GetString(1);
                        aeronave.Capacidade = reader.GetInt32(2);
                        aeronave.UltimaVenda = reader.GetDateTime(3);
                        aeronave.DataCadastro = reader.GetDateTime(4);
                        aeronave.Situacao = reader.GetString(5);

                        conn.Close();
                        return aeronave;
                    }
                }
            }
            conn.Close();
            return null;
        }
    }
}
