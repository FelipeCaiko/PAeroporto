using PAeroporto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class Venda
    {
        public int ID { get; set; }
        public DateTime DataVenda { get; set; }
        public Passageiro CPFPassageiro { get; set; }
        public float ValorTotal { get; set; }


        #region Cadastrar Venda de Passagem
        public void InserirVenda()
        {
            do
            {
                Console.WriteLine("Cadastro de Venda de Passagens:");
                Banco banco = new Banco();
                String sql;

                Passageiro passageiro = new Passageiro();
                do
                {
                    Console.Write("Informe o CPF do Passageiro que irá comprar a passagem: ");
                    string cpf = Console.ReadLine();

                    sql = $"SELECT * FROM Passageiro WHERE CPF = ('{cpf}');";
                    passageiro = banco.VerifyReturnPA(sql);
                    if (passageiro != null)
                    {
                        break;
                    }
                } while (true);

                this.DataVenda = DateTime.Now;

                Console.Write("Informe qual o ID do Voo que irá ser efetuada a compra de passagem: ");
                string idVoo = Console.ReadLine();

                sql = $"SELECT ID FROM Voo WHERE ID = '{idVoo}';";
                int verificar = banco.Verify(sql);

                PassagemVoo passagem = new PassagemVoo();
                List<PassagemVoo> lstVendas = new List<PassagemVoo>();

                int op = 0;

                if (verificar != 0)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Você deseja confirmar a compra de uma passagem para este voo?:");
                        Console.Write("1- Sim\n2- Não\nOpção: ");
                        op = int.Parse(Console.ReadLine());

                        switch (op)
                        {
                            case 1:
                                sql = $"SELECT * FROM PassagemVoo WHERE IDVoo = '{idVoo}' AND Situacao = 'L';";
                                passagem = banco.VerifyReturnPS(sql);

                                sql = $"UPDATE Situacao FROM PassagemVoo WHERE ID = '{passagem.ID}';";
                                banco.Update(sql);

                                this.ValorTotal = this.ValorTotal + passagem.Valor;

                                lstVendas.Add(passagem);

                                Console.WriteLine("Compra de uma passagem efetuada! PRESSIONE ENTER PARA CONTINUAR!");
                                Console.ReadKey();

                                break;
                            case 2:
                                Console.WriteLine("Você saiu do menu de compra de passagens!");
                                break;
                            default:
                                Console.Write("\nOpcao Inválida! Aperte ENTER para executar novamente.");
                                Console.ReadKey();
                                break;
                        }
                    } while (op != 2);

                    if (this.ValorTotal != 0)
                    {
                        sql = $"INSERT INTO Venda (DataVenda, ValorTotal, CPFPassageiro) values ('{this.DataVenda}','{this.ValorTotal}','{this.CPFPassageiro.CPF}';";
                        banco.Add(sql);
                    }

                    break;
                }
            } while (true);
        }
        #endregion
    }
}
