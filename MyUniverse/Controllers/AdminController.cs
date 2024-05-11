using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyUniverse.Dtos;
using MyUniverse.Models;
using MyUniverse.Services.Admin;
using Swashbuckle.AspNetCore.Annotations;

namespace MyUniverse.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// Get students list
        /// </summary>
        /// <returns>List of students</returns>
        [HttpGet("students")]
        [SwaggerOperation(Summary = "Get students list")]
        public async Task<ActionResult> GetStudentList()
        {
            return Ok(await _adminService.GetStudentList());
        }

        /// <summary>
        /// Get teachers list
        /// </summary>
        /// <returns>List of teachers</returns>
        [HttpGet("teachers")]
        public async Task<ActionResult> GetTeacherList()
        {
            return Ok(await _adminService.GetTeacherList());
        }

        /// <summary>
        /// Get Student with id
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Result of operation, the student model if success</returns>
        [HttpGet("students/{id}")]
        public async Task<ActionResult> GetStudentById(int id)
        {
            var result = await _adminService.GetStudentById(id);
            if(result.Item2 != null)
            {
                return Ok(result.Item2);
            }
            return NotFound(result.Item1);
        }

        /// <summary>
        /// Get Teacher with id
        /// </summary>
        /// <param name="id">Teacher id</param>
        /// <returns>Result of operation, the teacher model if success</returns>
        [HttpGet("teachers/{id}")]
        public async Task<ActionResult> GetTeacherById(int id)
        {
            var result = await _adminService.GetTeacherById(id);

            if (result.Item2 != null)
            {
                return Ok(result.Item2);
            }
            return NotFound(result.Item1);
        }

        /// <summary>
        /// Create new student
        /// </summary>
        /// <param name="newStudent">Student propeties</param>
        /// <returns>Result of the operation as the string</returns>
        [HttpPost("students")]
        public async Task<ActionResult> AddStudent([FromBody]StudentModel newStudent)
        {
            return Ok(await _adminService.AddStudent(newStudent));
        }

        /// <summary>
        /// Create new teacher
        /// </summary>
        /// <param name="newTeacher">Teacher properties</param>
        /// <returns>Result of the operation as the string</returns>
        [HttpPost("teachers")]
        public async Task<ActionResult> AddTeacher([FromBody]TeacherModel newTeacher)
        {
            return Ok(await _adminService.AddTeacher(newTeacher));
        }

        /// <summary>
        /// Update Student
        /// </summary>
        /// <param name="id">Student id</param>
        /// <param name="newStudent">Updated Student properties</param>
        /// <returns>Result of the operation, model if success</returns>
        [HttpPut("students/{id}")]
        public async Task<ActionResult> UpdateStudent(int id, [FromBody]UpdateDto newStudent)
        {
            var updatedStudent = await _adminService.UpdateStudent(id, newStudent);
            if (updatedStudent != null)
            {
                return Ok(updatedStudent);
            }

            return NotFound($"Student with id: {id} does not exist");
        }

        /// <summary>
        /// Update Teacher properties
        /// </summary>
        /// <param name="id">Teacher id</param>
        /// <param name="newTeacher">Updated Teacher properties</param>
        /// <returns>Result of the operation, model if success</returns>
        [HttpPut("teachers/{id}")]
        public async Task<ActionResult> UpdateTeacher(int id, [FromBody]UpdateDto newTeacher)
        {
            var updatedTeacher = await _adminService.UpdateTeacher(id, newTeacher);
            if (updatedTeacher != null)
            {
                return Ok(updatedTeacher);
            }
            return NotFound($"Teaher with id: {id} does not exist");
        }

        /// <summary>
        /// Delete student
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Result of the operaiton as string</returns>
        [HttpDelete("students/{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            return Ok(await _adminService.DeleteStudent(id));
        }

        /// <summary>
        /// Delete teacher
        /// </summary>
        /// <param name="id">Teacher Id</param>
        /// <returns>Result of the operaiton as string</returns>
        [HttpDelete("teachers/{id}")]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            return Ok(await _adminService.DeleteTeacher(id));
        }

        /// <summary>
        /// Get list of the courses
        /// </summary>
        /// <returns>List of the courses</returns>
        [HttpGet("courses")]
        public async Task<ActionResult> GetCourses()
        {
            return Ok(await _adminService.GetCourseList());
        }

        /// <summary>
        /// Get course by id
        /// </summary>
        /// <param name="id">Course id</param>
        /// <returns>Result of the operaiton, model if success</returns>
        [HttpGet("courses/{id}")]
        public async Task<ActionResult> GetCourseById(int id)
        {
            var result = await _adminService.GetCourseById(id);

            if(result.Item2 == null)
            {
                return NotFound(result.Item1);
            }
            return Ok(result.Item2);
        }

        /// <summary>
        /// Create new course
        /// </summary>
        /// <param name="newCourse">New course properties</param>
        /// <returns>Result of the operaiton as string</returns>
        [HttpPost("courses")]
        public async Task<ActionResult> AddCourse([FromBody]CourseModel newCourse)
        {
            return Ok(await _adminService.AddCourse(newCourse));
        }

        /// <summary>
        /// Update course properties
        /// </summary>
        /// <param name="id">Course id</param>
        /// <param name="newCourse">Update couse properties</param>
        /// <returns>Result of the operaiton, model if success</returns>
        [HttpPut("courses/{id}")]
        public async Task<ActionResult> UpdateCourse(int id, [FromBody]CourseModel newCourse)
        {
            var updatedCourse = await _adminService.UpdateCourse(id, newCourse);
            if(updatedCourse != null)
            {
                Ok(updatedCourse);
            }
            return NotFound($"Course with id: {id} does not exist");
        }

        /// <summary>
        /// Delete course
        /// </summary>
        /// <param name="id">Course id</param>
        /// <returns>Result of the operaiton as string</returns>
        [HttpDelete("courses/{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            return Ok(await _adminService.DeleteCourse(id));
        }

        /// <summary>
        /// Assign teacher to the course
        /// </summary>
        /// <param name="courseId">Course id</param>
        /// <param name="teacherId">Teacher Id</param>
        /// <returns>Result of the operaiton as string</returns>
        [HttpPost("courses/{courseId}/teacher/{teacherId}")]
        public async Task<ActionResult> AssignTeacherToCourse(int courseId, int teacherId)
        {
            return Ok(await _adminService.AssignTeacherToCourse(courseId, teacherId));
        }

        /// <summary>
        /// Add student to the course
        /// </summary>
        /// <param name="courseId">Course id</param>
        /// <param name="studentId">Student id</param>
        /// <returns>Result of the operaiton as string</returns>
        [HttpPost("courses/{courseId}/student/{studentId}")]
        public async Task<ActionResult> AddStudentToCourse(int courseId, int studentId)
        {
            return Ok(await _adminService.AddStudentToCourse(courseId, studentId));
        }

        /// <summary>
        /// Replace teacher on the course
        /// </summary>
        /// <param name="courseId">Couse id</param>
        /// <param name="teacherId">New teacher id</param>
        /// <returns>Result of the operaiton as string</returns>
        [HttpPut("courses/{courseId}/teacher/{teacherId}")]
        public async Task<ActionResult> UpdateTeacherOnCourse(int courseId, int teacherId)
        {
            return Ok(await _adminService.UpdateTeacherOnCourse(courseId, teacherId));
        }

        /// <summary>
        /// Remove student from course
        /// </summary>
        /// <param name="courseId">Course id</param>
        /// <param name="studentId">Student id</param>
        /// <returns>Result of the operaiton as string</returns>
        [HttpDelete("courses/{courseId}/student/{studentId}")]
        public async Task<ActionResult> RemoveStudentFromCourse(int courseId, int studentId)
        {
            return Ok(await _adminService.RemoveStudentFromCourse(courseId, studentId));
        }

        /// <summary>
        /// Delete course from teacher and students
        /// </summary>
        /// <param name="courseId">Course id</param>
        /// <param name="teacherId">Teacher id</param>
        /// <returns>Result of the operaiton as string</returns>
        [HttpDelete("courses/{courseId}/teacher/{teacherId}")]
        public async Task<ActionResult> RemoveCourseFromUsers(int courseId, int teacherId)
        {
            return Ok(await _adminService.RemoveCourseFromUsers(courseId, teacherId));
        }

        /// <summary>
        /// Get Course Info, course name, teacher name, students
        /// </summary>
        /// <param name="courseId">Course id</param>
        /// <param name="teacherId">Teacher id</param>
        /// <returns>List of Response object</returns>
        [HttpGet("courses/{courseId}/teacher/{teacherId}")]
        public async Task<ActionResult> GetCourseMembers(int courseId, int teacherId)
        {
            var result = await _adminService.GetCourseGroupInfo(courseId, teacherId);
            if(result.Item2 == null)
            {
                return NotFound(result.Item1);
            }
            return Ok(result.Item2);
        }
    }
}
