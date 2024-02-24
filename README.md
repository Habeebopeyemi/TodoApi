## EF Core CRUD Operation Methods

When a `DbContext` is created in Entity Framework Core (EF Core), it provides a comprehensive set of methods for performing CRUD (Create, Read, Update, Delete) operations. These operations are primarily facilitated through the `DbSet<TEntity>` property of the `DbContext`. Below is an overview of the key methods available for each CRUD operation:

### Create Operations

- **`Add(TEntity entity)`**: Adds the specified entity to the context in the Added state, which will be inserted into the database upon calling `SaveChanges()`.

- **`AddRange(IEnumerable<TEntity> entities)`**: Adds a collection of entities into the context, each marked as Added, useful for inserting multiple records at once.

### Read Operations

- **`Find(object[] keyValues)`**: Finds an entity with the given primary key values, returning it without a database call if it's already being tracked by the context.

- **`Where(Expression<Func<TEntity, bool>> predicate)`**: Uses LINQ to query entities from the database based on a predicate (not directly a `DbSet<TEntity>` method but commonly used with it).

- **`FirstOrDefault(Expression<Func<TEntity, bool>> predicate)`**: Finds the first entity matching the predicate, or returns `null` if no match is found.

- **`ToListAsync()`**: An extension method that asynchronously executes a query, returning the results as a `List<TEntity>`.

### Update Operations

- **`Update(TEntity entity)`**: Marks the entity as Modified, with EF Core generating an SQL UPDATE statement upon calling `SaveChanges()`.

- **`UpdateRange(IEnumerable<TEntity> entities)`**: Marks a collection of entities as Modified, useful for updating multiple records at once.

### Delete Operations

- **`Remove(TEntity entity)`**: Marks the entity as Deleted, with EF Core generating an SQL DELETE statement upon calling `SaveChanges()`.

- **`RemoveRange(IEnumerable<TEntity> entities)`**: Marks a collection of entities as Deleted, useful for deleting multiple records at once.

### Other Important Methods

- **`SaveChanges()`**: Persists all changes made in the context to the database, automatically generating the necessary SQL statements for all Added, Modified, and Deleted entities.

- **`SaveChangesAsync()`**: Asynchronously saves all changes made in the context to the database.

---

These methods, part of the `DbContext` and `DbSet<TEntity>` classes, enable developers to perform data access operations efficiently. EF Core also supports advanced features like transactions, concurrency handling, and custom SQL queries for complex scenarios.

# ASP.NET Core `Results` Class Methods

The `Results` class in ASP.NET Core provides a comprehensive set of factory methods for creating various types of HTTP responses. These methods simplify the process of returning common HTTP response types and status codes in minimal APIs. Here's an overview of some key methods provided by the `Results` class, alongside `NotFound` and `Ok`:

### `BadRequest(object? error)`
- Produces an HTTP 400 (Bad Request) status code response. Can include an error object in the response body.

### `Content(string? content, string? contentType = null)`
- Returns a result with a custom content string. Allows for an optional content type and defaults to an HTTP 200 (OK) status code.

### `Created(string uri, object? value)`
- Generates an HTTP 201 (Created) status code, indicating a new resource was created. Includes a `Location` header and optionally the resource in the response body.

### `NoContent()`
- Returns an HTTP 204 (No Content) status code, indicating a successful request with no content to return.

### `Json(object? data, JsonSerializerOptions? options = null, string? contentType = null, int? statusCode = null)`
- Serializes the provided data object to JSON for the response body. Allows specifying content type, status code, and serialization options.

### `Redirect(string url, bool permanent = false)`
- Produces an HTTP 302 (Found) or 301 (Moved Permanently) status code for redirects. The `permanent` flag determines the specific status code.

### `Unauthorized()`
- Generates an HTTP 401 (Unauthorized) status code, indicating missing or invalid authentication credentials.

### `Forbidden()`
- Returns an HTTP 403 (Forbidden) status code, indicating the server refuses to authorize the request.

### `Challenge()`
- Initiates an authentication challenge, typically used when authentication is required and the application wants to trigger the process.

### `Problem(string? detail = null, string? instance = null, int? statusCode = null, string? title = null, string? type = null)`
- Produces a problem details object for conveying error details in a machine-readable format. Often used with the `application/problem+json` media type.

These methods facilitate handling various outcomes and HTTP status codes in a standardized and concise manner, enhancing the development experience with minimal APIs in ASP.NET Core.
