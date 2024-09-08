using DomeGym.Domain.SubscriptionAggragate;
using DomeGym.Domain.UnitTests.TestUtils.Gyms;
using DomeGym.Domain.UnitTests.TestUtils.Subscriptions;
using FluentAssertions;

namespace DomeGym.Domain.UnitTests.SubscriptionAggregate;

public class SubscriptionTests
{
    [Fact]
    public void AddGym_WhenMoreThanSubscriptionAllow_ShouldFail()
    {
        // Arrange
        var subscription = SubscriptionFactory.CreateSubscription();
        var gyms = Enumerable.Range(0, subscription.GetMaxGyms() + 1)
            .Select(_ => GymFactory.CreateGym(id: Guid.NewGuid())).ToList();

        // Act
        var addGymResult = gyms.ConvertAll(subscription.AddGym);

        // Assert
        var allButLastAddGymResults = addGymResult.Take(..^1);
        allButLastAddGymResults.Should().AllSatisfy(result => result.IsError.Should().BeFalse());

        var lastAddGymResult = addGymResult.Last();
        lastAddGymResult.IsError.Should().BeTrue();
        lastAddGymResult.FirstError.Should().Be(SubscriptionErrors.CannotHaveMoreGymsThanSubscriptionAllows);
    }
}
