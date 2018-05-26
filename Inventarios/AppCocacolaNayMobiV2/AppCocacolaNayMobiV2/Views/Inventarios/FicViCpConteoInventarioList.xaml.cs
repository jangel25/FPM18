using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//--
using AppCocacolaNayMobiV2.ViewModels.Inventarios;
//using XLabs.Forms.Controls;
using System;
using AppCocacolaNayMobiV2.Models.Inventarios;
using System.Collections.Generic;
using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using System.Linq;
//using Acr.UserDialogs;
using System.Collections.ObjectModel;
using AppCocacolaNayMobiV2.Services.Inventarios;

namespace AppCocacolaNayMobiV2.Views.Inventarios
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FicViCpConteoInventarioList : ContentPage
    {
        private object FicParameter { get; set; }
        FicSrvConteoInventarioList service { get; }

        public FicViCpConteoInventarioList(object ficPaParameter)
        {
            InitializeComponent();
            FicParameter = ficPaParameter;
            service = new FicSrvConteoInventarioList();
            FicListView.IsRefreshing = true;
            //FIC: se manda llamar el metodo FicVmConteoinventario del Locator
            BindingContext = App.FicMetLocator.FicVmConteoInventarioList;

        }
        public async void FicSearchButtonPressed(object sender, EventArgs e)
        {
            string filtro = FicSearchBar.Text;
            if (filtro.Equals(""))
            {
                var lista = await service.FicMetGetListInventarios();
                FicListView.ItemsSource = FicAgregarDatos(lista);
                FicListView.IsRefreshing = true;
            }
            else
            {
                var lista = await service.FicMetGetListInventarios(filtro);
                FicListView.ItemsSource = FicAgregarDatos(lista);
                FicListView.IsRefreshing = true;
            }
        }
        private ObservableCollection<zt_inventarios> FicAgregarDatos(IList<zt_inventarios> FicPaListZt_Inventarios)
        {
            ObservableCollection<zt_inventarios> source = new ObservableCollection<zt_inventarios>();
            foreach (var item in FicPaListZt_Inventarios)
            {
                source.Add(item);
            }
            return source;
        }
        protected override void OnAppearing()
        {
            var viewModel = BindingContext as FicVmConteoInventarioList;
            if (viewModel != null) viewModel.OnAppearing(FicParameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as FicVmConteoInventarioList;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}