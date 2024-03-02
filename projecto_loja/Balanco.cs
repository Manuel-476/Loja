using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Windows.Forms.DataVisualization.Charting;


namespace projecto_loja
{
    public partial class Balanco : Form
    {
        public Balanco()
        {
            InitializeComponent();
        }

        private void Balanco_Load(object sender, EventArgs e)
        {
            DataTable valor = new DataTable();
            ManiDAO metodo = new ManiDAO();
            Title titulo = new Title();
            titulo.Font = new Font("Arial",23,FontStyle.Bold);
            titulo.ForeColor = Color.White;
            titulo.Text = "Balanço de Vendas";

            chart1.ChartAreas["ChartArea1"].AxisX.Title = "Ano 2022";
            chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new Font("Arial",12,FontStyle.Bold);
            chart1.ChartAreas["ChartArea1"].AxisX.TitleForeColor = Color.White;

            chart1.ChartAreas["ChartArea1"].AxisY.Title = "Quantidade Vendida";
            chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new Font("arial",12,FontStyle.Bold);
            chart1.ChartAreas["ChartArea1"].AxisY.TitleForeColor = Color.White;

            chart1.Titles.Add(titulo);
            chart1.ForeColor = Color.White;
            
            
            chart1.ChartAreas["ChartArea1"].AxisX2.TitleForeColor = Color.White;

            chart1.Series.Add("vendas");
            chart1.Series["vendas"].LegendText = "Venda";
            chart1.Series["vendas"].ChartType = SeriesChartType.Column;
            chart1.Series["vendas"].BorderWidth = 4;
            chart1.Series["vendas"].LabelForeColor= Color.White;
            chart1.Series["vendas"].LabelBackColor = Color.White;
            
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
           

            valor = metodo.listar("Registro_diario");
            foreach(DataRow c in valor.Rows)
            {
                chart1.Series["vendas"].Points.AddXY(c["data"].ToString(), int.Parse(c["total_venda"].ToString()));
          
            }
            dataGridView1.DataSource = valor;
        }
    }
}
