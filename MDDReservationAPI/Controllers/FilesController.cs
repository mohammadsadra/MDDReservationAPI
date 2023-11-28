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
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

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

        // [HttpPost("PostMultipleFile")]
        // public async Task<ActionResult> PostMultipleFile([FromForm] List<FileUploadDTO> fileDetails)
        // {
        //     if (fileDetails == null)
        //     {
        //         return BadRequest();
        //     }
        //
        //     try
        //     {
        //         await _reservationRepository.PostMultiFileAsync(fileDetails);
        //         return Ok();
        //     }
        //     catch (Exception)
        //     {
        //         throw;
        //     }
        // }
    }