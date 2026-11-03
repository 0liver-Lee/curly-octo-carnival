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

            for (i =  0; i < students.Length; i++)
            {
                Console.WriteLine($"{students[i].name.PadRight(15)}{students[i].surname.PadRight(25)}{students[i].interest.PadRight(25)}{students[i].score}");
            }
            Console.ReadLine();
        }
    }
}
