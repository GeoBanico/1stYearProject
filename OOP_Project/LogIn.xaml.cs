using System;
using System.Collections.Generic;
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

namespace OOP_Project
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        private string pass = "Admin";
        List<PersonInfo> PInfo = new List<PersonInfo>();
        List<PastInfo> PastTrans = new List<PastInfo>();

        public LogIn()
        {
            InitializeComponent();
            txtpass.Focus();
        }

        public LogIn(List<PersonInfo> PInfo, List<PastInfo> PastTrans, string pass)
        {
            InitializeComponent();
            this.PInfo = PInfo;
            this.pass = pass;
            this.PastTrans = PastTrans;
            txtpass.Focus();
        }

        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {
                if (txtpass.Password == pass)
                {
                    MessageBox.Show("Welcome");
                    MainForm x = new MainForm(PInfo,PastTrans,pass);
                    x.Show();
                    this.Close();
                }
                else 
                {
                    MessageBox.Show("Incorrect username or password");
                    txtpass.Clear();
                    txtpass.Focus();
                }
            }

        private void btnexit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
