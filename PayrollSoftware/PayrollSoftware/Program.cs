using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSoftware
{
    class Staff
    {
        //Amount staff are paid per hour
        private float hourlyRate;
        //Backing field for HoursWorked
        private int hWorked;

        /* Public variables including the total and basic pay for staff,
         * the name of staff members, and the hours worked.
         * These variables are essential to calculating pay,
         * and providing an accurate payroll
         */
        public float TotalPay { get; protected set; }
        public float BasicPay { get; private set; }
        public string NameOfStaff { get; private set; }
        //hWorked is the backing field for HoursWorked
        public int HoursWorked
        {
            get
            {
                return hWorked;
            }
            set
            {
                if(hWorked > 0)
                {
                    hWorked = value;
                }
                else
                {
                    hWorked = 0;
                }
            }
        }

        //Constructor for the staff class
        public Staff (string name, float rate)
        {
            NameOfStaff = name;
            hourlyRate = rate;
        }

        //CalculatePay Method
        public virtual void CalculatePay()
        {
            Console.WriteLine("Calculating Pay...");
            BasicPay = hWorked * hourlyRate;
            TotalPay = BasicPay;
        }

        //Message displayed on the payroll receipt
        public override string ToString()
        {
            return "\nNameOfStaff = " + NameOfStaff + "\nhourlyRate = " + hourlyRate + "\nhWorked = " + hWorked + "\nBasicPay = " + BasicPay + "\nTotalPay = " + TotalPay;
        }
    }

    //Child of the staff class
    class Manager : Staff
    {
        //Manager's hourly rate
        private const float managerHourlyRate = 50f;

        //Allowance the manager gets
        public int Allowance { get; private set; }

        //Constructor for the manager class
        public Manager(string name): base(name,managerHourlyRate)
        {

        }

        //Same as staff, except it takes the managers allowance into account
        public override void CalculatePay()
        {
            base.CalculatePay();
            Allowance = 1000;
            if (HoursWorked > 160)
            {
                TotalPay = BasicPay + Allowance;
            }
        }

        //Message displayed on the payroll receipt
        public override string ToString()
        {
            return "\nNameOfStaff = " + NameOfStaff + "\nmanagerHourlyRate = " + managerHourlyRate + "\nHoursWorked = " + HoursWorked + "\nBasicPay = " + BasicPay + "\nAllowance = " + Allowance + "\nTotalPay = " + TotalPay;
        }

        

    }

    class Admin : Staff
    {
        //Rate for overtime worked
        private const float overtimeRate = 15.5f;
        //Admin's hourly rate
        private const float adminHourlyrate = 30f;

        //Auto-implemented property for Overtime
        public float Overtime { get; private set; }

        //Constructor for the Admin class
        public Admin(string name) : base(name, adminHourlyrate)
        {

        }

        //Same as Staff and Manager, except it takes overtime into account
        public override void CalculatePay()
        {
            base.CalculatePay();
            if (HoursWorked > 160)
            {
                Overtime = overtimeRate * (HoursWorked - 160);
            }
        }

        //Message displayed on the payroll receipt
        public override string ToString()
        {
            return "\nNameOfStaff = " + NameOfStaff + "\nadminHourlyRate = " + adminHourlyrate + "\nHoursWorked = " + HoursWorked + "\nBasicPay = " + BasicPay + "\nOvertime = " + Overtime + "\nTotalPay = " + TotalPay;

        }
    }

    //This class reads the file staff.txt
    class FileReader
    {
        //ReadFile method
        public List<Staff> ReadFile()
        {
            /* Class Attributes needed to properly read the file,
             * The list staff, contains the list of all the staff members
             * the string result is the result from the list,
             * the string path is the path to staff.txt so the program can read it,
             * and finally, the string seperator seperates the the staff members name, and position
             */            
            List<Staff> myStaff = new List<Staff>();
            string[] result = new string[2];
            string path = "/Users/21Millisorz/Documents/GitHub/Payroll-Software/staff.txt";
            string seperator = ", ";

            /* If the file staff.txt exists, it will read it using streamreader,
             * and do the following, if the file staff.txt does not exist,
             * it will display the message "Error: File does not exist"
             * finally, it will return myStaff
             */
            if (File.Exists(path))
            {
                using(StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        result = sr.ReadLine().Split(seperator, StringSplitOptions.RemoveEmptyEntries);

                        if(result[1] == "Manager")
                        {
                            myStaff.Add(new Manager(result[0]));
                        }
                        else if(result[1] == "Admin")
                        {
                            myStaff.Add(new Admin(result[0]));
                        }
                        sr.Close();
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: File does not exist");
            }
            return myStaff;
        }
    }


    //Payslip class
    class PaySlip
    {
        //Month and year on the payslip
        private int month;
        private int year;

        //Enumeration for the months of the year
        enum MonthsOfYear { JAN = 1, FEB = 2, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC}

        //PaySlip constructor
        public PaySlip(int payMonth, int payYear)
        {
            month = payMonth;
            year = payYear;

        }

        //Method that generates the payslip
        public void GeneratePaySlip(List<Staff> myStaff)
        {
            string path;

            foreach(Staff f in myStaff)
            {
                path = f.NameOfStaff + ".txt";

                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine("PAYSLIP FOR {0} {1}", (MonthsOfYear)month, year);
                    sw.WriteLine("===================");
                    sw.WriteLine("Name of Staff: {0}", f.NameOfStaff);
                    sw.WriteLine("Hours Worked: {0}", f.HoursWorked);
                    sw.WriteLine(" ");
                    sw.WriteLine("Basic Pay: {0:C}", f.BasicPay);
                    if(f.GetType() == typeof(Manager))
                    {
                        sw.WriteLine("Allowance: {0:C}", ((Manager)f).Allowance);
                    }
                    else if(f.GetType() == typeof(Admin))
                    {
                        sw.WriteLine("Overtime: {0:C}", ((Admin)f).Overtime);
                    }

                    sw.WriteLine(" ");
                    sw.WriteLine("===================");

                    sw.WriteLine("Total Pay: {0:C}", f.TotalPay);
                    sw.WriteLine("===================");

                    sw.Close();
                }
            }
        }

        //Method that generates the summary
        public void GenerateSummary(List<Staff> myStaff)
        {
            var result
                = from f in myStaff
                  where f.HoursWorked < 10
                  orderby f.NameOfStaff ascending
                  select new { f.NameOfStaff, f.HoursWorked };

            string path = "/Users/21Millisorz/Documents/GitHub/Payroll-Software/summary.txt";

            using(StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine("Staff with less than 10 working hours");
                sw.WriteLine(" ");

                foreach(var f in result)
                {
                    sw.WriteLine("Name of Staff: {0}, Hours Worked: {1}", f.NameOfStaff, f.HoursWorked);
                }

                sw.Close();
            }
        }

        //Returns the month and the year
        public override string ToString()
        {
            return "month = " + month + "year = " + year;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
