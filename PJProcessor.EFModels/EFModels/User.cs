using System;
using System.Collections.Generic;

namespace PJProcessor.EFModels.EFModels;

public partial class User
{
    public Guid UserId { get; set; }

    public Guid TenantId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string Username { get; set; } = null!;

    public string? EmployeeId { get; set; }

    public Guid? DepartmentId { get; set; }

    public string? Title { get; set; }

    public string? Location { get; set; }

    public bool Enabled { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? LastLoginSource { get; set; }

    public bool Admin { get; set; }

    public bool CanBeScheduled { get; set; }

    public bool ManageFiles { get; set; }

    public bool ManageAppointments { get; set; }

    public string? Password { get; set; }

    public bool PreventPasswordChange { get; set; }

    public int? FailedLoginAttempts { get; set; }

    public DateTime? LastLockoutDate { get; set; }

    public string? Source { get; set; }

    public string? UDF01 { get; set; }

    public string? UDF02 { get; set; }

    public string? UDF03 { get; set; }

    public string? UDF04 { get; set; }

    public string? UDF05 { get; set; }

    public string? UDF06 { get; set; }

    public string? UDF07 { get; set; }

    public string? UDF08 { get; set; }

    public string? UDF09 { get; set; }

    public string? UDF10 { get; set; }

    public DateTime Added { get; set; }

    public string? AddedBy { get; set; }

    public DateTime LastModified { get; set; }

    public string? LastModifiedBy { get; set; }

    public bool Deleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? Preferences { get; set; }


    public virtual Department? Department { get; set; }

    public virtual ICollection<FileStorage> FileStorages { get; set; } = new List<FileStorage>();



    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<UserInGroup> UserInGroups { get; set; } = new List<UserInGroup>();
}
