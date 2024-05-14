using Microsoft.AspNetCore.Mvc;
using MyUniverse.Dtos;
using MyUniverse.Models;
using MyUniverse.Services.Student;

namespace MyUniverse.Controllers
{
    public class StudentController : BaseApiController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Register student
        /// </summary>
        /// <param name="newStudent">New student model</param>
        /// <returns>Student model</returns>
        [HttpPost("register-student")]
        public async Task<ActionResult> RegisterStudent([FromBody]StudentModel newStudent)
        {
            return Ok(await _studentService.Register(newStudent));
        }

        /// <summary>
        /// Get student Course
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <returns>List of courses</returns>
        [HttpGet("student/{studentId}/courses")]
        public async Task<ActionResult> GetStudentCourses(int studentId)
        {
            return Ok(await _studentService.ShowCourses(studentId));
        }

        /// <summary>
        /// Get one student course
        /// </summary>
        /// <param name="courseId">Course id</param>
        /// <param name="studentId">Student id</param>
        /// <returns>Single course model</returns>
        [HttpGet("student/{studentId}/courses/{courseId}")]
        public async Task<ActionResult> GetStudentCourse(int courseId, int studentId)
        {
            return Ok(await _studentService.ShowCourse(courseId, studentId));
        }

        /// <summary>
        /// Update student profile
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <param name="updatedStudent">Updated data of student</param>
        /// <returns>Result of operation as a string</returns>
        [HttpPut("student/{studentId}")]
        public async Task<ActionResult> UpdateStudentProfile(int studentId, [FromBody]UpdateDto updatedStudent)
        {
            return Ok(await _studentService.UpdateProfile(studentId, updatedStudent));
        }

        /// <summary>
        /// Create a question for teacher
        /// </summary>
        /// <param name="studentId">student id</param>
        /// <param name="receiverId">receiver id(teacher)</param>
        /// <param name="question">question as a string</param>
        /// <returns>Question answer model</returns>
        [HttpPost("student/{studentId}/ask-question/{receiverId}")]
        public async Task<ActionResult> AskQuestion(int studentId, int receiverId, [FromBody]string question)
        {
            var result = await _studentService.AskQuestion(studentId, receiverId, question);

            if (result.Item1 != null)
            {
                return NotFound(result.Item1);
            }

            return Ok(result.Item2);
        }

        /// <summary>
        /// Student answer for a question
        /// </summary>
        /// <param name="questionId">Question id</param>
        /// <param name="answer">Answer as a string</param>
        /// <returns>String as a result of a operation</returns>
        [HttpPatch("student/answer-question/{questionId}")]
        public async Task<ActionResult> AnswerQuestion(int questionId, [FromBody]string answer) 
        {
            var result = await _studentService.AnswerQuestion(questionId, answer);
            if (result.Item1 != null)
            {
                return NotFound(result.Item1);
            }
            return Ok(result.Item2);
        }

        /// <summary>
        /// Get all questions
        /// </summary>
        /// <returns>List of QA models</returns>
        [HttpGet("student/questions")]
        public async Task<ActionResult> GetAllQuestions()
        {
            return Ok(await _studentService.GetAllQuestions());
        }

        /// <summary>
        /// Download files
        /// </summary>
        /// <returns>Result of a operation as a string</returns>
        [HttpGet("student/download-files")]
        public async Task<ActionResult> DownloadFiles()
        {
            return Ok(await _studentService.DownloadFiles());
        }
    }
}
