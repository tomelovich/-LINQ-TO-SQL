using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using MongoDB.Driver.Core.Configuration;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data.Common;
using System.Data.Linq;

namespace z1
{
    public partial class Form1 : Form
    {
        SqlConnection _connection = new SqlConnection(
            "Data Source=SQLEXPRESS;Initial Catalog=msdb;Integrated Security=True");

        public string sql;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            printTour();
        }

        public void printTour()
        {
            DataContext db = new DataContext(_connection);
            Table<Turist> turists = db.GetTable<Turist>();
            foreach (var turist in turists)
            {
                textBox1.Text += turist.MiddleName;
                textBox1.Text += turist.FirstName;
                textBox1.Text += turist.LastName;
            }
        }

        public void deleteTourist()
        {
            DataContext db = new DataContext(_connection);
            var tourist = db.GetTable<Turist>().OrderByDescending(u => u.Id).FirstOrDefault();
            if (tourist != null)
            {
                db.GetTable<Turist>().DeleteOnSubmit(tourist);
            }
        }

        public void addTourist()
        {
            DataContext db = new DataContext(_connection);
            Turist tourist = new Turist { MiddleName = "Валерьевич", FirstName = "Павел", LastName = "Дуров" };
            db.GetTable<Turist>().InsertOnSubmit(tourist);
            db.SubmitChanges();
        }
    }


}
