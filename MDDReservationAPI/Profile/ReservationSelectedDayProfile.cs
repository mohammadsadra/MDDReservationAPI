namespace MDDReservationAPI.Profile
{
    public class ReservationSelectedDayProfile : AutoMapper.Profile
    {
        public ReservationSelectedDayProfile()
        {
            CreateMap<DTO.ReservationSelectedDaysDTO, Models.ReservationSelectedDay>();
        }
    }
}