namespace BluePrint.Generator;

using System.Text;
using Newtonsoft.Json.Linq;

public class Java
{

    public void GenerateJavaCode(dynamic jsonObj, string fileName)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("public class " + "BluePrint" + " {");

        GenerateFields(jsonObj, sb, 1);
        GenerateConstructor(jsonObj, sb);
        GenerateGettersSetters(jsonObj, sb);

        sb.AppendLine("}");
        var directory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        File.WriteAllText(directory + "\\" + fileName + ".java", sb.ToString());
    }

    private void GenerateFields(dynamic obj, StringBuilder sb, int indent)
    {
        string spaces = new string(' ', indent * 2);

        foreach (var prop in obj)
        {
            sb.AppendLine(spaces + "private " + MapType(prop.Value) + " " + prop.Name + ";");
        }
    }

    private string MapType(dynamic value)
    {
        switch (value.Type)
        {
            case JTokenType.String:
                return "String";

            case JTokenType.Integer:
                return "double";

            case JTokenType.Boolean:
                return "boolean";

            case JTokenType.Array:
                return "List<Object>";

            case JTokenType.Object:
                return "Map<String, Object>";

            case JTokenType.Null:
                return "null";

            case JTokenType.None:
                return "null";
        }

        return "Object";
    }

    private void GenerateConstructor(dynamic obj, StringBuilder sb)
    {
        string spaces = new string(' ', 1 * 2);
        sb.AppendLine(spaces + "public " + "BluePrint" + "() {}");
    }

    private void GenerateGettersSetters(dynamic obj, StringBuilder sb)
    {
        foreach (var prop in obj)
        {
            string type = MapType(prop.Value);
            sb.AppendLine("public " + type + " get" + PascalCase(prop.Name) + "() { return " + prop.Name + "; }");
            sb.AppendLine("public void set" + PascalCase(prop.Name) + "(" + type + " " + prop.Name + ") { this." + prop.Name + " = " + prop.Name + "; }");
        }
    }

    private string PascalCase(string text)
    {
        return char.ToUpper(text[0]) + text.Substring(1);
    }
}
