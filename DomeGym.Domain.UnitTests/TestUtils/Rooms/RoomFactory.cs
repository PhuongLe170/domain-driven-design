using DomeGym.Domain.RoomAggregate;
using Constants = DomeGym.Domain.UnitTests.TestConstants.Constants;

namespace DomeGym.Domain.UnitTests.TestUtils.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(
        int? maxDailySessions = null,
        Guid? gymId = null,
        Guid? id = null)
        => new Room(
            maxDailySessions: maxDailySessions ?? Constants.Room.MaxDailySessions,
            gymId: gymId ?? Constants.Gym.Id,
            id: id ?? Constants.Room.Id);
}
