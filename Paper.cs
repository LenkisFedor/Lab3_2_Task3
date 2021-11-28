using System;
using System.Collections.Generic;

namespace Lab3_2_Task3
{
    public class Paper : IComparable, IComparer<Paper>
    {
        #region Properies
        public string Name { get; set; }
        public Person Author { get; set; }
        public DateTime PublishDate { get; set; }
        #endregion

        #region Constructors
        public Paper(string name, Person author, DateTime publishDate)
        {
            Name = name;
            Author = author;
            PublishDate = publishDate;
        }

        public Paper()
        {
            Name = "Неизвестно";
            Author = new Person();
            PublishDate = DateTime.Now;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Paper<Name: {Name}, " +
                   $"Author: {Author}, " +
                   $"PublishDate: {PublishDate}>";
        }

        public virtual object DeepCopy()
        {
            return new Paper(Name, (Person)Author.DeepCopy(), PublishDate);
        }

        public int CompareTo(object obj)
        {
            if (obj is null) return 0;
            Paper otherPaper = (Paper)obj;

            return PublishDate.CompareTo(otherPaper.PublishDate);
        }

        public int Compare(Paper x, Paper y)
        {
            if (x is null || y is null) return 0;
            return String.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
        #endregion
    }

    public class PaperAuthorSurnameComparer : IComparer<Paper>//класс для сравнения объектов типа Paper по имени автора 
    {
        public int Compare(Paper x, Paper y)
        {
            if (x is null || y is null) return 0;
            return String.Compare(x.Author.Surname, y.Author.Surname, StringComparison.Ordinal);
        }
    }
}