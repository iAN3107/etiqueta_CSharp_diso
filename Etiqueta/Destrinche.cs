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
        public int Deposito { get; set; }
        public int Rua { get; set; }
        public int Bloco { get; set; }
        public int Nivel { get; set; }
        public int Apartamento { get; set; }
        public String Box { get; set; }
        public String Endereco { get; set; }
        public int NumeroEtiqueta { get; set; }
        public int TotalEtiqueta { get; set; }


        public Destrinche(string manifesto, string cliente, string delivery, string codProduto, string produto, int deposito, int rua, int bloco, int nivel, int apartamento, string box, string endereco, int numeroEtiqueta, int totalEtiqueta)
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
