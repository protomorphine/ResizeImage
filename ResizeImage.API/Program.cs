var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(op =>
{
    var xmlFilePaths = new[]
    {
        "ResizeImage.API.xml"
    };

    foreach (var xmlFilePath in xmlFilePaths)
    {
        var path = Path.Combine(AppContext.BaseDirectory, xmlFilePath);
        op.IncludeXmlComments(path);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();