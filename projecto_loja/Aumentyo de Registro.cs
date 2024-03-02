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
    public partial class Aumento_de_Registro : Form
    {
        int id=0;
        DAOmetodo metodo = new DAOmetodo();

        public Aumento_de_Registro()
        {
            InitializeComponent();
        }

        private void Aumento_de_Registro_Load(object sender, EventArgs e)
        {
            ManiDAO listar=new ManiDAO();

            dataGridView1.DataSource = listar.listar("estoque");
            dataGridView1.Columns["id_produto"].Visible = false;
            novo.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\imagens\add-image-icon-png-5.jpg");
            eliminar.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\imagens\delete-icon-vector-9.png");
            Editar.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\imagens\icon-edit-11.jpg.jpg");

        }

        private void voltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sim_Click(object sender, EventArgs e)
        {
            
            
            ManiDAO inserir = new ManiDAO();
            try
            {
                metodo.setData(dateTimePicker1.Text.ToString());
                metodo.setVgasto(float.Parse(textVgasto.Text.ToString()));
                metodo.setQuantidade(int.Parse(textQtd.Text.ToString()));
                inserir.alterartabelas("estoque", metodo, id);//alterar o produto
                dataGridView1.DataSource = inserir.listar("estoque");//actualizar lista
                dataGridView1.Columns["id_produto"].Visible = false;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Impossivel Executar a Acção", erro.Message, MessageBoxButtons.OK);
                return;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells["id_produto"].Value.ToString() == "")
                return;
             metodo.setIdproduto(int.Parse(dataGridView1.Rows[e.RowIndex].Cells["id_produto"].Value.ToString()));
             DAOmetodo.id= int.Parse(dataGridView1.Rows[e.RowIndex].Cells["id_produto"].Value.ToString());
             produto.Text = dataGridView1.Rows[e.RowIndex].Cells["nome"].Value.ToString(); 
        }

        private void novo_Click(object sender, EventArgs e)
        {
            Registro_produto r_produto = new Registro_produto();

            r_produto.ShowDialog();
                
        }

        private void Editar_Click(object sender, EventArgs e)
        {

            if (DAOmetodo.id <= 0)
                return;

                Editar_Produto editar = new Editar_Produto();
                editar.ShowDialog();
             
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ManiDAO listar = new ManiDAO();

            dataGridView1.DataSource = listar.listar("estoque");
            dataGridView1.Columns["id_produto"].Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ManiDAO listar = new ManiDAO();

            if (MessageBox.Show("Atenção", "Tens Certeza que Pretendes Eliminar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            listar.apagar(metodo.getIdproduto(),"estoque");
            listar.apagar(metodo.getIdproduto(),"produto");

            MessageBox.Show("Eliminado", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dataGridView1.DataSource = listar.listar("estoque");
            dataGridView1.Columns["id_produto"].Visible = false;
        }
        
    }
}
