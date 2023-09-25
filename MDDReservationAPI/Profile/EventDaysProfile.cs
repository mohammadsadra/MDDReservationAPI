namespace MDDReservationAPI.Profile
{
    public class EventDaysProfile: AutoMapper.Profile
    {
        public EventDaysProfile()
        {
            CreateMap<DTO.EventDaysCreationDTO, Models.EventDays>();
        }
    }
}