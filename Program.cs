using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// splitting the endpoints into groups
var TDITEM = app.MapGroup("/todoitems");

TDITEM.MapGet("/", GetAllTodos);

TDITEM.MapGet("/complete", GetCompleteTodos);

TDITEM.MapGet("/{id}", GetTodo);

TDITEM.MapPost("/", CreateTodo);

TDITEM.MapPut("/{id}", UpdateTodo);

TDITEM.MapDelete("/{id}", DeleteTodo);

app.Run();

static async Task<IResult> GetAllTodos(TodoDb db){
    return TypedResults.Ok(await db.Todos.Select(td => new TodoItemDTO(td)).ToArrayAsync());
}
static async Task<IResult> GetCompleteTodos(TodoDb db){
    return TypedResults.Ok(await db.Todos.Where(GetAllTodos => GetAllTodos.IsComplete).Select(td => new TodoItemDTO(td)).ToListAsync());
}
static async Task<IResult> GetTodo(int id, TodoDb db){
    return await db.Todos.FindAsync(id)
        is Todo todo ? TypedResults.Ok(new TodoItemDTO(todo)) : TypedResults.NotFound();
}
static async Task<IResult> CreateTodo(TodoItemDTO todoItemDTO, TodoDb db){
    var todoItem = new Todo { IsComplete = todoItemDTO.IsComplete, Name = todoItemDTO.Name };
    db.Todos.Add(todoItem);
    await db.SaveChangesAsync();
    todoItemDTO = new TodoItemDTO(todoItem);
    return TypedResults.Created($"/todoitems/{todoItem.Id}", todoItem);
}
static async Task<IResult> UpdateTodo(int id, TodoItemDTO todoItemDTO, TodoDb db){
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return TypedResults.NotFound();

    todo.Name = todoItemDTO.Name;
    todo.IsComplete = todoItemDTO.IsComplete;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}
static async Task<IResult> DeleteTodo(int id, TodoDb db){
    if(await db.Todos.FindAsync(id) is Todo todo){
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}