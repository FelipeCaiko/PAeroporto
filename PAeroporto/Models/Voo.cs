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
        public string Destino { get; set; }
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

                this.ID = "";

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

                    Iatas iatas = new Iatas();
                    do
                    {
                        iatas.ListarIatas();

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

                    Console.Write("\nInforme a Sigla da IATA do Destino do Voo: ");
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
            } while (true);
        }
        #endregion

    }
}
