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
    public partial class Registro_produto : Form
    {
        public Registro_produto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ManiDAO controlo = new ManiDAO();
            ManiDAO controlo1 = new ManiDAO();
            DAOmetodo metodo = new DAOmetodo();
            try
            {
                metodo.setNomeproduto(textNome.Text.ToString());
                metodo.setPreco(float.Parse(textPreco.Text.ToString()));
                metodo.setTipo(comboTipo.Text.ToString());
                metodo.setMarca(comboMarca.Text.ToString());
                controlo.Inserir(metodo, "produto");

                metodo.setQuantidade(int.Parse(textQtd.Text.ToString()));
                metodo.setVgasto(float.Parse(textVgasto.Text.ToString()));
                metodo.setData(dateTimePicker1.Text.ToString());
                controlo1.Inserir(metodo, "estoque");

                MessageBox.Show("Feito com Sucesso","Concluído",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception erro)
            {
                MessageBox.Show( erro.Message,"Impossivel Executar a Acção", MessageBoxButtons.OK);
                return;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Registro_produto_Load(object sender, EventArgs e)
        {

        }
    }
}
