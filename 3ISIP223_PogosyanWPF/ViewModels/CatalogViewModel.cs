using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class CatalogViewModel : BaseViewModel
    {
        public CatalogViewModel()
        {
            _Allbooks = dataBase.Books;


            Books = new ObservableCollection<Book>( _Allbooks);
            SortStrings = new List<string> { "Все", "По названию", "По оценке" };
            FilterListStrings = dataBase.Genres.Select(a=>a.GenreName).ToList();
            FilterListStrings.Insert(0, "Все");
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
