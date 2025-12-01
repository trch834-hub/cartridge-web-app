# Используем .NET 8.0 SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файл проекта и восстанавливаем зависимости
COPY ["CartridgeWebApp.csproj", "./"]
RUN dotnet restore "CartridgeWebApp.csproj"

# Копируем все файлы и собираем
COPY . .
RUN dotnet build "CartridgeWebApp.csproj" -c Release -o /app/build

# Публикуем приложение
FROM build AS publish
RUN dotnet publish "CartridgeWebApp.csproj" -c Release -o /app/publish

# Финальный образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CartridgeWebApp.dll"]