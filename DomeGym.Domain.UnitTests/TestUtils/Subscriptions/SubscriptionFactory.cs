using Constants = DomeGym.Domain.UnitTests.TestConstants.Constants;

namespace DomeGym.Domain.UnitTests.TestUtils.Subscriptions;

public static class SubscriptionFactory
{
    public static Subscription CreateSubscription(
        SubscriptionType? subscriptionType = null,
        Guid? adminId = null,
        Guid? id = null)
    => new Subscription(
        subscriptionType: subscriptionType ?? Constants.Subscription.DefaultSubscriptionType,
        adminId: adminId ?? Constants.Admin.Id,
        id: id ?? Constants.Subscription.Id);
}
