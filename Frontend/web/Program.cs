using Blazored.LocalStorage;
using MudBlazor.Services;
using Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddServerComponents();
builder.Services.AddAuthenticationCore();
builder.Services.AddMudServices();
builder.Services.AddSystemd();
builder.Services.AddBlazoredLocalStorage();
var ApiString = new Uri(builder.Configuration.GetConnectionString("ApiConnection"));
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("_myAllowSpecificOrigins", builder => builder.WithOrigins(ApiString.OriginalString)
         .AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials());
});

builder.Services.AddHttpClient("CleanAPI", (sp, cl) =>
{
    cl.BaseAddress = ApiString;
});
builder.Services.AddScoped(
    sp => sp.GetService<IHttpClientFactory>().CreateClient("CleanAPI"));
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapRazorComponents<App>();

app.Run();
