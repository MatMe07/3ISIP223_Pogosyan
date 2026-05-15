using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class CatalogViewModel : BaseViewModel
    {
        public CatalogViewModel()
        {
            LoadBooks();
            SortStrings = new List<string> { "Все", "По названию", "По оценке" };
            FilterListStrings = dataBase.Genres.Select(a=>a.GenreName).ToList();
            FilterListStrings.Insert(0, "Все");
            StatusesReadings = dataBase.StatusesReading;
            StatusesReadingsString = new ObservableCollection<string>( StatusesReadings.Select(a=>a.Name));
            StatusesReadingsMItem = new ObservableCollection<MenuItem>();
            foreach (var it in StatusesReadingsString)
            {
                StatusesReadingsMItem.Add(new MenuItem() { Header = it, IsCheckable= true });
            }
        }
        public void LoadBooks()
        {
            _Allbooks = dataBase.Books;

            Books = new ObservableCollection<Book>(_Allbooks);
        }


        private ObservableCollection<Book> _Allbooks;
        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get
            {
                return _books;
            }
            set
            {
                _books = value;
                OnPropertyChanged(nameof(Books));
            }

        }

        private string _search = "";

        public string Search
        {
            get
            {
                return _search;
            }
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));
                FilterdSearch();
            }
        }


        public List<string> SortStrings { get; set; }

        private string _selectedSortItem = "Все";
        public string SelectedSortItem
        {
            get {  return _selectedSortItem; }
            set
            {
                _selectedSortItem = value;
                OnPropertyChanged(nameof(SelectedSortItem));
                FilterdSearch();
            }
        }

        public List<string> FilterListStrings { get; set; }


        //public ObservableCollection<>

        private string _selectedFiltItem = "Все";
        public string SelectedFiltItem
        {
            get { return _selectedFiltItem; }
            set
            {
                _selectedFiltItem = value;
                OnPropertyChanged(nameof(SelectedFiltItem));
                FilterdSearch();
            }
        }

        public void SetSelectBook(Book book)
        {
            dataBase.SelectedBook= book;
        }

        public void UpdBooks()
        {
            LoadBooks();
        }


        private ObservableCollection<StatusesReading> _statusesReadings;
        public ObservableCollection<StatusesReading> StatusesReadings
        {
            get
            {
                return _statusesReadings;
            }
            set
            {
                _statusesReadings = value;
                OnPropertyChanged(nameof(StatusesReadings));
            }
        }
        public ObservableCollection<string> StatusesReadingsString {  get; set; }
        public ObservableCollection<MenuItem> StatusesReadingsMItem {  get; set; }

        public bool CheckReadigItemToList(Book book, string item)
        {
            var FirsBook = dataBase.ReadingLists.FirstOrDefault(b=>b.Book == book);
            if (FirsBook == null) return false;
            var status = FirsBook.StatusesReading.Name;
            if (status != null)
            {
                return status == item;
            }
            return false;
        }

        public void RemoveBookFromReadingList(Book book)
        {
            dataBase.RemoveBookFromReadingList(book);

        }
        public void AddBookToReadingList(Book book, string status)
        {
            dataBase.AddBookToReadingList(book, status);
        }
        public void UpdateToReadingList(Book book, string status)
        {
            dataBase.UpdateBookStatus(book, status);
        }

        public void FilterdSearch()
        {
            List<Book> filt = null;
            
            filt = _Allbooks.Where(b => b.Title.ToLower().Contains(Search.ToLower())   || b.User.DisplayName.ToLower().Contains(Search.ToLower())).ToList();

            if (SelectedSortItem.ToString() != "Все")
            {
                if (SelectedSortItem.ToString() == "По названию")
                {
                    filt = filt.OrderBy(b=>b.Title).ToList();
                }
                else if (SelectedSortItem == "По оценке")
                {
                    filt = filt.OrderByDescending(b=>b.Rating).ToList();
                }
            }

            if (SelectedFiltItem.ToString() != "Все")
            {
                filt = filt.Where(b => b.BookGenres.Where( g=> g.Genre.GenreName == SelectedFiltItem.ToString()).Count() >0  ).ToList();
            }

            Books.Clear();
            foreach (Book b in filt)
            {
                Books.Add(b);
            }
        }
    }
}
