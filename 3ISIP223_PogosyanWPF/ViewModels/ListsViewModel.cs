using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class ListsViewModel:BaseLibraryViewModel
    {
        public ListsViewModel() : base()
        {
            IconsTabItems = new Dictionary<string, PackIconKind>()
            {
                {"В планах", PackIconKind.Book },
                {"Заброшено", PackIconKind.BookCancel },
                {"Прочитано", PackIconKind.BookCheck },
                {"Читаю", PackIconKind.BookOpenPageVariant }
            };

            tabItems = new ObservableCollection<TabItemViewModel>();
            //SortStrings = new List<string> { "Все", "По названию", "По оценке" };
            //FilterListStrings = dataBase.Genres.Select(a => a.GenreName).ToList();
            //FilterListStrings.Insert(0, "Все");


            //StatusesReadings = dataBase.StatusesReading;
            //StatusesReadingsString = new ObservableCollection<string>(StatusesReadings.Select(a => a.Name));
            //StatusesReadingsMItem = new ObservableCollection<MenuItem>();
            //MyMessageQueue = new SnackbarMessageQueue();
            //foreach (var it in StatusesReadingsString)
            //{
            //    StatusesReadingsMItem.Add(new MenuItem() { Header = it, IsCheckable = true });
            //}

            LoadBooks();
            LoadTabItems();
            SelectTabItem = tabItems[0];

        }

        public void LoadBooks()
        {
            _Allbooks = new ObservableCollection<Book>(dataBase.Books.Join(
                    dataBase.GetReadingLists("Все книги")
                    .Select(r => r.Book),
                    b => b.BookId,
                    re => re.BookId,
                    (b, re) => b
                    )
                );

            Books = new ObservableCollection<Book>(_Allbooks);

        }

        public void UpdateBooks()
        {
            Books = new ObservableCollection<Book>(_Allbooks.Join(
                dataBase.GetReadingLists(SelectTabItem.Tag.ToString())
                .Select(r => r.Book),
                b => b.BookId,
                re => re.BookId,
                (b, re) => b
                )
            );
        }

        private Dictionary<string, PackIconKind> IconsTabItems;
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

        private TabItemViewModel _selectTabItem;
        public TabItemViewModel SelectTabItem
        {
            get {  return _selectTabItem; }
            set
            {
                if (_selectTabItem == value) return;
                _selectTabItem = value;
                OnPropertyChanged(nameof(SelectTabItem));
                //LoadBooks();
                UpdateBooks();
                FilterdSearch();
            }
        }



        public ObservableCollection<TabItemViewModel> tabItems { get; set; }

        public void LoadTabItems()
        {
            foreach (var it in StatusesReadingsString)
            {
                tabItems.Add(new TabItemViewModel() { HeaderText = it, Width = 90, IconKind= IconsTabItems[it].ToString(), Tag = it });
            }
            tabItems.Insert(0,new TabItemViewModel() { HeaderText = "Все книги", Width = 90, IconKind="Books", Tag = "Все книги" });
        }


        public void UpdBooks()
        {
            LoadBooks();
            FilterdSearch();
        }


        public override void FilterdSearch()
        {
            List<Book> filt = null;

            //filt = null;
            filt = _Allbooks.Where(b => b.Title.ToLower().Contains(Search.ToLower()) || b.User.DisplayName.ToLower().Contains(Search.ToLower())).ToList();

            if (SelectedSortItem.ToString() != "Все")
            {
                if (SelectedSortItem.ToString() == "По названию")
                {
                    filt = filt.OrderBy(b => b.Title).ToList();
                }
                else if (SelectedSortItem == "По оценке")
                {
                    filt = filt.OrderByDescending(b => b.Rating).ToList();
                }
            }

            if (SelectedFiltItem.ToString() != "Все")
            {
                filt = filt.Where(b => b.BookGenres.Where(g => g.Genre.GenreName == SelectedFiltItem.ToString()).Count() > 0).ToList();
            }

            Books = new ObservableCollection<Book>(filt.Join(
                dataBase.GetReadingLists(SelectTabItem.Tag.ToString())
                .Select(r => r.Book),
                b => b.BookId,
                re => re.BookId,
                (b, re) => b
                )
            );
        }

    }
}
