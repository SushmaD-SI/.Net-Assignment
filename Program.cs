using System.Text.RegularExpressions;

namespace MatchDetailsManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int choice = 0;
            MatchManagement matchManagement = new MatchManagement();
         
               
            bool exit= false;   
            do
            {
                Console.WriteLine("---------MENU---------");
                Console.WriteLine("1.Add Matches");
                Console.WriteLine("2.Display Matches");
                Console.WriteLine("3.Display Matches with Id");
                Console.WriteLine("4.Update the scores");
                Console.WriteLine("5.Remove a Match");
                Console.WriteLine("6.Sort Matches");
                Console.WriteLine("7.Filter Matches");
                Console.WriteLine("8.Display Statistics");
                Console.WriteLine("9.Search Matches");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter Match Details:");
                        Console.WriteLine("Enter Match Id:");
                        int matchid = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Sport:");
                        string sport = Console.ReadLine();
                        Console.WriteLine("Enter MatchDateTime:");
                        DateTime matchdatetime = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Match Location:");
                        string location = Console.ReadLine();
                        Console.WriteLine("Enter Home Teams:");
                        string hometeam = Console.ReadLine();
                        Console.WriteLine("Enter Away Team:");
                        string awayteam = Console.ReadLine();
                        Console.WriteLine("Enter Home Team Score:");
                        int hometeamscore = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Away Team Score:");
                        int awayteamscore = int.Parse(Console.ReadLine());
                        MatchDetails newMatch = new MatchDetails(matchid, sport, matchdatetime, location, hometeam, awayteam, hometeamscore, awayteamscore);
                        matchManagement.AddMatchDetails(newMatch);
                        break;
                    case 2:
                        Console.WriteLine("Match Details : ");
                        matchManagement.DisplayMatches();
                        break;
                    case 3:
                       
                        matchManagement.DisplayMatcheswithId();
                        break;
                    case 4:       
                        matchManagement.updateScores();
                        break;
                    case 5:
                        matchManagement.Removematch();
                        break;
                    case 6:
                        matchManagement.SortMatches();
                        matchManagement.DisplayMatches();
                        break;
                    case 7:
                        List<MatchDetails> filteredMatches = matchManagement.FilterMatches();
                        Console.WriteLine("\nFiltered Matches:");
                        foreach (var match in filteredMatches)
                        {
                            Console.WriteLine($"Match ID: {match.MatchId}, Sport: {match.Sport}, Date: {match.DateTime}, Location: {match.Location}, Teams: {match.HomeTeam} vs {match.AwayTeam}, Scores: {match.HomeTeamScore} - {match.AwayTeamScore}");
                        }
                        break;
                    case 8:
                        Console.WriteLine("Calculate statistics for: (averagescore/highestscore/lowestscore)");
                        string statisticsCriteria = Console.ReadLine();
                        matchManagement.CalculateStatistics(statisticsCriteria);
                        break;
                       
                    case 9:
                        Console.WriteLine("Search for: ");
                        string keyword = Console.ReadLine();
                        List<MatchDetails> searchedMatches = matchManagement.SearchMatches(keyword);
                        Console.WriteLine("\nSearched Matches:");
                        foreach (var match in searchedMatches)
                        {
                            Console.WriteLine($"Match ID: {match.MatchId}, Sport: {match.Sport}, Date: {match.DateTime}, Location: {match.Location}, Teams: {match.HomeTeam} vs {match.AwayTeam}, Scores: {match.HomeTeamScore} - {match.AwayTeamScore}");
                        }
                        break;
                    case 10:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Wrong Choice!!!");
                        break;
                }

             }
            while (!exit);
        }
    }
}