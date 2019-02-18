namespace SecretSanta.Api.ViewModels
{
    public class PairingViewModel
    {
        public int Id { get; set; }
        public int SantaId { get; set; }
        public int RecipientId { get; set; }
        public int GroupOrigin { get; set; }
    }
}