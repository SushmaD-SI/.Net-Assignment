using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MatchDetailsManagementSystem
{
    internal class MatchManagement
    {

        List<MatchDetails> matchDetails = new List<MatchDetails>();

        public void AddMatchDetails(MatchDetails matchdetail)
        {

            //  MatchDetails matchdetail = new MatchDetails(MatchId, Sport, Location, HomeTeam, AwayTeam, DateTime);
            if (IsValidMatch(matchdetail) == true)
            {
                matchDetails.Add(matchdetail);
                Console.WriteLine("Matches added successfully");
            }
            else
            {
                Console.WriteLine("Enter correct value");
            }
        }

        public void DisplayMatches()
        {
            if (matchDetails.Count != null)


                foreach (var matchdetail in matchDetails)
                {
                   
                    Console.WriteLine(matchdetail);
                    {
                        Console.WriteLine("Matches not present");
                    }

                }


        }
            public void DisplayMatcheswithId()
            {

            Console.WriteLine("Enter the id of Match : ");
            int Id = int.Parse(Console.ReadLine());
            MatchDetails matchdetail = matchDetails.FirstOrDefault(a => a.MatchId == Id);

                if (matchdetail != null)
                {
                    Console.WriteLine(matchdetail);
                }
                else
                {
                    Console.WriteLine("Matches not present");
                }
            }



            public void updateScores()
            {
                  int id = Convert.ToInt32(Console.ReadLine());
                 Console.WriteLine("Enter the Match Id to Update");

                MatchDetails matchdetail = matchDetails.FirstOrDefault(a => a.MatchId == id);
                  

                if (matchDetails != null)
                {


                    Console.WriteLine("Enter the new score of Home Team");
                    int newHomeTeamScore = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter the new score of Away Team");
                    int newAwayTeamScore = Convert.ToInt32(Console.ReadLine());

                    matchdetail.HomeTeamScore = newHomeTeamScore;
                    matchdetail.AwayTeamScore = newAwayTeamScore;
                    Console.WriteLine("Match details are added successfully!");
                }
                else
                {
                    Console.WriteLine("Match not found");
                }
            }

            public void Removematch()
            {
            Console.WriteLine("Enter the match id");
            int id = Convert.ToInt32(Console.ReadLine());
            MatchDetails matchdetail = matchDetails.FirstOrDefault(a => a.MatchId == id);
               
                if (matchDetails != null)
                {

                    matchDetails.Remove(matchdetail);
                    Console.WriteLine("Details removed successfully");
                }
            else {
                Console.WriteLine("Details not found");
            }
            }

            public void SortMatches()
            {
                Console.WriteLine("Sort by: (date/sport/location)");
                string criteria = Console.ReadLine();
                Console.WriteLine("Ascending? (true/false)");
                bool ascending = bool.Parse(Console.ReadLine());
                switch (criteria.ToLower())
                {
                    case "date":
                        matchDetails = ascending ? matchDetails.OrderBy(m => m.DateTime).ToList() : matchDetails.OrderByDescending(m => m.DateTime).ToList();
                        break;
                    case "sport":
                        matchDetails = ascending ? matchDetails.OrderBy(m => m.Sport).ToList() : matchDetails.OrderByDescending(m => m.Sport).ToList();
                        break;
                    case "location":
                        matchDetails = ascending ? matchDetails.OrderBy(m => m.Location).ToList() : matchDetails.OrderByDescending(m => m.Location).ToList();
                        break;
                    default:
                        Console.WriteLine("Invalid sorting criteria.");
                        break;
                }
            }
            public List<MatchDetails> FilterMatches()
            {
                Console.WriteLine("Filter by: (sport/location/daterange)");
                string criteria = Console.ReadLine();
                Console.WriteLine("Enter value: ");
                string value = Console.ReadLine();
                switch (criteria.ToLower())
                {
                    case "sport":
                        return matchDetails.Where(m => m.Sport.Equals(value, StringComparison.OrdinalIgnoreCase)).ToList();
                    case "location":
                        return matchDetails.Where(m => m.Location.Equals(value, StringComparison.OrdinalIgnoreCase)).ToList();
                    case "daterange":
                        DateTime startDate;
                        DateTime endDate;
                        if (DateTime.TryParse(value.Split('-')[0], out startDate) && DateTime.TryParse(value.Split('-')[1], out endDate))
                        {
                            return matchDetails.Where(m => m.DateTime >= startDate && m.DateTime <= endDate).ToList();
                        }
                        else
                        {
                            Console.WriteLine("Invalid date range format. Use format 'yyyy-mm-dd - yyyy-mm-dd'.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid filtering criteria.");
                        break;
                }

                return new List<MatchDetails>();
            }

            public void CalculateStatistics(string criteria)
            {
                switch (criteria.ToLower())
                {
                    case "averagescore":
                        double homeAvg = matchDetails.Average(m => m.HomeTeamScore);
                        double awayAvg = matchDetails.Average(m => m.AwayTeamScore);
                        Console.WriteLine($"Average Score - Home: {homeAvg}, Away: {awayAvg}");
                        break;
                    case "highestscore":
                        int highestHomeScore = matchDetails.Max(m => m.HomeTeamScore);
                        int highestAwayScore = matchDetails.Max(m => m.AwayTeamScore);
                        Console.WriteLine($"Highest Score - Home: {highestHomeScore}, Away: {highestAwayScore}");
                        break;
                    case "lowestscore":
                        int lowestHomeScore = matchDetails.Min(m => m.HomeTeamScore);
                        int lowestAwayScore = matchDetails.Min(m => m.AwayTeamScore);
                        Console.WriteLine($"Lowest Score - Home: {lowestHomeScore}, Away: {lowestAwayScore}");
                        break;
                    default:
                        Console.WriteLine("Invalid statistics criteria.");
                        break;
                }
            }

            public List<MatchDetails> SearchMatches(string keyword)
            {
                return matchDetails.Where(m =>
                    m.Sport.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    m.HomeTeam.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    m.AwayTeam.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    m.Location.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            private bool IsValidMatch(MatchDetails match)
            {
                if (match.MatchId <= 0 || matchDetails.Any(m => m.MatchId == match.MatchId))
                    return false;

                if (string.IsNullOrWhiteSpace(match.Sport))
                    return false;

                if (match.DateTime <= DateTime.Now)
                    return false;

                if (string.IsNullOrWhiteSpace(match.Location))
                    return false;

                if (string.IsNullOrWhiteSpace(match.HomeTeam) || string.IsNullOrWhiteSpace(match.AwayTeam) || match.HomeTeam == match.AwayTeam)
                    return false;

                if (match.HomeTeamScore < 0 || match.AwayTeamScore < 0)
                    return false;

                return true;
            }

        }
    
}
