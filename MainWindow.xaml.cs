using BusPlannerModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Marginean_Daniela_ProiectFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum ActionState
        {
            New,
            Edit,
            Delete,
            Nothing
        }
        ActionState action = ActionState.Nothing;
        BusPlannerEntitiesModel ctx = new BusPlannerEntitiesModel();
        CollectionViewSource plannerViewSource;
        CollectionViewSource busViewSource;
        CollectionViewSource driverViewSource;
        CollectionViewSource routeViewSource;
        CollectionViewSource shiftViewSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            busViewSource.Source = ctx.Buses.Local;
            ctx.Buses.Load();
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
            driverViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("driverViewSource")));
            driverViewSource.Source = ctx.Drivers.Local;
            ctx.Drivers.Load();
            // Load data by setting the CollectionViewSource.Source property:
            // driverViewSource.Source = [generic data source]
            routeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("routeViewSource")));
            routeViewSource.Source = ctx.Routes.Local;
            ctx.Routes.Load();
            // Load data by setting the CollectionViewSource.Source property:
            // routeViewSource.Source = [generic data source]
            shiftViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("shiftViewSource")));
            shiftViewSource.Source = ctx.Shifts.Local;
            ctx.Shifts.Load();// Load data by setting the CollectionViewSource.Source property:
            // shiftViewSource.Source = [generic data source]
            plannerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("plannerViewSource")));
            BindPlannerDataGrid();
            ctx.Planners.Load();// Load data by setting the CollectionViewSource.Source property:
            // plannerViewSource.Source = [generic data source]
            InitializePlannerComboboxes();
        }

        ////----------Buses
        private void NewBus_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            BusDetailsEnabled(true);
            busDataGrid.SelectedItem = null;
        }
        private void EditBus_Click(object sender, RoutedEventArgs e)
        {
            if (busDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a record to edit");
            }
            else
            {
                action = ActionState.Edit;
                BusDetailsEnabled(true);
                
            }  
        }
        private void DeleteBus_Click(object sender, RoutedEventArgs e)
        {
            if (busDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a record to delete");
            }
            else
            {
                try
                {
                    Bus bus = (Bus)busDataGrid.SelectedItem;
                    ctx.Buses.Remove(bus);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                busViewSource.View.Refresh();
            }
        }
        private void SaveBus_Click(object sender, RoutedEventArgs e)
        {
            Bus bus = null;
            if (action == ActionState.New)
            {
                
                try
                {
                    //instantiem Bus entity
                    bus = new Bus()
                    {
                        Capacity = Convert.ToInt32(capacityTextBox.Text.Trim()),
                        Type = typeTextBox.Text.Trim(),
                        EngineType = engineTypeTextBox.Text.Trim(),
                        LicensePlate = licensePlateTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Buses.Add(bus);
                    busViewSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if (action == ActionState.Edit) 
            {
                try
                {
                    bus = (Bus)busDataGrid.SelectedItem;
                    bus.Capacity = Convert.ToInt32(capacityTextBox.Text.Trim());
                    bus.Type = typeTextBox.Text.Trim();
                    bus.EngineType = engineTypeTextBox.Text.Trim();
                    bus.LicensePlate = licensePlateTextBox.Text.Trim();

                    //salvam modificarile
                    ctx.SaveChanges();
                    busViewSource.View.Refresh();
                    busViewSource.View.MoveCurrentTo(bus);
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // reset state
            action = ActionState.Nothing;
            // disable fields
            BusDetailsEnabled(false);
        }

        private void CancelBus_Click(object sender, RoutedEventArgs e)
        {
            // reset state
            action = ActionState.Nothing;
            // disable fields
            BusDetailsEnabled(false);
        }

        private void BusDetailsEnabled(bool enable)
        {
            capacityTextBox.IsEnabled = enable;
            typeTextBox.IsEnabled = enable;
            engineTypeTextBox.IsEnabled = enable;
            licensePlateTextBox.IsEnabled = enable;
        }


        ////----------Drivers
        private void NewDriver_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            DriverDetailsEnabled(true);
            driverDataGrid.SelectedItem = null;
        }
        private void EditDriver_Click(object sender, RoutedEventArgs e)
        {
            if (driverDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a record to edit");
            }
            else
            {
                action = ActionState.Edit;
                DriverDetailsEnabled(true);
            }
        }
        private void DeleteDriver_Click(object sender, RoutedEventArgs e)
        {
            if (driverDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a record to delete");
            }
            else
            {
                try
                {
                    Driver driver = (Driver)driverDataGrid.SelectedItem;
                    ctx.Drivers.Remove(driver);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                driverViewSource.View.Refresh();
            }
        }
        private void SaveDriver_Click(object sender, RoutedEventArgs e)
        {
            Driver driver = null;
            if (action == ActionState.New)
            {

                try
                {
                    //instantiem Driver entity
                    driver = new Driver()
                    {
                        FirstName = firstNameTextBox.Text.Trim(),
                        LastName_ = lastName_TextBox.Text.Trim(),
                        Address = addressTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Drivers.Add(driver);
                    driverViewSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    driver = (Driver)driverDataGrid.SelectedItem;
                    driver.FirstName = firstNameTextBox.Text.Trim();
                    driver.LastName_ = lastName_TextBox.Text.Trim();
                    driver.Address = addressTextBox.Text.Trim();

                    //salvam modificarile
                    ctx.SaveChanges();
                    driverViewSource.View.Refresh();
                    driverViewSource.View.MoveCurrentTo(driver);
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // reset state
            action = ActionState.Nothing;
            // disable fields
            DriverDetailsEnabled(false);
        }

        private void CancelDriver_Click(object sender, RoutedEventArgs e)
        {
            // reset state
            action = ActionState.Nothing;
            // disable fields
            DriverDetailsEnabled(false);
        }

        private void DriverDetailsEnabled(bool enable)
        {
            firstNameTextBox.IsEnabled = enable;
            lastName_TextBox.IsEnabled = enable;
            addressTextBox.IsEnabled = enable;
        }

        ////----------Routes
        private void NewRoute_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            RouteDetailsEnabled(true);
            routeDataGrid.SelectedItem = null;
        }
        private void EditRoute_Click(object sender, RoutedEventArgs e)
        {
            if (routeDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a record to edit");
            }
            else
            {
                action = ActionState.Edit;
                RouteDetailsEnabled(true);
            }
        }
        private void DeleteRoute_Click(object sender, RoutedEventArgs e)
        {
            if (routeDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a record to delete");
            }
            else
            {
                try
                {
                    Route route = (Route)routeDataGrid.SelectedItem;
                    ctx.Routes.Remove(route);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                routeViewSource.View.Refresh();
            }
        }
        private void SaveRoute_Click(object sender, RoutedEventArgs e)
        {
            Route route = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Route entity
                    route = new Route()
                    {
                        StartRoute = startRouteTextBox.Text.Trim(),
                        EndRoute = endRouteTextBox.Text.Trim(),
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Routes.Add(route);
                    routeViewSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    route = (Route)driverDataGrid.SelectedItem;
                    route.StartRoute = startRouteTextBox.Text.Trim();
                    route.EndRoute = endRouteTextBox.Text.Trim();

                    //salvam modificarile
                    ctx.SaveChanges();
                    routeViewSource.View.Refresh();
                    routeViewSource.View.MoveCurrentTo(route);
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // reset state
            action = ActionState.Nothing;
            // disable fields
            RouteDetailsEnabled(false);
        }

        private void CancelRoute_Click(object sender, RoutedEventArgs e)
        {
            // reset state
            action = ActionState.Nothing;
            // disable fields
            RouteDetailsEnabled(false);
        }

        private void RouteDetailsEnabled(bool enable)
        {
            startRouteTextBox.IsEnabled = enable;
            endRouteTextBox.IsEnabled = enable;
        }

        ////----------Shifts
        private void NewShift_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            ShiftDetailsEnabled(true);
            shiftDataGrid.SelectedItem = null;
        }
        private void EditShift_Click(object sender, RoutedEventArgs e)
        {
            if (shiftDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a record to edit");
            }
            else
            {
                action = ActionState.Edit;
                ShiftDetailsEnabled(true);
            }
        }
        private void DeleteShift_Click(object sender, RoutedEventArgs e)
        {
            if (shiftDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a record to delete");
            }
            else
            {
                try
                {
                    Shift shift = (Shift)shiftDataGrid.SelectedItem;
                    ctx.Shifts.Remove(shift);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                shiftViewSource.View.Refresh();
            }
        }
        private void SaveShift_Click(object sender, RoutedEventArgs e)
        {
            Shift shift = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Route entity
                    shift = new Shift()
                    {
                        ShiftName = shiftNameTextBox.Text.Trim(),
                        StartShift = TimeSpan.Parse(startShiftTextBox.Text.Trim()),
                        EndShift = TimeSpan.Parse(endShiftTextBox.Text.Trim())
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Shifts.Add(shift);
                    shiftViewSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    shift = (Shift)shiftDataGrid.SelectedItem;
                    shift.ShiftName = shiftNameTextBox.Text.Trim();
                    shift.StartShift = TimeSpan.Parse(startShiftTextBox.Text.Trim());
                    shift.EndShift = TimeSpan.Parse(endShiftTextBox.Text.Trim());

                    //salvam modificarile
                    ctx.SaveChanges();
                    shiftViewSource.View.Refresh();
                    shiftViewSource.View.MoveCurrentTo(shift);
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // reset state
            action = ActionState.Nothing;
            // disable fields
            ShiftDetailsEnabled(false);
        }

        private void CancelShift_Click(object sender, RoutedEventArgs e)
        {
            // reset state
            action = ActionState.Nothing;
            // disable fields
            ShiftDetailsEnabled(false);
        }

        private void ShiftDetailsEnabled(bool enable)
        {
            shiftNameTextBox.IsEnabled = enable;
            startShiftTextBox.IsEnabled = enable;
            endShiftTextBox.IsEnabled = enable;
        }
        ////----------Planner
        private void InitializePlannerComboboxes()
        {
            cmbBus.ItemsSource = ctx.Buses.Local;
            cmbBus.SelectedValuePath = "IdBus";

            cmbDriver.ItemsSource = ctx.Drivers.Local;
            cmbDriver.SelectedValuePath = "IdDriver";

            cmbShift.ItemsSource = ctx.Shifts.Local;
            cmbShift.DisplayMemberPath = "ShiftName";
            cmbShift.SelectedValuePath = "IdShift";

            cmbRoute.ItemsSource = ctx.Routes.Local;
            cmbRoute.SelectedValuePath = "IdRoute";

        }

        private void NewPlanner_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            PlannerDetailsEnabled(true);
            plannerDataGrid.SelectedItem = null;
        } 
        private void EditPlanner_Click(object sender, RoutedEventArgs e)
        {
            if (plannerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a record to edit");
            }
            else
            {
                action = ActionState.Edit;
                PlannerDetailsEnabled(true);
            }
        }
        private void DeletePlanner_Click(object sender, RoutedEventArgs e)
        {
            if (plannerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a record to delete");
            }
            else
            {
                try
                {
                    dynamic planner = plannerDataGrid.SelectedItem;
                    int curr_id = planner.IdPlanner;
                    var deletedPlanner = ctx.Planners.FirstOrDefault(s => s.IdPlanner == curr_id);
                    if (deletedPlanner != null)
                    {
                        ctx.Planners.Remove(deletedPlanner);
                        ctx.SaveChanges();
                        BindPlannerDataGrid();
                    }
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void CancelPlanner_Click(object sender, RoutedEventArgs e)
        {
            // reset state
            action = ActionState.Nothing;
            // disable fields
            PlannerDetailsEnabled(false);
        }
        private void SavePlanner_Click(object sender, RoutedEventArgs e)
        {
            if (action == ActionState.New)
            {
                try
                {
                    Bus bus = (Bus)cmbBus.SelectedItem;
                    Driver driver = (Driver)cmbDriver.SelectedItem;
                    Shift shift = (Shift)cmbShift.SelectedItem;
                    Route route = (Route)cmbRoute.SelectedItem;
                    DateTime date = (DateTime)dpDate.SelectedDate;

                    //instantiem Planner entity
                    Planner planner = new Planner()
                    {
                        IdBus = bus.IdBus,
                        IdDriver = driver.IdDriver,
                        IdShift = shift.IdShift,
                        IdRoute = route.IdRoute,
                        Date = date

                    };
                    //adaugam entitatea nou creata in context
                    ctx.Planners.Add(planner);
                    //salvam modificarile
                    ctx.SaveChanges();
                    BindPlannerDataGrid();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Edit)
            {
                try
                {
                    dynamic planner = plannerDataGrid.SelectedItem;
                    int curr_id = planner.IdPlanner;
                    var editedPlanner = ctx.Planners.FirstOrDefault(s => s.IdPlanner == curr_id);
                    if (editedPlanner != null)
                    {
                        Bus bus = (Bus)cmbBus.SelectedItem;
                        Driver driver = (Driver)cmbDriver.SelectedItem;
                        Shift shift = (Shift)cmbShift.SelectedItem;
                        Route route = (Route)cmbRoute.SelectedItem;
                        DateTime date = (DateTime)dpDate.SelectedDate;

                        editedPlanner.IdBus = bus.IdBus;
                        editedPlanner.IdDriver = driver.IdDriver;
                        editedPlanner.IdShift = shift.IdShift;
                        editedPlanner.IdRoute = route.IdRoute   ;
                        editedPlanner.Date = (DateTime)dpDate.SelectedDate;

                        ctx.SaveChanges();
                        BindPlannerDataGrid();
                    }
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // reset state
            action = ActionState.Nothing;
            // disable fields
            PlannerDetailsEnabled(false);
        }

       private void PlannerDetailsEnabled(bool enable)
        {
            cmbBus.IsEnabled = enable;
            cmbDriver.IsEnabled = enable;
            cmbShift.IsEnabled = enable;
            cmbRoute.IsEnabled = enable;
            dpDate.IsEnabled = enable;
        }

        private void BindPlannerDataGrid()
        {
            var query = from planner in ctx.Planners
                             join bus in ctx.Buses on planner.IdBus equals bus.IdBus
                             join driver in ctx.Drivers on planner.IdDriver equals driver.IdDriver
                             join shift in ctx.Shifts on planner.IdShift equals shift.IdShift
                             join route in ctx.Routes on planner.IdRoute equals route.IdRoute
                             select new
                             {
                                 planner.IdPlanner,
                                 planner.Date,
                                 planner.IdBus,
                                 planner.IdDriver,
                                 planner.IdShift,
                                 planner.IdRoute,
                                 bus.LicensePlate,
                                 bus.Type,
                                 driver.FirstName,
                                 driver.LastName_,
                                 shift.ShiftName,
                                 route.StartRoute,
                                 route.EndRoute
                             };
           plannerViewSource.Source = query.ToList();
        }
    }
}
