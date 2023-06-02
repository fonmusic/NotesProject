namespace NotesApi.Models;

public class Note
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }    
}
