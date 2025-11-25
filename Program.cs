using Gestor_de_Proyectos_Académicos.BLL;
using Gestor_de_Proyectos_Académicos.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSession();

builder.Services.AddScoped<TareaBLL>();
builder.Services.AddScoped<UsuarioBLL>();
builder.Services.AddScoped<TareaDAL>();
builder.Services.AddScoped<UsuarioDAL>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapGet("/", context =>
{
    context.Response.Redirect("/Login/Login");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();
