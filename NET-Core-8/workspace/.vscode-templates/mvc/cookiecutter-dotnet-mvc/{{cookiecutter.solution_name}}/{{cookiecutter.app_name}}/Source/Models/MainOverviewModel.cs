namespace {{ cookiecutter.app_name }}.Models;

public class MainOverviewModel
{
    public int Id { get; set; }

    public string? Name { get; set; } = "Nico";
    
    public string[] FullNameOptions { get; set; } = new[] { "John Smith", "Emily Johnson", "Michael Brown", "Sarah Davis", "Robert Wilson" };

    public string helptext_1 { get; set; } = "Help 1 is on it way, yes it is !\n not sure ? ";
    public string helptext_2 { get; set; } = "Help 2 is on it way ";
}
