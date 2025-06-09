using System.Text.Json;
using DotLiquid;
using DinkToPdf;
using DinkToPdf.Contracts;

var templatePath = Path.Combine("Templates", "ua_service_agreement.liquid");
var jsonPath = Path.Combine("Data", "contract_data.json");
var outputDir = "Output";
var outputPdfPath = Path.Combine(outputDir, "service_agreement.pdf");

Directory.CreateDirectory(outputDir);

// Read template
var templateText = await File.ReadAllTextAsync(templatePath);

// Read JSON data into dictionary
var jsonText = await File.ReadAllTextAsync(jsonPath);
var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};
var data = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonText, options) ?? new();

// Render Liquid template
Template.NamingConvention = new CSharpNamingConvention();
var template = Template.Parse(templateText);
var html = template.Render(Hash.FromDictionary(data));

// Convert HTML to PDF
var converter = new SynchronizedConverter(new PdfTools());
var doc = new HtmlToPdfDocument
{
    GlobalSettings = new GlobalSettings
    {
        ColorMode = ColorMode.Color,
        Orientation = Orientation.Portrait,
        PaperSize = PaperKind.A4,
        Out = outputPdfPath
    },
    Objects =
    {
        new ObjectSettings
        {
            HtmlContent = html
        }
    }
};

converter.Convert(doc);

Console.WriteLine($"PDF saved to {outputPdfPath}");
