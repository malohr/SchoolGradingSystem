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
    public class TeachersController : ControllerBase
    {
        private readonly SchoolGradeSystemDbContext _context;
        public TeachersController(SchoolGradeSystemDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(TeacherDTO teacherDTO)
        {

            //creating the teacher
            Teacher teacher = new Teacher();
            teacher.FirstName = teacherDTO.FirstName;
            teacher.LastName = teacherDTO.LastName;
            teacher.Gender = teacherDTO.Gender;
            teacher.Age = teacherDTO.Age;

            teacher.AvailableHours = teacherDTO.AvailableHours;
            teacher.IsFullTime = teacherDTO.IsFullTime;

            //1. we need to iterate or loop over the list of ids we got
            foreach (var subjectId in teacherDTO.Subjects)
            {

                //2. search for each id we have been provided with, the according subject in the database
                // to make sure it really exists!
                var foundSubject = await _context.Set<Subject>().FirstOrDefaultAsync(subject => subject.Id == subjectId);
                if (foundSubject == null) return NotFound($"The subject with the id {subjectId} is not existing in the database!");


                //3. if it exists then we are save to assign the subject to the teacher. we will then set
                // the subject to the teacher
                teacher.Subjects.Add(foundSubject);
            }
            
   
            //database go and write my teacher in the table
            _context.Add(teacher);
            //save everything we just did!
            await _context.SaveChangesAsync();

            return Ok(new { TeacherId = teacher.Id });
        }

        [HttpGet("{teacherId}")]
        public async Task<IActionResult> GetTeacherById(int teacherId)
        {
            var foundTeacher = await _context.Set<Teacher>().Include(i => i.Subjects).FirstOrDefaultAsync(teacher => teacher.Id == teacherId);
            if (foundTeacher == null) return NotFound("The teacher with the provided was not found");

            var teacherViewModel = new TeacherViewModel(foundTeacher.Id, foundTeacher.FirstName, 
                foundTeacher.LastName, foundTeacher.Age, foundTeacher.Gender, 
                foundTeacher.AvailableHours, foundTeacher.IsFullTime);

            foreach (var currentSubject in foundTeacher.Subjects)
            {
                var subjectViewModel = new SubjectViewModel(currentSubject.Id,currentSubject.Name, 
                    currentSubject.ExamCount,currentSubject.Hours, 
                    currentSubject.MinimumMark);

                teacherViewModel.Subjects.Add(subjectViewModel);
            }
            

            return Ok(teacherViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teacherList = await _context.Set<Teacher>().Include(i => i.Subjects).ToListAsync();

            var teacherViewModelList = new List<TeacherViewModel>(); 

            foreach (var teacher in teacherList)
            {
                var teacherViewModel = new TeacherViewModel(teacher.Id, teacher.FirstName,
                 teacher.LastName, teacher.Age, teacher.Gender,
                 teacher.AvailableHours, teacher.IsFullTime);

                foreach (var currentSubject in teacher.Subjects)
                {
                    var subjectViewModel = new SubjectViewModel(currentSubject.Id,currentSubject.Name,
                        currentSubject.ExamCount, currentSubject.Hours,
                        currentSubject.MinimumMark);

                    teacherViewModel.Subjects.Add(subjectViewModel);
                }

                teacherViewModelList.Add(teacherViewModel);
                  
            }

            return Ok(teacherViewModelList);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacherById(int id)
        {
            var teacher = await _context.Set<Teacher>().FirstOrDefaultAsync(teacher => teacher.Id == id);
            if (teacher == null) return BadRequest("The teacher with the provided id, could not be found");

            _context.Set<Teacher>().Remove(teacher);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut("{teacherId}")]
        public async Task<IActionResult> UpdateTeacher(int teacherId, UpdateTeacherDTO teacherDTO)
        {

            var teacher = await _context.Set<Teacher>().Include(i => i.Subjects).FirstOrDefaultAsync(teacher => teacher.Id == teacherId);
            if (teacher == null) return BadRequest("The teacher with the provided id, could not be found");

            //update the teacher
            teacher.LastName = teacherDTO.LastName;
            teacher.AvailableHours = teacherDTO.AvailableHours;
            teacher.IsFullTime = teacherDTO.IsFullTime;


            teacher.Subjects.RemoveAll(subjectFromDatabase => !teacherDTO.Subjects.Exists(subjectIdFromDto => subjectFromDatabase.Id == subjectIdFromDto));
            //1. we need to iterate or loop over the list of ids we got
            foreach (var subjectId in teacherDTO.Subjects)
            {
                //if the new subject does not exist in our subject list yet, we will add it
                if (!teacher.Subjects.Exists(sub => sub.Id == subjectId))
                {
                    // search for each id we have been provided with, the according subject in the database
                    // to make sure it really exists!
                    var foundSubject = await _context.Set<Subject>().FirstOrDefaultAsync(subject => subject.Id == subjectId);
                    if (foundSubject == null) return NotFound($"The subject with the id {subjectId} is not existing in the database!");

                    teacher.Subjects.Add(foundSubject);
                }
            }


            //database go and write my teacher in the table
            _context.Update(teacher);
            //save everything we just did!
            await _context.SaveChangesAsync();

            return Ok(new { TeacherId = teacher.Id });
        }

    }
}
