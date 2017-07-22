using CygSoft.CodeCat.Category.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.Controls;
using CygSoft.CodeCat.UI.WinForms.Dialogs;
using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms.Docked
{
    public partial class CategoryForm : DockContent
    {
        public event EventHandler<OpenSnippetEventArgs> OpenSnippet;

        private AppFacade application;
        private int openCategoryImageIndex = IconRepository.Get(Constants.ImageKeys.OpenCategory, false).Index;
        private int closedCategoryImageIndex = IconRepository.Get(Constants.ImageKeys.ClosedCategory, false).Index;

        public CategoryForm(AppFacade application)
        {
            InitializeComponent();
            this.application = application;
            this.Text = "Categories";
            this.Icon = IconRepository.Get(Constants.ImageKeys.OpenCategory, false).Icon;

            btnAddCategory.Image = Gui.Resources.GetImage(Constants.ImageKeys.AddTemplate);
            btnDelete.Image = Gui.Resources.GetImage(Constants.ImageKeys.RemoveTemplate);

            categoryTreeControl1.ImageList = IconRepository.ImageList;
            categoryTreeControl1.ItemIsExplandableRoutine = ItemIsCategory;
            categoryTreeControl1.LabelIsEditableRoutine = LabelIsEditable;
            categoryTreeControl1.AllowDropNonExpandableRoutine = AllowDropItem;
            categoryTreeControl1.SetTreeNodeRoutine = SetTreeNode;
            categoryTreeControl1.ItemExpanding += CategoryTree_ItemExpanding;
            categoryTreeControl1.ItemDblClicked += CategoryTree_ItemDblClicked;
            categoryTreeControl1.ItemMoved += CategoryTree_ItemMoved;
            categoryTreeControl1.ItemRenamed += CategoryTree_ItemRenamed;
        }

        private TreeNode SetTreeNode(ITitledEntity item)
        {
            TreeNode treeNode = new TreeNode();

            treeNode.Name = item.Id;
            treeNode.Text = item.Title;
            treeNode.Tag = item;

            if (item is ICategorizedKeywordIndexItem)
            {
                ICategorizedKeywordIndexItem indexItem = item as ICategorizedKeywordIndexItem;
                int imageIndex = IconRepository.GetKeywordIndexItemImage(indexItem.IndexItem).Index;
                treeNode.ImageIndex = imageIndex;
                treeNode.SelectedImageIndex = imageIndex;
            }
            else
            {
                treeNode.ImageIndex = closedCategoryImageIndex;
                treeNode.SelectedImageIndex = openCategoryImageIndex;
            }

            return treeNode;
        }

        private bool AllowDropItem(ITitledEntity entity)
        {
            return entity is IItemCategory;
        }

        private bool ItemIsCategory(ITitledEntity entity)
        {
            return entity is IItemCategory;
        }

        private bool LabelIsEditable(ITitledEntity entity)
        {
            return entity is IItemCategory;
        }

        public void LoadCategories()
        {
            categoryTreeControl1.LoadItemLevel(application.GetRootCategories(), null);
        }

        private void btnAddCategoryAsSibling_Click(object sender, EventArgs e)
        {
            IItemCategory newSiblingCategory = application.CreateCategory();

            if (!categoryTreeControl1.ItemsLoaded)
            {
                application.AddCategory(newSiblingCategory, string.Empty);
                categoryTreeControl1.AddItem(newSiblingCategory, null, false);

            }
            else if (categoryTreeControl1.SelectedItem is IItemCategory)
            {
                if (categoryTreeControl1.SelectedItemParent != null)
                {
                    application.AddCategory(newSiblingCategory, categoryTreeControl1.SelectedItemParent.Id);
                    categoryTreeControl1.AddItem(newSiblingCategory, categoryTreeControl1.SelectedItem, false);
                }
                else if (categoryTreeControl1.SelectedItemParent == null)
                {
                    application.AddCategory(newSiblingCategory, string.Empty);
                    categoryTreeControl1.AddItem(newSiblingCategory, null, false);
                }
            }
        }

        private void btnAddCategoryAsChild_Click(object sender, EventArgs e)
        {
            IItemCategory entity = application.CreateCategory();
            entity.Title = "New Category";

            if (!categoryTreeControl1.ItemsLoaded)
            {
                application.AddCategory(entity, string.Empty);
                categoryTreeControl1.AddItem(entity, null, false);

            }
            else if (categoryTreeControl1.SelectedItem is IItemCategory)
            {
                if (categoryTreeControl1.SelectedItem != null)
                {
                    application.AddCategory(entity, categoryTreeControl1.SelectedItem.Id);
                    categoryTreeControl1.AddItem(entity, categoryTreeControl1.SelectedItem, true);
                }
                else
                {
                    application.AddCategory(entity, string.Empty);
                    categoryTreeControl1.AddItem(entity, null, false);
                }
            }
        }

        private void CategoryTree_ItemRenamed(object sender, ItemRenamedEventArgs e)
        {
            application.RenameCategory(e.Item.Id, e.NewTitle);
        }

        private void CategoryTree_ItemDblClicked(object sender, ItemDblClickedEventArgs e)
        {
            if (e.Item is IItemCategory)
                return;

            if (e.Item is ICategorizedKeywordIndexItem)
                OpenSnippet?.Invoke(this, new OpenSnippetEventArgs((e.Item as ICategorizedKeywordIndexItem).IndexItem));
        }

        private void CategoryTree_ItemExpanding(object sender, ItemExpandingEventArgs e)
        {
            categoryTreeControl1.LoadItemLevel(application.GetChildCategorizedItemsByCategory(e.ExpandingItem.Id).OfType<ITitledEntity>().ToList(), e.ExpandingItem);
        }

        private void CategoryTree_ItemMoved(object sender, ItemMovedEventArgs e)
        {
            application.MoveCategory(e.DisplacedItem.Id, e.NewParent.Id);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ITitledEntity item = categoryTreeControl1.SelectedItem;
            application.DeleteCategoryOrItem(item);
            categoryTreeControl1.DeleteItem(item);
        }

        private void btnAddCategoryItem_Click(object sender, EventArgs e)
        {
            if (categoryTreeControl1.ItemsLoaded)
            {
                SearchDialog dialog = new SearchDialog(application);
                DialogResult result = dialog.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    IKeywordIndexItem[] indexItems = dialog.SelectedSnippets;

                    ITitledEntity parentEntity = null;

                    if (categoryTreeControl1.SelectedItem is IItemCategory)
                    {
                        parentEntity = categoryTreeControl1.SelectedItem;
                        foreach (IKeywordIndexItem indexItem in indexItems)
                            AddItem(indexItem, parentEntity.Id);
                    }
                    else
                    {
                        parentEntity = categoryTreeControl1.SelectedItemParent;
                        foreach (IKeywordIndexItem indexItem in indexItems)
                            AddItem(indexItem, parentEntity.Id);
                    }
                }
            }
        }

        private void AddItem(IKeywordIndexItem indexItem, string parentId)
        {
            ICategorizedKeywordIndexItem newItem = application.AddCategoryItem(indexItem, parentId);
            categoryTreeControl1.AddItem(newItem, categoryTreeControl1.SelectedItem, true);
        }
    }
}
