using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ONA_Tasks
{
    /// <summary>
    /// Interaction logic for TaskDemo.xaml
    /// </summary>
    public partial class TaskDemo : UserControl
    {
        public TaskDemo()
        {
            InitializeComponent();
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            details aa = new details();

            aa.code.Content = Code.Text;
            aa.Title.Content = Title.Text;
            aa.From_Date.Content = From.Text;
            aa.To_Date.Content = To.Text;
            aa.TO_DEVELOPER.Content = To_Developer.Text;
            aa.Duration.Content = Duration.Text;
            aa.Details.Text = Task_Details.Text;
            aa.DEVELOPER_NOTE.Text = DEVELOPER_NOTE.Text;
            aa.TESTER_NOTE.Text = TESTER_NOTE.Text;
            aa.TO_TESTER.Content = To_Tester.Text;

            if (DEVELOPER_NOTE.Text.Trim() == "")
                aa.g_DEVELOPER.Visibility = Visibility.Collapsed;
            if(TESTER_NOTE.Text.Trim() == "")
                aa.g_TESTER.Visibility = Visibility.Collapsed;

            aa.CREATED_BY.Content = Created_By.Text;
            aa.STARTED_DATE.Content = Started_Date.Text;
            aa.ENDED_DATE.Content = Ended_Date.Text;
            aa.DURATION_TAKED.Content = Duration_Taked.Text;
            if(Duration_Taked.Text.Trim()=="")
            aa.end.Visibility = Visibility.Collapsed;
            if(Task_Flage.Text == "S")
                aa.FLAGE.Content = "Created";
            else if(Task_Flage.Text == "D")
                aa.FLAGE.Content = "Developing";
            else if(Task_Flage.Text == "T")
                aa.FLAGE.Content = "Testing";
            else if(Task_Flage.Text == "Y") 
                aa.FLAGE.Content = "Ended";
            aa.ShowDialog();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e) {
            aaaaa.Background = Code.Background;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e) {
            aaaaa.Background = b_test.Background;
        }
    }
}
