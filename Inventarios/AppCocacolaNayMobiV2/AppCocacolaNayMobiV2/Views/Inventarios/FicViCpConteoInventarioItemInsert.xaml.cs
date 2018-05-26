using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCocacolaNayMobiV2.ViewModels.Inventarios;
using System;

namespace AppCocacolaNayMobiV2.Views.Inventarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViCpConteoInventarioItemInsert : ContentPage
    {
        private object FicLoParameter { get; set; }

        public FicViCpConteoInventarioItemInsert(object ficPaParameter)
        {
            InitializeComponent();

            FicLoParameter = ficPaParameter;
            if (FicLoParameter == null)
            {
                DisplayAlert("Advertencia", "Debe seleccionar un registro", "OK");
                return;
            }
            BindingContext = App.FicMetLocator.FicVmConteoInventarioItemInsert;

            //Obtener FechaActual y HoraActual
            var fecha_hora = DateTime.Now;
            fechaActual.Text = fecha_hora.Year + "-" + fecha_hora.Month + "-" + fecha_hora.Day;
            horaActual.Text = fecha_hora.Hour + ":" + fecha_hora.Minute + ":" + fecha_hora.Second;

        }

        protected override void OnAppearing()
        {
            //FIC: Aqui se declara una variable de tipo ViewModel Item
            var FicViewModel = BindingContext as FicVmConteoInventarioItemInsert;
            if (FicViewModel != null) FicViewModel.OnAppearing(FicLoParameter);
        }

        protected override void OnDisappearing()
        {
            var FicViewModel = BindingContext as FicVmConteoInventarioItemInsert;
            if (FicViewModel != null) FicViewModel.OnDisappearing();
        }
    }
}