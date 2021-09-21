using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace WineDB
{
    public partial class Form1 : Form
    {
        string sql;
        SqlConnection connection;
        DB_Connection db_connection;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db_connection = new DB_Connection();
            connection = new SqlConnection();
            connection = db_connection.getConnection;
            connection.Open();

            using (connection)
            {
                sql = "SELECT * FROM country";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBoxCountry.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }

        private void comboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCountry.SelectedItem != null)
            {
                db_connection = new DB_Connection();
                connection = new SqlConnection();
                connection = db_connection.getConnection;
                connection.Open();

                comboBoxRegion.Items.Clear();
                comboBoxProducer.Items.Clear();

                using (connection)
                {
                    string selectedText = comboBoxCountry.SelectedItem.ToString();
                    sql = $"SELECT * FROM region WHERE country_name = '{selectedText}'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBoxRegion.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
        }

        private void comboBoxRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxRegion.SelectedItem != null)
            {
                db_connection = new DB_Connection();
                connection = new SqlConnection();
                connection = db_connection.getConnection;
                connection.Open();

                comboBoxProducer.Items.Clear();

                using (connection)
                {
                    string selectedText = comboBoxRegion.SelectedItem.ToString();
                    sql = $"SELECT * FROM producer WHERE region_name = '{selectedText}'";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBoxProducer.Items.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxProducer.Text != null)
            {
                string region_name = comboBoxRegion.Text;
                string producer = comboBoxProducer.Text;
                db_connection = new DB_Connection();
                connection = new SqlConnection();
                connection = db_connection.getConnection;

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = $"INSERT INTO producer VALUES ('{producer}', (SELECT region_name FROM region WHERE region_name = '{region_name}'))";
                sqlCommand.Connection = connection;

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            } 
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (comboBoxProducer.Text != null)
            {
                string producer = comboBoxProducer.Text;
                db_connection = new DB_Connection();
                connection = new SqlConnection();
                connection = db_connection.getConnection;

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = $"DELETE FROM producer WHERE producer_name = '{producer}'";
                sqlCommand.Connection = connection;

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            } 
        }
    }
}
