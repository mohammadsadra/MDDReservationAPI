namespace MDDReservationAPI.Profile
{
    public class StudentProfile : AutoMapper.Profile
    {
        public StudentProfile()
        {
            CreateMap<DTO.StudentCreationDTO, Models.Student>();
        }
    }
}
