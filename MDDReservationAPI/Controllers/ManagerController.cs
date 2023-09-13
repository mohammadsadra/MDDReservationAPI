using AutoMapper;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Models;
using MDDReservationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MDDReservationAPI.Controllers
{
    [Route("manager")]
    [ApiController]
    public class ManagerController: ControllerBase
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly IMapper _mapper;
        private readonly IMDDReservationRepository _reservationRepository;

        public ManagerController(ILogger<ManagerController> logger, IMapper mapper, IMDDReservationRepository mddReservationRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _reservationRepository = mddReservationRepository ?? throw new ArgumentNullException(nameof(mddReservationRepository));
        }

        #region POST
        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        public async Task<ActionResult<School>> CreateManagerAsync([FromBody] ManagerCreationDTO managerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdManager = _mapper.Map<Manager>(managerDto);

            var result = await _reservationRepository.AddManagerAsync(createdManager);

            return Ok(result);
        }
        #endregion
    }
}
