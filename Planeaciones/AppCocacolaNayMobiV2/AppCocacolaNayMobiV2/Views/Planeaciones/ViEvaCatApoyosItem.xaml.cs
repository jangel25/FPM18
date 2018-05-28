using AppCocacolaNayMobiV2.ViewModels.Planeaciones;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace AppCocacolaNayMobiV2.Views.Planeaciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViEvaCatApoyosItem : ContentPage
    {
        private object FicLoParameter { get; set; }

        public ViEvaCatApoyosItem(object ficPaParameter)
        {
            InitializeComponent();
            FicLoParameter = ficPaParameter;

            var fecha = DateTime.Now;
            lblFecha.Text = fecha.ToString("dd-MM-yyyy");

            BindingContext = App.FicMetLocator.Eva_cat_apoyosItem;
        }//Fin constructor

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as VmEvaCatApoyosItem;
            if (viewModel != null)
            {
                viewModel.OnAppearing(FicLoParameter);
                lblPlaneacion.Text = viewModel.eva_cat_apoyos_item.IdPlaneacion.ToString();
            }
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as VmEvaCatApoyosItem;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}