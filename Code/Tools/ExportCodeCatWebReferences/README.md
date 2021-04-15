
## Get up and Running

### Run the Console
To run the console application only the `dotnet run` command is necessary unless running for the first time.

```
dotnet clean
dotnet restore
dotnet run --project Console/ExportCodeCatWebReferences.csproj
```

### Cmd Execution
```
cd "C:\Code\you\ExportCodeCatWebReferences\Console\bin\Debug\net5.0"
ExportCodeCatWebReferences -i "???" -o "C:\Users\RobB\Desktop\Notion Hyperlink Import\output.md"
```

### Powershell Execution
```
Start-Pr