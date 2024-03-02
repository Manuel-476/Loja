using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.IO;
using System.Data;

namespace projecto_loja
{
   abstract class Conector
    {
         protected SqlCeConnection conectar()
        {
           
           SqlCeConnection con = new SqlCeConnection();
           con.ConnectionString = @"Data Source=C:\Users\DELL\Pictures\Visual Studio 2012\Projects\projecto_loja\base_de_dados\proj_loja.sdf";
           return con;
        }

         protected int UltimoId(string tabela) 
         {
             int result=0;
             SqlCeConnection con = conectar();
             con.Open();
             SqlCeDataAdapter adapt = new SqlCeDataAdapter("select max(id_produto) as maximo from "+tabela,con);
             DataTable enche = new DataTable();
             adapt.Fill(enche);
             if (enche.Rows[0]["maximo"].ToString()!=null)
                  result=int.Parse(enche.Rows[0]["maximo"].ToString());
          

             return result; 

         }
         protected int UltimoIdUsuario() 
         {
             int id = 0;

             DataTable enche = new DataTable();
             SqlCeConnection con = conectar();
             SqlCeDataAdapter adapt = new SqlCeDataAdapter("select max(id_usuario) as id from usuario;",con);

             adapt.Fill(enche);

             if (enche.Rows[0]["id"].ToString()!=null)
              id=int.Parse(enche.Rows[0]["id"].ToString()); 

             return id;
         }
    }
}
