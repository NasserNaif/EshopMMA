


var builder = WebApplication.CreateBuilder(args);

// Add Services to the container (Dependincy Injection DI)

// add and register Carter to the API module

// ## NEW WAY TO REGISTER CARTER MODULES DYNAMICALLY
builder.Services
    .AddCarterModulesWithAssembles(typeof(CatalogModule).Assembly);

// Add the modules dependencies ( Catalog, Basket, Ordering ) to the API module
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

 
var app = builder.Build();

// Configure the HTTP request pipeline

// Carter middleware

app.MapCarter();
// configure each module pipeline
app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();


app.Run();
