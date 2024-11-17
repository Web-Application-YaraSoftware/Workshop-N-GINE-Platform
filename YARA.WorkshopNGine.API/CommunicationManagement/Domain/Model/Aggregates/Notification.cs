using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Entity;
using YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.ValueoObjects;

namespace YARA.WorkshopNGine.API.CommunicationManagement.Domain.Model.Aggregates;

public partial class Notification
{
    public long Id { get; private set; }
    public DateTime Date { get; private set; }
    public string Content { get; private set; }
    public long UserId { get; private set; }
    public NotificationState State { get; private set; }
    public long StateId { get; private set; }
    public string Endpoints { get; private set; }
    
    public Notification() { }
    
    public Notification(string content, int userId, string endpoint)
    {
        Date = DateTime.Now;
        Content = content;
        UserId = userId;
        StateId = 1;
        Endpoints = endpoint;
        
    }
    
}