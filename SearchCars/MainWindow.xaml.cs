using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace SearchCars
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string filePath = @"C:\Users\klaud\source\repos\CollectionsLinq\motfailures.csv"; // change the file if needed
        private List<Car> cars = new List<Car>(); // load data from csv file
        private List<Car> results = new List<Car>(); // store search results
        private string[] sortOptions = { "Year: ascending", "Year: descending", "Pass: ascending", "Pass: descending", "Fail: ascending", "Fail: descending" };
        private string make;
        private string model;
        private int year;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            sortBox.ItemsSource = sortOptions;
        }

        private void LoadData()
        {
            StreamReader reader = new StreamReader(File.OpenRead(filePath));
            String header = reader.ReadLine();

            while (!reader.EndOfStream) // read from the file
            {
                String line = reader.ReadLine();
                var values = line.Split(',');
                Car car = new Car();

                int year = 0;
                int pass = 0;
                int fail = 0;
                Int32.TryParse(values[2], out year);
                Int32.TryParse(values[3], out pass);
                Int32.TryParse(values[4], out fail);

                car.Make = values[0];
                car.Model = values[1];
                car.Year = year;
                car.Pass = pass;
                car.Fail = fail;

                cars.Add(car);
            }
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            // get user's input and try parsing numbers to int:
            make = makeBox.Text;
            model = modelBox.Text;
            year = 0;
            Int32.TryParse(yearBox.Text, out year);
            make=make.ToUpper();
            model=model.ToUpper();
            results.Clear();

            // depending on user's input, run the appropriate Linq commands and save the results:
            if (make!="" && model!= "" && year!=0)
            {
                var queryAll = from car in cars where ((car.Make == make) && (car.Model == model) && (car.Year == year)) select car;
                searchResultList.ItemsSource = queryAll;
                results = queryAll.ToList();
            }
            else if (make != "" && model != "")
            {
                var queryModelMake = from car in cars where ((car.Make == make) && (car.Model == model)) select car;
                searchResultList.ItemsSource = queryModelMake;
                results = queryModelMake.ToList();
            }
            else if (make != "" && year !=0)
            {
                var queryMakeYear = from car in cars where ((car.Make == make) && (car.Year == year )) select car;
                searchResultList.ItemsSource = queryMakeYear;
                results = queryMakeYear.ToList();
            }
            else if (model !="" && year !=0)
            {
                var queryModelYear = from car in cars where ((car.Model == model) && (car.Year==year)) select car;
                searchResultList.ItemsSource = queryModelYear;
                results = queryModelYear.ToList();
            }
            else 
            {
                var queryAny = from car in cars where ((car.Make == make) || (car.Model == model) || (car.Year == year)) select car;
                searchResultList.ItemsSource = queryAny;
                results = queryAny.ToList();
            }

            if (results.Count == 0)
                MessageBox.Show("No results to show!");
            resultsCount.Text = results.Count().ToString();
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            // clear all fields and results:
            searchResultList.ItemsSource = null;
            makeBox.Text = "";
            modelBox.Text = "";
            yearBox.Text = "";
            resultsCount.Text = "";
            sortBox.SelectedItem = null;
            results.Clear();
        }

        private void sortBtn_Click(object sender, RoutedEventArgs e)
        {
            int selection = sortBox.SelectedIndex; // get user's sorting choice from combobox

            if (selection<0) 
            {
                MessageBox.Show("No results to sort!");
            }
            else
            {
                switch (selection) // sort by:
                {
                    case 0: // year ascending
                        var queryYearAs = from car in results orderby car.Year ascending select car;
                        searchResultList.ItemsSource = queryYearAs;
                        results = queryYearAs.ToList();
                        break;
                    case 1: // year descending
                        var queryYearDe = from car in results orderby car.Year descending select car;
                        searchResultList.ItemsSource = queryYearDe;
                        results = queryYearDe.ToList();
                        break;
                    case 2: // pass ascending
                        var queryPassAs = from car in results orderby car.Pass ascending select car;
                        searchResultList.ItemsSource = queryPassAs;
                        results = queryPassAs.ToList();
                        break;
                    case 3: // pass descending
                        var queryPassDe = from car in results orderby car.Pass descending select car;
                        searchResultList.ItemsSource = queryPassDe;
                        results = queryPassDe.ToList();
                        break;
                    case 4: // fail ascending
                        var queryFailAs = from car in results orderby car.Fail ascending select car;
                        searchResultList.ItemsSource = queryFailAs;
                        results = queryFailAs.ToList();
                        break;
                    case 5: // fail descending
                        var queryFailDe = from car in results orderby car.Fail descending select car;
                        searchResultList.ItemsSource = queryFailDe;
                        results = queryFailDe.ToList();
                        break;
                    default:
                        break;
                }
            }
        }

        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "cvs files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog.FileName))
                {
                    string header = "Make, model, year, total number of tests:";
                    file.WriteLine(header);
                    foreach (Car c in results)
                    {
                        file.WriteLine(c.toCSV());
                    }
                }



            }
        }
    }
}
