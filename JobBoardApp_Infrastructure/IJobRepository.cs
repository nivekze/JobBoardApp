using System;
using System.Collections.Generic;
using System.Text;

using JobBoardApp_Models;
using JobBoardApp_Models.DTO;

namespace JobBoardApp_Infrastructure
{
    public interface IJobRepository : IRepository<Job>, IPagination<Job>
    {
        void CreateJob(JobDTO job);
        void EditJob(int id, JobDTO job);
        void DeleteJob(int id);
    }
}
