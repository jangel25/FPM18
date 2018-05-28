using AppCocacolaNayMobiV2.ViewModels.Planeaciones;
using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace AppCocacolaNayMobiV2.Views.Planeaciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViEvaCatFuentesList : ContentPage
    {
        private object Parameter { get; set; }

        public ViEvaCatFuentesList(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = App.FicMetLocator.Eva_cat_fuentesList;
        }//Fin constructor

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as VmEvaCatFuentesList;
            if (viewModel != null) viewModel.OnAppearing(Parameter);

            viewModel.filterTextChanged = OnFilterChanged;
        }//Fin OnApperaring

        private void OnFilterChanged()
        {
            var viewModel = BindingContext as VmEvaCatFuentesList;
            if (dataGrid.View != null)
            {
                this.dataGrid.View.Filter = viewModel.FilerRecords;
                this.dataGrid.View.RefreshFilter();
            }
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as VmEvaCatFuentesList;
            if (viewModel != null) viewModel.OnDisappearing();
        }//Fin OnDisappearing

        protected async void btnDetalle_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as VmEvaCatFuentesList;
            if (viewModel.seleccionoItem())
                viewModel.AddDetalleExecute();
            else
                await DisplayAlert("Aviso", "No se selecciono un registro", "OK");
        }

        protected async void btnEditar_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as VmEvaCatFuentesList;
            if (viewModel.seleccionoItem())
                viewModel.AddEditarExecute();
            else
                await DisplayAlert("Aviso", "No se selecciono un registro", "OK");
        }

        protected async void btnTemas_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as VmEvaCatFuentesList;
            if (viewModel.seleccionoItem())
                viewModel.AddTemasExecute();
            else
                await DisplayAlert("Aviso", "No se selecciono un registro", "OK");
        }

        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var viewModel = BindingContext as VmEvaCatFuentesList;
            if (e.NewTextValue == null)
                viewModel.FilterText = "";
            else
                viewModel.FilterText = e.NewTextValue;
        }
    }
}