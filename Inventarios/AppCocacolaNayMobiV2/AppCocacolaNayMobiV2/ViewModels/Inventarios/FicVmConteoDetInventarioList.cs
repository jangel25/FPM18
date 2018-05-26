using System.Collections.ObjectModel;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Inventarios;
using AppCocacolaNayMobiV2.Models.Inventarios;
using AppCocacolaNayMobiV2.ViewModels.Base;
using System.Windows.Input;
using Syncfusion.Data.Extensions;
using System.Linq;
using System;
using System.Diagnostics;

namespace AppCocacolaNayMobiV2.ViewModels.Inventarios
{
    public class FicVmConteoDetInventarioList : FicViewModelBase
    {
        private ObservableCollection<zt_inventarios_conteos> _zt_inventario_conteos;
        private zt_inventarios_conteos _selectedZt_inventario_conteos;
        private zt_inventarios _zt_inventarios_det;

        private ICommand _addCommand;
        private ICommand _addRegresarAcumulado;
        private ICommand _addEditar;
        private ICommand _addDetalle;

        private IFicSrvNavigationConteoDetInventario _navigationService;
        private IFicSrvConteoDetInventario _sqliteService;

        public FicVmConteoDetInventarioList(
            IFicSrvNavigationConteoDetInventario navigationService,
            IFicSrvConteoDetInventario sqliteService)
        {
            _navigationService = navigationService;
            _sqliteService = sqliteService;
        }//Fin constructor

        public ObservableCollection<zt_inventarios_conteos> Zt_inventarios_conteos
        {
            get { return _zt_inventario_conteos; }
            set
            {
                _zt_inventario_conteos = value;
                RaisePropertyChanged();
            }
        }//Fin Zt_inventarios

        public zt_inventarios_conteos SelectedZt_inventario_conteos
        {
            get { return _selectedZt_inventario_conteos; }
            set
            {
                _selectedZt_inventario_conteos = value;
                //_navigationService.NavigateTo<Zt_InvConteosItemVM>(_selectedZt_inventario_conteos);
            }
        }//Fin selectedZt_inventario

        public zt_inventarios Zt_inventario_det
        {
            get { return _zt_inventarios_det; }
            set
            {
                _zt_inventarios_det = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddCommand
        {
            get { return _addCommand = _addCommand ?? new FicVmDelegateCommand(AddCommandExecute); }
        }//Fin AddCommand

        public ICommand AddRegresarAcumulado
        {
            get { return _addRegresarAcumulado = _addRegresarAcumulado ?? new FicVmDelegateCommand(AddRegresarAcumuladoExecute); }
        }//Fin AddRegresarAcumulado

        public ICommand AddEditar
        {
            get { return _addEditar = _addEditar ?? new FicVmDelegateCommand(AddEditarExecute); }
        }//Fin AddCommand

        public ICommand AddDetalle
        {
            get { return _addDetalle = _addDetalle ?? new FicVmDelegateCommand(AddDetalleExecute); }
        }//Fin AddCommand

        public void paraOnAppearing(object navigationContext)
        {
            var zt_inventario_det = navigationContext as zt_inventarios;
            if (zt_inventario_det != null)
            {
                Zt_inventario_det = zt_inventario_det;
            }
        }

        public override async void OnAppearing(object navigationContext)
        {
            paraOnAppearing(navigationContext);

            base.OnAppearing(navigationContext);

            var result = await _sqliteService.GetAll_zt_inventarios_conteos();

            Zt_inventarios_conteos = new ObservableCollection<zt_inventarios_conteos>();
            foreach (var zt_inventario_conteos in result)
            {
                Zt_inventarios_conteos.Add(zt_inventario_conteos);
            }
        }//Fin OnAppearing

        private void AddCommandExecute()
        {
            var zt_inventario_conteosItem = new zt_inventarios_conteos();
            if (Zt_inventario_det != null)
            {
                zt_inventario_conteosItem.IdInventario = Zt_inventario_det.IdInventario;
                zt_inventario_conteosItem.IdCEDI = Zt_inventario_det.IdCEDI;
                zt_inventario_conteosItem.IdConteo = Zt_inventarios_conteos.Count() + 1;
            }

            _navigationService.NavigateTo<FicVmConteoDetInventarioItem>(zt_inventario_conteosItem);
        }//Fin AddCommandExecute

        private void AddRegresarAcumuladoExecute() //Comando para regresar a acumulados 
        {                                          //Falta implementar la ventana de Barajas
                                                   //Solo es cambiar el nombre del ViewModel
            var zt_inventario_detList = new zt_inventarios_det();
            _navigationService.NavigateTo<FicVmConteoInventarioList>(zt_inventario_detList);
        }//Fin AddCommandExecute

        public void AddEditarExecute()
        {
            if (_selectedZt_inventario_conteos != null)
                _navigationService.NavigateTo<FicVmConteoDetInventarioItem>(_selectedZt_inventario_conteos);
        }//Fin AddEditarExecute

        public void AddDetalleExecute()
        {
            _navigationService.NavigateTo<FicVmConteoDetInventarioDetalle>(_selectedZt_inventario_conteos);
        }//Fin AddEditarExecute

        public bool seleccionoItem()
        {
            if (_selectedZt_inventario_conteos != null)
                return true;
            else
                return false;
        }

        #region FilterData
        private string filterText = "";
        private string selectedColumn = "All Columns";
        private string selectedCondition = "Equals";
        internal delegate void FilterChanged();
        internal FilterChanged filterTextChanged;

        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                OnFilterTextChanged();
                RaisePropertyChanged("FilterText");
            }
        }

