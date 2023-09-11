using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace MatchDetailsManagementSystem
{
    internal class MatchDetails
    {
        public int MatchId { get; set; }
        public string Sport { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }


        public MatchDetails(int matchId, string sport, DateTime datetime, string location, string hometeam, string awayteam, int hometeamscore, int awayteamscore)
        {
            MatchId       = matchId;
            Sport         = sport;
            DateTime      = datetime;
            Location      = location;
            HomeTeam      = hometeam;
            AwayTeam      = awayteam;
            HomeTeamScore = hometeamscore;
            AwayTeamScore = awayteamscore;
        }
        public override string ToString()
        {
            return $"MatchId: {MatchId}, Sports: {Sport}, MatchDateTime: {DateTime}, Location: {Location}, HomeTeam: {HomeTeam}, HomeTeamScore: {HomeTeamScore}, AwayTeam: {AwayTeam}, AwayTeamScore: {AwayTeamScore}";
        }
    }
}
