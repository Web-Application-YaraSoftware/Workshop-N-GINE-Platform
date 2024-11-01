namespace YARA.WorkshopNGine.API.CommunicationManagement.Interfaces.REST.Resources;

public record NotificationResource(int Id, string Content, int UserId, int StateId, string Endpoints );