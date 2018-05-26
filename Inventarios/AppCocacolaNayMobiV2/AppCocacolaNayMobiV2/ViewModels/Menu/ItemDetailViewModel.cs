using System;

using AppCocacolaNayMobiV2.Models;
using AppCocacolaNayMobiV2.Models.Menu;

namespace AppCocacolaNayMobiV2.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
