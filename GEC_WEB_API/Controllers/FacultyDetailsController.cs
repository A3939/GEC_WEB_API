using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GEC_WEB_API.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class FacultyDetailsController : ControllerBase
    {
        private readonly DataContext _context;

        public FacultyDetailsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("FacultyList")]
        public async Task<ActionResult<List<FacultyDetails>>> Get()
        {
            return Ok(await _context.FacultyDetail.ToListAsync());
        }


        [HttpPost]
        [Route("FacultyAdd")]
        public async Task<ActionResult<List<FacultyDetails>>> AddFaculty(FacultyDetails FacultyDetail)
        {
            _context.FacultyDetail.Add(FacultyDetail);
            await _context.SaveChangesAsync();
            return Ok(await _context.FacultyDetail.ToListAsync());
        }

        [HttpPut]
        [Route("FacultyEdit")]
        public async Task<ActionResult<List<FacultyDetails>>> UpdateFaculty(FacultyDetails FacultyDetails)
        {
            var FacultyId = await _context.FacultyDetail.FindAsync(FacultyDetails.Id);
            if(FacultyId == null)
                return BadRequest("Not Found");

            if( FacultyDetails.Id != 0)
                FacultyId.Name = FacultyDetails.Name;
            
            if(FacultyDetails.DeptId != 0)
                FacultyId.DeptId = FacultyDetails.DeptId;
            
            if(FacultyDetails.DesignationId != 0)
                FacultyId.DesignationId = FacultyDetails.DesignationId;

            await _context.SaveChangesAsync();

            return Ok(await _context.FacultyDetail.ToListAsync());
        }

        [HttpDelete]
        [Route("FacultyDelete")]
        public async Task<ActionResult<List<FacultyDetails>>> Delete(int id)
        {
            var FacultyId = await _context.FacultyDetail.FindAsync(id);
            if (FacultyId == null)
                return BadRequest("Not Found");

            _context.FacultyDetail.Remove(FacultyId);
            await _context.SaveChangesAsync();

            return Ok(await _context.FacultyDetail.ToListAsync());
        }

    }
}
