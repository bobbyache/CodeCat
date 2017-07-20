using CygSoft.CodeCat.Category.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public class ItemDblClickedEventArgs : EventArgs
    {
        public ITitledEntity Item { get; private set; }

        internal ItemDblClickedEventArgs(ITitledEntity item)
        {
            this.Item = item;
        }
    }

    public class ItemExpandingEventArgs : EventArgs
    {
        internal ItemExpandingEventArgs(ITitledEntity expandingItem)
        {
            this.expandingItem = expandingItem;
        }

        private ITitledEntity expandingItem;
        public ITitledEntity ExpandingItem
        {
            get { return expandingItem; }
            set { expandingItem = value; }
        }
    }

    public class ItemMovedEventArgs : EventArgs
    {
        internal ItemMovedEventArgs(ITitledEntity displacedItem, ITitledEntity newParent)
        {
            this.newParent = newParent;
            this.displacedItem = displacedItem;
        }

        private ITitledEntity displacedItem;
        public ITitledEntity DisplacedItem
        {
            get { return displacedItem; }
            set { displacedItem = value; }
        }

        private ITitledEntity newParent;
        public ITitledEntity NewParent
        {
            get { return newParent; }
            set { newParent = value; }
        }
    }

    public class ItemRenamedEventArgs : EventArgs
    {
        internal ItemRenamedEventArgs(ITitledEntity item, string newTitle)
        {
            this.item = item;
            this.newTitle = newTitle;
        }

        private ITitledEntity item;
        public ITitledEntity Item
        {
            get { return item; }
        }

        private string newTitle;
        public string NewTitle
        {
            get { return newTitle; }
            set { newTitle = value; }
        }
    }
}
