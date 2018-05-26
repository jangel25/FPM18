using System.Collections.ObjectModel;
using System.Windows.Input;
using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Base;

namespace AppCocacolaNayMobiV2.ViewModels.Inventarios
{
    public class FicVmAlmacenList : FicViewModelBase
    {
        private ObservableCollection<zt_cat_almacenes> FicOcZt_cat_almacen_Items;
        private zt_cat_almacenes FicZt_cat_almacen_SelectedItem;

        private ICommand ficAddCommand;
        private ICommand ficEditCommand;
        private ICommand ficDeleteCommand;
        private ICommand ficDetailCommand;

        private IFicSrvNavigationAlmacen FicLoSrvNavigationAlmacen;
        private IFicSrvCatAlmacen FicLoSrvCatAlmacenes;

        public FicVmAlmacenList(
            IFicSrvNavigationAlmacen FicPaSrvNavigationAlmacen,
            IFicSrvCatAlmacen ficPaSrvCatAlmacenes)
        {
            FicLoSrvNavigationAlmacen = FicPaSrvNavigationAlmacen;
            FicLoSrvCatAlmacenes = ficPaSrvCatAlmacenes;
        }

        public ObservableCollection<zt_cat_almacenes> FicMetZt_cat_almacenes_Items
        {
            get { return FicOcZt_cat_almacen_Items; }
            set
            {
                FicOcZt_cat_almacen_Items = value;
                RaisePropertyChanged();
            }
        }

        public zt_cat_almacenes FicMetZt_cat_almacenes_SelectedItem
        {
            get { return FicZt_cat_almacen_SelectedItem; }
            set
            {
                FicZt_cat_almacen_SelectedItem = value;
                //FicLoSrvNavigationAlmacen.FicMetNavigateTo<FicVmAlmacenItem>(FicZt_cat_almacen_SelectedItem);
            }
        }

        public ICommand ficMetAddCommand
        {
            get { return ficAddCommand = ficAddCommand ?? new FicVmDelegateCommand(AddCommandExecute); }
        }

        public ICommand ficMetEditCommand
        {
            get { return ficEditCommand = ficEditCommand ?? new FicVmDelegateCommand(EditCommandExecute); }
        }

        public ICommand ficMetDeleteCommand
        {
            get { return ficDeleteCommand = ficDeleteCommand ?? new FicVmDelegateCommand(DeleteCommandExecute); }
        }

        public ICommand ficMetDetailCommand
        {
            get { return ficDetailCommand = ficDetailCommand ?? new FicVmDelegateCommand(DetailCommandExecute); }
        }
        
        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            var result = await FicLoSrvCatAlmacenes.FicMetGetListCatAlmacenes();

            FicMetZt_cat_almacenes_Items = new ObservableCollection<zt_cat_almacenes>();
            foreach (var ficPaItem in result)
            {
                FicMetZt_cat_almacenes_Items.Add(ficPaItem);
            }
        }

        private void AddCommandExecute()
        {
            var ficZt_cat_almacenes = new zt_cat_almacenes();
            FicLoSrvNavigationAlmacen.FicMetNavigateTo<FicVmAlmacenItem>(ficZt_cat_almacenes);           
        }

        private void EditCommandExecute()
        {
            if (FicZt_cat_almacen_SelectedItem != null)
            {
                FicLoSrvNavigationAlmacen.FicMetNavigateTo<FicVmAlmacenEditar>(FicZt_cat_almacen_SelectedItem);
            }
            FicZt_cat_almacen_SelectedItem = null;
        }

        private void DeleteCommandExecute()
        {
            if (FicZt_cat_almacen_SelectedItem != null)
            {
                FicLoSrvNavigationAlmacen.FicMetNavigateTo<FicVmAlmacenEliminar>(FicZt_cat_almacen_SelectedItem);
            }
            FicZt_cat_almacen_SelectedItem = null;
        }

        private void DetailCommandExecute()
        {
            if (FicZt_cat_almacen_SelectedItem != null)
            {
                FicLoSrvNavigationAlmacen.FicMetNavigateTo<FicVmAlmacenDetalle>(FicZt_cat_almacen_SelectedItem);
            }
            FicZt_cat_almacen_SelectedItem = null;
        }

    }
}
