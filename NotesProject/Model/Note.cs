using Microsoft.EntityFrameworkCore;

namespace NotesProject
{
    /// <summary>
    /// Модель данных для представления заметка
    /// </summary>
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}