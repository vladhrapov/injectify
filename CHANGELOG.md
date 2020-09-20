# Change Log\n\nAll notable changes to this project will be documented in this file. See [versionize](https://github.com/saintedlama/versionize) for commit guidelines.\n
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

