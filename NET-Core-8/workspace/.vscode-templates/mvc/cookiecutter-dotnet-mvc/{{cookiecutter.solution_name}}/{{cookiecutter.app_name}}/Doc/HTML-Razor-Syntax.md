# HTML Razor\Blazor GUI Syntax Options

## Introduction
The Razor template engine (**server-side technology**<sup>1</sup>) generates HTML code for clients. This can be done in different ways, which are described here with their usage cases, advantages, and examples.

> <sup>1 **Blazor**, since .NET 8.0, is the client-side counterpart technology. Blazor (Server and WebAssembly) uses the *.razor extension. **Razor** server-side templates for MVC use the *.cshtml extension. Both can coexist in the same project.</sup>

<br><br>

# MVC Razor Methods to Generate Code

These methods are used in traditional **ASP.NET Core MVC** applications. They render HTML on the server and send it to the client for traditional page-based navigation. Views use the *.cshtml extension and are processed by the Razor view engine during the HTTP request. These are ideal for server-side rendering scenarios where you need full control over HTML generation, model binding, and server-side validation.

1. ***Pure HTML***
    - **What:** Normal HTML syntax
    - **Example:**
        ```html
        <input type="text" class="nje-my-css-element" />
        ```
    - **When to use:**
        Use for simple, static elements or when you need full control over the HTML output. No server-side logic or model binding.


2. ***Tag Helper***
    - **What:** Server-side feature that generates HTML, enabling model binding and validation.
    - **Example:**
        ```cs
        <input asp-for="UserName" class="nje-my-css-element" />
        ```
        The above is an example of a default MS tag helper. See for reference: https://learn.microsoft.com/aspnet/core/mvc/views/tag-helpers/intro

    - **Create your own Tag Helper:**
        ```cs
        // MyInputTagHelper.cs
        using Microsoft.AspNetCore.Razor.TagHelpers;
        public class MyInputTagHelper : TagHelper
        {
            public override void Process(TagHelperContext context, TagHelperOutput output)
            {
                output.TagName = "input";
                output.Attributes.SetAttribute("class", "nje-my-css-element");
            }
        }
        ```
        ```cs
        // Usage in .cshtml:
        <my-input></my-input>
        ```

    - **When to use:**
        Use for elements that need model binding, validation, or integration with ASP.NET Core features. Also for reusable custom HTML logic.


3. ***Partial View***
    - **What:** Razor HTML source for a dedicated piece of UI that can be embedded in your main view (like a component).
    - **Example:**
        - In the file: "_My.cshtml" you have:
            ```cs
            <input type="text" class="nje-my-css-element" />
            ```
        - You can include it like:
            ```cs
            @await Html.PartialAsync("_My")
            ```
    - **When to use:**
        Use to reuse markup across multiple views, or to break up large views into smaller, manageable pieces. No logic, just markup and basic Razor.


4. ***HTML Helper***
    - **What:** C# method that generates HTML elements.
    - **Example:**
        ```cs
        @Html.TextBoxFor(m => m.UserName, new { @class = "nje-my-css-element" })
        ```
    - **Create your own HTML Helper:**
        You can create a custom HTML helper as an extension method:
        ```cs
        // In a static class, e.g., HtmlHelpers.cs
        using Microsoft.AspNetCore.Html;
        using Microsoft.AspNetCore.Mvc.Rendering;
        
        public static class HtmlHelpers
        {
            public static IHtmlContent NjeInput(this IHtmlHelper htmlHelper, string name, string value = "")
            {
                return new HtmlString($"<input type='text' name='{name}' value='{value}' class='nje-my-css-element' />");
            }
        }
        ```
        Then use it in your Razor view (after adding the namespace if needed):
        ```cs
        @Html.NjeInput("CustomField", "Initial value")
        ```

    - **When to use:**
        Use for server-side generation of form elements, especially when you want model binding, validation, or dynamic attributes.


5. ***View Component***
    - **What:** Compiled C# class plus a Razor view, for encapsulating reusable, logic-rich UI blocks.
    - **Example:**
        - In the view file: ***_MyView.cshtml*** you have:
            ```html
            <input type="text" class="nje-my-css-element" />
            ```
        - In a logic file: ***_MyView.cshtml.cs*** you may have:
            ```cs
            public class MyViewComponent : ViewComponent
            {
                public async Task<IViewComponentResult> InvokeAsync(string cssClass)
                {
                    // Your logic here
                    return View();
                }
            }
            ```
        - You can include your View Component like:
            ```cs
            @await Component.InvokeAsync("_MyView", new { cssClass = "nje-my-css-element" })
            ```
    - **When to use:**
        Use for complex, reusable UI with logic (e.g., widgets, menus, lists) that need their own C# code and view. Supports dependency injection and async logic.

