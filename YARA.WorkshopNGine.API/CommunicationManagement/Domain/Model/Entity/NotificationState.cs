using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.ValueoObjects;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Entity;

public class NotificationState
{
    public long Id { get; set; }
    public ENotificationState Name { get; set; }
    public ICollection<Notification> Notifications { get;}
    
    public NotificationState(){}
    public NotificationState(ENotificationState name)
    {
        Name = name;
    }
    public string GetStringName()
    {
        return Name.ToString();
    }
    
    public static NotificationState ToNotificationStateFromName(string name)
    {
        if (Enum.TryParse(name, out ENotificationState state)) return new NotificationState(state);
        throw new ArgumentException("Invalid state name");
    }
    
    public static NotificationState ToNotificationStateFromId(int id)
    {
        if (Enum.TryParse(id.ToString(), out ENotificationState state)) return new NotificationState(state);
        throw new ArgumentException("Invalid state id");
    }
    
}