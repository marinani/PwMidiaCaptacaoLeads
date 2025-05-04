using PwMidiaCaptacaoLeads.Aplicacao.Servicos;

var builder = WebApplication.CreateBuilder(args);



// Configurações de credenciais
builder.Services.AddSingleton(new GoogleAuthService(
    builder.Configuration["Google:ClientId"],
    builder.Configuration["Google:ClientSecret"]
));

builder.Services.AddSingleton(new MetaAuthService(
    builder.Configuration["Meta:AccessToken"]
));


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
