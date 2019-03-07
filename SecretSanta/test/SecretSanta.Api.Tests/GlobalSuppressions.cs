
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.CLSCompliant(false)] // Justification: WebApplicationFactory and IWebHostBuilder are not CLS-compliant and they must be used.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Do not directly await a Task", Justification = ".NET Core does not use synchronization contexts.")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "This collection is not used in the same sense as a normal collection (where we care about the data).  All we really care about is the pointer changing.", Scope = "member", Target = "~P:SecretSanta.Api.Tests.TestableGiftService.ToReturn")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Underscore used to enhance test method name description.")]


