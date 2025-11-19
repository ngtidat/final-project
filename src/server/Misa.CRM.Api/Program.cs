using Misa.CRM.Api.Middlewares;
using Misa.CRM.Business.Interfaces.Repositories;
using Misa.CRM.Business.Interfaces.Services;
using Misa.CRM.Business.Mappings;
using Misa.CRM.Business.Services;
using Misa.CRM.Data;
using Misa.CRM.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173") // URL của client
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Register DI services
builder.Services.AddSingleton<MisaDbContext>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ICustomerTypeRepository, CustomerTypeRepository>();
builder.Services.AddScoped<ICustomerTypeService, CustomerTypeService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowClient");

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Run();