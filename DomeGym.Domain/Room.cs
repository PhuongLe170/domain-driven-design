namespace DomeGym.Domain;

public class Room
{
    private readonly List<Guid> _sessionIds = new();
    private readonly Guid _gymId;
}
