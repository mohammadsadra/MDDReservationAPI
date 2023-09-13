using AutoMapper;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Models;
using MDDReservationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MDDReservationAPI.Controllers
{
    [Route("registrationForm")]
    [ApiController]
    public class RegistrationFormController : ControllerBase
    {
        private readonly ILogger<RegistrationFormController> _logger;
        private readonly IMapper _mapper;
        private readonly IMDDReservationRepository _reservationRepository;

        public RegistrationFormController(ILogger<RegistrationFormController> logger, IMapper mapper, IMDDReservationRepository mddReservationRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _reservationRepository = mddReservationRepository ?? throw new ArgumentNullException(nameof(mddReservationRepository));
        }

        #region POST
        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        public async Task<ActionResult<RegistrationForm>> CreateRegistrationFormAsync([FromBody] RegistrationFormCreationDTO registrationFormCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdRegistrationForm = _mapper.Map<RegistrationForm>(registrationFormCreationDto);

            var result = await _reservationRepository.AddRegistrationFormAsync(createdRegistrationForm);

            return Ok(result);
        }
        #endregion
    }
}
