using System;

namespace SingletonDI;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class SingletonAttribute : Attribute;
