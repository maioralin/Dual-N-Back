using System;
using System.Collections.Generic;
using System.Text;

namespace DualNBack
{
    public static class GlobalVars
    {
        public const int DEFAULT_SCORE = 2;
        public const int DEFAULT_STREAK = 0;
        public const int MAX_STREAK = 2;
        public static int BestScore { get; set; }
        public static int CurrentLevel { get; set; }
        public static int RelegationStreak { get; set; }
    }
}
