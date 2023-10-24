using System.Text;
using Newtonsoft.Json.Linq;
namespace BluePrint.Generator;

public class Python
{

    public void GeneratePythonCode(dynamic jsonObj, string fileName)
    {
        string code = GenerateClass(jsonObj);

        var directory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        File.WriteAllText($"{directory}\\{fileName}.py", code);
    }

    public string GenerateClass(dynamic jsonObj)
    {

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("class " + "BluePrint" + ":");

        GenerateProperties(jsonObj, sb, 1);

        return sb.ToString();

    }

    public void GenerateProperties(dynamic obj, StringBuilder sb, int indent)
    {
        string spaces = new string(' ', indent * 4);

        foreach (var prop in obj)
        {

            if (prop.Value.Type == JTokenType.Object)
            {
                sb.AppendLine(spaces + prop.Name + " = { }");
                GenerateProperties(prop.Value, sb, indent + 1);
            }
            else
            {
                string pyType = GetPythonType(prop.Value);
                sb.AppendLine(spaces + prop.Name + " = " + pyType);
            }
        }
    }

    public string GetPythonType(dynamic value)
    {
        if (value.Type == JTokenType.Integer) return "float";
        if (value.Type == JTokenType.String) return "str";
        if (value.Type == JTokenType.Boolean) return "bool";
        if (value.Type == JTokenType.Array) return "list";

        return "object";
    }
}
