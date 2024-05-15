using Microsoft.AspNetCore.Mvc;
using MyUniverse.Dtos;
using MyUniverse.Models;
using MyUniverse.Services.Teacher;

namespace MyUniverse.Controllers
{
    public class TeacherController : BaseApiController
    {
        private readonly TeacherService _teacherService;

        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost("register-teacher")]
        public async Task<ActionResult> RegisterTeacher([FromBody]TeacherModel newTeacher)
        {
            return Ok(await _teacherService.Register(newTeacher));
        }

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

        [HttpPost("teachers/{teacherId}")]
        public async Task<ActionResult> UpdateTeacherProfile(int teacherId, [FromBody]UpdateDto updateDto)
        {
            return Ok(await _teacherService.UpdateProfile(teacherId, updateDto));
        }

        [HttpGet("teachers/download-files")]
        public async Task<ActionResult> DownloadFiles()
        {
            return Ok(await _teacherService.DownloadFiles());
        }
    }
}
