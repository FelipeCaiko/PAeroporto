using PAeroporto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class Passageiro
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime UltimaCompra { get; set; }
        public string Situacao { get; set; }

        public Passageiro()
        {
        }

        #region Cadastrar Novo Passageiro
        public void InserirPassageiro()
        {
            do
            {
                Console.WriteLine("Cadastro de Passageiros:");
                bool Validacao = false;
                Banco banco = new Banco();

                while (Validacao == false)
                {
                    Console.Write("Informe o CPF do Passageiro: ");
                    this.CPF = Console.ReadLine();

                    Validacao = ValidarCpf(this.CPF);

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
                if (verificar == 0)
                {
                    Console.Write("Informe o Nome do Passageiro: ");
                    this.Nome = Console.ReadLine();

                    Console.Write("Informe a Data de Nascimento do Passageiro: ");
                    this.DataNascimento = DateTime.Parse(Console.ReadLine());

                    bool verifSexo;
                    do
                    {
                        Console.Write("Informe o Sexo. M / F : ");
                        this.Sexo = Console.ReadLine().ToUpper();

                        if (this.Sexo != "M" && this.Sexo != "F")
                        {
                            Console.WriteLine("Voce inseriu um sexo inválido!");
                            verifSexo = false;
                        }
                        else
                            verifSexo = true;

                    } while (verifSexo == false);

                    this.UltimaCompra = DateTime.Now;
                    this.DataCadastro = DateTime.Now;
                    this.Situacao = "A";

                    banco = new Banco();

                    sql = $"INSERT INTO Passageiro(CPF, Nome, DataNascimento, Sexo, UltimaCompra, DataCadastro, Situacao) VALUES ('{this.CPF}', '{this.Nome}', '{this.DataNascimento}', '{this.Sexo}', '{this.UltimaCompra}', '{this.DataCadastro}', '{this.Situacao}');";
                    banco.Add(sql);

                    Console.WriteLine("\nPassageiro Cadastrado com Sucesso! Pressione ENTER para Continuar!");
                    Console.ReadKey();
                    break;
                }
                if (verificar != 0)
                {
                    Console.WriteLine("CPF Informado já cadastrado! Pressione ENTER para continuar!");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (true);
        }
        #endregion

        #region Editar Passageiro
        public void EditarPassageiro()
        {
            Console.WriteLine("Editar Dados do Passageiro:");
            Banco banco = new Banco();

            do
            {
                Console.Write("Informe 0 caso deseje sair. \nInforme o CPF do Passageiro a ser Editado: ");
                CPF = Console.ReadLine();
                if (CPF == "0")
                    break;

                String sql = $"SELECT CPF, Nome, DataNascimento, Sexo, Situacao FROM Passageiro WHERE CPF = ('{CPF}');";
                int verificar = banco.Verify(sql);

                if (verificar != 0)
                {
                    banco = new Banco();

                    int op = 0;
                    do
                    {
                        Console.Clear();
                        ListarCPF(CPF);
                        Console.WriteLine("Informe a opcão que deseja alterar: ");
                        Console.WriteLine(" 1 - Nome");
                        Console.WriteLine(" 2 - Data de Nascimento");
                        Console.WriteLine(" 3 - Sexo");
                        Console.WriteLine(" 4 - Situacao");
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
                                Console.Write("Informe o novo Nome: ");
                                string novoNome = Console.ReadLine();

                                sql = $"UPDATE Passageiro SET Nome = ('{novoNome}') WHERE CPF = ('{CPF}');";
                                banco.Update(sql);

                                Console.WriteLine("\nNome do Passageiro alterado com secesso!");
                                Console.ReadKey();
                                break;
                            case 2:
                                Console.Write("Informe a nova Data de Nascimento: ");
                                DateTime novaDataNascimento = DateTime.Parse(Console.ReadLine());

                                sql = $"UPDATE Passageiro SET DataNascimento = ('{novaDataNascimento}') WHERE CPF = ('{CPF}');";
                                banco.Update(sql);

                                Console.WriteLine("\nData de Nascimento alterada com secesso!");
                                Console.ReadKey();
                                break;
                            case 3:
                                bool verifSexo;
                                string novoSexo;
                                do
                                {
                                    Console.Write("Informe o novo Sexo. M / F : ");
                                    novoSexo = Console.ReadLine();

                                    if (novoSexo != "M" && novoSexo != "F")
                                    {
                                        Console.WriteLine("Voce inseriu um sexo inválido!");
                                        verifSexo = false;
                                    }
                                    else
                                        verifSexo = true;

                                } while (verifSexo == false);

                                sql = $"UPDATE Passageiro SET Sexo = ('{novoSexo}') WHERE CPF = ('{CPF}');";
                                banco.Update(sql);

                                Console.WriteLine("\nSexo do Passageiro alterado com secesso!");
                                Console.ReadKey();
                                break;
                            case 4:
                                int opc = 0;
                                do
                                {
                                    Console.WriteLine("\nInforme qual a nova Situação do Passageiro: ");
                                    Console.WriteLine(" 1 - Situação Ativa");
                                    Console.WriteLine(" 2 - Situação Inativa");
                                    Console.Write(" Informe a opcao: ");
                                    opc = int.Parse(Console.ReadLine());

                                    switch (opc)
                                    {
                                        case 1:
                                            sql = $"UPDATE Passageiro SET Situacao = 'A' WHERE CPF = ('{CPF}');";
                                            banco.Update(sql);
                                            Console.WriteLine("\nSituação do Passageiro alterado com secesso!");
                                            break;
                                        case 2:
                                            sql = $"UPDATE Passageiro SET Situacao = 'I' WHERE CPF = ('{CPF}');";
                                            banco.Update(sql);
                                            Console.WriteLine("\nSituação do Passageiro alterado com secesso!");
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

        #region Listar Passageiros Cadastrados
        public void ListarPassageiros()
        {
            Banco banco = new Banco();
            Console.Clear();
            string sql = $"SELECT * FROM Passageiro;";
            banco.Select(sql, 2);
            Console.WriteLine("\nFim da Impressão de Passageiros. Pressione ENTER para continuar!");
            Console.ReadKey();
        }
        #endregion

        #region Listar um Passageiro
        public void ListarCPF(string cpf)
        {
            Banco banco = new Banco();

            if (cpf == null)
            {
                CPF = cpf;
                do
                {
                    Console.Clear();
                    Console.Write("Informe 0 caso deseje sair. \nInforme o CPF que irá ser buscado: ");
                    CPF = Console.ReadLine();
                    if (CPF == "0")
                        break;

                    string sql = $"SELECT * FROM Passageiro WHERE CPF = ('{CPF}');";
                    int verificar = banco.Verify(sql);

                    if (verificar != 0)
                    {
                        banco = new Banco();
                        banco.Select(sql, 2);
                        Console.WriteLine("\nCPF foi encontrado. Pressione ENTER para continuar!");
                        Console.ReadKey();
                        break;
                    }
                    if (verificar == 0)
                    {
                        Console.WriteLine("\nCPF Informado não foi encontrado! Pressione ENTER para continuar!");
                        Console.ReadKey();
                        Console.Clear();
                    }
                } while (true);
            }
            else
            {
                CPF = cpf;
                string sql = $"SELECT * FROM Passageiro WHERE CPF = ('{CPF}');";

                banco = new Banco();
                banco.Select(sql, 2);
            }
        }
        #endregion

        #region Validação do CPF
        public bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf, digito;
            int soma, resto;

            //Formatando para deixar o CPF somente com os números, sem caracteres especiais
            cpf = cpf.ToLower().Trim();
            cpf = cpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            cpf = cpf.Replace("+", "").Replace("*", "").Replace(",", "").Replace("?", "");
            cpf = cpf.Replace("!", "").Replace("@", "").Replace("#", "").Replace("$", "");
            cpf = cpf.Replace("%", "").Replace("¨", "").Replace("&", "").Replace("(", "");
            cpf = cpf.Replace("=", "").Replace("[", "").Replace("]", "").Replace(")", "");
            cpf = cpf.Replace("{", "").Replace("}", "").Replace(":", "").Replace(";", "");
            cpf = cpf.Replace("<", "").Replace(">", "").Replace("ç", "").Replace("Ç", "");


            //Se o CPF for informado vazio
            if (cpf.Length == 0)
                return false;

            //Se a quantidade de dígitos for diferente do permitido (11)
            if (cpf.Length != 11)
                return false;

            //Se os números informados forem todos iguais
            switch (cpf)
            {

                case "00000000000":

                    return false;

                case "11111111111":

                    return false;

                case "22222222222":

                    return false;

                case "33333333333":

                    return false;

                case "44444444444":

                    return false;

                case "55555555555":

                    return false;

                case "66666666666":

                    return false;

                case "77777777777":

                    return false;

                case "88888888888":

                    return false;

                case "99999999999":

                    return false;
            }

            tempCpf = cpf.Substring(0, 9);

            //Calculo para gerar um número de CPF válido
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
        #endregion
    }
}
