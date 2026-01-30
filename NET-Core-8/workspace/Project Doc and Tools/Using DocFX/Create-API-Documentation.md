# Create API Documentation

## Introduction

DocFX can generate API documentation from XML documentation comments produced by the .NET build. This procedure creates a generated documentation folder that you can safely delete and regenerate.

Note: .NET Core supports API documentation but does not provide independent module-level documentation like some other ecosystems (for example, Rust). The API documentation is integrated with the .NET build system and IDE features.

The API documentation system is useful for:

- IDE integration / IntelliSense (documentation tooltips)
- Keeping documentation aligned with refactoring (renames, etc.)

Restrictions

- XML comments must be attached to a type, member, or method.
- XML comments cannot be attached to top-level statements (for example: `var builder = WebApplication.CreateBuilder(args);` in `Program.cs`).

## Configuration

Enable generation of the XML documentation file in your `*.csproj`:

```xml
<PropertyGroup>
   <!-- Enable XML documentation output -->
   <GenerateDocumentationFile>true</GenerateDocumentationFile>
   <!-- Optional: suppress missing-doc warnings -->
   <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

Document members with XML comments. Keep the summary short (1–3 lines) and add more details in `<remarks>` when needed:

```csharp
/// <summary>
/// API Doc.
/// 
///     Handles: GET /. 
///     prefer a inliner, max 3 lines.
//
/// </summary>
```

```csharp
/// <summary>
/// API Doc.
/// 
///     Handles: GET /. 
///     prefer a inliner, max 3 lines.
/// 
/// </summary>
/// <param name="amount">Base amount.</param>
/// <returns>Total amount including tax.</returns>
/// 
/// <remarks> Use for desgn remarks/explanations </remarks>
/// <see cref="class/method"/>
/// See <see href="Doc/Create-API-Documentation.md">Create API Documentation</see>.
public decimal GetTotal(decimal amount) { ... }
```

Common XML tags: `<summary>`, `<remarks>`, `<param>`, `<returns>`, `<exception>`, `<example>`, `<see>`, `<seealso>`, `<typeparam>`.

See Microsoft's XML documentation guide for details:
https://learn.microsoft.com/dotnet/csharp/programming-guide/xmldoc/ (or the DocFX docs for doc-specific guidance).

## Build the XML file

Build your project to produce the XML documentation file alongside the assembly:

```powershell
dotnet build
```

The XML file appears in the build output folder, e.g. `bin/Debug/net8.0/YourProject.xml`.

## Create the documentation site with DocFX

1. Install DocFX (choose one):

```powershell
# Using Chocolatey (Windows)
choco install docfx -y

# Or as a .NET global tool
dotnet tool install -g docfx
```

2. Initialize a DocFX project (creates `docfx.json` and folders):

```powershell
docfx init
```

When prompted, set values that match your project (You can edit `docfx.json` later)

- Name: Your project name
- Generate .NET API Documentation: Y
- .NET projects location: You can point to the folder containing the .csproj file (e.g., . for current directory)
- Markdown docs location: Doc-API
- Enable site search: Y
- Enable PDF: Y
- Is this correct : Y (It is not correct but we need to say "yes" to be able to edit it)

3. **Important**: edit the generated `docfx.json` metadata to point to your built assembly (DLL) and XML file. Example `metadata` entry:

```json
"metadata": [
   {
      "src": [ { "files": ["bin/Debug/net8.0/YourProject.dll"], "cwd": "." } ],
      "dest": "Doc-Generated/api"
   }
]
```

4. Ensure the `build` section includes your generated YAML/MD content. Example snippet:

```json
"build": {
  "content": [
    {
         "files": ["**/*.{md,yml}"],
         "exclude": ["Doc-Generated/**/_site/**"]
    },
    {
         "files": ["Doc-Generated/api/**.yml"],
         "dest": "Doc-Generated"
    }
  ],
  ...
}
```

And in **resource** section set the output to `Doc-Generated/_site`: changes this

``` json
"output": "_site",
```

**To this**:

``` json
 "output": "Doc-Generated/_site",
```




5. Generate API metadata (YAML):

```powershell
docfx metadata
```
This extracts the documentation from the source code 

6. Build the site:

```powershell
docfx build
```

This creates the static site under `Doc-Generated/_site`.

7. **Fix** generation bug for API directory.  
Because the build systems copies them to the wrong destination directory we need to correct that manually, the correct API location is:
 `Doc-Generated/_site/api` I tried many settings in the `docfx.json` file but the API directory  always seems to get generated in the wrong folder.

<small><sub>PowerShell (Windows):</sub></small>

```powershell
Copy-Item -Path Doc-Generated/_site/Doc-Generated/api/* -Destination Doc-Generated/_site/api -Recurse -Force
```

<small><sub>Unix/macOS:</small></sub>

```bash
cp -r Doc-Generated/_site/Doc-Generated/api/* Doc-Generated/_site/api
```

## Serve locally

```powershell
docfx serve Doc-Generated/_site
```

Then open `http://localhost:8080` in your browser.

<details>
<summary class="clickable-summary"> <span class="summary-icon"></span>
    <span style="color: #097df1ff; font-size: 26px;">Optional</span> <span style="color: #409EFF; font-size: 16px; font-style: italic;"> -  : Create cleaner API URL (redirect) </span>
</summary>

By default, the API docs are at `http://localhost:8080/Doc-Generated/api/`. If you want a cleaner URL at the root, create a redirect file.

For example, create `Doc-Generated/_site/index.html` with:
```html
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>Redirecting...</title>
    <meta http-equiv="refresh" content="0; url=Doc-Generated/api/">
    <script type="text/javascript">
        window.location.href = 'Doc-Generated/api/';
    </script>
</head>
<body>
    <p>Redirecting to API documentation...</p>
</body>
</html>
```

This redirect will be overwritten on the next `docfx build`, so recreate it if needed.
</details>