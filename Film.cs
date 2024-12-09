namespace BazyDanych4g1;

public class Film
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}