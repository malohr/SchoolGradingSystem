using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolGradesystem.DataTransferObjects;
using SchoolGradesystem.Models;
using SchoolGradesystem.Persistence;

namespace SchoolGradesystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly SchoolGradeSystemDbContext _context;
        public SubjectsController(SchoolGradeSystemDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(SubjectDTO subjectDTO)
        {

            //creating the subject
            Subject subject = new Subject();
            subject.Name = subjectDTO.Name;
            subject.Hours = subjectDTO.Hours;
            subject.MinimumMark = subjectDTO.MinimumMark;
            subject.ExamCount = subjectDTO.ExamCount;

            //database go and write my subject in the table
            _context.Add(subject);
            //save everything we just did!
            await _context.SaveChangesAsync();

            return Ok(new { SubjectId = subject.Id });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int id)
        {
            var foundSubject = await _context.Set<Subject>().Include(i => i.Students).FirstOrDefaultAsync(subject => subject.Id == id);
            if (foundSubject == null)
            {
                return NotFound("The subject with the provided id was not found");
            }

            return Ok(foundSubject);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllSubjects()
        {
            var foundSubjects = await _context.Set<Subject>().Include(i => i.Students).ToListAsync();
            return Ok(foundSubjects);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(SubjectDTO subjectDTO, int id)
        {
            var foundSubject = await _context.Set<Subject>().Include(i => i.Students).FirstOrDefaultAsync(subject => subject.Id == id);
            if (foundSubject == null)
            {
                return NotFound("The subject with the provided id was not found");
            }

            foundSubject.MinimumMark = subjectDTO.MinimumMark;
            foundSubject.Name = subjectDTO.Name;
            foundSubject.Hours = subjectDTO.Hours;
            foundSubject.ExamCount = subjectDTO.ExamCount;

            _context.Set<Subject>().Update(foundSubject);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjectById(int id)
        {
            var subject = await _context.Set<Subject>().FirstOrDefaultAsync(subject => subject.Id == id);
            if (subject == null) return BadRequest("The subject with the provided id, could not be found");

            _context.Set<Subject>().Remove(subject);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
