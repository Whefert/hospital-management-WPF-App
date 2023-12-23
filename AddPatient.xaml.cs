using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HospitalManagementWPFApp
{
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window
    {
        public static int mode;
    

        //declare static properties for updating as user input is received. These properties will be used to query the database for insert or update
        public static Person Patient { get; set; } = new Person()
        /*        {
                    Title = "Mr",
                    FirstName = "Jerome",
                    LastName = "Daley",
                    DOB= "1994-12-14",
                    TRN= "213231312",
                    Gender ="1",
                    Race = "1",
                    Nationality = "1",
                }*/
        ;

        public static Person NextofKin = new Person()
        {
            /*       Title = "Ms",
                   FirstName = "Leone",
                   LastName = "Daley",
                   DOB = DateTime.Parse("12/03/2023"),
              */
        };
        public static Address POBAddress { get; set; } = new Address()
        {
            /*            AddressLine_1 = "51 Made up lane",
                        AddressLine_2 = "Made up",
                        City = "Kingston 20",
                        Parish = "1",
                        Country = "1"*/
        };
        public static Address HomeAddress { get; set; } = new Address()
        {
            /*          AddressLine_1 = "51 Made up lane",
                      AddressLine_2 = "Made up",
                      City = "Kingston 20",
                      Parish = "1",
                      Country = "1"*/
        };

        public static Contact PatientContact { get; set; } = new Contact()
        /*        {
                    Email = "test@gmail.com",
                    Number = "123456789",
                }*/
        ;
        public static Contact NextofKinContact { get; set; } = new Contact()
        /*      {
                    Email = "noktest@gmail.com",
                    Number = "123456789",
                }*/
        ;
        public static HealthDetails HealthDetails { get; set; } = new HealthDetails()
            /*        {
                       Weight= 100,
                       Height = 100,
                       BloodPressureSystolic= 100,
                       BloodPressureDiastolic= 100,
                       Diabetic = true,
                       HIVPositive = true,
                       Hypertensive = true,
                       AssignedDoctorId = 22,
                       LocationAtHospitalId = 1,
                       AdditionalNotes = "Tis person probably will not make it.",
                        BloodType = "A"

                    }*/
            ;
        public static Employment PatientEmployment { get; set; } = new Employment()
        /*        {
                    Employer = "Cornwall Regional Hospital",
                    JobTitle = "Doctor"
                }*/
        ;




        SqlConnection conn;

        public AddPatient()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["HospitalManagementWPFApp.Properties.Settings.HospitalConnectionString"].ConnectionString;
            conn = new SqlConnection(connectionString);
            AddPatient_1.Content = new AddPatientDemographics_UserControl();
            AddPatient_2.Content = new AddPatientNextofKin_UserControl();
            AddPatient_3.Content = new AddPatientHealthDetails_UserControl();
            AddPatient_4.Content = new AddPatientContact_UserControl();

        }
        public void InsertPerson(bool patient, int placeOfBirthId, int contactDetails, int nextOfKinId, int employmentId, int addressId)
        {
            try
            {

                if (patient)
                {

                    string query = "INSERT INTO Person\r\n(title, firstName, lastName, DOB, nextOfKinId, " +
                    "genderId, raceId, employmentId, nationalityId, placeOfBirthId, TRN, contactDetailsId, addressId)\r\nVALUES\r\n" +
                    "(@Title, @FirstName, @LastName, @DOB, @NextOfKin, @Gender, @Race, @Employment,  @Nationality, @PlaceOfBirth, @TRN, @ContactDetails, @Address)";
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    conn.Open();
                    sqlCommand.Parameters.AddWithValue("Title", Patient.Title);
                    sqlCommand.Parameters.AddWithValue("FirstName", Patient.FirstName);
                    sqlCommand.Parameters.AddWithValue("LastName", Patient.LastName);
                    sqlCommand.Parameters.AddWithValue("DOB", Patient.DOB);
                    sqlCommand.Parameters.AddWithValue("NextOfKin", nextOfKinId);
                    sqlCommand.Parameters.AddWithValue("Gender", Patient.Gender);
                    sqlCommand.Parameters.AddWithValue("Employment", employmentId);
                    sqlCommand.Parameters.AddWithValue("Nationality", Patient.Nationality);
                    sqlCommand.Parameters.AddWithValue("PlaceOfBirth", placeOfBirthId);
                    sqlCommand.Parameters.AddWithValue("Address", addressId);
                    sqlCommand.Parameters.AddWithValue("Race", Patient.Race);
                    sqlCommand.Parameters.AddWithValue("TRN", Patient.TRN);
                    sqlCommand.Parameters.AddWithValue("ContactDetails", contactDetails);
                    sqlCommand.ExecuteScalar();
                }
                else
                {
                    string query = "INSERT INTO Person\r\n(title, firstName, lastName, DOB)\r\nVALUES\r\n(@title, @firstName, @lastName, @DOB)";
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    conn.Open();
                    sqlCommand.Parameters.AddWithValue("title", NextofKin.Title);
                    sqlCommand.Parameters.AddWithValue("firstName", NextofKin.FirstName);
                    sqlCommand.Parameters.AddWithValue("lastName", NextofKin.LastName);
                    sqlCommand.Parameters.AddWithValue("DOB", NextofKin.DOB);
                    sqlCommand.ExecuteScalar();
                }

                MessageBox.Show("User added succesfully");
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

        public void InsertHealthDetails(int personId)
        {

            //            
            string query = "INSERT INTO HealthDetail\r\n" +
                "(weight,height, bloodPressureSystolic,bloodPressureDiastolic,isDiabetic,isHypertensive,isHIVPositive,assignedDoctorId, locationatHospitalId, personId, additionalNotes,bloodType)" +
                "VALUES\r\n(@weight, @height, @bloodPressureSystolic, @bloodPressureDiastolic, @isDiabetic, @isHypertensive, @isHIVPositive, @assignedDoctorId, @locationatHospitalId, @personId, @additionalNotes , @bloodType)";

            try
            {
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                conn.Open();
                sqlCommand.Parameters.AddWithValue("weight", HealthDetails.Weight);
                sqlCommand.Parameters.AddWithValue("height", HealthDetails.Height);
                sqlCommand.Parameters.AddWithValue("bloodPressureSystolic", HealthDetails.BloodPressureSystolic);
                sqlCommand.Parameters.AddWithValue("bloodPressureDiastolic", HealthDetails.BloodPressureDiastolic);
                sqlCommand.Parameters.AddWithValue("isDiabetic", HealthDetails.Diabetic);
                sqlCommand.Parameters.AddWithValue("isHypertensive", HealthDetails.Hypertensive);
                sqlCommand.Parameters.AddWithValue("isHIVPositive", HealthDetails.HIVPositive);
                sqlCommand.Parameters.AddWithValue("assignedDoctorId", HealthDetails.AssignedDoctorId);
                sqlCommand.Parameters.AddWithValue("locationatHospitalId", HealthDetails.LocationAtHospitalId);
                sqlCommand.Parameters.AddWithValue("personId", personId);
                sqlCommand.Parameters.AddWithValue("additionalNotes", HealthDetails.AdditionalNotes);
                sqlCommand.Parameters.AddWithValue("bloodType", HealthDetails.BloodType);
                sqlCommand.ExecuteScalar();
                MessageBox.Show("Health detail added succesfully");
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


        public void UpdateHealthDetails(int personId)
        {

            //            
            string query = "UPDATE HealthDetail SET weight = @weight, height = @height, bloodPressureSystolic = @bloodPressureSystolic, " +
                "bloodPressureDiastolic = @bloodPressureDiastolic, isDiabetic = @isDiabetic, " +
                "isHypertensive = @isHypertensive, isHIVPositive = @isHIVPositive, assignedDoctorId = @assignedDoctorId, locationatHospitalId = @locationatHospitalId," +
                " additionalNotes = @additionalNotes , bloodType = @bloodType " +
                "WHERE personId = @personId";

            try
            {
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                conn.Open();
                sqlCommand.Parameters.AddWithValue("weight", HealthDetails.Weight);
                sqlCommand.Parameters.AddWithValue("height", HealthDetails.Height);
                sqlCommand.Parameters.AddWithValue("bloodPressureSystolic", HealthDetails.BloodPressureSystolic);
                sqlCommand.Parameters.AddWithValue("bloodPressureDiastolic", HealthDetails.BloodPressureDiastolic);
                sqlCommand.Parameters.AddWithValue("isDiabetic", HealthDetails.Diabetic);
                sqlCommand.Parameters.AddWithValue("isHypertensive", HealthDetails.Hypertensive);
                sqlCommand.Parameters.AddWithValue("isHIVPositive", HealthDetails.HIVPositive);
                sqlCommand.Parameters.AddWithValue("assignedDoctorId", HealthDetails.AssignedDoctorId);
                sqlCommand.Parameters.AddWithValue("locationatHospitalId", HealthDetails.LocationAtHospitalId);
                sqlCommand.Parameters.AddWithValue("personId", personId);
                sqlCommand.Parameters.AddWithValue("additionalNotes", HealthDetails.AdditionalNotes);
                sqlCommand.Parameters.AddWithValue("bloodType", HealthDetails.BloodType);
                sqlCommand.ExecuteScalar();
                MessageBox.Show("Health detail added succesfully");
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


        public void InsertAddress(bool homeAddress)
        {

            //            locationatHospitalId,personId,
            string query = "INSERT INTO  Address (address_line_1, address_line_2, parish_id , city, countryId )" +
                "VALUES\r\n(@address_line_1, @address_line_2, @parish_id , @city, @countryId)";

            try
            {
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                conn.Open();
                if (homeAddress)
                {
                    sqlCommand.Parameters.AddWithValue("address_line_1", POBAddress.AddressLine_1);
                    sqlCommand.Parameters.AddWithValue("address_line_2", POBAddress.AddressLine_2);
                    sqlCommand.Parameters.AddWithValue("parish_id", POBAddress.Parish);
                    sqlCommand.Parameters.AddWithValue("city", POBAddress.City);
                    sqlCommand.Parameters.AddWithValue("countryId", POBAddress.Country);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("address_line_1", HomeAddress.AddressLine_1);
                    sqlCommand.Parameters.AddWithValue("address_line_2", HomeAddress.AddressLine_2);
                    sqlCommand.Parameters.AddWithValue("parish_id", HomeAddress.Parish);
                    sqlCommand.Parameters.AddWithValue("city", HomeAddress.City);
                    sqlCommand.Parameters.AddWithValue("countryId", HomeAddress.Country);
                }

                sqlCommand.ExecuteScalar();
                MessageBox.Show("Address detail added succesfully");
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

       


        public void InsertContact(bool patient)
        {

            string query = "INSERT INTO  Contact (phoneNumber, email)" +
                "VALUES\r\n(@phoneNumber, @email)";

            try
            {
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                conn.Open();
                if (patient)
                {
                    sqlCommand.Parameters.AddWithValue("email", PatientContact.Email);
                    sqlCommand.Parameters.AddWithValue("phoneNumber", PatientContact.Number);
                }
                else
                {
                    sqlCommand.Parameters.AddWithValue("email", NextofKinContact.Email);
                    sqlCommand.Parameters.AddWithValue("phoneNumber", NextofKinContact.Number);
                }

                sqlCommand.ExecuteScalar();
                MessageBox.Show("Contact added succesfully");
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


        public void InsertEmployment()
        {

            string query = "INSERT INTO  Employment (occupation, employer)" +
                "VALUES\r\n(@occupation, @employer)";
            try
            {
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                conn.Open();

                sqlCommand.Parameters.AddWithValue("occupation", PatientEmployment.JobTitle);
                sqlCommand.Parameters.AddWithValue("employer", PatientEmployment.Employer);
                sqlCommand.ExecuteScalar();
                MessageBox.Show("Employment added succesfully");
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

        public int FindLastAdditionId(string table)
        {
            string query = "SELECT IDENT_CURRENT(@table)";
            int id = -1;
            string connectionString = ConfigurationManager.ConnectionStrings["HospitalManagementWPFApp.Properties.Settings.HospitalConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    sqlCommand.Parameters.AddWithValue("@table", table);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MessageBox.Show(reader[0].ToString());
                            id = int.Parse(reader[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                connection.Close();
            }
            return id;
        }
        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            if (AddPatient.mode == 1)
            {
                UpdateHealthDetails(AddPatient.HealthDetails.PatientId);
            }
            else {
            //Insert Home Address
            InsertAddress(true);
            int homeAddressId = FindLastAdditionId("Address");
            //Insert Place of Birth
            InsertAddress(false);
            int placeofBirthAddressId = FindLastAdditionId("Address");
            //Insert Next of Kin Contact Details
            InsertContact(false);
            int nextOfKinContactDetailsId = FindLastAdditionId("Contact");
            // Insert Next of Kin with last contact details added to database
            InsertPerson(false, 0, nextOfKinContactDetailsId, 0, 0, 0);
            int NextofKinId = FindLastAdditionId("Person");
            //Insert Patient Contact Details
            InsertContact(true);
            int PatientContactDetailsId = FindLastAdditionId("Contact");

            //Insert Employment
            InsertEmployment();
            int EmploymentId = FindLastAdditionId("Employment");
            //Add Patient
            InsertPerson(true, placeofBirthAddressId, PatientContactDetailsId, NextofKinId, EmploymentId, homeAddressId);
            //Find last Added patient
            int PatientId = FindLastAdditionId("Person");
            InsertHealthDetails(PatientId);

            Window window = Application.Current.Windows[0];

            /*          Patient.Show();
                                   HomeAddress.Show();
                                    POBAddress.Show();
                                    HealthDetails.Show();*/
        }
        }
    }
}


