namespace ChgCharityJamPrototype.Models
{
    public class TeamModel
    {
        public List<Team> _teamList;

        public TeamModel()
        {
            _teamList = new List<Team>();
        }

        public void AddTeamMembers(IEnumerable<Team> teams)
        {
            _teamList.AddRange(teams);
        }

        public void DeleteTeams(List<string> teamNamesToDelete)
        {
            _teamList.RemoveAll(member => teamNamesToDelete.Contains(member.Name));
        }
    }
}
