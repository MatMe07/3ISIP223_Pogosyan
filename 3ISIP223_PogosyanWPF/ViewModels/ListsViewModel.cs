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
    public class ListsViewModel:BaseViewModel
    {
        public ListsViewModel()
        {
            IconsTabItems = new Dictionary<string, PackIconKind>()
            {
                {"В планах", PackIconKind.Book },
                {"Заброшено", PackIconKind.BookCancel },
                {"Прочитано", PackIconKind.BookCheck },
                {"Читаю", PackIconKind.BookOpenPageVariant }
            };

            tabItems = new ObservableCollection<TabItem>();
            SortStrings = new List<string> { "Все", "По названию", "По оценке" };
            FilterListStrings = dataBase.Genres.Select(a => a.GenreName).ToList();
            FilterListStrings.Insert(0, "Все");


            StatusesReadings = dataBase.StatusesReading;
            StatusesReadingsString = new ObservableCollection<string>(StatusesReadings.Select(a => a.Name));
            StatusesReadingsMItem = new ObservableCollection<MenuItem>();
            MyMessageQueue = new SnackbarMessageQueue();
            foreach (var it in StatusesReadingsString)
            {
                StatusesReadingsMItem.Add(new MenuItem() { Header = it, IsCheckable = true });
            }

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

            //Books = new ObservableCollection<Book>( _Allbooks.Join(
            //        dataBase.GetReadingLists(SelectTabItem.Tag.ToString())
            //        .Select(r=>r.Book),
            //        b=>b.BookId,
            //        re=>re.BookId,
            //        (b, re) => b
            //        )
            //    );
            //UpdateBooks();
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
        //private ObservableCollection<Book> _AllReadingBooks;
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

        private TabItem _selectTabItem;
        public TabItem SelectTabItem
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

        public List<string> SortStrings { get; set; }

        private string _selectedSortItem = "Все";
        public string SelectedSortItem
        {
            get { return _selectedSortItem; }
            set
            {
                _selectedSortItem = value;
                OnPropertyChanged(nameof(SelectedSortItem));
                FilterdSearch();
            }
        }

        public List<string> FilterListStrings { get; set; }

        public ObservableCollection<StatusesReading> StatusesReadings { get; set; }

        public ObservableCollection<string> StatusesReadingsString { get; set; }
        public ObservableCollection<MenuItem> StatusesReadingsMItem { get; set; }

        public SnackbarMessageQueue MyMessageQueue { get; set; }

        public bool CheckReadigItemToList(Book book, string item)
        {
            var FirsBook = dataBase.ReadingLists.FirstOrDefault(b => b.Book == book);
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
            ActionsClass.RemoveBookFromReadingList(book, MyMessageQueue);
            FilterdSearch();

        }

        public void UpdateToReadingList(Book book, string status)
        {
            var updOrAdd = dataBase.UpdateBookStatus(book, status);

            ActionsClass.UpdateToReadingList(book, status, updOrAdd, MyMessageQueue);
            FilterdSearch();
        }


        public ObservableCollection<TabItem> tabItems { get; set; }

        public void LoadTabItems()
        {
            StackPanel stackPanel;
            foreach (var it in StatusesReadingsString)
            {
                stackPanel = new StackPanel
                {
                    Children =
                        {
                            new PackIcon
                            {
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Kind = IconsTabItems[it]
                            },
                            new TextBlock
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Text = it
                            }
                        }
                };
                tabItems.Add(new TabItem() { Header = stackPanel, Width = 90, Tag= it });
            }
            stackPanel = new StackPanel
            {
                Children =
                        {
                            new PackIcon
                            {
                                Width = 24,
                                Height = 24,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Kind = PackIconKind.Books
                            },
                            new TextBlock
                            {
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Text = "Все книги"
                            }
                        }
            };
            tabItems.Insert(0,new TabItem() { Header = stackPanel, Width = 90, Tag = "Все книги" });
        }

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
            dataBase.SelectedBook = book;
        }

        public void UpdBooks()
        {
            LoadBooks();
            FilterdSearch();
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


        public void FilterdSearch()
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

            Books.Clear();
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
