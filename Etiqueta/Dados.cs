using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etiqueta
{ 
    internal class Dados
    {
        public static string connectionString = @"Data Source=localhost;Initial Catalog=ETIQUETA_WMS;User ID=sa;Password=Neutralizado12";
        static SqlConnection connection = new SqlConnection(connectionString);

        public static void salvaNovaImpressora(String nomeimpressora)
        {
            string queryString = "UPDATE config SET nomeImpressora = '" + nomeimpressora + "'";
            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


        public static String retornaImpressora()
        {
            string queryString = "SELECT nomeImpressora FROM config";
            SqlCommand command = new SqlCommand(queryString, connection);

            String nomeImpressora = @"";

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                String Impressora = reader["nomeImpressora"].ToString();
                nomeImpressora = Impressora;
            }
            connection.Close();

            return nomeImpressora;

        }

        //public static bool verificaSeManifestoJaFoiImpresso() { }
        public static bool verificaExistenciaManifesto(String manifesto)
        {
            
            string queryString = "SELECT * FROM destrinche where manifesto = '" + manifesto.Trim() + "' ORDER BY cod_produto";
            SqlCommand command = new SqlCommand(queryString, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }

        }

        public static void retornaEtiquetasDestrinche(String manifesto, String impressora)
        {
            List<Destrinche> listaItems = new List<Destrinche>();
            String ultimoProduto = "";
            int contagemProduto = 1;

            connection.Open();

            string queryString = "SELECT * FROM destrinche where manifesto = '" + manifesto.Trim() +"' ORDER BY cod_produto";
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {   
                String Manifesto = reader["manifesto"].ToString();
                String Cliente = reader["cliente"].ToString();
                String Delivery = reader["delivery"].ToString();
                String CodProduto = reader["cod_produto"].ToString();
                String Produto = reader["produto"].ToString();
                String Deposito = reader["deposito"].ToString();
                String Rua = reader["rua"].ToString();
                String Bloco = reader["bloco"].ToString();
                String Nivel = reader["nivel"].ToString();
                String Apartamento = reader["apartamento"].ToString();

                Destrinche destrinche = new Destrinche(Manifesto, Cliente, Delivery, CodProduto,
                    Produto, Deposito, Rua, Bloco, Nivel, Apartamento);

                if(ultimoProduto != "" && ultimoProduto != CodProduto) {

                    for (var i = 0; i < listaItems.Count; i++)
                    {
                        System.Console.WriteLine("Entrou");

                        var items = listaItems[i];

                        int itemAtual = i + 1;
                        int itemTotal = listaItems.Count;

                        imprimeItems(items, itemAtual, itemTotal, impressora);
                    }
                    System.Console.WriteLine("entrou");
                    listaItems.Clear();
                }

                ultimoProduto = CodProduto;
                listaItems.Add(destrinche);


                //System.Console.WriteLine(chave + "," + Categoria);
            }

            System.Console.WriteLine(listaItems.Count.ToString());
                for (var i = 0; i < listaItems.Count; i++)
                {
                    System.Console.WriteLine("Entrou");
                
                    var items = listaItems[i];

                    int itemAtual = i + 1;
                    int itemTotal = listaItems.Count;

                    imprimeItems(items, itemAtual, itemTotal, impressora);
                }

            connection.Close();
            System.Console.WriteLine("teste");
        }

        public static void imprimeItems(Destrinche destrinche, int volAtual, int volTotal, String impressora)
        {
            int metadeComprimento = destrinche.Produto.Length / 2;

            String produtoParte1 = destrinche.Produto.Substring(0, metadeComprimento);
            String produtoParte2 = destrinche.Produto.Substring(metadeComprimento);

            RawPrinterHelper.SendStringToPrinter(szPrinterName: impressora, szString:
                "\u0010CT~~CD,~CC^~CT~" +
                "\r\n^XA~TA000~JSN^LT0^MNW^MTD^PON^PMN^LH0,0^JMA^PR2,2~SD15^JUS^LRN^CI0^XZ" +
                "\r\n^XA" +
               "\r\n^MMT" +
                "\r\n^PW799" +
                "\r\n^LL0320" +
                "\r\n^LS0" +
                "\r\n^FT329,33^A0N,28,28^FH\\^FDMANIFESTO^FS" +
                "\r\n^FT264,80^A0N,56,55^FH\\^FD" + destrinche.Manifesto + "^FS" +
                "\r\n^FT122,100^A0N,28,28^FB100,1,0,C^FH \\^FDCLIENTE^FS" +
                "\r\n^FT37,148^A0N,56,55^FB270,1,0,C^FH\\^FD" + destrinche.Cliente + "^FS" +
                "\r\n^FT564,100^A0N,28,28^FH\\^FDDELIVERY^FS" +
                "\r\n^FT488,148^A0N,56,55^FH \\^FD" + destrinche.Delivery + "^FS" +
                "\r\n^FO0,0^GB799,320,8^FS" +
                "\r\n^FT31,281^A0N,28,31^FH\\^FDD^FS" +
                "\r\n^FT129,281^A0N,28,31^FH\\^FDR^FS" +
                "\r\n^FT234,281^A0N,28,31^FH\\^FDB^FS" +
                "\r\n^FT336,281^A0N,28,31^FH \\^FDN^FS" +
                "\r\n^FT446,281^A0N,28,31^FH\\^FDA^FS" +
                "\r\n^FT26,309^A0N,28,31^FH\\^FD" + destrinche.Deposito + "^FS" +
                "\r\n^FT122,309^A0N,28,31^FH\\^FD" + destrinche.Rua + "^FS" +
                "\r\n^FT227,309^A0N,28,31^FH\\^FD"+ destrinche.Bloco + "^FS" +
                "\r\n^FT331,309^A0N,28,31^FH\\^FD" + destrinche.Nivel + "^FS" +
                "\r\n^FT440,309^A0N,28,31^FH\\^FD" + destrinche.Apartamento + "^FS" +
                "\r\n^FT557,308^A0N,50,55^FH\\^FD" + volAtual.ToString() + "/" + volTotal.ToString() + "^FS" +
                "\r\n^FT148,184^A0N,28,28^FH\\^FDCOD^FS" +
                "\r\n^FT66,230^A0N,56,55^FB216,1,0,C^FH\\^FD" + destrinche.CodProduto + "^FS" +
                "\r\n^FT494,178^A0N,28,28^FH\\^FDPRODUTO^FS" +
                "\r\n^FT339,207^A0N,29,38^FB437,1,0,C^FH\\^FD" + produtoParte1 + "^FS" +
                "\r\n^FT339,243^A0N,29,38^FB437,1,0,C^FH\\^FD"+ produtoParte2 + "^FS" +
                "\r\n^FT26,47^A0N,28,28^FH\\^FD31/03/2023^FS" +
                "\r\n^FT693,47^A0N,28,28^FH\\^FD16:37^FS" +
                "\r\n^PQ1,0,1,Y^XZ\r\n");


        }
    }
}