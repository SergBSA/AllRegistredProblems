using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace AllRegistredProblems
{
    public partial class Form1 : Form
    {
        SqlConnection myConnection = new SqlConnection(@"Data Source=GOLD-PEN\SQLEXPRESSMES;Initial Catalog = MES_Project; Integrated Security = True;Timeout = 10");
        public Form1()
        {
            InitializeComponent();
            try
            {
                LoadData();
            }
            catch 
            {
                MessageBox.Show("Can't connect to Databases", "Connection problem");
                Environment.Exit(0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "mES_ProjectDataSet.RegistredProblems". При необходимости она может быть перемещена или удалена.
            //this.registredProblemsTableAdapter.Fill(this.mES_ProjectDataSet.RegistredProblems);
         
        }
        private void LoadData()
        {
            
            myConnection.Open();

            string query = "SELECT TOP (100) NrPlan, RegDate, Creator, category, category2, category3, category4,  Descript FROM RegistredProblems ORDER BY UpdDate DESC";

            SqlCommand command = new SqlCommand(query, myConnection);

            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            int colCount = 8;
            while (reader.Read())
            {
                data.Add(new string[colCount]);

                for (int i = 0; i< colCount; i++) 
                {
                    data[data.Count - 1][i] = reader[i].ToString();
                }

            }

            reader.Close();

            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }
     

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            LoadData();
        }
    }
}

