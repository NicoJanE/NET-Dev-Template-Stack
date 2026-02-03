---
title: API Documentation:Keyroll Server  System
uid: ID99
summary: How Keyroll Server works
---

# API Documentation <span style="color: #409EFF; font-size: 0.6em; font-style: italic;"> -  {{ cookiecutter.app_name }}</span>

<br>

## Introduction

What is it?




> [!TIP]
> [!WARNING]
> [!CAUTION]
> [!IMPORTANT]
See @Keyroll.Server.AuthService for details.
See <xref:Keyroll.Server.getting-started>


[!code-csharp[](../source/Program.cs)]


```mermaid
sequenceDiagram
Client->>Server: Login
Server->>DB: Validate
DB-->>Server: OK
Server-->>Client: Token
