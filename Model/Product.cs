public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }

    // Constructor
    public Product(int id, string name, decimal price, string description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }

    // Method to display product information
    public void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}, Name: {Name}, Price: {Price:C}, Description: {Description}");
    }
}