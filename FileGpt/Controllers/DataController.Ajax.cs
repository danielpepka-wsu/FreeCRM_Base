using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileGpt.Server.Controllers;

public partial class DataController
{
    [HttpPost]
    [Authorize]
    [Route("~/api/Data/AjaxUserSearch/")]
    public ActionResult<DataObjects.AjaxLookup> AjaxUserSearch(DataObjects.AjaxLookup Lookup)
    {
        var output = da.AjaxUserSearch(Lookup);
        return Ok(output);
    }
}
