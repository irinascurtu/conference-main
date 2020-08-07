dotnet run --launch-profile "QA" --project Conference.Api
difference between launch-profile and build
introduction
- what REST is about : using HTTP at its full power
1. Verbs
- very important, when using verbs with getbyId, you'll get an error, because webapi matches the verb first and then the parameter
- -so using httphead on getbyid, but calling it on /speakers will hit the action
2. StatusCodes
3. ApiBehavior
4. Versioning
5. Hateoas
- Content-Negotiation
- HAL media types

6. OData
