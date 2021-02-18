using FluentValidation.Results;
using Hahn.ApplicatonProcess.December2020.Data.Models;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.December2020.Api.Utilities.Validators
{
    public static class ValidationExtensions
    {
        public static bool IsValid(this Applicant applicantModel, out IEnumerable<string> errors)
        {
            var validator = new ApplicantValidator();

            var validationResult = validator.Validate(applicantModel);

            errors = AggregateErrors(validationResult);

            return validationResult.IsValid;
        }

        private static List<string> AggregateErrors(ValidationResult validationResult)
        {
            var errors = new List<string>();

            if (!validationResult.IsValid)
                foreach (var error in validationResult.Errors)
                    errors.Add(error.ErrorMessage);

            return errors;
        }
    }
}
