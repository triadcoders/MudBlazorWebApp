using System.Diagnostics;
using MudBlazor.Services;
using MudBlazorWebApp;
using MudBlazorWebApp.Client.Pages;
using MudBlazorWebApp.Components;
using MudBlazorWebApp.Service;
using MudBlazorWebApp.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

//builder.Services.AddMudServices();

builder.Services.AddMudServices(config =>
{
    // config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    //  config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
builder.Services.AddSingleton<IMessageController, MessageController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.MapGet("/Hello", () => "Hello, World!");
//https://khalidabuhakmeh.com/razorslices-razor-views-with-aspnet-core-minimal-apis

TimeSpan ts = new();
Stopwatch stopwatch = new();


app.MapGet("/StopWatch", () =>
{
    MyDbContext dbContext = new MyDbContext();

    Customer myCustomer = new Customer() { Id = 2, Name = "FRED" };

    dbContext.Update(myCustomer);
    dbContext.SaveChanges();
    
    dbContext.Customers.Add(myCustomer);
    
    if (stopwatch.IsRunning == false)
    {
        stopwatch.Start();
    }
    else
    {
        ts = stopwatch.Elapsed;
        if (ts.Seconds <= 10)
        {
            int diffTs = 10 - ts.Seconds;
            Thread.Sleep(int.Abs(diffTs) * 1000);
            stopwatch.Stop();
            stopwatch.Reset();
        }
    }

    stopwatch.Reset();
    stopwatch.Start();
    return $"The End Point Ran again - {DateTime.Now}";
});


app.Run();