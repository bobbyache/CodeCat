using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CygSoft.CodeCat.Category.Infrastructure;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;

namespace CygSoft.CodeCat.UI.WinForms.Controls
{
    public delegate void ItemExpandingHandler(object sender, ItemExpandingEventArgs e);
    public delegate void ItemRenamedHandler(object sender, ItemRenamedEventArgs e);
    public delegate void ItemMovedHandler(object sender, ItemMovedEventArgs e);

    public delegate bool ItemIsExpandableDelegate(ITitledEntity item);
    public delegate bool LabelIsEditableDelegate(ITitledEntity item);
    public delegate bool AllowDropNonExpandableDelegate(ITitledEntity item);
    public delegate TreeNode SetTreeNodeDelegate(ITitledEntity item);

    public partial class CategoryTreeControl : UserControl
    {
        public CategoryTreeControl()
        {
            InitializeComponent();
            AllowDrop = true;
            ItemIsExplandableRoutine = new ItemIsExpandableDelegate(ItemIsExpandable);
            SetTreeNodeRoutine = new SetTreeNodeDelegate(SetTreeNode);

            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            treeView1.NodeMouseDoubleClick += treeView1_NodeMouseDoubleClick;
            treeView1.BeforeExpand += treeView1_BeforeExpand;
            treeView1.AfterExpand += treeView1_AfterExpand;
            treeView1.BeforeLabelEdit += treeView1_BeforeLabelEdit;
            treeView1.AfterLabelEdit += treeView1_AfterLabelEdit;
            treeView1.AfterSelect += treeView1_AfterSelect;

            treeView1.DragDrop += treeView1_DragDrop;
            treeView1.DragEnter += treeView1_DragEnter;
            treeView1.ItemDrag += treeView1_ItemDrag;

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //if (e == null)
            //{
            //    // called from NodeMouseClick - user selected a node that was already selected...
            //}
            //else
            //{
            //    // https://msdn.microsoft.com/en-us/library/system.windows.forms.treeview.afterselect.aspx
            //    // see EventArgs ^^^ for more options...
            //}

            //if (ItemActivated != null)
            //    ItemActivated(this, new ItemEventArgs(SelectedItem));
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //if (e.Node != null && e.Button == MouseButtons.Right)
            //{

            //}
            //// http://stackoverflow.com/questions/1671153/why-isnt-there-a-selectednodechanged-event-for-windows-forms-treeview
            //if (e.Node == treeView1.SelectedNode)
            //    treeView1_AfterSelect(sender, null);
        }

        public ImageList ImageList
        {
            set { treeView1.ImageList = value; }
        }

        public event ItemExpandingHandler ItemExpanding;
        public event ItemRenamedHandler ItemRenamed;
        public event ItemMovedHandler ItemMoved;

        public event EventHandler<ItemDblClickedEventArgs> ItemDblClicked;
        public event EventHandler ItemClicked;

