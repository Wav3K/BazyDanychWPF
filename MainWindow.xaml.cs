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
            film.Category= _context.Categories.ToList().FirstOrDefault(category => category.Id == film.CategoryId);
        });
        
        data.ItemsSource = _context.Films.ToList();
        category.ItemsSource = _context.Categories.ToList().Select(c => c.Name).ToList();
    }
}