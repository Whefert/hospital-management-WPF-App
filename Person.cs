using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalManagementWPFApp
{
    public class Person
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int GenderId { get; set; }
        public string Race { get; set; }
        public string Nationality { get; set; }
        public string TRN { get; set; }
        public void  Show()
        {
            MessageBox.Show(String.Format("Id: {0}, Title: {1}, First Name: {2}, LastName: {3}, DOB: {4}, Gender: {5}, Race: {6}, " +
                "Nationality {7}, TRN {8}", this.Id, this.Title, this.FirstName, this.LastName, this.DOB, this.Gender, this.Race, this.Nationality, this.TRN));
        }

    }

}
