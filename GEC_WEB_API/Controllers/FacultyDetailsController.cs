using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GEC_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyDetailsController : ControllerBase
    {
        private static List<FacultyDetails> Faculty = new List<FacultyDetails>
            {
                new FacultyDetails
                {
                    Id = 1,
                    Name = "Aditya Patel",
                    DeptId = 300,
                    DesignationId = 100,
                    //IsDeleted = false
                },
                new FacultyDetails
                {
                    Id = 2,
                    Name = "Ojas Patel",
                    DeptId = 300,
                    DesignationId = 100,
                    //IsDeleted = false
                }
            };

        private readonly DataContext _context;

        public FacultyDetailsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<FacultyDetails>>> Get()
        {
            return Ok(await _context.FacultyDetail.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult<List<FacultyDetails>>> AddFaculty(FacultyDetails FacultyDetail)
        {
            _context.FacultyDetail.Add(FacultyDetail);
            await _context.SaveChangesAsync();
            return Ok(await _context.FacultyDetail.ToListAsync());
        }

        [HttpPut]
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

            return Ok(Faculty);
        }

        [HttpDelete]
        public async Task<ActionResult<List<FacultyDetails>>> Delete(int id)
        {
            var FacultyId = Faculty.Find(f => f.Id == id);
            if (FacultyId == null)
                return BadRequest("Not Found");

            Faculty.Remove(FacultyId);
            return Ok(Faculty);
        }

    }
}
