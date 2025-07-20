using Newtonsoft.Json.Linq;
using Rhino;
using rhinomcp.Serializers;
using System;

namespace RhinoMCPPlugin.Functions;

public partial class RhinoMCPFunctions
{
    public JObject GetDocumentInfo(JObject parameters)
    {
        const int LIMIT = 100;
        
        try
        {
            RhinoApp.WriteLine("Getting document info...");

            var doc = RhinoDoc.ActiveDoc;
            if (doc == null)
            {
                throw new InvalidOperationException("No active document found");
            }

            var metaData = new JObject
            {
                ["name"] = doc.Name ?? "Untitled",
                ["date_created"] = doc.DateCreated,
                ["date_modified"] = doc.DateLastEdited,
                ["tolerance"] = doc.ModelAbsoluteTolerance,
                ["angle_tolerance"] = doc.ModelAngleToleranceDegrees,
                ["path"] = doc.Path ?? "",
                ["units"] = doc.ModelUnitSystem.ToString(),
            };

            var objectData = new JArray();
            int count = 0;
            RhinoApp.WriteLine("Getting document objects...");
            foreach (var docObject in doc.Objects)
            {
                if (count >= LIMIT) break;
                
                try
                {
                    objectData.Add(Serializer.RhinoObject(docObject));
                }
                catch (Exception ex)
                {
                    RhinoApp.WriteLine($"Error serializing object: {ex.Message}");
                    continue;
                }
                count++;
            }

            var layerData = new JArray();
            count = 0;
            RhinoApp.WriteLine("Getting document layers...");
            foreach (var docLayer in doc.Layers)
            {
                if (count >= LIMIT) break;
                
                try
                {
                    if (docLayer.Id == Guid.Empty)
                    {
                        RhinoApp.WriteLine($"Skipping layer with invalid ID: {docLayer.Name}");
                        continue;
                    }

                    layerData.Add(new JObject
                    {
                        ["id"] = docLayer.Id.ToString(),
                        ["name"] = docLayer.Name ?? $"Layer_{count}",
                        ["color"] = docLayer.Color.ToString(),
                        ["visible"] = docLayer.IsVisible,
                        ["locked"] = docLayer.IsLocked
                    });
                }
                catch (Exception ex)
                {
                    RhinoApp.WriteLine($"Error processing layer: {ex.Message}");
                    continue;
                }
                count++;
            }

            var result = new JObject
            {
                ["meta_data"] = metaData,
                ["object_count"] = doc.Objects.Count,
                ["objects"] = objectData,
                ["layer_count"] = doc.Layers.Count,
                ["layers"] = layerData
            };

            RhinoApp.WriteLine($"Document info collected: {objectData.Count} objects, {layerData.Count} layers");
            return result;
        }
        catch (Exception ex)
        {
            RhinoApp.WriteLine($"Error in GetDocumentInfo: {ex.Message}");
            return new JObject
            {
                ["error"] = ex.Message,
                ["stack_trace"] = ex.StackTrace
            };
        }
    }
}