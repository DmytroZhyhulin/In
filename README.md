# Liquid PDF Exporter

This repository contains a simple .NET 8 console application that fills a Liquid template with data from a JSON file and exports the result as a PDF document.

## Building

```bash
# Ensure .NET 8 SDK is installed
cd LiquidPdfExporter
 dotnet restore
 dotnet build -c Release
```

## Running

```bash
# from the project directory
 dotnet run -- Template.liquid data.json output.pdf
```

This command generates `output.pdf` by applying `data.json` to `Template.liquid`.
