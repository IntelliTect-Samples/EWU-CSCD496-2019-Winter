## Assignment 5

For this assignment start from the existing intellitect/Assignment5 branch.

- Update all of the service classes to use the async EF (Entity Framework) methods. 
  - This change will spiral up to the unit tests and controllers.
  - You only need to update the existing unit tests to support async. It is not requires that you add or modify them beyond this.
- Create a new PairingController
  - It should expose a single endpoint to generate pairings for users in a group.
  - The endpoint should accept a groupId
  - You will need to create a pairing view model
  - This controller should be unit tested
- Create a new PairingService class to do the actual work
  - The pairing service should have a single async method that generates pairing for all users in a group.
  - This method **must** be thread safe
  - Because generating the pairings could take some time, the work of generating the pairings should be done inside of a Task. 
  - The pairings should be randomized.
  - For a collection of pairings to be valid, every person in the group should be the Santa for exactly one other person. And every person should be the Recipient of exactly one other person. And no person can be their own Santa.
  - This service should be unit tested

### Going above and beyond
- Ensure that there are not multiple loops in the pairings. That is, if you were to follow each pairing (going from the Santa to the Recipient) you should end up back at the person you started with. 
For example: John, Jane, Jim, Jaun
Pairing with one loop:
John => Jane
Jane => Jim
Jim => Jaun
Jaun => John
Pairing with two loops:
John => Jane
Jane => John
Jim => Jaun
Jaun => John
- The existing controllers and sevices are not fully unit tested. Update the existing unit tests so they are fully tested.
- The current Pairing object does not track what group created it. Add the ability to associate the pairing to the group it was created for. Test to ensure that you can add two pairings that differ only by the group.

### Useful Stuff

- Chapters 19/20 from the book
- [Task-based Asynchronous Pattern (TAP)](https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap)
- [Task Parallel Library (TPL)](https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-parallel-library-tpl)
