using BusinessLogic;
using EfcRepositories;
using FileRepositories;
using RepositoryContracts;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>(); // Add middleware to handle exceptions.
builder.Services.AddOpenApi();

// Add Repositories to the services.
builder.Services.AddScoped<UsersService, UsersService>();
builder.Services.AddScoped<IUserRepository, UserEfcRepository>();

builder.Services.AddScoped<PostsService, PostsService>();
builder.Services.AddScoped<IPostRepository, PostEfcRepository>();

builder.Services.AddScoped<CommentsService, CommentsService>();
builder.Services.AddScoped<ICommentRepository, CommentEfcRepository>();

builder.Services.AddDbContext<ForumContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();