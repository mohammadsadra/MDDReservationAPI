namespace MDDReservationAPI.Profile
{
    public class ProjectProfile : AutoMapper.Profile
    {
        public ProjectProfile()
        {
            CreateMap<DTO.ProjectCreationDTO, Models.Project>();
        }
    }
}
