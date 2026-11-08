using System.Globalization;
using System.Text;
using System.Threading.Tasks;

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
        public static bool stop = false;
        static void Main()
        {
            Student[] students = new Student[23];
            int i = 0;
            string temp;
            Student tempS;
            bool playing = true, sorted = false;
            StreamReader sr = new StreamReader(@"../../../wheelOfFortune.txt");

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
                        Play(ref students);
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
            Console.WriteLine("\nPress [Enter] to go back");
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
        static void Play(ref Student[] students)
        {
            int[] finalists = new int[10], values = [650, 3500, 900, 5000, 550, 300, 1500, 400, 2500, 850, 3000, 750, -2000, 700, 2000, 450, 1000, 350, 800, -3000, 600, 4000, 1200, 500];
            bool dupe, check;
            int hold, temp = 0;
            double x;
            Random rand = new Random();
            string[] words = ["historical", "understand", "volunteers", "generation", "discovery", "chemistry", "literature", "navigation", "revolution", "confidence"];

            foreach (int i in finalists)
            {
                do
                {
                    dupe = false;
                    hold = rand.Next(students.Length);
                    for (int j = 0; j < finalists.Length; j++)
                    {
                        if (hold == finalists[j])
                        {
                            dupe = true;
                        }
                    }
                } while (dupe);
                finalists[temp] = hold;
                temp++;
            }
            Console.WriteLine($"The finalists are:");
            for (int i = 0; i < finalists.Length; i++)
            {
                Console.WriteLine($"{students[finalists[i]].name.PadRight(15)}{students[finalists[i]].surname}");
            }
            Console.WriteLine("\nPress [Enter] to continue");
            Console.ReadLine();
            Console.Clear();
            hold = rand.Next(finalists.Length);
            Console.WriteLine("Our player is:");
            Console.WriteLine($"{students[finalists[hold]].name} {students[finalists[hold]].surname}");
            Console.WriteLine("\nPress [Enter] to start");
            Console.ReadLine();
            bool win = false;
            string word = words[rand.Next(words.Length)], guessed = new string('_', word.Length), letr;
            char guess, _ = ' ';
            int score = 0;
            char[] guesses = [];
            
            do
            {
                temp = rand.Next(values.Length);
                Timer();
                x = 1;
                do
                {
                    Console.Clear();
                    Console.WriteLine($"   {values[temp % values.Length]}\n   {values[(temp + 1) % values.Length]}\n>> {values[(temp + 2) % values.Length].ToString().PadRight(6)}<<\n   {values[(temp + 3) % values.Length]}\n   {values[(temp + 4) % values.Length]}");
                    Thread.Sleep(Convert.ToInt32(Math.Pow(10, x)));
                    x += 0.03;
                    temp++;
                } while (!stop);
                stop = false;
                check = false;
                Thread.Sleep(2000);
                if (values[(temp + 1) % values.Length] < 0)
                {
                    score += values[(temp + 1) % values.Length];
                    check = true;
                }

                while (!check)
                {
                    Console.Clear();
                    Console.WriteLine($"Current player: {students[finalists[hold]].name} {students[finalists[hold]].surname}\n");
                    Console.WriteLine(guessed);
                    Console.Write($"\nCurrent letter value: {values[(temp + 1) % values.Length]}\nGuessed letters:");
                    if (guesses.Length > 0)
                    {
                        Array.Sort(guesses);
                        for (int i = 0; i < guesses.Length; i++)
                        {
                            if (i % 5 == 0)
                            {
                                Console.Write($"\n{guesses[i]} ");
                            }
                            else
                            {
                                Console.Write($"{guesses[i]} ");
                            }
                        }
                    }
                    Console.WriteLine($"\n\nScore: {score}");
                    Console.WriteLine("Guess a letter: (Vowels cost $250)");
                    Console.Write(">> ");
                    letr = Console.ReadLine().ToLower();
                    dupe = false;
                    if (Char.TryParse(letr, out _))
                    {
                        guess = Convert.ToChar(letr);
                        for (int i = 0; i < guesses.Length; i++)
                        {
                            if (guesses[i] == guess)
                            {
                                guess = '0';
                            }
                        }
                        switch (guess)
                        {
                            case 'a' or 'e' or 'i' or 'o' or 'u':
                                if (score >= 250)
                                {
                                    score -= 250;
                                    Letter(guess, ref guessed, values[(temp + 1) % values.Length], ref score, word);
                                    guesses = [.. guesses, guess];
                                    check = true;
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Not enough money for a vowel");
                                    Console.ReadLine();
                                }
                                break;
                            case char c when c >= 'a' && c <= 'z':
                                Letter(guess, ref guessed, values[(temp + 1) % values.Length], ref score, word);
                                guesses = [.. guesses, guess];
                                check = true;
                                break;
                            default:
                                Console.Clear();
                                Console.WriteLine("Please enter a unique alphabetical character");
                                Console.ReadLine();
                                break;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a single letter");
                        Console.ReadLine();
                    }
                }
                check = false;
                Console.Clear();
                if (guessed == word)
                {
                    win = true;
                    break;
                }
                Console.WriteLine(guessed);
                Console.WriteLine($"\nScore: {score}");
                Console.WriteLine("\nPress [Enter] to spin again");
                Console.ReadLine();
            } while (!win);
        }
        static void Letter(char guess, ref string guessed, int value, ref int score, string word)
        {
            StringBuilder sb = new StringBuilder(guessed);
            for (int i = 0; i < guessed.Length; i++)
            {
                if (guess == word[i])
                {
                    sb[i] = guess;
                    score += value;
                }
            }
            guessed = sb.ToString();
        }
        static async Task Timer()
        {
            Random rand = new Random();
            await Task.Delay(rand.Next(6000, 8001));
            stop = true;
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
                                                }
                                                sw.Close();
                                                Console.WriteLine("Name saved");
                                                Console.ReadLine();
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
                                                }
                                                sw.Close();
                                                Console.WriteLine("Surname saved");
                                                Console.ReadLine();
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
                                                }
                                                sw.Close();
                                                Console.WriteLine("Interest saved");
                                                Console.ReadLine();
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
