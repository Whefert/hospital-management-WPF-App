using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalManagementWPFApp
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
        public partial class Login : UserControl
    {
            SqlConnection conn;
        public Login()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["HospitalManagementWPFApp.Properties.Settings.HospitalConnectionString"].ConnectionString;
            conn = new SqlConnection(connectionString);
        }

        private void testLbl_Click(object sender, RoutedEventArgs e)
        {
            try {
                string query = "SELECT * FROM Login WHERE username = @username and password = @password";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                using (adapter)
                {
                    sqlCommand.Parameters.AddWithValue("@username", username.Text);
                    sqlCommand.Parameters.AddWithValue("@password", password.Password);
                    
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if(dt.Rows.Count > 0)
                    {
                        MessageBox.Show("User found");
                        Window window = Window.GetWindow(this);
                        window.Content = new PatientList();
                    }
                    else
                    {
                        MessageBox.Show("No user found, please try again");
                    }
                 }

            }catch(Exception ex) {
                MessageBox.Show(ex.ToString());
            }finally { conn.Close(); }
        }
    }
}
