# Change Log\n\nAll notable changes to this project will be documented in this file. See [versionize](https://github.com/saintedlama/versionize) for commit guidelines.\n
<a name="0.4.2"></a>
## 0.4.2 (2020-10-10)

### Bug Fixes

* fix package cross references

### Features

* add sample for usage of the v0.4.*

### Other

* update dependencies
* Merge pull request #4 from vladhrapov/release/0.4.1

<a name="0.4.1"></a>
## 0.4.1 (2020-10-9)

### Bug Fixes

* handle NullReferenceException when class was not marked as Injectable, but Bootstrap was called

### Other

* update to v0.4.1 version additional annotations
* update github repository url and tags
* update migration info for v0.4.0
* fix typo of the bootstrapper name
* remove nuget.exe
* update migration guide
* Merge pull request #3 from vladhrapov/release/0.4.0
* bump manually the rest versions for Injectify, Injectify.Abstractions

<a name="0.4.0"></a>
## 0.4.0 (2020-10-7)

### Bug Fixes

* add dependencies to Injectify.Microsoft.DependencyInjection nuspec package metadata

### Features

* use InjectionContext from abstractions for common codebase, add service provider selectors as extensions, move InjectableAttribute to Injectify package
* simplify internal annotations API, replace a bunch of parameters with InjectionContext, move InjectableAttribute to Injectify package as a common code
* simplify internal abstraction method parameters using service selectors in scope of InjectionContext model
* extract InjectAttribute common logic and move it to Injectify package
* move common implementation of the InjectAttribute from Injectify.Microsoft.DependencyInjection to the Injectify package
* add DI framework agnostic service selector implementation as a passed in parameter
* add bootstrap for OnInit, handle multiple OnInit annotations and page cast
* add onInit common implementation, introspection of marked methods using OnInit annotation
* add IOnInit contract, add IInjectable page constraint to be referency type
* add migration guide, release versioning docs
* update docs, add quick start, folder structure and initial docs for migration and samples
* add usage sample of 0.3.0 version

### Other

* bump Injectify.Microsoft.DependencyInjection to v0.4.0
* remove moved annotations references, add reference to service provider extensions
* update bootstrap API methods using InjectionContext model wrapper
* wrap up parameters using InjectionContext model
* update internal parameters API
* mark Bootstrap as obsolete
* Merge pull request #2 from vladhrapov/release/0.3.0

<a name="0.3.0"></a>
## 0.3.0 (2020-9-20)

### Features

* specify strict versions of dependencies
* move DI framework agnostic implementations and helpers to Injectify package, reduce bootstrap amount of code and hide it internally
* move DependencyInjectionHelper as IntrospectionHelper from a specific DI referenced package to Injectify common, add reference to Injectify.Abstractions 0.3.0
* add new package Injectify with common implementations and helpers
* add description of the Injectify.Abstractions package
* add bootstrap for IInject, change core abstractions to be only internal visible
* improve page property injection api, get rid of BootstrapPage method invocation
* simplify Startup api for ConfigureServices method, upgrade from Injectify 0.1.0 to 0.2.0

### Other

* fix versionize reports generation
* bump Injectify.Microsoft.DependencyInjection to 0.3.0 version, add repository url in configurations
* bump Injectify.Abstractions version to release 0.3.0
* add internals visibility of the abstractions for Injectify package
* bump Injectify.Abstractions assembly version and file version to 0.3.0
* remove redundant generic parameters
* add nupkg files as ignored
* Merge pull request #1 from vladhrapov/release/0.2.0
* set reference to Injectify.Microsoft.DependencyInjection 0.2.0

<a name="0.2.0"></a>
## 0.2.0 (2020-9-12)

### Features

* remove TServiceProvider generic type, add collection instantiation inside the library and ConfigureServices method execution
* remove services property from IStartup api, update ConfigureServices to return void
* add sample for 0.x.x lib usage

### Other

* add github reference to the project
* remove binaries, add github reference to the project

<a name="0.1.0"></a>
## 0.1.0 (2020-9-11)

### Bug Fixes

* fix runtime issues during injection and type search

### Features

* add basic implementation
* add basic structure

### Other

* update project info
* update project info, property group, bump version to 0.1.0
* Initial commit

