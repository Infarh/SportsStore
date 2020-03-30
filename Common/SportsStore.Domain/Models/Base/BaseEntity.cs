using SportsStore.Domain.Models.Base.Interfaces;

namespace SportsStore.Domain.Models.Base
{
    public abstract class BaseEntity : IEntity
    {
        public long Id { get; set; }
    }

    public abstract class NamesEntity : BaseEntity, INamedEntity
    {
        public string Name { get; set; }
    }
}
