using AutoMapper;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MDDReservationAPI.Models;


[Route("school")]
[ApiController]
public class SchoolController : ControllerBase
{
    private readonly ILogger<SchoolController> _logger;
    private readonly IMapper _mapper;
    private readonly IMDDReservationRepository _reservationRepository;

    public SchoolController(ILogger<SchoolController> logger, IMapper mapper, IMDDReservationRepository mddReservationRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _reservationRepository = mddReservationRepository ?? throw new ArgumentNullException(nameof(mddReservationRepository));
    }


    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult<School>> CreateSchoolAsync([FromBody] SchoolCreationDTO schoolDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var createdSchool = new School()
        {
            Name = schoolDTO.Name,
            CreatedAt = DateTime.UtcNow,
            Gender = schoolDTO.Gender,
            SchoolType = schoolDTO.SchoolType,
        };

        await _reservationRepository.AddSchoolAsync(createdSchool);
        await _reservationRepository.SaveChangesAsync();

        
        return CreatedAtRoute("", createdSchool);





        return Ok() ;
    }
}