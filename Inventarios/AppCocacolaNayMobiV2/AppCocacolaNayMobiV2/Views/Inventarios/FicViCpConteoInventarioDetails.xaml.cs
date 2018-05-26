using AppCocacolaNayMobiV2.ViewModels.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCocacolaNayMobiV2.Views.Inventarios
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FicViCpConteoInventarioDetails : ContentPage
	{
        private object FicLoParameter { get; set; }
        public FicViCpConteoInventarioDetails (object ficPaParameter)
		{
			InitializeComponent ();
            FicLoParameter = ficPaParameter;
            if (FicLoParameter == null)
            {
                DisplayAlert("Advertencia", "Debe seleccionar un registro", "OK");
                return;
            }
            BindingContext = App.FicMetLocator.FicVmConteoInventarioDetails;
        }
        protected override void OnAppearing()
        {
            //FIC: Aqui se declara una variable de tipo ViewModel Item
            var FicViewModel = BindingContext as FicVmConteoInventarioDetails;            
            if (FicViewModel != null) FicViewModel.OnAppearing(FicLoParameter);
        }
        protected override void OnDisappearing()
        {
            var FicViewModel = BindingContext as FicVmConteoInventarioDetails;
            if (FicViewModel != null) FicViewModel.OnDisappearing();
        }
    }
}