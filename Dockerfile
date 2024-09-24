# Use the official .NET 8 SDK Alpine image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build-env

# Set the working directory inside the container
WORKDIR /app

# Copy the project files into the container
COPY *.sln ./
COPY storiesbook/*.csproj ./storiesbook/

# Restore dependencies
RUN dotnet restore

# Copy the remaining files into the container
COPY . ./

# Publish the application in Release mode to the 'out' folder
RUN dotnet publish -c Release -o /out

# Use the official .NET runtime image for production
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published application from the build environment
COPY --from=build-env /out .

# Expose the necessary port (adjust this if needed)
EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "storiesbook.dll"]
