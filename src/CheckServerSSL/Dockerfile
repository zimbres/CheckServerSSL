FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/CheckServerSSL/CheckServerSSL.csproj", "src/CheckServerSSL/"]
RUN dotnet restore "./src/CheckServerSSL/./CheckServerSSL.csproj"
COPY . .

ARG TARGETPLATFORM

RUN if [ "$TARGETPLATFORM" = "linux/amd64" ]; then \
    RID=linux-x64 ; \
    elif [ "$TARGETPLATFORM" = "linux/arm64" ]; then \
    RID=linux-arm64 ; \
    elif [ "$TARGETPLATFORM" = "linux/arm/v7" ]; then \
    RID=linux-arm ; \
    fi \
    && dotnet publish "src/CheckServerSSL/CheckServerSSL.csproj" -c Release -o /app/publish -r $RID --self-contained false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["./CheckServerSSL"]