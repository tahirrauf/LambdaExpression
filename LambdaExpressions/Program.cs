/*
 * Author: Tahir Rauf
 * Title: Use of Lambda Expressions
 * Description: This tutorial is created to help newbies understand how Lambda exprssion works
 * This takes a learner from initial concepts of Delegate to the replacement of it with newer concepts
 * of Lamda expression and then gradually taking the learning to Func and how its used in LINQ
 * I have also touched what Func, Action and Predicate is
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaExpressions
{
    class Program
    {
        delegate bool IsEligible(Student s);
        delegate bool IsAddmissible(Student s, Grade g);
        static void Main(string[] args)
        {
            DelegateExample();
            LambdaExample();
            MultipleArgLamdaExamle();
            LamdaExpressionUsingFuncKeyword();
            LamdaExpressionUsingActionKeyword();
            LamdaExpressionWithFuncInLinqKeyword();
            UsingPredicate();
            Console.ReadKey();
        }

        static void DelegateExample()
        {
            // Simple example usign Delegate keyword
            IsEligible isEligible = delegate (Student s) { return (s.Age > 18 && s.Height > 5.5); };

            Console.WriteLine(isEligible(new Student(10, 5.4f)));
            Console.WriteLine(isEligible(new Student(19, 5.6f)));
        }

        static void LambdaExample()
        {
            //This is same as DelegateExample, but using Lambda expression, which means you don't have to use the delgate keyword
            // In s=> s.Age>18, s is the input parameter, => is the lambda operator and the rest is the 
            // Body Lambda Expression
            IsEligible isEligible = s =>
            {
                Console.WriteLine("calling LambdaExample()");
                return s.Age > 18 && s.Height > 5.5;
            };

            Console.WriteLine(isEligible(new Student(10, 5.4f)));
            Console.WriteLine(isEligible(new Student(19, 5.6f)));
        }

        static void MultipleArgLamdaExamle()
        {
            IsAddmissible isAdmissible = (s, g) => s.Age > 18 && s.Height > 5.5 && g.GradeTaken > 3.5;

            Console.WriteLine(isAdmissible(new Student(10, 5.4f), new Grade(3.2)));
            Console.WriteLine(isAdmissible(new Student(19, 5.6f), new Grade(3.6)));
        }

        static void LamdaExpressionUsingFuncKeyword()
        {
            //Func is the generic delegate type by .NET, you don't have to explicity declare the delegate
            // if you use Func, it will automatically translate to the deleage
            // Last parameter in Func expression is the return type all the other parameters are input types
            // For exmaple Func<int,int, bool>, int and int are input parameters but bool is return type

            Func<Student, Grade, bool> isAdmissible = (s, g) => s.Age > 18 && s.Height > 5.5 && g.GradeTaken > 3.5;

            Console.WriteLine(isAdmissible(new Student(10, 5.4f), new Grade(3.2)));
            Console.WriteLine(isAdmissible(new Student(19, 5.6f), new Grade(3.6)));
        }

        static void LamdaExpressionUsingActionKeyword()
        {
            // Action method is same as Func, the only difference is that Action does not have any return type
            // it only takes inputs

            Action<Student, Grade> printStudentAndGrade = (s, g) => Console.WriteLine("Age: {0}, Height: {1} - Grade: {2}",s.Age, s.Height, g.GradeTaken);
            printStudentAndGrade(new Student(10, 5.4f), new Grade(3.2));
        }

        static void LamdaExpressionWithFuncInLinqKeyword()
        {
            List<Student> students = new List<Student>();
            students.Add(new Student(16, 1));
            students.Add(new Student(26, 5.6f));
            students.Add(new Student(36, 7));
            students.Add(new Student(46, 4));

            // Traditional Lambda Expression Style
            Func<Student, bool> isAllowed = s => s.Age > 18 && s.Height > 5.5;

            var allowedStudents = students.Where(isAllowed).ToList<Student>();
            Console.WriteLine("---------Traditional Lambda Expression Style----------");
            foreach(Student s in allowedStudents)
            {
                Console.WriteLine("Age: {0}, Height: {1}", s.Age, s.Height);
            }

            // LINQ style
            Console.WriteLine("---------LINQ Style----------");
            var allowedStudentsLinq = from s in students
                              where (isAllowed(s))
                              select s;
            foreach (Student s in allowedStudentsLinq)
            {
                Console.WriteLine("Age: {0}, Height: {1}", s.Age, s.Height);
            }
        }

        static void UsingPredicate()
        {
            //Predicate is just like lambda expression but is used to match elements from list etc
            // A Predicate alwyas returns bool, so basically this is equal
            // Predicate<T> == Func<T,bool>

            List<Student> students = new List<Student>();
            students.Add(new Student(16, 1));
            students.Add(new Student(26, 5.6f));
            students.Add(new Student(36, 7));
            students.Add(new Student(46, 4));

            Student age16Student = students.Find(s => s.Age == 16);
            List<Student> elderThan30Students = students.FindAll(s => s.Age >= 30);
        }

    }

    class Student
    {
        public int Age
        {
            get; set;
        }

        public float Height
        {
            get; set;
        }

        public Student(int ageArg, float heightArg)
        {
            Age = ageArg;
            Height = heightArg;
        }
    }

    class Grade
    {
        public double GradeTaken
        {
            get; set;
        }

        public Grade(double gradeArg)
        {
            GradeTaken = gradeArg;
        }
    }

    
}
