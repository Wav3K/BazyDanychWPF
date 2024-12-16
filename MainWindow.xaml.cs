using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace BazyDanych4g1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private dbContext _context = new dbContext();

    public MainWindow()
    {
        InitializeComponent();

        _context.Films.ToList().ForEach(film =>
        {
            film.Category = _context.Categories.ToList().FirstOrDefault(category => category.Id == film.CategoryId);
        });

        //ze stringami
        data.ItemsSource = _context.Films.ToList();
        category.ItemsSource = _context.Categories.ToList().Select(c => c.Name).ToList();
        categoryEdit.ItemsSource = _context.Categories.ToList().Select(c => c.Name).ToList();

        //strignow brak
        List<Category> categories = _context.Categories.ToList();
        categories.Insert(0, new Category {Id = 0, Name = "All" });
        categoryFilter.ItemsSource = categories;
        categoryFilter.SelectedIndex = 0;
    }

    private void AddFilm_OnClick(object sender, RoutedEventArgs e)
    {
        var film = new Film
        {
            Title = filmName.Text,
            Year = int.Parse(yearFilm.Text),
            CategoryId = _context.Categories.ToList().FirstOrDefault(c => c.Name == category.Text).Id
        };
        _context.Films.Add(film);
        _context.SaveChanges();
        data.ItemsSource = _context.Films.ToList();
    }

    private void CategoryFilter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var cat = (Category) categoryFilter.SelectedItem;
        if (cat != null)
        {
            if (cat.Id == 0)
            {
                data.ItemsSource = _context.Films.ToList();
            }
            else
            {
                data.ItemsSource = _context.Films.ToList()
                    .Where(film => film.CategoryId == cat.Id)
                    .ToList();
            }
        }
    }

    private void DeleteBtn_OnClick(object sender, RoutedEventArgs e)
    {
        List<Film> films = data.SelectedItems.Cast<Film>().ToList();
        foreach (var film in films)
        {
            _context.Films.Remove(film);
        }
        _context.SaveChanges();
        data.ItemsSource = _context.Films.ToList();
    }

    private void ConfirmEditFilm_OnClick(object sender, RoutedEventArgs e)
    {
        
    }

    private void EditBtn_OnClick(object sender, RoutedEventArgs e)
    {
        WrapPanelVisible.Visibility = Visibility.Visible;
        var film = (Film) data.SelectedItem;
        filmNameEdit.Text = film.Title;
        yearFilmEdit.Text = film.Year.ToString();
        categoryEdit.SelectedItem = film.Category;
    }
}