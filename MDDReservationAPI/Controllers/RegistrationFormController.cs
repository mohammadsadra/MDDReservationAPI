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
        
        [HttpPost]
        [Route("createFullForm")]
        [Produces("application/json")]
        public async Task<ActionResult<RegistrationForm>> CreateRegistrationFullFormAsync([FromBody] FullRegistrationFormDTO fullRegistrationFormDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            

            var manager = _mapper.Map<Manager>(fullRegistrationFormDto.managerCreationDto);
            var school = _mapper.Map<School>(fullRegistrationFormDto.schoolCreationDto);
            var schoolClass = _mapper.Map<SchoolClass>(fullRegistrationFormDto.schoolClassCreationDto);
            var eventDays = _mapper.Map<EventDays>(fullRegistrationFormDto.eventDaysCreationDto);
            var project = new Project();
            if (fullRegistrationFormDto.hasProject)
            {
                 project = _mapper.Map<Project>(fullRegistrationFormDto.projectCreationDto);
            }
            

            var createdManager = await _reservationRepository.AddManagerAsync(manager);
            school.ManagerId = createdManager.Id;
            var createdSchool = await _reservationRepository.AddSchoolAsync(school);
            schoolClass.SchoolId = createdSchool.Id;
            var createdClass = await _reservationRepository.AddSchoolClassAsync(schoolClass);
            project.SchoolId = createdSchool.Id;
            var createdProject = new Project();
            if (fullRegistrationFormDto.hasProject)
            {
                createdProject = await _reservationRepository.AddProjectAsync(project);
            }

            var createdEventDays = await _reservationRepository.AddEventDaysAsync(eventDays);

            RegistrationForm form;
            if (fullRegistrationFormDto.hasProject)
            {
                form = new RegistrationForm()
                {
                    ManagerId = createdManager.Id,
                    SchoolId = createdSchool.Id,
                    SchoolClassId = createdClass.Id,
                    EventDays = createdEventDays.Id,
                    ProjectId = createdProject.Id
                };
            }
            else
            {
                form =  new RegistrationForm()
                {
                    ManagerId = createdManager.Id,
                    SchoolId = createdSchool.Id,
                    SchoolClassId = createdClass.Id,
                    EventDays = createdEventDays.Id
                };
            }

            var createdForm = await _reservationRepository.AddRegistrationFormAsync(form);

            return Ok("Successfully created.");
        }
        
        #endregion
    }
}
