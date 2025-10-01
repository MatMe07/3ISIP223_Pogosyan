using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan
{
    internal class Program
    {

        static void Main(string[] args)
        { 
            
        }

    }

    class Person
    {
        public string FIO { get; set; }
        public int ID { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public char Gender { get; set; }

        public Person(string fIO, DateOnly dateOfBirth, char gender)
        {
            FIO = fIO;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }
    }

    class Student : Person
    {
        public static int  NextId = 0;
        public List<Course> Courses { get; set; }
        public List<int> Marks { get; set; }

        public Student(string fIO, DateOnly dateOfBirth, char gender): base(fIO, dateOfBirth, gender) 
        {
            base.ID = NextId++;

        }

        public void InfoCourse()
        {

        }
        public void InfoStudent()
        {

        }

        public void InfoMarks()
        {

        }


    }





}
