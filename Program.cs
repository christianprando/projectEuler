using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Microsoft.Extensions.CommandLineUtils;
using ProjectEuler.Problems;

namespace ProjectEuler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = RegisterComponents();
            var cmd = container.Resolve<CommandLineApplication>();
            var problems = cmd.Option("-p | --problems <value>", "Problems to run", CommandOptionType.MultipleValue);
            var executor = container.Resolve<IProblemExecutor>();

            cmd.OnExecute(() => executor.ExecuteProblems(problems.Value()));
            cmd.HelpOption("-? | -h | --help");
            cmd.Execute(args);
        }

        private static IWindsorContainer RegisterComponents()
        {
            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Register(Component.For<CommandLineApplication>().ImplementedBy<CommandLineApplication>());
            
            //Problems
            container.Register(Component.For<IEulerProblem, Problem62>());
            container.Register(Component.For<IEulerProblem, Problem63>());
            container.Register(Component.For<IEulerProblem, Problem64>());
            container.Register(Component.For<IEulerProblem, Problem65>());
            container.Register(Component.For<IEulerProblem, Problem66>());
            container.Register(Component.For<IEulerProblem, Problem67>());
            container.Register(Component.For<IEulerProblem, Problem68>());
            container.Register(Component.For<IEulerProblem, Problem69>());

            container.Register(Component.For<IProblemExecutor, ProblemExecutor>());
            return container;
        }
    }
}
