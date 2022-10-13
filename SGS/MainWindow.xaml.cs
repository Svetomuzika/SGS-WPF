using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SGS
{
    public partial class MainWindow : Window
    {
        public List<Shift> Employees = new List<Shift>()
        {
            new Shift("Не выбрано", "Не выбрано", "Не выбрано", 1),
            new Shift("Екатеринбург", "A", "Иванов Данил" , 1),
            new Shift("Екатеринбург", "A", "Данилов Илья", 1),
            new Shift("Екатеринбург", "B", "Петров Николай", 1),
            new Shift("Екатеринбург", "B", "Сергеев Александр", 1),
            new Shift("Екатеринбург", "C", "Калинин Сергей", 1),
            new Shift("Москва", "D", "Петров Петр", 1),
            new Shift("Москва", "E", "Иванова Мария", 1),
            new Shift("Москва", "E", "Орлов Никита", 1),
            new Shift("Москва", "F", "Данилов Данил", 1),
            new Shift("Санкт-Петербург", "G", "Мванченко Генадий", 1),
            new Shift("Санкт-Петербург", "G", "Иваненко Георгий", 1),
            new Shift("Санкт-Петербург", "H", "Ивнев Рустам", 1),
        };

        public MainWindow()
        {
            InitializeComponent();

            CitiesComboBox.ItemsSource = Employees.Select(x => x.City).Distinct().ToList();
            ShopsComboBox.ItemsSource = Employees.Select(x => x.Shop).Distinct().ToList();
            EmployeeComboBox.ItemsSource = Employees.Select(x => x.Employee).ToList();
            //BrigadeComboBox.ItemsSource = new List<int>() { 1, 2};
        }

        private void CitiesComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CitiesComboBox.SelectedItem != null)
                if (Employees.Select(x => x.City).Distinct().ToList().Contains(CitiesComboBox.SelectedItem.ToString()))
                {
                    if (CitiesComboBox.SelectedItem.ToString() == "Не выбрано")
                    {
                        ShopsComboBox.ItemsSource = Employees.Select(x => x.Shop).Distinct().ToList();
                        EmployeeComboBox.ItemsSource = Employees.Select(x => x.Employee).ToList();
                        return;
                    }

                    ShopsComboBox.ItemsSource = Employees.Where(x => x.City == CitiesComboBox.SelectedItem.ToString() || x.City == "Не выбрано").Select(a => a.Shop).Distinct().ToList();
                    EmployeeComboBox.ItemsSource = Employees.Where(x => x.City == CitiesComboBox.SelectedItem.ToString()).Select(a => a.Employee).Distinct().ToList();
                }
        }

        private void ShopsComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ShopsComboBox.SelectedItem != null)
                if (Employees.Select(x => x.Shop).Distinct().ToList().Contains(ShopsComboBox.SelectedItem.ToString()))
                {
                    if (ShopsComboBox.SelectedItem.ToString() == "Не выбрано")
                    {
                        CitiesComboBox.ItemsSource = Employees.Select(x => x.City).Distinct().ToList();
                        EmployeeComboBox.ItemsSource = Employees.Select(x => x.Employee).ToList();
                        return;
                    }

                    CitiesComboBox.ItemsSource = Employees.Where(x => x.Shop == ShopsComboBox.SelectedItem.ToString() || x.Shop == "Не выбрано").Select(a => a.City).Distinct().ToList();
                    EmployeeComboBox.ItemsSource = Employees.Where(x => x.Shop == ShopsComboBox.SelectedItem.ToString()).Select(a => a.Employee).ToList();
                }
        }

        private void ShiftEnter_Click(object sender, RoutedEventArgs e)
        {
            var appDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var relativePath = @"Shifts.json";
            var fullPath = Path.Combine(appDir, relativePath);

            if (EmployeeComboBox.SelectedItem != null && EmployeeComboBox.SelectedItem.ToString() != "Не выбрано")
            {
                var employeesArr = new List<Shift>();
                var newEmployee = Employees.Where(x => x.Employee == EmployeeComboBox.SelectedItem.ToString()).FirstOrDefault();

                if (File.ReadAllText(fullPath) != "")
                    employeesArr = JsonConvert.DeserializeObject<List<Shift>>(File.ReadAllText(fullPath));
                
                newEmployee.Brigade = Brigade2.IsChecked == true ? 2 : 1;
                employeesArr.Add(newEmployee);

                File.WriteAllText(fullPath, JsonConvert.SerializeObject(employeesArr, Formatting.Indented));
            }
        }
    } 
}
