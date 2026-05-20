using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

namespace _3ISIP223_PogosyanWPF.ViewModels.AuthorViewModels
{
    public class AddEditViewModel : BaseViewModel
    {
        //public BitmapImage bi = null;
        //private string _tempImagePath;
        private Book origBook;
        public AddEditViewModel()
        {
            Genres = new ObservableCollection<Genre>(dataBase.Genres);
            CurBook = new Book()
            {
                //BookId = dataBase.GetLastIdBook + 1
            };
            BookGenresCur = new ObservableCollection<BookGenre>( CurBook.BookGenres);
            MyMessageQueue = new SnackbarMessageQueue();
        }
        private Book CloneBook(Book source)
        {
            return new Book
            {
                BookId = source.BookId,
                Title = source.Title,
                Description = source.Description,
                Content = source.Content,
                Rating = source.Rating,
                CoverPath = source.CoverPath,
                BookGenres = source.BookGenres?.ToList()
            };
        }

        public void LoadData(bool IsEdit, int? bookId = null)
        {
            if (IsEdit && bookId.HasValue)
            {
                var bookFromDb = dataBase.Books.FirstOrDefault(b => b.BookId == bookId.Value);

                if (bookFromDb != null)
                {
                    origBook = CloneBook(bookFromDb);

                    CurBook.BookId = bookFromDb.BookId;
                    CurBook.Title = bookFromDb.Title;
                    CurBook.Description = bookFromDb.Description;
                    CurBook.Content = bookFromDb.Content;
                    CurBook.Rating = bookFromDb.Rating;
                    CurBook.CoverPath = bookFromDb.CoverPath;

                    BookGenresCur.Clear();
                    foreach (var bookGenre in bookFromDb.BookGenres)
                    {
                        BookGenresCur.Add(new BookGenre
                        {
                            Book = CurBook,
                            Genre = bookGenre.Genre,
                            GenreId = bookGenre.GenreId
                        });
                    }

                    FullPath = CurBook.CoverFullPath;
                    PathIm = CurBook.CoverPath;

                    if (!string.IsNullOrEmpty(CurBook.Content))
                    {
                        FileName = "Файл загружен";
                    }

                    OnPropertyChanged(nameof(CurBook));
                    OnPropertyChanged(nameof(BookGenresCur));
                }
            }
            else
            {
                origBook = null;
                CurBook = new Book();
                BookGenresCur.Clear();
            }
        }

