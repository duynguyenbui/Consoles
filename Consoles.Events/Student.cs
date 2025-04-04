namespace Consoles.Events
{
    class Student(string name)
    {
        public string Name { get; } = name;

        public event EventHandler<GradeEventArgs> OnHighScore;

        public event EventHandler OnResignation;

        public void TakeExam(string subject, double grade)
        {
            Console.WriteLine($"{Name} lam bai thi mon {subject}, duoc {grade}");

            if (grade >= 9)
            {
                OnHighScore?.Invoke(this, new GradeEventArgs(subject, grade));
            }
        }

        public void TakeResignation()
        {
            OnResignation?.Invoke(this, EventArgs.Empty);
        }
    }
}
