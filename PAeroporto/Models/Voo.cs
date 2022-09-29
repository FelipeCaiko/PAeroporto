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

                    banco = new Banco();

                    sql = $"INSERT INTO Voo(ID, InscAeronave, DataCadastro, DataVoo, Destino, AssentosOcupados, Situacao) VALUES ('{this.ID}', '{this.InscAeronave.Inscricao}', '{this.DataCadastro}', '{this.DataVoo}', '{this.Destino.Sigla}', '{this.AssentosOcupados}', '{this.Situacao}');";
                    banco.Add(sql);

                    Console.WriteLine("\nVoo Cadastrado com Sucesso! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                    break;
                }
            } while (true);
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
