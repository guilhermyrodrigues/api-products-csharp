public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    // Constructor
    public User(int id, string name, string email, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }

    // Method to display user information
    public void DisplayInfo()
    {
        Console.WriteLine($"ID: {Id}, Name: {Name}, Email: {Email}");
    }
}