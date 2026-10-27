using ApiInicial.Data;
using ApiInicial.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar CORS para permitir peticiones desde Angular (Puerto 4200)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAngular", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 2. Registrar el DbContext con PostgreSQL (ajusta "DefaultConnection" si tu connection string en appsettings.json se llama distinto)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Registrar tus servicios de la capa de negocios (¡Aquí se soluciona el error anterior!)
builder.Services.AddScoped<IProductoService, ProductoService>();

// 4. Agregar controladores y configuración de API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5. Configurar el pipeline HTTP
//if (app.Environment.IsDevelopment())
//{
    //app.UseSwagger();
    //app.UseSwaggerUI();
//}

app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.Run();