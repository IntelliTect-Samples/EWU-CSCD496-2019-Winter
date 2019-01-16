namespace SecretSanta.Domain
{
    public class Pairing
    {
        User Recipient { set; get; }
        User Santa { set; get; }

        public Pairing(User recipient, User santa)
        {
            Recipient = recipient;
            Santa = santa;
        }


    }
}