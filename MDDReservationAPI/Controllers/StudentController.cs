using AutoMapper;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Models;
using MDDReservationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MDDReservationAPI.Controllers
{
    [Route("student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly IMDDReservationRepository _reservationRepository;

        public StudentController(ILogger<StudentController> logger, IMapper mapper, IMDDReservationRepository mddReservationRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _reservationRepository = mddReservationRepository ?? throw new ArgumentNullException(nameof(mddReservationRepository));
        }

        #region POST

        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        public async Task<ActionResult<Student>> CreateStudentAsync([FromBody] StudentCreationDTO studentCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdStudent = _mapper.Map<Student>(studentCreationDto);

            var result = await _reservationRepository.AddStudentAsync(createdStudent);

            return Ok(result);
        }
        #endregion
    }
}
