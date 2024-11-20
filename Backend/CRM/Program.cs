using CRM.Aplication.Interfaces;
using CRM.Aplication.Mapper;
using CRM.Aplication.Mapper.IMappers;
using CRM.Aplication.UseCases;
using CRM.Infraestructure.Command;
using CRM.Infraestructure.Percistence.Context;
using CRM.Infraestructure.Query;
using CRM.Infrastructure.Query;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<CrmContext>(options => options.UseSqlServer(connectionString));

//sinbleton 
builder.Services.AddScoped<ICampaignTypeService, CampaignTypeService>();
builder.Services.AddScoped<ICampaingTypeQuery, CampaingTypeQuery>();
builder.Services.AddScoped<ICampaignTypeMapper, CampaignTypeMapper>();


builder.Services.AddScoped<IClientsCommand, ClientsCommand>();
builder.Services.AddScoped<IClientService, ClientsService>();
builder.Services.AddScoped<IClientsQuery, ClientsQuery>();
builder.Services.AddScoped<IClientMapper, ClientMapper>();

builder.Services.AddScoped<IInteractionTypesQuery, InterationTypesQuery>();
builder.Services.AddScoped<IInteractionTypesService, InteractionTypesService>();
builder.Services.AddScoped<IInteractionTypesMapper, InteractionTypesMapper>();


builder.Services.AddScoped<IProjectsCommand, ProjectsCommand>();
builder.Services.AddScoped<IProjectsQuery, ProjectsQuery>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<IProjectMapper, ProjectMapper>();


builder.Services.AddScoped<ITaskQuery, TaskQuery>();
builder.Services.AddScoped<ITasksCommand, TasksCommand>();
builder.Services.AddScoped<ITaskMapper, TaskMapper>();



builder.Services.AddScoped<ITaskStatusQuery, TaskStatusQuery>();
builder.Services.AddScoped<ITaskStatusService, TaskStatusService>();
builder.Services.AddScoped<ITaskStatusMapper, TaskStatusMapper>();


builder.Services.AddScoped<IUserQuery, UserQuery>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserMapper, UserMapper>();

builder.Services.AddScoped<IInteractionQuery, InteractionQuery>();
builder.Services.AddScoped<IInteractionMapper, InteractionMapper>();



//CORS deshabilitar
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
