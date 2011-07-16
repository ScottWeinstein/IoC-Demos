function Get-ScriptDirectory
{
    $Invocation = (Get-Variable MyInvocation -Scope 1).Value
    Split-Path $Invocation.MyCommand.Path
}

function BootStrapNuGet($wd)
{	
	$ngexe = "$wd\NuGet.exe"
	$ngexe
	if (!(Test-Path $ngexe))
	{
		$ngUrl = "http://nuget.codeplex.com/Project/Download/FileDownload.aspx?ProjectName=nuget&DownloadId=222685&FileTime=129528596690500000&Build=17950"
		$wc.DownloadFile($ngUrl,$ngexe)
		& $ngexe update -Self
	}
}

$wd = Get-ScriptDirectory
BootStrapNuGet $wd
$ngexe = "$wd\NuGet.exe"
Get-ChildItem -Recurse -Filter packages.config | % { & $ngexe install $_.FullName -o Packages }

#$ENV:HOME=''
#slideshow .\IoC.markdown -o Slideshow