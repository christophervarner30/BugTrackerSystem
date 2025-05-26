var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
// Make sure the default route goes to our Bug controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Bug}/{action=Index}/{id?}");

// Redirect root URL to Bug controller
app.MapGet("/", context => {
    context.Response.Redirect("/Bug");
    return Task.CompletedTask;
});
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
