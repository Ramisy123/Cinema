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
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommand sqlCommand = null;
        int selected_place;
        int selected_row;
        int film_price;
        double time_index;
        double day_index;
        List<String> placeData = new List<String>();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
            List<DateTime> dates = new List<DateTime>();
            DateTime today = DateTime.Today;
            dates.Add(today);
            for (int i = 1; i < 2 ; i++)
            {
                DateTime next = today.AddDays(i);
                dates.Add(next);
            }

          
            comboBox2.DataSource = dates;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Инст\БД\WindowsFormsApplication2\WindowsFormsApplication2\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            sqlCommand = new SqlCommand("SELECT (Name) from Cinemas", sqlConnection);
            SqlDataReader reader;
            reader = sqlCommand.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Load(reader);
            comboBox1.ValueMember = "Name";
            comboBox1.DataSource = dt;
            sqlConnection.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.ResetText();
            comboBox4.ResetText();
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Инст\БД\WindowsFormsApplication2\WindowsFormsApplication2\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            DateTime day = Convert.ToDateTime(comboBox2.Text);
            string day_name = day.DayOfWeek.ToString();
            string query = "SELECT Films.[Name] from Day_Index INNER JOIN(Cinemas INNER JOIN (Films INNER JOIN ((Hall INNER JOIN [Session] on Hall.Id_hall = [Session].Hall) INNER JOIN Time_Table on [Session].Id_session = Time_Table.[Session]) on Films.Id_films = Time_Table.Film) on Cinemas.Id_cinema = Hall.Cinema) on Day_Index.Id = Time_Table.[Day] where (((Cinemas.[Name]) = '" + comboBox1.Text + "') AND ((Day_Index.Day_name) = '" + day_name + "'))";
            string query2 = "SELECT Day_Index.[Index] from Day_Index where (((Day_Index.Day_name) = '" + day_name + "'))";
            sqlCommand = new SqlCommand(query, sqlConnection);
            SqlCommand sqlCommand2 = new SqlCommand(query2, sqlConnection);
            day_index = (double)sqlCommand2.ExecuteScalar();
            sqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(dt);
            if(dt != null)
            {
                comboBox3.ValueMember = dt.Columns[0].ToString();
                comboBox3.DataSource = dt;
            }
            
            sqlConnection.Close();
            placeData.Clear();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.ResetText();
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Инст\БД\WindowsFormsApplication2\WindowsFormsApplication2\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            string query = "SELECT Time_Index.[Time] FROM Day_Index INNER JOIN (Time_Index INNER JOIN (((Cinemas INNER JOIN Hall on Cinemas.Id_cinema = Hall.Cinema) INNER JOIN [Session] on Hall.Id_hall = [Session].Hall) INNER JOIN (Films INNER JOIN Time_Table on Films.Id_films = Time_Table.Film) on [Session].Id_session = Time_Table.[Session]) on Time_Index.Id = [Session].[Time]) on Day_Index.Id = Time_Table.[Day] WHERE (((Films.[Name]) = '"+comboBox3.Text+"') AND ((Cinemas.[Name]) = '"+comboBox1.Text+"'))";
            string query2 = "SELECT Films.Bsic_Price FROM Films Where (((Films.[Name]) = '"+ comboBox3.Text +"'))";
            sqlCommand = new SqlCommand(query, sqlConnection);
            SqlCommand sqlCommand2 = new SqlCommand(query2, sqlConnection);
            film_price = (int)sqlCommand2.ExecuteScalar();
            sqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(dt);
            if (dt != null)
            {
                comboBox5.ValueMember = dt.Columns[0].ToString();
                comboBox5.DataSource = dt;
            }
            sqlConnection.Close();
            placeData.Clear();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Инст\БД\WindowsFormsApplication2\WindowsFormsApplication2\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            string query = "SELECT Hall.Row_count FROM Day_Index INNER JOIN (Time_Index INNER JOIN (((Cinemas INNER JOIN Hall on Cinemas.Id_cinema = Hall.Cinema) INNER JOIN [Session] on Hall.Id_hall = [Session].Hall) INNER JOIN (Films INNER JOIN Time_Table on Films.Id_films = Time_Table.Film) on [Session].Id_session = Time_Table.[Session]) on Time_Index.Id = [Session].[Time]) on Day_Index.Id = Time_Table.[Day] WHERE (((Films.[Name]) = '"+comboBox3.Text+"') AND ((Cinemas.[Name]) = '"+comboBox1.Text+"') AND ((Time_Index.[Time]) = '"+comboBox5.Text+"'))";
            string query2 = "SELECT Hall.Place_count FROM Day_Index INNER JOIN (Time_Index INNER JOIN (((Cinemas INNER JOIN Hall on Cinemas.Id_cinema = Hall.Cinema) INNER JOIN [Session] on Hall.Id_hall = [Session].Hall) INNER JOIN (Films INNER JOIN Time_Table on Films.Id_films = Time_Table.Film) on [Session].Id_session = Time_Table.[Session]) on Time_Index.Id = [Session].[Time]) on Day_Index.Id = Time_Table.[Day] WHERE (((Films.[Name]) = '" + comboBox3.Text + "') AND ((Cinemas.[Name]) = '" + comboBox1.Text + "') AND ((Time_Index.[Time]) = '" + comboBox5.Text + "'))";
            string query3 = "Select [Session].Id_session FROM Day_Index INNER JOIN (Time_Index INNER JOIN (((Cinemas INNER JOIN Hall on Cinemas.Id_cinema = Hall.Cinema) INNER JOIN [Session] on Hall.Id_hall = Session.Hall) INNER JOIN (Films INNER JOIN Time_Table ON Films.Id_films = Time_Table.Film) on [Session].Id_session = Time_Table.[Session]) ON Time_Index.Id = [Session].[Time]) on Day_Index.Id = Time_Table.[Day] WHERE (((Films.[Name]) = '" + comboBox3.Text + "') AND ((Cinemas.[Name]) = '" + comboBox1.Text + "') AND ((Time_Index.[Time]) = '" + comboBox5.Text + "'))";
            string query4 = "SELECT Ticket.Place FROM Cinemas INNER JOIN ((Hall INNER JOIN (Films INNER JOIN ((Time_Index INNER JOIN [Session] on Time_Index.Id = [Session].[Time]) INNER JOIN (Day_Index INNER JOIN Time_Table on Day_Index.Id = Time_Table.[Day]) on [Session].Id_session = Time_Table.[Session]) on Films.Id_films = Time_Table.Film) on Hall.Id_hall = [Session].Hall) INNER JOIN Ticket on [Session].Id_session = Ticket.[Session]) on Cinemas.Id_cinema = Hall.Cinema WHERE (((Ticket.[Date]) = '"+comboBox2.Text+"') AND ((Films.[Name]) = '"+comboBox3.Text+"') AND ((Hall.Id_hall) = "+comboBox4.Text+") AND ((Cinemas.[Name]) = '"+comboBox1.Text+"'))";
            sqlCommand = new SqlCommand(query, sqlConnection);
            SqlCommand sqlCommand2 = new SqlCommand(query2, sqlConnection);
            SqlCommand sqlCommand3 = new SqlCommand(query3, sqlConnection);
            SqlCommand sqlCommand4 = new SqlCommand(query4, sqlConnection);
            SqlDataReader reader = sqlCommand4.ExecuteReader();
            while(reader.Read())
            {
                placeData.Add(Convert.ToString(reader.GetInt32(0)));
            }
            reader.Close();
            int column_count = (int)sqlCommand2.ExecuteScalar();
            int row_count = (int)sqlCommand.ExecuteScalar();
            sqlCommand3.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand3);
            da.Fill(dt);
            if (dt != null)
            {
                comboBox6.ValueMember = dt.Columns[0].ToString();
                comboBox6.DataSource = dt;
            }
            sqlConnection.Close();
            dataGridView1.RowCount = row_count;
            dataGridView1.ColumnCount = column_count;

            int val = 1;
            for(int i = 0; i < row_count; i++)
            {
                for (int j = 0; j < column_count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Blue;
                    dataGridView1.Rows[i].Cells[j].Value = val;
                    val++;
                    
                }
            }

            for (int i = 0; i < row_count; i++)
            {
                for (int j = 0; j < column_count; j++)
                {
                    if (placeData.Contains(dataGridView1.Rows[i].Cells[j].Value.ToString()))
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                        dataGridView1.Rows[i].Cells[j].ReadOnly = true;
                    }

                }
            }
            placeData.Clear();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.ResetText();
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Инст\БД\WindowsFormsApplication2\WindowsFormsApplication2\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            string query = "SELECT Hall.[Id_hall] FROM Day_Index INNER JOIN (Time_Index INNER JOIN (((Cinemas INNER JOIN Hall on Cinemas.Id_cinema = Hall.Cinema) INNER JOIN [Session] on Hall.Id_hall = [Session].Hall) INNER JOIN (Films INNER JOIN Time_Table on Films.Id_films = Time_Table.Film) on [Session].Id_session = Time_Table.[Session]) on Time_Index.Id = [Session].[Time]) on Day_Index.Id = Time_Table.[Day] WHERE (((Films.[Name]) = '"+comboBox3.Text+"') AND ((Cinemas.[Name]) = '"+comboBox1.Text+"') AND ((Time_Index.[Time]) = '"+comboBox5.Text+"'))";
            string query2 = "SELECT Time_Index.[Index] FROM Time_Index Where (((Time_Index.[Time]) = '"+comboBox5.Text+"'))";
            sqlCommand = new SqlCommand(query, sqlConnection);
            SqlCommand sqlCommand2 = new SqlCommand(query2, sqlConnection);
            time_index = (double)sqlCommand2.ExecuteScalar();
            sqlCommand.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(dt);
            if (dt != null)
            {
                comboBox4.ValueMember = dt.Columns[0].ToString();
                comboBox4.DataSource = dt;
            }
            sqlConnection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor != Color.Red)
            {
                selected_place = (int)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                selected_row = (e.RowIndex + 1);
                textBox1.Text = selected_row.ToString();
                textBox2.Text = selected_place.ToString();
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = Math.Round(((film_price * day_index) + (film_price * time_index)),0).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Инст\БД\WindowsFormsApplication2\WindowsFormsApplication2\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            sqlCommand = new SqlCommand("INSERT INTO Ticket (Date, Place, Row, Session, Price) VALUES (@Date, @Place, @Row, @Session, @Price)", sqlConnection);
            sqlCommand.Parameters.Add("@Date", comboBox2.Text);
            sqlCommand.Parameters.Add("@Place", textBox2.Text);
            sqlCommand.Parameters.Add("@Row", textBox1.Text);
            sqlCommand.Parameters.Add("@Session", comboBox6.Text);
            sqlCommand.Parameters.Add("@Price", textBox3.Text);
            sqlCommand.ExecuteNonQuery();
            placeData.Clear();
            string query4 = "SELECT Ticket.Place FROM Cinemas INNER JOIN ((Hall INNER JOIN (Films INNER JOIN ((Time_Index INNER JOIN [Session] on Time_Index.Id = [Session].[Time]) INNER JOIN (Day_Index INNER JOIN Time_Table on Day_Index.Id = Time_Table.[Day]) on [Session].Id_session = Time_Table.[Session]) on Films.Id_films = Time_Table.Film) on Hall.Id_hall = [Session].Hall) INNER JOIN Ticket on [Session].Id_session = Ticket.[Session]) on Cinemas.Id_cinema = Hall.Cinema WHERE (((Ticket.[Date]) = '" + comboBox2.Text + "') AND ((Films.[Name]) = '" + comboBox3.Text + "') AND ((Hall.Id_hall) = " + comboBox4.Text + ") AND ((Cinemas.[Name]) = '" + comboBox1.Text + "'))";
            SqlCommand sqlCommand2 = new SqlCommand(query4, sqlConnection);
            SqlDataReader reader = sqlCommand2.ExecuteReader();
            while (reader.Read())
            {
                placeData.Add(Convert.ToString(reader.GetInt32(0)));
            }
            reader.Close();
            placeData.Add(selected_place.ToString());
            string query = "SELECT Hall.Row_count FROM Day_Index INNER JOIN (Time_Index INNER JOIN (((Cinemas INNER JOIN Hall on Cinemas.Id_cinema = Hall.Cinema) INNER JOIN [Session] on Hall.Id_hall = [Session].Hall) INNER JOIN (Films INNER JOIN Time_Table on Films.Id_films = Time_Table.Film) on [Session].Id_session = Time_Table.[Session]) on Time_Index.Id = [Session].[Time]) on Day_Index.Id = Time_Table.[Day] WHERE (((Films.[Name]) = '" + comboBox3.Text + "') AND ((Cinemas.[Name]) = '" + comboBox1.Text + "') AND ((Time_Index.[Time]) = '" + comboBox5.Text + "'))";
            string query2 = "SELECT Hall.Place_count FROM Day_Index INNER JOIN (Time_Index INNER JOIN (((Cinemas INNER JOIN Hall on Cinemas.Id_cinema = Hall.Cinema) INNER JOIN [Session] on Hall.Id_hall = [Session].Hall) INNER JOIN (Films INNER JOIN Time_Table on Films.Id_films = Time_Table.Film) on [Session].Id_session = Time_Table.[Session]) on Time_Index.Id = [Session].[Time]) on Day_Index.Id = Time_Table.[Day] WHERE (((Films.[Name]) = '" + comboBox3.Text + "') AND ((Cinemas.[Name]) = '" + comboBox1.Text + "') AND ((Time_Index.[Time]) = '" + comboBox5.Text + "'))";
            SqlCommand sqlCommand3 = new SqlCommand(query, sqlConnection);
            SqlCommand sqlCommand4 = new SqlCommand(query2, sqlConnection);
            int column_count = (int)sqlCommand4.ExecuteScalar();
            int row_count = (int)sqlCommand3.ExecuteScalar();
            for (int i = 0; i < row_count; i++)
            {
                for (int j = 0; j < column_count; j++)
                {
                    if (placeData.Contains(dataGridView1.Rows[i].Cells[j].Value.ToString()))
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Red;
                        dataGridView1.Rows[i].Cells[j].ReadOnly = true;
                    }

                }
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dataGridView1.ClearSelection();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrintDialog1.AllowSomePages = true;

            PrintDialog1.ShowHelp = true;

            PrintDialog1.Document = docToPrint;

            DialogResult result = PrintDialog1.ShowDialog();

            
            if (result == DialogResult.OK)
            {
                docToPrint.Print();
            }
        }

        private void document_PrintPage(object sender,
    System.Drawing.Printing.PrintPageEventArgs e)
        {
            string text = "In document_PrintPage method.";
            System.Drawing.Font printFont = new System.Drawing.Font
                ("Arial", 35, System.Drawing.FontStyle.Regular);

            e.Graphics.DrawString(text, printFont,
                System.Drawing.Brushes.Black, 10, 10);
        }
    }


}


