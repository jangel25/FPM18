using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//--
using AppCocacolaNayMobiV2.ViewModels.Inventarios;

namespace AppCocacolaNayMobiV2.Views.Inventarios
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FicViCpAlmacenList : ContentPage
	{
        private object FicParameter { get; set; }

        public FicViCpAlmacenList (object ficPaParameter)
		{
			InitializeComponent ();

            FicParameter = ficPaParameter;
            BindingContext = App.FicMetLocator.FicVmAlmacenList;
		}

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as FicVmAlmacenList;
            if (viewModel != null) viewModel.OnAppearing(FicParameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as FicVmAlmacenList;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}