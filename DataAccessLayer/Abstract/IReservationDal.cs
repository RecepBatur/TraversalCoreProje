using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IReservationDal : IGenericDal<Reservation>
    {
        List<Reservation> GetListWithReservationByWaitApproval(int id);//onay bekleyen rezvr.
        List<Reservation> GetListWithReservationByAccepted(int id);//kabul edilen rezvr.
        List<Reservation> GetListWithReservationByPrevious(int id);//geçmiş rezvr.
    }
}
