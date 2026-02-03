namespace {{ cookiecutter.app_name }}.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
//  codegen_trace=1 dotnet aspnet-codegenerator view Create Create -m {{ cookiecutter.app_name }}.Models.MainOverviewModel --relativeFolderPath Source/Views/Home -udl
// <Target Name="EvaluateProjectInfoForCodeGeneration" />

//<PropertyGroup>
//  <CodeGeneration>true</CodeGeneration>
//</PropertyGroup>