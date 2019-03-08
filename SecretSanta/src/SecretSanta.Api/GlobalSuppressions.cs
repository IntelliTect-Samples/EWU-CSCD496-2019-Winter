
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.CLSCompliant(false)] // Migration is not CLS compliant
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Do not directly await a Task", Justification = "ASP.NET core does not have a synchronization context")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "Arguably in this circumstance this improves readability. The Get verb is necessary for clean API coding suggestions.", Scope = "member", Target = "~M:SecretSanta.Api.Controllers.UsersController.GetUser(System.Int32)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.ActionResult{SecretSanta.Api.ViewModels.UserViewModel}}")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1060:Move pinvokes to native methods class", Justification = "This is a simple helper file. In the future it may be wise to extract this and similar calls to a NativeMethods class", Scope = "type", Target = "~T:SecretSanta.Api.Models.CurrentDirectoryHelpers")]

