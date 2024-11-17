namespace YARA.WorkshopNGine.API.Subscription.Domain.Model.ValueObjects;

public class Limitations
{
    public int MaxMechanics { get; private set; }
    public int MaxClients { get; private set; }
    public int MaxActiveInterventions { get; private set; } 
    public int MaxTasksPerMechanic { get; private set; } 
    public int MaxItems { get; private set; }
    public bool MetricsAvailable { get; private set; } 
    
    public Limitations(int maxMechanics, int maxClients, int maxActiveInterventions, int maxTasksPerMechanic, int maxItems, bool metricsAvailable)
    {
        MaxMechanics = maxMechanics;
        MaxClients = maxClients;
        MaxActiveInterventions = maxActiveInterventions;
        MaxTasksPerMechanic = maxTasksPerMechanic;
        MaxItems = maxItems;
        MetricsAvailable = metricsAvailable;
    }
}