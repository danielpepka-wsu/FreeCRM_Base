namespace FreeLLM;

public partial class DataObjects
{
    public enum SignalRUpdateType
    {
        Department,
        DepartmentGroup,
        File,
        Language,
        LastAccessTime,
        Setting,
        Tenant,
        UDF,
        Undelete,
        Unknown,
        User,
        UserAttendance,
        UserGroup,
        UserPreferences,
    }

    public partial class SignalRUpdate
    {
        public Guid? TenantId { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? UserId { get; set; }
        public string? UserDisplayName { get; set; }
        public SignalRUpdateType UpdateType { get; set; }
        public string Message { get; set; } = "";
        public object? Object { get; set; }
        public string? ObjectAsString { get; set; }
    }
}
