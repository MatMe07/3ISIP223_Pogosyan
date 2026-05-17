using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class CatalogViewModel : BaseLibraryViewModel
    {
        public CatalogViewModel() : base()
        {
            LoadBooks();
            //SortStrings = new List<string> { "Все", "По названию", "По оценке" };
            //FilterListStrings = dataBase.Genres.Select(a=>a.GenreName).ToList();
            //FilterListStrings.Insert(0, "Все");

            //StatusesReadings = dataBase.StatusesReading;
            //StatusesReadingsString = new ObservableCollection<string>( StatusesReadings.Select(a=>a.Name));
            //StatusesReadingsMItem = new ObservableCollection<MenuItem>();
            //MyMessageQueue = new SnackbarMessageQueue();
            //foreach (var it in StatusesReadingsString)
            //{
            //    StatusesReadingsMItem.Add(new MenuItem() { Header = it, IsCheckable= true });
            //}
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


        public void UpdBooks()
        {
            LoadBooks();
        }


        public override void FilterdSearch()
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
