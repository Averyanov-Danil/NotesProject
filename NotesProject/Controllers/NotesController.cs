using Microsoft.AspNetCore.Mvc;

namespace NotesProject.Controllers
{
    // Контроллер для обработки запросов связанных с заметками
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetAllNotes()
        {
            var notes = await _noteService.GetAllNotes();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNoteById(int id)
        {
            var note = await _noteService.GetNoteById(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpPost("AddNote/")]
        public async Task<ActionResult<int>> CreateNote([FromBody] Note note)
        {
            var id = await _noteService.CreateNote(note);
            return CreatedAtAction(nameof(GetNoteById), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNote(int id, [FromBody] Note note)
        {
            var success = await _noteService.UpdateNote(id, note);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNote(int id)
        {
            var success = await _noteService.DeleteNote(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
