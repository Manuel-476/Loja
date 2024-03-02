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
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aumento_de_Registro r_produto = new Aumento_de_Registro();

            if (this.MdiChildren.Length != 0)
                this.ActiveMdiChild.Close();

            r_produto.MdiParent = this;
            r_produto.Show();
            
        }

        private void vendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
           
            venda tela = new venda();

            if (this.MdiChildren.Length!=0)
                 this.ActiveMdiChild.Close();

            tela.MdiParent = this;
            tela.Show();

        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Usuario tela = new Usuario();

            tela.ShowDialog();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            ManiDAO rs = new ManiDAO();
            Tela_login tela = new Tela_login();
            tela.MdiParent = this;

            tela.Show();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }

        private void balançoTotalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Balanco f = new Balanco();
            if (this.MdiChildren.Length != 0)
                this.ActiveMdiChild.Close();

            f.MdiParent = this;
            f.Show();
        }
    }
}
