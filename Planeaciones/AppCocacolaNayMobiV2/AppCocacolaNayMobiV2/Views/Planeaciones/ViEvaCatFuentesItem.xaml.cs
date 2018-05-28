using AppCocacolaNayMobiV2.ViewModels.Planeaciones;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace AppCocacolaNayMobiV2.Views.Planeaciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViEvaCatFuentesItem : ContentPage
    {
        private object FicLoParameter { get; set; }

        public ViEvaCatFuentesItem(object ficPaParameter)
        {
            InitializeComponent();
            FicLoParameter = ficPaParameter;

            BindingContext = App.FicMetLocator.Eva_cat_fuentesItem;
        }//Fin constructor

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as VmEvaCatFuentesItem;
            if (viewModel != null)
            {
                viewModel.OnAppearing(FicLoParameter);
                lblPlaneacion.Text = viewModel.eva_cat_fuentes_item.IdPlaneacion.ToString();
            }
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as VmEvaCatFuentesItem;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}