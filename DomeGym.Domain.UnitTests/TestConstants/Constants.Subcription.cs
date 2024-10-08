﻿using DomeGym.Domain.SubscriptionAggragate;

namespace DomeGym.Domain.UnitTests.TestConstants;

public static partial class Constants
{
    public static class Subscription
    {
        public static readonly SubscriptionType DefaultSubscriptionType = SubscriptionType.Free;
        public static readonly Guid Id = Guid.NewGuid();
        public const int MaxDailySessionsFreeTier = 4;
        public const int MaxRoomsFreeTier = 1;
        public const int MaxGymFreeTier = 1;
    }
}
