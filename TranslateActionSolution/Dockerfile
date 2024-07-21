# Stage 1: Build the .NET application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Install necessary tools for building
RUN apt-get update \
    && apt-get install -y clang gcc zlib1g-dev git \
    && rm -rf /var/lib/apt/lists/*

COPY ["TranslateAction/TranslateAction.csproj", "TranslateAction/"]
RUN dotnet restore "TranslateAction/TranslateAction.csproj"
COPY . .
WORKDIR "/src/TranslateAction"

# Publish the application with trimming and ReadyToRun enabled
RUN dotnet publish "TranslateAction.csproj" -c Release -r linux-x64 --self-contained -p:PublishTrimmed=true -p:PublishReadyToRun=true -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS final
WORKDIR /app

# Install git and other runtime dependencies
RUN apt-get update \
    && apt-get install -y --no-install-recommends git \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish /app
RUN chmod +x /app/TranslateAction
ENTRYPOINT ["/app/TranslateAction"]
