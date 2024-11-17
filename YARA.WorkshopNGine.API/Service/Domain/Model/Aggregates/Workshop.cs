using YARA.WorkshopNGine.API.Service.Domain.Model.Commands;

namespace YARA.WorkshopNGine.API.Service.Domain.Model.Aggregates;

public class Workshop
{
    public long Id { get;}
    
    public string Name { get; private set; }

    public Workshop()
    {
        this.Name = string.Empty;
    }
    
    public Workshop(string name)
    {
        this.Name = name;
    }
    
    public Workshop(CreateWorkshopCommand command)
    {
        this.Name = command.Name;
    }
    
    public void UpdateWorkshopInformation(UpdateWorkshopCommand command)
    {
        this.Name = command.Name;
    }
}