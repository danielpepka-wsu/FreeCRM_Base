namespace FreeCICD;

public partial interface IDataAccess
{
    Task SignalRUpdate(DataObjects.SignalRUpdate update);
}

public partial class DataAccess
{
    public async Task SignalRUpdate(DataObjects.SignalRUpdate update)
    {
        var baseURL = ApplicationURL;
        if (String.IsNullOrEmpty(baseURL)) {
            baseURL = String.Empty;
        }

        if (!baseURL.EndsWith("/")) { baseURL += "/"; }

        HttpClient client = Utilities.GetHttpClient(baseURL);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (update.Object != null && String.IsNullOrWhiteSpace(update.ObjectAsString)) {
            update.ObjectAsString = SerializeObject(update.Object);
        }

        if(update.UserId.HasValue && String.IsNullOrWhiteSpace(update.UserDisplayName)) {
            update.UserDisplayName = LastModifiedDisplayName(update.UserId.ToString());
        }

        string updateData = SerializeObject(update);

        try {

            var result = await client.PostAsync(baseURL + "api/Data/SignalRUpdate/",
            new StringContent(updateData, System.Text.Encoding.UTF8, "application/json"));
            Console.WriteLine($"SignalRUpdate: {result.StatusCode}");
        } catch (Exception ex) {
            Console.WriteLine($"SignalRUpdate Error: {ex.Message}");
        }
        Console.WriteLine("");
    }
}