using YARA.WorkshopNGine.API.Subscription.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Subscription.Domain.Services;

public interface IPlanCommandService
{
    Task Handle(SeedPlansCommand command);
}