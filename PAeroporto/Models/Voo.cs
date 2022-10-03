using PAeroporto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class Voo
    {
        public string ID { get; set; }
        public Iatas Destino { get; set; }
        public DateTime DataVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Situacao { get; set; }
        public Aeronave InscAeronave { get; set; }
        public int AssentosOcupados { get; set; }

        public Voo()
        {
        }

        #region Cadastrar Novo Voo
        public void InserirVoo()
        {
            do
            {
                Console.WriteLine("Cadastro de Voos:");
                Banco banco = new Banco();

                this.ID = "V" + GeraNumero();

                String sql = $"SELECT ID FROM Voo WHERE ID = ('{this.ID}');";
                int verificar = banco.Verify(sql);
                if (verificar == 0)
                {
                    Aeronave aeronave = new Aeronave();
                    do
                    {
                        aeronave.ListarAeronaves();

                        Console.Write("\nInforme a Inscrição da Aeronave que irá realizar o Voo: ");
                        string inscricao = Console.ReadLine();

                        sql = $"SELECT * FROM Aeronave WHERE Inscricao = ('{inscricao}');";
                        aeronave = banco.VerifyReturnAN(sql);
                        if (aeronave != null)
                        {
                            break;
                        }
                    } while (true);

                    this.InscAeronave = aeronave;

                    Iatas iatas = new Iatas();
                    do
                    {
                        iatas.ListarIatas();

                        Console.Write("\nInforme a Sigla da IATA do Destino do Voo: ");
                        string sigla = Console.ReadLine();

                        sql = $"SELECT * FROM Iatas WHERE Sigla = ('{sigla}');";
                        iatas = banco.VerifyReturnIA(sql);
                        if (iatas != null)
                        {
                            break;
                        }
                    } while (true);

                    this.Destino = iatas;

                    Console.Write("Informe a Data e Hora de Partida do Voo: ");
                    this.DataVoo = DateTime.Parse(Console.ReadLine());

                    this.DataCadastro = DateTime.Now;
                    this.AssentosOcupados = 0;
                    this.Situacao = "A";

                    Console.Write("Informe qual será o valor das Passagens desse Voo: ");
                    float valor = float.Parse(Console.ReadLine());

                    string idVoo = this.ID;

                    banco = new Banco();

                    sql = $"INSERT INTO Voo(ID, InscAeronave, DataCadastro, DataVoo, Destino, AssentosOcupados, Situacao) VALUES ('{this.ID}', '{this.InscAeronave.Inscricao}', '{this.DataCadastro}', '{this.DataVoo}', '{this.Destino.Sigla}', '{this.AssentosOcupados}', '{this.Situacao}');" +
                        $"EXEC dbo.CadastroPassagens '{valor}','{idVoo}';";
                    banco.Add(sql);

                    Console.WriteLine("\nVoo Cadastrado com Sucesso! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                    break;
                }
            } while (true);
        }
        #endregion

        #region Editar Voo
        public void EditarVoo()
        {
            Console.WriteLine("Editar Dados do Voo:");
            Banco banco = new Banco();

            do
            {
                Console.Write("Informe 0 caso deseje sair. \nInforme o ID do Voo a ser Editado: ");
                ID = Console.ReadLine();
                if (ID == "0")
                    break;

                String sql = $"SELECT ID, InscAeronave, DataVoo, Situacao FROM Voo WHERE ID = ('{ID}');";
                int verificar = banco.Verify(sql);

                if (verificar != 0)
                {
                    banco = new Banco();

                    int op = 0;
                    do
                    {
                        Console.Clear();
                        ListarIDVoo(ID);
                        Console.WriteLine("Informe a opcão que deseja alterar: ");
                        Console.WriteLine(" 1 - Aeronave Responsável pelo Voo");
                        Console.WriteLine(" 2 - Data do Voo");
                        Console.WriteLine(" 3 - Situacao");
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
                                Aeronave aeronave = new Aeronave();

                                Console.WriteLine("Informe qual será a Aeronave que irá realizar o Voo: ");
                                aeronave.ListarAeronaves();

                                Console.Write("Informe a Inscrição da Aeronave que irá realizar o Voo: ");
                                string novaAeronave = Console.ReadLine();

                                sql = $"SELECT * FROM Aeronave WHERE Inscricao = ('{novaAeronave}');";
                                aeronave = banco.VerifyReturnAN(sql);

                                if (verificar != 0)
                                {
                                    sql = $"UPDATE Voo SET InscAeronave = ('{aeronave.Inscricao}') WHERE ID = '{ID}';";
                                    banco.Update(sql);

                                    Console.WriteLine("\nAeronave reponsável pelo Voo foi atualizada com secesso!");
                                    Console.ReadKey();
                                }
                                else
                                    Console.WriteLine("Inscrição da Aeronave informada não foi encontrada!");
                                break;
                            case 2:
                                Console.Write("Informe a nova Data do Voo: ");
                                DateTime novaDataVoo = DateTime.Parse(Console.ReadLine());

                                sql = $"UPDATE Voo SET DataVoo = ('{novaDataVoo}');";
                                banco.Update(sql);

                                Console.WriteLine("\nData do Voo alterada com secesso!");
                                Console.ReadKey();
                                break;
                            case 3:
                                int opc = 0;
                                do
                                {
                                    Console.WriteLine("\nInforme qual a nova Situação do Voo: ");
                                    Console.WriteLine(" 1 - Situação Ativa");
                                    Console.WriteLine(" 2 - Situação Inativa");
                                    Console.Write(" Informe a opcao: ");
                                    opc = int.Parse(Console.ReadLine());

                                    switch (opc)
                                    {
                                        case 1:
                                            sql = $"UPDATE Voo SET Situacao = 'A' WHERE ID = '{ID}'; ";
                                            banco.Update(sql);
                                            Console.WriteLine("\nSituação do Voo alterada com secesso!");
                                            break;
                                        case 2:
                                            sql = $"UPDATE Voo SET Situacao = 'I' WHERE ID = '{ID}';";
                                            banco.Update(sql);
                                            Console.WriteLine("\nSituação do Voo alterada com secesso!");
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
                                Console.Write("\nOpção Inválida! Aperte ENTER para executar novamente.");
                                Console.ReadKey();
                                break;
                        }
                    } while (op != 0);
                    break;
                }
            } while (true);
        }
        #endregion

        #region Listar Voos Cadastrados
        public void ListarVoos()
        {
            Banco banco = new Banco();
            Console.Clear();
            string sql = $"SELECT * FROM Voo;";
            banco.Select(sql, 4);
            Console.WriteLine("\nFim da Impressão de Voos. Pressione ENTER para continuar!");
            Console.ReadKey();
        }
        #endregion

        #region Listar um Voo
        public void ListarIDVoo(string id)
        {
            Banco banco = new Banco();

            if (id == null)
            {
                ID = id;
                do
                {
                    Console.Clear();
                    Console.Write("Informe 0 caso deseje sair. \nInforme o ID do Voo que irá ser buscado: ");
                    ID = Console.ReadLine();
                    if (ID == "0")
                        break;

                    string sql = $"SELECT * FROM Voo WHERE ID = ('{ID}');";
                    int verificar = banco.Verify(sql);

                    if (verificar != 0)
                    {
                        banco = new Banco();
                        banco.Select(sql, 4);
                        Console.WriteLine("\nID do Voo foi encontrado. Pressione ENTER para continuar!");
                        Console.ReadKey();
                        break;
                    }
                    if (verificar == 0)
                    {
                        Console.WriteLine("\nID do Voo informado não foi encontrado! Pressione ENTER para continuar!");
                        Console.ReadKey();
                        Console.Clear();
                    }
                } while (true);
            }
            else
            {
                ID = id;
                string sql = $"SELECT * FROM Voo WHERE ID = ('{ID}');";

                banco = new Banco();
                banco.Select(sql, 4);
            }
        }
        #endregion

        #region GeraNumeros
        public String GeraNumero()
        {
            Random rand = new Random();
            int[] numero = new int[100];
            int aux = 0;
            String convert = "";
            for (int k = 0; k < numero.Length; k++)
            {
                int rnd = 0;
                do
                {
                    rnd = rand.Next(0000, 9999);
                } while (numero.Contains(rnd));
                numero[k] = rnd;
                aux = numero[k];
                convert = aux.ToString();
                break;
            }
            return convert;
        }
        #endregion
    }
}