<br><br>

# Blazor-Specific Methods to Generate Code

**Blazor** is a component-based framework for building interactive web applications. Unlike traditional MVC Razor (items 1-5), Blazor components use the *.razor extension and support both **Blazor Server** (server-side interactivity with WebSocket) and **Blazor WebAssembly** (client-side C# execution in the browser). Blazor components are reusable, self-contained units with encapsulated logic and UI. They support real-time two-way data binding, dependency injection, and rich interactivity without JavaScript. Both MVC Razor views (.cshtml) and Blazor components (.razor) can coexist in the same .NET 8.0 project.

6. ***Component Libraries***
    - **What:** An autonomous component that can be used across projects, designed for Blazor. Supports DI and async operations.
    - **Example:**
        - Definition: Define this in a Razor file: **MyComponent.razor**
            ```html
            @namespace MyComponentLibrary

            <div class="nje-container">
                <div class="nje-layoutbox-flex-parent">
                    <div id="block-bkground" class="nje-contentbox-flex-child" style="--nje-width-perc: 100%; --nje-width-max-perc: 100%;">
                        <div class="nje_header" style="--nje-font-size-header:10px;--nje-font-color-header: #b020b0">Toolbar</div>
                    </div>
                </div>
            </div>
            ```
        - Then use it in a Blazor component or view: **MyView.razor**
            ```html
            @using MyComponentLibrary

            <MyComponent></MyComponent>  <!-- Namespace without 'Library' suffix -->
            ```
    - **When to use:**
        Use for autonomous, reusable UI components that can be shared across multiple projects and need strong dependency injection, async logic, and lifecycle management.


7. ***RenderFragment***
    - **What:** A feature introduced in ASP.NET Core 7 that allows defining reusable Razor code blocks. It provides a flexible way to compose and organize Razor code within views or components. Definitions and callers can be in the same file or separated across files/classes.
    - **Simple Example:**
        In a Razor view, define and use a RenderFragment inline:
        ```html
        @{
            RenderFragment greeting = @<div>Hello, @Name!</div>;
        }

        <div>@greeting</div>
        ```
    - **Advanced Example:**
        Using RenderTreeBuilder in a C# class: **HtmlRenderers.cs**
        ```cs
        using Microsoft.AspNetCore.Razor.TagHelpers;

        namespace MyProjectNamespace
        {
            public static class HtmlRenderers
            {
                public static RenderFragment GreetingFragment(string name)
                {
                    return builder =>
                    {
                        builder.OpenElement(0, "div");
                        builder.AddContent(1, $"Hello, {name}!");
                        builder.CloseElement();
                    };
                }
            }
        }
        ```
        - Use it in a Razor view like:
            ```html
            @page "/example"
            @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
            @using MyProjectNamespace

            <h1>RenderFragment with Parameters</h1>

            <div>
                <h2>This is a regular HTML element</h2>
                <div>@HtmlRenderers.GreetingFragment("John Doe")</div>
                <div>@HtmlRenderers.GreetingFragment("Jane Smith")</div>
            </div>
            ```
    - **When to use:**
        Use for reusable Razor code blocks that need to be parameterized and composed dynamically. Ideal for template rendering and complex HTML logic.


8. ***Rare Things Not Covered Here***
- Editor Templates / Display Templates (for strongly-typed, reusable field rendering, e.g., EditorForModel)
- Sections/Layout features (e.g., @section, _Layout.cshtml) — but these are more about page structure than individual controls
- Direct JavaScript/JSX integration (not typical in Razor, but possible)
- Dynamic rendering with ViewBag/ViewData (but this is more about data passing than control creation)

<br><br>

## Summary: Use Cases and .NET Versions

| Method | Use Case | .NET Version |
|--------|----------|--------------|
| Pure HTML | Simple, static elements or when you need full control over the HTML output | All versions |
| Tag Helper | Elements that need model binding, validation, or integration with ASP.NET Core features. Also for reusable custom HTML logic | .NET Core 1.0+ |
| Partial View | Reuse markup across multiple views, or break up large views into smaller, manageable pieces. No logic, just markup and basic Razor | .NET Core 1.0+ |
| HTML Helper | Server-side generation of form elements, especially when you want model binding, validation, or dynamic attributes | .NET Core 1.0+ |
| View Component | Complex, reusable UI with logic (e.g., widgets, menus, lists) that need their own C# code and view. Supports dependency injection and async logic | .NET Core 1.0+ |
| Component Libraries | Autonomous components that can be used across projects, designed for Blazor. Supports DI and async | .NET 6.0+ |
| RenderFragment | Reusable Razor code blocks, providing a flexible way to compose and organize Razor code within views or components | .NET Core 7.0+ |
