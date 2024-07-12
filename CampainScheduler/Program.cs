using CampainScheduler.DAL.Contexts;
using CampainScheduler.Utils;
using CampainScheduler.Application;
using CampainScheduler.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<CampainSchedulerContext>();

// Register all dependent projects
builder.Services.AddUtils();
builder.Services.AddApplication();
builder.Services.AddDAL();

var app = builder.Build();

app.SetupDAL();

// is needed only for debug
app.SetupApplication();

app.UseAuthorization();
app.MapControllers();

app.Run();
