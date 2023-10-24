using BluePrint.Generator;
using Newtonsoft.Json;

string json = "";
bool validPath = false;

while(!validPath) {

  Console.Write("Enter json file name location (path + filename) e.g (C:\\folder\\filename.json): ");
  string jsonFileName = Console.ReadLine();

  if (File.Exists(jsonFileName)) {
    
    try {
      json = File.ReadAllText(jsonFileName);
      JsonConvert.DeserializeObject(json);

      Console.WriteLine("JSON file is valid");
      validPath = true;

    } catch (Exception) {
      Console.WriteLine("Invalid JSON format");
    }

  } else {
    Console.WriteLine("File not found");
  }

}

Console.Write("Choose programming language (cs, py, ts, php, java, all): ");
string language = Console.ReadLine();

dynamic jsonObj = JsonConvert.DeserializeObject(json);

string fileName = "BluePrint";

Console.WriteLine("");
Console.WriteLine("Thanks for using BluePrint Generator");
Console.WriteLine("");


switch (language)
{
    case "cs":
        var c = new Csharp();
        c.GenerateCSharpCode(jsonObj, fileName);
        Console.WriteLine("BluePrint successfully downloaded, please check your downloads folder for BluePrint.cs");
        break;

    case "py":
        var py = new Python();
        py.GeneratePythonCode(jsonObj, fileName);
         Console.WriteLine("BluePrint successfully downloaded, please check your downloads folder for BluePrint.py");
        break;

    case "ts":
        var ts = new Typescript();
        ts.GenerateTypeScriptCode(jsonObj, fileName);
         Console.WriteLine("BluePrint successfully downloaded, please check your downloads folder for BluePrint.ts");
        break;

    case "php":
        var php = new Php();
        php.GeneratePHPCode(jsonObj, fileName);
         Console.WriteLine("BluePrint successfully downloaded, please check your downloads folder for BluePrint.php");
        break;

    case "java":
        var java = new Java();
        java.GenerateJavaCode(jsonObj, fileName);
         Console.WriteLine("BluePrint successfully downloaded, please check your downloads folder for BluePrint.java");
        break;
    case "all":
        var cTest = new Csharp();
        cTest.GenerateCSharpCode(jsonObj, fileName);
        Console.WriteLine("BluePrint successfully downloaded, please check your downloads folder for BluePrint.cs");
        var pyTest = new Python();
        pyTest.GeneratePythonCode(jsonObj, fileName);
        Console.WriteLine("BluePrint successfully downloaded, please check your downloads folder for BluePrint.py");
        var tsTest = new Typescript();
        tsTest.GenerateTypeScriptCode(jsonObj, fileName);
        Console.WriteLine("BluePrint successfully downloaded, please check your downloads folder for BluePrint.ts");
        var phpTest = new Php();
        phpTest.GeneratePHPCode(jsonObj, fileName);
        Console.WriteLine("BluePrint successfully downloaded, please check your downloads folder for BluePrint.php");
        var javaTest = new Java();
        javaTest.GenerateJavaCode(jsonObj, fileName);
         Console.WriteLine("BluePrint successfully downloaded, please check your downloads folder for BluePrint.java");
        break;
}


Console.ReadLine();