        public bool HasChanges()
        {
            if (origBook != null)
            {
                if (origBook.Title != CurBook.Title) return true;
                if (origBook.Description != CurBook.Description) return true;
                if (origBook.Content != CurBook.Content) return true;
                if (origBook.CoverPath != CurBook.CoverPath) return true;
                var originalGenres = origBook.BookGenres?.Select(g => g.GenreId).OrderBy(id => id).ToList();
                var currentGenres = BookGenresCur?.Select(g => g.GenreId).OrderBy(id => id).ToList();

                if (!originalGenres.SequenceEqual(currentGenres)) return true;
            }
            return false;
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
        
        private string _pathIm;
        public string PathIm
        {
            get
            {
                return string.IsNullOrEmpty(_pathIm) ? "/Images/Covers/empty_cover_booknet.jpg" : _pathIm;
            }
            set
            {
                CurBook.CoverPath = value;
            }
        }
        private string _fullPath;
        public string FullPath
        {
            get
            {
                return string.IsNullOrEmpty(_fullPath) ? "/Images/Covers/empty_cover_booknet.jpg" : _fullPath;
            }
            set
            {
                _fullPath = value;
                OnPropertyChanged(nameof(FullPath));
            }
        }

        public ObservableCollection<BookGenre> BookGenresCur
        {
            get;
            set;
        }

        public void RemoveGenre(Genre genre)
        {
            BookGenresCur.Remove(BookGenresCur.First(g => g.Genre == genre));

        }
        public void AddBookGenre()
        {
            if (BookGenresCur.FirstOrDefault(a => a.Genre == _selectGenre) != null) return;
            BookGenresCur.Add(
                new BookGenre
                {
                    Book = CurBook,
                    Genre = _selectGenre
                }
            );

            OnPropertyChanged(nameof(CurBook));

        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        public void AttachCover()
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "Image documents (.jpg)|*.jpg";
            bool? res = dialog.ShowDialog();
            if (res == true)
            {
                //bi = new BitmapImage();
                //bi.BeginInit();
                //bi.UriSource = new Uri(dialog.FileName);
                //bi.CacheOption = BitmapCacheOption.OnLoad;
                //bi.EndInit();

                string currentDir = Environment.CurrentDirectory;

                string projectDirectory = currentDir;
                for (int i = 0; i < 2; i++)
                {
                    projectDirectory = Path.GetDirectoryName(projectDirectory);
                    if (projectDirectory == null) break;
                }

                string coversDirectory = Path.Combine(projectDirectory, "Images", "Covers");


                string imgPath = Path.GetFileName(dialog.FileName);

                string destinationPath = Path.Combine(coversDirectory, imgPath);
                if (!File.Exists(destinationPath))
                {
                    File.Copy(dialog.FileName, destinationPath, overwrite: true);
                }
                FullPath = destinationPath;

                PathIm = $@"\Images\Covers\{imgPath}";
            }
        }
        public void AttachFile()
        {
            var dialog = new OpenFileDialog();

            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? res = dialog.ShowDialog();
            if (res == true)
            {
                var ind = dialog.FileName.LastIndexOf('\\') + 1;
                FileName = dialog.FileName.Substring(ind);

                if (File.Exists(dialog.FileName))
                {
                    CurBook.Content = File.ReadAllText(dialog.FileName).Substring(0, 200)+"...";
                    OnPropertyChanged(nameof(CurBook));


                }

            }
        }
        public string BookTitle { get;  set; } = "";

        public SnackbarMessageQueue MyMessageQueue { get; set; }    
        public bool SaveBook()
        {
            if (string.IsNullOrWhiteSpace(CurBook.Title))
            {
                ActionsClass.SnackBarEnqueue(
                    text: "Введите название книги",
                    foregroundHEX: "#FFBE0404",
                    iconKind: PackIconKind.Error,
                    MyMessageQueue: MyMessageQueue
                );
                return false;
            }
            if (string.IsNullOrWhiteSpace(CurBook.Description))
            {
                ActionsClass.SnackBarEnqueue(
                    text: "Введите описание книги",
                    foregroundHEX: "#FFBE0404",
                    iconKind: PackIconKind.Error,
                    MyMessageQueue: MyMessageQueue
                );
                return false;
            }
            if (BookGenresCur == null || BookGenresCur.Count == 0)
            {
                ActionsClass.SnackBarEnqueue(
                    text: "Выберите хотя бы один жанр",
                    foregroundHEX: "#FFBE0404",
                    iconKind: PackIconKind.Error,
                    MyMessageQueue: MyMessageQueue
                );
                return false;
            }
            if (string.IsNullOrWhiteSpace(CurBook.Content))
            {
                ActionsClass.SnackBarEnqueue(
                    text: "Прикрепите файл с содержимым книги",
                    foregroundHEX: "#FFBE0404",
                    iconKind: PackIconKind.Error,
                    MyMessageQueue: MyMessageQueue
                );
                return false;
            }
            //dataBase.SaveNewBook(CurBook, BookGenresCur);
            if (CurBook.BookId == 0)
            {
                dataBase.SaveNewBook(CurBook, BookGenresCur);
                ActionsClass.SnackBarEnqueue(
                    text: "Книга успешно добавлена!",
                    foregroundHEX: "#FF04BE5A",
                    iconKind: PackIconKind.CheckCircle,
                    MyMessageQueue: MyMessageQueue
                );
            }
            else
            {
                dataBase.UpdateBook(CurBook, BookGenresCur);
                ActionsClass.SnackBarEnqueue(
                    text: "Книга успешно обновлена!",
                    foregroundHEX: "#FF04BE5A",
                    iconKind: PackIconKind.CheckCircle,
                    MyMessageQueue: MyMessageQueue
                );
            }

            return true;
        }
    }
}
