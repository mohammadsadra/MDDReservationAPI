using Microsoft.AspNetCore.Mvc;

namespace MDDReservationAPI.Models;


[Route("school")]
[ApiController]
public class SchoolController : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    public ActionResult<School> CreatSchool([FromBody] School school)
    {
        // if (!ModelState.IsValid)
        // {
        //     return BadRequest();
        // }

          
        // var createdSchool = new SchoolDTO()
        // {
        //     Id = id,
        //     SchoolName = school.SchoolName,
        //     Address = school.Address
        // };
        //
        // SchoolsDataStore.current.Schools.Add(createdSchool);
        //

        return Ok() ;
    }
}