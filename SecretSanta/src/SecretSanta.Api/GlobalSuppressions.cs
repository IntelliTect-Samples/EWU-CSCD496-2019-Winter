
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "<Pending>", Scope = "member", Target = "~M:SecretSanta.Api.Startup.ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>", Scope = "member", Target = "~M:SecretSanta.Api.Startup.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder,Microsoft.AspNetCore.Hosting.IHostingEnvironment)")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "<Pending>", Scope = "member", Target = "~M:SecretSanta.Api.Controllers.UsersController.GetUser(System.Int32)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.ActionResult{SecretSanta.Api.ViewModels.UserViewModel}}")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1060:Move pinvokes to native methods class", Justification = "<Pending>", Scope = "type", Target = "~T:SecretSanta.Api.Models.CurrentDirectoryHelpers")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<Pending>", Scope = "member", Target = "~P:SecretSanta.Api.ViewModels.GiftInputViewModel.Url")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<Pending>", Scope = "member", Target = "~P:SecretSanta.Api.ViewModels.GiftViewModel.Url")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Pending>", Scope = "member", Target = "~P:SecretSanta.Api.ViewModels.GroupViewModel.GroupUsers")]