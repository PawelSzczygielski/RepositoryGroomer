using System;
using System.Linq;
using RepositoryGroomer.Core;

namespace RepositoryGroomer.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileFinder=  new ProjectFileFinder();
            var allProjects = fileFinder.GetAllProjects(@"C:\ElinGit\Powel.Elin");


            //foreach (var projectFile in allProjects)
            //{
            //    Console.WriteLine(projectFile.FilePath);
            //    foreach (var link in projectFile.Links)
            //    {
            //        Console.WriteLine(link.LinkedFileUnwrappedPath);
            //    }
            //}

            Console.WriteLine($"Total number of csproj: {allProjects.Count}");
            Console.WriteLine($"Total number of links: {allProjects.SelectMany(x=>x.Links).Count()}");
            allProjects.SelectMany(x => x.Links)
                .Where(l => !l.IsLinkValid)
                .ToList()
                .ForEach(x => Console.WriteLine(x.LinkedFileUnwrappedPath));

            Console.ReadLine();
        }
    }
}
