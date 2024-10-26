# Fanscript Documentation

Documentation for the language [FanScript](https://github.com/BitcoderCZ/FanScript)

Documentation in markdown format can be found [here](MdDocs/README.md)

Example "games" can be found [here](https://github.com/BitcoderCZ/FanScript-Games)

Some information was copied from the [Fancade Wiki](https://www.fancade.com/wiki/home)

## Building
- Make sure you have installed [dotnet 8 sdk](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Run `dotnet build -c Release` in FanScript.DocumentationGenerator/

## Contributing

- Update the generator and/or files in DocSrc/ (do not change files in MdDocs/, they will be overwritten)
- [Build](#building)
- Copy
    ```
    FanScript.DocumentationGenerator.deps.json
    FanScript.DocumentationGenerator.dll
    FanScript.DocumentationGenerator.exe
    FanScript.DocumentationGenerator.runtimeconfig.json
    ```
    From FanScript.DocumentationGenerator\bin\Release\net8.0 to the top directory
- Run FanScript.DocumentationGenerator.exe
- PRs with modified FanScript.dll will be rejected