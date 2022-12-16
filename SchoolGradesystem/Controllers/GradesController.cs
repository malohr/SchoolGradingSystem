using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolGradesystem.Models;
using SchoolGradesystem.Persistence;

namespace SchoolGradesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly SchoolGradeSystemDbContext _context;
        public GradesController(SchoolGradeSystemDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGrade(int gradeNumber)
        {
            //validate if grade number is not existent
            var existingGrade = await _context.Set<Grade>().FirstOrDefaultAsync(grade => grade.Number == gradeNumber);
            if (existingGrade != null) return ValidationProblem("There is already a grade existing with the provided number");

            //creating the grade and assign the number provided by the customer
            Grade grade = new Grade();
            grade.Number = gradeNumber;

            //save it to the database
            _context.Add(grade);
            await _context.SaveChangesAsync();

            return Ok(grade.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGradeById(int id)
        {
            var grade = await _context.Set<Grade>().FirstOrDefaultAsync(grade => grade.Id == id);
            if (grade == null) return BadRequest("The grade with the provided id, could not be found");
            
            return Ok(grade);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGrades()
        {
            List<Grade> gradeList = await _context.Set<Grade>().ToListAsync();
            return Ok(gradeList);
        }

        /// <summary>
        /// Updates the grade with the grade number 
        /// </summary>
        /// <param name="gradeId">The id of the grade</param>
        /// <param name="gradeNumber">the number you wish to change</param>
        /// <returns></returns>
        [HttpPut("{gradeId}")]
        public async Task<IActionResult> UpdateGrade(int gradeId, int gradeNumber)
        {
            var foundGrade = await _context.Set<Grade>().FirstOrDefaultAsync(grade => grade.Id == gradeId);
            if (foundGrade == null)
            {
                return BadRequest("The grade with the provided id was not found");
            }

            // locally changed
            foundGrade.Number = gradeNumber;


            // saving our changes to the database now!
            _context.Update(foundGrade);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGradeById(int id)
        {
            var grade = await _context.Set<Grade>().FirstOrDefaultAsync(grade => grade.Id == id);
            if (grade == null) return BadRequest("The grade with the provided id, could not be found");

            _context.Set<Grade>().Remove(grade);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
