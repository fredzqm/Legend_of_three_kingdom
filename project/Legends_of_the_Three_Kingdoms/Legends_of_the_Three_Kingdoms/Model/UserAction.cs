namespace LOTK.Model
{
    public struct UserAction
    {
        public const int YES = 1;
        public const int NO = 0;


        public UserActionType type { get; set; }

        public int detail;

        public UserAction(UserActionType t, int v)
        {
            type = t;
            detail = v;
        }
    }

    public enum UserActionType
    {
        YES_OR_NO, // 1 or 0
        CARD, // CardID
        PLAYER // PlayerID
    }
}