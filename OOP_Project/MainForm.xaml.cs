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
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        List<PersonInfo> PInfo = new List<PersonInfo>();
        List<PastInfo> PastTrans = new List<PastInfo>();
        List<PersonInfo> PStore = new List<PersonInfo>();


        public string brwName, brwAmnt, brwPercent, brwDate, brwBday, brwAge, brwaddress, brwCno, brwCnoPer, brwPId, pass;
        public int brwAccID;

        public MainForm()
        {
            InitializeComponent();
            disablebuttons();
        }

        public MainForm(List<PersonInfo> PInfo, List<PastInfo> PastTrans, string pass)
        {
            InitializeComponent();
            this.PInfo = PInfo;
            this.PastTrans = PastTrans;
            this.pass = pass;
            addList();
            disablebuttons();
        }

        private void btnArchive_Click(object sender, RoutedEventArgs e)
        {
            int r = 0;
            foreach (PersonInfo y in this.PInfo)
            {
                if (txtSearch.Text == "")
                {
                    ArchiveButton(y, r);
                }
                else
                {
                    if (y.Name.ToLower().Contains(txtSearch.Text.ToLower()))
                    {
                        
                        ArchiveButton(y,r);
                    }
                }
            }

            
            PastTransaction x = new PastTransaction(PInfo, PastTrans, brwAccID, pass);
            x.Show();
            this.Close();
        }

        private int ArchiveButton(PersonInfo y, int r) 
        {
            
            if (r == lstbrw.SelectedIndex)
            {
                brwAccID = y.AccountID;
            }

            return brwAccID;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CustomerInfo x = new CustomerInfo(this.PInfo, this.PastTrans, pass);
            x.Show();
            this.Close();
        }

        private void TxtSearch_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            
            lstbrw.Items.Clear();
            foreach (PersonInfo a in PInfo)
            {
                if (a.Name.ToLower().Contains(txtSearch.Text.ToLower()))
                {
                    lstbrw.Items.Add(a);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePass x = new ChangePass(PInfo, PastTrans, pass);
            x.Show();
            this.Close();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            Payment x = CallPayment();
            x.Show();
            this.Hide();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            int r = 0;
            foreach (PersonInfo x in this.PInfo)
            {
                if (txtSearch.Text == "")
                {
                    editbutton(x, r);
                }
                else
                {
                    if (x.Name.ToLower().Contains(txtSearch.Text.ToLower()))
                    {
                        editbutton(x, r);
                    }

                }
                
            }
            CustomerInfo y = new CustomerInfo(PStore, PInfo, PastTrans, pass);
            y.Show();
            this.Close();
        }

         private void editbutton(PersonInfo x, int r)
         {
            if (r == lstbrw.SelectedIndex)
            {
                PStore.Clear();
                this.PStore.Add(new PersonInfo { Name = x.Name, AccountID = x.AccountID, address = x.address, Age = x.Age, Amnt = x.Amnt, Bday = x.Bday, Cno = x.Cno, CnoPer = x.CnoPer, Loan_Date = x.Loan_Date, Percentage = x.Percentage, PId = x.PId });
            }
            r += 1;
         }
          

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this account?", "DELETE", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) 
            {
                int r = 0;
                foreach (PersonInfo a in PInfo)
                {
                    if (txtSearch.Text == "")
                    {
                        this.PInfo.RemoveAt(lstbrw.SelectedIndex);
                        break;
                    }
                    else
                    {
                        if (a.Name.ToLower().Contains(txtSearch.Text.ToLower()))
                        {
                            if (r == lstbrw.SelectedIndex)
                            {
                                this.PInfo.Remove(a);
                                break;
                            }
                            r += 1;
                        }
                    }

                }
                refresh();
            }
        }

        private void lstbrw_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnPay.IsEnabled = true;
            btnArchive.IsEnabled = true;

            Payment x = CallPayment();
            decimal amnt = x.interestTotal();
            lblBalance.Content = "P " + amnt;
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to return to the Log-In page?", "Back", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LogIn x = new LogIn(PInfo, PastTrans, pass);
                x.Show();
                this.Close();
            }
        }

        private void disablebuttons()
        {
            btnDelete.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnPay.IsEnabled = false;
            btnArchive.IsEnabled = false;
        }

        private void refresh()
        {
            MainForm x = new MainForm(PInfo, PastTrans, pass);
            x.Show();
            this.Hide();
        }

        private void addList()
        {
            foreach (PersonInfo x in PInfo)
            {
                lstbrw.Items.Add(new PersonInfo { Name = x.Name, AccountID = x.AccountID, Amnt = x.Amnt, Loan_Date = x.Loan_Date, Percentage = x.Percentage });
            }
        }

        private Payment CallPayment()
        {
            int r = 0;
            foreach (PersonInfo y in this.PInfo)
            {
                if (txtSearch.Text == "")
                {
                    paymentbutton(y, r);
                }
                else
                {
                    if (y.Name.ToLower().Contains(txtSearch.Text.ToLower()))
                    {
                        paymentbutton(y, r);
                    }
                }
            }
            Payment x = new Payment(PStore, PInfo, PastTrans, pass);
            return (x);
        }

        private void paymentbutton(PersonInfo y, int r) 
        {
            if (r == lstbrw.SelectedIndex)
            {
                PStore.Add(new PersonInfo { Name = y.Name, Amnt = y.Amnt, Percentage = y.Percentage, Loan_Date = y.Loan_Date, AccountID = y.AccountID });
            }
            r += 1;
        }
    }
}
