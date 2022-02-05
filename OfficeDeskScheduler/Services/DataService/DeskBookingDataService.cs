using Services.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataService
{
    public class DeskBookingDataService
    {
        private AppDbContext context;

        public DeskBookingDataService(AppDbContext _context)
        {
            context = _context;
        }
        public List<DeskBooking> GetAll(long managerId)
        {
            try
            {

                return context.DeskBookings.Where(x=>x.BookedBy == managerId).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeskBookingDelete(long Id)
        {
            try
            {
                DeskBooking deskBookng = new DeskBooking();
                deskBookng = context.DeskBookings.Where(x => x.Id == Id).FirstOrDefault();
                if(deskBookng != null)
                {
                    context.DeskBookings.Remove(deskBookng);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool InviteContributors(TeamAndContributorMapper teamAndContributorMapper)
        {
            try
            {
                TeamAndContributorMapper TACM = context.TeamAndContributorMappers.Where(x=> x.Id == teamAndContributorMapper.Id).FirstOrDefault();

                if(TACM != null)
                {
                    TACM.ContributorId = teamAndContributorMapper.ContributorId;
                    TACM.ContributorName = teamAndContributorMapper.ContributorName;
                    TACM.ManagerId = teamAndContributorMapper.ManagerId;
                    TACM.ManagerName = teamAndContributorMapper.ManagerName;
                    TACM.IsInvaitationAccept = teamAndContributorMapper.IsInvaitationAccept;
                    TACM.ChoosedDeskId = teamAndContributorMapper.ChoosedDeskId;
                    TACM.TeamId = teamAndContributorMapper.TeamId;
                    context.SaveChanges();

                }

              
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int GetContributorAndTeamdataByTeamIdAndContributorId(TeamAndContributorMapper teamAndContributorMapper)
        {
            try
            {
                int count = 0;
                count = context.TeamAndContributorMappers.Where(x => x.TeamId == teamAndContributorMapper.TeamId && x.ContributorId == teamAndContributorMapper.ContributorId).Count();

                return count;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int GetDeskBookingCountByTeamId(long teamId)
        {
            try
            {
                int count = 0;
                count = context.DeskBookings.Where(x => x.TeamId == teamId).Count();
                return count;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetContributorCountinMapperTableByTeamId(long teamId)
        {
            try
            {
                int count = 0;
                count = context.TeamAndContributorMappers.Where(x => x.TeamId == teamId).Count();

                return count;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public List<TeamAndContributorMapper> GetAllInvitationsOfContributorBycontributorId(long Id)
        {
            try
            {
               return context.TeamAndContributorMappers.Where(x => x.ContributorId == Id).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool AcceptOrRejectInvitaions(bool isAccept,long Id)
        {
            TeamAndContributorMapper teamAndContributorMapper = new TeamAndContributorMapper();
            teamAndContributorMapper = context.TeamAndContributorMappers.Where(x => x.Id == Id).FirstOrDefault();
            if(teamAndContributorMapper != null)
            {
                teamAndContributorMapper.IsInvaitationAccept = isAccept;
                context.SaveChanges();
                return true;
            }
            return false;
        }


        public bool CreateNewDeskBooking(DeskBooking deskBooking)
        {
         
            try
            {
              
                context.DeskBookings.Add(deskBooking);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DeskBooking GetDeskByID(long id)
        {
            try
            {
                return context.DeskBookings.FirstOrDefault(d => d.Id == id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool UpdateDesk(DeskBooking _desk)
        {
            try
            {
                if (_desk != null)

                {
                    DeskBooking deskBooking = context.DeskBookings.FirstOrDefault(d => d.Id == _desk.Id);
                    if (deskBooking != null)
                    {
                                          
                        deskBooking.StartDateTime = _desk.StartDateTime;
                        deskBooking.EndDateTime = _desk.EndDateTime; 
                        deskBooking.AssignedContributor = _desk.AssignedContributor;
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
                Desk desk = context.Desks.FirstOrDefault(d => d.Id == id);
                if (desk != null)
                {
                    context.Desks.Remove(desk);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }

    }
}
