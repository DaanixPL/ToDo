#!/bin/bash
set -e  # Exit on any error

echo "=========================================="
echo "   Building Todo.Frontend for Render"
echo "=========================================="

# Install .NET SDK 8.0
echo "Installing .NET SDK 8.0..."
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 8.0

# Add .NET to PATH
export DOTNET_ROOT=$HOME/.dotnet
export PATH=$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools
echo ".NET SDK installed and added to PATH."

# Verify .NET installation
echo "Verifying .NET installation..."
dotnet --version

# Install wasm-tools workload
echo "Installing wasm-tools workload..."
dotnet workload install wasm-tools

# Verify workloads
echo "Installed workloads:"
dotnet workload list

# Navigate to Todo.Frontend project (we're already in Frontend/)
echo "Navigating to Todo.Frontend..."
cd Todo.Frontend

# Restore dependencies
echo "Restoring dependencies..."
dotnet restore

# Build and publish
echo "Building and publishing project..."
dotnet publish -c Release -o ../out

# Navigate back to Frontend/
cd ..

# Verify wwwroot exists
echo "Verifying build output..."
if [ ! -d "out/wwwroot" ]; then
    echo "ERROR: wwwroot directory not found!"
    echo "Expected location: Frontend/out/wwwroot"
    exit 1
fi

# Verify index.html exists
if [ ! -f "out/wwwroot/index.html" ]; then
    echo "ERROR: index.html not found in wwwroot!"
    ls -la out/wwwroot/
    exit 1
fi

# Verify Blazor framework files exist
if [ ! -d "out/wwwroot/_framework" ]; then
    echo "ERROR: _framework directory not found!"
    ls -la out/wwwroot/
    exit 1
fi

# Verify MudBlazor content exists
if [ ! -d "out/wwwroot/_content" ]; then
    echo "WARNING: _content directory not found (MudBlazor files may be missing)"
fi

# Display build artifacts
echo "=========================================="
echo "  Build completed successfully!"
echo "=========================================="
echo "  Build output summary:"
echo ""
echo "  Root files:"
ls -lh out/wwwroot/ | grep -v "^d"
echo ""
echo "  Directories:"
ls -ld out/wwwroot/*/
echo ""
echo "  Framework files (first 10):"
ls -lh out/wwwroot/_framework/*.dll 2>/dev/null | head -10 || echo "No DLL files found"
echo ""
echo "  WASM files:"
ls -lh out/wwwroot/_framework/*.wasm 2>/dev/null || echo "No WASM files found"
echo ""
echo "  Total file count: $(find out/wwwroot -type f | wc -l)"
echo "=========================================="
echo "  Ready to deploy!"
echo "=========================================="