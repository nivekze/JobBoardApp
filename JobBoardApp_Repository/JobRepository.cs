using System;
using System.Collections.Generic;
using System.Text;

using JobBoardApp_Models;
using JobBoardApp_Infrastructure;
using JobBoardApp_DataAcces.Context;
using JobBoardApp_DataAccess.Repository;
using JobBoardApp_Models.DTO;
using System.Linq;

namespace JobBoardApp_Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(JobBoardAppContext context) : base(context)
        {

        }

        public void CreateJob(JobDTO job)
        {
            try
            {
                if (job.ExpiresAt.HasValue && job.ExpiresAt <= DateTime.Now)
                    throw new Exception("Expires date must be greater than today");

                var newJob = new Job();
                newJob.Title = job.Title;
                newJob.Description = job.Description;
                newJob.CreatedAt = DateTime.Now;
                newJob.ExpiresAt = job.ExpiresAt;

                Create(newJob);
            }
            catch
            {
                throw;
            }
        }

        public void DeleteJob(int id)
        {
            try
            {
                var deleteJob = GetById(id);
                if (deleteJob == null) throw new Exception("Job not found");

                Delete(deleteJob);
            }
            catch
            {
                throw;
            }
        }

        public void EditJob(int id, JobDTO job)
        {
            try
            {
                if (job.ExpiresAt.HasValue && job.ExpiresAt <= DateTime.Now)
                    throw new Exception("Expires date must be greater than today");

                var editedJob = GetById(id);
                if (editedJob == null) throw new Exception("Job not found");

                editedJob.Title = job.Title;
                editedJob.Description = job.Description;
                editedJob.ExpiresAt = job.ExpiresAt;

                Update(editedJob);
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<Job> GetPaginated(string filter, int initialPage, int pageSize, string order, out int totalRecords, out int recordsFiltered)
        {
            var data = _context.Jobs.AsQueryable();
            totalRecords = data.Count();


            if (!string.IsNullOrEmpty(filter))
            {
                data = data.Where(x => x.Title.ToUpper().Contains(filter.ToUpper()));
            }

            recordsFiltered = data.Count();

            if (order.ToUpper().Equals("ASC"))
                data = data.OrderBy(x => x.Id)
                        .ThenBy(x => x.Title)
                        .Skip((initialPage * pageSize))
                        .Take(pageSize);
            else
                data = data.OrderByDescending(x => x.Id)
                        .ThenBy(x => x.Title)
                        .Skip((initialPage * pageSize))
                        .Take(pageSize);

            return data;
        }

    }
}
