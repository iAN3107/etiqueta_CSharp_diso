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
        
        public static string connectionWmsString = @"Data Source=wms;Initial Catalog=BRKDSO;User ID=wms;Password=wms";
        static SqlConnection connectionWms = new SqlConnection(connectionWmsString);

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

            string queryString = "set nocount on\r\nif exists (select * from tempdb..sysobjects where name = '##ordint') drop table ##ordint\r\nselect number, DeliveryNumber, LoadNumber,\r\nconvert(varchar,convert(varchar,isnull(CustomerCode,''))+'-'+convert(varchar,isnull(CustomerName,''))) 'Cliente' into ##ordint\r\nfrom WMS_SAP_INTEGRATOR.dbo.[Order] \r\nwhere CreatedAt>=getdate ()-5 and ProcessedAt is not null AND synclog is null\r\nset nocount off\r\nselect \r\nom.number 'Carga',\r\nom.Box 'Box',\r\nwo.Cliente 'Cliente',\r\noi.DeliveryNumber 'Delivery',\r\nconvert(varchar,\r\nconvert(varchar,ad.warehouse)+'-'+convert(varchar,ad.Street)+'-'+convert(varchar,ad.Block)+'-'+convert(varchar,ad.Level)+'-'+convert(varchar,ad.Apartment)) 'Endereco',\r\np.code 'CODE_Produto',\r\np.name 'Name_Produto',\r\nc1.name 'Unidade de Separação',\r\nc2.name 'Unidade de Armazenagem',\r\nc3.name 'Unidade de Venda',\r\nconvert(numeric(10,2),sum(oi.MasterAmount)) 'Caixas',\r\nconvert(numeric(10,2),sum(oi.UnitAmount)) 'Unidade',\r\nconvert(numeric(10,2),sum(oi.Ammount)) 'Separado',\r\noi.factor 'Fator',\r\nconvert(varchar,ad.warehouse) Deps,\r\nconvert(varchar,ad.Street)Ruas,\r\nconvert(varchar,ad.Block)Blocos,\r\nconvert(varchar,ad.Level)Nivels,\r\nconvert(varchar,ad.Apartment) aptos,\r\nrank( )over( order by ad.warehouse,ad.Street,ad.Block,ad.Level,ad.Apartment, om.box, wo.cliente,p.code)SEQ,\r\ncase \r\n\twhen ad.Warehouse = 2 then 'Climatizado' \r\n\twhen (c1.name = 'UNI') and (p.GreatnessSeparation = 0) then 'Fracionado' else 'Grandeza' end Tipo\r\n from WMS_SAP.dbo.orderitem oi\r\njoin WMS_SAP.dbo.[order] o on o.id = oi.[order]\r\njoin ##ordint wo on \r\n\t\to.LoadNumber = wo.LoadNumber collate SQL_Latin1_General_CP1_CI_AS and \r\n\t\to.InvoiceNumber = wo.DeliveryNumber collate SQL_Latin1_General_CP1_CI_AS and \r\n\t\to.Number = wo.Number collate SQL_Latin1_General_CP1_CI_AS\r\njoin WMS_SAP.dbo.OrderManifest om on om.id = o.OrderManifest\r\njoin WMS_SAP.dbo.product p on p.id = oi.Product\r\njoin WMS_SAP.dbo.class c1 on c1.id = oi.Unit -- Unidade Pedido\r\njoin WMS_SAP.dbo.class c2 on c2.id = p.StorageUnit -- Unidade de Armazenagem\r\njoin WMS_SAP.dbo.class c3 on c3.id = p.SalesUnit -- Unidade de Venda\r\njoin WMS_SAP.dbo.Picking pi on pi.Product = p.Id\r\njoin WMS_SAP.dbo.Address ad on pi.Address = ad.id\r\nwhere om.date >=getdate()-2 and om.number = '" + manifesto.ToString() + "' and oi.MasterAmount > 0\r\ngroup by \r\n\tp.code,p.name ,oi.DeliveryNumber,ad.warehouse,ad.Street,ad.Block,ad.Level,ad.Apartment,c1.name,c2.name,c3.name,wo.Cliente,om.Number,om.Box ,oi.factor,p.GreatnessSeparation\r\norder by\r\n\tom.Box,oi.DeliveryNumber, Cliente,CODE_Produto";
            SqlCommand command = new SqlCommand(queryString, connectionWms);

            connectionWms.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                connectionWms.Close();
                return true;
            }
            else
            {
                connectionWms.Close();
                return false;
            }

        }

        public static void insereEmBancoASerImpresso(Destrinche destrinche)
        {
            string queryString = "insert into destrinche values('"+ destrinche.Manifesto + "','" + destrinche.Cliente + "','" + destrinche.Delivery + "','" + destrinche.CodProduto + "','" + destrinche.Produto + "','" + destrinche.Deposito +"','" + destrinche.Rua + "','" + destrinche.Bloco + "','" + destrinche.Nivel + "','" + destrinche.Apartamento + "','" + destrinche.Box + "','" + destrinche.Endereco + "'," + destrinche.NumeroEtiqueta + "," + destrinche.TotalEtiqueta + ")";
            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


        public static void updateTotalEtiquetasEmBancoASerImpresso(String Cliente, int total)
        {
            string queryString = "UPDATE destrinche SET totalEtiqueta = " + total.ToString() +" WHERE cliente = '" + Cliente.ToString() + "'";
            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public static void retornaEtiquetasDestrinche(String manifesto, String impressora)
        {
            List<Destrinche> listaItems = new List<Destrinche>();
            String ultimoCliente = "";
            int contagemProduto = 0;

            connectionWms.Open();

            String queryString = "set nocount on\r\nif exists (select * from tempdb..sysobjects where name = '##ordint') drop table ##ordint\r\n\r\nselect number, DeliveryNumber, LoadNumber,\r\nconvert(varchar,convert(varchar,isnull(CustomerCode,''))\r\n-- mudado aqui\r\n--+'-'+convert(varchar,isnull(CustomerName,''))\r\n)\r\n'Cliente' into ##ordint\r\nfrom WMS_SAP_INTEGRATOR.dbo.[Order] \r\nwhere CreatedAt>=getdate ()-5 and ProcessedAt is not null AND synclog is null\r\n\r\n\r\nif exists (select * from tempdb..sysobjects where name = '##resultado') drop table ##resultado\r\n\r\nselect \r\nom.number 'Carga',\r\nom.Box 'Box',\r\nwo.Cliente 'Cliente',\r\noi.DeliveryNumber 'Delivery',\r\nconvert(varchar,\r\nconvert(varchar,ad.warehouse)+'-'+convert(varchar,ad.Street)+'-'+convert(varchar,ad.Block)+'-'+convert(varchar,ad.Level)+'-'+convert(varchar,ad.Apartment)) 'Endereco',\r\np.code 'CODE_Produto',\r\np.name 'Name_Produto',\r\nc1.name 'Unidade de Separação',\r\nc2.name 'Unidade de Armazenagem',\r\nc3.name 'Unidade de Venda',\r\ncase when p.GreatnessSeparation = 1 \r\n\tthen convert(numeric(10,2),sum(oi.UnitAmount)) \r\n\telse convert(numeric(10,2),sum(oi.MasterAmount)) end 'Caixas',\r\ncase when p.GreatnessSeparation = 1\r\n\tthen convert(numeric(10,2),sum(oi.MasterAmount))\r\n\telse convert(numeric(10,2),sum(oi.UnitAmount)) end 'Unidade',\r\nconvert(numeric(10,2),sum(oi.Ammount)) 'Separado',\r\noi.factor 'Fator',\r\nconvert(varchar,ad.warehouse) Deps,\r\nconvert(varchar,ad.Street)Ruas,\r\nconvert(varchar,ad.Block)Blocos,\r\nconvert(varchar,ad.Level)Nivels,\r\nconvert(varchar,ad.Apartment) aptos,\r\nrank( )over( order by ad.warehouse,ad.Street,ad.Block,ad.Level,ad.Apartment, om.box, wo.cliente,p.code)SEQ,\r\ncase \r\n\t--when ad.Warehouse = 2 then 'Climatizado' \r\n\twhen (sum(oi.MasterAmount)>0 or p.GreatnessSeparation = 1) then 'Grandeza' else 'Fracionado' end Tipo into ##resultado\r\n from WMS_SAP.dbo.orderitem oi\r\njoin WMS_SAP.dbo.[order] o on o.id = oi.[order]\r\njoin ##ordint wo on \r\n\t\to.LoadNumber = wo.LoadNumber collate SQL_Latin1_General_CP1_CI_AS and \r\n\t\to.InvoiceNumber = wo.DeliveryNumber collate SQL_Latin1_General_CP1_CI_AS and \r\n\t\to.Number = wo.Number collate SQL_Latin1_General_CP1_CI_AS\r\njoin WMS_SAP.dbo.OrderManifest om on om.id = o.OrderManifest\r\njoin WMS_SAP.dbo.product p on p.id = oi.Product\r\njoin WMS_SAP.dbo.class c1 on c1.id = oi.Unit -- Unidade Pedido\r\njoin WMS_SAP.dbo.class c2 on c2.id = p.StorageUnit -- Unidade de Armazenagem\r\njoin WMS_SAP.dbo.class c3 on c3.id = p.SalesUnit -- Unidade de Venda\r\njoin WMS_SAP.dbo.Picking pi on pi.Product = p.Id\r\njoin WMS_SAP.dbo.Address ad on pi.Address = ad.id\r\n\r\n-- mudado daqui\r\n\r\nwhere om.date >=getdate()-2 \r\n\r\n--and om.number = 8028373507\r\n\r\n\r\n\r\ngroup by \r\n\tp.code,p.name ,oi.DeliveryNumber,ad.warehouse,ad.Street,ad.Block,ad.Level,ad.Apartment,c1.name,c2.name,c3.name,wo.Cliente,om.Number,om.Box ,oi.factor,p.GreatnessSeparation\r\n--order by om.Box, Cliente,CODE_Produto\r\n-- até aqui\r\ndelete from ##resultado where Tipo <> 'Grandeza'\r\nset nocount off\r\n\r\nselect Carga, Box, Cliente, Delivery,Endereco, CODE_Produto,Name_Produto,Caixas,Deps,Ruas,Blocos,Nivels,Aptos from ##resultado where carga = 8028373686 order by Cliente";
            SqlCommand command = new SqlCommand(queryString, connectionWms);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                

                String Manifesto = reader["Carga"].ToString();
                String Cliente = reader["Cliente"].ToString();
                String Delivery = reader["Delivery"].ToString();
                String CodProduto = reader["CODE_Produto"].ToString();
                String Produto = reader["Name_Produto"].ToString();
                String Deposito = reader["Deps"].ToString();
                String Rua = reader["Ruas"].ToString();
                String Bloco = reader["Blocos"].ToString();
                String Nivel = reader["Nivels"].ToString();
                String Apartamento = reader["aptos"].ToString();
                String Caixas = reader["Caixas"].ToString();
                String Endereco = reader["Endereco"].ToString();
                String Box = reader["Box"].ToString();
                int valorTotal = Convert.ToInt32(Double.Parse(Caixas));

                if(ultimoCliente != "" && ultimoCliente != Cliente)
                {
                    updateTotalEtiquetasEmBancoASerImpresso(ultimoCliente, contagemProduto);
                    contagemProduto = 0;
                }

                ultimoCliente = Cliente;

                for (var i = 0; i < valorTotal; i++)
                {
                    contagemProduto++;
                    Destrinche destrinche = new Destrinche(Manifesto, Cliente, Delivery, CodProduto, Produto, Deposito, Rua, Bloco, Nivel, Apartamento, Box, Endereco, contagemProduto, 0);
                    insereEmBancoASerImpresso(destrinche);
                }


                //imprimeItems(destrinche, valorTotal, impressora);
                /*
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
                */


                //System.Console.WriteLine(chave + "," + Categoria);
            }
            updateTotalEtiquetasEmBancoASerImpresso(ultimoCliente, contagemProduto);
            /*
            System.Console.WriteLine(listaItems.Count.ToString());
                for (var i = 0; i < listaItems.Count; i++)
                {
                    System.Console.WriteLine("Entrou");
                
                    var items = listaItems[i];

                    int itemAtual = i + 1;
                    int itemTotal = listaItems.Count;

                    imprimeItems(items, itemAtual, itemTotal, impressora);
                }
            */
            connectionWms.Close();
            System.Console.WriteLine("teste");
        }

        public static void imprimeItems(Destrinche destrinche, int volTotal, String impressora)
        {
            int metadeComprimento = destrinche.Produto.Length / 2;

            String produtoParte1 = destrinche.Produto.Substring(0, metadeComprimento);
            String produtoParte2 = destrinche.Produto.Substring(metadeComprimento);
            for(int i = 0; i < volTotal; i++) {
                int numeroEtiqueta = i + 1;

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
                "\r\n^FT557,308^A0N,50,55^FH\\^FD" + numeroEtiqueta.ToString() + "/" + volTotal.ToString() + "^FS" +
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
}