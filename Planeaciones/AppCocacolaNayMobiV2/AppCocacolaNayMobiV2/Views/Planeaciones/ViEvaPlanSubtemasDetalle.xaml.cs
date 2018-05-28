using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCocacolaNayMobiV2.ViewModels.Planeaciones;


namespace AppCocacolaNayMobiV2.Views.Planeaciones
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViEvaPlanSubtemasDetalle : ContentPage
	{
        private object FicLoParameter { get; set; }

		public ViEvaPlanSubtemasDetalle(object ficPaParameter)
		{
            InitializeComponent ();


            btnEliminar.Clicked += btnEliminar_Clicked;

            FicLoParameter = ficPaParameter;

            BindingContext = App.FicMetLocator.Eva_planeacion_subtemasDetalle;
        }

   
        protected override void OnAppearing()
        {
            var viewModel = BindingContext as VmEvaPlanSubtemasDetalle;
            if (viewModel != null) viewModel.OnAppearing(FicLoParameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as VmEvaPlanSubtemasDetalle;
            if (viewModel != null) viewModel.OnDisappearing();
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
           bool res = await DisplayAlert("Aviso", "Se va a eliminar este subtema, ¿Está seguro?", "Si", "No");
           if(res)
            {
                var viewModel = BindingContext as VmEvaPlanSubtemasDetalle;
                viewModel.DeleteCommandExecute();
            }
        }

        
    }


}