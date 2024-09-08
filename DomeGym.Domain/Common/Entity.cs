namespace DomeGym.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        return Id == ((Entity)obj).Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
