using PAeroporto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class Aeronave
    {
        public String Inscricao { get; set; }
        public int Capacidade { get; set; }
        public DateTime UltimaVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Situacao { get; set; }
        public CompanhiaAerea CNPJCompanhia { get; set; }

        public Aeronave()
        {
        }

        #region Cadastrar Nova Aeronave
        public void InserirAeronave()
        {
            do
            {
                Console.WriteLine("Cadastro de Aeronaves:");
                Banco banco = new Banco();

                String prefixo = SelecionarPrefixo();

                if (prefixo == null)
                {
                    Console.WriteLine("Você saiu do Cadastro de Aeronaves! PRESSIONE ENTER PARA CONTINUAR!");
                    Console.ReadKey();
                    break;
                }

                String sufixo = SufixoAeronave();

                this.Inscricao = prefixo + "-" + sufixo;

                String sql = $"SELECT Inscricao FROM Aeronave WHERE Inscricao = ('{this.Inscricao}');";
                int verificar = banco.Verify(sql);
                if (verificar == 0)
                {
                    CompanhiaAerea companhiaAerea = new CompanhiaAerea();
                    do
                    {
                        companhiaAerea.ListarCompanhias();

                        Console.Write("\nInforme o CNPJ da Companhia Aérea que a Aeronave pertence: ");
                        string cnpj = Console.ReadLine();

                        sql = $"SELECT * FROM CompanhiaAerea WHERE CNPJ = ('{cnpj}');";
                        companhiaAerea = banco.VerifyReturnCA(sql);
                        if (companhiaAerea != null)
                        {
                            break;
                        }
                    } while (true);

                    this.CNPJCompanhia = companhiaAerea;

                    Console.Write("\nInforme a Capacidade de Passageiros da Aeronave: ");
                    this.Capacidade = int.Parse(Console.ReadLine());

                    this.UltimaVenda = DateTime.Now;
                    this.DataCadastro = DateTime.Now;
                    this.Situacao = "A";

                    banco = new Banco();

                    sql = $"INSERT INTO Aeronave(Inscricao, CNPJCompanhia, Capacidade, UltimaVenda, DataCadastro, Situacao) VALUES ('{this.Inscricao}', '{this.CNPJCompanhia.CNPJ}', '{this.Capacidade}', '{this.UltimaVenda}', '{this.DataCadastro}', '{this.Situacao}');";
                    banco.Add(sql);

                    Console.WriteLine("\nAeronave Cadastrada com Sucesso! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                    break;
                }
                if (verificar != 0)
                {
                    Console.WriteLine("Inscrição da Aeronave informada já cadastrada! Pressione ENTER para continuar!");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (true);
        }
        #endregion

        #region Editar Aeronave
        public void EditarAeronave()
        {
            Console.Clear();
            Console.WriteLine("Editar Dados da Aeronave:");
            Banco banco = new Banco();

            do
            {
                Console.Write("Informe 0 caso deseje sair. \nInforme a Inscrição da Aeronave a ser Editada: ");
                Inscricao = Console.ReadLine();
                if (Inscricao == "0")
                    break;

                String sql = $"SELECT Inscricao, Capacidade, Situacao FROM Aeronave WHERE Inscricao = ('{Inscricao}');";
                int verificar = banco.Verify(sql);

                if (verificar != 0)
                {
                    banco = new Banco();

                    int op = 0;
                    do
                    {
                        Console.Clear();
                        ListarInscricao(Inscricao);
                        Console.WriteLine("Informe a opcão que deseja alterar: ");
                        Console.WriteLine(" 1 - Capacidade");
                        Console.WriteLine(" 2 - Alterar Companhia Responsável pela Aeronave");
                        Console.WriteLine(" 3 - Situação");
                        Console.WriteLine(" 0 - Sair");
                        Console.Write(" Informe a opcao: ");
                        op = int.Parse(Console.ReadLine());

                        switch (op)
                        {
                            case 0:
                                Console.WriteLine("Você saiu do Menu de Alteração de Cadastro! Aperte ENTER para sair.");
                                Console.ReadKey();
                                break;
                            case 1:
                                Console.Write("Informe a nova Capacidade: ");
                                int novaCapacidade = int.Parse(Console.ReadLine());

                                sql = $"UPDATE Aeronave SET Capacidade = ('{novaCapacidade}');";
                                banco.Update(sql);

                                Console.WriteLine("\nCapacidade da Aeronave alterada com secesso!");
                                Console.ReadKey();
                                break;
                            case 2:
                                CompanhiaAerea companhiaAerea = new CompanhiaAerea();

                                Console.WriteLine("Informe qual será a nova Companhia Aérea Responsável pela Aeronave: ");
                                companhiaAerea.ListarCompanhias();

                                Console.Write("\nInforme o CNPJ da Companhia Aérea que a Aeronave será transferida: ");
                                string novaCompanhia = Console.ReadLine();

                                sql = $"SELECT * FROM CompanhiaAerea WHERE CNPJ = ('{novaCompanhia}');";
                                companhiaAerea = banco.VerifyReturnCA(sql);

                                if (verificar != 0)
                                {
                                    sql = $"UPDATE Aeronave SET CNPJCompanhia = ('{companhiaAerea.CNPJ}');";
                                    banco.Update(sql);

                                    Console.WriteLine("\nCompanhia Aérea reponsável pela Aeronave foi transferida com secesso!");
                                    Console.ReadKey();
                                }
                                else
                                    Console.WriteLine("CNPJ da Companhia Aérea informado não foi encontrado");
                                break;
                            case 3:
                                int opc = 0;
                                do
                                {
                                    Console.WriteLine("\nInforme qual a nova Situação da Aeronave: ");
                                    Console.WriteLine(" 1 - Situação Ativa");
                                    Console.WriteLine(" 2 - Situação Inativa");
                                    Console.Write(" Informe a opcao: ");
                                    opc = int.Parse(Console.ReadLine());

                                    switch (opc)
                                    {
                                        case 1:
                                            sql = $"UPDATE Aeronave SET Situacao = 'A';";
                                            banco.Update(sql);
                                            Console.WriteLine("\nSituação da Aeronave alterada com secesso!");
                                            break;
                                        case 2:
                                            sql = $"UPDATE Aeronave SET Situacao = 'I';";
                                            banco.Update(sql);
                                            Console.WriteLine("\nSituação da Aeronave alterada com secesso!");
                                            break;
                                        default:
                                            Console.Write("\nOpcao Inválida! Aperte ENTER para executar novamente.");
                                            Console.ReadKey();
                                            break;
                                    }
                                } while (opc != 1 && opc != 2);

                                Console.ReadKey();
                                break;
                            default:
                                Console.Write("\nOpcao Inválida! Aperte ENTER para executar novamente.");
                                Console.ReadKey();
                                break;
                        }
                    } while (op != 0);
                    break;
                }
            } while (true);
        }
        #endregion

        #region Listar Aeronaves Cadastradas
        public void ListarAeronaves()
        {
            Banco banco = new Banco();
            string sql = $"SELECT * FROM Aeronave;";
            banco.Select(sql, 3);
            Console.WriteLine("\nFim da Impressão de Aeronaves. Pressione ENTER para continuar!");
            Console.ReadKey();
        }
        #endregion

        #region Listar uma Aeronave
        public void ListarInscricao(string inscricao)
        {
            Banco banco = new Banco();

            if (inscricao == null)
            {
                Inscricao = inscricao;
                do
                {
                    Console.Clear();
                    Console.Write("Informe 0 caso deseje sair. \nInforme a Inscrição da Aeronave que irá ser buscada: ");
                    Inscricao = Console.ReadLine();
                    if (Inscricao == "0")
                        break;

                    string sql = $"SELECT * FROM Aeronave WHERE Inscricao = ('{Inscricao}');";
                    int verificar = banco.Verify(sql);

                    if (verificar != 0)
                    {
                        banco = new Banco();
                        banco.Select(sql, 3);
                        Console.WriteLine("\nInscrição da Aeronave foi encontrada. Pressione ENTER para continuar!");
                        Console.ReadKey();
                        break;
                    }
                    if (verificar == 0)
                    {
                        Console.WriteLine("\nInscrição da Aeronave informada não foi encontrado! Pressione ENTER para continuar!");
                        Console.ReadKey();
                        Console.Clear();
                    }
                } while (true);
            }
            else
            {
                Inscricao = inscricao;
                string sql = $"SELECT * FROM Aeronave WHERE Inscricao = ('{Inscricao}');";

                banco = new Banco();
                banco.Select(sql, 3);
            }
        }
        #endregion

        #region Selecionar Prefixo para Cadastro
        public String SelecionarPrefixo()
        {
            int prefixo;
            do
            {
                Console.Write("Informe o prefixo da aeronave: \n1 - PP\n2 - PT\n3 - PR\n4 - PS\n5 - BR\n0 - Sair\nOpção: ");
                prefixo = int.Parse(Console.ReadLine());
                switch (prefixo)
                {
                    case 1:
                        return "PP";
                        break;
                    case 2:
                        return "PT";
                        break;
                    case 3:
                        return "PR";
                        break;
                    case 4:
                        return "PS";
                        break;
                    case 5:
                        return "BR";
                        break;
                    default:
                        Console.WriteLine("PREFIXO INVÁLIDO");
                        break;
                }
            } while (true);
            return null;
        }
        #endregion

        #region Sufixo da Aeronave para Cadastro
        public String SufixoAeronave()
        {
            string sufixo;
            bool aux;
            do
            {
                Console.Write("Informe as 3 últimas letras da inscrição da aeronave: ");
                sufixo = Console.ReadLine();
                aux = VerificarSufixo(sufixo);
                if (!aux) Console.WriteLine("SUFIXO INVÁLIDO");
            } while (sufixo.Length != 3 || !aux);
            return sufixo.ToUpper();
        }
        #endregion
        #region Verificar Sufixo para Cadastro
        public bool VerificarSufixo(String sufixo)
        {
            for (int i = 0; i < 3; i++)
            {
                char aux = sufixo[i];
                if (Char.IsLetter(aux)) ;
                else return false;
            }
            return true;
        }
        #endregion
    }
}
