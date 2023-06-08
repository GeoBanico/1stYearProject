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
    /// Interaction logic for PastTransaction.xaml
    /// </summary>
    public partial class PastTransaction : Window
    {
        List<PersonInfo> PInfo = new List<PersonInfo>();
        List<PastInfo> PastTrans = new List<PastInfo>();
        private string pass;
        private int AcctID;
        public PastTransaction(List<PersonInfo> PInfo, List<PastInfo> PastTrans, int AcctID, string pass)
        {
            InitializeComponent();
            this.PInfo = PInfo;
            this.PastTrans = PastTrans;
            this.pass = pass;
            this.AcctID = AcctID;

            foreach (PastInfo a in PastTrans)
            {
                if (a.AccountID == this.AcctID)
                {
                    lstpsttrans.Items.Add(a);
                    lblName.Content = a.Name;
                }
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainForm x = new MainForm(PInfo,PastTrans,pass);
            x.Show();
            this.Close();
        }

        private void lstpsttrans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int r = 0;
            foreach (PastInfo a in PastTrans)
            {
                if (a.AccountID == AcctID && r == lstpsttrans.SelectedIndex)
                {
                    lblamnt.Content = a.Amnt;
                    lblAmntPaid.Content = a.PaidAmnt;
                    lblDateToday.Content = a.DatePaid;
                    lblLoanDate.Content = a.Loan_Date;
                    lblpercent.Content = a.Percentage;
                    lblTotal.Content = a.IntAmnt;
                    lblReceipt.Content = a.ReceiptNo;
                }
                r += 1;
            }
        }
    }
}
