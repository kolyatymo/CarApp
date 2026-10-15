using Library;
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

namespace LibraryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    
    public partial class MainWindow : Window
    {
        LibraryMethods lb = new LibraryMethods();
        string Authorname = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        public async void GetAuthors(object sender, RoutedEventArgs e)
        {
           Authors.ItemsSource = await lb.GetAllAuthors();
        }
        
        public async void GetBooksByAuthor()
        {      
            ListBooks.ItemsSource = await lb.GetBooksByAuthors(Authorname);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int priceBook = int.Parse(price.Text);

            ListBooksByPrice.ItemsSource = await lb.SearchBook(priceBook);

        }

        private void Authors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Authors.SelectedItem != null)
            {
                Authorname = (Authors.SelectedItem as Authors).Name!;
                GetBooksByAuthor();
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string BookName = NameB.Text;
            if (BookName.Length >= 3)
            {
                ListBooksByBookName.ItemsSource = await lb.GetBooksByName(BookName);
            }
            else
            {
                MessageBox.Show(">= 3 symbols!!!");
            }
        }
    }
}