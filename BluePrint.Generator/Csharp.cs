using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BluePrint.Generator;

public class Csharp
{
    public void GenerateCSharpCode(dynamic jsonObj, string fileName)
    {
        string code = GenerateClass(jsonObj);
        var directory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        File.WriteAllText($"{directory}\\{fileName.Split('.')[0]}.cs", code);
    }

    public string GenerateClass(dynamic jsonObj)
    {
        StringBuilder sb = new StringBuilder();

        // class declaration
        sb.AppendLine($"public class BluePrint {{");

        // constructor
        sb.AppendLine($"  public BluePrint() {{ }}");

        // properties
        GenerateProperties(jsonObj, sb, 1);

        // end class
        sb.AppendLine("}");

        return sb.ToString();
    }

    public void GenerateProperties(dynamic obj, StringBuilder sb, int indent)
    {
        string spaces = new string(' ', indent * 2);

        foreach (var prop in obj)
        {
            if (prop.Value.Type == JTokenType.Object)
            {
                sb.AppendLine($"{spaces}public {prop.Value.className} {prop.Name} {{ get; set; }}");
                GenerateProperties(prop.Value, sb, indent + 1);
            }
            else
            {
                sb.AppendLine($"{spaces}public {GetCSharpType(prop.Value)} {prop.Name} {{ get; set; }}");
            }
        }
    }

    public string GetCSharpType(dynamic value)
    {
        switch (value.Type)
        {
            case JTokenType.Integer:
                return "double";
            case JTokenType.String:
                return "string";
            case JTokenType.Boolean:
                return "bool";
            case JTokenType.Array:
                return "List<object>";
            default:
                return "object";
        }
    }
}
