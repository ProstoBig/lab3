using Lab3.Services;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<DataReader>();

var app = builder.Build();

// Logging configuration
Trace.Listeners.Add(new TextWriterTraceListener("./App_Data/data_trace.log"));
Trace.AutoFlush = true;
Trace.WriteLine($"Started at {DateTime.Now}:");
Trace.Indent();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.Run();
