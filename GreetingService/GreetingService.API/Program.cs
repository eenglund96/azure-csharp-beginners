using GreetingService.Core.Interfaces;
using GreetingService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//This is a new concept we haven't covered so far. Dependency Injection is built into ASP.NET. This code is executed when the application starts 
//Here we "register" i.e. we tell our application to use FileGreetingRepository as the implementation for IGreetingRepository
//We also get the FileRepositoryFilePath config value from appsettings.json and use it to construct our FileGreetingRepository
//This plumbing allows us to get the correct IGreetingRepository in our constructor in GreetingController
//builder.Services.AddScoped<IGreetingRepository, FileGreetingRepository>(c => 
//{
//    var config = c.GetService<IConfiguration>();
//    return new FileGreetingRepository(config["FileRepositoryFilePath"]);
//});

//Writing to file system in a PAAS service like Azure App Service can be hard to get right because we don't have 100% control of where things are executing and also don't have 100% permissions.
//Use MemoryGreetingRepository for now when running in App Service.
//Register this as a Singleton to ensure all requests use the same instance for reading/writing
builder.Services.AddSingleton<IGreetingRepository, MemoryGreetingRepository>();

builder.Services.AddScoped<IUserService, HardCodedUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
