using AppCocacolaNayMobiV2.ViewModels.Planeaciones;
using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace AppCocacolaNayMobiV2.Views.Planeaciones
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViEvaPlaneacionList : ContentPage
    {
        private object Parameter { get; set; }

        public ViEvaPlaneacionList(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = App.FicMetLocator.Eva_planeacionList;
        }//Fin constructor

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as VmEvaPlaneacionList;
            if (viewModel != null) viewModel.OnAppearing(Parameter);

            viewModel.filterTextChanged = OnFilterChanged;
        }//Fin OnApperaring

        private void OnFilterChanged()
        {
            var viewModel = BindingContext as VmEvaPlaneacionList;
            if (dataGrid.View != null)
            {
                this.dataGrid.View.Filter = viewModel.FilerRecords;
                this.dataGrid.View.RefreshFilter();
            }
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as VmEvaPlaneacionList;
            if (viewModel != null) viewModel.OnDisappearing();
        }//Fin OnDisappearing

        protected async void btnDetalle_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as VmEvaPlaneacionList;
            if (viewModel.seleccionoItem())
                viewModel.AddDetalleExecute();
            else
                await DisplayAlert("Aviso", "No se selecciono un registro", "OK");
        }

        protected async void btnApoyos_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as VmEvaPlaneacionList;
            if (viewModel.seleccionoItem())
                viewModel.AddApoyosExecute();
            else
                await DisplayAlert("Aviso", "No se selecciono un registro", "OK");
        }

        protected async void btnEditar_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as VmEvaPlaneacionList;
            if (viewModel.seleccionoItem())
                viewModel.AddEditarExecute();
            else
                await DisplayAlert("Aviso", "No se selecciono un registro", "OK");
        }

        protected async void btnTemas_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as VmEvaPlaneacionList;
            if (viewModel.seleccionoItem())
                viewModel.AddTemasExecute();
            else
                await DisplayAlert("Aviso", "No se selecciono un registro", "OK");
        }

        private void OnFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var viewModel = BindingContext as VmEvaPlaneacionList;
            if (e.NewTextValue == null)
                viewModel.FilterText = "";
            else
                viewModel.FilterText = e.NewTextValue;
        }

        protected async void btnFuentes_Clicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as VmEvaPlaneacionList;
            if (viewModel.seleccionoItem())
                viewModel.AddFuentesExecute();
            else
                await DisplayAlert("Aviso", "No se selecciono un registro", "OK");
        }
    }
}