        public string SelectedCondition
        {
            get { return selectedCondition; }
            set { selectedCondition = value; }
        }

        public string SelectedColumn
        {
            get { return selectedColumn; }
            set { selectedColumn = value; }
        }

        private void OnFilterTextChanged()
        {
            filterTextChanged();
        }

        private bool MakeStringFilter(zt_inventarios_conteos o, string option, string condition)
        {
            var value = o.GetType().GetProperty(option);
            var exactValue = value.GetValue(o, null);
            exactValue = exactValue.ToString().ToLower();
            string text = FilterText.ToLower();
            var methods = typeof(string).GetMethods();
            if (methods.Count() != 0)
            {
                if (condition == "Contains")
                {
                    var methodInfo = methods.FirstOrDefault(method => method.Name == condition);
                    bool result1 = (bool)methodInfo.Invoke(exactValue, new object[] { text });
                    return result1;
                }
                else if (exactValue.ToString() == text.ToString())
                {
                    bool result1 = String.Equals(exactValue.ToString(), text.ToString());
                    if (condition == "Equals")
                        return result1;
                    else if (condition == "NotEquals")
                        return false;
                }
                else if (condition == "NotEquals")
                {
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        private bool MakeNumericFilter(zt_inventarios_conteos o, string option, string condition)
        {
            var value = o.GetType().GetProperty(option);
            var exactValue = value.GetValue(o, null);
            double res;
            bool checkNumeric = double.TryParse(exactValue.ToString(), out res);
            if (checkNumeric)
            {
                switch (condition)
                {
                    case "Equals":
                        try
                        {
                            if (exactValue.ToString() == FilterText)
                            {
                                if (Convert.ToDouble(exactValue) == (Convert.ToDouble(FilterText)))
                                    return true;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case "NotEquals":
                        try
                        {
                            if (Convert.ToDouble(FilterText) != Convert.ToDouble(exactValue))
                                return true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            return true;
                        }
                        break;
                }
            }
            return false;
        }


        public bool FilerRecords(object o)
        {
            double res;
            bool checkNumeric = double.TryParse(FilterText, out res);
            var item = o as zt_inventarios_conteos;
            if (item != null && FilterText.Equals(""))
            {
                return true;
            }
            else
            {
                if (item != null)
                {
                    if (checkNumeric && !SelectedColumn.Equals("All Columns"))
                    {
                        bool result = MakeNumericFilter(item, SelectedColumn, SelectedCondition);
                        return result;
                    }
                    else if (SelectedColumn.Equals("All Columns"))
                    {
                        if (item.Id.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.SKU.ToLower().Contains(FilterText.ToLower()) ||
                            item.IdInventario.ToLower().Contains(FilterText.ToLower()) ||
                            item.IdAlmacen.ToLower().Contains(FilterText.ToLower()))
                            return true;
                        return false;
                    }
                    else
                    {
                        bool result = MakeStringFilter(item, SelectedColumn, SelectedCondition);
                        return result;
                    }
                }
            }
            return false;
        }
        #endregion
    }//Fin clase 
}
