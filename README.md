# Musicalog Music Store Catalog Web App

## What's included

1. **Database**

   By request, the database was created first. There is no EDMX in .NET Core but the models and context were auto-generated.

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
   
    To keep the Web API controllers clean, each request is handled by its own implementation of `IRequestHandler<>`. The deeper folder/file structure in `Musicalog.Core` is worth examining in this regard.



1. **Unit tests**

    There wasn't time to add lots of unit tests. Instead, a single deep example is available in `AddAlbumCommandHandlerTests` (`Musicalog.Core.Tests` project). It includes a reusable implementation of auto-mocking in its base class that can be used in most unit tests. The base class is in project `Musicalog.TestUtilities`.
    
## What's NOT included ##

1. Paging hasn't been implemented.
1. Authentication and authorization
1. There would likely be much more shared code, probably in its own local project.
1. SignalR
1. Versioning support
1. Automated end-to-end acceptance tests
1. If the project's going to get big, we might consider using Domain Driven Design (DDD). Everyone involved needs to understand DDD for this to work.
1. Database normalization: we don't yet have much to go on, but at a minumum we'd expect the `Album` table to reference a separate Artist table.
