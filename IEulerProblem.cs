namespace EulerProject
{
    interface IEulerProblem
    {
        int ProblemNumber { get; }
        string Run();
    }
}