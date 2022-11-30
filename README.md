# UWP Dependency Injection

<p align="left">
  <a href="https://github.com/vladhrapov/uwp-dependency-injection/blob/master/LICENSE">
    <img src="https://img.shields.io/badge/license-MIT-blue.svg" alt="Injectify is released under the MIT license." />
  </a>
</p>

Dependency injection packages for the UWP projects.

# Contents

<!--ts-->

- [About](#about)
- [Quick Start](#quick-start)
- [Folder Structure](#folder-structure)
- [Code Samples](#code-samples)
- [Migration Guide](#migration-guide)
- [Release Versioning](#release-versioning)

<!--te-->

# About

Injectify is a useful tool for building robust, resilient bridge between a DI framework and a UWP app. It helps to define, register all dependencies in one place and later on inject them into components. Project was inspired by [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1), [Nest.js](https://nestjs.com/), [UWP-IoC](https://github.com/MrCSharp22/UWP-IoC).

# Quick start

Install package with a DI dependent framework (other common packages like `Injectify.Abstractions` and `Injectify` will be installed as peer dependencies under the hood). For the versions lower than `0.4.0` please [reference](./docs/QUICK_START.md) the detailed guide.

## List of available packages

Microsoft.Extensions.DependencyInjection:

```
dotnet add package Injectify.Microsoft.DependencyInjection
```

Autofac:

```
dotnet add package Injectify.Autofac
```

# Folder Structure

Project is structured using a monorepo approach, contains a bunch of interlinked packages. Here is detailed folder structure:

Injectify consists of a 2 main packages with a common code:
 - `Injectify` - DI framework agnostic common helpers and implementations.
 - `Injectify.Abstractions` - abstractions for UWP application and internal interfaces.

Additional packages dependent on a specific DI framework:
 - `Injectify.Microsoft.DependencyInjection` (Microsoft.Extentions.DependencyInjection)
 - `Injectify.Autofac` (Autofac)

# Code Samples

Reference our [code samples](./docs/SAMPLES.md) of how to use DI using Injectify. Samples are split by a major version and include detailed explanation of breaking changes which were introduced from version to version.

# Migration Guide

Please follow up our migration guide [here](./docs/MIGRATION.md) if you stuck with any issue or need some guidance for migration to the higher versions.

# Release Versioning

Injectify follows synchronous updates strategy for all packages during the release. Always try to install the same versions of:
- `Injectify.Abstractions`,
- `Injectify`,
- `Injectify.<di_framework>`
