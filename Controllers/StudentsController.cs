using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Management_API.Models;

namespace Student_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        StudentContext context;
        public StudentsController(StudentContext _context)
        {
            context = _context;
        }

        //Add a new student
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        //Retrieve all students
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await context.Students.ToListAsync();
            return Ok(students);
        }

        //Retrieve details of a specific student by their ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await context.Students.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
            if (student == null) 
                return NotFound();
            return Ok(student);
        }

        //Update an existing student's data
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            if (id != updatedStudent.Id)
                return BadRequest();

            context.Entry(updatedStudent).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Students.Any(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        //delete a student by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null || student.IsDeleted) return NotFound();
            student.IsDeleted = true;
            context.Students.Update(student);
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
