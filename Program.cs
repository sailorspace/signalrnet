using signalrnet.hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddSignalR();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
var app = builder.Build();
//app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.MapHub<MessageHub>("/messages");
app.UseMvc();
app.Run();
