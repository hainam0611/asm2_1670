namespace ASM2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book>? Book { get; set; }
    }
}
