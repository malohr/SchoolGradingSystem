using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolGradesystem.DataTransferObjects;
using SchoolGradesystem.Models;
using SchoolGradesystem.Persistence;
using SchoolGradesystem.ViewModels;

namespace SchoolGradesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly SchoolGradeSystemDbContext _context;

        public SchoolsController(SchoolGradeSystemDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchool(SchoolDTO schoolDTO)
        {

            if (schoolDTO.Address == null) return BadRequest("There is no address provided!");
            var address = new Address(schoolDTO.Address.StreetName, 
                schoolDTO.Address.HouseNumber, schoolDTO.Address.CityName,
                schoolDTO.Address.ZipCode);
            
            var school = new School(schoolDTO.Name, schoolDTO.IsPrivate);
            school.AddAddress(address);

            await _context.Set<School>().AddAsync(school);
            await _context.SaveChangesAsync();

            return Ok(new { id = school.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchools()
        {
            List<School> schools = await _context.Set<School>().Include(i => i.Address).ToListAsync();

            List<SchoolViewModel> mappedSchools = new List<SchoolViewModel>();
            foreach (var school in schools)
            {
                var mappedSchool = MapToSchoolViewModel(school);
                mappedSchools.Add(mappedSchool);
            }
            return Ok(mappedSchools);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchoolById(int id)
        {
            var school = await _context.Set<School>().Include(i => i.Address).FirstOrDefaultAsync(school => school.Id == id);
            if (school == null) return BadRequest("The school with the provided id, could not be found");
            var mappedSchoolViewModel = MapToSchoolViewModel(school);
           
            return Ok(mappedSchoolViewModel);
        }

        private SchoolViewModel MapToSchoolViewModel(School school)
        {
            var schoolViewModel = new SchoolViewModel();
            schoolViewModel.Name = school.Name;
            schoolViewModel.IsPrivate = school.IsPrivate;
            schoolViewModel.Id = school.Id;

            if (school.Address != null)
            {
                var addressViewModel = new AddressViewModel();
                addressViewModel.Id = school.Address.Id;
                addressViewModel.StreetName = school.Address.StreetName;
                addressViewModel.HouseNumber = school.Address.HouseNumber;
                addressViewModel.CityName = school.Address.CityName;
                addressViewModel.ZipCode = school.Address.ZipCode;

                // assign address view model to school view model
                schoolViewModel.Address = addressViewModel;
            }

            return schoolViewModel;
        }


        [HttpPut("{schoolId}")]
        public async Task<IActionResult> UpdateSchool(int schoolId, string name, bool isPrivate)
        {
            var foundSchool = await _context.Set<School>().FirstOrDefaultAsync(school => school.Id == schoolId);
            if (foundSchool == null)
            {
                return NotFound("The school with the provided id was not found");
            }

            // locally changed
            foundSchool.Name = name;
            foundSchool.IsPrivate = isPrivate;

            // saving our changes to the database now!
            _context.Update(foundSchool);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolById(int id)
        {
            var school = await _context.Set<School>().FirstOrDefaultAsync(school => school.Id == id);
            if (school == null) return BadRequest("The school with the provided id, could not be found");

            _context.Set<School>().Remove(school);
            await _context.SaveChangesAsync();

            return Ok();
        }



    }
}
