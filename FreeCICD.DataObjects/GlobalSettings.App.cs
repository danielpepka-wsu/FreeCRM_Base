namespace FreeCICD;

/// <summary>
/// This static class is used to persist a few settings about the startup state of the application.
/// </summary>
public static partial class GlobalSettings
{
    public enum EnvironmentType
    {
        DEV,
        PROD,
        CRM
    }
    public class EnvironmentOptions
    {
        public string AgentPool { get; set; } = "Default";
        public string Hostname { get; set; }
        public string IISJsonFilePath { get; set; }
    }
    public static class App {
        public static string Name { get; set; } = "CICD";
        public static string Version { get; set; } = "1.0.0";
        public static string ReleaseDate { get; set; } = "3/6/2025";
        public static string CompanyName { get; set; } = "Company Name";
        public static string CompanyUrl { get; set; } = "em.wsu.edu";

        public static List<GlobalSettings.EnvironmentType> EnviormentTypeOrder = new List<GlobalSettings.EnvironmentType>() { EnvironmentType.DEV, EnvironmentType.PROD, EnvironmentType.CRM };

        public static Dictionary<EnvironmentType, EnvironmentOptions> EnvironmentOptions = new Dictionary<EnvironmentType, EnvironmentOptions>() {
            { EnvironmentType.DEV, new (){AgentPool ="AzureDev", Hostname = $"dev.{CompanyUrl}", IISJsonFilePath = "" } },
            { EnvironmentType.PROD, new (){AgentPool ="AzureProd",Hostname =  $"prod.{CompanyUrl}", IISJsonFilePath = "" } },
            { EnvironmentType.CRM, new (){AgentPool ="AzureCRM",Hostname =  $"crm.{CompanyUrl}", IISJsonFilePath = "" } },
        };

        // anything starting with . _ or XX - OLD - will be ignored.  some common methods of indicating private things so might as well follow it
        public static List<string> AzureDevOpsProjectNameStartsWithIgnoreValues = ["XX - OLD - ",".", "_"];

        public static string IISJsonFilePathDefault= "IISInfo";
        
        public static string VariableGroupNameDefault = "AppSettings";

        public static string[] AnonamousAccessList = ["ABOUT", "DATABASEOFFLINE", "LOGIN", "LOGOUT", "PROCESSLOGIN", "SETUP", "DEVOPS", "", "HOME"];

        public static string BuildPiplelinePool = "BuildVM";

        public static string BuildPipelineTemplate = @"
# build-and-deploy-pipeline.yml
name: '$(Date:yyyyMMdd)$(Rev:.r)'

resources:
  repositories:
    - repository: TemplateRepo
      type: git
      name: '{{DEVOPS_PROJECTNAME}}'
      ref: 'refs/heads/{{DEVOPS_REPO_BRANCH}}'
      trigger: none
    - repository: BuildRepo
      type: git
      name: '{{CODE_PROJECT_NAME}}/{{CODE_REPO_NAME}}'
      ref: 'refs/heads/{{CODE_REPO_BRANCH}}'
      trigger:
        branches:
          include:
            - {{CODE_REPO_BRANCH}}

variables:
  - template: templates/common-variables.yml@TemplateRepo

{{PIPELINE_VARIABLES}}

pool:
  name: '{{PIPELINE_POOL}}'

stages:
  ####################################################################
  # 0. Pre-Build Stage
  ####################################################################
  - stage: PreBuildStage
    displayName: ""Pre-Build Stage""
    jobs:
      - job: PreBuildJob
        workspace:
          clean: all
        displayName: ""Dump Environment Variables""
        steps:
          - checkout: none
          - download: none
          - template: templates/dump-env-variables-template.yml@TemplateRepo

  # -------------------------------------------------------------------
  # Build Stage: pass the runtime variables to the build template
  # -------------------------------------------------------------------
  - stage: BuildStage
    displayName: ""Build Stage""
    jobs:
      - job: BuildJob
        displayName: ""Build and Publish Application""
        pool:
          name: '{{PIPELINE_POOL}}'
        steps:
          - template: templates/build-template.yml@TemplateRepo
            parameters:
              buildProjectName: ""$(CI_ProjectName)""
              buildCsProjectPath: ""$(CI_BUILD_CsProjectPath)""
              buildPublishArgs: ""$(CI_PIPELINE_COMMON_PublishArgs)""
              buildNamespace: ""$(CI_BUILD_Namespace)""

  # -------------------------------------------------------------------
  # Info Stage: Basic information (env dump, etc.)
  # -------------------------------------------------------------------
  - stage: InfoStage
    displayName: ""Info Stage""
    jobs:
      - job: InfoJob
        displayName: ""Info Job""
        pool:
          name: '{{PIPELINE_POOL}}'
        steps:
          - checkout: none
          - download: none
          - task: PowerShell@2
            displayName: ""Just a filler step""
            inputs:
              targetType: 'inline'
              script: |
                Write-Host ""Hello World""

{{DEPLOY_STAGES}}";
    }
}