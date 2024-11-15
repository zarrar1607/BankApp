[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/e8CdihvW)
[![Open in Codespaces](https://classroom.github.com/assets/launch-codespace-2972f46106e565e64193e422d61a12cf1da4916b45550586e14ef0a7c637dd04.svg)](https://classroom.github.com/open-in-codespaces?assignment_repo_id=16991446)
# csc436_FinalProject

## Requirements
Choose a project that fulfills the following requirements:

	1. Front end must be a react application
	2. Back end must be a dotnet application
		a. Must include GET, POST and PUT api calls
	3. Must have data persistence to a database.  This can be a SQL lite database, MS SQL, MySQL, Mongo, etc.  You can use multiple databases if you'd like. 
	4. The Entity Framework must handle all communication between the dotnet application and the database
	5. Your application must have a login page with support of Oauth2.0 Authentication and handle multiple users.
		a. It should have a workflow for creating a new account
		b. Authenticating against that new account
		c. Being able to login with that new account
	6. Must have multiple pages and/or views
	7. Project demo will be live during week 10 or by video and submitted via D2L
		a. Submission will require a document or PowerPoint presentation describing your project
			i. Describe each of the above requirements and how your project meets them
			ii. Why did you choose this specific project
			iii. If you had more time, what would you do differently
		b. Project  source code must be committed to GitHub.  If you submit a presentation but do not commit source code then you will receive a 0%.  


## Example projects:
	• Blogging Platform
	• Bank / ATM application
	• Personalized Weather Application
	• Chat application
	• Gradebook supporting multiple students
	• Reservation system (restaurants, equipment, cars, etc.)
	• Shopping application (limit the scope)
	• Finance app (Display stocks, Crypto etc.)
	• Crime alert by location/user
	• Book Review site
	• Resale of used items (aka Craigslist )

# Architecture
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

