using System.Text;
using Newtonsoft.Json.Linq;

namespace BluePrint.Generator;

public class Typescript
{
    public void GenerateTypeScriptCode(dynamic jsonObj, string fileName)
    {
        string code = GenerateInterface(jsonObj);

        var directory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        File.WriteAllText($"{directory}\\{fileName}.ts", code);
    }

    public string GenerateInterface(dynamic jsonObj)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"class BluePrint {{");

        GenerateProperties(jsonObj, sb, 1);

        sb.AppendLine("}");
        sb.AppendLine("\n");
        sb.AppendLine("export { BluePrint };");

        return sb.ToString();
    }

    public void GenerateProperties(dynamic obj, StringBuilder sb, int indent)
    {
        string spaces = new string(' ', indent * 2);

        foreach (var prop in obj)
        {

            if (prop.Value.Type == JTokenType.Object)
            {
                sb.AppendLine($"{spaces}{prop.Name}: {prop.Value.className};");

                GenerateProperties(prop.Value, sb, indent + 1);
            }
            else
            {
                string tsType = GetTypeScriptType(prop.Value);

                sb.AppendLine($"{spaces}{prop.Name}: {tsType};");
            }
        }
    }

    public string GetTypeScriptType(dynamic value)
    {
        if (value.Type == JTokenType.Integer) return "number";
        if (value.Type == JTokenType.String) return "string";
        if (value.Type == JTokenType.Boolean) return "boolean";
        if (value.Type == JTokenType.Array) return "any[]";

        return "any";
    }
}
