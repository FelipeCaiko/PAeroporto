using PAeroporto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class Restritos
    {
        public string CPF { get; set; }

        public Restritos()
        {
        }

        #region Inserir Passageiro da lista de Restritos
        public void InserirRestrito()
        {
            Passageiro passageiro = new Passageiro();
            bool Validacao = false;
            Banco banco = new Banco();
            Console.WriteLine("Cadastro de Passageiros Restritos:");

            do
            {
                while (Validacao == false)
                {
                    Console.Write("Informe o CPF do Passageiro: ");
                    this.CPF = Console.ReadLine();

                    Validacao = passageiro.ValidarCpf(CPF);

                    if (Validacao == false)
                    {
                        Console.WriteLine("\nNÚMERO DE CPF INVÁLIDO.");
                        Console.WriteLine("PRESSIONE QUALQUER TECLA PARA INFORMAR NOVAMENTE!");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

                String sql = $"SELECT CPF FROM Passageiro WHERE CPF = ('{this.CPF}');";
                int verificar = banco.Verify(sql);
                if (verificar != 0)
                {
                    sql = $"INSERT INTO Cadastro_Restritos values CPF = ('{this.CPF}');";
                    banco.Add(sql);

                    sql = $"UPDATE Passageiro SET Situacao = 'I' WHERE CPF = ('{this.CPF}');";
                    banco.Update(sql);

                    Console.WriteLine("\nPassageiro adicionado a lista de Restritos! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                }
                else
                {
                    sql = $"INSERT INTO Cadastro_Restritos values CPF = ('{this.CPF}');";
                    banco.Add(sql);

                    Console.WriteLine("\nPassageiro adicionado a lista de Restritos! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                }
            } while (true);
        }
#endregion

        #region Remover Passageiro da lista de Restritos
        public void RemoverRestrito()
        {
            Passageiro passageiro = new Passageiro();
            Banco banco = new Banco();
            Console.WriteLine("Remoção de Passageiros Restritos:");

            do
            {
                Console.Write("Informe o CPF do Passageiro a ser Removido da Lista de Restritos: ");
                this.CPF = Console.ReadLine();


                String sql = $"SELECT CPF FROM Cadastro_Restritos WHERE CPF = ('{this.CPF}');";
                int verificar = banco.Verify(sql);
                if (verificar != 0)
                {
                    sql = $"DELETE FROM Cadastro_Restritos values CPF = ('{this.CPF}');";
                    banco.Delete(sql);

                    sql = $"SELECT * FROM Passageiro WHERE CPF = ('{this.CPF}');";
                    verificar = banco.Verify(sql);

                    if (verificar != 0)
                    {
                        sql = $"UPDATE Passageiro SET Situacao = 'A' WHERE CPF = ('{this.CPF}');";
                        banco.Update(sql);
                    }

                    Console.WriteLine("\nPassageiro removido da lista de Restritos! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("\nPassageiro não foi encontrado na lista de Restritos! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                    break;
                }
            } while (true);
        }
        #endregion
    }

}
