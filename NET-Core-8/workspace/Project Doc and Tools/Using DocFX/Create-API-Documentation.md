# Create API Documentation

## Introduction

> Note : Docfx is not particularly a joy to work with, but this procedure should create a specific generated folder in your document root that you can easily delete and regenerate.

.NET Core only supports API documentation, no independent module documentation (like in Rust)
The API Documentation is interweaved(coupled) in the .NET Build systems and has other functions beside the API documentation itself, most importantly:

- IDE Tool integration\IntelliSense (Documentation tooltips)
- Refactoring (renaming does update the documentation)

For this reason the usage of the API documentation tag has a few restrictions: 

- Comment must be attached to class, method or member
- Can **not** be attached to top-level statement. for example items like: ***var builder = WebApplication.CreateBuilder(args);*** in ***Program.cs*** 

## Configuration. &  Usage

Add the following to the ***.csproj*** file:

```
<PropertyGroup>
    ...
    <!-- For documentation  generation -->
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <!-- Optional: suppress warnings for missing docs -->
    <NoWarn>$(NoWarn);1591</NoWarn> 
    ...
</PropertyGroup>
```

Add the following to a class/method to document the item

```
/// <summary>
/// API Doc.
/// 
///     Handles: GET /. 
///     prefer a onliner, max 3 lines.
//
/// </summary>
```

or more detailed like:
```
/// <summary>
/// API Doc.
/// 
///     Handles: GET /. 
///     prefer a onliner, max 3 lines.
///
/// </summary>
/// <param name="sureName">... </param>
/// <returns>The total price including tax.</returns>
///
/// <remarks> Use for desgn remarks/explanations </remarks>
/// <see cref="class/method"/>
/// See <see href="Doc/Create-API-Documentation.md">Create API Documentation</see>.
```

Some other common XML tags:
``
<exception> <example> <see> <seealso> <typeparam>
``
More can be found here:[website](https://www.nu.nl)

## Result

Using the above will generate a raw ***[projectname].xml*** file that contains the documentation per items and can be used to create a real document

## Create the documentation site

The ***[projectname].xml*** file can be used to create the developer documentation. Use:

1. **Prerequisite**: Ensure your project is **built** first
   ```
   dotnet build
   ```
   This generates the compiled `.dll` file and the `[projectname].xml` documentation file in the `bin/Debug/net[version]/` directory.

2. Install docfx
   ```
   dotnet tool install -g docfx
   ```

3. Generate the documentation structure
   ```
   docfx init
   ```  
   This will prompt for configuration. Suggestions: 
   - **Name**: Your project name
   - **Generate .NET API Documentation**: Y
   - **.NET projects location**: You can point to the folder containing the `.csproj` file (e.g., `.` for current directory)
   - **Markdown docs location**: Doc-API
   - **Enable site search**: Y
   - **Enable PDF**: Y
   - **Is this correct** : Y   (It is **not** correct but we need to say "yes" to be able to edit it)

4. **Important**: Edit the generated `docfx.json` 
   
   Update the `metadata` section to point to your **compiled DLL** and output to `Doc-Generated/api`:
   ```json
   "metadata": [
     {
       "src": [
         {
           "files": ["bin/Debug/net8.0/[YourProjectName].dll"],
           "cwd": "."
         }
       ],
       "dest": "Doc-Generated/api"
     }
   ]
   ```
   
   Also update the `build` section to include the API YAML files:
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

   And in **resource** set the output to `Doc-Generated/_site`: changes this
   ``` json
   "output": "_site",
   ```
   To this
   ``` json
    "output": "Doc-Generated/_site",
   ```


5. Generate the API metadata YAML files
   ```
   docfx metadata
   ```
   This converts your compiled DLL and XML documentation into YAML files in the `Doc-Generated/api` directory.

6. Generate the documentation
   ```
   docfx build
   ```  
   This generates the `Doc-Generated/_site` directory with your complete documentation including API reference.

7. **Fix API navigation**: Copy API files to where the template expects them
   ```bash
   cp -r Doc-Generated/_site/Doc-Generated/api/*  Doc-Generated/_site/api
   ```
   This allows the template's "API" tab to work correctly. Without this step, the API is only accessible at `http://localhost:8080/Doc-Generated/api/`.

## View the Documentation Site

To display and run the documentation site locally:
```
docfx serve Doc-Generated/_site
```

Then open your browser to `http://localhost:8080`

<details>
<summary class="clickable-summary"> <span class="summary-icon"></span>
    <span style="color: #097df1ff; font-size: 26px;">Optional</span> <span style="color: #409EFF; font-size: 16px; font-style: italic;"> -  Optional: Create cleaner API URL (redirect) </span>
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