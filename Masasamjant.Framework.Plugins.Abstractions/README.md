# Masasamjant Framework Plugin Abstractions
This project provides abstractions and interfaces for building plugins.
It is using .net standard 2.0 and can be used in any .net standard 2.0 compatible project.
To use plugins at runtime, you need to use Masasamjant.Framework.Plugins.Runtime package or implement your own plugin loader.
To implement your own plugin loader, you can use the interfaces provided in this package or use base plugin in Masasamjant.Framework.Plugins.Core package.