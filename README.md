# Library Management System
## This Repository Contains a very simple ** Library Management System ** Developed as an ASP.NET MVC 5 Application

### The main System user is a Librarian who can take the following Actions:
- Add A new Book to the Database
- Add A new Borrower to the Database
- Perform a Borrowing operation
- Perform a Returning operation

### For a Book to be added:
- Total Copies number must be greater than 0 
- The Available Copies must be less than the Total Copies Number

### For a Borrowing action to be done:
- The Book name must be listed
- The Book Available Copies must be greater than zero
- and the Book must be available for borrowing

### For a Returning action to be done:
- Book name & Borrower name should be on the list
- A matching record with the same Book name & Borrower name Must be found
- The matching record should have a property ** DeliveredBack ** equals * false *

#### There are also a class library project for Unit Testing which contains tests for only the ** Borrowings Controller **
#### Testing all the cases of Borrowing a Book OR Returning it Back
