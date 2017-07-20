using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Category
{
    public class Blueprint
    {
        public string BlueprintId { get; set; }

        public string HeaderText { get; set; }
        public string BodyText { get; set; }
        public string FooterText { get; set; }

        public Blueprint(string blueprintId)
        {
            BlueprintId = blueprintId;
            HeaderText = string.Empty;
            BodyText = string.Empty;
            FooterText = string.Empty;
        }

        public Blueprint Copy()
        {
            Blueprint blueprintCopy = new Blueprint(this.BlueprintId);
            blueprintCopy.BodyText = this.BodyText;
            blueprintCopy.FooterText = this.FooterText;
            blueprintCopy.HeaderText = this.HeaderText;

            return blueprintCopy;
        }

        public bool IsDifferent(Blueprint blueprint)
        {
            if (this.BlueprintId == blueprint.BlueprintId &&
                this.BodyText == blueprint.BodyText &&
                this.FooterText == blueprint.FooterText &&
                this.HeaderText == blueprint.HeaderText)
                return false;

            return true;
        }
    }
}
