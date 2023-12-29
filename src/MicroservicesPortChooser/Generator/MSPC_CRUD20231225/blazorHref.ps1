param(
     [Parameter(mandatory=$true)]
     [string]$file,
     [Parameter(mandatory=$true)]
     [Alias('Parameter2')]
     [string]$newRelativePath ="/blazor/"

 )
cls
# $file = "index.html"
Write-Host "start reading " $file
$FileContents = Get-Content -Path $file

$i = 1
ForEach ($Line in $FileContents) {
    # Process each line here
 # Write-Host "Line# $i :" $Line
if ($Line -like '* href="/*') { 
    $Line=$Line.Replace(' href="/',' href="'+$newRelativePath)
    Write-Host "with / Line# $i :" $Line
    continue  
}

if ($Line -like '* href="*') { 
    $Line=$Line.Replace(' href="',' href="'+$newRelativePath)
    Write-Host "no / Line# $i :" $Line
  
}
if ($Line -like '* src="*') { 
    $Line=$Line.Replace(' src="',' src="'+$newRelativePath)
    Write-Host "no / Line# $i :" $Line
  
}

    $i++
}

Write-Host "End " $file
