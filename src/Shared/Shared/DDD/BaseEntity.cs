
namespace Shared.DDD;

public abstract class BaseEntity<T> : IEntity<T>
{
    public T Id { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
}
