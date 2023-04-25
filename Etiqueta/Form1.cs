using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
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
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

            // Loop através de cada interface e imprimir seu endereço MAC
            foreach (NetworkInterface ni in interfaces)
            {
                String mac = ni.GetPhysicalAddress().ToString();
                if (mac == "A4BB6DBF8B36" || mac == "D09466D90E68") {
                    textoLog.Text = "LOG DE IMPRESSÃO";
                    return;
                } else
                {
                    MessageBox.Show("Este aplicativo não é liberado para essa máquina!");
                    this.Close();
                }
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                String impressora = Dados.retornaImpressora();
                String manifesto = textoManifesto.Text;
                String separador = textSeparador.Text;
                if (!Dados.verificaExistenciaManifesto(manifesto))
                {
                    MessageBox.Show("Este código de manifesto não existe!");
                }
                else
                {
                    var impressaoEtiquetas = Dados.retornaEtiquetasDestrinche(manifesto, impressora, separador.ToUpper());

                    if (checkboxSemPallet.Checked) {
                        impressaoEtiquetas = ImpressaoEtiquetasSemPallet.retornaEtiquetasDestrinche(manifesto, impressora, separador.ToUpper());
                    }

                    int totalEtiquetas = impressaoEtiquetas;
                    DateTime now = DateTime.Now;
                    string data = now.ToShortDateString();
                    string hora = now.ToShortTimeString();
                    textoLog.Text = textoLog.Text + "\r\n\r\n" + data + "  " + hora;
                    textoLog.Text = textoLog.Text + "\r\n" + "Manifesto: " + manifesto;
                    textoLog.Text = textoLog.Text + "\r\n" + "Etiquetas sendo emitidas: " + totalEtiquetas;

                    button1.Enabled = true;
                }
                button1.Enabled = true;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
            }
            //Dados.retornaEtiquetasDestrinche();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void configuraçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void textoManifesto_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


    }
}