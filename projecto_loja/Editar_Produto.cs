using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace projecto_loja
{
    public partial class Editar_Produto : Form
    {
        public Editar_Produto()
        {
            InitializeComponent();
        }

        private void Editar_Produto_Load(object sender, EventArgs e)
        {
            ManiDAO controlo = new ManiDAO();
            try
            {
                DataTable enche = controlo.listarpeloId("produto", DAOmetodo.id);

                textNome.Text = enche.Rows[0]["Nome"].ToString();
                comboTipo.Text = enche.Rows[0]["tipo"].ToString();
                comboMarca.Text = enche.Rows[0]["marca"].ToString();
                textPreco.Text = enche.Rows[0]["preco"].ToString();

                DataTable enc = controlo.listarpeloId("estoque", DAOmetodo.id);
                textQtd.Text = enc.Rows[0]["Quantidade"].ToString();
                dateTimePicker1.Text = enc.Rows[0]["data"].ToString();
                textVgasto.Text = enc.Rows[0]["valor_gasto"].ToString();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Impossivel Executar a Acção", erro.Message, MessageBoxButtons.OK);
                return;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DAOmetodo metodo = new DAOmetodo();
            ManiDAO controlo = new ManiDAO();

            try
            {
                metodo.setNomeproduto(textNome.Text.ToString());
                metodo.setPreco(float.Parse(textPreco.Text.ToString()));
                metodo.setTipo(comboTipo.Text.ToString());
                metodo.setMarca(comboMarca.Text.ToString());

                metodo.setQuantidade(int.Parse(textQtd.Text.ToString()));
                metodo.setVgasto(float.Parse(textVgasto.Text.ToString()));
                metodo.setData(dateTimePicker1.Text.ToString());
                metodo.setIdproduto(DAOmetodo.id);

                controlo.alterartabelas("produto", metodo, 0);
                controlo.alterarEstoqueSemSoma(metodo);
                MessageBox.Show("Feito com Sucesso", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception erro)
            {
                MessageBox.Show("Impossivel Executar a Acção", erro.Message, MessageBoxButtons.OK);
                return;
            }
        }
    }
}
