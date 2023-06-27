using System;
using System.Reflection;
using System.Reflection.Emit;

namespace TStealer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConstructorInfo[] constructors = typeof(Student).GetConstructors();

            Console.WriteLine();
        }

        class Student
        {
            public Student()
            {

            }

            public Student(string name, int age)
            {
                Name = name;
                Age = age;
            }

            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
