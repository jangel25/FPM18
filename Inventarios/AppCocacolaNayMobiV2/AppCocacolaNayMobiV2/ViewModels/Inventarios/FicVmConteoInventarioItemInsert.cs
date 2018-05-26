using System.Windows.Input;
using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System.Collections.ObjectModel;
using System;

namespace AppCocacolaNayMobiV2.ViewModels.Inventarios
{
    public class FicVmConteoInventarioItemInsert : FicViewModelBase
    {
        //FIC: se declara el metodo para la clase modelo
        private zt_inventarios Fic_Zt_inventarios_Item;
        private ObservableCollection<zt_cat_cedis> Fic_Items_CEDIS;
        public zt_cat_cedis Fic_Item_CEDI;

        public bool IsEnabledFechaReg
        {
            get { return false; }
        }
        public bool IsEnabledHora
        {
            get { return false; }
        }

        public bool IsEnabledUsuario
        {
            get { return true; }
        }

        //FIC: se declara las variables para los e botones del frontend
        private ICommand FicSaveCommand;
        //private ICommand FicDeleteCommand;
        private ICommand FicCancelCommand;

        private IFicSrvNavigationInventario FicLoSrvNavigationInventario;
        //FIC: Se declara metodo de interfaz donde se encuentran
        //todos los metodos definidos para la clase de zt_inventarios
        private IFicSrvConteoInventario FicLoSrvConteoInventario;

        //FIC: Se declara el constructor
        public FicVmConteoInventarioItemInsert(
            IFicSrvNavigationInventario FicPaSrvNavigationInventario,
            IFicSrvConteoInventario FicPaSrvConteoInventario)
        {
            //FIC: se asigna el objeto que se recibe como parametro de tipo navigation services
            FicLoSrvNavigationInventario = FicPaSrvNavigationInventario;
            //FIC: se asigna el objeto que se recibe como parametro de tipo SqlServices 
            FicLoSrvConteoInventario = FicPaSrvConteoInventario;
        }

        //FIC: se desarrolla el metodo declarado de la clase modelo
        public zt_inventarios Item
        {
            get { return Fic_Zt_inventarios_Item; }
            set
            {
                Fic_Zt_inventarios_Item = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<zt_cat_cedis> FicMetItemsCEDIS
        {
            get { return Fic_Items_CEDIS; }
            set
            {
                Fic_Items_CEDIS = value;
                RaisePropertyChanged();
            }
        }


        public zt_cat_cedis SelectedCEDI
        {
            get
            {
                return Fic_Item_CEDI;
            }
            set
            {
                Fic_Item_CEDI = value;
                RaisePropertyChanged();
            }
        }


        public ICommand FicMetSaveCommand
        {
            get { return FicSaveCommand = FicSaveCommand ?? new FicVmDelegateCommand(SaveCommandExecute); }
        }

        /*public ICommand FicMetDeleteCommand
        {
            get { return FicDeleteCommand = FicDeleteCommand ?? new FicVmDelegateCommand(DeleteCommandExecute); }
        }*/

        public ICommand FicMetCancelCommand
        {
            get { return FicCancelCommand = FicCancelCommand ?? new FicVmDelegateCommand(CancelCommandExecute); }
        }

        public async override void OnAppearing(object FicPaNavigationContext)
        {
            var FicLoZt_inventarios = FicPaNavigationContext as zt_inventarios;

            if (FicLoZt_inventarios != null)
            {
                Item = FicLoZt_inventarios;
            }

            var result = await FicLoSrvConteoInventario.FicMetGetListCEDIS();

            FicMetItemsCEDIS = new ObservableCollection<zt_cat_cedis>();
            foreach (var ficPaItem in result)
            {
                FicMetItemsCEDIS.Add(ficPaItem);
            }

            var resultCedi = await FicLoSrvConteoInventario.FicMetGetCEDIS(FicLoZt_inventarios);

            SelectedCEDI = new zt_cat_cedis();

            if (resultCedi != null)
            {
                SelectedCEDI = resultCedi;
            }

            base.OnAppearing(FicPaNavigationContext);
        }


        private async void SaveCommandExecute()
        {
            //Agregar Hora y Fecha Actual
            var fecha_hora = DateTime.Now;
            String fechaActual = fecha_hora.Year + "-" + fecha_hora.Month + "-" + fecha_hora.Day;
            String horaActual = fecha_hora.Hour + ":" + fecha_hora.Minute + ":" + fecha_hora.Second;
            Item.Hora = horaActual;
            Item.FechaReg = fechaActual;
            Item.UsuarioReg = "EB1";
            //Agregar Hora y Fecha Actual

            Item.IdCEDI = SelectedCEDI.CEDI;
            await FicLoSrvConteoInventario.FicMetInsertNewInventario(Item);
            FicLoSrvNavigationInventario.FicMetNavigateBack();
        }

        /*private async void DeleteCommandExecute()
        {
            await FicLoSrvConteoInventario.FicMetRemoveInventario(Item);
            FicLoSrvNavigationInventario.FicMetNavigateBack();
        }*/

        private void CancelCommandExecute()
        {
            FicLoSrvNavigationInventario.FicMetNavigateBack();
        }
    }
}
