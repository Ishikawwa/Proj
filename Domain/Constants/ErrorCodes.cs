namespace Domain.Constants
{
    public static class ErrorCodes
    {
        public static string UserNotFound => "UserNotFound";
        public static string InstitutionNotFound => "InstitutionNotFound";
        public static string UserIsBanned => "UserIsBanned";
        public static string UserIsMuted => "UserIsMuted";
        public static string ReviewIsBanned => "ReviewIsBanned";
        public static string ReviewNotFound => "ReviewNotFound";
        public static string NullReviewScore => "NullReviewScore";
        public static string OwnerNotFound => "OwnerNotFound";
        public static string OwnerAlreadyAssigned => "OwnerAlreadyAssigned";
    }
}
