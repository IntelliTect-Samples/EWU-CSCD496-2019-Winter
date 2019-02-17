namespace SecretSanta.Domain.Services.Interfaces
{
    public interface IRandom
    {
        int Next();
        int Next(int min, int max);
    }
}