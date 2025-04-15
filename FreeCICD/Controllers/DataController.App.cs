// Use this file as a place to put any application-specific API endpoints.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Mysqlx.Crud;

namespace FreeCICD.Server.Controllers;

public partial class DataController
{
    /// <summary>
    /// these are the project repo and branch were we are storing the yml files for release pipelines
    /// </summary>
    /// <returns></returns>
    private (string orgName, string pat, string projectId, string repoId, string branch) GetReleasePipelinesDevOpsConfig()
    {
        string orgName = configurationHelper?.OrgName ?? "";
        string pat = configurationHelper?.PAT ?? "";
        string projectId = configurationHelper?.ProjectId ?? "";
        string repoId = configurationHelper?.RepoId ?? "";
        string branch = configurationHelper?.Branch ?? "";
        return (orgName, pat, projectId, repoId, branch);
    }

    #region Git & Pipeline Endpoints (Authenticated)




    //DataObjects.Endpoints.DevOps.GetDevOpsFiles 
    //DataObjects.Endpoints.DevOps.GetDevOpsProjects 
    //DataObjects.Endpoints.DevOps.GetDevOpsRepos 
    //DataObjects.Endpoints.DevOps.GetDevOpsPipelines 
    //DataObjects.Endpoints.DevOps.SaveDevOpsPipeline 
    //DataObjects.Endpoints.DevOps.GetDevOpsIISInfo 

    //DataObjects.Endpoints.DevOps.GetDevOpsBranches 

    [HttpGet($"~/{DataObjects.Endpoints.DevOps.GetDevOpsBranches}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<DataObjects.DevopsGitRepoBranchInfo>>> GetDevOpsBranches([FromQuery] string projectId, [FromQuery] string repoId, [FromQuery] string? pat = null, [FromQuery] string? orgName = null, [FromQuery] string? connectionId = null)
    {
        List<DataObjects.DevopsGitRepoBranchInfo> output;
        if (CurrentUser.Enabled) {
            var config = GetReleasePipelinesDevOpsConfig();
            output = await da.GetDevOpsBranchesAsync(config.pat, config.orgName, projectId, repoId, connectionId);
        } else if (!string.IsNullOrWhiteSpace(pat) && !string.IsNullOrWhiteSpace(orgName)) {
            output = await da.GetDevOpsBranchesAsync(pat, orgName, projectId, repoId, connectionId);
        } else {
            return BadRequest("No PAT or OrgName provided and user is not logged in.");
        }

        return Ok(output);
    }

    [HttpGet($"~/{DataObjects.Endpoints.DevOps.GetDevOpsFiles}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<DataObjects.DevopsFileItem>>> GetDevOpsFiles([FromQuery] string projectId, [FromQuery] string repoId, [FromQuery] string branchName, [FromQuery] string? pat = null, [FromQuery] string? orgName = null, [FromQuery] string? connectionId = null)
    {
        List<DataObjects.DevopsFileItem> output;
        if (CurrentUser.Enabled) {
            var config = GetReleasePipelinesDevOpsConfig();
            output = await da.GetDevOpsFilesAsync(config.pat, config.orgName, projectId, repoId, branchName, connectionId);
        } else if (!string.IsNullOrWhiteSpace(pat) && !string.IsNullOrWhiteSpace(orgName)) {
            output = await da.GetDevOpsFilesAsync(pat, orgName, projectId, repoId, branchName, connectionId);
        } else {
            return BadRequest("No PAT or OrgName provided and user is not logged in.");
        }

        return Ok(output);
    }

    [HttpGet($"~/{DataObjects.Endpoints.DevOps.GetDevOpsProjects}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<DataObjects.DevopsProjectInfo>>> GetDevOpsProjects([FromQuery] string? pat = null, [FromQuery] string? orgName = null, [FromQuery] string? connectionId = null)
    {
        List<DataObjects.DevopsProjectInfo> output;

        if (CurrentUser.Enabled) {
            var config = GetReleasePipelinesDevOpsConfig();
            output = await da.GetDevOpsProjectsAsync(config.pat, config.orgName, connectionId);
        } else if (!string.IsNullOrWhiteSpace(pat) && !string.IsNullOrWhiteSpace(orgName)) {            
            output = await da.GetDevOpsProjectsAsync(pat, orgName, connectionId);
        } else {
            return BadRequest("No PAT or OrgName provided and user is not logged in.");
        }

        return Ok(output);
    }

    [HttpGet($"~/{DataObjects.Endpoints.DevOps.GetDevOpsRepos}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<DataObjects.DevopsGitRepoInfo>>> GetDevOpsRepos([FromQuery] string projectId, [FromQuery] string? pat = null, [FromQuery] string? orgName = null, [FromQuery] string? connectionId = null)
    {
        List<DataObjects.DevopsGitRepoInfo> output;

        // check pat or login
        
        if (CurrentUser.Enabled) {
            var config = GetReleasePipelinesDevOpsConfig();
            output = await da.GetDevOpsReposAsync(config.pat, config.orgName, projectId, connectionId);
        }else if (!string.IsNullOrWhiteSpace(pat) && !string.IsNullOrWhiteSpace(orgName)) { 
            output = await da.GetDevOpsReposAsync(pat, orgName, projectId, connectionId);
        } else {
            return BadRequest("No PAT or OrgName provided and user is not logged in.");
        }

        return Ok(output);
    }

    //GetDevOpsPipelines

    [HttpGet($"~/{DataObjects.Endpoints.DevOps.GetDevOpsPipelines}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<DataObjects.DevopsPipelineDefinition>>> GetDevOpsPipelines([FromQuery] string? projectId, [FromQuery] string? repoId, [FromQuery] string? pat = null, [FromQuery] string? orgName = null, [FromQuery] string? connectionId = null)
    {
        List<DataObjects.DevopsPipelineDefinition> output;

        // check pat or login

        if (CurrentUser.Enabled) {
            var config = GetReleasePipelinesDevOpsConfig();
            output = await da.GetDevOpsPipelines(config.projectId, config.pat, config.orgName, connectionId); 
        } else if (!string.IsNullOrWhiteSpace(pat) && !string.IsNullOrWhiteSpace(orgName) && !string.IsNullOrEmpty(projectId)) {
            output = await da.GetDevOpsPipelines(projectId, pat, orgName, connectionId);
        } else {
            return BadRequest("No PAT or OrgName provided and user is not logged in.");
        }

        return Ok(output);
    }



    
    [HttpGet($"~/{DataObjects.Endpoints.DevOps.GetDevOpsYmlFileContent}")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> GetDevOpsYmlFileContent(string? filePath, [FromQuery] string? projectId, [FromQuery] string repoId, [FromQuery] string branchName, [FromQuery] string? pat = null, [FromQuery] string? orgName = null, [FromQuery] string? connectionId = null)
    {
        string output = string.Empty;

        // check pat or login

        if (CurrentUser.Enabled) {
            var config = GetReleasePipelinesDevOpsConfig();
            output = await da.GetGitFile(filePath, config.projectId,config.repoId,config.branch, config.pat, config.orgName, connectionId);
        } else if (!string.IsNullOrWhiteSpace(pat) && !string.IsNullOrWhiteSpace(orgName) && !string.IsNullOrEmpty(projectId)) {
            output = await da.GetGitFile(filePath, projectId,repoId, branchName ,pat, orgName, connectionId);
        } else {
            return BadRequest("No PAT or OrgName provided and user is not logged in.");
        }

        return Ok(output);
    }
    #endregion Git & Pipeline Endpoints
}
