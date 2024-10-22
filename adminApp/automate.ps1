# Define the paths
$libPath = "wwwroot/lib"
$outputPath = "bundleconfig.json"

# Initialize an empty array for bundles
$bundles = @()

# Get all CSS files
$cssFiles = Get-ChildItem "$libPath/**/*.css"
if ($cssFiles) {
    $bundles += @{
        outputFileName = "wwwroot/css/site.min.css"
        inputFileNames = $cssFiles.FullName
        minify = @{ enabled = $true }
    }
}

# Get all JS files
$jsFiles = Get-ChildItem "$libPath/**/*.js"
if ($jsFiles) {
    $bundles += @{
        outputFileName = "wwwroot/js/site.min.js"
        inputFileNames = $jsFiles.FullName
        minify = @{ enabled = $true }
    }
}

# Convert to JSON and save to bundleconfig.json
$bundles | ConvertTo-Json -Depth 5 | Set-Content $outputPath
