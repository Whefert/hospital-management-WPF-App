using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for AddUser_2.xaml
    /// </summary>
    public partial class AddPatientNextofKin_UserControl : UserControl
    {
        public AddPatientNextofKin_UserControl()
        {
            InitializeComponent();
        }

        public void UpdateNextOfKin()
        {
            AddPatient.NextofKin.Title = NextOfKinTitle.Text;
            AddPatient.NextofKin.FirstName = NextOfFirstKinFirstName.Text;
            AddPatient.NextofKin.LastName = NextOfKinLastName.Text;
            AddPatient.NextofKin.DOB = NextOfKinDOB.SelectedDate;
            AddPatient.NextofKin.Show();
        }


        public void UpdateNextOfKinContact()
        {
            AddPatient.NextofKinContact.Email = NextOfKinEmail.Text;
            AddPatient.NextofKinContact.Number = NextOfKinNumber.Text;
            AddPatient.NextofKinContact.Show();
        }

        private void SaveProgressButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                UpdateNextOfKin();
                UpdateNextOfKinContact();
            }catch (Exception ex)
            {
                MessageBox.Show("Please add data to all fields before saving progress");
            }
            }
    }

    
}
