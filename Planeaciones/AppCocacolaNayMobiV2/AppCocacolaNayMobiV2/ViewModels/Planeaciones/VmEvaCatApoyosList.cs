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
    public class VmEvaCatApoyosList : FicViewModelBase
    {
        private ObservableCollection<Eva_cat_apoyos_didacticos> _eva_cat_apoyos;
        private Eva_cat_apoyos_didacticos _selected_eva_cat_apoyos;
        private Eva_planeacion _selected_eva_planeacion;

        private ICommand _addCommand;
        private ICommand _addEditar;
        private ICommand _addDetalle;

        private INavigationPlaneacion _navigationService;
        private ISrvPlaneacion _sqliteService;

        public VmEvaCatApoyosList(
            INavigationPlaneacion navigationService,
            ISrvPlaneacion sqliteService)
        {
            _navigationService = navigationService;
            _sqliteService = sqliteService;
        }//Fin constructor

        public ObservableCollection<Eva_cat_apoyos_didacticos> eva_cat_apoyos_list
        {
            get { return _eva_cat_apoyos; }
            set
            {
                _eva_cat_apoyos = value;
                RaisePropertyChanged();
            }
        }//Fin Zt_inventarios

        public Eva_cat_apoyos_didacticos Selected_eva_cat_apoyos
        {
            get { return _selected_eva_cat_apoyos; }
            set
            {
                _selected_eva_cat_apoyos = value;
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

        public override async void OnAppearing(object navigationContext)
        {
            var eva_planeacion_Item = navigationContext as Eva_planeacion;

            if (eva_planeacion_Item != null)
            {
                Selected_eva_planeacion = eva_planeacion_Item;
            }

            base.OnAppearing(navigationContext);

            var result = await _sqliteService.GetAll_eva_cat_apoyos_didacticos();

            eva_cat_apoyos_list = new ObservableCollection<Eva_cat_apoyos_didacticos>();
            foreach (var zt_inventario_conteos in result)
            {
                eva_cat_apoyos_list.Add(zt_inventario_conteos);
            }
        }//Fin OnAppearing

        private void AddCommandExecute()
        {
            var eva_cat_apoyosItem = new Eva_cat_apoyos_didacticos();
            eva_cat_apoyosItem.IdPlaneacion = Selected_eva_planeacion.IdPlaneacion;
            var fecha = DateTime.Now;
            eva_cat_apoyosItem.FechaReg = fecha.ToString("dd-MM-yyyy");

            _navigationService.NavigateTo<VmEvaCatApoyosItem>(eva_cat_apoyosItem);
        }//Fin AddCommandExecute

        public void AddEditarExecute()
        {
            if (_selected_eva_cat_apoyos != null)
                _navigationService.NavigateTo<VmEvaCatApoyosItem>(_selected_eva_cat_apoyos);
        }//Fin AddEditarExecute

        public void AddDetalleExecute()
        {
            _navigationService.NavigateTo<VmEvaCatApoyosDetalle>(_selected_eva_cat_apoyos);
        }//Fin AddEditarExecute

        public void AddTemasExecute()
        {
            if (_selected_eva_cat_apoyos != null)
                _navigationService.NavigateTo<VmEvaPlaneacionTemasList>(_selected_eva_cat_apoyos);
        }//Fin AddEditarExecute

        public bool seleccionoItem()
        {
            if (_selected_eva_cat_apoyos != null)
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

        private bool MakeStringFilter(Eva_cat_apoyos_didacticos o, string option, string condition)
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

        private bool MakeNumericFilter(Eva_cat_apoyos_didacticos o, string option, string condition)
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
            var item = o as Eva_cat_apoyos_didacticos;
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
                        if (item.IdApoyoDidactico.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.DesApoyoDidactico.ToLower().Contains(FilterText.ToLower()) ||
                            item.Activo.ToString().ToLower().Contains(FilterText.ToLower()))
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
