using BlazorBootstrap;
using MudBlazor;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Markup;
using static FreeCICD.DataObjects;

namespace FreeCICD.Client;

/// <summary>
/// The Model used on every page in the Blazor application to share database in the interface.
/// </summary>
public partial class BlazorDataModel
{
    private DataObjects.SignalrClientRegistration _SignalrClientRegistration = new();

    public DataObjects.SignalrClientRegistration SignalrClientRegistration {
        get { return _SignalrClientRegistration == null ? new() : _SignalrClientRegistration; }
        set {
            if (!ObjectsAreEqual(_SignalrClientRegistration?.RegistrationId, value?.RegistrationId)
                ||
                !ObjectsAreEqual(_SignalrClientRegistration?.ConnectionId, value?.ConnectionId)
                ) {
                _SignalrClientRegistration = new DataObjects.SignalrClientRegistration {
                    RegistrationId = (string.Empty + value?.RegistrationId).Trim(),
                    ConnectionId = (string.Empty + value?.ConnectionId).Trim(),
                };
                _ModelUpdated = DateTime.UtcNow;
                NotifyDataChanged();
            }
        }
    }
}