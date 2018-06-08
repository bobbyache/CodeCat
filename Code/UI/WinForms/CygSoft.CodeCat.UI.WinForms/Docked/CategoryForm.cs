using CygSoft.CodeCat.Domain.Code;
using CygSoft.CodeCat.Domain.Topics;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Infrastructure.Graphics;
using CygSoft.CodeCat.UI.WinForms.Controls;
using CygSoft.CodeCat.UI.WinForms.Dialogs;
using System;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static CygSoft.CodeCat.UI.Resources.ImageResources;

namespace CygSoft.CodeCat.UI.WinForms.Docked
{
    public partial class CategoryForm : DockContent
    {
        public event EventHandler<TopicIndexEventArgs> OpenWorkItem;

        private IAppFacade application;
        private IImageResources imageResources;
        private int openCategoryImageIndex;
        private int closedCategoryImageIndex;

        public CategoryForm(IAppFacade application, IImageResources imageResources)
        {
            InitializeComponent();

            if (imageResources == null)
                throw new ArgumentNullException("Image Resources is a required constructor parameter and cannot be null");

            this.imageResources = imageResources;

            openCategoryImageIndex = imageResources.Get(ImageKeys.OpenCategory, false).Index;
            closedCategoryImageIndex = imageResources.Get(ImageKeys.ClosedCategory, false).Index;

            if (application == null)
                throw new ArgumentNullException("Application is a required constructor parameter and cannot be null");
            this.application = application;

            Text = "Categories";
            Icon = imageResources.Get(ImageKeys.OpenCategory, false).Icon;
            HideOnClose = true;

            btnAddCategory.Image = imageResources.GetImage(ImageKeys.AddTemplate);
            btnDelete.Image = imageResources.GetImage(ImageKeys.RemoveTemplate);

            categoryTreeControl1.ImageList = imageResources.ImageList;
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
                int imageIndex = GetKeywordIndexItemImage(indexItem.IndexItem).Index;
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

        private IImageOutput GetKeywordIndexItemImage(IKeywordIndexItem item)
        {
            string imageKey = null;

            if (item is ICodeKeywordIndexItem)
                imageKey = (item as ICodeKeywordIndexItem).Syntax;

            else if (item is ITopicKeywordIndexItem)
                imageKey = TopicSections.CodeGroup;

            return imageResources.GetKeywordIndexItemImage(imageKey);
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
                OpenWorkItem?.Invoke(this, new TopicIndexEventArgs((e.Item as ICategorizedKeywordIndexItem).IndexItem));
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
                SearchDialog dialog = new SearchDialog(application, imageResources);
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
