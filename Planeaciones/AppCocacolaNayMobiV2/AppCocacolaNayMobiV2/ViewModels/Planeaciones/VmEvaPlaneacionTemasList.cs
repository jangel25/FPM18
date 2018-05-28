using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Planeaciones;
using AppCocacolaNayMobiV2.Models.Planeaciones;
using AppCocacolaNayMobiV2.ViewModels.Base;
using Syncfusion.Data.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace AppCocacolaNayMobiV2.ViewModels.Planeaciones
{
    public class VmEvaPlaneacionTemasList : FicViewModelBase
    {
        private ObservableCollection<Eva_planeacion_temas> _eva_planeacion_temas;
        private Eva_planeacion_temas _selected_eva_planeacion_temas;
        private Eva_planeacion _selected_eva_planeacion;

        private ICommand _addCommand;
        private ICommand _addSubtema;
        private ICommand _addEditar;
        private ICommand _addDetalle;

        private INavigationPlaneacion _navigationService;
        private ISrvPlaneacion _sqliteService;

        public VmEvaPlaneacionTemasList(
            INavigationPlaneacion navigationService,
            ISrvPlaneacion sqliteService)
        {
            _navigationService = navigationService;
            _sqliteService = sqliteService;
        }//Fin constructor

        public ObservableCollection<Eva_planeacion_temas> eva_planeacion_temas_list
        {
            get { return _eva_planeacion_temas; }
            set
            {
                _eva_planeacion_temas = value;
                RaisePropertyChanged();
            }
        }//Fin Zt_inventarios

        public Eva_planeacion_temas Selected_eva_planeacion_temas
        {
            get { return _selected_eva_planeacion_temas; }
            set
            {
                _selected_eva_planeacion_temas = value;
                //_navigationService.NavigateTo<Zt_InvConteosItemVM>(_selectedZt_inventario_conteos);
            }
        }//Fin selectedZt_inventario

        public Eva_planeacion Selected_eva_planeacion
        {
            get { return _selected_eva_planeacion; }
            set
            {
                _selected_eva_planeacion = value;
                //_navigationService.NavigateTo<Zt_InvConteosItemVM>(_selectedZt_inventario_conteos);
            }
        }//Fin selectedZt_inventario

        public ICommand AddCommand
        {
            get { return _addCommand = _addCommand ?? new FicVmDelegateCommand(AddCommandExecute); }
        }//Fin AddCommand

        public ICommand AddEditar
        {
            get { return _addEditar = _addEditar ?? new FicVmDelegateCommand(AddEditarExecute); }
        }//Fin AddCommand

        public ICommand AddDetalle
        {
            get { return _addDetalle = _addDetalle ?? new FicVmDelegateCommand(AddDetalleExecute); }
        }//Fin AddCommand

        public ICommand AddSubtemaCommand
        {
            get { return _addSubtema = _addSubtema ?? new FicVmDelegateCommand(AddSubtemasExecute); }
        }//Fin AddCommand

        public override async void OnAppearing(object navigationContext)
        {

            var eva_planeacion_Item = navigationContext as Eva_planeacion;

            if (eva_planeacion_Item != null)
            {
                Selected_eva_planeacion = eva_planeacion_Item;
            }

            base.OnAppearing(navigationContext);

            var result = await _sqliteService.GetAll_eva_planeacion_temas();

            eva_planeacion_temas_list = new ObservableCollection<Eva_planeacion_temas>();
            foreach (var zt_inventario_conteos in result)
            {
                if(Selected_eva_planeacion.IdPlaneacion == zt_inventario_conteos.IdPlaneacion)
                    eva_planeacion_temas_list.Add(zt_inventario_conteos);
            }
        }//Fin OnAppearing

        private void AddCommandExecute()
        {
            var eva_planeacionItem = new Eva_planeacion_temas();
            if (Selected_eva_planeacion != null)
                eva_planeacionItem.IdPlaneacion = Selected_eva_planeacion.IdPlaneacion;

            _navigationService.NavigateTo<VmEvaPlaneacionTemasItem>(eva_planeacionItem);
        }//Fin AddCommandExecute

        public void AddSubtemasExecute()
        {
            if (_selected_eva_planeacion_temas != null)
                _navigationService.NavigateTo<VmEvaPlanSubtemasList>(_selected_eva_planeacion_temas);
        }//Fin AddEditarExecute

        public void AddEditarExecute()
        {
            if (_selected_eva_planeacion_temas != null)
                _navigationService.NavigateTo<VmEvaPlaneacionTemasItem>(_selected_eva_planeacion_temas);
        }//Fin AddEditarExecute

        public void AddDetalleExecute()
        {
            _navigationService.NavigateTo<VmEvaPlaneacionTemasDetalle>(_selected_eva_planeacion_temas);
        }//Fin AddEditarExecute

        public bool seleccionoItem()
        {
            if (_selected_eva_planeacion_temas != null)
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

        private bool MakeStringFilter(Eva_planeacion_temas o, string option, string condition)
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

        private bool MakeNumericFilter(Eva_planeacion_temas o, string option, string condition)
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
            var item = o as Eva_planeacion_temas;
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
                        if (item.IdTema.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.DesTema.ToLower().Contains(FilterText.ToLower()) ||
                            item.Observaciones.ToLower().Contains(FilterText.ToLower()))
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
