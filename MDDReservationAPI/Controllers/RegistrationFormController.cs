using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using AutoMapper;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Models;
using MDDReservationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MDDReservationAPI.Enums;

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
                form =  new RegistrationForm()
                {
                    ManagerId = createdManager.Id,
                    SchoolId = createdSchool.Id,
                    SchoolClassId = createdClass.Id,
                    ReservationSelectedDaysId = createdDay.Id,
                };
            }

            var createdForm = await _reservationRepository.AddRegistrationFormAsync(form);
            await _reservationRepository.ChangeRegisterFormId(fullRegistrationFormDto.StudentListFileId, createdForm.Id);
            await _reservationRepository.ChangeRegisterFormId(fullRegistrationFormDto.ManagerFormId, createdForm.Id);

            return Ok("Successfully created.");
        }
        
        #endregion
        
        #region GET
        [HttpGet]
        [Route("api/createReport")]
        public async Task<string> CreatePdfFromQuery(int id)
        {
            // Assume GetDataFromQuery is a method that executes SQL query and returns data
            
            using (var memoryStream = new MemoryStream())
            {
                var result = await _reservationRepository.CreatePdfFromRegistrationFormId(id); 
                
                return result;
            }
        }
        
        // [HttpGet]
        // [Route("api/createReport")]
        // public async Task<string> CreatePdfFromQuery(int query)
        // {
        //     // Assume GetDataFromQuery is a method that executes SQL query and returns data
        //     
        //     using (var memoryStream = new MemoryStream())
        //     {
        //         var form = await _reservationRepository.GetRegistrationFormByIdAsync(query); 
        //         var school = await _reservationRepository.GetSchoolByIdAsync(form!.SchoolId); 
        //         var schoolClass = await  _reservationRepository.GetSchoolClassByIdAsync(form!.SchoolClassId); 
        //         var manager = await _reservationRepository.GetManagerByIdAsync(form.ManagerId); 
        //         var days =  await _reservationRepository.GetSelectedDaysByReservationId(form.ReservationSelectedDaysId); 
        //         // var project =  _reservationRepository.GetManagerByIdAsync(form.Result!.ProjectId); 
        //         
        //         // Find the Tehran Time Zone
        //         TimeZoneInfo tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
        //
        //         var fileName = Guid.NewGuid() + "_" + school!.Name + ".pdf";
        //         
        //         using (Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f))
        //         {
        //             PdfWriter.GetInstance(pdfDoc, new FileStream(fileName, FileMode.Create));
        //             pdfDoc.Open();
        //             pdfDoc.NewPage();
        //             BaseFont bf = BaseFont.CreateFont("./Fonts/IRANSansX-Regular.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        //             Font font = new Font(bf, 12);
        //
        //             if (form != null)
        //             {
        //                 PdfPTable table = new PdfPTable(1);
        //                 PdfPCell cell = new PdfPCell(new Phrase("گزارش ثبت‌نام مدرسه", font))
        //                 {
        //                     RunDirection = PdfWriter.RUN_DIRECTION_RTL,
        //                     Border = 1,
        //                     Padding = 10
        //                     
        //                 };
        //                 table.AddCell(cell);
        //                 
        //                 PdfPCell cell2 = new PdfPCell(new Phrase("نام مدرسه:  " + school.Name, font))
        //                 {
        //                     RunDirection = PdfWriter.RUN_DIRECTION_RTL,
        //                     Border = 1,
        //                     Padding = 10
        //                 };
        //                 table.AddCell(cell2);
        //                 PdfPCell cell2_1 = new PdfPCell(new Phrase("جنسیت دانش‌آموزان:  " + Enum.GetName(typeof(Gender), school.Gender!)  , font))
        //                 {
        //                     RunDirection = PdfWriter.RUN_DIRECTION_RTL,
        //                     Border = 0,
        //                     Padding = 10
        //                 };
        //                 table.AddCell(cell2_1);
        //                 PdfPCell cell2_2 = new PdfPCell(new Phrase("مقطع:  " + Enum.GetName(typeof(GradeEnum), schoolClass!.Grade)  , font))
        //                 {
        //                     RunDirection = PdfWriter.RUN_DIRECTION_RTL,
        //                     Border = 0,
        //                     Padding = 10
        //                 };
        //                 table.AddCell(cell2_2);
        //                 PdfPCell cell2_3 = new PdfPCell(new Phrase("نوع مدرسه:  " + Enum.GetName(typeof(SchoolType), school.SchoolType!)  , font))
        //                 {
        //                     RunDirection = PdfWriter.RUN_DIRECTION_RTL,
        //                     Border = 0,
        //                     Padding = 10
        //                 };
        //                 table.AddCell(cell2_3);
        //                 
        //                 PdfPCell cell3 = new PdfPCell(new Phrase("نام ثبت‌نام کننده: " + manager.Name, font))
        //                 {
        //                     RunDirection = PdfWriter.RUN_DIRECTION_RTL,
        //                     Border = 0,
        //                     Padding = 10
        //                 };
        //                 table.AddCell(cell3);
        //                 
        //                 PdfPCell cell4 = new PdfPCell(new Phrase(" شماره تماس ثبت‌نام کننده: " + manager.Phone,font))
        //                 {
        //                     RunDirection = PdfWriter.RUN_DIRECTION_RTL,
        //                     Border = 0,
        //                     Padding = 10
        //                 };
        //                 table.AddCell(cell4);
        //                 
        //                 PdfPCell cell5 = new PdfPCell(new Phrase( " سمت: " + manager.Position, font))
        //                 {
        //                     RunDirection = PdfWriter.RUN_DIRECTION_RTL,
        //                     Border = 0,
        //                     Padding = 10
        //                 };
        //                 table.AddCell(cell5);
        //                 
        //                 
        //                 var ans = schoolClass.IsProgrammer ? "بله" : "خیر";
        //                 _logger.LogError(ans);
        //                 PdfPCell cell6 = new PdfPCell(new Phrase("آیا برنامه‌نویس هستند: " + ans, font))
        //                 {
        //                     RunDirection = PdfWriter.RUN_DIRECTION_RTL,
        //                     Border = 0,
        //                     Padding = 10
        //                 };
        //                 table.AddCell(cell6);
        //                 
        //                 if (schoolClass.IsProgrammer) 
        //                 { 
        //                     PdfPCell cell7 = new PdfPCell(new Phrase("زبان برنامه‌نویسی: " + Enum.GetName(typeof(ProgrammingLanguage),  schoolClass.ProgrammingLanguage), font))
        //                     {
        //                         RunDirection = PdfWriter.RUN_DIRECTION_RTL,
        //                         Border = 0,
        //                         Padding = 10
        //                     };
        //                     table.AddCell(cell7);
        //                 }
        //                 PersianCalendar pc = new PersianCalendar();
        //                 var gregorianDate = TimeZoneInfo.ConvertTimeFromUtc(days.FirstDay, tehranTimeZone);
        //                 int year = pc.GetYear(gregorianDate);
        //                 int month = pc.GetMonth(gregorianDate);
        //                 int day = pc.GetDayOfMonth(gregorianDate);
        //                 
        //                 PdfPCell cell8 = new PdfPCell(new Phrase("روز اول: ", font))
        //                 {
        //                     RunDirection = PdfWriter.RUN_DIRECTION_RTL,
        //                     Border = 0,
        //                     Padding = 5
        //                 };
        //                 table.AddCell(cell8);
        //                 PdfPCell cell8_1 = new PdfPCell(new Phrase($"{year}/{month.ToString("00")}/{day.ToString("00")}", font))
        //                 {
        //                     RunDirection = PdfWriter.RUN_DIRECTION_LTR,
        //                     Border = 0,
        //                     Padding = 10
        //                 };
        //                 table.AddCell(cell8_1);
        //                 
        //                 if (days.SecondDay != null)
        //                 {
        //                     PersianCalendar pc2 = new PersianCalendar();
        //                     var gregorianDate2 = TimeZoneInfo.ConvertTimeFromUtc(days.SecondDay, tehranTimeZone);
        //                     int year2 = pc2.GetYear(gregorianDate2);
        //                     int month2 = pc2.GetMonth(gregorianDate2);
        //                     int day2 = pc2.GetDayOfMonth(gregorianDate2);
        //                     PdfPCell cell9 = new PdfPCell(new Phrase("روز دوم: ", font))
        //                     {
        //                         RunDirection = PdfWriter.RUN_DIRECTION_RTL,
        //                         Border = 0,
        //                         Padding = 5
        //                     };
        //                     table.AddCell(cell9);
        //                     PdfPCell cell9_1 = new PdfPCell(new Phrase($"{year2}/{month2.ToString("00")}/{day2.ToString("00")}", font))
        //                     {
        //                         RunDirection = PdfWriter.RUN_DIRECTION_LTR,
        //                         Border = 0,
        //                         Padding = 10
        //                     };
        //                     table.AddCell(cell9_1);
        //                     
        //                 }
        //                 
        //                 pdfDoc.Add(table);
        //             }
        //             else
        //             {
        //                 pdfDoc.Add(new Paragraph("No data available."));
        //             }
        //
        //             pdfDoc.Close();
        //         }
        //         return "https://bazididapi.hamrah.academy/download/" + fileName;
        //     }
        // }

        
        // [HttpGet]
        // [Route("api/downloadpdf")]
        // public async Task<HttpResponseMessage> DownloadPdf(int query)
        // {
        //     var pdfBytes = await CreatePdfFromQuery(query);
        //     var result = new HttpResponseMessage(HttpStatusCode.OK)
        //     {
        //         Content = new ByteArrayContent(pdfBytes)
        //     };
        //     result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        //     {
        //         FileName = "report.pdf"
        //     };
        //     result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
        //     _logger.LogError(result.Headers.ToString());
        //
        //     return result;
        // }
        
       

        
        #endregion
    }
}
