namespace Contacts.Models
{
    [Serializable]
    public class Contact
    {
        public string Name { get; set; }
        public string MobilePhone { get; set; }
        public string AlternateMobilePhone { get; set; }
        public string Email { get; set; }
        public string ShortDescription { get; set; }

    }
}


