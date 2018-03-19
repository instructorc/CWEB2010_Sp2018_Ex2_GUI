using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApp
{

    public partial class Form1 : Form
    {
        List<Account> listOfAccounts = new List<Account>();

        //Constructor
        public Form1()
        {
            InitializeComponent();

            listOfAccounts = readInAccounts();
            getWithdrawDeposit(listOfAccounts);
            dataGridView1.DataSource = listOfAccounts;
            label20.Text = listOfAccounts.Count.ToString();

        }
        private void selectedRowsButton_Click(object sender, System.EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public static List<Account> readInAccounts()
        {
            //DECLARATIONS
            List<Account> accounts = new List<Account>();

            Savings savAccount;
            Checking checkAccount;
            CD cdAccount;
            const char DELIMITER = ',';
            string[] arrayOfValues;
            const string FILEPATH = @"C:\Users\fulchr\Box Sync\CWEB2010\Spring 2018\acct_ex.csv";
            Random randAcctNum = new Random();


            try
            {
                FileStream file = new FileStream(FILEPATH, FileMode.Open, FileAccess.Read);
                StreamReader read = new StreamReader(file);

                while (!read.EndOfStream)
                {
                    arrayOfValues = read.ReadLine().Split(DELIMITER);  //Splitting the information at delimiter of ','
                    switch (arrayOfValues[3])
                    {
                        case "Saving":
                            savAccount = new Savings(randAcctNum.Next(100, 999), arrayOfValues[0], arrayOfValues[1], Convert.ToDouble(arrayOfValues[2]), arrayOfValues[3]);
                            Console.WriteLine(savAccount);
                            accounts.Add(savAccount);
                            break;
                        case "Checking":
                            checkAccount = new Checking(randAcctNum.Next(100, 999), arrayOfValues[0], arrayOfValues[1], Convert.ToDouble(arrayOfValues[2]), arrayOfValues[3]);
                            Console.WriteLine(checkAccount);
                            accounts.Add(checkAccount);
                            break;

                        case "CD":
                            cdAccount = new CD(randAcctNum.Next(100, 999), arrayOfValues[0], arrayOfValues[1], Convert.ToDouble(arrayOfValues[2]), arrayOfValues[3]);
                            Console.WriteLine(cdAccount);
                            accounts.Add(cdAccount);
                            break;
                        default:
                            Console.WriteLine("Account is not assigned a type");
                            break;
                    }




                }
                read.Close();
                file.Close();


            }
            catch (Exception i)
            {
                Console.WriteLine(i.StackTrace);
            }

            //Calling this method
            getHighAccounts(accounts);

            return accounts;

        }

        public static IEnumerable<Account> getHighAccounts(List<Account> accounts)
        {

            var highAccounts =  //Query name
                from acct in accounts //Data set
                where acct.acctBalance > 8000.0  //formatted query
                orderby acct.lname
                select acct; 
            return highAccounts;
        }

        public static void getWithdrawDeposit(List<Account> list)
        {


            List<Double> deposit = new List<Double>();
            List<Double> withdraw = new List<Double>();
            const char DELIMITER = ',';
            string[] arrayOfValues;
            const string FILEPATH = @"C:\Users\fulchr\Box Sync\CWEB2010\Spring 2018\acct_deposit_withdraw.csv";

            try
            {
                FileStream file = new FileStream(FILEPATH, FileMode.Open, FileAccess.Read);
                StreamReader read = new StreamReader(file);

                while (!read.EndOfStream)
                {

                    arrayOfValues = read.ReadLine().Split(DELIMITER);  //Splitting the information at delimiter of ','
                    deposit.Add(Convert.ToDouble(arrayOfValues[0]));
                    withdraw.Add(Convert.ToDouble(arrayOfValues[1]));



                }
                read.Close();
                file.Close();



            }
            catch (Exception i)
            {
                Console.WriteLine(i.StackTrace);
            }

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Checking checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
         }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void selectedRowsButton_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            label6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();//Account #
            label7.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();//Account first name
            label8.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();//Account last name
            label9.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();//Account balance
            label10.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString(); //Account Creation Date
            label11.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString(); //Acount Type

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        //Deposit Method
        private void button3_Click(object sender, EventArgs e)
        {
            var value =listOfAccounts.Where<Account>(x => x.acctNum == Convert.ToDouble(label6.Text));
            var instance =value.ToArray().First();
            instance.Deposit(Convert.ToDouble(textBox3.Text));
            textBox3.Text = "";
            MessageBox.Show(instance.ToString());  
        }

        private void checkBox6_MouseClicked(object sender, MouseEventArgs e)
        {
            //Option 1 - Using Linq
            var checkingAccounts =  //Query name
                from acct in listOfAccounts //Data set
                where acct.accountType == "Checking" //formatted query

                select acct;
            //Option 2 - Lambda Expression and Iteration
            var accts2 = listOfAccounts.Where<Account>(x => x.accountType == "Checking");

            BindingSource bindingSource1 = new BindingSource();
            foreach(var i in checkingAccounts)
            {
                bindingSource1.Add(i);
            }
           
            dataGridView1.DataSource = bindingSource1;
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox8_MouseClicked(object sender, MouseEventArgs e)
        {
            //Option 1 - Using Linq
            var checkingAccounts =  //Query name
                from acct in listOfAccounts //Data set
                where acct.accountType == "Saving" //formatted query

                select acct;

            BindingSource bindingSource1 = new BindingSource();
            foreach (var i in checkingAccounts)
            {
                bindingSource1.Add(i);
            }

            dataGridView1.DataSource = bindingSource1;
        }

        private void checkBox7_MouseClicked(object sender, MouseEventArgs e)
        {
            //Option 2 - Lambda Expression and Iteration
            var accts2 = listOfAccounts.Where<Account>(x => x.accountType == "CD");

            BindingSource bindingSource1 = new BindingSource();
            foreach (var i in accts2)
            {
                bindingSource1.Add(i);
            }

            dataGridView1.DataSource = bindingSource1;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var value = listOfAccounts.Where<Account>(x => x.acctNum == Convert.ToDouble(label6.Text));
            var instance = value.ToArray().First();
            instance.WithDraw(Convert.ToDouble(textBox4.Text));
            textBox3.Text = "";
            MessageBox.Show(instance.ToString());
        }
    }
    public abstract class Account : IBankAccountFunction
    {
        public int acctNum { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public double acctBalance { get; set; }
        public DateTime dateCreated { get; set; }
        public string accountType { get; set; }

        public Account()
        {

        }
        public Account(int acctNum, string fname, string lname, double acctBalance, string acctType)
        {
            this.acctNum = acctNum;
            this.fname = fname;
            this.lname = lname;
            this.acctBalance = acctBalance;
            dateCreated = DateTime.Now;
            accountType = acctType;

        }

        public abstract void Deposit(double depo);
        public abstract void WithDraw(double withdraw);
        public abstract double Balance();
    }//End of Account Class

    class BalanceBelowZero : Exception
    {
        private static string outputMessage = "Found Account below zero.";

        public BalanceBelowZero() : base(outputMessage)
        {

        }
    }
}
