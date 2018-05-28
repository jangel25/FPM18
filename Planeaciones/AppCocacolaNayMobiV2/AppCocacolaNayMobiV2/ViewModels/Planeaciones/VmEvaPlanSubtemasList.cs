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
    public class VmEvaPlanSubtemasList : FicViewModelBase
    {
        private ObservableCollection<Eva_planeacion_subtemas> _eva_planeacion_subtemas;
        private Eva_planeacion_subtemas _selected_eva_planeacion_subtemas;
        private Eva_planeacion_temas _selected_eva_planeacion_temas;

        private ICommand _addCommand;
        private ICommand _addEditar;
        private ICommand _addDetalle;

        private INavigationPlaneacion _navigationService;
        private ISrvPlaneacion _sqliteService;

        public VmEvaPlanSubtemasList(
            INavigationPlaneacion navigationService,
            ISrvPlaneacion sqliteService)
        {
            _navigationService = navigationService;
            _sqliteService = sqliteService;
        }//Fin constructor

        public ObservableCollection<Eva_planeacion_subtemas> eva_planeacion_subtemas_list
        {
            get { return _eva_planeacion_subtemas; }
            set
            {
                _eva_planeacion_subtemas = value;
                RaisePropertyChanged();
            }
        }//Fin Zt_inventarios

        public Eva_planeacion_subtemas Selected_eva_planeacion_subtemas
        {
            get { return _selected_eva_planeacion_subtemas; }
            set
            {
                _selected_eva_planeacion_subtemas = value;
                //_navigationService.NavigateTo<Zt_InvConteosItemVM>(_selectedZt_inventario_conteos);
            }
        }//Fin selectedZt_inventario

        public Eva_planeacion_temas Selected_eva_planeacion_temas
        {
            get { return _selected_eva_planeacion_temas; }
            set
            {
                _selected_eva_planeacion_temas = value;
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

            var eva_planeacion_temasItem = navigationContext as Eva_planeacion_temas;

            if (eva_planeacion_temasItem != null)
            {
                Selected_eva_planeacion_temas = eva_planeacion_temasItem;
            }

            base.OnAppearing(navigationContext);

            var result = await _sqliteService.GetAll_eva_planeacion_subtemas();

            eva_planeacion_subtemas_list = new ObservableCollection<Eva_planeacion_subtemas>();
            foreach (var zt_inventario_conteos in result)
            {
                if(Selected_eva_planeacion_temas.IdTema == zt_inventario_conteos.IdTema)
                    eva_planeacion_subtemas_list.Add(zt_inventario_conteos);
            }
        }//Fin OnAppearing

        private void AddCommandExecute()
        {
            var eva_planeacionItem = new Eva_planeacion_subtemas();
            if (Selected_eva_planeacion_temas != null)
                eva_planeacionItem.IdTema = Selected_eva_planeacion_temas.IdTema;

            _navigationService.NavigateTo<VmEvaPlanSubtemasItem>(eva_planeacionItem);
        }//Fin AddCommandExecute

        public void AddEditarExecute()
        {
            if (_selected_eva_planeacion_subtemas != null)
                _navigationService.NavigateTo<VmEvaPlanSubtemasItem>(_selected_eva_planeacion_subtemas);
        }//Fin AddEditarExecute

        public void AddDetalleExecute()
        {
            _navigationService.NavigateTo<VmEvaPlanSubtemasDetalle>(_selected_eva_planeacion_subtemas);
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

        private bool MakeStringFilter(Eva_planeacion_subtemas o, string option, string condition)
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

        private bool MakeNumericFilter(Eva_planeacion_subtemas o, string option, string condition)
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
            var item = o as Eva_planeacion_subtemas;
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
                        if (item.IdSubtema.ToString().ToLower().Contains(FilterText.ToLower()) ||
                            item.DesSubtema.ToLower().Contains(FilterText.ToLower()))
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
