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
using System.Windows.Controls.Primitives;

namespace HospitalManagementWPFApp
{
    /// <summary>
    /// Interaction logic for AddUser_3.xaml
    /// </summary>
    public partial class AddPatientHealthDetails_UserControl : UserControl
    {
        SqlConnection conn;
        public AddPatientHealthDetails_UserControl()
        {
            InitializeComponent();
            string connectionString = ConfigurationManager.ConnectionStrings["HospitalManagementWPFApp.Properties.Settings.HospitalConnectionString"].ConnectionString;
            conn = new SqlConnection(connectionString);
            BindisDiabetic();
            BindisHypertensive();
            BindisHIVPositive();
            BindAssignedDoctor();
            BindRoomandWard();

            if (AddPatient.mode == 1)
            {
                txtWeight.Text = AddPatient.HealthDetails.Weight.ToString();
                txtHeight.Text = AddPatient.HealthDetails.Height.ToString();
                BloodPressureDistolic.Text = AddPatient.HealthDetails.BloodPressureDiastolic.ToString();
                BloodPressureSystolic.Text = AddPatient.HealthDetails.BloodPressureSystolic.ToString() ;
                isDiabetic.SelectedValue = AddPatient.HealthDetails.Diabetic;
                BloodType.Text = AddPatient.HealthDetails.BloodType.ToString();/*
                AdditionalNotes.Txt = AddPatient.HealthDetails.AdditionalNotes.ToString();
                RoomandWard.SelectedValue = AddPatient.HealthDetails.LocationAtHospitalId;*/


            }

        }
        public void updateHealthDetails()
        {
            AddPatient.HealthDetails.Weight = int.Parse(txtWeight.Text);
            AddPatient.HealthDetails.Height = Convert.ToInt32(txtHeight.Text.Trim());
            AddPatient.HealthDetails.BloodPressureDiastolic = Convert.ToInt32(BloodPressureDistolic.Text.Trim());
            AddPatient.HealthDetails.BloodPressureSystolic = Convert.ToInt32(BloodPressureSystolic.Text.Trim());
            AddPatient.HealthDetails.Diabetic = (isDiabetic.SelectedValue.ToString() == "1");
            AddPatient.HealthDetails.Hypertensive = (isHypertensive.SelectedValue.ToString() == "1");
            AddPatient.HealthDetails.HIVPositive = (isHIVPositive.SelectedValue.ToString() == "1");
          AddPatient.HealthDetails.AssignedDoctorId = int.Parse(AssignedDoctor.SelectedValue.ToString());
             AddPatient.HealthDetails.BloodType = BloodType.Text.ToString();
                       AddPatient.HealthDetails.AdditionalNotes = AdditionalNotes.Text.ToString();
                       AddPatient.HealthDetails.LocationAtHospitalId = Convert.ToInt32(RoomandWard.SelectedValue);
            AddPatient.HealthDetails.Show();

        }

        private void BindisDiabetic()
        {

            DataTable dtDiabetic = new DataTable();
            dtDiabetic.Columns.Add("Text");
            dtDiabetic.Columns.Add("Value");
            dtDiabetic.Rows.Add("-- SELECT --", null);
            dtDiabetic.Rows.Add("False", 0);
            dtDiabetic.Rows.Add("True", 1);
    

            isDiabetic.ItemsSource = dtDiabetic.DefaultView;
            isDiabetic.DisplayMemberPath = "Text";
            isDiabetic.SelectedValuePath = "Value";

        }

        private void BindisHypertensive()
        {

            DataTable dtHyoertensive = new DataTable();
            dtHyoertensive.Columns.Add("Text");
            dtHyoertensive.Columns.Add("Value");
            dtHyoertensive.Rows.Add("-- SELECT --", null);
            dtHyoertensive.Rows.Add("False", 0);
            dtHyoertensive.Rows.Add("True", 1);


            isHypertensive.ItemsSource = dtHyoertensive.DefaultView;
            isHypertensive.DisplayMemberPath = "Text";
            isHypertensive.SelectedValuePath = "Value";

        }

        private void BindisHIVPositive()
        {

            DataTable dtHIVPositive = new DataTable();
            dtHIVPositive.Columns.Add("Text");
            dtHIVPositive.Columns.Add("Value");
            dtHIVPositive.Rows.Add("-- SELECT --", null);
            dtHIVPositive.Rows.Add("False", 0);
            dtHIVPositive.Rows.Add("True", 1);


            isHIVPositive.ItemsSource = dtHIVPositive.DefaultView;
            isHIVPositive.DisplayMemberPath = "Text";
            isHIVPositive.SelectedValuePath = "Value";

        }
                public void BindAssignedDoctor()
        {
            {
                DataTable dtDoctors = new DataTable();
                try
                {
                    string query = "SELECT Person.Id, Person.firstName + ' '  + Person.lastName as doctor, Employment.occupation FROM Employment \r\nLEFT JOIN  Person ON Employment.personId = Person.Id\r\nWHERE Employment.occupation = 'Doctor'";
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    using (adapter)
                    {
                        DataTable docotrsTable = new DataTable();
                        adapter.Fill(docotrsTable);
                        if (docotrsTable.Rows.Count > 0)
                        {
                            AssignedDoctor.ItemsSource = docotrsTable.DefaultView;
                            AssignedDoctor.DisplayMemberPath = "doctor";
                            AssignedDoctor.SelectedValuePath = "Id";
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
        }
        public void BindRoomandWard() {

            {
                try
                {
                    string query = "SELECT LocationAtHospital.Id, CAST(LocationAtHospital.roomNumber AS nvarchar(10))  + ' - ' + Ward.ward as roomAndWard FROM LocationAtHospital LEFT JOIN Ward ON LocationAtHospital.wardId = Ward.Id";                    
                    SqlCommand sqlCommand = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    using (adapter)
                    {
                        DataTable roomAndWardTable = new DataTable();
                        adapter.Fill(roomAndWardTable);
                        if (roomAndWardTable.Rows.Count > 0)
                        {
                            RoomandWard.ItemsSource = roomAndWardTable.DefaultView;
                            RoomandWard.DisplayMemberPath = "roomAndWard";
                            RoomandWard.SelectedValuePath = "Id";
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
        }


        private void SaveProgress_Click(object sender, RoutedEventArgs e)
        {
            try {
            updateHealthDetails();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }


}
