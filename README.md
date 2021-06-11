# Musicalog Music Store Catalog Web App

## What's Included

1. **Database**

   By request, the database was created first. There is no EDMX file in .NET Core, but the models and context were auto-generated using the `scafford` option of the `dotnet ef` command-line interface.

   The `Musicalog` database should be created using the one-and-only script available here:
   ```
   Musicalog\Musicalog.Data\SqlScripts\CreateDatabase.sql
   ```

1. **Test harness**

   When the main `Musicalog` project is running a Swagger UI is available here:

   ```
    http://localhost:5000/swagger 
   ```
    This includes the Try It Out feature.

1. **Project structure**

   |Project|Purpose|
   |-|-|
   |`Musicalog`|The Web API project|
   |`Musicalog.Core`|Class Library containing request handlers and associated types, and generic services|
   |`Musicalog.Core.Tests`|Class Library containing tests for the `Musicalog.Core` project|
   |`Musicalog.Data`|Class Library containing the DB Context, repositories, SQL script|
   |`Musicalog.Domain`|Class Library containing entity types|
   |`Musicalog.TestUtilities`|Class Library containing the `MockBase` unit test base class|
   
    To keep the Web API controllers clean, each request is handled by its own implementation of `IRequestHandler<>`. This represents the biggest design decision pattern in how to organize the code. The deeper folder/file structure in `Musicalog.Core` is worth examining in this regard.

1. **Unit tests**

    There wasn't time to add lots of unit tests. Instead, a single deep example is available in `AddAlbumCommandHandlerTests` (`Musicalog.Core.Tests` project). It includes a reusable implementation of auto-mocking in its base class that can be used in most unit tests. The base class is in project `Musicalog.TestUtilities`.
    
1. **Middleware**

   `NotFound` exceptions are handled automatically via middleware, which the repository base class throwing an exception as necessary. This makes the request handlers easier to unit test, because we only need to verify that the `SingleAsync` method is called.

   We don't have to stop there. We could for example handle database key violations encounted when saving in the DbContext and return them as `409/Conflict`, with a bit of work.

1. **Validation**

    Some validation has been implemented via the `FluentValidation` package. This is handled by custom middleware. For example, you can't add an album with an empty `Title` or set `Stock `to less than zero.


## Thoughts About What's NOT Included ##

1. If the project's going to get *really* big, we might consider using Domain Driven Design (DDD). Everyone involved needs to agree in order for this to work.
1. Database normalization: we don't yet have much to go on, but at a minumum we'd expect the `Album` table to reference a separate Artist table.
1. Authentication and authorization
1. There would likely be much more shared code, probably in its own local project.
1. SignalR: however, it's an example of a reason we might want to call repository methods indirectly via services that additionally make SignalR callbacks.
1. Versioning support
1. Automated end-to-end acceptance tests
1. Error handling for when we already have an artist/title (affects additions and updates).
1. Commits are absolutely ad hoc! Normally, we'd a expect a bit more organization as each PR is completed.

