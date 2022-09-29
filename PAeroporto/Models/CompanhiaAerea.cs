using PAeroporto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class CompanhiaAerea
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime UltimoVoo { get; set; }
        public string Situacao { get; set; }

        public CompanhiaAerea()
        {
        }

        #region Cadastrar Nova Companhia
        public void InserirCompanhia()
        {
            do
            {
                Console.WriteLine("Cadastro de Companhia Aérea:");
                bool Validacao = false;
                Banco banco = new Banco();

                while (Validacao == false)
                {
                    Console.Write("Informe o CNPJ da sua Companhia: ");
                    this.CNPJ = Console.ReadLine();

                    Validacao = ValidarCnpj(this.CNPJ);

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
                if (verificar == 0)
                {
                    Console.Write("Informe a Razão Social da sua Companhia: ");
                    this.RazaoSocial = Console.ReadLine();

                    Console.Write("Informe a Data de Abertura da sua Companhia: ");
                    this.DataAbertura = DateTime.Parse(Console.ReadLine());

                    this.DataCadastro = DateTime.Now;
                    this.UltimoVoo = DateTime.Now;
                    this.Situacao = "A";

                    banco = new Banco();

                    sql = $"INSERT INTO CompanhiaAerea(CNPJ, RazaoSocial, DataAbertura, DataCadastro, UltimoVoo, Situacao) VALUES ('{this.CNPJ}', '{this.RazaoSocial}', '{this.DataAbertura}', '{this.DataCadastro}', '{this.UltimoVoo}', '{this.Situacao}');";
                    banco.Add(sql);

                    Console.WriteLine("\nCompanhia Aérea Registrada com Sucesso! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                    break;
                }
                if (verificar != 0)
                {
                    Console.WriteLine("CNPJ Informado já cadastrado! Pressione ENTER para continuar!");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (true);
        }
        #endregion

        #region Editar Companhia
        public void EditarCompanhia()
        {
            Console.WriteLine("Editar Dados de Companhia Aérea:");
            Banco banco = new Banco();

            do
            {
                Console.Write("Informe 0 caso deseje sair. \nInforme o CNPJ da Companhia Aérea a ser Editado: ");
                CNPJ = Console.ReadLine();
                if (CNPJ == "0")
                    break;

                String sql = $"SELECT CNPJ, RazaoSocial, DataAbertura, Situacao FROM CompanhiaAerea WHERE CNPJ = ('{CNPJ}');";
                int verificar = banco.Verify(sql);


                if (verificar != 0)
                {
                    banco = new Banco();

                    int op = 0;
                    do
                    {
                        Console.Clear();
                        ListarCNPJ(CNPJ);
                        Console.WriteLine("Informe a opcão que deseja alterar: ");
                        Console.WriteLine(" 1 - Razão Social");
                        Console.WriteLine(" 2 - Data de Abertura");
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
                                Console.Write("Informe a nova Razão Social: ");
                                string novaRazao = Console.ReadLine();

                                sql = $"UPDATE CompanhiaAerea SET RazaoSocial = ('{novaRazao}');";
                                banco.Update(sql);

                                Console.WriteLine("Razão Social alterada com secesso!");
                                Console.ReadKey();
                                break;
                            case 2:
                                Console.Write("Informe a nova Data de Abertura: ");
                                DateTime novaDataAbertura = DateTime.Parse(Console.ReadLine());

                                sql = $"UPDATE CompanhiaAerea SET DataAbertura = ('{novaDataAbertura}');";
                                banco.Update(sql);

                                Console.WriteLine("Data de Abertura alterada com secesso!");
                                Console.ReadKey();
                                break;
                            case 3:
                                int opc = 0;
                                do
                                {
                                    Console.WriteLine("\nInforme qual a nova Situação da Companhia: ");
                                    Console.WriteLine(" 1 - Situação Ativa");
                                    Console.WriteLine(" 2 - Situação Inativa");
                                    Console.Write(" Informe a opcao: ");
                                    opc = int.Parse(Console.ReadLine());

                                    switch (opc)
                                    {
                                        case 1:
                                            sql = $"UPDATE CompanhiaAerea SET Situacao = 'A';";
                                            banco.Update(sql);
                                            Console.WriteLine("Situação da Companhia alterada com secesso!");
                                            break;
                                        case 2:
                                            sql = $"UPDATE CompanhiaAerea SET Situacao = 'I';";
                                            banco.Update(sql);
                                            Console.WriteLine("Situação da Companhia alterada com secesso!");
                                            break;
                                        default:
                                            Console.Write("\n Opcao Inválida! Aperte ENTER para executar novamente.");
                                            Console.ReadKey();
                                            break;
                                    }
                                } while (opc != 1 && opc != 2);

                                Console.ReadKey();
                                break;
                            default:
                                Console.Write("\n Opcao Inválida! Aperte ENTER para executar novamente.");
                                Console.ReadKey();
                                break;
                        }
                    } while (op != 0);
                }
            } while (true);
        }
        #endregion

        #region Listar Companhias Cadastradas
        public void ListarCompanhias()
        {
            Banco banco = new Banco();
            Console.Clear();
            string sql = $"SELECT * FROM CompanhiaAerea;";
            banco.Select(sql, 1);
            Console.WriteLine("\nFim da Impressão de Companhias. Pressione ENTER para continuar!");
            Console.ReadKey();
        }
        #endregion

        #region Listar uma Companhia
        public void ListarCNPJ(string cnpj)
        {
            Banco banco = new Banco();

            if (cnpj == null)
            {
                CNPJ = cnpj;
                do
                {
                    Console.Clear();
                    Console.Write("Informe 0 caso deseje sair. \nInforme o CNPJ que irá ser buscado: ");
                    CNPJ = Console.ReadLine();
                    if (CNPJ == "0")
                        break;

                    string sql = $"SELECT * FROM CompanhiaAerea WHERE CNPJ = ('{CNPJ}');";
                    int verificar = banco.Verify(sql);

                    if (verificar != 0)
                    {
                        banco = new Banco();
                        banco.Select(sql, 1);
                        Console.WriteLine("\nCNPJ foi encontrado. Pressione ENTER para continuar!");
                        Console.ReadKey();
                        break;
                    }
                    if (verificar == 0)
                    {
                        Console.WriteLine("CNPJ Informado não foi encontrado! Pressione ENTER para continuar!");
                        Console.ReadKey();
                        Console.Clear();
                    }
                } while (true);
            }
            else
            {
                CNPJ = cnpj;
                string sql = $"SELECT * FROM CompanhiaAerea WHERE CNPJ = ('{CNPJ}');";

                banco = new Banco();
                banco.Select(sql, 1);
            }
        }
        #endregion

        #region Validação do CNPJ
        public bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma, resto;
            string digito, tempCnpj;

            //limpa caracteres especiais e deixa em minusculo
            cnpj = cnpj.ToLower().Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");

            // Se vazio
            if (cnpj.Length == 0)
                return false;

            //Se o tamanho for < 14 então retorna como falso
            if (cnpj.Length != 14)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cnpj)
            {

                case "00000000000000":

                    return false;

                case "11111111111111":

                    return false;

                case "22222222222222":

                    return false;

                case "33333333333333":

                    return false;

                case "44444444444444":

                    return false;

                case "55555555555555":

                    return false;

                case "66666666666666":

                    return false;

                case "77777777777777":

                    return false;

                case "88888888888888":

                    return false;

                case "99999999999999":

                    return false;
            }

            tempCnpj = cnpj.Substring(0, 12);

            //CNPJ é gerado a partir de uma função matemática, logo para validar, sempre irá utilizar esse calculo 
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }
        #endregion
    }
}
