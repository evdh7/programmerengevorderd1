using JobInterviewBL.Managers;
using System.Windows;

namespace JobInterviewUI
{
    /// <summary>
    /// Interaction logic for InteviewWindow.xaml
    /// </summary>

    public partial class InterviewWindow : Window
    {
        private JobInterviewManager manager;
        public InterviewWindow(JobInterviewManager manager)
        {
            InitializeComponent();
            this.manager = manager;
        }

        private void ButtonExperts_Click(object sender, RoutedEventArgs e)
        {
            ExpertsWindow w = new ExpertsWindow(manager);
            w.ShowDialog();
        }
    }
}

