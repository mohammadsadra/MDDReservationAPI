using System.Globalization;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MDDReservationAPI.Controllers;
using MDDReservationAPI.Data;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Enums;
using MDDReservationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;


namespace MDDReservationAPI.Repositories
{
    public class MDDReservationRepository : IMDDReservationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MDDReservationRepository> _logger;

        public MDDReservationRepository(ApplicationDbContext applicationDbContext, ILogger<MDDReservationRepository> logger)
        {
            _context = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }


        #region School

        public async Task<School> AddSchoolAsync(School school)
        {
            _context.Schools.Add(school);
            await SaveChangesAsync();
            return school;
        }
        
        public async Task<School?> GetSchoolByIdAsync(int id)
        {
            var school = await _context.Schools.Where(x => x.Id == id).FirstOrDefaultAsync();
            return school;
        }

        public async Task<bool> SchoolExistsAsync(int schoolId)
        {
            return await _context.Schools.AnyAsync(s => s.Id == schoolId);
        }

        public string schoolEnumToString(string? value)
        {
            if (value == "NonProfit")
            {
                return "غیرانتفاعی";
            }
            else if (value == "Governmental")
            {
                return "دولتی";
            }
            else if (value == "Sampad")
            {
                return "سمپاد";
            }
            else if (value == "GovernmentSample")
            {
                return "نمونه‌دولتی";
            }
            else if (value == "Other")
            {
                return "سایر";
            }
            else
            {
                return "سایر";
            }
        }
        

        #endregion

        #region Manager

        public async Task<Manager> AddManagerAsync(Manager manager)
        {
            _context.Managers.Add(manager);
            await SaveChangesAsync();

            return manager;
        }
        
        public async Task<Manager?> GetManagerByIdAsync(int id)
        {
            var manager = await _context.Managers.Where(x => x.Id == id).FirstOrDefaultAsync();
            return manager;
        }

        #endregion

        #region Admin

        public async Task<Admin> AddAdminAsync(Admin admin)
        {
            _context.Admins.Add(admin);
            await SaveChangesAsync();
            var admins = _context.Admins.ToList();
            return admin;
        }
        
        public async Task<Admin?> GetAdminByIdAsync(int id)
        {
            var admin = await _context.Admins.Where(x => x.Id == id).FirstOrDefaultAsync();
            return admin;
        }

        #endregion

        #region Student

        public async Task<Student> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await SaveChangesAsync();

