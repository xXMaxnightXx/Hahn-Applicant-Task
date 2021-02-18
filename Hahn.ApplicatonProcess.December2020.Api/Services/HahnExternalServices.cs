using Hahn.ApplicatonProcess.December2020.Api.Utilities;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Api.Services
{
    public class HahnExternalServices
    {

        public static async Task<HttpResponseMessage> GetMatchingCountries(string searchCountryTxt, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://restcountries.eu/rest/v2/name/" + searchCountryTxt + "?fullText=true");

            request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
            return await Http.GetHttpClientInstance().SendAsync(request, cancellationToken).ConfigureAwait(false);
           
        }
    }
}
