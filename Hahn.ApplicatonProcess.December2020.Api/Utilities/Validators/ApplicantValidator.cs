using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Api.Services;
using Hahn.ApplicatonProcess.December2020.Data.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Api.Utilities.Validators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(applicant => applicant.Name).NotEmpty().MinimumLength(5);           //at least 5 Characters
            RuleFor(applicant => applicant.FamilyName).NotEmpty().MinimumLength(5);     //at least 5 Characters
            RuleFor(applicant => applicant.Address).NotEmpty().MinimumLength(10);       //at least 10 Characters
            RuleFor(applicant => applicant.CountryOfOrigin).NotEmpty();                 //must be a valid Country
            RuleFor(applicant => applicant.EmailAddress).NotEmpty().EmailAddress();     //must be an valid EMail
            RuleFor(applicant => applicant.Age).NotEmpty().InclusiveBetween(20, 60);    //must be between 20 and 60
            RuleFor(applicant => applicant.CountryOfOrigin).MustAsync(CheckIfCountryValid).WithMessage("Invalid CountryOfOrigin");
            RuleFor(applicant => applicant.Hired).Must(x => x == false || x == true);   //If provided should not be null

        }
      
        public async Task<bool> CheckIfCountryValid(string countryTxt, CancellationToken cancellationToken)
        {
            var countriesResponse = await HahnExternalServices.GetMatchingCountries(countryTxt, cancellationToken);
            return countriesResponse.IsSuccessStatusCode;
        }
    }

}
