

namespace MDDReservationAPI.Profile
{
    public class SchoolProfile: AutoMapper.Profile
    {
        public SchoolProfile()
        {
            CreateMap< Models.School, DTO.SchoolCreationDTO > ();
        }
    }
}
