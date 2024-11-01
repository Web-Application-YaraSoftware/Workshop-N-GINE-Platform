using YARA.WorkshopNGine.API.IAM.Domain.Model.Aggregates;
using YARA.WorkshopNGine.API.IAM.Domain.Model.ValueObjects;

namespace YARA.WorkshopNGine.API.IAM.Domain.Model.Entities;

public class Role
{
    public long Id { get; set; }
    public Roles Name { get; set; }
    
    public Role(Roles name) { Name = name; }

    public Role() { }
    
    public ICollection<User> Users { get; }

    public string GetStringName()
    {
        return Name.ToString();
    }
    
    public static Role ToRoleFromName(string name)
    {
        if (Enum.TryParse(name, out Roles role)) return new Role(role);
        throw new ArgumentException("Invalid role name");
    }
    
    public static Role ToRoleFromId(long id)
    {
        if (Enum.TryParse(id.ToString(), out Roles role)) return new Role(role);
        throw new ArgumentException("Invalid role id");
    }
}