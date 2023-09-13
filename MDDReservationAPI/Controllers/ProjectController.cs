using AutoMapper;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Models;
using MDDReservationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MDDReservationAPI.Controllers
{
    [Route("project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly IMapper _mapper;
        private readonly IMDDReservationRepository _reservationRepository;

        public ProjectController(ILogger<ProjectController> logger, IMapper mapper, IMDDReservationRepository mddReservationRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _reservationRepository = mddReservationRepository ?? throw new ArgumentNullException(nameof(mddReservationRepository));
        }

        #region POST
        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        public async Task<ActionResult<School>> CreateProjectAsync([FromBody] ProjectCreationDTO projectCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdProject = _mapper.Map<Project>(projectCreationDto);

            var result = await _reservationRepository.AddProjectAsync(createdProject);

            return Ok(result);
        }
        #endregion
    }
}
