using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCocacolaNayMobiV2.ViewModels.Inventarios;


namespace AppCocacolaNayMobiV2.Views.Inventarios
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FicViConteoDetInventarioDetalle : ContentPage
	{
        private object FicLoParameter { get; set; }

		public FicViConteoDetInventarioDetalle(object ficPaParameter)
		{
            InitializeComponent ();


            btnEliminar.Clicked += btnEliminar_Clicked;

            FicLoParameter = ficPaParameter;

            BindingContext = App.FicMetLocator.Zt_InvConteosDetalleVM;
        }

   
        protected override void OnAppearing()
        {
            var viewModel = BindingContext as FicVmConteoDetInventarioDetalle;
            if (viewModel != null) viewModel.OnAppearing(FicLoParameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as FicVmConteoDetInventarioDetalle;
            if (viewModel != null) viewModel.OnDisappearing();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
           bool res = await DisplayAlert("Aviso", "Se va a eliminar este conteo, ¿Está seguro?", "Si", "No");
           if(res)
            {
                var viewModel = BindingContext as FicVmConteoDetInventarioDetalle;
                viewModel.DeleteCommandExecute();
            }
        }

        
    }


}