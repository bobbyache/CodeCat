using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.CodeCat.Infrastructure
{
    #region TrulyObservableCollection
    public class TrulyObservableCollection<T> : ObservableCollection<T>
        where T : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler ItemPropertyChanged;

        public TrulyObservableCollection()
            : base()
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(TrulyObservableCollection_CollectionChanged);
        }

        public TrulyObservableCollection(IEnumerable<T> collection)
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(TrulyObservableCollection_CollectionChanged);
            foreach (T item in collection)
                Add(item);
            
        }

        public TrulyObservableCollection(List<T> list)
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(TrulyObservableCollection_CollectionChanged);
            foreach (T item in list)
                Add(item);
            
        }

        void TrulyObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Object item in e.NewItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                }
            }
            if (e.OldItems != null)
            {
                foreach (Object item in e.OldItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                }
            }
        }

        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs a = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(a);

            ItemPropertyChanged?.Invoke(sender, e);
        }
    }
    #endregion
}
