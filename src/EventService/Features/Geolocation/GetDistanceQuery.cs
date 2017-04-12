using MediatR;
using EventService.Data;
using EventService.Features.Core;
using System;
using System.Threading.Tasks;
using System.Device.Location;

namespace EventService.Features.Geolocation
{
    public class GetDistanceQuery
    {
        public class Point
        {
            public double Longitude { get; set; }
            public double Latitude { get; set; }
        }

        public class GetDistanceRequest : IRequest<GetDistanceResponse>
        {
            public Guid TenantUniqueId { get; set; }
            public Point Origin { get; set; }
            public Point Destination { get; se; }

        }

        public class GetDistanceResponse
        {
            public double DistanceInMeters { get; set; }
        }

        public class GetDistanceHandler : IAsyncRequestHandler<GetDistanceRequest, GetDistanceResponse>
        {
            public GetDistanceHandler(EventServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetDistanceResponse> Handle(GetDistanceRequest request)
            {
                var origin = new GeoCoordinate(request.Origin.Latitude, request.Origin.Latitude);
                var destination = new GeoCoordinate(request.Destination.Latitude, request.Destination.Latitude);

                return new GetDistanceResponse()
                {
                    DistanceInMeters = origin.GetDistanceTo(destination)
                };
            }

            private readonly EventServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
