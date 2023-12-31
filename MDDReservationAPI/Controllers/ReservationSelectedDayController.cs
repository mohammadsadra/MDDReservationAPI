using AutoMapper;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Models;
using MDDReservationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MDDReservationAPI.Controllers;

public class ReservationSelectedDayController: ControllerBase
{
    private readonly ILogger<RegistrationFormController> _logger;
    private readonly IMapper _mapper;
    private readonly IMDDReservationRepository _reservationRepository;

    public ReservationSelectedDayController(ILogger<RegistrationFormController> logger, IMapper mapper, IMDDReservationRepository mddReservationRepository)
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
    
    #region PUT
    
    [HttpPut]
    [Route("updateSelectedDay")]
    [Produces("application/json")]
    public async Task<ActionResult<String>> UpdateSelectedDayAsync(int id, int selectedDay)
    {
        if (selectedDay > 2 || selectedDay < 0) 
        {
            return BadRequest("SelectedDay number is wrong!");
        }
        var result = await _reservationRepository.ChangeReservationSelectedDayId(id, selectedDay);
        if (result)
        {
            return Ok("SelectedDay updated!");
        }
        else
        {
            return BadRequest("Something went wrong!");
        }
    }

    #endregion
    // [HttpPost]
    // [Route("getAllSelectedDays")]
    // [Produces("application/json")]
    // public async Task<ActionResult<RegistrationForm>> GetMonthSelectedDays(int year, int month)
    // {
    //     var result = await _reservationRepository.GetMonthSelectedDays(year, month);
    //
    //     return Ok(result);
    // }
}