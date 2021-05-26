Demo application for `tye`.
- Runs a .NET `frontend` app uses HTTP to query data from a .NET `backend` app.
- The `backend` app retrieves data from a PostgreSQL database that runs in a container.

# Running the sample

Install `podman`/`docker` from your distro.

Install `tye`:

```
$ dotnet tool install -g Microsoft.Tye --version "*-*"
```

Create a folder for storing the database we use during development:
```
$ mkdir -p tye/customerdb
```

Run tye:
```
$ tye run
```