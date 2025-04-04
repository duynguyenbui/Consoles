namespace Consoles.Events
{
    class NotificationService
    {
        public void Congratulate(object sender, GradeEventArgs e)
        {
            Student student = sender as Student;
            Console.WriteLine($"Chuc mung {student?.Name}! Ban da duoc {e.Grade} diem mon {e.Subject}!");
        }

        public void TimeOff(object sender, GradeEventArgs e)
        {
            Console.WriteLine("Take time off due to high score");
        }

        public void TakeResignation(object sender, EventArgs e)
        {
            Console.WriteLine("Take resignation");
        }
    }
}
