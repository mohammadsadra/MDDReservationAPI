using AutoMapper;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Models;
using MDDReservationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MDDReservationAPI.Controllers
{
    [Route("schoolClass")]
    [ApiController]
    public class SchoolClassController : ControllerBase
    {
        private readonly ILogger<SchoolClassController> _logger;
        private readonly IMapper _mapper;
        private readonly IMDDReservationRepository _reservationRepository;

        public SchoolClassController(ILogger<SchoolClassController> logger, IMapper mapper, IMDDReservationRepository mddReservationRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _reservationRepository = mddReservationRepository ?? throw new ArgumentNullException(nameof(mddReservationRepository));
        }

        #region POST
        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        public async Task<ActionResult<SchoolClass>> CreateSchoolClassAsync([FromBody] SchoolClassCreationDTO schoolClassCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdSchoolClass = _mapper.Map<SchoolClass>(schoolClassCreationDto);

            if (!await _reservationRepository.SchoolExistsAsync(createdSchoolClass.SchoolId))
            {
                return NotFound("School Id is wrong.");
            }

           
            var result = await _reservationRepository.AddSchoolClassAsync(createdSchoolClass);


            return Ok(result);
        }
        #endregion

        #region PUT

        
        #endregion
    }
}
