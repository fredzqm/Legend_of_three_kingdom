namespace LOTK.Model
{
    public struct UserAction
    {
        public UserActionType type { get; }
        int detail;
    }

    public enum UserActionType
    {
        YES_OR_NO, // 1 or 0
        CARD, // CardID
        PLAYER // PlayerID
    }
}