using Hahn.ApplicatonProcess.December2020.Data.Models;
using Hahn.ApplicatonProcess.December2020.Domain.Repository.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services
{
   public  class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _repository;

        public ApplicantService(IApplicantRepository repository)
        {
            _repository = repository;
        }
        public async Task<Applicant> Create(Applicant applicant)
        {
            if (applicant.Hired == null)
                applicant.Hired = false;

            var success = await _repository.Create(applicant);

            if (success)
                return applicant;
            else
                return null;
        }

        public async  Task<bool> Delete(int Id)
        {
            var success = await _repository.Delete(Id);

            return success;
        }
    

        public Applicant Get(int applicantId)
        {
            var result = _repository.Get(applicantId);

            return result;
        }

        public IOrderedQueryable<Applicant> GetAll()
        {
            var result = _repository.GetAll();

            return result;
        }

        public async Task<Applicant> Update(Applicant applicant)
        {
            if (applicant.Hired == null)
                applicant.Hired = false;

            var success = await _repository.Update(applicant);

            if (success)
                return applicant;
            else
                return null;
        }

        Applicant IApplicantService.Search(string keyword)
        {
            var result = _repository.Search(keyword);
            return result;
        }
    }

  
}
