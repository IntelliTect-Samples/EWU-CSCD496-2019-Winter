
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.CLSCompliant(false)]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Do not directly await a Task",
    Justification = "No SynchronizationContext in .net core")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores",
    Justification = "Naming convention not applicable to descriptive test methods.")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only",
    Justification = "Class is just used as a Mock", Scope = "member", Target = "~P:SecretSanta.Api.Tests.TestableGiftService.ToReturn")]
