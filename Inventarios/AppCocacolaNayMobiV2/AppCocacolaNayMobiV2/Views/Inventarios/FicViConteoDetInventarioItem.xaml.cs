using AppCocacolaNayMobiV2.ViewModels.Inventarios;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace AppCocacolaNayMobiV2.Views.Inventarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViConteoDetInventarioItem : ContentPage
    {
        ZXingScannerPage scanPage;
        public String texto;
        private object FicLoParameter { get; set; }

        public FicViConteoDetInventarioItem(object ficPaParameter)
        {
            InitializeComponent();

            btnBarras.Clicked += ButtonScanDefault_Clicked;
            txtIdInventario.IsEnabled = false;
            txtIdConteo.IsEnabled = false;
            txtCEDI.IsEnabled = false;

            var fecha = DateTime.Now;
            lblFecha.Text = fecha.Month + "-" + fecha.Day + "-" + fecha.Year;
            lblHora.Text = fecha.Hour + ":" + fecha.Minute;
            lblUsuario.Text = "Equipo DAM-2";

            FicLoParameter = ficPaParameter;

            BindingContext = App.FicMetLocator.Zt_InvConteosItemVM;
        }//Fin constructor

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as FicVmConteoDetInventarioItem;
            if (viewModel != null) viewModel.OnAppearing(FicLoParameter);

            txtIdConteo.Text = viewModel.Zt_inventario_conteos.IdConteo.ToString();

            if (viewModel.Zt_inventario_conteos.FechaReg != null)
            {
                txtMaterial.IsEnabled = false;
                txtCantidadPZA.IsEnabled = false;
            }
            else
            {
                txtMaterial.IsEnabled = true;
                txtCantidadPZA.IsEnabled = true;
            }
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as FicVmConteoDetInventarioItem;
            if (viewModel != null) viewModel.OnDisappearing();
        }

        private async void ButtonScanDefault_Clicked(object sender, EventArgs e)
        {
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) => {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() => {
                    Navigation.PopModalAsync();
                    DisplayAlert("Código Escaneado", result.Text, "OK");
                    texto = result.Text;
                    txtBarra.Text = texto;
                });
                ;
            };

            await Navigation.PushModalAsync(scanPage);
        }
    }
}