using System.Text.Json.Serialization;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Commands;
using YARA.WorkshopNGine.API.IAM.Domain.Model.Entities;

namespace YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;

public class User
{
    public long Id { get;}
    public string Username { get; private set; }
    [JsonIgnore] 
    public string Password { get; private set; }
    public Role Role { get; }
    public long RoleId { get; private set; }
    public long WorkshopId { get; private set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public User(SignUpCommand command, long roleId)
    {
        Username = command.Username;
        Password = command.Password;
        WorkshopId = command.WorkshopId;
        RoleId = roleId;
    }
    public User(string username, string password, long roleId, long workshopId)
    {
        Username = username;
        Password = password;
        RoleId = roleId;
        WorkshopId = workshopId;
    }
}