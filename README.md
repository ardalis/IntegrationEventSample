# IntegrationEventSample

Sample showing Domain Events and Integration Events with Web API 2

This sample shows how to use domain events and event handlers to add additional context information to events prior to sending them outside of the current domain/application. In this case, a domain event is raised when a student registers for a course, which you can test by using Swagger and doing a POST to the TrainingCourses node after creating a Student and a TrainingCourse.

The `TrainingCourse` entity has a `Register` method that adds students to a collection and also raises an event:

```
    public void Register(Student student)
    {
        RegisteredStudents.Add(student);
        DomainEvents.Raise(new NewStudentRegistrationEvent(student, this));
    }
```

Note that this entity has no knowledge of any web request or UI concerns, nor does this method include any way of knowing who the identity is of the user performing the action. However, a separate application needs to be able to show an Activity Log that includes actions like this one, along with who performed the action.

The event handler is responsible for this, and accomplishes the task by getting the user information from the `HttpContext` and creating a new data type that includes the original information as well as the user information. This could easily be done as JSON and sent through a message queue or bus. In this example the `NewsfeedItem` type is just another entity in the system to keep the demo simple.

The user info in the demo is faked from the site's home page. Visit there and a Guid userName will be created along with a time stamp and stored in a cookie. If you want to modify the default userName, pass a querystring with a value for `userName`.

## MediatR

I've also wired up MediatR to show how it would work in this scenario. It could be used to completely replace the static `DomainEvents` class, but for now I have a separate `DomainEvents` handler configured to create a denormalized message and then to send this message using MediatR. The MediatR handler then finishes the task of writing the information to the Newsfeed, resulting in output like this:

```
    {
        "Title": "Student Steve Smith registered for course Intro to C#",
        "UserInfo": "2209c5d9-afcb-4175-9570-971e5a0b596c:6/13/2018 3:06:03 PM (from MediatR)",
        "Id": 3
    },
    {
        "Title": "Student Steve Smith registered for course Intro to C#",
        "UserInfo": "2209c5d9-afcb-4175-9570-971e5a0b596c:6/13/2018 3:06:03 PM",
        "Id": 4
    }
```
