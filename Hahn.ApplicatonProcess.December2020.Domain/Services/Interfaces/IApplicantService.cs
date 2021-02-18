using Hahn.ApplicatonProcess.December2020.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services.Interfaces
{
    public interface IApplicantService
    {
        public Task<Applicant> Create(Applicant applicant);

        public Task<Applicant> Update(Applicant applicant);

        Applicant Get(int applicantId);

        IOrderedQueryable<Applicant> GetAll();

        Task<bool> Delete(int Id);

        Applicant Search(string keyword);
    }
}
