
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1060:Move pinvokes to native methods class", Justification = "There is no need to make the suggested changes.", Scope = "type", Target = "~T:SecretSanta.Api.Models.CurrentDirectoryHelpers")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "URL strings are to be used until a need to use URI has raised.", Scope = "member", Target = "~P:SecretSanta.Api.ViewModels.GiftInputViewModel.Url")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "URL strings are to be used until a need to use URI has raised.", Scope = "member", Target = "~P:SecretSanta.Api.ViewModels.GiftViewModel.Url")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "This suggestion will break the code for the API's EF, thus the Collection property is remain writable.", Scope = "member", Target = "~P:SecretSanta.Api.ViewModels.GroupViewModel.GroupUsers")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "This is done to prevent the need to regenerate the SecretSantaClient.cs file.", Scope = "member", Target = "~M:SecretSanta.Api.Controllers.UsersController.GetUser(System.Int32)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.IActionResult}")]

