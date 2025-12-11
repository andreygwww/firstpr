using System;

namespace StudentSystem
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Group { get; set; }

        public void Study()
        {
            Console.WriteLine($"Студент по имени {Name}, которому {Age} лет, учится в группе {Group}");
        }
    }

    public class Master : Student
    {
        public void DefendThesis()
        {
            Console.WriteLine($"Магистр {Name} успешно защищает диплом.");
        }
    }

    public class Bachelor : Student
    {
        public void TakeExams()
        {
            Console.WriteLine($"Бакалавр {Name} сдает экзамены.");
        }
    }

    public class Program
    {
        static void Main()
        {
            Master master = new Master();
            Console.Write("Введите Имя Магистра");
            master.Name = Console.ReadLine();
            Console.Write("Введите Возраст Магистра: ");
            master.Age = int.Parse(Console.ReadLine()); 
            Console.Write("Введите Группу Магистра");
            master.Group = Console.ReadLine();
            Bachelor bachelor = new Bachelor();
            Console.Write("Введите Имя Бакалавра");
            bachelor.Name = Console.ReadLine();
            Console.Write("Введите Возраст Бакалавра");
            bachelor.Age = int.Parse(Console.ReadLine());
            Console.Write("Введите Группу Бакалавра");
            bachelor.Group = Console.ReadLine();
            master.Study();
            master.DefendThesis();
            bachelor.Study();
            bachelor.TakeExams();
        }
    }
}