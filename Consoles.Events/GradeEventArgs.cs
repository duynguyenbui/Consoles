namespace Consoles.Events
{
    class GradeEventArgs : EventArgs
    {
        public string Subject { get; }
        public double Grade { get; }

        public GradeEventArgs(string subject, double grade)
        {
            Subject = subject;
            Grade = grade;
        }
    }
}
