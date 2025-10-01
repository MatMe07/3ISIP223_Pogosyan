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

    class Teacher : Person
    {
        public static int NextId = 0;
        public List<Course> Courses { get; set; }
        public int Cabinet { get; set; }
        public void InfoTeacher()
        {

        }

        public Teacher(string fIO, DateOnly dateOfBirth, char gender, int cabinet) : base(fIO, dateOfBirth, gender)
        { 
            base.ID = NextId++; 
            Cabinet = cabinet;
        }

    }

    class Course
    {
        public string Name { get; set; }
        public List<Student> Students { get; set; }

        public int CountStudent { get; set; }

        public Course(string name)
        {
            Name = name;
        }

        public bool AddStudent()
        {
            return true;
        }
        public void InfoCourse()
        {

        }
    }

    class University
    {
        public List<Course> Courses { get; set; }
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }


    }

}
