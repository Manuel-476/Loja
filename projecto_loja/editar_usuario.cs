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
    public partial class editar_usuario : Form
    {
        public editar_usuario()
        {
            InitializeComponent();
        }

        private void editar_usuario_Load(object sender, EventArgs e)
        {
            ManiDAO rs = new ManiDAO();
            DataTable dado = rs.listarpeloId("usuario", ManiDAO.id_usuario);

            textNome.Text=dado.Rows[0]["nome"].ToString();
            textFuncao.Text = dado.Rows[0]["funcao"].ToString();
            textSenha.Text=dado.Rows[0]["senha"].ToString();

           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ManiDAO rs = new ManiDAO();
            DAOmetodo met = new DAOmetodo();

            if (textSenha.Text == textConf.Text)
            {
                met.setUsuario(textNome.Text.ToString());
                met.setFuncao(textFuncao.Text.ToString());
                met.setSenha(textSenha.Text.ToString());

                rs.alterartabelas("usuario",met,ManiDAO.id_usuario);
                MessageBox.Show("   Alteração Feita", "Feito com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
