using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace projecto_loja
{
    public partial class Usuario : Form
    {
        public Usuario()
        {
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ManiDAO met = new ManiDAO();
            DAOmetodo busca = new DAOmetodo();
            if (textSenha.Text.ToString() == textConf.Text.ToString()) 
            {
                busca.setUsuario(textNome.Text.ToString());
                busca.setFuncao(textFuncao.Text.ToString());
                busca.setSenha(textSenha.Text.ToString());
                met.Inserir(busca,"usuario");
                MessageBox.Show("Funcionario Cadastrado","Feito com Sucesso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
                 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
