using AppCocacolaNayMobiV2.ViewModels.Planeaciones;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace AppCocacolaNayMobiV2.Views.Planeaciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViEvaPlaneacionTemasItem : ContentPage
    {
        private object FicLoParameter { get; set; }

        public ViEvaPlaneacionTemasItem(object ficPaParameter)
        {
            InitializeComponent();
            FicLoParameter = ficPaParameter;

            BindingContext = App.FicMetLocator.Eva_planeacion_temasItem;
        }//Fin constructor

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as VmEvaPlaneacionTemasItem;
            if (viewModel != null) {
                viewModel.OnAppearing(FicLoParameter);
                lblPlaneacion.Text = viewModel.eva_planeacion_temas_item.IdPlaneacion.ToString();
            }
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as VmEvaPlaneacionTemasItem;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}