using CygSoft.CodeCat.Category.Infrastructure;
using CygSoft.CodeCat.Domain;
using CygSoft.CodeCat.Domain.Base;
using CygSoft.CodeCat.Infrastructure;
using CygSoft.CodeCat.Search.KeywordIndex.Infrastructure;
using CygSoft.CodeCat.UI.WinForms.Controls;
using CygSoft.CodeCat.UI.WinForms.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CygSoft.CodeCat.UI.WinForms.Docked
{
    public partial class CategoryForm : DockContent
    {
        public event EventHandler<OpenSnippetEventArgs> OpenSnippet;

        private AppFacade application;

        public CategoryForm(AppFacade application)
        {
            InitializeComponent();
            this.application = application;

            categoryTreeControl1.ItemIsExplandableRoutine = ItemIsCategory;
            categoryTreeControl1.LabelIsEditableRoutine = LabelIsEditable;
            categoryTreeControl1.AllowDropNonExpandableRoutine = AllowDropItem;
            //categoryTreeControl1.SetTreeNodeRoutine = SetTreeNode;
            categoryTreeControl1.ItemExpanding += CategoryTree_ItemExpanding;
            categoryTreeControl1.ItemDblClicked += CategoryTree_ItemDblClicked;
            categoryTreeControl1.ItemMoved += CategoryTree_ItemMoved;
            categoryTreeControl1.ItemRenamed += CategoryTree_ItemRenamed;
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

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (categoryTreeControl1.ItemsLoaded)
            {
                SearchDialog dialog = new SearchDialog(application);
                DialogResult result = dialog.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    IKeywordIndexItem indexItem = dialog.SelectedSnippet as IKeywordIndexItem;

                    ITitledEntity parentEntity = null;

                    if (categoryTreeControl1.SelectedItem is IItemCategory)
                    {
                        parentEntity = categoryTreeControl1.SelectedItem;
                        application.AddCategoryItem(indexItem, parentEntity.Id);
                        categoryTreeControl1.AddItem(indexItem, categoryTreeControl1.SelectedItem, true);
                    }
                    else
                    {
                        parentEntity = categoryTreeControl1.SelectedItemParent;
                        application.AddCategoryItem(indexItem, parentEntity.Id);
                        categoryTreeControl1.AddItem(indexItem, categoryTreeControl1.SelectedItem, false);
                    }
                }
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            ITitledEntity item = categoryTreeControl1.SelectedItem;
            application.DeleteCategoryOrItem(item);
            categoryTreeControl1.DeleteItem(item);
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            ITitledEntity item = categoryTreeControl1.SelectedItem;
            application.DeleteCategoryOrItem(item);
            categoryTreeControl1.DeleteItem(item);
        }
    }
}
