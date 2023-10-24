using System.Text;
using Newtonsoft.Json.Linq;
namespace BluePrint.Generator;

public class Php
{

    public void GeneratePHPCode(dynamic jsonObj, string fileName)
    {
        string code = GenerateClass(jsonObj);

        var directory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        File.WriteAllText($"{directory}\\{fileName}.php", code);

    }

    public string GenerateClass(dynamic jsonObj)
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<?php");
        sb.AppendLine("");
        sb.AppendLine("class " + "BluePrint" + " {");

        GenerateProperties(jsonObj, sb, 1);

        sb.AppendLine("}");
        sb.AppendLine("");
        sb.AppendLine("?>");

        return sb.ToString();
    }

    public void GenerateProperties(dynamic obj, StringBuilder sb, int indent)
    {
        string spaces = new string(' ', indent * 2);

        foreach (var prop in obj)
        {

            if (prop.Value.Type == JTokenType.Object)
            {
                sb.AppendLine(spaces + "public $" + prop.Name + ";");

                GenerateProperties(prop.Value, sb, indent + 1);
            }
            else
            {
                sb.AppendLine(spaces + "public $" + prop.Name + ";");
            }
        }
    }
}
