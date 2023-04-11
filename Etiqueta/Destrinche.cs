using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etiqueta
{
    internal class Destrinche
    {
        public String Manifesto { get; set; }
        public String Cliente { get; set; }
        public String Delivery { get; set; }
        public String CodProduto { get; set; }
        public String Produto { get; set; }
        public String Deposito { get; set; }
        public String Rua { get; set; }
        public String Bloco { get; set; }
        public String Nivel { get; set; }
        public String Apartamento { get; set; }
        public String Box { get; set; }
        public String Endereco { get; set; }
        public int NumeroEtiqueta { get; set; }
        public int TotalEtiqueta { get; set; }


        public Destrinche(string manifesto, string cliente, string delivery, string codProduto, string produto, string deposito, string rua, string bloco, string nivel, string apartamento, string box, string endereco, int numeroEtiqueta, int totalEtiqueta)
        {
            Manifesto = manifesto;
            Cliente = cliente;
            Delivery = delivery;
            CodProduto = codProduto;
            Produto = produto;
            Deposito = deposito;
            Rua = rua;
            Bloco = bloco;
            Nivel = nivel;
            Apartamento = apartamento;
            Box = box;
            Endereco = endereco;
            NumeroEtiqueta = numeroEtiqueta;
            TotalEtiqueta = totalEtiqueta;
        }
    }
}
