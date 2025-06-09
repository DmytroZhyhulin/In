# Service Agreement Generator

This example demonstrates how to fill a Liquid template with data from a JSON file and export the result to PDF.

## Structure
- `Templates/ua_service_agreement.liquid` – Liquid template of the agreement.
- `Data/contract_data.json` – sample data for the template.
- `Program.cs` and `ServiceAgreementApp.csproj` – console application using DotLiquid and DinkToPdf.
- `Output/` – generated PDF file will be written here.

## Usage
Restore packages and run the application with the .NET SDK:

```bash
 dotnet run --project ServiceAgreementApp.csproj
```

The resulting file `service_agreement.pdf` will appear in the `Output` directory.
