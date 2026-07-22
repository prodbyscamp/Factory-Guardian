using FactoryGuardian.Models;
using FactoryGuardian.Repositories;

namespace FactoryGuardian.Services
{
    public class MaintenanceService
    {
        private readonly MaintenanceRepository _repository;


        public MaintenanceService()
        {
            _repository = new MaintenanceRepository();
        }



        // 정비 작업 생성
        public void CreateTask(MaintenanceTask task)
        {
            task.Status = "Created";

            _repository.Add(task);
        }



        // 작업 시작
        public void StartTask(MaintenanceTask task)
        {
            task.Status = "InProgress";

            task.StartedDate = DateTime.Now;

            _repository.Update(task);
        }



        // 작업 완료
        public void CompleteTask(MaintenanceTask task)
        {
            task.Status = "Completed";

            task.CompletedDate = DateTime.Now;

            _repository.Update(task);
        }



        // 전체 작업 조회
        public List<MaintenanceTask> GetTasks()
        {
            return _repository.GetAll();
        }
    }
}