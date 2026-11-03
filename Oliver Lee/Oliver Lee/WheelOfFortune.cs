namespace Oliver_Lee
{
    public struct Student
    {
        public string name;
        public string surname;
        public string interest;
        public int score;
    }
    internal class WheelOfFortune
    {
        static void Main()
        {
            Student[] students = new Student[23];
            int i = 0;
            string temp;
            Student tempS;
            bool playing = true, sorted = false;
            StreamReader sr = new StreamReader(@"wheelOfFortune.txt");

            while (!sr.EndOfStream)
            {
                students[i].name = sr.ReadLine();
                students[i].surname = sr.ReadLine();
                students[i].interest = sr.ReadLine();
                students[i].score = Convert.ToInt32(sr.ReadLine());
                i++;
            }
            sr.Close();

            do
            {
                sorted = true;
                for (i = 1; i < students.Length; i++)
                {
                    if (students[i - 1].surname.CompareTo(students[i].surname) > 0)
                    {
                        tempS = students[i - 1];
                        students[i - 1] = students[i];
                        students[i] = tempS;
                        sorted = false;
                    }
                }
            } while (!sorted);

            while (playing)
            {
                Console.Clear();
                Console.WriteLine("Wheel of Fortune");
                Console.WriteLine("1. Play\n2. Show contestants\n3. Edit contestants\n0. Quit");
                Console.Write(">> ");
                temp = Console.ReadLine();
                Console.Clear();
                switch (temp)
                {
                    case "1":
                        Play();
                        break;
                    case "2":
                        DisplayStudents(students);
                        break;
                    case "3":
                        EditStudents(ref students);
                        break;
                    case "0":
                        playing = false;
                        break;
                    default:
                        Console.WriteLine($"{temp} is not a valid input");
                        Console.ReadLine();
                        break;
                }
            }
        }
        static void DisplayStudents(Student[] students)
        {
            int i;

            for (i = 0; i < students.Length; i++)
            {
                Console.WriteLine($"{students[i].name.PadRight(15)}{students[i].surname.PadRight(25)}{students[i].interest.PadRight(25)}{students[i].score}");
            }
            Console.ReadLine();
        }
        static void EditStudents(ref Student[] students)
        {
            string temp;
            int i, picked = 0;
            bool found = false, check = false;

            Console.WriteLine("Which contestant would you like to edit?");
            Console.Write(">> ");
            temp = Console.ReadLine();
            Console.Clear();


            for (i = 0; i < students.Length; i++)
            {
                if (students[i].name.ToLower() ==  temp.ToLower())
                {
                    picked = i;
                    found = true;
                }
            }

            if (found)
            {
                Edit(ref students, picked);
            }
            else
            {
                for (i = 0; i < students.Length; i++)
                {
                    if (students[i].surname.ToLower() == temp.ToLower())
                    {
                        picked = i;
                        found = true;
                    }
                }
                if (found)
                {
                    Edit(ref students, picked);
                }
                else
                {
                    Console.WriteLine($"No contestant \"{temp}\" found");
                    Console.ReadLine();
                }
            }
            
        }
        static void Play()
        {

        }
        static void Edit(ref Student[] students, int picked)
        {
            bool check = false;
            string temp, change;

            do
            {
                Console.Clear();
                Console.WriteLine($"Contestant {students[picked].name} {students[picked].surname} found\nEdit this contestant? (Y/N)");
                Console.Write(">> ");
                temp = Console.ReadLine();
                Console.Clear();

                switch (temp.ToLower())
                {
                    case "y":
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Choose a field to edit\n1. Name\n2. Surname\n3. Interest\n0. Back");
                            Console.Write(">> ");
                            temp = Console.ReadLine();
                            Console.Clear();
                            switch (temp)
                            {
                                case "1":
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Enter a new name");
                                        Console.Write(">> ");
                                        change = Console.ReadLine();
                                        Console.Clear();
                                        Console.WriteLine($"Updated contestant:\n{change} {students[picked].surname} - {students[picked].interest}\nIs this information correct? (Y/N)");
                                        Console.Write(">> ");
                                        temp = Console.ReadLine();
                                        Console.Clear();
                                        switch (temp.ToLower())
                                        {
                                            case "y":
                                                StreamWriter sw = new StreamWriter(@"wheelOfFortune.txt");
                                                students[picked].name = change;

                                                for (int i = 0; i < students.Length; i++)
                                                {
                                                    sw.WriteLine(students[i].name);
                                                    sw.WriteLine(students[i].surname);
                                                    sw.WriteLine(students[i].interest);
                                                    sw.WriteLine(students[i].score);
                                                    i++;
                                                }
                                                sw.Close();
                                                Console.WriteLine("Name saved");
                                                check = true;
                                                break;
                                            case "n":
                                                break;
                                            default:
                                                Console.WriteLine($"{temp} is not a valid input");
                                                Console.ReadLine();
                                                break;
                                        }
                                    } while (!check);
                                    check = false;
                                    break;
                                case "2":
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Enter a new surname");
                                        Console.Write(">> ");
                                        change = Console.ReadLine();
                                        Console.Clear();
                                        Console.WriteLine($"Updated contestant:\n{students[picked].name} {change} - {students[picked].interest}\nIs this information correct? (Y/N)");
                                        Console.Write(">> ");
                                        temp = Console.ReadLine();
                                        Console.Clear();
                                        switch (temp.ToLower())
                                        {
                                            case "y":
                                                StreamWriter sw = new StreamWriter(@"wheelOfFortune.txt");
                                                students[picked].surname = change;

                                                for (int i = 0; i < students.Length; i++)
                                                {
                                                    sw.WriteLine(students[i].name);
                                                    sw.WriteLine(students[i].surname);
                                                    sw.WriteLine(students[i].interest);
                                                    sw.WriteLine(students[i].score);
                                                    i++;
                                                }
                                                sw.Close();
                                                Console.WriteLine("Surname saved");
                                                check = true;
                                                break;
                                            case "n":
                                                break;
                                            default:
                                                Console.WriteLine($"{temp} is not a valid input");
                                                Console.ReadLine();
                                                break;
                                        }
                                    } while (!check);
                                    check = false;
                                    break;
                                case "3":
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Enter a new interest");
                                        Console.Write(">> ");
                                        change = Console.ReadLine();
                                        Console.Clear();
                                        Console.WriteLine($"Updated contestant:\n{students[picked].name} {students[picked].surname} - {change}\nIs this information correct? (Y/N)");
                                        Console.Write(">> ");
                                        temp = Console.ReadLine();
                                        Console.Clear();
                                        switch (temp.ToLower())
                                        {
                                            case "y":
                                                StreamWriter sw = new StreamWriter(@"wheelOfFortune.txt");
                                                students[picked].interest = change;

                                                for (int i = 0; i < students.Length; i++)
                                                {
                                                    sw.WriteLine(students[i].name);
                                                    sw.WriteLine(students[i].surname);
                                                    sw.WriteLine(students[i].interest);
                                                    sw.WriteLine(students[i].score);
                                                    i++;
                                                }
                                                sw.Close();
                                                Console.WriteLine("Interest saved");
                                                check = true;
                                                break;
                                            case "n":
                                                break;
                                            default:
                                                Console.WriteLine($"{temp} is not a valid input");
                                                Console.ReadLine();
                                                break;
                                        }
                                    } while (!check);
                                    check = false;
                                    break;
                                case "0":
                                    check = true;
                                    break;
                                default:
                                    Console.WriteLine($"{temp} is not a valid input");
                                    Console.ReadLine();
                                    break;
                            }
                        } while (!check);
                        check = false;
                        break;
                    case "n":
                        check = true;
                        break;
                    default:
                        Console.WriteLine($"{temp} is not a valid input");
                        Console.ReadLine();
                        break;
                }
            } while (!check);
        }
    }
}
