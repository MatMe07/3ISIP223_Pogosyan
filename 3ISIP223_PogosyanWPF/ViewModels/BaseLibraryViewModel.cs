using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class BaseLibraryViewModel:BaseViewModel
    {
        protected BaseLibraryViewModel()
        {
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
        }

        public List<string> SortStrings { get; set; }
        public List<string> FilterListStrings { get; set; }
        public ObservableCollection<StatusesReading> StatusesReadings { get; set; }
        public ObservableCollection<string> StatusesReadingsString { get; set; }
        public ObservableCollection<MenuItem> StatusesReadingsMItem { get; set; }
        public SnackbarMessageQueue MyMessageQueue { get; set; }

        protected string _search = "";
        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));
                FilterdSearch();
            }
        }

        protected string _selectedSortItem = "Все";
        public string SelectedSortItem
        {
            get => _selectedSortItem;
            set
            {
                _selectedSortItem = value;
                OnPropertyChanged(nameof(SelectedSortItem));
                FilterdSearch();
            }
        }

        protected string _selectedFiltItem = "Все";
        public string SelectedFiltItem
        {
            get => _selectedFiltItem;
            set
            {
                _selectedFiltItem = value;
                OnPropertyChanged(nameof(SelectedFiltItem));
                FilterdSearch();
            }
        }

        public virtual void FilterdSearch() { }

        public bool CheckReadigItemToList(Book book, string item)
        {
            var firstBook = dataBase.ReadingLists.FirstOrDefault(b => b.Book == book);
            if (firstBook == null) return false;
            return firstBook.StatusesReading.Name == item;
        }

        public void RemoveBookFromReadingList(Book book)
        {
            dataBase.RemoveBookFromReadingList(book);
            ActionsClass.SnackBarEnqueue($"\"{book.Title}\" удалена из всех списков", "#F25022", PackIconKind.CancelBold, MyMessageQueue, true);
            FilterdSearch();
        }

        public void UpdateToReadingList(Book book, string status)
        {
            var updOrAdd = dataBase.UpdateBookStatus(book, status);
            string txt = updOrAdd ? $"\"{book.Title}\" перемещена в \"{status}\"" : $"\"{book.Title}\" добавлена в \"{status}\"";
            ActionsClass.SnackBarEnqueue(txt, "#42A757", updOrAdd ? PackIconKind.SwapHorizontalBold : PackIconKind.CheckBold, MyMessageQueue, true);
            FilterdSearch();
        }

        public void SetSelectBook(Book book)
        {
            dataBase.SelectedBook = book;
        }

    }
}
