This assignment is going to set the foundation for the rest of the quarter and the Secret Santa project. In this section, you will be building out the data/domain layer that will contain the information we use later on in the API's and UI.

## Objects we will need to create
- User
  - hold the user's first and last name, list of gifts, groups the user belongs to
- Gift
  - hold the title, order of importance, url, description, and User
- Group
  - Will have a title and a list of User's who are part of that group. Users can belong to more than one group
- Pairing
  - Holds the User who is the recipient and the User who is the Santa for each group
- Message
  - Chat used so that Recipient and Santa can hold an anonymous discussion

## Services needed to start
- Users
  - Need to be able to add and update a user, not worried about deleting
- Groups
  - Need to be able to create a group and add/remove users from the group
- Gifts
  - Need to be able to create/edit/delete a gift for a particular user
- Pairing
  - Need to be able to create a pairing for a group that associates a recipient to a santa (no need to validate at the moment that pairings are unique)
- Messages
  - Need to be able to store messages between a recipient and santa

All services need to be unit tested to verify the above functionality

Make sure you follow the [coding standards](https://github.com/IntelliTect-Samples/EWU-CSCD496-2019-Winter/wiki/Coding-Guidelines)