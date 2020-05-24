# LocalizationServer

## Current Database
The database is hosted on Azure using its SQL Database services. This is a code-first database, therefore you can see the `Migration` folder with the history of changing occurred.

Currently there is a table `Student` which records students' details, some mock data has been added. To see the current data, simply use the `GET` request, which is described in the next part.

## Current existing API
In order to test the API, please clone and run the App locally and test the following APIs with Postman

`GET`: To get all students or get student by ID:
* API:
	* https://localhost:44387/api/Students
	* https://localhost:44387/api/Students/1

`PUT`: To change details for a student by ID
* API: https://localhost:44387/api/Students/1
* Header		
	* `Content-Type: application/json`
* Body
```json
{
	"StudentId": 1,
	"FirstName": "Bob",
	"LastName": "Michael 1",
	"Email": "nn@abcmail.com",
	"Summary": "This student comes from Vietnam"
}
```

`POST`: To create a new student instance
* API: https://localhost:44387/api/Students		
* Header		
	* `Content-Type: application/json`		
* Body
```json
{
	"FirstName": "Nguyen",
	"LastName": "Nguyen",
	"Email": "nn@abcmail.com",
	"Summary": "This student comes from Vietnam"
}
```

`DELETE`: To delete a specific student with ID
* API: https://localhost:44387/api/Students/1

## To see the API response in other languages:

I have set up 3 different languages that this simple API application supports: English, Spanish, and French. The API requests that have been implemented with this support is GET and DELETE.

*Note: I don't know much about other languages so the translation is not 100% correctly. However, of course we can adjust if needed*

There are 2 ways to see this implementation:		
**Please make sure that the id input into these requests must not be in the database.**

`GET`: Simply call the following APIs
* French: https://localhost:44387/api/Students/1000?culture=fr-FR
* Spanish: https://localhost:44387/api/Students/1000?culture=es
* English: https://localhost:44387/api/Students/1000?culture=en-US

If there is no parameter `culture` defined, default English will be applied

Or in the `Header` of the `GET` request, add in:
* French: `Accept-Language: fr-FR`
* Spanish: `Accept-Language: es`
* English: `Accept-Language: en-US`

`DELETE`:
* French: https://localhost:44387/api/Students/1000?culture=fr-FR
* Spanish: https://localhost:44387/api/Students/1000?culture=es
* English: https://localhost:44387/api/Students/1000?culture=en-US

If there is no parameter `culture` defined, default English will be applied

Or in the `Header` of the `DELETE` request, add in:
* French: `Accept-Language: fr-FR`
* Spanish: `Accept-Language: es`
* English: `Accept-Language: en-US`
