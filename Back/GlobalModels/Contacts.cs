namespace GlobalModels;

public class Contact
{
    public string Link { get; set; }
    
    public TypeOfContacts TypeOfContact { get; set; }
    
    public string DisplayName { get; set; }
    
    public bool IsVerified { get; set; }
}