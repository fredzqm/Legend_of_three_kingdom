namespace LOTK.Model
{
    public struct UserAction
    {
        public UserActionType type { get; set; }
        int detail;

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