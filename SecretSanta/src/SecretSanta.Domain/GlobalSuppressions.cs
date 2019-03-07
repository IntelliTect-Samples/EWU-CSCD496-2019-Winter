
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.CLSCompliant(false)]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Do not directly await a Task", Justification = "No SynchronizationContext in .net core")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity Framework requires public setters to function properly with DTO's and domain model objects.")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "My understanding is that there is no direct way to map System.Uri to a string with EF")]
