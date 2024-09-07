﻿using ErrorOr;

namespace DomeGym.Domain;

public static class GymErrors
{
    public static readonly Error CannotHaveMoreRoomsThanSubscriptionAllows =
        Error.Validation(code: "Gym.CannotHaveMoreRoomsThanSubscriptionAllows",
            "A gym cannot have more rooms than the subscription allows");
}
