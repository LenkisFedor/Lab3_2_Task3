using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_2_Task3
{
    public delegate TKey KeySelector<TKey>(ResearchTeam rt);

    public class ResearchTeamCollection<TKey>
    {
        #region Fields
        private Dictionary<TKey, ResearchTeam> ResearchTeamDict = new();

        private KeySelector<TKey> KeyGenerator;
        #endregion

        #region Constructors
        public ResearchTeamCollection(KeySelector<TKey> keyGenerator)
        {
            KeyGenerator = keyGenerator;
        }
        #endregion

        #region Methods
        public void AddDefaults()
        {
            Random rnd = new();

            for (var i = 0; i < rnd.Next(3, 6); i++) AddResearchTeams(new ResearchTeam());

        }

        public void AddResearchTeams(params ResearchTeam[] researchTeams)
        {
            foreach (var team in researchTeams)
            {
                var key = KeyGenerator(team);
                ResearchTeamDict.Add(key, team);
            }
        }

        public override string ToString()
        {
            return "ResearchTeamCollection[" +
                   String.Join(", ", ResearchTeamDict.Select(kvp => $"{kvp.Key}: {kvp.Value}")) +
                   "]";
        }

        public string ToShortString()
        {
            return "ResearchTeamCollection[" +
                   String.Join(", ", ResearchTeamDict.Select(kvp => $"{kvp.Key}: {kvp.Value.ToShortString()}")) +
                   "]";
        }
        #endregion

        public DateTime GetDateLastPublishedPaper 
            => ResearchTeamDict
                ?.SelectMany(kvp => kvp.Value.PapersList)
                .Max(paper => paper.PublishDate) ?? DateTime.Now;

        public IEnumerable<KeyValuePair<TKey, ResearchTeam>> GetResearchTeamsWithTimeFrame(TimeFrame frame)
            => ResearchTeamDict
                .Where(kvp => kvp.Value.ResearchTimeFrame == frame);

        public IEnumerable<IGrouping<TimeFrame, KeyValuePair<TKey, ResearchTeam>>> GroupByTimeFrame
            => ResearchTeamDict.GroupBy(kvp => kvp.Value.ResearchTimeFrame);
    }
}