using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Configuration;

namespace HospitalManagementWPFApp
{
    /// <summary>
    /// Interaction logic for AddUser_4.xaml
    /// </summary>
    public partial class AddPatientContact_UserControl : UserControl
    {
        SqlConnection conn;
        public AddPatientContact_UserControl()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["HospitalManagementWPFApp.Properties.Settings.HospitalConnectionString"].ConnectionString;
            conn = new SqlConnection(connectionString);
            BindParish();
            BindCountry();
        }
        public void updatePatientEmployment()
        {
            AddPatient.PatientEmployment.JobTitle = JobTitle.Text;
            AddPatient.PatientEmployment.Employer = Employer.Text;
            AddPatient.PatientEmployment.Show();
        }


        public void updateHomeAddress()
        {

            AddPatient.HomeAddress.AddressLine_1 = AddressLine1.Text;
            AddPatient.HomeAddress.AddressLine_2 = AddressLine2.Text;
            AddPatient.HomeAddress.Parish = Parish.SelectedValue.ToString();
            AddPatient.HomeAddress.City = City.Text;
            AddPatient.HomeAddress.Country = Country.SelectedValue.ToString();
            AddPatient.HomeAddress.Show();
        }

        public void updatePatientContact()
        {
            AddPatient.PatientContact.Email = Email.Text;
            AddPatient.PatientContact.Number = PhoneNumber.Text;
            AddPatient.PatientContact.Show();
        }

        private void BindParish()
        {
            DataTable dtParish = new DataTable();
            try
            {
                string query = "SELECT * FROM parish";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                using (adapter)
                {
                    DataTable parishTable = new DataTable();
                    adapter.Fill(parishTable);
                    if (parishTable.Rows.Count > 0)
                    {
                        Parish.ItemsSource = parishTable.DefaultView;
                       Parish.DisplayMemberPath = "parish";
                        Parish.SelectedValuePath = "Id";
                    }
                    else
                    {
                        MessageBox.Show("Patient not assigned to a doctor");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }

        }


        private void BindCountry()
        {
            DataTable DtCountry = new DataTable();
            try
            {
                string query = "SELECT * FROM Country";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                using (adapter)
                {
                    DataTable countryTable = new DataTable();
                    adapter.Fill(countryTable);
                    if (countryTable.Rows.Count > 0)
                    {
                        Country.ItemsSource = countryTable.DefaultView;
                        Country.DisplayMemberPath = "country";
                        Country.SelectedValuePath = "Id";
                    }
                    else
                    {
                        MessageBox.Show("Patient not assigned to a doctor");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }

        }

        private void SaveProgress_Click(object sender, RoutedEventArgs e)
        {
            try { 
            updatePatientEmployment();
            updatePatientContact();
            updateHomeAddress();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please add datat to all fields before saving progress");
            }
        }
    }
}
