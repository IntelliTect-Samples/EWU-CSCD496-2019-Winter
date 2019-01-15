using System;
using System.Collections.Generic;

namespace SecretSanta.Domain
{
    public class User
    {
        string FirstName { set; get; }
        string LastName { set; get; }
        List<Gift> UserGifts { set; get; }
        List<Group> UserGroups { set; get; }

        public User(string firstName, string lastName, Gift gift = null, Group group = null)
        {
            FirstName = firstName;
            LastName = lastName;
            UserGifts = new List<Gift>();
            UserGroups = new List<Group>();

            if (gift != null)
                UserGifts.Add(gift);
            if (group != null)
                UserGroups.Add(group);
        }

        private void AddUser()
        {

        }

        private void UpdateUser()
        {

        }

    }

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

        private void CreateGift(User theUser)
        {

        }

        private void EditGift(User theUser)
        {

        }

        private void DeleteGift(User theUser)
        {

        }
    }

    public class Group
    {
        string Title { set; get; }
        List<User> Members { set; get; }

        public Group(string title)
        {
            Title = title;
            Members = new List<User>();
        }

        private void CreateGroup()
        {

        }

        private void AddUser()
        {

        }

        private void RemoveUser()
        {

        }
    }

    public class Pairing
    {
        User Recipient { set; get; }
        User Santa { set; get; }

        public Pairing(User recipient, User santa)
        {
            Recipient = recipient;
            Santa = santa;
        }

        public Pairing CreatePairing(Group group)
        {
            //associate recipient to santa without uniqueness validation

            return new Pairing(null, null);
        }
    }

    public class Message
    {
        List<string> Messages { get; }

        public Message(string message)
        {

        }

        public void AddMessage(string message, string id)
        {
            string id; //pin to start of each message - id either "Santa" or "Recipient"
        }

    }
}
