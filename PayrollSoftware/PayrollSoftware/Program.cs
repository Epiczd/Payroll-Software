using System;

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
        public float TotalPay;
        public float BasicPay;
        public string NameOfStaff;
        public int HoursWorked;

        public Staff (string name, float rate)
        {

        }

        public virtual void CalculatePay()
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    class Manager : Staff
    {

    }

    class Admin : Staff
    {

    }

    class FileReader
    {

    }

    class PaySlip
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
