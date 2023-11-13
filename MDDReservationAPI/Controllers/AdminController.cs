using AutoMapper;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Models;
using MDDReservationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MDDReservationAPI.Controllers
{
    [Route("admin")]
    [ApiController]
    public class AdminController: ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IMapper _mapper;
        private readonly IMDDReservationRepository _reservationRepository;

        public AdminController(ILogger<AdminController> logger, IMapper mapper, IMDDReservationRepository mddReservationRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _reservationRepository = mddReservationRepository ?? throw new ArgumentNullException(nameof(mddReservationRepository));
        }

        #region POST

        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        public async Task<ActionResult<School>> CreateAdminAsync([FromBody] AdminCreationDTO adminCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdAdmin = _mapper.Map<Admin>(adminCreationDto);
            var result = await _reservationRepository.AddAdminAsync(createdAdmin);
            _logger.LogError(result.ToString());

            return Ok(result);
        }

        #endregion

    }
}
