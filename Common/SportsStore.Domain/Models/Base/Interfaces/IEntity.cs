namespace SportsStore.Domain.Models.Base.Interfaces
{
    public interface IEntity
    {
        long Id { get; set; }
    }

    public interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}
