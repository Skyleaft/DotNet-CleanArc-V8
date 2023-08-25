
namespace DomainLayer.Interfaces;

public interface ISession
{
    public int UserId { get; }

    public DateTime Now { get; }
}