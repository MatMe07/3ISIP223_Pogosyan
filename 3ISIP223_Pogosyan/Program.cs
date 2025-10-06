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
                    case "3": FindStudentInCourse(); break;
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
                    case "2": PrintTeacherInfo(); break;
                    case "3": FindTeacherToCourse(); break;
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
                    case "2": PrintStatistics(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор!"); break;
                }
                Console.WriteLine("Нажмите Enter!");
                Console.ReadLine();
            }
        }

        static void AddStudent()
        {
            Console.Write("Введите ФИО студента: ");
            string fio = Console.ReadLine();

            Console.Write("Введите дату рождения (гггг-мм-дд): ");
            if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly dateOfBirth))
            {
                Console.WriteLine("Неверный формат даты!");
                return;
            }

            Console.Write("Введите пол (M/Ж): ");
            char gender = Console.ReadKey().KeyChar;
            Console.WriteLine();

            Student student = new Student(fio, dateOfBirth, gender);
            university.AddStudent(student);
            Console.WriteLine($"Студент добавлен с ID: {student.ID}");
        }
        static void AddTeacher()
        {
            Console.Write("Введите ФИО преподавателя: ");
            string fio = Console.ReadLine();

            Console.Write("Введите дату рождения (гггг-мм-дд): ");
            if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly dateOfBirth))
            {
                Console.WriteLine("Неверный формат даты!");
                return;
            }

            Console.Write("Введите пол (M/Ж): ");
            char gender = Console.ReadKey().KeyChar;
            Console.WriteLine();

            Console.Write("Введите номер кабинета: ");
            if (!int.TryParse(Console.ReadLine(), out int cabinet))
            {
                Console.WriteLine("Неверный формат номера кабинета!");
                return;
            }

            Teacher teacher = new Teacher(fio, dateOfBirth, gender, cabinet);
            university.AddTeacher(teacher);
            Console.WriteLine($"Преподаватель добавлен с ID: {teacher.ID}");
        }

        static void AddCourse()
        {
            Console.Write("Введите название курса: ");
            string name = Console.ReadLine();

            Console.Write("Введите максимальное количество студентов на курсе: ");
            if (!int.TryParse(Console.ReadLine(), out int maxStudents) || maxStudents <= 0)
            {
                Console.WriteLine("Неверный формат! Установлено значение по умолчанию: 30");
                maxStudents = 30;
            }

            Course course = new Course(name, maxStudents);
            university.AddCourse(course);
            Console.WriteLine($"Курс создан с ID: {course.ID}, максимальное количество студентов: {maxStudents}");
        }

        static void FindStudentInCourse()
        {
            Console.Write("Введите ID студента: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Неверный формат ID!");
                return;
            }

            Console.Write("Введите ID курса: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Неверный формат ID!");
                return;
            }

            university.FindStudentInCourse(studentId, courseId);
        }

        static void AddGradeToStudent()
        {
            Console.Write("Введите ID студента: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Неверный формат ID!");
                return;
            }

            Console.Write("Введите ID курса: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Неверный формат ID!");
                return;
            }

            Console.Write("Введите оценку (2-5): ");
            if (!int.TryParse(Console.ReadLine(), out int grade) || grade < 2 || grade > 5)
            {
                Console.WriteLine("Неверная оценка! Допустимые значения: 2, 3, 4, 5");
                return;
            }

            university.AddGradeToStudent(studentId, courseId, grade);
        }

        static void FindTeacherToCourse()
        {
            Console.Write("Введите ID преподавателя: ");
            if (!int.TryParse(Console.ReadLine(), out int teacherId))
            {
                Console.WriteLine("Неверный формат ID!");
                return;
            }

            Console.Write("Введите ID курса: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Неверный формат ID!");
                return;
            }

            university.FindTeacherToCourse(teacherId, courseId);
        }

        static void PrintStudentInfo()
        {
            Console.Write("Введите ID студента: ");
            if (!int.TryParse(Console.ReadLine(), out int studentId))
            {
                Console.WriteLine("Неверный формат ID!");
                return;
            }

            university.PrintStudentInfo(studentId);
        }

        static void PrintTeacherInfo()
        {
            Console.Write("Введите ID преподавателя: ");
            if (!int.TryParse(Console.ReadLine(), out int teacherId))
            {
                Console.WriteLine("Неверный формат ID!");
                return;
            }

            university.PrintTeacherInfo(teacherId);
        }

        static void CourseInfo()
        {
            Console.Write("Введите ID курса: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Неверный формат ID!");
                return;
            }

            university.CourseInfo(courseId);
        }

        static void PrintStatistics()
        {
            Console.WriteLine("=== Статистика университета ===");
            Console.WriteLine($"Количество студентов: {university.Students.Count}");
            Console.WriteLine($"Количество преподавателей: {university.Teachers.Count}");
            Console.WriteLine($"Количество курсов: {university.Courses.Count}");
            var studentsWithGrades = university.Students.Where(s => s.GetAllGrades().Count != 0).ToList();
            if (studentsWithGrades.Count != 0)
            {
                double overallAverage = studentsWithGrades.Average(s => s.CalculateAverageGrade());
                Console.WriteLine($"Средний балл всех студентов: {overallAverage}");
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
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void AddTeacher(Teacher teacher)
        {
            Teachers.Add(teacher);
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }

        public void FindStudentInCourse(int studentId, int courseId)
        {
            var student = Students.FirstOrDefault(s => s.ID == studentId);
            var course = Courses.FirstOrDefault(c => c.ID == courseId);

            if (student == null)
            {
                Console.WriteLine("Студент не найден!");
                return;
            }

            if (course == null)
            {
                Console.WriteLine("Курс не найден!");
                return;
            }

            if (course.AddStudent(student))
            {
                Console.WriteLine($"Студент {student.FIO} успешно записан на курс {course.Name}");
            }
            else
            {
                Console.WriteLine("Не удалось записать студента на курс!");
            }
        }

        public void AddGradeToStudent(int studentId, int courseId, int grade)
        {
            var student = Students.FirstOrDefault(s => s.ID == studentId);
            var course = Courses.FirstOrDefault(c => c.ID == courseId);

            if (student == null)
            {
                Console.WriteLine("Студент не найден!");
                return;
            }

            if (course == null)
            {
                Console.WriteLine("Курс не найден!");
                return;
            }

            if (!student.Courses.Contains(course))
            {
                Console.WriteLine("Студент не записан на этот курс!");
                return;
            }

            student.AddGrade(courseId, grade);
            Console.WriteLine($"Студенту {student.FIO} выставлена оценка {grade} за курс {course.Name}");
        }

        public void FindTeacherToCourse(int teacherId, int courseId)
        {
            var teacher = Teachers.FirstOrDefault(t => t.ID == teacherId);
            var course = Courses.FirstOrDefault(c => c.ID == courseId);

            if (teacher == null)
            {
                Console.WriteLine("Преподаватель не найден!");
                return;
            }

            if (course == null)
            {
                Console.WriteLine("Курс не найден!");
                return;
            }

            course.Teacher = teacher;
            teacher.Courses.Add(course);
            Console.WriteLine($"Преподаватель {teacher.FIO} назначен на курс {course.Name}");
        }


        public void PrintStudentInfo(int studentId)
        {
            var student = Students.FirstOrDefault(s => s.ID == studentId);
            if (student != null)
            {
                student.InfoStudent();
            }
            else
            {
                Console.WriteLine("Студент не найден!");
            }
        }

        public void PrintTeacherInfo(int teacherId)
        {
            var teacher = Teachers.FirstOrDefault(t => t.ID == teacherId);
            if (teacher != null)
            {
                teacher.InfoTeacher();
            }
            else
            {
                Console.WriteLine("Преподаватель не найден!");
            }
        }

        public void CourseInfo(int courseId)
        {
            var course = Courses.FirstOrDefault(c => c.ID == courseId);
            if (course != null)
            {
                course.InfoCourse();
            }
            else
            {
                Console.WriteLine("Курс не найден!");
            }
        }

        public void PrintStudents()
        {
            Console.WriteLine("=== Список всех студентов ===");
            if (Students.Count != 0)
            {
                foreach (var student in Students)
                {
                    student.InfoStudent();
                    Console.WriteLine("---");
                }
            }
            else
            {
                Console.WriteLine("Студентов нет в системе");
            }
        }

        public void PrintTeachers()
        {
            Console.WriteLine("=== Список всех преподавателей ===");
            if (Teachers.Count != 0)
            {
                foreach (var teacher in Teachers)
                {
                    teacher.InfoTeacher();
                    Console.WriteLine("---");
                }
            }
            else
            {
                Console.WriteLine("Преподавателей нет в системе");
            }
        }

        public void PrintCourses()
        {
            Console.WriteLine("=== Список всех курсов ===");
            if (Courses.Count != 0)
            {
                foreach (var course in Courses)
                {
                    course.InfoCourse();
                    Console.WriteLine("---");
                }
            }
            else
            {
                Console.WriteLine("Курсов нет в системе");
            }
        }

        public void PrintAll()
        {
            PrintStudents();
            PrintTeachers();
            PrintCourses();
        }

    }

}