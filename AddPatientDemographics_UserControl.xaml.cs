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
    /// 
    public partial class AddPatientDemographics_UserControl : UserControl
    {
        public Person person;
        public Address address;
        SqlConnection conn;
        public AddPatientDemographics_UserControl()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["HospitalManagementWPFApp.Properties.Settings.HospitalConnectionString"].ConnectionString;
            conn = new SqlConnection(connectionString);
            BindGender();
            BindNationality();
            BindRace();
            BindParish();
            BindCountry();           

            if(AddPatient.mode == 1)

            {
                //find patient and update fields
                int patientGender;
                if(AddPatient.Patient.Gender == "M") { patientGender = 0; } else { patientGender = 1; }
                FirstName.Text = AddPatient.Patient.FirstName;
                LastName.Text = AddPatient.Patient.LastName;
                Title.Text = AddPatient.Patient.Title;
                DOB.SelectedDate = AddPatient.Patient.DOB;
                TRN.Text = AddPatient.Patient.TRN;
                Gender.SelectedIndex = patientGender;
              }

        }


        private void BindGender()
        {

            try
            {
                string query = "SELECT * FROM Gender";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                using (adapter)
                {
                    DataTable genderTable = new DataTable();
                    adapter.Fill(genderTable);
                    if (genderTable.Rows.Count > 0)
                    {
                        Gender.ItemsSource = genderTable.DefaultView;
                        Gender.DisplayMemberPath = "gender";
                        Gender.SelectedValuePath = "Id";
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


        private void BindNationality()
        {
            DataTable dtNationality = new DataTable();
            try
            {
                string query = "SELECT * FROM nationality";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                using (adapter)
                {
                    DataTable nationalityTable = new DataTable();
                    adapter.Fill(nationalityTable);
                    if (nationalityTable.Rows.Count > 0)
                    {
                        Nationality.ItemsSource = nationalityTable.DefaultView;
                        Nationality.DisplayMemberPath = "nationality";
                        Nationality.SelectedValuePath = "Id";
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


        private void BindRace()
        {
            DataTable dtRace = new DataTable();
            try
            {
                string query = "SELECT * FROM race";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                using (adapter)
                {
                    DataTable raceTable = new DataTable();
                    adapter.Fill(raceTable);
                    if (raceTable.Rows.Count > 0)
                    {
                        Race.ItemsSource = raceTable.DefaultView;
                        Race.DisplayMemberPath = "race";
                        Race.SelectedValuePath = "Id";
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
                        POBParish.ItemsSource = parishTable.DefaultView;
                        POBParish.DisplayMemberPath = "parish";
                        POBParish.SelectedValuePath = "Id";
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
                        POBCountry.ItemsSource = countryTable.DefaultView;
                        POBCountry.DisplayMemberPath = "country";
                        POBCountry.SelectedValuePath = "Id";
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


        public void updatePerson()
        {

            AddPatient.Patient.Title = Title.Text;
            AddPatient.Patient.FirstName = FirstName.Text;
            AddPatient.Patient.LastName = LastName.Text;
            AddPatient.Patient.DOB = DOB.SelectedDate;
            AddPatient.Patient.TRN = TRN.Text;
            AddPatient.Patient.Gender = Gender.SelectedValue.ToString();
            AddPatient.Patient.Race = Race.SelectedValue.ToString();
            AddPatient.Patient.Nationality = Nationality.SelectedValue.ToString();
            AddPatient.Patient.Show();
        }

        public void updatePlaceOfBirth()
        {

            AddPatient.POBAddress.AddressLine_1 = POBAddresLine1.Text;
            AddPatient.POBAddress.AddressLine_2 = POBAddresLine2.Text;
            AddPatient.POBAddress.Parish = POBParish.SelectedValue.ToString();
            AddPatient.POBAddress.City = POBCity.Text;
            AddPatient.POBAddress.Country = POBCountry.SelectedValue.ToString();
            AddPatient.POBAddress.Show();

        }


        private void SaveProgressButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                updatePerson();
                updatePlaceOfBirth();
               /* MessageBox.Show(String.Format("First name: {0}, Last Name: {1}, DOB {2}, TRN {3}, Gender {4}, Race{5}, Nationality {6}", AddPatient.Patient.FirstName, AddPatient.Patient.LastName, 
                    AddPatient.Patient.DOB, AddPatient.Patient.TRN, AddPatient.Patient.Gender, AddPatient.Patient.Race, AddPatient.Patient.Nationality));*/
            }catch(Exception ex)
            {
                MessageBox.Show("Please add data to all fields before saving progress");
            }
        }

        private void Race_Loaded(object sender, RoutedEventArgs e)
        {
            Race.SelectedItem = 1;
        }

        private void Nationality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }

}
