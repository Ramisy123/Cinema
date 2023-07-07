using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace WindowsFormsApplication2
{
    public partial class querys : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommand sqlCommand = null;
        public querys()
        {
            InitializeComponent();
            int summ = 0;
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Инст\БД\WindowsFormsApplication2\WindowsFormsApplication2\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            var get_profit = "SELECT Cinemas.[Name], Sum(Ticket.Price) as [Profit] FROM (((Cinemas INNER JOIN Hall on Cinemas.Id_cinema = Hall.Cinema) INNER JOIN [Session] on Hall.Id_hall = [Session].Hall) INNER JOIN Ticket on [Session].Id_session = Ticket.[Session]) INNER JOIN Time_Table on [Session].Id_session = Time_Table.[Session] GROUP BY Cinemas.[Name]";
            SqlDataAdapter sqlDa = new SqlDataAdapter(get_profit, sqlConnection);
            DataTable dt = new DataTable();
            sqlDa.Fill(dt);
            dataGridView1.DataSource = dt;
            sqlConnection.Close();
            DataTable dtPieChartData = new DataTable();
            dtPieChartData.Columns.Add("Cinema");
            dtPieChartData.Columns.Add("Profit");
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int indexCinema = 0;
                int indexProfit = 1;
                dtPieChartData.Rows.Add(row.Cells[indexCinema].Value, row.Cells[indexProfit].Value);
            }

            chart1.DataSource = dtPieChartData;
            chart1.Series["Series1"].XValueMember = "Cinema";
            chart1.Series["Series1"].YValueMembers = "Profit";
            this.chart1.Titles.Add("Profit from Cinemas");
            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            chart1.Series["Series1"].IsValueShownAsLabel = true;

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                summ += (int)dataGridView1.Rows[i].Cells[1].Value;
            }
            label3.Text = summ.ToString();
        }

   
    }
}
