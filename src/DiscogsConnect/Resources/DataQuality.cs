﻿using System.Runtime.Serialization;

namespace DiscogsConnect
{
    public enum DataQuality
    {
        [EnumMember(Value = "Needs Vote")]
        NeedsVote,

        [EnumMember(Value = "Correct")]
        Correct,

        [EnumMember(Value = "Complete and Correct")]
        CompleteAndCorrect
    }
}