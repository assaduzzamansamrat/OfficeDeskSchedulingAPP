using Services.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataService
{
    public class TeamDataService
    {
        private AppDbContext context;

        public TeamDataService(AppDbContext _context)
        {
            context = _context;
        }
        public List<Team> GetAll()
        {
            try
            {

                return context.Teams.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<Team> GetAllByManagerId(long MaagerId)
        {
            try
            {

                return context.Teams.Where(x=>x.ManagerId == MaagerId).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool CreateNewTeam(Team team)
        {
         
            try
            {
                team.CreatedDate = DateTime.Now;
                context.Teams.Add(team);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Team GetTeamByID(long id)
        {
            try
            {
                return context.Teams.FirstOrDefault(d => d.Id == id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool UpdateTeam(Team _team)
        {
            try
            {
                if (_team != null)

                {
                    Team team = context.Teams.FirstOrDefault(d => d.Id == _team.Id);
                    if (team != null)
                    {
                        team.EditedDate = DateTime.Now;
                        team.TeamName = _team.TeamName;
                        team.ManagerId = _team.ManagerId;
                      
                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool Delete(long id)
        {
            if (id > 0)
            {
                Team team = context.Teams.FirstOrDefault(d => d.Id == id);
                if (team != null)
                {
                    context.Teams.Remove(team);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }

    }
}
