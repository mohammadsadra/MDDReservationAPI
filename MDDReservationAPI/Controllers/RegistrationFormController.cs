using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using AutoMapper;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Models;
using MDDReservationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MDDReservationAPI.Enums;
using MDDReservationAPI.Services;

namespace MDDReservationAPI.Controllers
{
    [Route("registrationForm")]
    [ApiController]
    public class RegistrationFormController : ControllerBase
    {
        private readonly ILogger<RegistrationFormController> _logger;
        private readonly IMapper _mapper;
        private readonly IMDDReservationRepository _reservationRepository;
        private readonly IMailService _mailService;

        public RegistrationFormController(ILogger<RegistrationFormController> logger, IMapper mapper,
            IMDDReservationRepository mddReservationRepository, IMailService localMailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _reservationRepository = mddReservationRepository ??
                                     throw new ArgumentNullException(nameof(mddReservationRepository));
            _mailService = localMailService ?? throw new ArgumentException(nameof(localMailService));
        }

        #region POST

        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        public async Task<ActionResult<RegistrationForm>> CreateRegistrationFormAsync(
            [FromBody] RegistrationFormCreationDTO registrationFormCreationDto)
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
        public async Task<ActionResult<RegistrationForm>> CreateRegistrationFullFormAsync(
            [FromBody] FullRegistrationFormDTO fullRegistrationFormDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var manager = _mapper.Map<Manager>(fullRegistrationFormDto.managerCreationDto);
            var school = _mapper.Map<School>(fullRegistrationFormDto.schoolCreationDto);
            var schoolClass = _mapper.Map<SchoolClass>(fullRegistrationFormDto.schoolClassCreationDto);
            var days = _mapper.Map<ReservationSelectedDay>(fullRegistrationFormDto.reservationSelectedDays);
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

            var createdDay = await _reservationRepository.AddSelectedDays(days);

            RegistrationForm form;
            if (fullRegistrationFormDto.hasProject)
            {
                form = new RegistrationForm()
                {
                    ManagerId = createdManager.Id,
                    SchoolId = createdSchool.Id,
                    SchoolClassId = createdClass.Id,
                    ReservationSelectedDaysId = createdDay.Id,
                    ProjectId = createdProject.Id
                };
            }
            else
            {
                form = new RegistrationForm()
                {
                    ManagerId = createdManager.Id,
                    SchoolId = createdSchool.Id,
                    SchoolClassId = createdClass.Id,
                    ReservationSelectedDaysId = createdDay.Id,
                };
            }

            var createdForm = await _reservationRepository.AddRegistrationFormAsync(form);
            await _reservationRepository.ChangeRegisterFormId(fullRegistrationFormDto.StudentListFileId,
                createdForm.Id);
            await _reservationRepository.ChangeRegisterFormId(fullRegistrationFormDto.ManagerFormFileId, createdForm.Id);
            // var result = await _reservationRepository.CreatePdfFromRegistrationFormId(createdForm.Id);
            var result = await _reservationRepository.CreateExcelFromRegistrationFormId(createdForm.Id);
            // _mailService.Email(subject: "گزارش ثبت‌نام مدرسه", htmlString: result);
            
            return Ok("Successfully created.");
        }

        #endregion

        #region GET

        [HttpGet]
        [Route("api/createReportPDF")]
        public async Task<string> CreatePdfFromQuery(int id)
        {
            // Assume GetDataFromQuery is a method that executes SQL query and returns data

            var result = await _reservationRepository.CreatePdfFromRegistrationFormId(id);
            _mailService.Email(subject: "گزارش ثبت‌نام مدرسه", htmlString: result);

            return result;
        }
        
        [HttpGet]
        [Route("api/createReportExcel")]
        public async Task<ActionResult<String>> CreateExcelFromQuery(int id)
        {
            var result = await _reservationRepository.CreateExcelFromRegistrationFormId(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/getAllRegistrations")]
        [Produces("application/json")]
        public async Task<List<RegisteriationFormDTO>> GetAllForms()
        {
            var result = await _reservationRepository.GetAllFormsDataAsync();
            return result;
        }
        
        
        #endregion
        
        #region DELETE
        
        [HttpDelete]
        [Route("api/deleteRegistrationForm")]
        [Produces("application/json")]
        public async Task<bool> DeleteRegistrationForm(int id)
        {
            var result = await _reservationRepository.DeleteRegistrationForm(id);
            return result;
        }
        
        #endregion
        
    }
}
