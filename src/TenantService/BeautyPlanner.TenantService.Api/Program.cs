var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TenantDbContext>(options =>
{
    options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
           .UseSqlServer(connectionString,
                         o => o.MigrationsAssembly("BeautyPlanner.TenantService")
                               .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
    options.LogTo(Console.WriteLine, LogLevel.Information);
    options.EnableSensitiveDataLogging();
});
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddScoped<IUserContext, HttpUserContext>();
builder.Services.TryAddScoped<IRepository<Salon>, SalonRepository>();
builder.Services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddDbContext<TenantDbContext>();
builder.Services.TryAddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.TryAddScoped<ITenantService, TenantService>();
builder.Services.TryAddScoped<ISalonService, SalonService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseGlobalExceptionHandling();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
