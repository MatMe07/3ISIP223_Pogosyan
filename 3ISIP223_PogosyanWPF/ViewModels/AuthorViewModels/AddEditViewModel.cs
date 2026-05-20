using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.ViewModels.AuthorViewModels
{
    public class AddEditViewModel : BaseViewModel
    {
        public AddEditViewModel()
        {
            Genres = new ObservableCollection<Genre>(dataBase.Genres);
            CurBook = new Book()
            {
                //BookId = dataBase.GetLastIdBook + 1
            };
            BookGenresCur = new ObservableCollection<BookGenre>( CurBook.BookGenres);
        }
        private Book _cruBook;
        public Book CurBook
        {
            get
            {
                return _cruBook;
            }
            set
            {
                _cruBook = value;
                OnPropertyChanged(nameof(CurBook));
            }
        }
        public ObservableCollection<Genre> Genres { get; set; }

        //private ObservableCollection<Genre> 

        private Genre _selectGenre;
        public Genre SelectGenre
        {
            get
            {
                return _selectGenre;
            }
            set
            {
                if(value != null)
                {
                    _selectGenre = value;
                    OnPropertyChanged(nameof(SelectGenre));

                    AddBookGenre();
                }
            }
        }

        public ObservableCollection<BookGenre> BookGenresCur
        {
            get;
            set;
        }

        public void AddBookGenre()
        {
            BookGenresCur.Add(
                new BookGenre
                {
                    Book = CurBook,
                    Genre = _selectGenre
                }
            );

            OnPropertyChanged(nameof(CurBook));

        }
        public void AttachFile()
        {

        }
        public string BookTitle { get;  set; } = "";

    }
}
