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
    public class StudentsController : ControllerBase
    {
        private readonly SchoolGradeSystemDbContext _context;
        public StudentsController(SchoolGradeSystemDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(StudentDTO studentDTO)
        {
          
            //creating the student
            Student student = new Student();
            student.FirstName = studentDTO.FirstName;
            student.LastName = studentDTO.LastName;
            student.Gender = studentDTO.Gender;
            student.Age = studentDTO.Age;

            //1. get the grade with the grade number from our database
            // == will check if it equals; != will check if it is NOT equal; 
            var gradeFromDatabase = _context.Set<Grade>().Where(g => g.Number == studentDTO.GradeNumber).FirstOrDefault();

            //if we couldnt find the grade in our database then we need to tell the customer that we couldnt find it
            if (gradeFromDatabase == null) {
                return BadRequest("Grade was not found!");
            }

            //3. attach the found grade's id to our student
            student.GradeId = gradeFromDatabase.Id;

            //database go and write my student in the table
            _context.Add(student);
            //save everything we just did!
            await _context.SaveChangesAsync();

            return Ok(new { StudentId = student.Id, GradeId = student.GradeId });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var foundStudent = await _context.Set<Student>().Include(i => i.Grade).FirstOrDefaultAsync(student => student.Id == id);
            if (foundStudent == null) {
                return NotFound("The student with the provided id was not found");
            }

            StudentViewModel studentViewModel = new StudentViewModel();
            studentViewModel.Id = foundStudent.Id;
            studentViewModel.FirstName = foundStudent.FirstName;
            studentViewModel.LastName = foundStudent.LastName;
            studentViewModel.Gender = foundStudent.Gender;
            studentViewModel.Age = foundStudent.Age;
            
            studentViewModel.GradeId = foundStudent.GradeId;
            if(foundStudent.Grade != null)
            {
                studentViewModel.GradeNumber = foundStudent.Grade.Number;
            }


            return Ok(studentViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            List<Student> studentsList = await _context.Set<Student>().ToListAsync();
            return Ok(studentsList);
        }

        [HttpPut("{studentId}")]
        public async Task<IActionResult> UpdateStudent(int studentId, string lastName, int gradeId)
        {
            var foundStudent = await _context.Set<Student>().Include(i => i.Grade).FirstOrDefaultAsync(student => student.Id == studentId);
            if (foundStudent == null)
            {
                return NotFound("The student with the provided id was not found");
            }

            // locally changed
            foundStudent.LastName = lastName;
            foundStudent.GradeId = gradeId;

            // saving our changes to the database now!
            _context.Update(foundStudent);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentById(int id)
        {
            var student = await _context.Set<Student>().FirstOrDefaultAsync(student => student.Id == id);
            if (student == null) return BadRequest("The student with the provided id, could not be found");

            _context.Set<Student>().Remove(student);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
