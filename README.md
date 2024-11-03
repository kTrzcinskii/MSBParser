# MSBParser

MSBParser is a GUI application made with WPF for analyzing MSBuild (.csproj) files.
Its core features are:

- parsing file into tree structure with `ProjectNode` in its root
- syntax highlighting for different parts of the file (elements, attributes, values)
- finding syntax errors, highligting them and showing corresponding error messages

The main goal of this project was to properly parse MSBuild files and based on this data find errors and provide syntax highlighting. There are some minor bugs with the highlighting itself, but as it's more WPF and fronted related I didn't put much time into that - I was focused on the "backend" side of the application.

When developing the app I used [Microsoft Docs](https://learn.microsoft.com/en-us/visualstudio/msbuild/msbuild-project-file-schema-reference?view=vs-2022) as reference.

Currently application only checks for syntax errors in "Elements" (e.g. it shows an error if unknown tag is found inside `Project`), though there aren't any checks made on the attributes and their values (meaning if some tag should always have an attribute and the attribute is not provided then MSBParser will NOT consider it to be an error).

## Examples

- MSBuild file without any errors:
  ![Without errors example](Examples/without_errors.png)
- MSBuild file with errors:
  ![With errors example](Examples/with_errors.png)
