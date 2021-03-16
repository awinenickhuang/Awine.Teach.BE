#引入镜像
FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build
#设定工作目录
WORKDIR /app/financial
#在容器中程序使用的端口，一定要和程序启动使用的端口对应上
EXPOSE 8002
#复制文件到工作目录
COPY . .
#ENV ASPNETCORE_ENVIRONMENT Production
#启动程序
ENTRYPOINT ["dotnet", "Awine.Teach.FinancialService.dll"]