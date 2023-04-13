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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Banco banco = Dados.retornaBanco();
            textoImpressora.Text = banco.NomeImpressora;
            textIp.Text = banco.Ip;
            textUsuario.Text = banco.Usuario;
            textSenha.Text = banco.Senha;
            textNomeBanco.Text = banco.NomeBanco;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dados.salvaNovaConfig(textoImpressora.Text, textIp.Text, textUsuario.Text,
                textSenha.Text, textNomeBanco.Text);
            MessageBox.Show("Configuração salva com sucesso!");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
