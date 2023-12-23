using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for PatientList.xaml
    /// </summary>
    public partial class PatientList : UserControl
    {
        SqlConnection conn;

        public BindingList<Person> Patients = new BindingList<Person>();
        public BindingList<HealthDetails> HealthDetails = new BindingList<HealthDetails>();

        public PatientList()
        {

            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["HospitalManagementWPFApp.Properties.Settings.HospitalConnectionString"].ConnectionString;
            conn = new SqlConnection(connectionString);

            GetAllPatients();
            PatientGridView.IsReadOnly = true;
            PatientGridView.ItemsSource = Patients;
            PatientDetailListBox.ItemsSource = HealthDetails;
        }


        public void GetAllPatients()
        {
            Patients.Clear();
            try
            {
                string query = "SELECT \r\nperson.Id, person.title,\r\nperson.firstName,\r\nperson.lastName,\r\nperson.TRN,\r\nPerson.DOB, \r\nPerson.Age, \r\ngender.gender, " +
                    "\r\naddress.address_line_1 as poBirth_line_1,\r\naddress.address_line_2 as poBirth_line_2,\r\nrace.race,\r\nnationality.nationality," +
                    "\r\nHealthDetail.weight,\r\nHealthDetail.height,\r\nHealthDetail.bloodPressureSystolic,\r\nHealthDetail.bloodPressureDiastolic,\r\nHealthDetail.isDiabetic," +
                    "\r\nHealthDetail.isHypertensive,\r\nHealthDetail.isHIVPositive,\r\nHealthDetail.assignedDoctorId\r\nFROM PERSON \r\nLEFT JOIN gender " +
                    "ON person.genderId = gender.Id\r\nLEFT JOIN race ON person.raceId = race.Id\r\nLEFT JOIN address ON person.placeofBirthId = address.Id\r\nLEFT JOIN " +
                    "nationality ON person.nationalityId = nationality.Id\r\nINNER JOIN HealthDetail ON person.Id = HealthDetail.personId";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                using (adapter)
                {

                    DataTable patientTable = new DataTable();

                    adapter.Fill(patientTable);
                    if (patientTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in patientTable.Rows)
                        {
                            Patients.Add(new Person
                            {
                                Id = (int)row["id"],
                                Title = row["title"].ToString(),
                                FirstName = row["firstName"].ToString(),
                                LastName = row["lastName"].ToString(),
                                DOB = DateTime.Parse(row["DOB"].ToString()),
                                Age = (int)row["Age"],
                                TRN = row["TRN"].ToString(),
                                Race = row["race"].ToString(),
                                Nationality = row["nationality"].ToString(),
                                Gender = row["gender"].ToString().Trim().ToUpper()
                            });
                        }
                    }
                    else
                    {
                        MessageBox.Show("No users found, please try again");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
        }

        private void DeletePatientButton_Click(object sender, RoutedEventArgs e)
        {
            Person selectedPerson = (Person)PatientGridView.SelectedItem;
            DeletePatient((int)selectedPerson.Id);

        }

        private void DeletePatient(int Id)
        {
            try
            {
                string query = "DELETE FROM Person WHERE Id = @Id";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                conn.Open();
                sqlCommand.Parameters.AddWithValue("id", Id);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show(String.Format("User: {0} deleted successfully", Id));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
                GetAllPatients();
            }

        }



        public void GetPatientHealthDetails(int personId)
        {
            {
                HealthDetails.Clear();
                try
                {
                    string query = "SELECT HealthDetail.Id,\r\n" +
                        " HealthDetail.weight, \r\n HealthDetail.height, \r\n HealthDetail.bloodPressureSystolic,\r\nHealthDetail.bloodPressureDiastolic, " +
                        "\r\nHealthDetail.isDiabetic, \r\n HealthDetail.isHypertensive, HealthDetail.additionalNotes, HealthDetail.bloodType,\r\n HealthDetail.isHIVPositive, \r\n doctor.firstName doctorFirstName, " +
                        "\r\n doctor.lastName doctorLastName,\r\n patient.firstName patientFirstName, \r\n patient.lastName patientLastName, " +
                        "\r\n HealthDetail.locationatHospitalId,\r\n LocationAtHospital.roomNumber,\r\n Ward.ward\r\n FROM\r\nHealthDetail " +
                        "\r\nLEFT JOIN Person doctor ON HealthDetail.assignedDoctorId = doctor.Id \r\nLEFT JOIN Person patient ON HealthDetail.personId = patient.Id" +
                        "\r\nLEFT JOIN LocationAtHospital ON HealthDetail.locationatHospitalId = LocationAtHospital.Id" +
                        "\r\nLEFT JOIN Ward ON Ward.Id = LocationAtHospital.Id WHERE HealthDetail.personId = @personId;"
                    ;
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    sqlCommand.Parameters.AddWithValue("@personId", personId);
                    using (adapter)
                    {

                        DataTable healthTable = new DataTable();
                        adapter.Fill(healthTable);
                        if (healthTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in healthTable.Rows)
                            {
                                HealthDetails.Add(new HealthDetails
                                {
                                    Id = (int)row["id"],
                                    Weight = Convert.ToInt32(row["weight"]),
                                    Height = Convert.ToInt32(row["height"]),
                                    BloodPressure = String.Format("{0} / {1}", row["bloodPressureSystolic"], row["bloodPressureDiastolic"]),
                                    Diabetic = Convert.ToBoolean(row["isDiabetic"]),
                                    HIVPositive = Convert.ToBoolean(row["isHIVPositive"]),
                                    Hypertensive = Convert.ToBoolean(row["isHypertensive"]),
                                    AssignedDoctor = String.Format("{0} {1}", row["doctorFirstName"], row["doctorLastName"]),
                                    RoomNumber = Convert.ToInt32(row["roomNumber"]),
                                    AdditionalNotes = row["additionalNotes"].ToString(),
                                    BloodType = row["bloodType"].ToString(),
                                    Ward = row["ward"].ToString()
                                });
                            }
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

                    PatientDetailListBox.ItemsSource = HealthDetails;
                }
            }

        }


        public void GetPatientDetailsForUpdate(int personId)
        {
            {
                try
                {
                    string query = "SELECT HealthDetail.Id,\r\n\tHealthDetail.personId,\r\n" +
                        " HealthDetail.weight, \r\n HealthDetail.height, \r\n HealthDetail.bloodPressureSystolic,\r\nHealthDetail.bloodPressureDiastolic, HealthDetail.assignedDoctorId, " +
                        "\r\nHealthDetail.isDiabetic, \r\n HealthDetail.isHypertensive, HealthDetail.additionalNotes, HealthDetail.bloodType,\r\n HealthDetail.isHIVPositive, \r\n doctor.firstName doctorFirstName, " +
                        "\r\n doctor.lastName doctorLastName,\r\n patient.firstName patientFirstName, \r\n patient.lastName patientLastName, " +
                        "\r\n HealthDetail.locationatHospitalId,\r\n LocationAtHospital.roomNumber,\r\n Ward.ward\r\n FROM\r\nHealthDetail " +
                        "\r\nLEFT JOIN Person doctor ON HealthDetail.assignedDoctorId = doctor.Id \r\nLEFT JOIN Person patient ON HealthDetail.personId = patient.Id" +
                        "\r\nLEFT JOIN LocationAtHospital ON HealthDetail.locationatHospitalId = LocationAtHospital.Id" +
                        "\r\nLEFT JOIN Ward ON Ward.Id = LocationAtHospital.Id WHERE HealthDetail.personId = @personId;"
                    ;
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    sqlCommand.Parameters.AddWithValue("@personId", personId);
                    using (adapter)
                    {

                        DataTable healthTable = new DataTable();
                        adapter.Fill(healthTable);
                        if (healthTable.Rows.Count > 0)
                        {
                            foreach (DataRow row in healthTable.Rows)
                            {
                                AddPatient.HealthDetails = new HealthDetails()
                                {
                                    Id = (int)row["Id"],
                                    PatientId = (int)row["personId"],
                                    Weight = Convert.ToInt32(row["weight"]),
                                    Height = Convert.ToInt32(row["height"]),
                                    BloodPressureDiastolic = Convert.ToInt32(row["bloodPressureSystolic"]),
                                    BloodPressureSystolic = Convert.ToInt32(row["bloodPressureDiastolic"]),
                                    Diabetic = Convert.ToBoolean(row["isDiabetic"]),
                                    HIVPositive = Convert.ToBoolean(row["isHIVPositive"]),
                                    Hypertensive = Convert.ToBoolean(row["isHypertensive"]),
                                    AssignedDoctor = String.Format("{0} {1}", row["doctorFirstName"], row["doctorLastName"]),
                                    AssignedDoctorId = Convert.ToInt32(row["assignedDoctorId"]),
                                    RoomNumber = Convert.ToInt32(row["roomNumber"]),
                                    AdditionalNotes = row["additionalNotes"].ToString(),
                                    BloodType = row["bloodType"].ToString(),
                                    Ward = row["ward"].ToString()
                                };
                            }
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

                    PatientDetailListBox.ItemsSource = HealthDetails;
                }
            }

        }

        public void InitPerson(int patientId)
        {
            try
            {
                string query = "SELECT \r\nperson.Id, person.title,\r\nperson.firstName,\r\nperson.lastName,\r\nperson.TRN,\r\nPerson.DOB, \r\nPerson.Age, \r\ngender.gender, " +
                    "\r\naddress.address_line_1 as poBirth_line_1,\r\naddress.address_line_2 as poBirth_line_2,\r\nrace.race,\r\nnationality.nationality," +
                    "\r\nHealthDetail.weight,\r\nHealthDetail.height,\r\nHealthDetail.bloodPressureSystolic,\r\nHealthDetail.bloodPressureDiastolic,\r\nHealthDetail.isDiabetic," +
                    "\r\nHealthDetail.isHypertensive,\r\nHealthDetail.isHIVPositive,\r\nHealthDetail.assignedDoctorId\r\nFROM PERSON \r\nLEFT JOIN gender " +
                    "ON person.genderId = gender.Id\r\nLEFT JOIN race ON person.raceId = race.Id\r\nLEFT JOIN address ON person.placeofBirthId = address.Id\r\nLEFT JOIN " +
                    "nationality ON person.nationalityId = nationality.Id\r\nLEFT JOIN HealthDetail ON person.Id = HealthDetail.personId WHERE personId = @patientId";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                sqlCommand.Parameters.AddWithValue("patientId", patientId);
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                using (adapter)
                {

                    DataTable patientTable = new DataTable();

                    adapter.Fill(patientTable);
                    if (patientTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in patientTable.Rows)
                        {


                            AddPatient.Patient = new Person()
                            {
                                Id = (int)row["id"],
                                Title = row["title"].ToString(),
                                FirstName = row["firstName"].ToString(),
                                LastName = row["lastName"].ToString(),
                                DOB = DateTime.Parse(row["DOB"].ToString()),
                                Age = (int)row["Age"],
                                TRN = row["TRN"].ToString(),
                                Race = row["race"].ToString(),
                                Nationality = row["nationality"].ToString(),
                                Gender = row["gender"].ToString().Trim().ToUpper()
                            };
                        }
                    }
                    else
                    {
                        MessageBox.Show("No users found, please try again");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }


        }

        private void PatientGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Person selectedPerson = (Person)PatientGridView.SelectedItem;
            GetPatientHealthDetails((int)selectedPerson.Id);
        }

        private void RefreshViewButton_Click(object sender, RoutedEventArgs e)
        {
            GetAllPatients();
        }


        private void AddPatientButton_Click(object sender, RoutedEventArgs e)
        {
            AddPatient.mode = 0;
            Window window = new AddPatient();

            window.Show();
        }

        private void UpdatePatientRecordButton_Click(object sender, RoutedEventArgs e)
        {
            //Update Add Patient propertites
            Person patient = (Person)PatientGridView.SelectedItem;
            AddPatient.Patient = patient;
            AddPatient.mode = 1;
            GetPatientDetailsForUpdate((int)patient.Id);


            Window window = new AddPatient();

            window.Show();
        }
    }




}


