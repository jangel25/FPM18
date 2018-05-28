using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCocacolaNayMobiV2.ViewModels.Planeaciones;


namespace AppCocacolaNayMobiV2.Views.Planeaciones
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViEvaCatFuentesDetalle : ContentPage
	{
        private object FicLoParameter { get; set; }

		public ViEvaCatFuentesDetalle(object ficPaParameter)
		{
            InitializeComponent ();


            btnEliminar.Clicked += btnEliminar_Clicked;

            FicLoParameter = ficPaParameter;

            BindingContext = App.FicMetLocator.Eva_cat_fuentesDetalle;
        }

   
        protected override void OnAppearing()
        {
            var viewModel = BindingContext as VmEvaCatFuentesDetalle;
            if (viewModel != null) viewModel.OnAppearing(FicLoParameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as VmEvaCatFuentesDetalle;
            if (viewModel != null) viewModel.OnDisappearing();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
           bool res = await DisplayAlert("Aviso", "Se va a eliminar está planeacion, ¿Está seguro?", "Si", "No");
           if(res)
            {
                var viewModel = BindingContext as VmEvaCatFuentesDetalle;
                viewModel.DeleteCommandExecute();
            }
        }
    }
}