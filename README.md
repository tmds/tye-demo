Demo application for `tye`.
- Runs a .NET `Frontend` app uses HTTP to query data from a .NET `Backend` app.
- The `Backend` app retrieves data from a PostgreSQL database that runs in a container.

# Running the sample

Install `podman`/`docker` from your distro.

Install `tye`:

```
$ dotnet tool install -g Microsoft.Tye --version "*-*"
```

Run tye:
```
$ tye run --dashboard
```
# Code snippits

## Service bindings

`Frontend` finds url for backend:

```cs
services.AddHttpClient<BackendClient>(client =>
{
    client.BaseAddress = Configuration.GetServiceUri("backend");
});
```

`Backend` fits connectionstring for PostgreSQL:

```cs
services.AddDbContext<CustomerDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("customerdb")));
```

## Database configuration

`tye` doesn't have first-class support for applying migrations: https://github.com/dotnet/tye/issues/627.

For this demo app, we let the `Backend` app handle it when the `MIGRATE=y` envvar is set.

```cs
private static void MigrateDatabase(IApplicationBuilder app, bool seedDatabase)
{
    using (var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope())
    {
        using (var context = serviceScope.ServiceProvider.GetService<CustomerDbContext>())
        {
            context.Database.Migrate();

            if (seedDatabase)
            {
                foreach (var name in new[] { "John", "Matt", "Omair", "Dan", "Radka", "Andrew", "Tom" })
                {
                    if (!context.Customers.Any())
                    {
                        context.Customers.Add(new Customer { Name = name });
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
```