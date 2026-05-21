using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
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
            if (User != null)
                LoadData();
        }
        public void LoadData()
        {
            BookGenres = Core.kingEntities.BookGenres.ToList();
            _AllReviews = new ObservableCollection<Review>(Core.kingEntities.Reviews);
            //User = Core.kingEntities.Users.FirstOrDefault(u=>u.UserId == 1);
            //User = Core.kingEntities.Users.FirstOrDefault(u=>u.Role.RoleName == "Администратор");
            ReadingLists = new ObservableCollection<ReadingList>(Core.kingEntities.ReadingLists.Where(b => b.UserId == User.UserId));
            GetReasons = new ObservableCollection<Reason>(Core.kingEntities.Reasons);
            targetTypes = Core.kingEntities.TargetTypes.ToList();
            StatusesReading = new ObservableCollection<StatusesReading>(Core.kingEntities.StatusesReadings);
            statusesRequests = Core.kingEntities.StatusesRequests.ToList();

            Books = new ObservableCollection<Book>(Core.kingEntities.Books);
            unfreezeRequests = new ObservableCollection<UnfreezeRequest>(Core.kingEntities.UnfreezeRequests);
            Complaints = new ObservableCollection<Complaint>(Core.kingEntities.Complaints);
            AuthorRequests = new ObservableCollection<AuthorRequest>(Core.kingEntities.AuthorRequests);
            GetRoles = Core.kingEntities.Roles.ToList();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------

        public bool RegisterUser(string login, string displayName, string email, string password)
        {
            var newUser = new User
            {
                Login = login,
                DisplayName = displayName,
                Email = email,
                Password = password,
                RoleId = 1,
                IsFrozen = false,
                CreatedAt = DateTime.Now
            };

            Users.Add(newUser);
            Core.kingEntities.Users.Add(newUser);
            Core.kingEntities.SaveChanges();
            return true;
        }
        public void GetAuthorReq()
        {
            var authorReq = new AuthorRequest
            {
                User = User,
                StatusId = 1,
                CreatedAt = DateTime.Now,
            };
            AuthorRequests.Add(
                authorReq
                );
            Core.kingEntities.AuthorRequests.Add(authorReq);
            Core.kingEntities.SaveChanges();
        }
        public void UpdateUserProfile(string displayName, string email)
        {

            if (User != null)
            {
                User.DisplayName = displayName;
                User.Email = email;
                Core.kingEntities.SaveChanges();
            }
        }
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

        public bool CheckIsUnfreezeReq()
        {
            return unfreezeRequests.LastOrDefault(c=>c.UserId == User.UserId) != null;
        }

        public void SaveRole(User us, Role newRole)
        {
            var _originalUser = Users.FirstOrDefault(u => u.UserId == us.UserId);
            _originalUser.Role = newRole;
            Core.kingEntities.SaveChanges();

        }

        public void SaveNewBook(Book book, ObservableCollection<BookGenre> bookGenres)
        {
            book.CreatedAt = DateTime.Now;
            book.AuthorId = User.UserId;
            book.Rating = 0;
            Books.Add(book);
            Core.kingEntities.Books.Add(book);

            foreach (var item in bookGenres)
            {
                BookGenres.Add(item);
                Core.kingEntities.BookGenres.Add(item);

            }
            Core.kingEntities.SaveChanges();
        }

        public void UpdateBook(Book newBook, ObservableCollection<BookGenre> bookGenres)
        {
            var exBook = Books.FirstOrDefault(b => b.BookId == newBook.BookId);
            exBook.Title = newBook.Title;
            exBook.Description = newBook.Description;
            exBook.Content = newBook.Content;
            exBook.CoverPath = newBook.CoverPath;
            var existingGenres = BookGenres.Where(bg => bg.BookId == newBook.BookId).ToList();
            foreach (var item in existingGenres)
            {
                BookGenres.Remove(item);
                Core.kingEntities.BookGenres.Remove(item);
            }
            foreach (var bookGenre in bookGenres)
            {
                var bookG = new BookGenre
                {
                    BookId = newBook.BookId,
                    Genre = bookGenre.Genre
                };
                BookGenres.Add(bookG);
                Core.kingEntities.BookGenres.Add(bookG);

            }
            Core.kingEntities.SaveChanges();

        }

        public void SendRequest(string txt, string target, int? bookId = null)
        {
            var targReq = targetTypes.First(t=>t.Name == target);
            UnfreezeRequest unfreezeRequest;
            if (bookId == null)
            {
                unfreezeRequest = new UnfreezeRequest
                {
                    User = User,
                    TargetType = targReq,
                    StatusId = 1,
                    Reason= txt,
                    CreatedAt = DateTime.Now,
                };
            }
            else
            {
                unfreezeRequest = new UnfreezeRequest
                {
                    User = User,
                    TargetType = targReq,
                    StatusId = 1,
                    Reason = txt,
                    BookId = bookId,
                    CreatedAt = DateTime.Now,
                };
            }
            unfreezeRequests.Add(unfreezeRequest);
            Core.kingEntities.UnfreezeRequests.Add(unfreezeRequest);
            Core.kingEntities.SaveChanges();

        }

        public void SaveUserEdit(int _origId, User EditingUser)
        {
            var _originalUser = Users.FirstOrDefault(u=>u.UserId==_origId);
            _originalUser.DisplayName = EditingUser.DisplayName;
            _originalUser.Login = EditingUser.Login;
            _originalUser.Email = EditingUser.Email;
            _originalUser.Role = EditingUser.Role;
            _originalUser.Password = EditingUser.Password;
            _originalUser.IsFrozen = EditingUser.IsFrozen;
            _originalUser.Reason = EditingUser.Reason;
            Core.kingEntities.SaveChanges();
        }

        public void UnfreezeUser(int usId)
        {
            var _originalUser = Users.FirstOrDefault(u => u.UserId == usId);
            _originalUser.IsFrozen = false;
            _originalUser.Reason = null;
            Core.kingEntities.SaveChanges();

        }

        public List<Role> GetRoles {  get; set; }

        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get { 
                if (_users == null) _users = new ObservableCollection<User>( Core.kingEntities.Users);
                return _users; 
            }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }


        public ObservableCollection<ReadingList> GetReadingLists(string status)
        {
            if (status == "Все книги") return _readingLists;
            return new ObservableCollection<ReadingList>( _readingLists.Where(r=>r.StatusesReading.Name == status));
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


        public List<BookGenre> BookGenres {  get; set; }

        public List<BookGenre> GetBookGenres(int bookID) => BookGenres.Where(b=>b.BookId == bookID).ToList();


        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Book> GetAllBooks
        {
            get
            {
                return new ObservableCollection<Book>(Books.Where(r => !r.IsFrozen));
            }
        }
        public ObservableCollection<Book> GetAllAuthorBooks (int AuthId)
        {
            return new ObservableCollection<Book>(Books.Where(b=>b.AuthorId == AuthId));
            
        }



        public ObservableCollection<Review> _AllReviews;


        public ObservableCollection<Review> GetReviewsToBook(int bookID)
        {
            return new ObservableCollection<Review>(_AllReviews.Where(r => r.BookId == bookID && !r.IsFrozen));
        }

        public int GetLastIdBook
        {
            get => Books.Max(a => a.BookId);
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
        public bool CheckHaveReview(int bookID)
        {
            return GetReviewsToBook(bookID).FirstOrDefault(b=>b.UserId == User.UserId) != null;
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

        }


        public ObservableCollection<Complaint> Complaints { get; set; }

        public List<StatusesRequest> statusesRequests { get; set; }

        /// <summary>
        /// Одобрение жалобы: заморозка автора, книги или отзыва
        /// </summary>
        /// <param name="complaint">Объект жалобы</param>
        /// <returns>True - операция выполнена успешно</returns>
        public bool AcceptComplain(Complaint complaint)

        {
            var CompDB = Complaints.FirstOrDefault(c=>c.ComplaintId==complaint.ComplaintId);

            CompDB.StatusesRequest = statusesRequests.First(a=>a.Name == "Одобрена");
            switch (CompDB.TargetType.Name)
            {
                case "Author":
                    {
                        var us = Users.First(a => a.UserId == CompDB.User1.UserId);
                        us.IsFrozen = true;
                        us.ReasonId = CompDB.ReasonId;
                        break;
                    }
                case "Book":
                    {
                        var book = Books.First(b => b.BookId == CompDB.BookId);
                        book.IsFrozen = true;
                        book.ReasonId = CompDB.ReasonId;

                        break;
                    }
                case "Review":
                    {
                        var revi = _AllReviews.FirstOrDefault(r => r.ReviewId == CompDB.ReviewId);
                        revi.IsFrozen = true;
                        revi.ReasonId = CompDB.ReasonId;

                        break;
                    }
            }
            Core.kingEntities.SaveChanges();
            return true;
        }
        public bool CancelAcceptRejectComplaint(Complaint complaint)
        {
            var CompDB = Complaints.FirstOrDefault(c=>c.ComplaintId==complaint.ComplaintId);

            CompDB.StatusesRequest = statusesRequests.First(a=>a.Name == "На рассмотрении");
            switch (CompDB.TargetType.Name)
            {
                case "Author":
                    {
                        var us = Users.First(a => a.UserId == CompDB.User1.UserId);
                        us.IsFrozen = false;
                        us.ReasonId = null;
                        break;
                    }
                case "Book":
                    {
                        var book = Books.First(b => b.BookId == CompDB.BookId);
                        book.IsFrozen = false;
                        book.ReasonId = null;

                        break;
                    }
                case "Review":
                    {
                        var revi = _AllReviews.FirstOrDefault(r => r.ReviewId == CompDB.ReviewId);
                        revi.IsFrozen = false;
                        revi.ReasonId = null;

                        break;
                    }
            }
            Core.kingEntities.SaveChanges();
            return true;
        }
        public bool RejectComplain(Complaint complaint)
        {
            var CompDB = Complaints.FirstOrDefault(c=>c.ComplaintId==complaint.ComplaintId);

            CompDB.StatusesRequest = statusesRequests.First(a=>a.Name == "Отклонена");
            Core.kingEntities.SaveChanges();
            return true;
        }

        public bool AcceptUnfreezeRequest(UnfreezeRequest request)
        {
            var CompDB = unfreezeRequests.FirstOrDefault(c => c.RequestId == request.RequestId);

            CompDB.StatusesRequest = statusesRequests.First(a => a.Name == "Одобрена");
            switch (CompDB.TargetType.Name)
            {
                case "Author":
                    {
                        var us = Users.First(a => a.UserId == CompDB.User.UserId);
                        us.ReasonIdHesh = us.ReasonId;
                        us.IsFrozen = false;
                        us.ReasonId = null;
                        break;
                    }
                case "Book":
                    {
                        var book = Books.First(b => b.BookId == CompDB.BookId);
                        book.ReasonIdHesh = book.ReasonId;
                        book.IsFrozen = false;
                        book.ReasonId = null;
                        //Books.Remove(book);
                        break;
                    }
            }
            Core.kingEntities.SaveChanges();
            return true;
        }
        public bool CancelAcceptRejectUnfreezeRequest(UnfreezeRequest request)
        {
            var CompDB = unfreezeRequests.FirstOrDefault(c => c.RequestId == request.RequestId);

            CompDB.StatusesRequest = statusesRequests.First(a => a.Name == "На рассмотрении");
            switch (CompDB.TargetType.Name)
            {
                case "Author":
                    {
                        var auth = Users.First(a => a.UserId == CompDB.User.UserId);
                        auth.IsFrozen = true;
                        auth.ReasonId = auth.ReasonIdHesh;
                        break;
                    }
                case "Book":
                    {
                        var book = Books.First(b => b.BookId == CompDB.BookId);
                        book.IsFrozen = true;
                        book.ReasonId = book.ReasonIdHesh;
                        break;
                    }
            }
            Core.kingEntities.SaveChanges();
            return true;
        }
        public bool RejectUnfreezeRequest(UnfreezeRequest request)
        {
            var CompDB = unfreezeRequests.FirstOrDefault(c => c.RequestId == request.RequestId);

            CompDB.StatusesRequest = statusesRequests.First(a => a.Name == "Отклонена");
            Core.kingEntities.SaveChanges();
            return true;
        }


        public ObservableCollection<AuthorRequest> AuthorRequests { get; set; }



        public bool AcceptAuthorRequest(AuthorRequest request)
        {
            var requestDB = AuthorRequests.FirstOrDefault(r => r.RequestId == request.RequestId);
            if (requestDB == null) return false;

            requestDB.StatusesRequest = statusesRequests.First(s => s.Name == "Одобрена");

            var user = Core.kingEntities.Users.First(u => u.UserId == requestDB.UserId);
            user.RoleId = Core.kingEntities.Roles.First(r => r.RoleName == "Автор").RoleId;

            Core.kingEntities.SaveChanges();
            return true;
        }

        public bool CancelAcceptRejectAuthorRequest(AuthorRequest request)
        {
            var requestDB = AuthorRequests.FirstOrDefault(r => r.RequestId == request.RequestId);
            if (requestDB == null) return false;

            requestDB.StatusesRequest = statusesRequests.First(s => s.Name == "На рассмотрении");

            var user = Core.kingEntities.Users.First(u => u.UserId == requestDB.UserId);
            user.RoleId = Core.kingEntities.Roles.First(r => r.RoleName == "Читатель").RoleId;

            Core.kingEntities.SaveChanges();
            return true;
        }

        public bool RejectAuthorRequest(AuthorRequest request)
        {
            var requestDB = AuthorRequests.FirstOrDefault(r => r.RequestId == request.RequestId);
            if (requestDB == null) return false;

            requestDB.StatusesRequest = statusesRequests.First(s => s.Name == "Отклонена");

            Core.kingEntities.SaveChanges();
            return true;
        }



        /// <summary>
        /// Подача жалобы на книгу, отзыв или автора
        /// </summary>
        /// <param name="item">Объект, на который подаётся жалоба (Book, Review, User)</param>
        /// <param name="reason">Причина жалобы</param>
        public void SaveFreezeRequest(object item, Reason reason)

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
                    StatusesRequest = statusesRequests.First(r => r.Name == "На рассмотрении"),

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
                    StatusesRequest = statusesRequests.First(r => r.Name == "На рассмотрении"),

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
                    TargetType = targetTypes.First(a => a.Name == "Author"),
                    StatusesRequest = statusesRequests.First(r=>r.Name == "На рассмотрении")

                };
            }
            Complaints.Add(complaint);
            Core.kingEntities.Complaints.Add(complaint);
            Core.kingEntities.SaveChanges();
        }

        public void FreezeUserReviewAdmin(Object item, Reason reason)
        {

            if (item is Review review)
            {
                var revi = _AllReviews.First(r => r.ReviewId == review.ReviewId);
                revi.IsFrozen = true;
                revi.ReasonId = reason.ReasonId;
            }
            else
            {
                var user = item as User;
                var us = Users.FirstOrDefault(u => u.UserId == user.UserId);
                user.IsFrozen = true;
                us.IsFrozen = true;

                user.Reason = reason;
                us.ReasonId = reason.ReasonId;
            }
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


        public ObservableCollection<UnfreezeRequest> unfreezeRequests {  get; set; }

    }
}
