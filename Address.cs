using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalManagementWPFApp
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine_1 { get; set; }
        public string AddressLine_2 { get; set; }
        public string Parish { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public void Show()
        {
            MessageBox.Show(String.Format("Id: {0}, Line 1: {1}, Line 2: {2}, Parish: {3}, City: {4}, Country: {5}",
                this.Id, this.AddressLine_1, this.AddressLine_2, this.Parish, this.City, this.Country));


        }
    }
}
