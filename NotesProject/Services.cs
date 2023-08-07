using System.Collections.Concurrent;

namespace NotesProject
{
    /// <summary>
    /// Интерфейс сервиса для управления заметками
    /// </summary>
    public interface INoteService
    {
        Task<List<Note>> GetAllNotes();
        Task<Note> GetNoteById(int id);
        Task<int> CreateNote(Note note);
        Task<bool> UpdateNote(int id, Note note);
        Task<bool> DeleteNote(int id);
    }

    /// <summary>
    /// Реализация сервиса управления заметками
    /// </summary>
    public class NoteService : INoteService
    {
        private readonly ConcurrentDictionary<int, Note> _notes = new ConcurrentDictionary<int, Note>();

        /// <summary>
        /// Получить все заметки
        /// </summary>
        /// <returns></returns>
        public async Task<List<Note>> GetAllNotes()
        {
            return await Task.FromResult(_notes.Values.ToList());
        }

        /// <summary>
        /// Получить заметку по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Note> GetNoteById(int id)
        {
            _notes.TryGetValue(id, out var note);
            return await Task.FromResult(note);
        }

        /// <summary>
        /// Создать новую заметку
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public async Task<int> CreateNote(Note note)
        {
            note.Id = _notes.Count + 1;
            note.CreatedAt = DateTime.UtcNow;
            _notes[note.Id] = note;
            return await Task.FromResult(note.Id);
        }

        /// <summary>
        /// Обновить существующую заметку
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedNote"></param>
        /// <returns></returns>
        public async Task<bool> UpdateNote(int id, Note updatedNote)
        {
            if (_notes.TryGetValue(id, out var existingNote))
            {
                existingNote.Title = updatedNote.Title;
                existingNote.Content = updatedNote.Content;
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        /// <summary>
        /// Удалить заметку по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteNote(int id)
        {
            return await Task.FromResult(_notes.TryRemove(id, out _));
        }
    }
}
