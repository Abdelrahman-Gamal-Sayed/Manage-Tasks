
using System.Windows.Controls;
namespace ONA_Tasks
{
   public static class User
    {
        public static string Code { get; set; }
        public static string Name { get; set; }
        public static string TYPE_CODE { get; set; }
        public static string TYPE_NAME { get; set; }
        public static string CONFIRM_PASS { get; set; }
        public static string Name_Show { get; set; }
  
        public static string AUTHORITY_CODE { get; set; }
        public static string AUTHORITY_NAME { get; set; }
        public static string TASK_FRM { get; set; }
        public static string DASHBOARD_FRM { get; set; }
        public static string TESTER_FRM { get; set; }
        public static string DEVELOPER_FRM { get; set; }
        public static string ADMIN_FRM { get; set; }        
        public static void CleanAll(this Panel s)
        {

            foreach (System.Windows.UIElement child in s.Children)
            {
                GroupBox gb = child as GroupBox;
                if (gb != null)
                    ((Grid)gb.Content).CleanAll();

                Grid dg = child as Grid;
                if (dg != null)
                    dg.CleanAll();
                CheckBox chb = child as CheckBox;
                if (chb != null)
                    chb.IsChecked = false;
                TextBox txt = child as TextBox;
                if (txt != null)
                    txt.Text = "";
                ComboBox cbx = child as ComboBox;
                if (cbx != null)
                    cbx.Text = "";
                DatePicker dp = child as DatePicker;
                if (dp != null)
                    dp.Text = "";
                StackPanel sp = child as StackPanel;
                if (sp != null)
                    ((StackPanel)sp).CleanAll();

                DataGrid DG = child as DataGrid;
                if (DG != null)
                    DG.ItemsSource = null;
            }

        }

    }
}
