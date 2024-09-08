using DomeGym.Domain.GymAggregate;
using DomeGym.Domain.UnitTests.TestUtils.Gyms;
using DomeGym.Domain.UnitTests.TestUtils.Rooms;
using FluentAssertions;

namespace DomeGym.Domain.UnitTests.GymAggregate;

public class GymTests
{
    [Fact]
    public void AddRoom_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        var gym = GymFactory.CreateGym(maxRoom: 1);
        var room1 = RoomFactory.CreateRoom(id: Guid.NewGuid());
        var room2 = RoomFactory.CreateRoom(id: Guid.NewGuid());

        // Act
        var addRomResult1 = gym.AddRoom(room1);
        var addRomResult2 = gym.AddRoom(room2);

        // Assert
        addRomResult1.IsError.Should().BeFalse();
        addRomResult2.IsError.Should().BeTrue();

        addRomResult2.FirstError.Should().Be(GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows);

    }
}
