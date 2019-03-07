
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.CLSCompliant(false)]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Do not directly await a Task", Justification = ".NET Core does not use synchronization contexts.")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "Method name is only meant to match HTTP verbs.")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "We need this to be settable so that AutoMapper can work properly.", Scope = "member", Target = "~P:SecretSanta.Api.ViewModels.GroupViewModel.GroupUsers")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "We are only accepting Url's and shouldn't have any parsing or encoding errors.  However it would be wise to refactor to Uri type if given the chance.  Also, as I currently understand, EF would have trouble mapping a type of Uri.")]
