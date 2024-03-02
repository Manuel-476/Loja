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
    public partial class Tela_login : Form
    {
        public Tela_login()
        {
            InitializeComponent();
        }


        private void txtsenha_TextChanged(object sender, EventArgs e)
        {
            if (txtsenha.Text.Length < 8)
                errorProvider1.SetError(txtsenha,"Senha de 8 ou mais dígitos");
            else
                errorProvider1.SetError(txtsenha, "");
        }

        private void BotaoOK_Click_1(object sender, EventArgs e)
        {
            ManiDAO rs= new ManiDAO();
            string nome_usuario= txtn_usuario.Text.ToString();
            string palavra_passe= txtsenha.Text.ToString();


            if (rs.login(nome_usuario, palavra_passe) == true)
            {
                Inicio inicio = new Inicio();
                editar_usuario edita = new editar_usuario();
                this.Hide();
                int num;
              
                if(ManiDAO.telas>0)
                {
                    edita.ShowDialog();
                } 
                else if (ManiDAO.telas <1)
                {  
                    ManiDAO.telas=ManiDAO.id_usuario;
                    
                    inicio.ShowDialog();
                }
                
            }
              
        }

        private void Tela_login_Load(object sender, EventArgs e)
        {
            BotaoOK.BackgroundImage = Image.FromFile(Environment.CurrentDirectory + @"\imagens\icon-door-4.jpg");
        }

       
    }
}
