using Hahn.ApplicatonProcess.December2020.Data.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Repository.Interfaces;
using Hahn.ApplicatonProcess.December2020.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Repository
{
    
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly IServiceScope _scope;
        private readonly ApplicantDbContext _databaseContext;

        public ApplicantRepository(IServiceProvider services)
        {
            _scope = services.CreateScope();

            _databaseContext = _scope.ServiceProvider.GetRequiredService<ApplicantDbContext>();
        }

        public async Task<bool> Create(Applicant applicant)
        {
            var success = false;

            _databaseContext.Applicant.Add(applicant);

            var numberOfApplicantsCreated = await _databaseContext.SaveChangesAsync();

            if (numberOfApplicantsCreated == 1)
                success = true;

            return success;
        }

        public async Task<bool> Update(Applicant applicant)
        {
            var success = false;

            var existingApplicant = Get(applicant.Id);

            if (existingApplicant != null)
            {
                existingApplicant.Name = applicant.Name;
                existingApplicant.FamilyName = applicant.FamilyName;
                existingApplicant.Address = applicant.Address;
                existingApplicant.CountryOfOrigin = applicant.CountryOfOrigin;
                existingApplicant.EmailAddress = applicant.EmailAddress;
                existingApplicant.Age = applicant.Age;
                existingApplicant.Hired = applicant.Hired;

                _databaseContext.Applicant.Attach(existingApplicant);

                var numberOfApplicantsCreated = await _databaseContext.SaveChangesAsync();

                if (numberOfApplicantsCreated == 1)
                    success = true;
            }

            return success;
        }

        public Applicant Get(int applicantId)
        {
            var result = _databaseContext.Applicant
                                .Where(x => x.Id == applicantId)
                                .FirstOrDefault();

            return result;
        }

        public IOrderedQueryable<Applicant> GetAll()
        {
            var result = _databaseContext.Applicant
                                .OrderByDescending(x => x.Id);

            return result;
        }


        public async Task<bool> Delete(int applicantId)
        {
            var success = false;

            var existingApplicant = Get(applicantId);

            if (existingApplicant != null)
            {
                _databaseContext.Applicant.Remove(existingApplicant);

                var numberOfApplicantsDeleted = await _databaseContext.SaveChangesAsync();

                if (numberOfApplicantsDeleted == 1)
                    success = true;
            }

            return success;
        }

        public Applicant Search(string keyword)
        {
            var result = _databaseContext.Applicant
                               .Where(x => keyword.Contains(x.Name) || keyword.Contains(x.FamilyName))
                               .FirstOrDefault();

            return result;
        }
    }
}
