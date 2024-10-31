namespace MSBParser;
internal static class TagNames
{
    public const string Project = "Project";
    public const string PropertyGroup = "PropertyGroup";
    public const string ItemGroup = "ItemGroup";
    public const string Target = "Target";
    public const string OnError = "OnError";
    public const string Output = "Output";

    private static readonly string[] Tags = [Project, PropertyGroup, ItemGroup, Target, OnError, Output];

    public static bool IsAnyOfTags(string tagName)
    {
        return Tags.Contains(tagName);
    }
}
