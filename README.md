## Requirements
This project fulfills the following requirements:

1. Front end must be a react application
2. Back end must be a dotnet application: Must include GET, POST and PUT api calls
4. Must have data persistence to a database.  This can be a SQL lite database, MS SQL, MySQL, Mongo, etc.  You can use multiple databases if you'd like.
5. The Entity Framework must handle all communication between the dotnet application and the database
6. Your application must have a login page with support of Oauth2.0 Authentication and handle multiple users, it should have a workflow for creating a new account, Authenticating against that new account,and being able to login with that new account
   
## Project:
Bank / ATM application

## Architecture
<code>
 +----------------------------------------+                     +-------------------+
 |                                        |  HTTP Request       |                   |
 |       Frontend (View) - React          |<------------------->|  Controller (.NET)|
 |  - Handles user interface              |                     |  - Handles requests|
 |  - Sends HTTP requests to backend      |  HTTP Response      |  - Calls Model to  |
 |  - Receives data via REST API          |<------------------->|    process data    |
 |                                        |                     +---------+---------+
 +-------------------+--------------------+                               |
                     |                                                Data Interaction
                     v                                                   with MongoDB
 +---------------------------------+                       +------------+-------------+
 |                                 |                       |                          |
 |            Model                |                       |        MongoDB           |
 |     - Represents data and       |   Read/Write          |     - NoSQL Database     |
 |       business logic            |<--------------------->|     - Stores user data   |
 |     - Communicates with DB      |                       |       (credentials, etc.)|
 |                                 |                       |                          |
 +---------------------------------+                       +--------------------------+
 </code>

  
## Video
https://www.loom.com/share/8c18860d0d584c9085093dd631bd02a7?sid=370b6cb2-786f-4618-9a6d-c48747612b2d
