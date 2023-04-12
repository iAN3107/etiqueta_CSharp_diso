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
            textoLog.Text = "LOG DE IMPRESSÃO";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                String impressora = Dados.retornaImpressora();
                String manifesto = textoManifesto.Text;
                if (!Dados.verificaExistenciaManifesto(manifesto))
                {
                    MessageBox.Show("Este código de manifesto não existe!");
                }
                else
                {
                    int totalEtiquetas = Dados.retornaEtiquetasDestrinche(manifesto, impressora);
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
                MessageBox.Show(ex.ToString());
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
    }
}