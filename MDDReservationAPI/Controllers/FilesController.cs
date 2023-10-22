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
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost("PostMultipleFile")]
        public async Task<ActionResult> PostMultipleFile([FromForm] List<FileUploadDTO> fileDetails)
        {
            if (fileDetails == null)
            {
                return BadRequest();
            }

            try
            {
                await _reservationRepository.PostMultiFileAsync(fileDetails);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("DownloadFile")]
        public async Task<ActionResult> DownloadFile(Guid guid)
        {
            try
            {
                await _reservationRepository.DownloadFileById(guid);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }