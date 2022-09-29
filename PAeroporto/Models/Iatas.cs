using PAeroporto.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAeroporto.Models
{
    internal class Iatas
    {
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public Iatas()
        {
        }

        #region Listar Iatas Cadastradas
        public void ListarIatas()
        {
            Banco banco = new Banco();
            Console.Clear();
            string sql = $"SELECT * FROM Iatas;";
            banco.Select(sql, 2);
            Console.WriteLine("\nFim da Impressão de Iatas. Pressione ENTER para continuar!");
            Console.ReadKey();
        }
        #endregion
    }
}
