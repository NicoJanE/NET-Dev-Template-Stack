
# HTML Razor GUI Syntax Options

## Introduction
The Razor template engine (**server-side technology**<sup>1*</sup>) generates HTML code for clients. This can be done in different ways, which are described here with their usage cases, advantages, and examples.

> <sup>1 **Blazor**, since .NET 8.0, is the client-side counterpart technology. Blazor (Server and WebAssembly) is related, **Razor** server-side templates use the extension: *.cshtml, while Blazor use the extension: *.razor </sup>

# Razor Methods to Generate Code


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
        - in a logic file: ***_MyView.cshtml.cs*** you may have:
            ```
            //
            public class MyViewComponent : ViewComponent
            {
                public async Task<IViewComponentResult> InvokeAsync(string cssClass)
                {
                    // Your logic here
                    return View();
                }
            }
            ```
        - You can include  your View Component like:
            ```cs
            @await Component.InvokeAsync("_MyView", new { cssClass = "nje-my-css-element" })
            ```
    - **When to use:**
        Use for complex, reusable UI with logic (e.g., widgets, menus, lists) that need their own C# code and view. Supports dependency injection and async logic.

6.  **Component Libraries:**
    - **What:** An autonomic component that be used across projects, designed for MVC and razor. Supports DI, Async.
    - **Example:**
        - Definition. Define this in a Razor file: **MyComponent.razor** 
            ``` html
                @namespace MyComponentLibrary
                
                <div class="nje-container">
                    <div class="nje-layoutbox-flex-parent">
                        <div id="block-bkground" class="nje-contentbox-flex-child" style="--nje-width-perc: 100%; --nje-width-max-perc: 100%;">                    
                                <div class="nje_header" style="--nje-font-size-header:10px;--nje-font-color-header: #b020b0">Toolbar</div>
                            </div>
                        </div>
                    </div>
                </div>
            ```
        -  Then call it like in for example: **MyView.cshtml**
            ``` html
                @using MyComponentLibrary                  <!--  MyComponent.razor should be in same project, not nessacery in the same  directorie -->

                <MyComponent></MyComponent>       <!-- Namespace  without 'Library'  -->
            ```

7. ***Rare things not covered  here***
- Editor Templates / Display Templates (for strongly-typed, reusable field rendering, e.g., EditorForModel)
- Sections/Layout features (e.g., @section, _Layout.cshtml) — but these are more about page structure than individual controls
- Direct JavaScript/JSX integration (not typical in Razor, but possible)
- Dynamic rendering with ViewBag/ViewData (but this is more about data passing than control creation)



