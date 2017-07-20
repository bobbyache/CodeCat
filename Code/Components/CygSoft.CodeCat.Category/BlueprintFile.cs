using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CygSoft.CodeCat.Category
{
    public class BlueprintFile
    {
        public string BlueprintDirectory { get; private set; }

        public BlueprintFile(string blueprintDirectory)
        {
            this.BlueprintDirectory = blueprintDirectory;
        }

        public bool BlueprintFileExists(BlueprintHeader blueprintHeader)
        {
            if (File.Exists(CreateBlueprintFilePath(blueprintHeader.Id)))
            {
                return true;
            }
            return false;
        }

        public void DeleteBlueprintFile(BlueprintHeader blueprintHeader)
        {
            if (File.Exists(CreateBlueprintFilePath(blueprintHeader.Id)))
            {
                File.Delete(CreateBlueprintFilePath(blueprintHeader.Id));
            }
        }

        public void SaveBlueprint(Blueprint blueprint)
        {
            CreateBlueprintFile(blueprint);
        }

        public bool LoadBlueprint(BlueprintHeader blueprintHeader, out Blueprint blueprint)
        {
            blueprint = null;

            if (File.Exists(CreateBlueprintFilePath(blueprintHeader.Id)))
            {
                XDocument xDocument = XDocument.Load(CreateBlueprintFilePath(blueprintHeader.Id), LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);
                XElement xBlueprint = xDocument.Element("Blueprint");

                blueprint = new Blueprint(blueprintHeader.Id)
                {
                    HeaderText = (string)xBlueprint.Element("Sections").Element("Header").Value,
                    BodyText = (string)xBlueprint.Element("Sections").Element("Body").Value,
                    FooterText = (string)xBlueprint.Element("Sections").Element("Footer").Value
                };
                return true;
            }

            return false;
        }


        public void CreateBlueprintFile(Blueprint blueprint)
        {
            XElement documentElement = new XElement("Blueprint",
                new XElement("DataSource"),
                new XElement("Variables"),
                new XElement("Sections",
                    new XElement("Header", new XCData(blueprint.HeaderText)),
                    new XElement("Body", new XCData(blueprint.BodyText)),
                    new XElement("Footer", new XCData(blueprint.FooterText))
                    ));

            //new XElement("Variables",
            //    new XAttribute("PlaceholderPrefix", "{"),
            //    new XAttribute("PlaceholderPostfix", "}"),
            //    new XElement("Variable",
            //        new XAttribute("Symbol", "Author"),
            //        new XAttribute("Value", "Author Name")
            //        ),
            //    new XElement("Variable",
            //        new XAttribute("Symbol", "AuthorCode"),
            //        new XAttribute("Value", "Author Code")
            //        )
            //    ),
            //new XElement("Code", new XCData("Code Template here: {Author} and {AuthorCode} and {CurrentDate}"))
            //    ),
            //new XElement("ScriptFolders"),
            //new XElement("DatabaseOperations")
            //);

            documentElement.Save(CreateBlueprintFilePath(blueprint.BlueprintId));
        }

        private string FileNameFromId(string id)
        {
            return string.Format("{0}.{1}", id, "blu");
        }

        private string CreateBlueprintFilePath(string id)
        {
            return Path.Combine(this.BlueprintDirectory, FileNameFromId(id));
        }
    }
}
