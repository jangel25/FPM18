using System;
using System.Collections.Generic;
using System.Text;

namespace AppCocacolaNayMobiV2.Models.Inventarios
{
    public class SelectableItem<T>
    {
        public bool IsSelected { get; set; }
        public T Item { get; set; }
    }
}
