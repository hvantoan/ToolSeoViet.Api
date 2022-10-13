set outputPath=api-publish

if exist %outputPath% (
	rmdir /S /Q %outputPath%
)

dotnet clean ToolSeoViet.Api.sln
dotnet restore
dotnet publish ToolSeoViet.Api\ToolSeoViet.Api.csproj --output %outputPath% -c Release
