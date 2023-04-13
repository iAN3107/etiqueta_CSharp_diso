using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etiqueta
{
    internal class Banco
    {
        public String NomeImpressora { get; set; }
        public String Ip { get; set; }
        public String Usuario { get; set; }
        public String Senha{ get; set; }
        public String NomeBanco { get; set; }

        public Banco(string nomeImpressora, string ip, string usuario, string senha, string nomeBanco)
        {
            NomeImpressora = nomeImpressora;
            Ip = ip;
            Usuario = usuario;
            Senha = senha;
            NomeBanco = nomeBanco;
        }
    }
}
