using RemoteReps.ImageReceiver.WebApi.Hubs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<ImageReceiverHub>("/hubs/image-receiver");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();