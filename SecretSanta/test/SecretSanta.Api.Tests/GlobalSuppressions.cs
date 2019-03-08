
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.CLSCompliant(false)] // Factories not CLS compliant
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Do not directly await a Task", Justification = "ASP.NET core does not have a synchronization context")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Underscores are used in these Unit Tests to provide additional clarity")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "This is a simple property used to allow abstraction for testing. It is ok to allow overwriting of the entire list.", Scope = "member", Target = "~P:SecretSanta.Api.Tests.TestableGiftService.ToReturn")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "<Pending>", Scope = "member", Target = "~M:SecretSanta.Api.Tests.Controllers.GiftControllerTests.ConfigureAutoMapper(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext)")]
