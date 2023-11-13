var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opetion => opetion.AddPolicy(name: "MontyHallorigin",
     policy =>
     {
         policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
     }
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("MontyHallorigin");
app.UseRouting();


app.UseAuthorization();

app.MapControllers();

app.Run();
