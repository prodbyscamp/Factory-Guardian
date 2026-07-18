using FactoryGuardian.Models;

namespace FactoryGuardian.Repositories;

public class MaintenanceRepository
{
    private readonly List<MaintenanceTask> _tasks = new();


    public List<MaintenanceTask> GetAll()
    {
        return _tasks;
    }


    public void Add(MaintenanceTask task)
    {
        _tasks.Add(task);
    }


    public void Update(MaintenanceTask task)
    {
        var old = _tasks.FirstOrDefault(x => x.Id == task.Id);

        if (old != null)
        {
            old.Status = task.Status;
            old.Priority = task.Priority;
        }
    }
}