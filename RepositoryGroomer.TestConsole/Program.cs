using System;
using System.Linq;
using RepositoryGroomer.Core;

namespace RepositoryGroomer.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var fileFinder = new ProjectFileFinder();
            var allProjects = fileFinder.GetAllProjects(@"C:\Projects\Powel.Elin");


            Console.WriteLine($"Total number of csproj: {allProjects.Count}");
            Console.WriteLine($"Total number of links: {allProjects.SelectMany(x => x.Links).Count()}");
            allProjects.SelectMany(x => x.Links)
                .Where(l => !l.TargetLinkedFileExists)
                .ToList()
                .ForEach(x => Console.WriteLine(x.LinkedFileUnwrappedPath));

            Console.ReadLine();
        }
    }
}
