using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Etiqueta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RawPrinterHelper.SendStringToPrinter(szPrinterName: @"\\disorec001\Zebra1", szString:
                "\u0010CT~~CD,~CC^~CT~" +
                "\r\n^XA~TA000~JSN^LT0^MNW^MTD^PON^PMN^LH0,0^JMA^PR2,2~SD15^JUS^LRN^CI0^XZ" +
                "\r\n^XA" +
                "\r\n^MMT" +
                "\r\n^PW799" +
                "\r\n^LL0320" +
                "\r\n^LS0" +
                "\r\n^FT329,33^A0N,28,28^FH\\^FDMANIFESTO^FS" +
                "\r\n^FT264,80^A0N,56,55^FH\\^FD8028337703^FS" +
                "\r\n^FT122,100^A0N,28,28^FB100,1,0,C^FH \\^FDCLIENTE^FS" +
                "\r\n^FT37,148^A0N,56,55^FB270,1,0,C^FH\\^FD0005687202^FS" +
                "\r\n^FT564,100^A0N,28,28^FH\\^FDDELIVERY^FS" +
                "\r\n^FT488,148^A0N,56,55^FH \\^FD8028337703^FS" +
                "\r\n^FO0,0^GB799,320,8^FS" +
                "\r\n^FT31,281^A0N,28,31^FH\\^FDD^FS" +
                "\r\n^FT129,281^A0N,28,31^FH\\^FDR^FS" +
                "\r\n^FT234,281^A0N,28,31^FH\\^FDB^FS" +
                "\r\n^FT336,281^A0N,28,31^FH \\^FDN^FS" +
                "\r\n^FT446,281^A0N,28,31^FH\\^FDA^FS" +
                "\r\n^FT26,309^A0N,28,31^FH\\^FD01^FS" +
                "\r\n^FT122,309^A0N,28,31^FH\\^FD01^FS" +
                "\r\n^FT227,309^A0N,28,31^FH\\^FD01^FS" +
                "\r\n^FT331,309^A0N,28,31^FH\\^FD01^FS" +
                "\r\n^FT440,309^A0N,28,31^FH\\^FD01^FS" +
                "\r\n^FT557,308^A0N,50,55^FH\\^FD01/01^FS" +
                "\r\n^FT148,184^A0N,28,28^FH\\^FDCOD^FS" +
                "\r\n^FT66,230^A0N,56,55^FB216,1,0,C^FH\\^FD12325231^FS" +
                "\r\n^FT494,178^A0N,28,28^FH\\^FDPRODUTO^FS" +
                "\r\n^FT339,207^A0N,29,38^FB437,1,0,C^FH\\^FDPASSATEMPO Bisc^FS" +
                "\r\n^FT339,243^A0N,29,38^FB437,1,0,C^FH\\^FDRecheado Choc 70x130g BR^FS" +
                "\r\n^FT26,47^A0N,28,28^FH\\^FD31/03/2023^FS" +
                "\r\n^FT693,47^A0N,28,28^FH\\^FD16:37^FS" +
                "\r\n^PQ1,0,1,Y^XZ\r\n");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

