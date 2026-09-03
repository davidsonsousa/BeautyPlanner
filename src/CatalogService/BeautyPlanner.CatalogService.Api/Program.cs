var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CatalogDbContext>(options =>
{
    options.UseSqlServer(connectionString,
                         o => o.MigrationsAssembly("BeautyPlanner.CatalogService")
                               .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

builder.Services.TryAddScoped<BaseDbContext>(sp => sp.GetRequiredService<CatalogDbContext>());
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddScoped<IUserContext, HttpUserContext>();
builder.Services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.TryAddScoped<ITreatmentRepository, TreatmentRepository>();
builder.Services.TryAddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.TryAddScoped<ITreatmentCategoryManagementService, TreatmentCategoryManagementService>();
builder.Services.TryAddScoped<ITreatmentManagementService, TreatmentManagementService>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

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

app.UseRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
