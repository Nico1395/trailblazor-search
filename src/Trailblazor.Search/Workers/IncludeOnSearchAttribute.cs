namespace Trailblazor.Search.Workers;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public sealed class IncludeOnSearchAttribute : Attribute { }
