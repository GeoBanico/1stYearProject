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
    /// Interaction logic for ChangePass.xaml
    /// </summary>
    public partial class ChangePass : Window
    {
        List<PersonInfo> PInfo = new List<PersonInfo>();
        List<PastInfo> PastTrans = new List<PastInfo>();
        private string pass;

        public ChangePass(List<PersonInfo> PInfo, List<PastInfo> PastTrans, string pass)
        {
            InitializeComponent();
            this.PInfo = PInfo;
            this.PastTrans = PastTrans;
            this.pass = pass;
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtPass.Text == txtcrntPass.Password)
            {
                MessageBox.Show("Password Saved");
                pass = txtPass.Text;
                MainForm x = new MainForm(PInfo, PastTrans, pass);
                x.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Password does not match");
            }
        }
    }
}
