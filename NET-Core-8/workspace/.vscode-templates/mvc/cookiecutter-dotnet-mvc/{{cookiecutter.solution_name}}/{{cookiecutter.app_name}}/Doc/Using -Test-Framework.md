# Using Test Framework, xUnit

## Introduction

The decision to use xUnit is based on the following knowledge:

- It is the most used, modern, and performant test framework for .NET projects
- It is cross-platform compatible, and is also supported by **Avalonia** ([see](https://avaloniaui.net/)), the .NET equivalent for *Linux* and *macOS* systems

Alternative test frameworks that can be used are:
- NUnit, can also be used with **Avalonia**
- MSTest (Built-in into .Net)

## Installation & Configuration 

This is rather simple, but for completeness

### 1.1 VS Code (.NET)
- Check the latest available version [here](https://xunit.net/releases/) and use that version in the command below
- Use this command in your project folder: `dotnet add package xunit`  # To get the latest version
- Use this command in your project folder: `dotnet add package xunit --version 3.2.2` # If you require a specific version"

### 1.2 VS Code (.Avalonia)
- Use this command in your project folder: `dotnet add package xunit`  # To get the latest version (**Required** for testing)
- Use this command in your project folder: `dotnet add package Avalonia.Headless.XUnit`  # add the **required** 'headless package' for UI testing

<br>

> Note: To run tests in **VS Code** you can install the extension: **Test Explorer**


### 1.3 Visual Studio

- Check the latest available version [here](https://xunit.net/releases/) and use that version in the command below
- In **Visual Studio** go to: **Tools > NuGet Package Manager > Package Manager Console**
- Use this command in your project folder: `Install-Package xunit` # Latest version
- Use this command in your project folder: `Install-Package xunit -Version 3.2.2` # Specific version

**Note:** You can also install the 'Visual Studio adapter' for better integration. This enables you to run your tests directly from within **Visual Studio** with a visual interface for running and debugging. Install:

- Use this command in your project folder: `Install-Package xunit.runner.visualstudio`

> This enables running and debugging tests directly from Visual Studio.

<br>

---

## Basic xUnit Usage

### 1. Standard Unit Tests

For non-UI logic (e.g., view models, services), use standard xUnit attributes:

``` C#
public class MyTests
{
    [Fact]
    public void TestAddition()
    {
        int result = 2 + 2;
        Assert.Equal(4, result);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(-1, 1, 0)]
    public void TestAdditionTheory(int a, int b, int expected)
    {
        Assert.Equal(expected, a + b);
    }
}

```

### 2. Avalonia UI Tests

For UI testing in Avalonia, use the [AvaloniaFact] and [AvaloniaTheory] attributes:

``` C#
using Avalonia.Headless.XUnit;
using Avalonia.Controls;

public class MyAvaloniaTests
{
    [AvaloniaFact]
    public void TestButtonContent()
    {
        var button = new Button { Content = "Click Me" };
        Assert.Equal("Click Me", button.Content);
    }

    [AvaloniaTheory]
    [InlineData("Hello")]
    [InlineData("World")]
    public void TestButtonContentTheory(string content)
    {
        var button = new Button { Content = content };
        Assert.Equal(content, button.Content);
    }
}

```

###  3. Test Fixtures and Setup

For shared test setup (e.g., initializing Avalonia apps):

``` C#
public class AvaloniaTestFixture : IDisposable
{
    public AvaloniaTestFixture()
    {
        // Initialize Avalonia app or services here
    }

    public void Dispose()
    {
        // Cleanup resources
    }
}

public class MyAvaloniaTests : IClassFixture<AvaloniaTestFixture>
{
    [AvaloniaFact]
    public void TestWithFixture()
    {
        // Test logic using the fixture
    }
}

```

### 4. Assertions

xUnit provides a rich set of assertions:

``` C#
[Fact]
public void TestAssertions()
{
    var list = new List<int> { 1, 2, 3 };
    Assert.NotEmpty(list);
    Assert.Contains(2, list);
    Assert.DoesNotContain(4, list);
}
```

### 5. Mocking (Example with Moq)

For testing dependencies, use a mocking framework like Moq:

> Note: To use Moq, install it via: `dotnet add package Moq`

``` C#
[Fact]
public void TestWithMock()
{
    var mockService = new Mock<IMyService>();
    mockService.Setup(s => s.GetValue()).Returns(42);

    var viewModel = new MyViewModel(mockService.Object);
    Assert.Equal(42, viewModel.Value);
}

```

<br>

---

## Running Tests

### VS Code

1. **Using the Test Explorer Extension:**
   - Install the **Test Explorer** extension from the VS Code marketplace
   - Open the **Test Explorer** view from the sidebar
   - Click the **Run** icon to execute all tests, or run individual tests by clicking their **Run** button

2. **Using the Terminal:**
   - Run all tests: `dotnet test`
   - Run tests in a specific project: `dotnet test ./MyTestProject.csproj`
   - Run tests with a specific filter: `dotnet test --filter "FullyQualifiedName~MyTests"`
   - Run with verbose output: `dotnet test --verbosity detailed`

3. **Watch Mode (Auto-run on file changes):**
   - Use: `dotnet watch test`
   - This will automatically re-run tests when you save changes

### Visual Studio

1. **Using Test Explorer:**
   - Go to **Test > Test Explorer** (or press `Ctrl+E, T`)
   - Select tests to run using the checkboxes
   - Click **Run All**, **Run Selected**, or right-click for more options

2. **Using the Terminal:**
   - Open the Package Manager Console: **Tools > NuGet Package Manager > Package Manager Console**
   - Run: `dotnet test` or use PowerShell equivalents

3. **Running from Code:**
   - Right-click on a test method or test class and select **Run Tests** or **Debug Tests**

### Command Line Options

Common options for `dotnet test`:

- `--configuration Release` - Run in Release mode
- `--no-build` - Skip building before testing
- `--verbosity [quiet|minimal|normal|detailed|diag]` - Control output level
- `--logger "console;verbosity=detailed"` - Specify logger output
- `--collect:"XPlat Code Coverage"` - Generate code coverage reports

### Example Workflow

```bash
# Build the project
dotnet build

# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity detailed

# Run specific test class
dotnet test --filter "FullyQualifiedName~MyAvaloniaTests"

# Run with code coverage
dotnet test --collect:"XPlat Code Coverage"
```

<br>

---

## Next Steps

- Explore [xUnit’s official documentation](https://xunit.net/docs/getting-started/netfx/visual-studio) for advanced features.
- For Avalonia, check out the [headless testing guide](https://docs.avaloniaui.net/docs/concepts/headless/headless-xunit).
- Learn about CI/CD integration for xUnit and Avalonia.

