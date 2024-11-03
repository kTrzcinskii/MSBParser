namespace MSBParser;
internal static class TagNames
{
    public const string Project = "Project";
    public const string PropertyGroup = "PropertyGroup";
    public const string ItemGroup = "ItemGroup";
    public const string Target = "Target";
    public const string OnError = "OnError";
    public const string Output = "Output";
    public const string Import = "Import";
    public const string ImportGroup = "ImportGroup";
    public const string ItemDefinitionGroup = "ItemDefinitionGroup";
    public const string ParameterGroup = "ParameterGroup";
    public const string UsingTask = "UsingTask";
    public const string ProjectExtensions = "ProjectExtensions";

    private static readonly string[] Tags = [Project, PropertyGroup, ItemGroup, Target, OnError, Output, ImportGroup, Import, ItemDefinitionGroup, ParameterGroup, UsingTask, ProjectExtensions];

    public static bool IsAnyOfTags(string tagName)
    {
        return Tags.Contains(tagName);
    }
}
