using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalManagementWPFApp
{
    public class Employment
    {
        public string JobTitle { get; set; }
        public string Employer { get; set; }

        public void Show()
        {
            MessageBox.Show(String.Format("Job Title: {0}, Employer: {1}", this.JobTitle, this.Employer));
        }
    }
}
