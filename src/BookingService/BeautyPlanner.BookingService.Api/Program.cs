var builder = WebApplication.CreateBuilder(args);

// Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Data access
builder.Services.AddDbContext<BookingDbContext>(options =>
{
    options.UseSqlServer(connectionString,
                         o => o.MigrationsAssembly("BeautyPlanner.BookingService")
                               .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

builder.Services.TryAddScoped<BaseDbContext>(sp => sp.GetRequiredService<BookingDbContext>());
builder.Services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.TryAddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.TryAddScoped<IUnitOfWork, UnitOfWork>();

// Infrastructure
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddScoped<IUserContext, HttpUserContext>();

// External services
builder.Services.AddHttpClient("ClientService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:ClientService"]
                                 ?? throw new InvalidOperationException("Services:ClientService is not configured."));
}).AddStandardResilienceHandler();

builder.Services.AddHttpClient("StaffService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:StaffService"]
                                 ?? throw new InvalidOperationException("Services:StaffService is not configured."));
}).AddStandardResilienceHandler();

builder.Services.AddHttpClient("CatalogService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:CatalogService"]
                                 ?? throw new InvalidOperationException("Services:CatalogService is not configured."));
}).AddStandardResilienceHandler();

builder.Services.AddHttpClient("TenantService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:TenantService"]
                                 ?? throw new InvalidOperationException("Services:TenantService is not configured."));
}).AddStandardResilienceHandler();

builder.Services.TryAddScoped<IApiLookupService, ApiLookupService>();

// Application services
builder.Services.TryAddScoped<IAppointmentManagementService, AppointmentManagementService>();

// API
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Middleware pipeline
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
