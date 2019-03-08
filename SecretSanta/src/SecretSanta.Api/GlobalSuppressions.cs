
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "<I believe it's good naming for generating the user as properties don't have explicit getter/setters like in Java>", Scope = "member", Target = "~M:SecretSanta.Api.Controllers.UsersController.GetUser(System.Int32)~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.ActionResult{SecretSanta.Api.ViewModels.UserViewModel}}")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1060:Move pinvokes to native methods class", Justification = "<I didn't believe it's necessary to move the aleady established class for this reason. Due to the visbility of the class, having as an internal class I believe for this small project is okay for more decoupled design>", Scope = "type", Target = "~T:SecretSanta.Api.Models.CurrentDirectoryHelpers")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "<Having as read only in our case was not possible as we needed to assign later in program.>", Scope = "member", Target = "~P:SecretSanta.Api.ViewModels.GroupViewModel.GroupUsers")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", Justification = "<Not Good Practice to make classes as Static unless good reason.>", Scope = "type", Target = "~T:SecretSanta.Api.Program")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Not Good Practice to make classes as Static unless good reason. In this case I didn't see one.>", Scope = "member", Target = "~M:SecretSanta.Api.Startup.Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder,Microsoft.AspNetCore.Hosting.IHostingEnvironment)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "<In this context, I was unable to understand and follow the ctr+. suggestion.>", Scope = "member", Target = "~M:SecretSanta.Api.Startup.ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<Pending>", Scope = "member", Target = "~P:SecretSanta.Api.ViewModels.GiftInputViewModel.Url")]

