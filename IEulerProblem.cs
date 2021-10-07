namespace ProjectEuler
{
    interface IEulerProblem
    {
        int ProblemNumber { get; }
        string Run();
    }
}