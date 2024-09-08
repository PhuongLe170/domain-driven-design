using DomeGym.Domain.GymAggregate;
using Constants = DomeGym.Domain.UnitTests.TestConstants.Constants;

namespace DomeGym.Domain.UnitTests.TestUtils.Gyms;

public static class GymFactory
{
    public static Gym CreateGym(
        int maxRoom = Constants.Subscription.MaxRoomsFreeTier,
        Guid? subscriptionId = null,
        Guid? id = null)
    => new Gym(
        maxRooms: maxRoom,
        subscriptionId: subscriptionId ?? Constants.Subscription.Id,
        id: id?? Constants.Gym.Id);
}
