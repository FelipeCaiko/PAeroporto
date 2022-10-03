using PAeroporto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class PassagemVoo
    {
        public int ID { get; set; }
        public Voo IDVoo { get; set; }
        public DateTime DataUltimaOperacao { get; set; }
        public float Valor { get; set; }
        public string Situacao { get; set; }

        #region Listar Passagens Cadastradas
        public void ListarPassagens()
        {
            Banco banco = new Banco();
            Console.Clear();
            string sql = $"SELECT * FROM PassagemVoo;";
            banco.Select(sql, 5);
            Console.WriteLine("\nFim da Impressão de Passagens. Pressione ENTER para continuar!");
            Console.ReadKey();
        }
        #endregion

        #region Listar uma Passagem
        public void ListarIDPassagem(string id)
        {
            Banco banco = new Banco();

            if (id == null)
            {
                ID = id;
                do
                {
                    Console.Clear();
                    Console.Write("Informe 0 caso deseje sair. \nInforme o ID da Passagem que irá ser buscado: ");
                    ID = Console.ReadLine();
                    if (ID == "0")
                        break;

                    string sql = $"SELECT * FROM PassagemVoo WHERE ID = ('{ID}');";
                    int verificar = banco.Verify(sql);

                    if (verificar != 0)
                    {
                        banco = new Banco();
                        banco.Select(sql, 5);
                        Console.WriteLine("\nID da Passagem foi encontrado. Pressione ENTER para continuar!");
                        Console.ReadKey();
                        break;
                    }
                    if (verificar == 0)
                    {
                        Console.WriteLine("\nID da Passagem informado não foi encontrado! Pressione ENTER para continuar!");
                        Console.ReadKey();
                        Console.Clear();
                    }
                } while (true);
            }
            else
            {
                ID = id;
                string sql = $"SELECT * FROM CompanhiaAerea WHERE CNPJ = ('{ID}');";

                banco = new Banco();
                banco.Select(sql, 5);
            }
        }
        #endregion
    }
}
