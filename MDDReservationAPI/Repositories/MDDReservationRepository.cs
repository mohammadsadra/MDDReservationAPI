using MDDReservationAPI.Controllers;
using MDDReservationAPI.Data;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Enums;
using MDDReservationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> SchoolExistsAsync(int schoolId)
        {
            return await _context.Schools.AnyAsync(s => s.Id == schoolId);
        }
        #endregion

        #region Manager

        public async Task<Manager> AddManagerAsync(Manager manager)
        {
            _context.Managers.Add(manager);
            await SaveChangesAsync();

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

        #endregion

        #region Student

        public async Task<Student> AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await SaveChangesAsync();

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

        #endregion

        #region SchoolClass

        public async Task<SchoolClass> AddSchoolClassAsync(SchoolClass schoolClass)
        {
            _context.SchoolsClasses.Add(schoolClass);
            await SaveChangesAsync();
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
        
        

        public async Task<bool> PostFileAsync(FileUploadDTO fileDetails)
        {
            var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(),
                $"Upload{Path.DirectorySeparatorChar}BazididFiles{Path.DirectorySeparatorChar}");
            var path = Path.Combine(Directory.GetCurrentDirectory(),
                $"Upload{Path.DirectorySeparatorChar}BazididFiles{Path.DirectorySeparatorChar}", fileDetails.FileDetails.FileName);
            
            var allowedExtension = fileDetails.FilePathType == (FilePathType) 1 ? "pdf" : "xlsx";
            
            try
            {
                if (!IsAllowedFileExtension(fileDetails.FileDetails.FileName, new[] { allowedExtension },
                        out string extension))
                {
                    return false;
                }
                    


                var file = new FileDetails()
                {
                    FileName = fileDetails.FileDetails.FileName,
                    FilePathType = fileDetails.FilePathType,
                    FileKind = fileDetails.FileKind
                };

                using (var stream = new MemoryStream())
                {
                    fileDetails.FileDetails.CopyTo(stream);
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
                return true;
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
            if (file != null) file.RegistrationFormId = registerFormId;
            else
            {
                return false;
            }
            await _context.SaveChangesAsync();
            return true;
        }
        
        private async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
               await stream.CopyToAsync(fileStream);
            }
        }
        
        
        #endregion

        #region ReservationDays
        public async Task<ReservationSelectedDay> AddSelectedDays(ReservationSelectedDay reservationSelectedDay)
        { _context.ReservationSelectedDay.Add(reservationSelectedDay);
            await SaveChangesAsync();

            return reservationSelectedDay;
        }

        #endregion
    }
}
