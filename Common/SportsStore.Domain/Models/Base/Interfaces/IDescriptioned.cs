namespace SportsStore.Domain.Models.Base.Interfaces
{
    public interface IDescriptioned : IEntity
    {
        string Description { get; set; }
    }
}