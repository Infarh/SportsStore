namespace SportsStore.Domain.Models.Base.Interfaces
{
    public interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}