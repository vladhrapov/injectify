# Quick start

## Version 0.4.0+

Starting from version `0.4.0` you need to reference **only** DI framework dependant package (other common packages like `Injectify.Abstractions` and `Injectify` will be installed as peer dependencies under the hood):

```
dotnet add package Injectify.Microsoft.DependencyInjection
```

## Version 0.3.0+

Starting from version `0.3.0` you need to reference a common, DI framework agnostic package:

```
dotnet add package Injectify
```

## General

Install package with abstractions:

```
dotnet add package Injectify.Abstractions
```

Install package with a DI dependent framework:

`Microsoft.Extensions.DependencyInjection:`

```
dotnet add package Injectify.Microsoft.DependencyInjection
```


[Back](../README.md)