namespace YARA.WorkshopNGine.API.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}