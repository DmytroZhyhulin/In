using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Fluid;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace LiquidPdfExporter
{
    class Program
    {
        static object ConvertJsonElement(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    var dict = new Dictionary<string, object?>();
                    foreach (var property in element.EnumerateObject())
                    {
                        dict[property.Name] = ConvertJsonElement(property.Value);
                    }
                    return dict;
                case JsonValueKind.Array:
                    var list = new List<object?>();
                    foreach (var item in element.EnumerateArray())
                    {
                        list.Add(ConvertJsonElement(item));
                    }
                    return list;
                case JsonValueKind.String:
                    return element.GetString();
                case JsonValueKind.Number:
                    if (element.TryGetInt64(out var l)) return l;
                    if (element.TryGetDouble(out var d)) return d;
                    return null;
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.False:
                    return false;
                default:
                    return null;
            }
        }

        static void Main(string[] args)
        {
            var templateFile = args.Length > 0 ? args[0] : "Template.liquid";
            var jsonFile = args.Length > 1 ? args[1] : "data.json";
            var outputFile = args.Length > 2 ? args[2] : "output.pdf";

            var templateText = File.ReadAllText(templateFile);
            var jsonText = File.ReadAllText(jsonFile);

            using var document = JsonDocument.Parse(jsonText);
            var model = (Dictionary<string, object?>)ConvertJsonElement(document.RootElement);

            var parser = new FluidParser();
            if (!parser.TryParse(templateText, out var template, out var error))
            {
                Console.WriteLine($"Template error: {error}");
                return;
            }

            var options = new TemplateOptions();
            options.MemberAccessStrategy.RegisterDictionaryAccess();
            var context = new TemplateContext(model, options);

            var html = template.Render(context);

            PdfDocument pdf = PdfGenerator.GeneratePdf(html, PdfSharpCore.PageSize.A4);
            pdf.Save(outputFile);

            Console.WriteLine($"PDF generated: {outputFile}");
        }
    }
}
