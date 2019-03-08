﻿
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Do not directly await a Task", 
    Justification = "No SynchronizationContext in .net core")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1052:Static holder types should be Static or NotInheritable", 
    Justification = "Class Program contains Main method.", Scope = "type", Target = "~T:SecretSanta.Web.Program")]