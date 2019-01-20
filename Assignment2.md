# Assignment 1

The purpose of this assignment is to practice the discipline of test driven development and, more generally, unit testing  To achieve this we will write create a library project that imports a file that contains a wish list of gifts for a specific person.

## Details

Create a unit testing project and a system under test library project that will import a file with a wish list of gifts (add both projects to the existing solution).  Given a valid import file, the "header" (the first line) will contains the name of the person in one of two formats:
    `Name: <FirstName> <LastName>
    Name: <LastName>, <FirstName>`
You can assume all remaining lines will be items for the wish list except that all blank lines should be ignored.

## Notes

* Paring is encouraged
* Consider checking your code coverage to verify you are not writing code that doesn't have a corresponding test.
* Leveraging the existing Objects & Services from Assignment 1 is encouraged
* Avoid "import" related unit tests that access the database.
* Write tests that check your APIs appropriately handle files missing and invalid headers.
* Pay attention as to whether validation belongs in your Objects (such as your `User` or `Gift` object or in the import logic) and create tests accordingly.

## Going Above and Beyond

The following are ideas of items to consider for going above and beyond (in addition to the standard of writing awesome code :)

* Turn on all warnings as errors in your project.
* Turn on code analysis, selecting the rules that makes sense for this project.

In the future, the above items will be expected for all homework.

Make sure you follow the [coding standards](https://github.com/IntelliTect-Samples/EWU-CSCD496-2019-Winter/wiki/Coding-Guidelines)