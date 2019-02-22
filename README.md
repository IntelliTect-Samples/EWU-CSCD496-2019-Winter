# EWU-CSCD496-2019-Winter

[![Join the chat at https://gitter.im/IntelliTect/CSCD496-2019-Winter](https://badges.gitter.im/IntelliTect/CSCD496-2019-Winter.svg)](https://gitter.im/IntelliTect/CSCD496-2019-Winter?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)


## Assignment 7

For this assignment start from the existing intellitect/Assignment7 branch.

- Create controllers and views to do the following
  - Users
     - Display all users
     - Add a user
     - Edit a user
     - Remove a user
  - Groups
     - Display all grpups
     - Add a user
     - Edit a user
     - Remove a user
- Create a page for managing a users gifts
   - This will be done using VueJs and will be using typescript for sending data to and from the API
     - Show all gifts
     - Add a gift
     - Edit a gift
     - Remove a gift
- The web application should use the generated classes that are created from nswagstudio (or other tool)
- If I enter invalid data (empty first name for a user) it should not save off the data and should tell me about the error
- No unit tests are expected for this since most of the logic is actually already tested in the API

### Going above and beyond
- Create screens that allow you to manage which users are associated with different groups. Remember, since it is a many to many, users can belong to more than one group. This can be done either using the server side calls or the javascript calls, dealers choice.

### Useful Stuff

- [NSwagStudio](https://github.com/RSuter/NSwag/wiki/NSwagStudio)
- [NodeJS](https://nodejs.org/en/)
- [Bulma Documentation](https://bulma.io/documentation/)
- [vue.js](https://vuejs.org/)

