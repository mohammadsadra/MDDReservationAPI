namespace MDDReservationAPI.Profile
{
    public class RegistrationFormProfile : AutoMapper.Profile
    {
        public RegistrationFormProfile()
        {
            CreateMap<DTO.RegistrationFormCreationDTO, Models.RegistrationForm>();
        }
    }
}
