using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalManagementWPFApp
{
    public class Contact

    {
        public string Email { get; set; }
        public string Number { get; set; }
        public void Show()
        {
            MessageBox.Show(String.Format("Email: {0}, Number: {1}", this.Email, this.Number));
        }
    }


}

