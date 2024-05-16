using Microsoft.AspNetCore.Mvc;
using MyUniverse.Dtos;
using MyUniverse.Models;
using MyUniverse.Services.Teacher;

namespace MyUniverse.Controllers
{
    public class TeacherController : BaseApiController
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        /// <summary>
        /// Register as a teacher
        /// </summary>
        [HttpPost("register-teacher")]
        public async Task<ActionResult> RegisterTeacher([FromBody]TeacherModel newTeacher)
        {
            return Ok(await _teacherService.Register(newTeacher));
        }

        /// <summary>
        /// Teacher answers to a question
        /// </summary>
        [HttpPatch("teachers/answer-question/{questionId}")]
        public async Task<ActionResult> AnswerQuestion(int questionId, [FromBody]string answer)
        {
            var result = await _teacherService.AnswerQuestion(questionId, answer);

            if(result.Item1 != null)
            {
                return NotFound(result.Item1);
            }

            return Ok(result.Item2);
        }

        /// <summary>
        /// Teacher asks a question from a student
        /// </summary>
        [HttpPost("teachers/{teacherId}/ask-question/{receiverId}")]
        public async Task<ActionResult> AddQuestion(int teacherId, int receiverId, [FromBody]string question)
        {
            var result = await _teacherService.AddQuestion(teacherId, receiverId, question);

            if (result.Item1 != null)
            {
                return NotFound(result.Item1);
            }

            return Ok(result.Item2);
        }

        /// <summary>
        /// Update teacher profile
        /// </summary>
        [HttpPut("teachers/{teacherId}")]
        public async Task<ActionResult> UpdateTeacherProfile(int teacherId, [FromBody]UpdateDto updateDto)
        {
            return Ok(await _teacherService.UpdateProfile(teacherId, updateDto));
        }

        /// <summary>
        /// Download files
        /// </summary>
        [HttpGet("teachers/download-files")]
        public async Task<ActionResult> DownloadFiles()
        {
            return Ok(await _teacherService.DownloadFiles());
        }

        /// <summary>
        /// Show questions for a specific teacher
        /// </summary>
        [HttpGet("teachers/questions/{teacherId}")]
        public async Task<ActionResult> GetQuestionsForTeacher(int teacherId)
        {
            return Ok(await _teacherService.GetQuestionsForTeacher(teacherId));
        }
    }
}
