using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_2_Task3
{
    static class Program
    {
        static string KeyGen(ResearchTeam rt)
        {
            return rt.Name + "_Key";
        }

        static KeyValuePair<Team, ResearchTeam> ElemGen(int i)
        {
            ResearchTeam rt = new ResearchTeam($"Theme_{i}", $"Organization_{i}", i, TimeFrame.Year + i % 3);
            Team team = rt.TeamInfo;

            KeyValuePair<Team, ResearchTeam> kvp = new(team, rt);

            return kvp;
        }

        static void Main(string[] args)
        {
            // 1
            ResearchTeam team = new ResearchTeam();

            team.SortPapersByPublishDate();
            Console.WriteLine("Сортировка публикаций по дате выхода:");
            Console.WriteLine(team);
            Console.WriteLine();

            team.SortPapersByName();
            Console.WriteLine("Сортировка публицаций по названию:");
            Console.WriteLine(team);
            Console.WriteLine();

            team.SortPapersByAuthorSurname();
            Console.WriteLine("Сортировка публикаций по фамилии автора:");
            Console.WriteLine(team);
            Console.WriteLine();

            // 2
            ResearchTeamCollection<string> rtCollection = new ResearchTeamCollection<string>(KeyGen);
            rtCollection.AddDefaults();
            Console.WriteLine(rtCollection);
            Console.WriteLine();

            // 3
            Console.WriteLine($"Дата выхода последней публикации: {rtCollection.GetDateLastPublishedPaper}");
            Console.WriteLine();
            Console.WriteLine("Команды с продолжительностью исследования TwoYears:");
            Console.WriteLine(String.Join(", ",
                rtCollection.GetResearchTeamsWithTimeFrame(TimeFrame.TwoYears)
                    .Select(kvp => $"{kvp.Key}: {kvp.Value.ToShortString()}")));
            Console.WriteLine();
            Console.WriteLine("Группировка элементов коллекции по продолжительности исследований:");
            Console.WriteLine(String.Join("\n",
                rtCollection.GroupByTimeFrame.Select(
                    group => $"{group.Key}: [{String.Join(", ", group.Select(kvp => kvp.Value.ToShortString()))}]"
                )
            ));
            Console.WriteLine();

            // 4
            TestCollection<Team, ResearchTeam> testCollection;
            while (true)
            {
                try
                {
                    Console.Write("Введите количество элементов в TestCollection: ");
                    var i = UInt32.Parse(Console.ReadLine() ?? string.Empty);
                    testCollection = new((int)i, ElemGen);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Произошла ошибка, попробуйте ввести другое значение!");
                }
            }
            
            testCollection.TestWithContainsInLists();
            testCollection.TestWithContainsKeyInDict();
            testCollection.TestWithContainsValueInDict();
        }
    }
}