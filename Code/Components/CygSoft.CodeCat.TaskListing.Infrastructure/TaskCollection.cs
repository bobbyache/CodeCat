using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.TaskListing.Infrastructure
{
    public class TaskCollection<T> : ObservableCollection<T>
        where T : ITask, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler ItemPropertyChanged;
        public event EventHandler FilterChanged;
        public event EventHandler ItemAdded;

        public TaskCollection()
            : base()
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(TaskCollection_CollectionChanged);
        }

        public TaskCollection(IEnumerable<T> collection)
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(TaskCollection_CollectionChanged);
            foreach (T item in collection)
                Add(item);

        }

        public TaskCollection(List<T> list)
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(TaskCollection_CollectionChanged);
            foreach (T item in list)
                Add(item);

        }

        void TaskCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (T item in e.NewItems)
                {
                    item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                    item.FilterChanged += Item_FilterChanged;
                }
                if (e.OldItems != null)
                {
                    foreach (T item in e.OldItems)
                    {
                        item.PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                        item.FilterChanged -= Item_FilterChanged;
                    }
                }
            }
        }

        private void Item_FilterChanged(object sender, EventArgs e)
        {
            FilterChanged?.Invoke(sender, new EventArgs());
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs a = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(a);

            ItemPropertyChanged?.Invoke(sender, e);
        }
    }
}