        /// <summary>
        /// Used to pass in a custom predicate that informs the tree whether the item
        /// is an expandable item with child items. If one is not assigned, the default
        /// ItemIsExpandable function is called, which always returns true.
        /// http://www.experts-exchange.com/Programming/Languages/C_Sharp/Q_23514825.html
        /// http://stackoverflow.com/questions/3135371/properties-wont-get-serialized-into-the-designer-cs-file
        /// http://stackoverflow.com/questions/4973506/failed-to-create-component-type-is-not-marked-as-serializable
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ItemIsExpandableDelegate ItemIsExplandableRoutine
        {
            get;
            set;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LabelIsEditableDelegate LabelIsEditableRoutine
        {
            get;
            set;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LabelIsEditableDelegate AllowDropNonExpandableRoutine
        {
            get;
            set;
        }

        /// <summary>
        /// Intercept the creation of a node in the tree to set the properties (tag, name, key etc.) to what you require.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SetTreeNodeDelegate SetTreeNodeRoutine
        {
            get;
            set;
        }

        public bool ItemsLoaded
        {
            get
            {
                return (treeView1.Nodes.Count == 0) ? false : true;
            }
        }

        public ITitledEntity SelectedItem
        {
            get
            {
                ITitledEntity item = null;

                if (this.treeView1.SelectedNode != null)
                {
                    if (this.treeView1.SelectedNode.Tag != null)
                        item = this.treeView1.SelectedNode.Tag as ITitledEntity;
                }
                return item;
            }
        }

        public ITitledEntity SelectedItemParent
        {
            get
            {
                ITitledEntity item = null;

                if (this.treeView1.SelectedNode != null && this.treeView1.SelectedNode.Parent != null)
                {
                    item = this.treeView1.SelectedNode.Parent.Tag as ITitledEntity;
                }
                return item;
            }
        }

        public void ClearItems()
        {
            treeView1.Nodes.Clear();
        }

        public void AddItem(ITitledEntity item, ITitledEntity relative, bool isChild)
        {
            TreeNode nd = null;
            TreeNode relativeNode = null;

            if (relative != null)
                relativeNode = GetNodeByKey(relative.Id);

            if (relativeNode != null)
            {
                if (isChild)
                {
                    nd = CreateTreeNode(item, relativeNode, false);
                    relativeNode.Expand();
                }
                else
                {
                    if (relativeNode.Parent != null)
                        nd = CreateTreeNode(item, relativeNode.Parent, false);
                    else
                        nd = CreateTreeNode(item, null, false);
                }
            }
            else
                nd = CreateTreeNode(item, null, false);
        }

        public void DeleteItem(ITitledEntity item)
        {
            TreeNode node = GetNodeByKey(item.Id);
            if (node.Parent == null)
                treeView1.Nodes.Remove(node);
            else
                node.Parent.Nodes.Remove(node);
        }

        public void LoadItemLevel(List<ITitledEntity> items, ITitledEntity parent)
        {
            TreeNode parentNode = null;
            if (parent != null)
            {
                parentNode = GetNodeByKey(parent.Id);
                if (parentNode != null)
                    parentNode.Nodes.Clear();
            }
            else
                treeView1.Nodes.Clear();

            foreach (ITitledEntity navItem in items)
                CreateTreeNode(navItem, parentNode, false);
        }

        private TreeNode GetNodeByKey(string key)
        {
            TreeNode foundNode = null;
            TreeNode[] nodesCollection = treeView1.Nodes.Find(key, true);
            if (nodesCollection.Length == 1)
            {
                foundNode = nodesCollection[0];
            }
            return foundNode;
        }

        private TreeNode CreateDummyNode(TreeNode parent)
        {
            TreeNode dummy = new TreeNode();
            dummy.Text = "dummy";
            parent.Nodes.Add(dummy);
            return dummy;
        }

        private TreeNode SetTreeNode(ITitledEntity item)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.Name = item.Id;
            treeNode.Text = item.Title;
            treeNode.Tag = item;

            return treeNode;
        }

        private TreeNode CreateTreeNode(ITitledEntity item, TreeNode parent, bool editAfterCreate)
        {
            TreeNode nd = SetTreeNodeRoutine(item);

            if (parent == null)
            {
                treeView1.Nodes.Add(nd);
            }
            else
            {
                parent.Nodes.Add(nd);
            }

            if (ItemIsExplandableRoutine(item))
                CreateDummyNode(nd);

            if (editAfterCreate)
            {
                treeView1.Focus();
                nd.EnsureVisible();
                nd.BeginEdit();
            }
            return nd;
        }

        private void RemoveDummyNode(TreeNode parent)
        {
            TreeNode dummy = null;
            foreach (TreeNode nd in parent.Nodes)
            {
                if (nd.Text == "dummy")
                {
                    dummy = nd;
                    break;
                }
            }
            if (dummy != null)
                parent.Nodes.Remove(dummy);
        }

        private bool ItemIsExpandable(ITitledEntity item)
        {
            return true;
        }

        private bool LabelIsEditable(ITitledEntity item)
        {
            return true;
        }

        public bool AllowDropNonExpandable(ITitledEntity item)
        {
            return true;
        }

        private void raiseItemMovedEvent(TreeNode displacedNode, TreeNode newParent)
        {
            if (ItemMoved != null)
            {
                if (newParent != null)
                    ItemMoved(this,
                      new ItemMovedEventArgs(displacedNode.Tag as ITitledEntity,
                      newParent.Tag as ITitledEntity));
                else
                    ItemMoved(this,
                      new ItemMovedEventArgs(displacedNode.Tag as ITitledEntity,
                      null));
            }
        }

        #region Drag and Drop Operations

        private TreeNode sourceNode = null;

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.sourceNode = (TreeNode)e.Item;

            if (AllowDropNonExpandableRoutine(this.sourceNode.Tag as ITitledEntity))
            {
                string strItem = e.Item.ToString();
                DoDragDrop(strItem, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void treeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
            Point position = treeView1.PointToClient(new Point(e.X, e.Y)); // new Point(e.X, e.Y);
            TreeNode dropNode = treeView1.GetNodeAt(position);


            if (dropNode != null && dropNode.Parent != this.sourceNode && ItemIsExplandableRoutine(dropNode.Tag as ITitledEntity))
            {
                TreeNode dragNode = this.sourceNode;
                treeView1.Nodes.Remove(this.sourceNode);
                dropNode.Nodes.Insert(0, dragNode);
                raiseItemMovedEvent(dragNode, dropNode);
            }
            else if (dropNode == null && ItemIsExplandableRoutine(this.sourceNode.Tag as ITitledEntity))
            {
                TreeNode dragNode = this.sourceNode;
                treeView1.Nodes.Remove(this.sourceNode);
                treeView1.Nodes.Insert(0, dragNode);
                raiseItemMovedEvent(dragNode, dropNode);
            }
        }

        #endregion

        #region Other Events

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Label))
            {
                e.CancelEdit = true;
                return;
            }

            if (ItemRenamed != null)
            {
                ITitledEntity item = e.Node.Tag as ITitledEntity;
                if (item != null)
                    ItemRenamed(this, new ItemRenamedEventArgs(item, e.Label));
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            RemoveDummyNode(e.Node);
            if (ItemExpanding != null)
            {
                if (e.Node.Tag != null && ItemIsExplandableRoutine(e.Node.Tag as ITitledEntity))
                    ItemExpanding(this, new ItemExpandingEventArgs(e.Node.Tag as ITitledEntity));


            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            //if (newlyAddedNode != null)
            //{
            //    treeView1.SelectedNode = newlyAddedNode;
            //    newlyAddedNode = null;
            //}
        }

        #endregion

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (ItemDblClicked != null)
            {
                ITitledEntity item = e.Node.Tag as ITitledEntity;
                if (item != null)
                    ItemDblClicked(this, new ItemDblClickedEventArgs(item));
            }
        }

        private void treeView1_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Tag == null || !LabelIsEditableRoutine(e.Node.Tag as ITitledEntity))
                e.CancelEdit = true;
        }
    }
}
