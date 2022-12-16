using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolGradesystem.Models;
using SchoolGradesystem.Persistence;

namespace SchoolGradesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly SchoolGradeSystemDbContext _context;
        public MarksController(SchoolGradeSystemDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMark(int mark, int studentId, int subjectId)
        {
            //validate if student is existent
            var existingStudent = await _context.Set<Student>().FirstOrDefaultAsync(student => student.Id == studentId);
            if (existingStudent == null) return ValidationProblem("There was no student found with the provided id!");

            //validate if subject is existent
            var existingSubject = await _context.Set<Subject>().FirstOrDefaultAsync(subject => subject.Id == subjectId);
            if (existingSubject == null) return ValidationProblem("There was no subject found with the provided id!");

            //creating the mark and assign the number 
            Mark newMark = new Mark(mark, existingSubject.Id, existingStudent.Id);

            //save it to the database
            _context.Add(newMark);
            await _context.SaveChangesAsync();

            return Ok(newMark.Id);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetMarkById(int id)
        {
            var mark = await _context.Set<Mark>().FirstOrDefaultAsync(mark => mark.Id == id);
            if (mark == null) return BadRequest("The grade with the provided id, could not be found");

            return Ok(mark);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMarks()
        {
            List<Mark> markList = await _context.Set<Mark>().ToListAsync();
            return Ok(markList);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMark(int id)
        {
            var mark = await _context.Set<Mark>().FirstOrDefaultAsync(mark => mark.Id == id);
            if (mark == null) return BadRequest("The mark with the provided id, could not be found");

            _context.Set<Mark>().Remove(mark);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("{markId}")]
        public async Task<IActionResult> UpdateMark(int markId, int updatedMark)
        {
            var foundMark = await _context.Set<Mark>().FirstOrDefaultAsync(mark => mark.Id == markId);
            if (foundMark == null)
            {
                return BadRequest("The mark with the provided id was not found");
            }

            // locally changed
            foundMark.Value = updatedMark;


            // saving our changes to the database now!
            _context.Update(foundMark);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
