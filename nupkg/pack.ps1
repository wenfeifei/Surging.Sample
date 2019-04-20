Param(
    [parameter(Mandatory=$false)][string]$repo="http://192.168.31.115:8081/nuget",
    [parameter(Mandatory=$false)][bool]$push=$false,
	[parameter(Mandatory=$false)][string]$apikey,
	[parameter(Mandatory=$false)][bool]$build=$true
)

# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$slnPath = Join-Path $packFolder "../sln"
$srcPath = Join-Path $packFolder "../src/Core"

$projects = (
  "Surging.Core.ApiGateWay",
  "Surging.Core.Caching",
  "Surging.Core.Codec.MessagePack",
  "Surging.Core.Codec.ProtoBuffer",
  "Surging.Core.Common",
  "Surging.Core.Consul",
  "Surging.Core.CPlatform",
  "Surging.Core.DotNetty",
  "Surging.Core.EventBusKafka",
  "Surging.Core.EventBusRabbitMQ",
  "Surging.Core.KestrelHttpServer",
  "Surging.Core.Log4net",
  "Surging.Core.NLog",
  "Surging.Core.Protocol.Http",
  "Surging.Core.Protocol.WS",
  "Surging.Core.ProxyGenerator",
  "Surging.Core.ServiceHosting",
  "Surging.Core.Swagger",
  "Surging.Core.System",
  "Surging.Core.Zookeeper",
  "Surging.Core.Domain",
  "Surging.Core.Schedule",
  "Surging.Core.AutoMapper",
  "Surging.Core.Dapper",
  "WebSocketCore"
)

if ($build) {
  Set-Location $slnPath
  & dotnet restore Surging.sln

  foreach($project in $projects) {
    $projectFolder = Join-Path $srcPath $project
    
    Set-Location $projectFolder
    Remove-Item -Recurse (Join-Path $projectFolder "bin/Release")
	& dotnet msbuild /p:Configuration=Release /p:SourceLinkCreate=true
	& dotnet msbuild /t:pack /p:Configuration=Release /p:SourceLinkCreate=true
	
	$projectPackPath = Join-Path $projectFolder ("/bin/Release/" + $project + ".*.nupkg")
    Move-Item $projectPackPath $packFolder
	
}
  Move-Item $projectPackPath $packFolder

  Set-Location $packFolder
}



if($push) {
    if ([string]::IsNullOrEmpty($apikey)){
        Write-Warning -Message "未设置nuget仓库的APIKEY"
		exit 1
	}
	dotnet nuget push *.nupkg -s $repo -k $apikey
}

