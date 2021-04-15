using System;
using System.CommandLine;
using System.CommandLine.Invocation;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace ExportCodeCatWebReferences
{
    class Program
    {
        static int Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                new Option<string>(new[] { "--input", "-i" }, "The CodeCat file to process."),
                new Option<string>(new[] { "--output", "-o" } , "The markup file output.")
            };

            rootCommand.Description = "Qik Console Application";

            // Note that the parameters of the handler method are matched according to the names of the options
            rootCommand.Handler = CommandHandler.Create<string, string>((Action<string, string>)(
                (
                    input,
                    output
                ) =>
            {
                bool addDescriptions = true;

                var extractor = new WebReferenceExtractor();
                
                extractor.Extract(input, output, addDescriptions);

                Console.ReadLine();
            }));

            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;
        }
    }
}
