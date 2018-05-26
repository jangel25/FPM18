using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCocacolaNayMobiV2.Views.Inventarios
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FicViCpPrincipal : MasterDetailPage
	{
		public FicViCpPrincipal ()
		{
			InitializeComponent ();
            listView.SelectedItem = (listView.ItemsSource as IList)?[0];
        }
        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            // Show the detail page.
            IsPresented = false;
        }
    }
}