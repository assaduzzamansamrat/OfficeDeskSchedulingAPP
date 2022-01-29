using Services.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataService
{
    public class DeskDataService
    {
        private AppDbContext context;

        public DeskDataService(AppDbContext _context)
        {
            context = _context;
        }
        public List<Desk> GetAll()
        {
            try
            {

                return context.Desks.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool CreateNewDesk(Desk desk)
        {
         
            try
            {
                desk.CreatedDate = DateTime.Now;
                context.Desks.Add(desk);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Desk GetDeskByID(long id)
        {
            try
            {
                return context.Desks.FirstOrDefault(d => d.Id == id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool UpdateDesk(Desk _desk)
        {
            try
            {
                if (_desk != null)

                {
                    Desk desk = context.Desks.FirstOrDefault(d => d.Id == _desk.Id);
                    if (desk != null)
                    {
                        desk.EquipmentDetails = _desk.EquipmentDetails;
                        desk.EditedDate = DateTime.Now;
                        desk.DeskNumber = _desk.DeskNumber;
                        desk.DeskType = _desk.DeskType;
                        desk.NumberOfSeats = _desk.NumberOfSeats;


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
