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
        static University university = new University();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Система управления университетом ===\n");
                Console.WriteLine("1. Работа со студентами");
                Console.WriteLine("2. Работа с преподавателями");
                Console.WriteLine("3. Работа с курсами");
                Console.WriteLine("4. Общая информация");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите категорию: ");

                string n = Console.ReadLine();
                Console.WriteLine();

                switch (n)
                {
                    case "1": StudentMenu(); break;
                    case "2": TeacherMenu(); break;
                    case "3": CourseMenu(); break;
                    case "4": GeneralInfoMenu(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор!"); break;
                }

                Console.WriteLine("Нажмите Enter!");
                Console.ReadLine();
            }
        }


        static void StudentMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Работа со студентами ===\n");
                Console.WriteLine("1. Добавить студента");
                Console.WriteLine("2. Просмотреть информацию о студенте");
                Console.WriteLine("3. Записать студента на курс");
                Console.WriteLine("4. Выставить оценку студенту");
                Console.WriteLine("5. Список всех студентов");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите опцию: ");

                string n = Console.ReadLine();
                Console.WriteLine();

                switch (n)
                {
                    case "1": AddStudent(); break;
                    case "2": PrintStudentInfo(); break;
                    case "3": EnrollStudentInCourse(); break;
                    case "4": AddGradeToStudent(); break;
                    case "5": university.PrintStudents(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор!"); break;
                }
                Console.WriteLine("Нажмите Enter!");
                Console.ReadLine();
            }
        }

        static void TeacherMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Работа с преподавателями ===\n");
                Console.WriteLine("1. Добавить преподавателя");
                Console.WriteLine("2. Просмотреть информацию о преподавателе");
                Console.WriteLine("3. Назначить преподавателя на курс");
                Console.WriteLine("4. Список всех преподавателей");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите опцию: ");

                string n = Console.ReadLine();
                Console.WriteLine();

                switch (n)
                {
                    case "1": AddTeacher(); break;
                    case "2": ShowTeacherInfo(); break;
                    case "3": AssignTeacherToCourse(); break;
                    case "4": university.PrintTeachers(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор!"); break;
                }
                Console.WriteLine("Нажмите Enter!");
                Console.ReadLine();
            }
        }

        static void CourseMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Работа с курсами ===\n");
                Console.WriteLine("1. Создать курс");
                Console.WriteLine("2. Просмотреть информацию о курсе");
                Console.WriteLine("3. Список всех курсов");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите опцию: ");

                string n = Console.ReadLine();
                Console.WriteLine();

                switch (n)
                {
                    case "1": AddCourse(); break;
                    case "2": CourseInfo(); break;
                    case "3": university.PrintCourses(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор!"); break;
                }
                Console.WriteLine("Нажмите Enter!");
                Console.ReadLine();
            }
        }

        static void GeneralInfoMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Общая информация ===\n");
                Console.WriteLine("1. Полная информация об университете");
                Console.WriteLine("2. Статистика университета");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите опцию: ");

                string n = Console.ReadLine();
                Console.WriteLine();

                switch (n)
                {
                    case "1": university.PrintAll(); break;
                    case "2": ShowStatistics(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор!"); break;
                }
                Console.WriteLine("Нажмите Enter!");
                Console.ReadLine();
            }
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

        public virtual void PrintInfo()
        {
            Console.WriteLine($"ФИО: {FIO}");
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"Дата рождения: {DateOfBirth}");
            Console.WriteLine($"Пол: {Gender}");
        }
    }

    class Student : Person
    {
        public static int NextId = 1;
        public List<Course> Courses { get; set; } = new List<Course>();
        public Dictionary<int, List<int>> Grades { get; set; } = new Dictionary<int, List<int>>();

        public Student(string fIO, DateOnly dateOfBirth, char gender) : base(fIO, dateOfBirth, gender)
        {
            ID = NextId++;
        }
        public void AddGrade(int courseId, int grade)
        {
            if (!Grades.ContainsKey(courseId))
            {
                Grades[courseId] = new List<int>();
            }
            Grades[courseId].Add(grade);
        }

        public List<int> GetGradesForCourse(int courseId)
        {
            return Grades.ContainsKey(courseId) ? Grades[courseId] : new List<int>();
        }

        public List<int> GetAllGrades()
        {
            return Grades.Values.SelectMany(g => g).ToList();
        }

        public double CalculateAverageGrade()
        {
            var allGrades = GetAllGrades();
            return allGrades.Count != 0 ? allGrades.Average() : 0;
        }

        public double CalculateAverageGradeForCourse(int courseId)
        {
            var courseGrades = GetGradesForCourse(courseId);
            return courseGrades.Count != 0 ? courseGrades.Average() : 0;
        }
        public void InfoStudent()
        {
            PrintInfo();
            Console.WriteLine("Курсы:");
            if (Courses.Count != 0)
            {
                foreach (var course in Courses)
                {
                    var courseGrades = GetGradesForCourse(course.ID);
                    double courseAverage = CalculateAverageGradeForCourse(course.ID);
                    Console.WriteLine($"  - {course.Name} (ID: {course.ID})");
                    Console.WriteLine($"    Оценки: {(courseGrades.Count != 0 ? string.Join(", ", courseGrades) : "нет оценок")}");
                    if (courseGrades.Count != 0)
                    {
                        Console.WriteLine($"    Средний балл: {courseAverage}");
                    }
                }
            }
            else
            {
                Console.WriteLine("  Нет записей на курсы");
            }

            double overallAverage = CalculateAverageGrade();
            Console.WriteLine($"Общий средний балл: {(GetAllGrades().Count != 0 ? overallAverage.ToString("F2") : "нет оценок")}");
        }


    }

    class Teacher : Person
    {
        public static int NextId = 1;
        public List<Course> Courses { get; set; } = new List<Course>();
        public int Cabinet { get; set; }
        public Teacher(string fIO, DateOnly dateOfBirth, char gender, int cabinet) : base(fIO, dateOfBirth, gender)
        {
            ID = NextId++;
            Cabinet = cabinet;
        }

        public void InfoTeacher()
        {
            PrintInfo();
            Console.WriteLine($"Кабинет: {Cabinet}");
            Console.WriteLine("Курсы:");
            if (Courses.Count != 0)
            {
                foreach (var course in Courses)
                {
                    Console.WriteLine($"  - {course.Name} (ID: {course.ID})");
                }
            }
            else
            {
                Console.WriteLine("  Нет назначенных курсов");
            }
        }

    }

    class Course
    {
        public static int NextId = 1;
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public Teacher Teacher { get; set; }
        public int MaxStudents { get; set; }
        public int CountStudent => Students.Count;
        public bool IsFull => Students.Count >= MaxStudents;
        public Course(string name, int maxStudents = 30)
        {
            ID = NextId++;
            Name = name;
            MaxStudents = maxStudents;
        }

        public bool AddStudent(Student student)
        {
            if (IsFull)
            {
                Console.WriteLine($"Курс '{Name}' заполнен! Максимальное количество студентов: {MaxStudents}");
                return false;
            }

            if (!Students.Contains(student))
            {
                Students.Add(student);
                student.Courses.Add(this);
                return true;
            }
            return false;
        }
        public void InfoCourse()
        {
            Console.WriteLine($"Курс: {Name}");
            Console.WriteLine($"ID: {ID}");
            Console.WriteLine($"Преподаватель: {(Teacher != null ? Teacher.FIO : "не назначен")}");
            Console.WriteLine($"Студентов: {CountStudent}/{MaxStudents} {(IsFull ? "(ЗАПОЛНЕН)" : "")}");
            Console.WriteLine("Студенты:");
            if (Students.Count != 0)
            {
                foreach (var student in Students)
                {
                    var studentGrades = student.GetGradesForCourse(ID);
                    double averageGrade = student.CalculateAverageGradeForCourse(ID);
                    Console.WriteLine($"  - {student.FIO} (ID: {student.ID})");
                    if (studentGrades.Count != 0)
                    {
                        Console.WriteLine($"    Оценки: {string.Join(", ", studentGrades)} | Средний: {averageGrade}");
                    }
                }
            }
            else
            {
                Console.WriteLine("  Нет записанных студентов");
            }
        }
    }

    class University
    {
        public List<Course> Courses { get; set; }
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }


    }

}