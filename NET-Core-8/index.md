---
layout: default_c
RefPages:
 - setup

TableCont:
  - Introduction
  - Setup references

---  
<br>

# .NET 8.0 Development Environment <span style="color: #409EFF; font-size: 0.6em; font-style: italic;"> -  Docker Container</span>

![MIT License](https://img.shields.io/badge/License-MIT-green) ![Commercial Services Available](https://img.shields.io/badge/Services-Optional-blue)

<a id="Introduction"></a>

## Introduction

This is the  **.NET 8 development Docker Environment**  with **11 ready-to-use project templates** for rapid application development within Docker<sup>*1</sup> and Visual Studio Code.  <span class="nje-br1"> </span>  <span class="nje-indent2"> 
<sub>*1. Building under Windows is supported when your local Window  environment is configured for it. </sub></span>

### 🎯 What's Included

- **.NET 8 SDK** with all runtimes (console, web, desktop)
- **11 project templates:** Console, Web API, MVC, Blazor, gRPC, Tests, and more
- **Cross-platform development** (Windows ↔ Linux containers)
- **Development tools** and utilities for streamlined workflows, including Visual Studio code task and launch files
- **A Workspace** folder for your project(s)

## 📁Environment & Project Structure

<pre class="nje-cmd-multi-line-sm">
NET-Dev-Template-Stack/NET-Core-8/
├── Howtos/                         # Installation guides for the environment
├── Net8-Service/                   # Docker installation scripts & project template scripts.
│   ├── script-templates/
│   ├── Dockerfile_netcore_cont
│   └── compose_netcore_cont.yml
<span style="color: #f98585; font-size: 1.1em; font-style: italic;">├── Workspace/        # Project(s) go here! </span>
│   <span style="color: #f98585; font-size: 1.1em; font-style: italic;">├── README.md     # README.MD file for your project </span>
│   <span style="color: #f98585; font-size: 1.1em; font-style: italic;">├── Index.md      # index.md file for your project </span>
└── README.md                        # .NET 8.0 environment information

</pre>

<a id="Setup references"></a>

### Setup References

Setup for the .NET 8.0 development environment  container. To setup, choose between the quick setup and the full setup instructions.

- **🚀 Quick Links**  
  <span class="nje-indent1">Get coding in 5 minutes with .NET 8.0: [Quick Start Guide](https://nicojane.github.io/NET-Dev-Template-Stack/NET-Core-8/Howtos/setup#Quick install)
  </span>
- **📚 Full Setup**  
  <span class="nje-indent1">Complete Setup Guide for .NET 8.0: [Setup Documentation](https://nicojane.github.io/NET-Dev-Template-Stack/NET-Core-8/Howtos/setup)  <span style="color: #0dbf60ff; font-size: 1.0em; "> (Recommended) </span></span>
- **📚 .NET scripts**  
  <span class="nje-indent1">Overview of the available .NET 8.0 scripts:  [Available Scripts](./Howtos/setup.md#available-script-templates) </span>

<span style="color: #6d757dff; font-size: 13px; font-style: italic;"> 
<i><b>License</b><br>This file is part of: **Net-Core-Template Stack**  Copyright (c) 2025-2026 Nico Jan Eelhart.This repository is [MIT licensed](MIT-license.md) and free to use. For optional commercial support, customization, training, or long-term maintenance, see [COMMERCIAL.md](COMMERCIAL.md).</i>
</span>

<br><br>
<center>─── ✦ ───</center>
