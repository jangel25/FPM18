using System.Collections.ObjectModel;
using System.Windows.Input;
using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Base;
//using Acr.UserDialogs;
using System;
using AppCocacolaNayMobiV2.Views.Inventarios;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCocacolaNayMobiV2.ViewModels.Inventarios
{
    public class FicVmConteoInventarioList : FicViewModelBase
    {
        private ObservableCollection<zt_inventarios> FicOcZt_inventarios_Items;
        private zt_inventarios FicZt_inventarios_SelectedItem;
        //public string ficFilter;
        //public bool ficIsRefreshing;
        public SearchBar ficSearchBar;
        
        //private ObservableCollection<SelectableItem<zt_inventarios>> FicOc_Inventarios_Seleccionados;

        private ICommand ficAddCommand;
        private ICommand ficEditCommand;
        private ICommand ficDetailsCommand;
        private ICommand ficDeleteCommand;
        private ICommand ficDetCommand;
        //Agregado por el equipo de CASAS
        private ICommand _ficConteosDetInventarios;
        //private ICommand ficSearchButtonPressed;
        //private ICommand ficSearchCommand;
        //private ICommand ficRefreshCommand;

        private IFicSrvNavigationInventario FicLoSrvNavigationInventario;
        private IFicSrvConteoInventario FicLoSrvConteoInventario;

        //FIC: Constructor
        public FicVmConteoInventarioList(
            IFicSrvNavigationInventario ficPaSrvNavigationInventario,
            IFicSrvConteoInventario ficPaSrvConteoInventario)
        {
            //FIC: se asigna el objeto que se recibe como parametro de tipo navigation services
            FicLoSrvNavigationInventario = ficPaSrvNavigationInventario;
            //FIC: se asigna el objeto que se recibe como parametro de tipo SqlServices 
            FicLoSrvConteoInventario = ficPaSrvConteoInventario;
        }

        //FIC: Metodo para obtener la lista de registros de inventarios
        public ObservableCollection<zt_inventarios> FicMetZt_inventarios_Items
        {
            get { return FicOcZt_inventarios_Items; }
            set
            {
                FicOcZt_inventarios_Items = value;
                RaisePropertyChanged();
            }
        }

        //FIC: Metodo para tomar solo un registro de la lista de registros de inventarios
        /*public zt_inventarios FicMetZt_inventarios_SelectedItem
        {
            get { return FicZt_inventarios_SelectedItem; }
            set
            {
                FicZt_inventarios_SelectedItem = value;
                FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmConteoInventarioItem>(FicZt_inventarios_SelectedItem);
            }
        }*/
        public zt_inventarios FicMetZt_inventarios_SelectedItem
        {
            get { return FicZt_inventarios_SelectedItem; }
            set
            {
                FicZt_inventarios_SelectedItem = value;
            }
        }
        /*public SearchBar FicSearchBar
        {
            get { return ficSearchBar; }
            set
            {
                ficSearchBar = value;
                RaisePropertyChanged();
            }
        }*/
        /*public string FicFilter
        {
            get { return ficFilter; }
            set
            {
                if (ficFilter != value)
                {
                    ficFilter = value;
                    RaisePropertyChanged();
                    //FicSearch();
                    //RaisePropertyChanged(FicFilter);
                    //PropertyChanged.invoke(this, new PropertyChangedEventsArgs(nameof(FicFilter)));
                }
            }
        }*/
        /*public bool FicIsRefreshing
        {
            get
            {
                return ficIsRefreshing;
            }
            set
            {
                if (ficIsRefreshing != value)
                {
                    ficIsRefreshing = value;
                    RaisePropertyChanged();
                }
            }
        }*/
        /*public void FicSearch()
        {
            FicIsRefreshing = true;

            if (string.IsNullOrEmpty(FicFilter))
            {
                FicMetZt_inventarios_Items = new ObservableCollection<zt_inventarios>(
                    FicOcZt_inventarios_Items.OrderBy(x => x.CEDI));
            }
            else
            {
                FicMetZt_inventarios_Items = new ObservableCollection<zt_inventarios>(FicOcZt_inventarios_Items
                    .Where(x => x.CEDI.ToLower().Contains(FicFilter.ToLower()))
                    .OrderBy(x => x.CEDI));
            }
            FicIsRefreshing = false;
        }*/

        //FIC: Metodo de tipo comando para agregar un registro 
        public ICommand FicMetAddCommand
        {
            get { return ficAddCommand = ficAddCommand ?? new FicVmDelegateCommand(AddCommandExecute); }
        }

        //Método de tipo comando para editar un registro
        public ICommand FicMetEditCommand
        {
            get { return ficEditCommand = ficEditCommand ?? new FicVmDelegateCommand(EditCommandExecute); }
        }

        //Método de tipo comando para ver los detalles de un registro
        public ICommand FicMetDetailsCommand
        {
            get { return ficDetailsCommand = ficDetailsCommand ?? new FicVmDelegateCommand(DetailsCommandExecute); }
        }

        //Método de tipo comando para eliminar un registro
        public ICommand FicMetDeleteCommand
        {
            get { return ficDeleteCommand = ficDeleteCommand ?? new FicVmDelegateCommand(DeleteCommandExecute); }
        }

        //Método de tipo comando para ir a zt_inventarios_det
        public ICommand FicMetDetCommand
        {
            get { return ficDetCommand = ficDetCommand ?? new FicVmDelegateCommand(DetCommandExecute); }
        }

        //Método de tipo comando para ir a zt_inventarios_conteos 
        //Agregado por equipo CASAS
        public ICommand FicMetConteosCommand
        {
            get { return _ficConteosDetInventarios = _ficConteosDetInventarios ?? new FicVmDelegateCommand(DetCommandConteoDetExecute); }
        }
        /*public ICommand FicSearchButtonPressed
        {
            get { return ficSearchButtonPressed = ficSearchButtonPressed ?? new FicVmDelegateCommand(SearchButtonPressedCommandExecute); }
        }*/
        /*public ICommand FicSearchCommand
        {
            get { return ficSearchCommand = ficSearchCommand ?? new FicVmDelegateCommand(SearchCommandExecute); }
        }
        public ICommand FicRefreshCommand
        {
            get
            {
                return ficRefreshCommand = ficRefreshCommand ?? new FicVmDelegateCommand(RefreshCommandExecute);
            }
        }*/

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            //FIC: Ejecuto uno de los metodos definidos en los servicios de Interfaz de inventarios
            var result = await FicLoSrvConteoInventario.FicMetGetListInventarios();

            FicMetZt_inventarios_Items = new ObservableCollection<zt_inventarios>();
            foreach (var ficPaItem in result)
            {
                FicMetZt_inventarios_Items.Add(ficPaItem);
            }
            FicZt_inventarios_SelectedItem = null;
        }

        // Agregado por EQUIPO CASAS
        private void DetCommandConteoDetExecute()
        {
            if (FicZt_inventarios_SelectedItem != null)
            {
                FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmConteoDetInventarioList>(FicZt_inventarios_SelectedItem);
            }
        }

        private void AddCommandExecute()
        {
            var ficZt_inventarios = new zt_inventarios();
            //FicLoSrvNavigationInventario.FicMetNavigateTo<zt_inventarios>(ficZt_inventarios_TodoItems);
            FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmConteoInventarioItemInsert>(ficZt_inventarios);
        }
        private void EditCommandExecute()
        {
            if (FicZt_inventarios_SelectedItem != null)
            {
                FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmConteoInventarioItem>(FicZt_inventarios_SelectedItem);
            }
        }
        private void DetailsCommandExecute()
        {
            if (FicZt_inventarios_SelectedItem != null)
            {
                /*FicVmConteoInventarioDetails Details = new FicVmConteoInventarioDetails(null, null);
                Details.ActDelete = false;
                Details.ActDetails = true;*/
                FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmConteoInventarioDetails>(FicZt_inventarios_SelectedItem);
            }
        }
        private void DeleteCommandExecute()
        {
            if (FicZt_inventarios_SelectedItem != null)
            {
                FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmConteoInventarioDelete>(FicZt_inventarios_SelectedItem);
            }
        }
        private void DetCommandExecute()
        {
            if (FicZt_inventarios_SelectedItem != null)
            {
                FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmInventariosDetList>(FicMetZt_inventarios_SelectedItem);
            }
            //FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmInventariosDetList>();
        }
        /*private void SearchButtonPressedCommandExecute()
        {
            if (string.IsNullOrEmpty(FicSearchBar.Text))
            {
                FicMetZt_inventarios_Items = new ObservableCollection<zt_inventarios>(
                    FicOcZt_inventarios_Items.OrderBy(x => x.CEDI));
            }
            else
            {
                FicMetZt_inventarios_Items = new ObservableCollection<zt_inventarios>(FicOcZt_inventarios_Items
                    .Where(x => x.CEDI.ToLower().Contains(FicSearchBar.Text.ToLower()))
                    .OrderBy(x => x.CEDI));
            }
            FicLoSrvNavigationInventario.FicMetNavigateTo<FicVmConteoInventarioList>(FicMetZt_inventarios_Items);
        }*/
        /*private void SearchCommandExecute()
        {
            FicSearch();
        }
        private void RefreshCommandExecute()
        {
            FicSearch();
            FicIsRefreshing = false;
        }*/
    }
}
