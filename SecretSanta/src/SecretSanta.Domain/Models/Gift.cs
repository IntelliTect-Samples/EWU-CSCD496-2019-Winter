namespace SecretSanta.Domain
{
    public class Gift
    {
        string Title { set; get; }
        string URL { set; get; }
        string Description { set; get; }
        int OrderOfImportance { set; get; }
        User User { set; get; }

        public Gift(string title, string url, string description, int order, User user)
        {

        }

    }
}