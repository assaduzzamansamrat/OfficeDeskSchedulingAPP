﻿using Services.EntityModels;
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

        public bool InviteContributors(TeamAndContributorMapper teamAndContributorMapper)
        {
            try
            {
                context.TeamAndContributorMappers.Add(teamAndContributorMapper);
                context.SaveChanges();
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
                        deskBooking.DeskId = _desk.DeskId;
                        deskBooking.Location = _desk.Location;
                        deskBooking.Map = _desk.Map;
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
