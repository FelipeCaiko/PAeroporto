using PAeroporto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class Bloqueados
    {
        public string CNPJ { get; set; }

        public Bloqueados()
        {
        }

        #region Inserir Companhia na lista de Bloqueados
        public void InserirBloqueado()
        {
            CompanhiaAerea companhiaAerea = new CompanhiaAerea();
            bool Validacao = false;
            Banco banco = new Banco();
            Console.WriteLine("Inserir Companhia Aérea na Lista de Bloqueados:");
            do
            {
                while (Validacao == false)
                {
                    Console.Write("Informe o CNPJ do Companhia Aérea: ");
                    this.CNPJ = Console.ReadLine();

                    Validacao = companhiaAerea.ValidarCnpj(CNPJ);

                    if (Validacao == false)
                    {
                        Console.WriteLine("\nNÚMERO DE CNPJ INVÁLIDO.");
                        Console.WriteLine("PRESSIONE QUALQUER TECLA PARA INFORMAR NOVAMENTE!");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

                String sql = $"SELECT CNPJ FROM CompanhiaAerea WHERE CNPJ = ('{this.CNPJ}');";
                int verificar = banco.Verify(sql);
                if (verificar != 0)
                {
                    sql = $"INSERT INTO Cadastro_Bloqueados values CNPJ = ('{this.CNPJ}');";
                    banco.Add(sql);

                    sql = $"UPDATE CompanhiaAerea SET Situacao = 'I' WHERE CNPJ = ('{this.CNPJ}');";
                    banco.Update(sql);

                    Console.WriteLine("\n Companhia Aérea adicionada a lista de Bloqueados! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                }
                else
                {
                    sql = $"INSERT INTO Cadastro_Bloqueados values CNPJ = ('{this.CNPJ}');";
                    banco.Add(sql);

                    Console.WriteLine("\n Companhia Aérea adicionada a lista de Bloqueados! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                }
            } while (true);
        }
#endregion

        #region Remover Companhia da Lista de Bloqueados
        public void RemoverBloqueado()
        {
            CompanhiaAerea companhiaAerea = new CompanhiaAerea();
            Banco banco = new Banco();
            Console.WriteLine("Remoção de Companhias Aéreas bloqueadas:");

            do
            {
                Console.Write("Informe o CNPJ da Companhia a ser Removida da Lista de Bloqueados: ");
                this.CNPJ = Console.ReadLine();


                String sql = $"SELECT CNPJ FROM Cadastro_Bloqueados WHERE CNPJ = ('{this.CNPJ}');";
                int verificar = banco.Verify(sql);
                if (verificar != 0)
                {
                    sql = $"DELETE FROM Cadastro_Bloqueados values CNPJ = ('{this.CNPJ}');";
                    banco.Delete(sql);

                    sql = $"SELECT * FROM CompanhiaAerea WHERE CNPJ = ('{this.CNPJ}');";
                    verificar = banco.Verify(sql);

                    if (verificar != 0)
                    {
                        sql = $"UPDATE CompanhiaAerea SET Situacao = 'A' WHERE CNPJ = ('{this.CNPJ}');";
                        banco.Update(sql);
                    }

                    Console.WriteLine("\nCompanhia Aérea removida da lista de Bloqueados! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("\nCompanhia Aérea removida da lista de Bloqueados! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                    break;
                }
            } while (true);
        }
        #endregion

        #region Listar Companhias Bloqueadas
        public void ListarBloqueados()
        {
            Banco banco = new Banco();
            Console.Clear();
            string sql = $"SELECT * FROM Cadastro_Bloqueados;";
            banco.Select(sql, 1);
            Console.WriteLine("\nFim da Impressão de Companhias Bloqueadas. Pressione ENTER para continuar!");
            Console.ReadKey();
        }
        #endregion
    }
}
