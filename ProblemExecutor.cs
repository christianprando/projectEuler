using System;
using System.Collections.Generic;
using System.Linq;

namespace EulerProject
{
    internal class ProblemExecutor : IProblemExecutor
    {
        private readonly IEnumerable<IEulerProblem> _availableProblems;

        public ProblemExecutor(IEnumerable<IEulerProblem> availableProblems)
        {
            _availableProblems = availableProblems;
        }

        public int ExecuteProblems(string problems)
        {
            Console.WriteLine("These are the problems passed as arguments:");
            Console.WriteLine(problems);

            var problemsToExecute = new List<IEulerProblem>();

            Console.WriteLine("Validating problems...");
            foreach (var problem in problems.Split(','))
            { 
                var problemNumber = problem.Trim();
                if(!int.TryParse(problemNumber, out var problemInt))
                {
                    Console.WriteLine($"Invalid problem number: {problemNumber} | SKIP");
                    continue;
                }

                if (_availableProblems.All(x => x.ProblemNumber != problemInt))
                {
                    Console.WriteLine($"Unavailable problem number: {problemNumber} | SKIP");
                    continue;
                }

                Console.WriteLine($"Available problem number: {problemNumber} | ADDED");
                problemsToExecute.Add(_availableProblems.FirstOrDefault(x => x.ProblemNumber == problemInt));
            }

            Console.WriteLine($"Running problems...");

            foreach (var problem in problemsToExecute)
            {
                try
                {
                    Console.WriteLine($"Starting problem #{problem.ProblemNumber}...");
                    var result = problem.Run();
                    Console.WriteLine($"Finished problem #{problem.ProblemNumber}! Result: {result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Could not finish problem #{problem.ProblemNumber}! Error: {e}");
                }
            }

            return 0;
        }
    }
}