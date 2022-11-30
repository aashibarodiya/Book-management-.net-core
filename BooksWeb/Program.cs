using ConceptArchitect.BookManagement;
using ConceptArchitect.BookManagement.Repositories.Ado;
using ConceptArchitect.Utils;
using System.Data.Common;
using System.Data.SqlClient;
using BooksWeb.Config;
using ConceptArchitect.BookManagement.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using BooksWeb.Utils;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BooksWeb.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .AddControllersWithViews()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

//configure the authentication services
builder
      .Services
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(opt =>
      {

          opt.RequireHttpsMetadata = false;
          opt.SaveToken = true;
          opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
          {
              ValidateIssuer = true,
              ValidateAudience = true,

              ValidAudience = builder.Configuration["Jwt:Audience"],
              ValidIssuer = builder.Configuration["Jwt:Issuer"],
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
          };

      });

//mongodb configuration


//builder.Services.AddAdoRepository();

builder.Services.AddDbContext<BMSContext>((provider, opt) =>
{
    var configuration = provider.GetService<IConfiguration>();

    var connectionString = configuration["ConnectionStrings:BooksWebEF"];
    opt.UseSqlServer(connectionString);
    // opt.UseLazyLoadingProxies();
});

//services
builder.Services.AddTransient<IAuthorService, SimpleAuthorService>();
builder.Services.AddTransient<IUserService, SimpleUserService>();
builder.Services.AddTransient<IBookService, SimpleBookService>();
builder.Services.AddTransient<IDataSeeder<Author>, DummyAuthorSeeder>();


//EF repositories
builder.Services.AddTransient<IRepository<Author, String>, AuthorEFRepository>();
builder.Services.AddTransient<IRepository<Book, String>, BookEFRepository>();

//builder.Services.AddSingleton<IAuthorService,InMemoryAuthorService>();
builder.Services.AddTransient<IRepository<User,string>,UserEFRepository>();
builder.Services.AddTransient<IRepository<Role, string>, RoleEFRepository>();


//Mongo Repositories
//builder.Services.AddMongoRepository(builder.Configuration);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add SignalR Service
builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseCors(opt =>
{
    opt.AllowAnyHeader();
    opt.AllowAnyOrigin();
    opt.AllowAnyMethod();
});

//app.UseExceptionHandler<InvalidIdException>(404, responseBuilder: ex=>new {Id=ex.Id, ErrorMessage=ex.Message});



if (app.Environment.IsDevelopment())
{
    app.MapGet("/seed/author", async context =>
    {
        var service = context.RequestServices.GetService<IDataSeeder<Author>>();
        await service.Seed();
        context.Response.Redirect("/");
    });


    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/seed/authors", async context =>
{
    if (app.Environment.IsDevelopment())
    {
        var service = context.RequestServices.GetService<IDataSeeder<Author>>();
        await service.Seed();
        context.Response.Redirect("/");

    }
    else
    {
        context.Response.StatusCode = 403; //Forbidden
        await context.Response.WriteAsync($"This feature is availble only in development mode");
    }
});

//Add Middleware for SingalR
app.MapHub<ChatHub>("/chat");


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Author}/{action=Index}/{id?}");

app.Run();
