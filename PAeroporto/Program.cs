using PAeroporto.Models;
using PAeroporto.Services;
using System;

namespace PAeroporto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int op;
            do
            {
                op = Menu();
                switch (op)
                {
                    case 1:
                        Console.Clear();
                        MenuPassageiro();
                        break;
                    case 2:
                        Console.Clear();
                        MenuCompanhia();
                        break;
                    case 3:
                        Console.Clear();
                        MenuAeronave();
                        break;
                    case 4:
                        Console.Clear();
                        MenuVoo();
                        break;
                    case 5:
                        Console.Clear();
                        MenuPassagem();
                        break;
                    case 6:
                        Console.Clear();
                        MenuVenda();
                        break;
                    case 0:
                        Console.WriteLine("Obrigado por usar nosso aplicativo!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção Inválida! Digite opção válida.");
                        break;
                }
            } while (true);
        }
        #region Menus
        #region MenuPrincipal
        public static int Menu()
        {
            Console.Clear();
            int opc;

            Console.WriteLine("1 - Menu de Passageiros");
            Console.WriteLine("2 - Menu de Companhias Aéreas ");
            Console.WriteLine("3 - Menu de Aeronaves");
            Console.WriteLine("4 - Menu de Voos");
            Console.WriteLine("5 - Menu de Passagens ");
            Console.WriteLine("6 - Menu de Vendas ");
            Console.WriteLine("0 - Sair do Menu Principal");
            Console.Write("Opção: ");

            return opc = int.Parse(Console.ReadLine());
        }
        #endregion
        #region MenuPassageiro
        public static void MenuPassageiro()
        {
            Passageiro passageiro = new Passageiro();
            Restritos restritos = new Restritos();
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Cadastrar Passageiro");
                Console.WriteLine("2 - Buscar passageiro");
                Console.WriteLine("3 - Editar Passageiro");
                Console.WriteLine("4 - Listar Passageiros");
                Console.WriteLine("5 - Cadastrar CPF na lista de restritos");
                Console.WriteLine("6 - Remover CPF na lista de restritos");
                Console.WriteLine("7 - Listar Passageiros na lista de restritos");
                Console.WriteLine("0 - Sair do Menu de Passageiros");
                Console.Write("Opção: ");
                int opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        passageiro.InserirPassageiro();
                        break;
                    case 2:
                        Console.Clear();
                        passageiro.ListarCPF(null);
                        break;
                    case 3:
                        Console.Clear();
                        passageiro.EditarPassageiro();
                        break;
                    case 4:
                        Console.Clear();
                        passageiro.ListarPassageiros();
                        break;
                    case 5:
                        Console.Clear();
                        restritos.InserirRestrito();
                        break;
                    case 6:
                        Console.Clear();
                        restritos.RemoverRestrito();
                        break;
                    case 7:
                        Console.Clear();
                        restritos.ListarRestritos();
                        break;
                    case 0:
                        Console.WriteLine("Você saiu do Menu de Passageiros!");
                        return;
                    default:
                        Console.WriteLine("Opção Inválida! Favor selecionar uma das opções acima!");
                        break;
                }
            } while (true);
        }
        #endregion
        #region MenuCompanhia
        public static void MenuCompanhia()
        {
            CompanhiaAerea companhiaAerea = new CompanhiaAerea();
            Bloqueados bloqueados = new Bloqueados();
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Cadastrar Companhia");
                Console.WriteLine("2 - Buscar Companhia");
                Console.WriteLine("3 - Editar Companhia");
                Console.WriteLine("4 - Listar Companhias");
                Console.WriteLine("5 - Inserir CNPJ na lista de Empresas Bloqueadas");
                Console.WriteLine("6 - Remover CNPJ na lista de Empresas Bloqueadas");
                Console.WriteLine("7 - Listar Empresas Bloqueadas");
                Console.WriteLine("0 - Sair do Menu de Companhias");
                Console.Write("Opção: ");
                int opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        companhiaAerea.InserirCompanhia();
                        break;
                    case 2:
                        Console.Clear();
                        companhiaAerea.ListarCNPJ(null);
                        break;
                    case 3:
                        Console.Clear();
                        companhiaAerea.EditarCompanhia();
                        break;
                    case 4:
                        Console.Clear();
                        companhiaAerea.ListarCompanhias();
                        break;
                    case 5:
                        Console.Clear();
                        bloqueados.InserirBloqueado();
                        break;
                    case 6:
                        Console.Clear();
                        bloqueados.RemoverBloqueado();
                        break;
                    case 7:
                        Console.Clear();
                        bloqueados.ListarBloqueados();
                        break;
                    case 0:
                        Console.WriteLine("Você saiu do Menu de Companhias!");
                        return;
                    default:
                        Console.WriteLine("Opção Inválida! Favor selecionar uma das opções acima!");
                        break;
                }
            } while (true);
        }
        #endregion
        #region MenuAeronave
        public static void MenuAeronave()
        {
            Aeronave aeronave = new Aeronave();
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Cadastrar Aeronave");
                Console.WriteLine("2 - Buscar Aeronave");
                Console.WriteLine("3 - Editar Aeronave");
                Console.WriteLine("4 - Listar Aeronaves");
                Console.WriteLine("0 - Sair do Menu de Aeronaves");
                Console.Write("Opção: ");
                int opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        aeronave.InserirAeronave();
                        break;
                    case 2:
                        Console.Clear();
                        aeronave.ListarInscricao(null);
                        break;
                    case 3:
                        Console.Clear();
                        aeronave.EditarAeronave();
                        break;
                    case 4:
                        Console.Clear();
                        aeronave.ListarAeronaves();
                        break;
                    case 0:
                        Console.WriteLine("Você saiu do Menu de Aeronaves!");
                        return;
                    default:
                        Console.WriteLine("Opção Inválida! Favor selecionar uma das opções acima!");
                        break;
                }
            } while (true);
        }
        #endregion
        #region MenuVoo
        public static void MenuVoo()
        {
            Voo voo = new Voo();
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Cadastrar Voo");
                Console.WriteLine("2 - Buscar Voo");
                Console.WriteLine("3 - Editar Voo");
                Console.WriteLine("4 - Listar Voos");
                Console.WriteLine("0 - Sair do Menu de Voos");
                Console.Write("Opção: ");
                int opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        voo.InserirVoo();
                        break;
                    case 2:
                        Console.Clear();
                        voo.ListarIDVoo(null);
                        break;
                    case 3:
                        Console.Clear();
                        voo.EditarVoo();
                        break;
                    case 4:
                        Console.Clear();
                        voo.ListarVoos();
                        break;
                    case 0:
                        Console.WriteLine("Você saiu do Menu de Voos!");
                        return;
                    default:
                        Console.WriteLine("Opção Inválida! Favor selecionar uma das opções acima!");
                        break;
                }
            } while (true);
        }
        #endregion
        #region MenuPassagem
        public static void MenuPassagem()
        {
            PassagemVoo passagemVoo = new PassagemVoo();
            do
            {
                Console.WriteLine("1 - Buscar Passagem");
                Console.WriteLine("2 - Listar Passagens");
                Console.WriteLine("0 - Sair do Menu de Passagens");
                Console.Write("Opção: ");
                int opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        passagemVoo.ListarIDPassagem(null);
                        break;
                    case 2:
                        Console.Clear();
                        passagemVoo.ListarPassagens();
                        break;
                    case 0:
                        Console.WriteLine("Você saiu do Menu de Passagens!");
                        return;
                    default:
                        Console.WriteLine("Opção Inválida! Favor selecionar uma das opções acima!");
                        break;
                }
            } while (true);
        }
        #endregion
        #region MenuVenda
        public static void MenuVenda()
        {
            Venda venda = new Venda();
            do
            {
                Console.WriteLine("1 - Cadastrar Venda de Passagem");
                Console.WriteLine("2 - Buscar Venda de Passagem");
                Console.WriteLine("3 - Listar Vendas de Passagens");
                Console.WriteLine("0 - Sair do Menu de Vendas");
                Console.Write("Opção: ");
                int opc = int.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        Console.Clear();
                        break;
                    case 2:
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        break;
                    case 0:
                        Console.WriteLine("Você saiu do Menu de Vendas!");
                        return;
                    default:
                        Console.WriteLine("Opção Inválida! Favor selecionar uma das opções acima!");
                        break;
                }
            } while (true);
        }
        #endregion
        #endregion Menus

    }
}

