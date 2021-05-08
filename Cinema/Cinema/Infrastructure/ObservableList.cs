using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure
{
    class ObservableList<T> : ObservableCollection<T>
    {
        public void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }
    }
}
