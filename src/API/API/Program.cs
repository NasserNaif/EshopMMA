var builder = WebApplication.CreateBuilder(args);

// Add Services to the container (Dependincy Injection DI)
var app = builder.Build();

// Configure the HTTP request pipeline

app.Run();
