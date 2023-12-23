using HospitalManagementWPFApp.HospitalDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalManagementWPFApp
{
    public class HealthDetails
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }

        public int PatientId {  get; set; }
        public int BloodPressureSystolic { get; set; } 
        public int BloodPressureDiastolic { get; set; } 
        public string BloodPressure {  get; set; }
        public bool Diabetic { get; set; }
        public bool Hypertensive { get; set; }
        public bool HIVPositive { get; set; }
        public int RoomNumber { get; set; }
        public string Ward { get; set; }
        public string AssignedDoctor { get; set; }
        public int AssignedDoctorId { get; set; }
        public string Building { get; set; }      
        public string AdditionalNotes {  get; set; }
        public int LocationAtHospitalId { get; set; }
        public string BloodType {  get; set; }

        public void Show()
        {
            MessageBox.Show(String.Format("Id: {0},  Weight: {1}, Height: {2}, BloodPressureSystolic: {3}, BloodPressureDiastolic: {4}, BloodPressure: {5}, Diabetic: {6}, " +
                "Hypertensive: {7}, HIVPositive {8}, RoomNumber {9} Ward {10} AssignedDoctorId {11} AssignedDoctor {12}, Additional Notes: {13}, location at hospital Id: {14}, BloodType {15}, Patient Id: {16}  "
               , this.Id, this.Weight, this.Height, this.BloodPressureSystolic, this.BloodPressureDiastolic, this.BloodPressure, this.Diabetic, this.Hypertensive, this.HIVPositive,
               this.RoomNumber, this.Ward, this.AssignedDoctorId, this.AssignedDoctor, this.AdditionalNotes, this.LocationAtHospitalId, this.BloodType, this.PatientId
               )); ;
        }


    }
}
