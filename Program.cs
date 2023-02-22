

using Microsoft.EntityFrameworkCore;
using NoScrip.Db;



var builder = WebApplication.CreateBuilder(args);

var AllowSpecificOrigins = "_allowSpecificOrigins";
var PORT = builder.Configuration.GetSection("port");
var CORS_POLICY = builder.Configuration.GetSection("CorsPolicy");


var CORS_URL = new string[]{
   CORS_POLICY["cp1"],
   CORS_POLICY["cp2"],
   CORS_POLICY["cp3"],
   CORS_POLICY["cp4"],
   CORS_POLICY["cp5"],
};
// Add services to the container.


builder.WebHost.UseUrls($"http://*:{PORT["p1"]}");
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(option =>
{
    option.UseMySQL(builder.Configuration.GetConnectionString("MysqlDb"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.Add


builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: AllowSpecificOrigins, policy =>
        {
            policy.WithOrigins(CORS_URL).AllowAnyHeader().AllowAnyMethod();
        });
});

// builder.Services.
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseCors(AllowSpecificOrigins);



app.UseHttpsRedirection();


app.UseAuthorization();
// app.MapGet("/h", (HttpRequest req, HttpResponse res) =>

// {
//     res.WriteAsJsonAsync(new
//     {
//         data = CORS_URL
//     });
// });



app.MapControllers();

app.Run();
