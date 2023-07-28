using GncYldzSignalRRabbitMq.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(optiions=> optiions.AddDefaultPolicy(policy => policy.AllowCredentials()
.AllowAnyHeader()
.AllowAnyOrigin()
.AllowAnyMethod()
.SetIsOriginAllowed(x=>true)));

builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();

app.MapControllers();
app.MapHub<MessageHub>("/messagehub");

app.Run();
