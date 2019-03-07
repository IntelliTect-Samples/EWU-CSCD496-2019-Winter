
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.CLSCompliant(false)]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Do not directly await a Task", Justification = "No SynchronizationContext in .net core")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "Method naming conventions are meant to match web API expected HTTP verb GET, not a usual c# getter method. " + "Naming also assists Swagger documentation(NSwag).")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "EF will not map URI to string")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Class is meant to be mutable (mostly because of automapper)", Scope = "member", Target = "~P:SecretSanta.Api.ViewModels.GroupViewModel.GroupUsers")]