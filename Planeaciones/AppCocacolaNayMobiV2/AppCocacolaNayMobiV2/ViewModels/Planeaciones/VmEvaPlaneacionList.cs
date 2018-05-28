using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppCocacolaNayMobiV2.Models.Planeaciones;
using AppCocacolaNayMobiV2.ViewModels.Base;
using AppCocacolaNayMobiV2.Services.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Navigation;
using AppCocacolaNayMobiV2.Interfaces.Planeaciones;
using Syncfusion.Data.Extensions;
using System.Linq;

namespace AppCocacolaNayMobiV2.ViewModels.Planeaciones
{
    public class VmEvaPlaneacionList : FicViewModelBase
    {
        private ObservableCollection<Eva_planeacion> _eva_planeacion;
        private Eva_planeacion _selected_eva_planeacion;
        //private zt_inventarios _zt_inventarios_det;

        private ICommand _addCommand;
        private ICommand _addTemas;
        private ICommand _addEditar;
        private ICommand _addDetalle;
        private ICommand _addFuentes;
        private ICommand _addApoyos;

        private INavigationPlaneacion _navigationService;
        private ISrvPlaneacion _sqliteService;

        public VmEvaPlaneacionList(
            INavigationPlaneacion navigationService,
            ISrvPlaneacion sqliteService)
        {
            _navigationService = navigationService;
            _sqliteService = sqliteService;
        }//Fin constructor

        public ObservableCollection<Eva_planeacion> eva_planeacion_list
        {
            get { return _eva_planeacion; }
            set
            {
                _eva_planeacion = value;
                RaisePropertyChanged();
            }
        }//Fin Zt_inventarios

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

        public ICommand AddTemasCommand
        {
            get { return _addTemas = _addTemas ?? new FicVmDelegateCommand(AddTemasExecute); }
        }//Fin AddCommand

        public ICommand AddFuentesCommand
        {
            get { return _addFuentes = _addFuentes ?? new FicVmDelegateCommand(AddFuentesExecute);}
        }//Fin AddCommand

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            var result = await _sqliteService.GetAll_eva_planeacion();

            eva_planeacion_list = new ObservableCollection<Eva_planeacion>();
            foreach (var zt_inventario_conteos in result)
            {
                eva_planeacion_list.Add(zt_inventario_conteos);
            }
        }//Fin OnAppearing

        private void AddCommandExecute()
        {
            var eva_planeacionItem = new Eva_planeacion();
            _navigationService.NavigateTo<VmEvaPlaneacionItem>(eva_planeacionItem);
        }//Fin AddCommandExecute

        public void AddFuentesExecute()
        {
            if (_selected_eva_planeacion != null)
                _navigationService.NavigateTo<VmEvaCatFuentesList>(_selected_eva_planeacion);
        }//Fin AddEditarExecute

        public void AddApoyosExecute()
        {
            if (_selected_eva_planeacion != null)
                _navigationService.NavigateTo<VmEvaCatApoyosList>(_selected_eva_planeacion);
        }//Fin AddEditarExecute

        public void AddEditarExecute()
        {
            if (_selected_eva_planeacion != null)
                _navigationService.NavigateTo<VmEvaPlaneacionItem>(_selected_eva_planeacion);
        }//Fin AddEditarExecute

        public void AddDetalleExecute()
        {
            _navigationService.NavigateTo<VmEvaPlaneacionDetalle>(_selected_eva_planeacion);
        }//Fin AddEditarExecute

        public void AddTemasExecute()
        {
            if (_selected_eva_planeacion != null)
                _navigationService.NavigateTo<VmEvaPlaneacionTemasList>(_selected_eva_planeacion);
        }//Fin AddEditarExecute

        public bool seleccionoItem()
        {
            if (_selected_eva_planeacion != null)
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

        private bool MakeStringFilter(Eva_planeacion o, string option, string condition)
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

        private bool MakeNumericFilter(Eva_planeacion o, string option, string condition)
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
            var item = o as Eva_planeacion;
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
                        if (item.IdPlaneacion.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.ReferenciaNorma.ToLower().Contains(FilterText.ToLower()) ||
                            item.Revision.ToLower().Contains(FilterText.ToLower()))
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
