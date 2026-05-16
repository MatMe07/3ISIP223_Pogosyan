using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace _3ISIP223_PogosyanWPF.ViewModels
{
    public class WorkDataBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static WorkDataBase _instance;
        public static WorkDataBase Instanse => _instance ?? (_instance = new WorkDataBase());

        public WorkDataBase()
        {
            bookGenres = Core.kingEntities.BookGenres.ToList();
            _AllReviews = new ObservableCollection<Review>( Core.kingEntities.Reviews);
            User = Core.kingEntities.Users.FirstOrDefault(u=>u.Role.RoleName == "Автор");
            ReadingLists = new ObservableCollection<ReadingList>(Core.kingEntities.ReadingLists.Where(b=>b.UserId == User.UserId).ToList());
            GetReasons = new ObservableCollection<Reason>( Core.kingEntities.Reasons);
            targetTypes = Core.kingEntities.TargetTypes.ToList();
            StatusesReading = new ObservableCollection<StatusesReading>( Core.kingEntities.StatusesReadings);
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        public bool IsAuthor => User.Role.RoleName == "Автор";
        public bool IsAdmin => User.Role.RoleName == "Администратор";

        public User User { get; set; }

        private ObservableCollection<ReadingList> _readingLists;
        public ObservableCollection<ReadingList> ReadingLists
        {
            get
            {
                return _readingLists;
            }
            set
            {
                _readingLists = value;
                OnPropertyChanged(nameof(ReadingLists));
            }
        }


        private Book _selectedBook;
        public Book SelectedBook
        {
            get { 
                return _selectedBook; 
            }
            set { 
                _selectedBook = value; 
                OnPropertyChanged(nameof(SelectedBook));
            }
        }


        public List<BookGenre> bookGenres {  get; set; }

        public List<BookGenre> GetBookGenres(int bookID) => bookGenres.Where(b=>b.BookId == bookID).ToList();


        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get
            {
                if (_books == null) _books = new ObservableCollection<Book>(Core.kingEntities.Books);
                return _books;
            }
            set
            {
                _books = value;
                OnPropertyChanged(nameof(Books));
            }

        }


        private ObservableCollection<Review> _AllReviews;


        public ObservableCollection<Review> GetReviewsToBook(int bookID)
        {
            return new ObservableCollection<Review>(_AllReviews.Where(r => r.BookId == bookID));
        }

        private List<Genre> _genres;
        public List<Genre> Genres
        {
            get {
                if (_genres == null)
                {
                    Genres = Core.kingEntities.Genres.ToList();
                }
                return _genres;
            
            }
            set
            {
                _genres = value;
                OnPropertyChanged(nameof(Genres));
            }
        }

        public List<TargetType> targetTypes;

        public ObservableCollection<Reason> GetReasons { get; set; }

        private ObservableCollection<StatusesReading> _statusesReading;
        public ObservableCollection<StatusesReading> StatusesReading
        {
            get
            {
                return _statusesReading;
            }
            set
            {
                _statusesReading = value;
                OnPropertyChanged(nameof(StatusesReading));
            }
        }

        public void AddReview(double rating, string Comment)
        {
            Review review = new Review()
            {
                Book = SelectedBook,
                User = User,
                Rating = (int)rating,
                Comment = Comment,
                CreatedAt = DateTime.Now,
                IsFrozen = false,
            };
            Core.kingEntities.Reviews.Add(review);
            _AllReviews.Add(review);
            Core.kingEntities.SaveChanges();

            Core.kingEntities.Entry(SelectedBook).Reload();

            //UpdBooks();
        }

        public void UpdBooks()
        {
            Books = new ObservableCollection<Book>( Core.kingEntities.Books);
        }


        public void SaveFreezeRequest(Object item, Reason reason)
        {
            Complaint complaint;
            if (item is Book book)
            {
                complaint = new Complaint()
                {
                    UserId = User.UserId,
                    Book = book,
                    Reason = reason,
                    CreatedAt = DateTime.Now,

                    TargetType = targetTypes.First(a => a.Name == "Book")
                };
            }
            else if(item is Review review)
            {
                complaint = new Complaint()
                {
                    UserId = User.UserId,
                    Review = review,
                    Reason = reason,
                    CreatedAt = DateTime.Now,

                    TargetType = targetTypes.First(a => a.Name == "Review")
                };
            }
            else 
            {
                User author = item as User;
                complaint = new Complaint()
                {
                    UserId = User.UserId,
                    AuthorId = author.UserId,
                    Reason = reason,
                    CreatedAt = DateTime.Now,
                    TargetType = targetTypes.First(a => a.Name == "Author")
                    
                };
            }
            Core.kingEntities.Complaints.Add(complaint);
            Core.kingEntities.SaveChanges();
        }

        public void RemoveBookFromReadingList(Book book)
        {
            var re = ReadingLists.FirstOrDefault(r => r.BookId == book.BookId);
            if (re != null)
            {
                Core.kingEntities.ReadingLists.Remove(re);
                ReadingLists.Remove(re);
                Core.kingEntities.SaveChanges();
            }
        }
        public void AddBookToReadingList(Book book, string status)
        {
            ReadingList readingList = new ReadingList()
            {
                User = User,
                Book= book,
                StatusesReading = StatusesReading.First(a=>a.Name == status),
                AddedAt = DateTime.Now,
            };
            Core.kingEntities.ReadingLists.Add(readingList);
            ReadingLists.Add(readingList);
            Core.kingEntities.SaveChanges();
        }
        public bool UpdateBookStatus(Book book, string newStatus)
        {
            var existingRecord = ReadingLists
                .FirstOrDefault(r => r.BookId == book.BookId && r.UserId == User.UserId);

            if (existingRecord != null)
            {
                var statusEntity = StatusesReading.FirstOrDefault(s => s.Name == newStatus);
                if (statusEntity != null)
                {
                    existingRecord.StatusesReading = statusEntity;
                    existingRecord.StatusId = statusEntity.StatusId;
                    existingRecord.AddedAt = DateTime.Now;

                    Core.kingEntities.SaveChanges();
                }
                return true;
            }
            else
            {
                ReadingList readingList = new ReadingList()
                {
                    User = User,
                    Book = book,
                    StatusesReading = StatusesReading.First(a => a.Name == newStatus),
                    AddedAt = DateTime.Now,
                };
                Core.kingEntities.ReadingLists.Add(readingList);
                ReadingLists.Add(readingList);
                Core.kingEntities.SaveChanges();
                return false;

            }
        }
    }
}
