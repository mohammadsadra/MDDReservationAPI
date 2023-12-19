using System.Net;
using System.Net.Mail;
using MDDReservationAPI.DTO;
using MDDReservationAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MDDReservationAPI.Controllers;

    [Route("files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly IMDDReservationRepository _reservationRepository;

        public FilesController(ILogger<ManagerController> logger, IMDDReservationRepository mddReservationRepository)
        {
            _reservationRepository = mddReservationRepository ?? throw new ArgumentNullException(nameof(mddReservationRepository));
        }
        


        [HttpPost("PostSingleFile")]
        public async Task<ActionResult> PostSingleFile([FromForm] FileUploadDTO fileDetails)
        {
            if(fileDetails == null)
            {
                return BadRequest();
            }

            try
            {
                var id = await _reservationRepository.PostFileAsync(fileDetails);
                if (id == 0)
                {
                    return BadRequest("File type not supported");
                }
                return Ok(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
        [HttpGet]
        [Route("/download/{fileName}")]
        public IActionResult DownloadFile(string fileName)
        {
            // Define the path to the file
            var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Reports", fileName);

            // Check if file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found!");
            }

            // Read the file into a byte array
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Return the file with a file download name
            return File(fileBytes, "application/octet-stream", fileName);
        }
        
        [HttpGet]
        [Route("/download/documents/{fileName}")]
        public IActionResult DownloadDocumentsFile(string fileName)
        {
            // Define the path to the file
            var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Upload/BazididFiles", fileName);

            // Check if file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            // Read the file into a byte array
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Return the file with a file download name
            return File(fileBytes, "application/octet-stream", fileName);
        }
    }