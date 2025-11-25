using JobInterviewBL.Model;
using JobInterviewUI.Model;
using System.Windows;

namespace JobInterviewUI
{
    /// <summary>
    /// Interaction logic for HREmployeeWindow.xaml
    /// </summary>
    public partial class HREmployeeWindow : Window
    {
        public HREmployeeUI HREmployee { get; set; }
        private bool isUpdate;
        public HREmployeeWindow(bool isUpdate, HREmployeeUI hREmployee)
        {
            InitializeComponent();
            this.isUpdate = isUpdate;
            if (isUpdate)
            {
                ButtonHREmployee.Content = "Update";
                HREmployee = hREmployee;
                TextBoxId.Text = HREmployee.ID.ToString();
                TextBoxName.Text = HREmployee.Name;
                TextBoxEmail.Text = HREmployee.Email;
                TextBoxPhone.Text = HREmployee.Phone;
                TextBoxExpertise.Text = HREmployee.Expertise;
            }
            else
            {
                ButtonHREmployee.Content = "New";
            }
        }


        private void ButtonNewUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isUpdate)
                {
                    HREmployee.Email = TextBoxEmail.Text;
                    HREmployee.Name = TextBoxName.Text;
                    HREmployee.Expertise = TextBoxExpertise.Text;
                    HREmployee.Phone = TextBoxPhone.Text;
                }
                else
                {
                    HREmployee = new HREmployeeUI
                    (TextBoxName.Text, TextBoxEmail.Text, TextBoxPhone.Text, TextBoxExpertise.Text);
                }
                DialogResult = true;
                Close();
            }
            catch (Exception ex) { throw; }
        }
    }
} 
