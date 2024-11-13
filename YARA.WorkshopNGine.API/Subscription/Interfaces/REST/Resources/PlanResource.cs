namespace YARA.WorkshopNGine.API.Subscription.Interfaces.REST.Resources;

public record PlanResource(long Id, decimal Price, int DurationInMonths, string Type, string Cycle, int MaxMechanics, int MaxClients, int MaxActiveInterventions, int MaxTasksPerMechanic, int MaxItems, bool MetricsAvailable);