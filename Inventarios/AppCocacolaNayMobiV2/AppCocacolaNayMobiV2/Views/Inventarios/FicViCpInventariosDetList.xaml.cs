using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Inventarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCocacolaNayMobiV2.Services.Inventarios;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace AppCocacolaNayMobiV2.Views.Inventarios
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FicViCpInventariosDetList : ContentPage
	{
        private object FicParameter { get; set; }
        FicSrvConteoInventarioList service { get; }

        public FicViCpInventariosDetList(object ficPaParameter)
        {
            InitializeComponent();
            FicParameter = ficPaParameter;
            service = new FicSrvConteoInventarioList();
            FicListView.IsRefreshing = true;
            BindingContext = App.FicMetLocator.FicVmInventariosDetList;
        }

        public async void FicSearchButtonPressed(object sender, EventArgs e)
        {
            string filtro = FicSearchBar.Text;
            if (filtro.Equals(""))
            {
                var lista = await service.FicMetGetListInventariosDet();
                FicListView.ItemsSource = FicAgregarDatos(lista);
                FicListView.IsRefreshing = true;
            }
            else
            {
                var lista = await service.FicMetGetListInventariosDet(filtro);
                FicListView.ItemsSource = FicAgregarDatos(lista);
                FicListView.IsRefreshing = true;
            }
            //FicListView.IsRefreshing = true;
            /*FicListView.ItemsSource = lista;
            if (filtro.Equals(""))
            {
                return;
            }
            lista = (IEnumerable<zt_inventarios_det>)FicListView.ItemsSource;
            IEnumerable<zt_inventarios_det> searchResult = lista.Where(item => item.IdInventario == filtro).ToList();
            FicListView.ItemsSource = searchResult;
            FicListView.IsRefreshing = true;*/
        }
        
        private ObservableCollection<zt_inventarios_det> FicAgregarDatos(IList<zt_inventarios_det> FicPaListZt_Inventarios)
        {
            ObservableCollection<zt_inventarios_det> source = new ObservableCollection<zt_inventarios_det>();
            foreach (var item in FicPaListZt_Inventarios)
            {
                source.Add(item);
            }
            return source;
        }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as FicVmInventariosDetList;
            if (viewModel != null) viewModel.OnAppearing(FicParameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as FicVmInventariosDetList;
            if (viewModel != null) viewModel.OnDisappearing();
        }
    }
}