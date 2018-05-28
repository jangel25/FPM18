using AppCocacolaNayMobiV2.ViewModels.Planeaciones;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace AppCocacolaNayMobiV2.Views.Planeaciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViEvaPlaneacionItem : ContentPage
    {
        private object FicLoParameter { get; set; }

        public ViEvaPlaneacionItem(object ficPaParameter)
        {
            InitializeComponent();
            FicLoParameter = ficPaParameter;

            BindingContext = App.FicMetLocator.Eva_planeacionItem;
        }//Fin constructor

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as VmEvaPlaneacionItem;
            if (viewModel != null) viewModel.OnAppearing(FicLoParameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as VmEvaPlaneacionItem;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}