using SQLite;

namespace ContactSyncApp.Model;

public class Contact
{
    [PrimaryKey, AutoIncrement]
    public int ContactId { get; set; }

    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}

