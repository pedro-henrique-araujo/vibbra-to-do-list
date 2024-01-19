using Microsoft.EntityFrameworkCore;
using VibbraToDoList.Data;
using VibbraToDoList.Repositories;
using VibbraToDoList.Repositories.Imp;
using VibbraToDoList.Services;
using VibbraToDoList.Services.Imp;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VibbraToDoListDbContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("VibbraToDoList");
    optionsBuilder.UseSqlite(connectionString);
});


builder.Services.AddScoped<UserRepository, UserRepositoryImp>();
builder.Services.AddScoped<ToDoListRepository, ToDoListRepositoryImp>();
builder.Services.AddScoped<ToDoListUserRepository, ToDoListUserRepositoryImp>();
builder.Services.AddScoped<ToDoRepository, ToDoRepositoryImp>();
builder.Services.AddScoped<UserService, UserServiceImp>();
builder.Services.AddScoped<ToDoListService, ToDoListServiceImp>();
builder.Services.AddScoped<ToDoListUserService, ToDoListUserServiceImp>();
builder.Services.AddScoped<ToDoService, ToDoServiceImp>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var dbContext = scope.ServiceProvider.GetRequiredService<VibbraToDoListDbContext>();
dbContext.Database.Migrate();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.Run();
