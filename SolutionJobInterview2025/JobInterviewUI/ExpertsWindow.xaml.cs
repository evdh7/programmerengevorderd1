using JobInterviewBL.Managers;
using JobInterviewBL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace JobInterviewUI
{
    /// <summary>
    /// Interaction logic for ExpertsWindow.xaml
    /// </summary>
    public partial class ExpertsWindow : Window
    {
        private ObservableCollection<Expert> allExperts;
        private ObservableCollection<Expert> selectedExperts;

        public ExpertsWindow(JobInterviewManager manager)
        {
            InitializeComponent();
            allExperts = new ObservableCollection<Expert>(manager.GetExperts());
            selectedExperts = new();
            ListBoxAllExperts.ItemsSource = allExperts;
            ListBoxSelectedExperts.ItemsSource = selectedExperts;
        }

        private void ButtonAddAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (Expert expert in allExperts)
            {
                selectedExperts.Add(expert);
            }
            allExperts.Clear();
        }
        private void ButtonAddSelected_Click(object sender, RoutedEventArgs e)
        {
            List<Expert> data = new();
            foreach (Expert expert in ListBoxAllExperts.SelectedItems)
            {
                data.Add(expert);
            }
            foreach (Expert expert in data)
            {
                selectedExperts.Add(expert);
                allExperts.Remove(expert);

            }
        }
        private void ButtonRemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            List<Expert> data = new();
            foreach (Expert expert in ListBoxSelectedExperts.SelectedItems) 
            { 
                data.Add(expert); 
            }
            foreach(Expert expert in data)
            {
                allExperts.Add(expert);
                selectedExperts.Remove(expert);
            }

        }
        private void ButtonRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            foreach(Expert expert in selectedExperts)
            {
                allExperts.Add(expert);
            }
            selectedExperts.Clear();
        }
    }
}
