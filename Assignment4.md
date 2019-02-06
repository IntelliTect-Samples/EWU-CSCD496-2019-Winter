This assignment is going to focus on refining code that has already been written. There is
an application that has already been started in the Assignment4 branch and this will be
your starting point.

Peer code reviews will not be required for this assignment, so you won't need to have
things turned in until Thursday at midnight

## Enhancements to Swashbuckle documentation
- Add a reference to Microsoft.AspNetCore.Mvc.Api.Analyzers and make sure there are no warnings being generated
- Add proper ProducesResponseType and Produces attributes
  - this can be done individually or by applying the global ApiConventionType attribute to the assembly and using the DefaultApiConventions

## Swap out custom transformation code for Automapper *
- Add a reference to AutoMapper and AutoMapper.Extensions.Microsoft.DependencyInjection
- Configure mappings and replace custom tranform calls with Mapper.Map calls

## Configure aspnet core project to use physical sqlite database
- Configure the Sqlite connection to use a physical connection string
- Create an initial migration
- Move the Database.EnsureCreated out of the ApplicationDbContext constructor

## Modify models and viewmodels for data integrity
- Changes that should be made at the database level
  - Group should require a Name and it should be unique
  - Gift should require a Title, but should not require an OrderOfImportance
  - Create a second migration with the updated Model changes
- Changes that should only be applied at the viewModel level
  - User should require a FirstName
  - Create unit tests to verify that viewModel requirements are working correctly

## Modify all controller actions to return an IActionResult instead of ActionResult or ActionResult&lt;T&gt;
