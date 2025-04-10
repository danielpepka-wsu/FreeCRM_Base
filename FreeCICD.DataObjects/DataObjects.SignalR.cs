using System.Runtime.CompilerServices;

namespace FreeCICD;

public partial class DataObjects
{
    /// <summary>
    ///  fake enum so that we can use partial definitions... 
    ///  im not certain i like this but gona try it out
    /// </summary>
    public enum SignalRUpdateType
    {
        File ,
        Language ,
        LastAccessTime ,
        Setting ,
        Tenant ,
        User ,
        UserAttendance ,
        UserPreferences ,

        //mystuff
        RegisterSignalR ,
        LoadingDevOpsInfoStatusUpdate ,
        Unknown

        }

    public partial class SignalRUpdate
    {
        public Guid? TenantId { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? UserId { get; set; }
        public string? UserDisplayName { get; set; }
        public SignalRUpdateType UpdateType { get; set; } = SignalRUpdateType.Unknown;
        public string Message { get; set; } = "";
        public object? Object { get; set; }
        public string? ObjectAsString { get; set; }
    }
}