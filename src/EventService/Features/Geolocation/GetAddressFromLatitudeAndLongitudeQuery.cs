using MediatR;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;

namespace EventService.Features.Geolocation
{
    public class GetAddressFromLatitudeAndLongitudeQuery
    {
        public class GetAddressFromLatitudeAndLongitudeRequest : IRequest<GetAddressFromLatitudeAndLongitudeResponse>
        {
            public Guid TenantUniqueId { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        public class GetAddressFromLatitudeAndLongitudeResponse
        {
            public string Address { get; set; }
        }

        public class GetAddressFromLatitudeAndLongitudeHandler : IAsyncRequestHandler<GetAddressFromLatitudeAndLongitudeRequest, GetAddressFromLatitudeAndLongitudeResponse>
        {
            public GetAddressFromLatitudeAndLongitudeHandler(HttpClient client)
            {
                _client = client;
            }

            public async Task<GetAddressFromLatitudeAndLongitudeResponse> Handle(GetAddressFromLatitudeAndLongitudeRequest request)
            {
                var httpResponse = await _client.GetAsync($"http://maps.googleapis.com/maps/api/geocode/json?latlng={request.Latitude},{request.Longitude}&sensor=false");
                var googleEncodeResponse = await httpResponse.Content.ReadAsAsync<GoogleEncodeResponse>();
                var addressComponents = googleEncodeResponse.results.ElementAt(0).address_components;
                var streetAddress = addressComponents.First(x => x.types.Any(t => t == "street_number")).long_name;
                var street = addressComponents.First(x => x.types.Any(t => t == "route")).long_name;
                var city = addressComponents.First(x => x.types.Any(t => t == "locality")).long_name;

                return new GetAddressFromLatitudeAndLongitudeResponse()
                {
                    Address = $"{streetAddress} {street}, {city}"
                };
            }
            protected readonly HttpClient _client;
        }
    }
}
