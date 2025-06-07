using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuscany.DataAccess.Repository.IRepository;
using Tuscany.Models;
using Tuscany.WebModels;

namespace Tuscany.Utility
{
    public class ConvertUtility
    {
        private readonly IUnitOfWork _unitOfWork;
        public ConvertUtility(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TourCard> ConvertTourCards(IEnumerable<Tour> tours)
        {
            List<TourCard> cards = [];
            foreach (var tour in tours)
            {
                ToursSchedule schedule = _unitOfWork.ToursSchedule.Get(x => x.TourId == tour.Id)!;
                ToursScheduleType? scheduleType = null;
                if (schedule is not null)
                {
                    scheduleType = _unitOfWork.ToursScheduleType.Get(x => x.Id == schedule.ScheduleTypeId);
                }
                string? imgUrl = _unitOfWork.Gallery.Get(x => x.TourId == tour.Id)?.Img;
                cards.Add(new TourCard()
                {
                    Id = tour.Id,
                    Name = tour.Name,
                    Price = tour.AdultPrice,
                    Img = imgUrl,
                    ScheduleType = scheduleType?.ScheduleType,
                    MinNumberOfGroup = tour.MinNumberOfGroup,
                    MaxNumberOfGroup = tour.MaxNumberOfGroup,
                    Description = tour.Description,
                });
            }
            return cards;
        }
    }
}
