---
layout: default_c

RefPages:
  - Setup

TableCont:
  - Introduction
  - Setup

--- 
<br>

# .NET Development Environments <span style="color: #409EFF; font-size: 0.6em; font-style: italic;"> -  Docker Container</span>

![MIT License](https://img.shields.io/badge/License-MIT-green) ![Commercial Services Available](https://img.shields.io/badge/Services-Optional-blue)

<a id="Introduction"></a>

## Introduction

This repository contains Docker-based **development environments**for .NET projects. Currently featuring **.NET 8.0** - see the [.NET 8.0 overview](https://nicojane.github.io/NET-Dev-Template-Stack//NET-Core-8/) or jump directly to [Setup and Usage](https://nicojane.github.io/NET-Dev-Template-Stack/NET-Core-8/Howtos/setup). Support for additional .NET versions planned as needed.

### 🎯 What's Included

Each .NET development environment provides:

- **.NET SDK** with all runtimes (console, web, desktop)
- **Project templates generation scripts:** Console, Web API, MVC, Blazor, gRPC, Tests, and more
- **Cross-platform development** (Windows ↔ Linux containers)
- **Development tools** and utilities for streamlined workflows, including Visual Studio code task and launch files
- **A Workspace** folder for your project(s)

### 📁 Repository Structure

<pre class="nje-cmd-multi-line-sm">
NET-Dev-Template-Stack/
├── NET-Core-8/                         # .NET 8.0 development environment
│   ├── Howtos/                         # Documentation and guides
│   ├── Net8-Service/                   # Main service container
│   │   ├── script-templates/           # Project creation scripts
│   │   ├── Dockerfile_netcore_cont     # Container definition
│   │   └── compose_netcore_cont.yml    # Docker Compose
│   ├── <span style="color: #f98585; font-size: 1.2em; font-style: italic;">Workspace/                  # Project(s) go here! </span>
│   └── README.md                       # .NET 8.0 specific documentation
├── assets/                             # Documentation assets
└── README.md                           # This file
</pre>

<a id="Setup"></a>

### Setup .NET 8 Core Development Environment

This refers to the .NET 8.0 development container, the only .Net development environment Container currently available. To setup refer to this page:

- **📖🚀 Enhanced Documentation .NET core 8.0**  
  <span class="nje-indent1">View full documentation with better navigation: [Documentation Site](https://nicojane.github.io/NET-Dev-Template-Stack//NET-Core-8/)
  </span>

### Sample

The default MVC template project in .NET 8.0 provides a complete starting point for C# development. It includes pre-built custom CSS components and demonstrates best practices for implementing them in your application.

<a href="Sample-mvc.png.jpg" target="_blank">
  <img src="./Sample-mvc.png.jpg" alt="MVC template with custom CSS components and usage examples" style="width: 500px; height: auto; border-radius: 8px; cursor: pointer;">
</a>

<br><br>
<span style="color: #6d757dff; font-size: 13px; font-style: italic;"> 
<i><b>License</b><br>This file is part of: **Net-Core-Template Stack**  Copyright (c) 2025-2026 Nico Jan Eelhart.This repository is [MIT licensed](MIT-license.md) and free to use. For optional commercial support, customization, training, or long-term maintenance, see [COMMERCIAL.md](COMMERCIAL.md).</i>
</span>

<br><br>
<center>─── ✦ ───</center>
