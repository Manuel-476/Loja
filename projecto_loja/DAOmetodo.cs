using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace projecto_loja
{
    class DAOmetodo
    {
        private int id_produto;
        private int quantidade;
        private string nome_produto;
        private string data;
        private string marca;
        private string tipo;
        private string usuario;
        private string senha;
        private string funcao;
        private float preco;
        private float qtd_vendido;
        private float desconto;
        private float vgasto;
        private float vpago;
        private float ganho_dia;
        private float perda_dia;
        private float total_venda;
        public static int id;

        public void setIdproduto(int id_produto) 
         {
            this.id_produto = id_produto;
         }

        public void setQuantidade(int quantidade)
         {
            this.quantidade = quantidade;
         }

        public void setUsuario(string usuario)
         {
            this.usuario = usuario;
         }

        public void setSenha(string senha)
        {
            this.senha = senha;
        }

        public void setFuncao(string funcao)
        {
            this.funcao = funcao;
        }


        public void setNomeproduto(string nome_produto)
         {
            this.nome_produto = nome_produto;
         }

        public void setData(string data)
         {
            this.data = data;
         }

        public void setMarca(string marca)
         {
            this.marca = marca;
         }

        public void setTipo(string tipo)
         {
            this.tipo = tipo;
         }

        public void setGdia(float ganho_dia)
        {
            this.ganho_dia = ganho_dia;
        }

        public void setPdia(float perda_dia)
        {
            this.perda_dia = perda_dia;
        }
        public void setTvenda(float total_venda)
        {
            this.total_venda = total_venda;
        }

        public void setPreco(float preco)
         {
            this.preco = preco;
         }

        public void setQtdvendido(float qtd_vendido)
        {
            this.qtd_vendido = qtd_vendido;
        }

        public void setDesconto(float desconto)
         {
            this.desconto = desconto;
         }

        public void setVgasto(float vgasto)
         {
            this.vgasto = vgasto;
         }

        public void setVpago(float vpago)
         {
            this.vpago = vpago;
         }
        //============================GET================================================
        public int getIdproduto()
        {
            return this.id_produto;
        }

        public int getQuantidade()
        {
           return this.quantidade;
        }

        public string getUsuario()
        {
            return this.usuario;
        }

        public string getSenha()
        {
            return this.senha;
        }

        public string getFuncao()
        {
            return this.funcao;
        }

        public string getNomeproduto()
        {
            return this.nome_produto;
        }

        public string getData()
        {
            return this.data;
        }

        public string getMarca()
        {
            return this.marca;
        }

        public string getTipo()
        {
            return this.tipo;
        }


        public float getGdia()
        {
            return this.ganho_dia;
        }

        public float getPdia()
        {
            return this.perda_dia;
        }

        public float getTvenda()
        {
            return this.total_venda;
        }

        public float getPreco()
        {
            return this.preco;
        }

        public float getQtdvendido()
        {
            return this.qtd_vendido;
        }

        public float getDesconto()
        {
            return this.desconto;
        }

        public float getVgasto()
        {
            return this.vgasto;
        }

        public float getVpago()
        {
            return this.vpago;
        }
    }
}
