using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.Services.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Inventarios;
using System;
using System.Collections.Generic;
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
        //variable lista del tipo zt_cat_almacenes almacenara los registros
        List<zt_cat_almacenes> almacenes;
        //objeto de la clase que contiene la funcion para traer la lista de la tabla
        FicSrvCatAlmacenList ficSrvCatAlmacenList;
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
            //inicializar la variable 
            ficSrvCatAlmacenList = new FicSrvCatAlmacenList();
            //almaceno en la variable creada el resultado de la funcion para obt. la lista
            almacenes = ficSrvCatAlmacenList.GetAll_zt_cat_almacenes();
            //recorro la lista de almacenes
            foreach (zt_cat_almacenes almacen in almacenes)
            {
                //agregar cada elemento de la lista al picker
                pickerAlmacen.Items.Add(almacen.Almacen);
            }
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