            return student;
        }
        
        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            var student = await _context.Students.Where(x => x.Id == id).FirstOrDefaultAsync();
            return student;
        }

        #endregion

        #region Project

        public async Task<Project> AddProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await SaveChangesAsync();

            return project;
        }
        
        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            var project = await _context.Projects.Where(x => x.Id == id).FirstOrDefaultAsync();
            return project;
        }

        #endregion

        #region SchoolClass

        public async Task<SchoolClass> AddSchoolClassAsync(SchoolClass schoolClass)
        {
            _context.SchoolsClasses.Add(schoolClass);
            await SaveChangesAsync();
            return schoolClass;
        }
        
        public async Task<SchoolClass?> GetSchoolClassByIdAsync(int id)
        {
            var schoolClass = await _context.SchoolsClasses.Where(x => x.Id == id).FirstOrDefaultAsync();
            return schoolClass;
        }

        #endregion

        #region RegistrationForm

        public async Task<RegistrationForm> AddRegistrationFormAsync(RegistrationForm registrationForm)
        {
            _context.RegistrationForms.Add(registrationForm);
            await SaveChangesAsync();

            return registrationForm;
        }
        
        public async Task<RegistrationForm?> GetRegistrationFormByIdAsync(int id)
        {
            var form = await _context.RegistrationForms.Where(x => x.Id == id).FirstOrDefaultAsync();
            return form;
        }

        public async Task<List<RegisteriationFormDTO>> GetAllFormsDataAsync()
        {
            // Assuming _context is your database context and RegistrationForms is your DbSet
            var allForms = await _context.RegistrationForms.ToListAsync();
            var allFormsDto = new List<RegisteriationFormDTO>();
            foreach (var form in allForms)
            {
                var school = await GetSchoolByIdAsync(form.SchoolId); 
                var schoolClass = await  GetSchoolClassByIdAsync(form.SchoolClassId); 
                var manager = await GetManagerByIdAsync(form.ManagerId); 
                var days =  await GetSelectedDaysByReservationId(form.ReservationSelectedDaysId);
                var files = await GetFileDataFromDb(form.Id);
                var reportLink = "";
                var student = "";
                var letter = "";
                
                foreach(FileDetails f in files)
                {
                    if (f.FilePathType == FilePathType.PDF && f.FileKind == FileKind.ManagerForm)
                    {
                        letter = "https://bazididapi.hamrah.academy/download/documents/" + f.FileName;
                    }

                    if (f.FilePathType == FilePathType.XLSX && f.FileKind == FileKind.StudentList)
                    {
                        student = "https://bazididapi.hamrah.academy/download/documents/" + f.FileName;
                    }

                    if (f.FilePathType == FilePathType.XLSX && f.FileKind == FileKind.Document)
                    {
                        reportLink = "https://bazididapi.hamrah.academy/download/" + f.FileName;
                    }
                }
                
                
                var formDto = new RegisteriationFormDTO()
                {
                    Id = form.Id,
                    ManagerId = form.ManagerId,
                    Manager = manager,
                    SchoolId = form.SchoolId,
                    School = school,
                    SchoolClassId = form.SchoolClassId,
                    SchoolClass = schoolClass,
                    ProjectId = form.ProjectId,
                    ReservationSelectedDaysId = form.ReservationSelectedDaysId,
                    ReservationSelectedDays = days,
                    DownloadLink = reportLink,
                    DownloadStudentLink = student,
                    DownloadLetterLink = letter,
                    CreatedAt = form.CreatedAt
                };
                allFormsDto.Add(formDto);
            }
            
            return allFormsDto;
        }
        
        public async Task<string> CreatePdfFromRegistrationFormId(int id)
        {
            using (var memoryStream = new MemoryStream())
            {
                var form = await GetRegistrationFormByIdAsync(id); 
                var school = await GetSchoolByIdAsync(form!.SchoolId); 
                var schoolClass = await  GetSchoolClassByIdAsync(form!.SchoolClassId); 
                var manager = await GetManagerByIdAsync(form.ManagerId); 
                var days =  await GetSelectedDaysByReservationId(form.ReservationSelectedDaysId); 
                // var project =  GetManagerByIdAsync(form.Result!.ProjectId); 
                
                // Find the Tehran Time Zone
                TimeZoneInfo tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");

                var fileName = Guid.NewGuid() + "_" + school!.Name + ".pdf";
                
                using (Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f))
                {
                    PdfWriter.GetInstance(pdfDoc, new FileStream(fileName, FileMode.Create));
                    pdfDoc.Open();
                    pdfDoc.NewPage();
                    BaseFont bf = BaseFont.CreateFont("./Fonts/IRANSansX-Regular.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Font font = new Font(bf, 12);

                    if (form != null)
                    {
                        PdfPTable table = new PdfPTable(1);
                        PdfPCell cell = new PdfPCell(new Phrase("گزارش ثبت‌نام مدرسه", font))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            Border = 1,
                            Padding = 10
                            
                        };
                        table.AddCell(cell);
                        
                        PdfPCell cell2 = new PdfPCell(new Phrase("نام مدرسه:  " + school.Name, font))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            Border = 1,
                            Padding = 10
                        };
                        table.AddCell(cell2);
                        PdfPCell cell2_1 = new PdfPCell(new Phrase("جنسیت دانش‌آموزان:  " + Enum.GetName(typeof(Gender), school.Gender!)  , font))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            Border = 0,
                            Padding = 10
                        };
                        table.AddCell(cell2_1);
                        PdfPCell cell2_2 = new PdfPCell(new Phrase("مقطع:  " + Enum.GetName(typeof(GradeEnum), schoolClass!.Grade)  , font))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            Border = 0,
                            Padding = 10
                        };
                        table.AddCell(cell2_2);
                        PdfPCell cell2_3 = new PdfPCell(new Phrase("نوع مدرسه:  " + Enum.GetName(typeof(SchoolType), school.SchoolType!)  , font))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            Border = 0,
                            Padding = 10
                        };
                        table.AddCell(cell2_3);
                        
                        PdfPCell cell3 = new PdfPCell(new Phrase("نام ثبت‌نام کننده: " + manager.Name, font))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            Border = 0,
                            Padding = 10
                        };
                        table.AddCell(cell3);
                        
                        PdfPCell cell4 = new PdfPCell(new Phrase(" شماره تماس ثبت‌نام کننده: " + manager.Phone,font))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            Border = 0,
                            Padding = 10
                        };
                        table.AddCell(cell4);
                        
                        PdfPCell cell5 = new PdfPCell(new Phrase( " سمت: " + manager.Position, font))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            Border = 0,
                            Padding = 10
                        };
                        table.AddCell(cell5);
                        
                        
                        var ans = schoolClass.IsProgrammer ? "بله" : "خیر";
                        PdfPCell cell6 = new PdfPCell(new Phrase("آیا برنامه‌نویس هستند: " + ans, font))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            Border = 0,
                            Padding = 10
                        };
                        table.AddCell(cell6);
                        
                        if (schoolClass.IsProgrammer) 
                        { 
                            PdfPCell cell7 = new PdfPCell(new Phrase("زبان برنامه‌نویسی: " + Enum.GetName(typeof(ProgrammingLanguage),  schoolClass.ProgrammingLanguage), font))
                            {
                                RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                                Border = 0,
                                Padding = 10
                            };
                            table.AddCell(cell7);
                        }
                        PersianCalendar pc = new PersianCalendar();
                        var gregorianDate = TimeZoneInfo.ConvertTimeFromUtc(days.FirstDay, tehranTimeZone);
                        int year = pc.GetYear(gregorianDate);
                        int month = pc.GetMonth(gregorianDate);
                        int day = pc.GetDayOfMonth(gregorianDate);
                        
                        PdfPCell cell8 = new PdfPCell(new Phrase("روز اول: ", font))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                            Border = 0,
                            Padding = 5
                        };
                        table.AddCell(cell8);
                        PdfPCell cell8_1 = new PdfPCell(new Phrase($"{year}/{month.ToString("00")}/{day.ToString("00")}", font))
                        {
                            RunDirection = PdfWriter.RUN_DIRECTION_LTR,
                            Border = 0,
                            Padding = 10
                        };
                        table.AddCell(cell8_1);
                        
                        if (days.SecondDay != null)
                        {
                            PersianCalendar pc2 = new PersianCalendar();
                            var gregorianDate2 = TimeZoneInfo.ConvertTimeFromUtc((DateTime) days.SecondDay, tehranTimeZone);
                            int year2 = pc2.GetYear(gregorianDate2);
                            int month2 = pc2.GetMonth(gregorianDate2);
                            int day2 = pc2.GetDayOfMonth(gregorianDate2);
                            PdfPCell cell9 = new PdfPCell(new Phrase("روز دوم: ", font))
                            {
                                RunDirection = PdfWriter.RUN_DIRECTION_RTL,
                                Border = 0,
                                Padding = 5
                            };
                            table.AddCell(cell9);
                            PdfPCell cell9_1 = new PdfPCell(new Phrase($"{year2}/{month2.ToString("00")}/{day2.ToString("00")}", font))
                            {
                                RunDirection = PdfWriter.RUN_DIRECTION_LTR,
                                Border = 0,
                                Padding = 10
                            };
                            table.AddCell(cell9_1);
                            
                        }
                        
                        pdfDoc.Add(table);
                    }
                    else
                    {
                        pdfDoc.Add(new Paragraph("No data available."));
                    }

                    pdfDoc.Close();
                }
                return "https://bazididapi.hamrah.academy/download/" + fileName;
            }
        }
        
        public async Task<string> CreateExcelFromRegistrationFormId(int id)
        {
            
            var fileNameInDb = await CheckExcelFileAvailable(id);
            if (fileNameInDb != "")
            {
                return "https://bazididapi.hamrah.academy/download/" + fileNameInDb; 
            }
            using (var package = new ExcelPackage())
            {
                var form = await GetRegistrationFormByIdAsync(id); 
                var school = await GetSchoolByIdAsync(form!.SchoolId); 
                var schoolClass = await GetSchoolClassByIdAsync(form!.SchoolClassId); 
                var manager = await GetManagerByIdAsync(form.ManagerId); 
                var days = await GetSelectedDaysByReservationId(form.ReservationSelectedDaysId);
                var files = await GetFileDataFromDb(id);

                // Find the Tehran Time Zone
                TimeZoneInfo tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");

                var worksheet = package.Workbook.Worksheets.Add("Registration Report");

                // Set font for the entire worksheet
                worksheet.Cells.Style.Font.Name = "IRANSansX";
                worksheet.Cells.Style.Font.Size = 12;

                // Populate the worksheet with data
                int row = 1;

                worksheet.Cells[row, 1].Value = "گزارش ثبت‌نام مدرسه";
                row++;
                
                worksheet.Cells[row, 1].Value = "نام مدرسه: ";
                worksheet.Cells[row, 2].Value = school.Name;
                row++;
                
                worksheet.Cells[row, 1].Value = "جنسیت دانش‌آموزان: ";
                worksheet.Cells[row, 2].Value = school.Gender! == 0 ? "پسر" : "دختر";
                row++;
                
                worksheet.Cells[row, 1].Value = "مقطع: ";
                worksheet.Cells[row, 2].Value = schoolClass!.Grade == 1 ? "متوسطه اول" : "متوسطه دوم";
                row++;
                
                worksheet.Cells[row, 1].Value = "نوع مدرسه: ";
                worksheet.Cells[row, 2].Value = schoolEnumToString(Enum.GetName(typeof(SchoolType), school.SchoolType!));
                row++;
                
                worksheet.Cells[row, 1].Value = "نام ثبت‌نام کننده:";
                worksheet.Cells[row, 2].Value = manager.Name;
                row++;
                
                worksheet.Cells[row, 1].Value = "شماره تماس ثبت‌نام کننده:" ;
                worksheet.Cells[row, 2].Value = manager.Phone;
                row++;
                
                worksheet.Cells[row, 1].Value = " سمت:" ;
                worksheet.Cells[row, 2].Value = manager.Position;
                row++;
                
                var ans = schoolClass.IsProgrammer ? "بله" : "خیر";
                worksheet.Cells[row, 1].Value = "آیا برنامه‌نویس هستند:" ;
                worksheet.Cells[row, 2].Value = ans;
                row++;
                
                if (schoolClass.IsProgrammer) 
                { 
                    worksheet.Cells[row, 1].Value = "زبان برنامه‌نویسی: " ;
                    worksheet.Cells[row, 2].Value = Enum.GetName(typeof(ProgrammingLanguage),  schoolClass.ProgrammingLanguage);
                    row++;
                }
                
                PersianCalendar pc = new PersianCalendar();
                var gregorianDate = TimeZoneInfo.ConvertTimeFromUtc(days.FirstDay, tehranTimeZone);
                worksheet.Cells[row, 1].Value = "روز اول: ";
                worksheet.Cells[row, 2].Value = $"{pc.GetYear(gregorianDate)}/{pc.GetMonth(gregorianDate).ToString("00")}/{pc.GetDayOfMonth(gregorianDate).ToString("00")}";
                row++;
                
                
                if (days.SecondDay != null)
                {
                    PersianCalendar pc2 = new PersianCalendar();
                    var gregorianDate2 = TimeZoneInfo.ConvertTimeFromUtc((DateTime) days.SecondDay, tehranTimeZone);
                    worksheet.Cells[row, 1].Value = "روز دوم: ";
                    worksheet.Cells[row, 2].Value = $"{pc2.GetYear(gregorianDate2)}/{pc2.GetMonth(gregorianDate2).ToString("00")}/{pc2.GetDayOfMonth(gregorianDate2).ToString("00")}";
                    row++;
                }
                
                foreach(FileDetails f in files)
                {
                    if (f.FilePathType == FilePathType.PDF && f.FileKind == FileKind.ManagerForm)
                    {
                        worksheet.Cells[row, 1].Value =  "فایل نامه درخواست مدرسه:";
                        worksheet.Cells[row, 2].Value = "https://bazididapi.hamrah.academy/download/documents/" + f.FileName;
                        row++;
                    }

                    if (f.FilePathType == FilePathType.XLSX && f.FileKind == FileKind.StudentList)
                    {
                        worksheet.Cells[row, 1].Value = "فایل لیست دانش‌آموزان:" ;
                        worksheet.Cells[row, 2].Value = "https://bazididapi.hamrah.academy/download/documents/" + f.FileName;
                        row++;
                    }
                }

                // Saving the Excel file
                string directoryPath = @"Reports";
                var fileName = form.Id + "_" + school!.Name + ".xlsx";
                string fullPath = Path.Combine(directoryPath, fileName);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                FileInfo fileInfo = new FileInfo(fullPath);
                package.SaveAs(fileInfo);
                
                var fileDetails = new FileDetails
                {
                    FileKind = FileKind.Document,
                    FilePathType = FilePathType.XLSX,
                    FileName =  fileInfo.Name,
                    FileData = Encoding.UTF8.GetBytes(""),
                    RegistrationFormId = id
                };
                
                await AddFileDetailToDB(fileDetails: fileDetails);

                // Return the path or URL to the file
                return "https://bazididapi.hamrah.academy/download/" + fileInfo.Name;
            }
        }

        public async Task<bool> DeleteRegistrationForm(int id)
        {
            // Retrieve the item from the database
            var item = await _context.RegistrationForms.Where(x => x.Id == id).FirstOrDefaultAsync();

            // Check if the item exists
            if (item == null)
            {
                // Item not found, handle accordingly, maybe throw an exception or return a specific value
                return false;
            }
            var reservationSelectedDay = _context.ReservationSelectedDay.Where(x => x.Id == item.ReservationSelectedDaysId).FirstOrDefault();
            if (reservationSelectedDay != null) _context.ReservationSelectedDay.Remove(reservationSelectedDay);
            
            var manager = _context.Managers.Where(x => x.Id == item.ManagerId).FirstOrDefault();
            if (manager != null) _context.Managers.Remove(manager);
            
            var schoolClass = _context.SchoolsClasses.Where(x => x.Id == item.SchoolClassId).FirstOrDefault();
            if (schoolClass != null) _context.SchoolsClasses.Remove(schoolClass);

            var school = _context.Schools.Where(x => x.Id == item.SchoolId).FirstOrDefault();
            if (school != null) _context.Schools.Remove(school);

            var files = await _context.FileDetails.Where(x => x.RegistrationFormId == item.Id).ToListAsync();
            foreach (var file in files)
            {
                _context.FileDetails.Remove(file);
            }
            
            // Remove the item from the database context
            _context.RegistrationForms.Remove(item);

            // Save changes to the database
            await _context.SaveChangesAsync();
            
            return true;
        }


        #endregion

        #region DB

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region File
        
        private static bool IsAllowedFileExtension(string fileName, IEnumerable<string> allowedExtensions,
            out string fileExtension)
        {
            fileExtension = string.Empty;

            if (!fileName.Contains('.'))
            {
                return false;
            }

            fileExtension = ExtractFileExtension(fileName);

            var extension = fileExtension;

            return allowedExtensions.Any(ext => ext == extension);
        }

        public static string ExtractFileExtension(string fileName)
        {
            var extension = fileName.Split('.')[fileName.Split('.').Length - 1];
            extension = extension.ToLower();
            return extension;
        }

        public async Task<int> PostFileAsync(FileUploadDTO fileDetails)
        {
            var guid = Guid.NewGuid();
            var fileName = guid + "_" + fileDetails.FileDetails.FileName;
            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(),
                $"Upload{Path.DirectorySeparatorChar}BazididFiles{Path.DirectorySeparatorChar}");
            var path = Path.Combine(Directory.GetCurrentDirectory(),
                $"Upload{Path.DirectorySeparatorChar}BazididFiles{Path.DirectorySeparatorChar}",  fileName);
            
            var allowedExtension = fileDetails.FilePathType == (FilePathType) 1 ? "pdf" : "xlsx";
            
            try
            {
                if (!IsAllowedFileExtension(fileDetails.FileDetails.FileName, new[] { allowedExtension },
                        out string extension))
                {
                    return 0;
                }
                

                var file = new FileDetails()
                {
                    FileName = fileName,
                    FilePathType = fileDetails.FilePathType,
                    FileKind = fileDetails.FileKind,
                    RegistrationFormId = fileDetails.RegistrationFormId
                };

                using (var stream = new MemoryStream())
                {
                    await fileDetails.FileDetails.CopyToAsync(stream);
                    file.FileData = stream.ToArray();
                }
                
                if (!Directory.Exists(pathBuilt))
                    Directory.CreateDirectory(pathBuilt);
                using (FileStream stream = new(path, FileMode.Create))
                {
                    fileDetails.FileDetails.CopyToAsync(stream).Wait();
                }

                _context.FileDetails.Add(file);
                await _context.SaveChangesAsync();
                return file.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task PostMultiFileAsync(List<FileUploadDTO> fileData)
        {
            try
            {
                foreach(FileUploadDTO file in fileData)
                {
                    var fileDetails = new FileDetails()
                    {
                        FileName = file.FileDetails.FileName,
                        FilePathType = file.FilePathType,
                    };

                    using (var stream = new MemoryStream())
                    {
                        file.FileDetails.CopyTo(stream);
                        fileDetails.FileData = stream.ToArray();
                    }

                    var result = _context.FileDetails.Add(fileDetails);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DownloadFileById(Guid guid)
        {
            try
            {
                var file =  _context.FileDetails.Where(x => x.Guid == guid).FirstOrDefaultAsync();

                var content = new System.IO.MemoryStream(file.Result!.FileData);
                var path = Path.Combine(
                   Directory.GetCurrentDirectory(), "FileDownloaded",
                   file.Result.FileName);

                await CopyStream(content, path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ChangeRegisterFormId(int fileId, int registerFormId)
        {
            var file = await _context.FileDetails.Where(x => x.Id == fileId).FirstOrDefaultAsync();
            _logger.LogError(file.Id.ToString());
            if (file != null) file.RegistrationFormId = registerFormId;
            else
            {
                return false;
            }
            await _context.SaveChangesAsync();
            _logger.LogError(file.RegistrationFormId.ToString());
            return true;
        }
        
        private async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
               await stream.CopyToAsync(fileStream);
            }
        }
        
        private async Task<List<FileDetails>> GetFileDataFromDb(int id)
        {
            var files = await _context.FileDetails.Where(x => x.RegistrationFormId == id).ToListAsync();
            return files;
        }
        
        public async Task<bool> AddFileDetailToDB(FileDetails fileDetails)
        {
            _context.FileDetails.Add(fileDetails);
            await SaveChangesAsync();
            return true;
        }

        public async Task<string> CheckExcelFileAvailable(int formID)
        {
            var result = await _context.FileDetails.Where(x => x.RegistrationFormId ==formID && x.FilePathType == FilePathType.XLSX && x.FileKind == FileKind.Document).FirstOrDefaultAsync();
            if (result == null)
            {
                return "";
            }
            else
            {
                return result.FileName;
            }
        }
        
        
        #endregion

        #region ReservationDays
        public async Task<ReservationSelectedDay> AddSelectedDays(ReservationSelectedDay reservationSelectedDay)
        { _context.ReservationSelectedDay.Add(reservationSelectedDay);
            await SaveChangesAsync();

            return reservationSelectedDay;
        }

        

        public async Task<ReservationSelectedDay?> GetSelectedDaysByReservationId(int reservationId)
        {
            var reservationSelectedDay = await _context.ReservationSelectedDay.Where(x => x.Id == reservationId).FirstOrDefaultAsync();
            return reservationSelectedDay;
        }
        
        public async Task<bool> ChangeReservationSelectedDayId(int reservationSelectedDayId, int selectedDay)
        {
            var selectedDays = await _context.ReservationSelectedDay.Where(x => x.Id == reservationSelectedDayId)
                .FirstOrDefaultAsync();
            if (selectedDays != null)
            {
                selectedDays.SelectedDay = selectedDay;
                selectedDays.IsVerify = true;
            }
            else
            {
                return false;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}