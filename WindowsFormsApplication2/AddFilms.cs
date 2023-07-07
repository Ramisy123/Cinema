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
    public partial class AddFilms : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommand sqlCommand = null;
        
        public AddFilms()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Инст\БД\WindowsFormsApplication2\WindowsFormsApplication2\Database1.mdf;Integrated Security=True");
            sqlConnection.Open();
            sqlCommand = new SqlCommand("INSERT INTO Films (Name, Age_category, Genre, Duration, Bsic_price) VALUES (@Name, @Age_category, @Genre, @Duration, @Bsic_price)", sqlConnection);
            sqlCommand.Parameters.Add("@Name", textBox1.Text);
            sqlCommand.Parameters.Add("@Age_category", textBox2.Text);
            sqlCommand.Parameters.Add("@Genre", textBox3.Text);
            sqlCommand.Parameters.Add("@Duration", textBox4.Text);
            sqlCommand.Parameters.Add("@Bsic_price", textBox5.Text);
            sqlCommand.ExecuteNonQuery();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            sqlConnection.Close();
            
        }

        private void AddFilms_Load(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
