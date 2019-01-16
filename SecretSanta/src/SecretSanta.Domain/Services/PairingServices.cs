namespace SecretSanta.Domain
{
    public class PairingService
    {
        public Pairing CreatePairing(Group group)
        {
            //associate recipient to santa without uniqueness validation

            return new Pairing(null, null);
        }
    }
}