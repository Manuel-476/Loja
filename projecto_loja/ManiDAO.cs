using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace projecto_loja
{
    class ManiDAO:Conector
    {
        public static int id_usuario;
        public static int telas;
        public static string nome;
        private SqlCeDataAdapter adapt;
         
        private DataTable enche;

        public bool login(string nome,string senha)//metodo para fazer o login 
        {
            int linha_cont=0;
           
           
            SqlCeConnection con = conectar(); //Fazer conexao com a base de dados

            con.Open();
            adapt = new SqlCeDataAdapter("select * from usuario;",con);
            enche = new DataTable();
            adapt.Fill(enche);
            
            linha_cont=enche.Rows.Count;

           /* foreach (DataRow linhas in enche.Rows)
            {
                
                if (linhas["Nome"].ToString()==nome && linhas["senha"].ToString()==senha)
                {
                    

                }
             }*/
            for (int i = 0; i < linha_cont; i++)//listar a tabela usuario para comparar como os dados inseridos na variavel nome e senha
                if (enche.Rows[i]["Nome"].ToString() == nome && enche.Rows[i]["senha"].ToString() == senha)
                {

                    id_usuario = int.Parse(enche.Rows[i]["id_usuario"].ToString());
                    con.Close();
                    adapt.Dispose();
                    enche.Dispose();

                    return true;

                }

                   return false;     
             }

        public DataTable listar(string tabela) //listar cada tabela na base de dados consoante os nomes passados pelos parametros
        {
            SqlCeConnection con = conectar();
            SqlCeDataAdapter adapt=null;
            DataTable enche = new DataTable();

            if(tabela.ToUpper()=="usuario".ToUpper())
                adapt= new SqlCeDataAdapter("select * from usuario;",con);

            else if(tabela.ToUpper()=="produto".ToUpper())
                adapt= new SqlCeDataAdapter("select * from produto;",con);

            else if(tabela.ToUpper()=="venda".ToUpper())
                adapt = new SqlCeDataAdapter("select p.nome,v.quantidade_vendida,v.valor_pago,v.desconto,v.data_hora from venda as v join produto as p on p.id_produto=v.id_produto;", con);

            else if (tabela.ToUpper() == "estoque".ToUpper())
                adapt = new SqlCeDataAdapter("select e.id_produto,p.nome,e.quantidade,e.valor_gasto,e.data,p.preco from estoque as e  inner join produto as p on e.id_produto=p.id_produto;", con);

            else if (tabela.ToUpper() == "registro_diario".ToUpper())
                adapt = new SqlCeDataAdapter("select * from registro_diario;", con);


            adapt.Fill(enche);
            return enche;//retorna todos os registros da tabela selecionada

        }

        public DataTable listarpeloId(string tabela,int id) //listar cada tabela na base de dados consoante os nomes passados pelos parametros
        {
            SqlCeConnection con = conectar();
            SqlCeDataAdapter adapt = null;      
            DataTable enche = new DataTable();


         

            if (tabela.ToUpper() == "usuario".ToUpper())
                adapt = new SqlCeDataAdapter("select * from usuario where id_usuario="+id+";", con);

            else if (tabela.ToUpper() == "produto".ToUpper())
                adapt = new SqlCeDataAdapter("select * from produto where id_produto="+id+";", con);

            else if (tabela.ToUpper() == "venda".ToUpper())
                adapt = new SqlCeDataAdapter("select * from venda where id_produto=" + id + ";", con);

            else if (tabela.ToUpper() == "estoque".ToUpper())
                adapt = new SqlCeDataAdapter("select * from estoque where id_produto=" + id + ";", con);

            else if (tabela.ToUpper() == "registro_diario".ToUpper())
                adapt = new SqlCeDataAdapter("select * from registro_diario where id_produto=" + id + ";", con);


            adapt.Fill(enche);
            return enche;//retorna todos os registros da tabela selecionada

        }
        
        public void alterarEstoqueSemSoma(DAOmetodo mani) 
        {
            SqlCeConnection con = conectar();
            con.Open();
            SqlCeCommand insere = new SqlCeCommand();
            insere.Connection = con;

            insere.Parameters.AddWithValue("@nome", mani.getNomeproduto());
            insere.Parameters.AddWithValue("@marca", mani.getMarca());
            insere.Parameters.AddWithValue("@tipo", mani.getTipo());
            insere.Parameters.AddWithValue("@preco", mani.getPreco());
         
            insere.Parameters.AddWithValue("@vpago", mani.getVpago());
            insere.Parameters.AddWithValue("@vgasto", mani.getVgasto());
            insere.Parameters.AddWithValue("@data", mani.getData());
            insere.Parameters.AddWithValue("@idproduto", mani.getIdproduto());
            insere.Parameters.AddWithValue("@quantidade", mani.getQuantidade());
            
            

            insere.CommandText = "update  Estoque set quantidade=@quantidade,valor_gasto=@vgasto,data=@data where id_produto=@idproduto;";
            insere.ExecuteNonQuery();
                
        }

        public void Inserir(DAOmetodo mani,string tabela) 
        {   
            SqlCeConnection con= conectar();
            con.Open();
            SqlCeCommand insere=new SqlCeCommand();
            insere.Connection = con;

           int num= UltimoId("produto")+1;
           int id = UltimoIdUsuario() + 1;
           mani.setVpago( mani.getVpago() - mani.getDesconto());
          

            insere.Parameters.AddWithValue("@nome", mani.getNomeproduto());
            insere.Parameters.AddWithValue("@id_produto",num);
            insere.Parameters.AddWithValue("@id_produtos", num-1);//para o estoque
            insere.Parameters.AddWithValue("@marca",mani.getMarca());
            insere.Parameters.AddWithValue("@tipo",mani.getTipo());
            insere.Parameters.AddWithValue("@preco", mani.getPreco());
            insere.Parameters.AddWithValue("@nome_usuario", mani.getUsuario());
            insere.Parameters.AddWithValue("@senha", mani.getSenha());
            insere.Parameters.AddWithValue("@funcao", mani.getFuncao());
            insere.Parameters.AddWithValue("@vpago", (mani.getPreco()*mani.getQtdvendido())-mani.getDesconto());
            insere.Parameters.AddWithValue("@vgasto", mani.getVgasto());
            insere.Parameters.AddWithValue("@qtdvendido", mani.getQtdvendido());
            insere.Parameters.AddWithValue("@desconto", mani.getDesconto());
            insere.Parameters.AddWithValue("@data", mani.getData());
            insere.Parameters.AddWithValue("@idproduto", mani.getIdproduto());
            insere.Parameters.AddWithValue("@quantidade", mani.getQuantidade());
            insere.Parameters.AddWithValue("@pdia", mani.getPdia());
            insere.Parameters.AddWithValue("@gdia", mani.getGdia());
            insere.Parameters.AddWithValue("@totalvenda", mani.getTvenda());
            insere.Parameters.AddWithValue("@id",id);

            if (tabela.ToUpper() == "usuario".ToUpper())
            {
                insere.CommandText = "insert into usuario values (@id,@nome_usuario,@senha,@funcao);";
                insere.ExecuteNonQuery();
            }
               

            else if (tabela.ToUpper() == "produto".ToUpper())
            {
                insere.CommandText="insert into produto values(@id_produto,@nome,@marca,@tipo,@preco)";
                insere.ExecuteNonQuery();
            }

            else if (tabela.ToUpper() == "venda".ToUpper())
            {
                insere.CommandText="insert into venda values (@idproduto,@qtdvendido,@vpago,@desconto,@data)";
                insere.ExecuteNonQuery();
            }

            else if (tabela.ToUpper() == "estoque".ToUpper())
            {
                insere.CommandText = "insert into estoque values(@id_produtos,@quantidade,@vgasto,@data)";
                insere.ExecuteNonQuery();
            }

            else if (tabela.ToUpper() == "registro_diario".ToUpper())
            {
                insere.CommandText ="insert into Registro_diario values (@data,@pdia,@gdia,@totalvenda)";
                insere.ExecuteNonQuery();
            }

          

            insere.Dispose();
            con.Close();
            
        }//inserir dados na tabela mediante os nomes passados por parametros 

        public void alterartabelas(string tabela,DAOmetodo mani,int num) 
        {
            SqlCeConnection con = conectar();
            con.Open();
            SqlCeCommand insere = new SqlCeCommand();
            insere.Connection = con;

            insere.Parameters.AddWithValue("@nome", mani.getNomeproduto());
            insere.Parameters.AddWithValue("@id_produto", num);
            insere.Parameters.AddWithValue("@marca", mani.getMarca());
            insere.Parameters.AddWithValue("@tipo", mani.getTipo());
            insere.Parameters.AddWithValue("@preco", mani.getPreco());
            insere.Parameters.AddWithValue("@nome_usuario", mani.getUsuario());
            insere.Parameters.AddWithValue("@senha", mani.getSenha());
            insere.Parameters.AddWithValue("@funcao", mani.getFuncao());
            insere.Parameters.AddWithValue("@vpago", mani.getVpago());
            insere.Parameters.AddWithValue("@vgasto", mani.getVgasto());
            insere.Parameters.AddWithValue("@qtdvendido", mani.getQtdvendido());
            insere.Parameters.AddWithValue("@desconto", mani.getDesconto());
            insere.Parameters.AddWithValue("@data", mani.getData());
            insere.Parameters.AddWithValue("@idproduto", mani.getIdproduto());
            insere.Parameters.AddWithValue("@quantidade", mani.getQuantidade());
            insere.Parameters.AddWithValue("@pdia", mani.getPdia());
            insere.Parameters.AddWithValue("@gdia", mani.getGdia());
            insere.Parameters.AddWithValue("@totalvenda", mani.getTvenda());
            insere.Parameters.AddWithValue("@id", num);

            if (tabela.ToUpper() == "usuario".ToUpper())
            {
                insere.CommandText = "update usuario set nome=@nome_usuario,senha=@senha,funcao=@funcao where id_usuario=@id;";
                insere.ExecuteNonQuery();
            }

            else if (tabela.ToUpper() == "produto".ToUpper())
            {
                insere.CommandText = "update  produto set nome=@nome,marca=@marca,tipo=@tipo,preco=@preco WHERE id_produto=@idproduto;";
                insere.ExecuteNonQuery();
                
            }

            else if (tabela.ToUpper() == "venda".ToUpper())
            {
                insere.CommandText = "update venda set quantidade_vendida=@qtdvendido,valor_pago=@vpago,desconto=@desconto,data_hora=@data where id_produto=@idproduto;";
                insere.ExecuteNonQuery();
            }

            else if (tabela.ToUpper() == "estoque".ToUpper())
            {
                insere.CommandText = "update  Estoque set quantidade=quantidade+@quantidade,valor_gasto=valor_gasto+@vgasto,data=@data where id_produto=@idproduto;";
                insere.ExecuteNonQuery();
                insere.CommandText = "insert into gastos values (@idproduto,@vgasto,@data);";
                insere.ExecuteNonQuery();
                
            }
           

            insere.Dispose();
            con.Close();
            
        }//alterar dados na tabela mediante os nomes passados por parametros 

        public void alteraestoque(DAOmetodo alter) //Reducao na quantidade do estoque apos  venda
        {
            SqlCeConnection con = conectar();
            con.Open();
            SqlCeCommand alt = new SqlCeCommand();
            alt.Connection = con;
            alt.Parameters.AddWithValue("@qtd",alter.getQtdvendido());
            alt.Parameters.AddWithValue("@id",alter.getIdproduto());
            alt.CommandText = "update estoque set quantidade=quantidade-@qtd where id_produto=@id";
            alt.ExecuteNonQuery();

        }

        public void alteraRegistro(string data,DAOmetodo dados)
        {
            SqlCeConnection con = conectar();
            SqlCeCommand insere = new SqlCeCommand();
            insere.Connection = con;

            insere.Parameters.AddWithValue("@data",data);
            insere.Parameters.AddWithValue("@pdia",dados.getPdia());
            insere.Parameters.AddWithValue("@gdia",dados.getGdia());
            insere.Parameters.AddWithValue("@totalvenda",dados.getTvenda());

            insere.CommandText = "update set perdas_dia=@pdia,ganhos_dia=@gdia,total_venda=@totalvenda where data=@data;";
            insere.ExecuteNonQuery();
            insere.Dispose();
            con.Close();
        }  //alterar dados na tabela Registros
       
        public void apagar(int id,string tabela)
       {
           SqlCeConnection con = conectar();
           SqlCeCommand insere = new SqlCeCommand();
           con.Open();
           insere.Connection = con;

           insere.Parameters.AddWithValue("@id",id);

           if (tabela.ToUpper() == "usuario".ToUpper())
               // insere = new SqlCeCommand(usuari, con);
               id = 2;

           else if (tabela.ToUpper() == "produto".ToUpper())
           {
               insere.CommandText = "delete from produto where id_produto=@id;";
               insere.ExecuteNonQuery();
           }

           else if (tabela.ToUpper() == "venda".ToUpper())
           {
               insere.CommandText = "delete from venda where id_produto=@id;";
               insere.ExecuteNonQuery();
           }

           else if (tabela.ToUpper() == "estoque".ToUpper())
           {
               insere.CommandText = "delete from  Estoque where id_produto=@id;";
               insere.ExecuteNonQuery();
           }

    
           insere.Dispose();
           con.Close();
      } //apagar registros na tabela mediante os nomes passados por parametros 

        public void apagaRegistro(string data) 
        {
            SqlCeConnection con = conectar();
            con.Open();
            SqlCeCommand insere = new SqlCeCommand();
            insere.Connection = con;

            insere.Parameters.AddWithValue("@data",data);

            insere.CommandText = "delete from Registro_diario where data=@data;";
            insere.ExecuteNonQuery();

            insere.Dispose();
            con.Close();

        }//apagar resitros na tabela Registro_diario 
    }
  
}
