# LocalizationServer

## Current Database

## Current existing API
`GET`: To get all students or get student by id:
* https://localhost:44387/api/Students
* https://localhost:44387/api/Students/1
`PUT`:

```json
{
	"StudentId": 1,
	"FirstName": "Bob",
	"LastName": "Michael 1",
	"Email": "nn@abcmail.com",
	"Summary": "This student comes from Vietnam"
}
```
`POST`:
Header
Content-Type: application/json
Body
```json
{
	"FirstName": "Nguyen",
	"LastName": "Nguyen",
	"Email": "nn@abcmail.com",
	"Summary": "This student comes from Vietnam"
}
```

DELETE:

## To see the API response in other language:
GET:
https://localhost:44387/api/Students/1000?culture=fr-FR
https://localhost:44387/api/Students/1000?culture=es
https://localhost:44387/api/Students/1000?culture=en-US

Or:
  Header:
    Accept-Language: fr-FR
  Header:
    Accept-Language: es
  Header:
    Accept-Language: en-US


Note: I don't know much about other languages so the translation is not 100% correctly
