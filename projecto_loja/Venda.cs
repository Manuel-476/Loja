using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace projecto_loja
{
    public partial class venda : Form
    { 
        DAOmetodo met =new DAOmetodo();
        List<DAOmetodo> lista=new List<DAOmetodo>();

        public venda()
        {
            InitializeComponent();
        }

        private void SIM_Click(object sender, EventArgs e)
        {
            ManiDAO insere = new ManiDAO();
            DAOmetodo novo = new DAOmetodo(); 
            DateTime data = new DateTime();
            int id;
            

            met.setVpago(float.Parse(textPagar.Text.ToString()));
            met.setDesconto(float.Parse(textdesconto.Text.ToString()));
            met.setQtdvendido(int.Parse(textqtd.Value.ToString()));
            met.setData(/*data.Date.ToShortDateString()*/"2022-07-11");
           // MessageBox.Show(/*data.Date.ToShortDateString()*/);
            
            if (met.getIdproduto() <= 0)
                return;
            novo.setNomeproduto(met.getNomeproduto());
            novo.setPreco(met.getPreco());
            novo.setQtdvendido(met.getQtdvendido());
            novo.setVpago(met.getVpago());
            novo.setDesconto(met.getDesconto());

            insere.Inserir(met,"venda");
            insere.alteraestoque(met);
            lista.Add(novo);
            dataGridView1.DataSource = insere.listar("estoque");
            
            //folha.Print();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DAOmetodo novo = new DAOmetodo();
            if (dataGridView1.Rows[e.RowIndex].Cells["id_produto"].Value.ToString()=="")
                return;
             met.setIdproduto(int.Parse(dataGridView1.Rows[e.RowIndex].Cells["id_produto"].Value.ToString()));
             met.setNomeproduto(dataGridView1.Rows[e.RowIndex].Cells["Nome"].Value.ToString());
             met.setPreco(float.Parse(dataGridView1.Rows[e.RowIndex].Cells["preco"].Value.ToString()));
             
        }

        private void venda_Load(object sender, EventArgs e)
        {
            ManiDAO mostrar = new ManiDAO();
            DataTable enche = mostrar.listar("estoque");
            DataTable enche2 = mostrar.listar("venda");
            dataGridView1.DataSource = enche;
            dataGridView2.DataSource = enche2;
            dataGridView1.Columns["valor_gasto"].Visible = false;
            dataGridView1.Columns["data"].Visible = false;

            textdesconto.Text = "00";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ManiDAO mostrar = new ManiDAO();
            
            DataTable enche2 = mostrar.listar("venda");
            dataGridView2.DataSource = enche2;
            dataGridView1.Columns["valor_gasto"].Visible = false;
            dataGridView1.Columns["data"].Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Voltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            folha.Print();
        }

        private void folha_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int i = 0;
            float troco=0;
            float precototal=0;
            float vpagototal = 0;
            float descontototal=0;

            ManiDAO usu = new ManiDAO();

            DataTable tab=usu.listarpeloId("usuario",ManiDAO.telas);//Buscar nome do usuario apartir do Id
            string nome = tab.Rows[0]["Nome"].ToString();//Passar o nome do usuario parar uma variavel
            string titulo = "FACTURA";
            string conteudo = "Total:\n\nEntregue:\n\nDesconto:\n\nTroco";//Nome de cada registro  na factura
            string sub = "Nome Produto                    Quantidade                      Preço";//Os tituloos das informacoes na factura
            string valores;

            Font letra = new Font("Arial",18,FontStyle.Bold,GraphicsUnit.Pixel);//tipo de letra
            SolidBrush cor = new SolidBrush(Color.Black);
            Pen lapis = new Pen(cor);//lapios para desenho dos rectangulos

            Rectangle posicaot = new Rectangle(50, 0, e.MarginBounds.Width, 30);//Rectangulo para posicao do titulo
            Rectangle subt = new Rectangle(50, 35, e.MarginBounds.Width, 20);//Rectangulos para posicao dos subititulos ou seja para a variavel sub
            Rectangle pos2;
            Rectangle pos3;
            Rectangle pos4;
            Rectangle pos5;
    
            StringFormat alinhar= new StringFormat();//alinhamento usado para o centro da folha
            StringFormat alinhar2 = new StringFormat();//alinhamento usado para o inicio da folha
            alinhar.Alignment = StringAlignment.Center;
            alinhar.LineAlignment = StringAlignment.Center;
            alinhar2.LineAlignment = StringAlignment.Near;

            e.Graphics.DrawString(titulo,letra,cor,posicaot,alinhar);//Desnho do titulo na factura
            e.Graphics.DrawString(sub,letra,cor,subt,alinhar);//Desenho do Sutitulo
            foreach (DAOmetodo n in lista)//listar todas as vendas feitas de um determinado cliente
            {
                precototal += n.getPreco()*n.getQtdvendido();//Calcular o preco total das somas de todas as vendas
                vpagototal += n.getVpago();
                descontototal += n.getDesconto();
                string res = n.getNomeproduto() + "                      " + n.getQtdvendido().ToString() + "                      " + n.getPreco().ToString();//Conteudo das vendas ou registros
                pos2 = new Rectangle(50, 55 + i, e.MarginBounds.Width , 20);//Posicao dos registros da venda
                e.Graphics.DrawString(res,letra,cor,pos2,alinhar);//Desenho do registros
                e.Graphics.DrawRectangle(lapis, pos2);//Desnho dos rectangulos dos registros
                i =i+ 20;
            }

            troco = vpagototal-(precototal - descontototal);//Calculo do troco
            pos3 = new Rectangle((110 + e.MarginBounds.Width) / 2, 65 + i, e.MarginBounds.Width / 2, 190);//posicao dos informacoes de registros numericos da venda
            pos4 = new Rectangle(50, 65 + i, e.MarginBounds.Width / 2, 190);//posicao dos registros numericos da venda
            e.Graphics.DrawRectangle(lapis,posicaot);

            
            
           
              valores =  precototal+"\n\n"+vpagototal+"\n\n"+descontototal.ToString()+ "\n\n"+troco+"\n\n";//informacoes dos registros numericos
              e.Graphics.DrawString(valores,letra,cor,pos3,alinhar2);//Desenho dos registros numerico
              e.Graphics.DrawString(conteudo,letra,cor,pos4,alinhar2);//Densenho das informacoes de registros numericos
              pos5 = new Rectangle(50,275+1,e.MarginBounds.Width,90);////Posicao do reodape

              e.Graphics.DrawString("Tel:\nNIF:00000000000\nFuncionario:"+nome+"\nObrigado Volte Sempre...",letra,cor,pos5,alinhar);//dESENHO DO rODAPE
           // e.Graphics.DrawRectangle(lapis,pos3);
            
        }

        private void textdesconto_TextChanged(object sender, EventArgs e)
        {
            if (textdesconto.TextLength == 0)
                textdesconto.Text = "00";
        }
    }
}
