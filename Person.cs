using System;
using System.Globalization;

namespace Lab3_2_Task3
{
    public class Person : INameAndCopy
    {
        // Закрытые поля
        private string _Name;
        private string _Surname;
        private DateTime _BirthDate;
        
        
        // Публичные свойства
        public string Name
        {
            get => _Name;
            set => _Name = value;
        }

        public string Surname
        {
            get => _Surname;
            set => _Surname = value;
        }

        public System.DateTime BirthDate
        {
            get => _BirthDate;
            set => _BirthDate = value;
        }

        public int BitrhYear
        {
            get => _BirthDate.Year;
            set => _BirthDate = new DateTime(value, _BirthDate.Month, _BirthDate.Day);
        }
        
        
        // Конструкторы
        public Person(string name, string surname, System.DateTime birthDate)
        {
            _Name = name;
            _Surname = surname;
            _BirthDate = birthDate;
        }
        
        
        // Конструктор по-умолчанию
        public Person()
        {
            _Name = "Ivan";
            _Surname = "Ivanov";
            _BirthDate = DateTime.Today;
        }
        
        // Перегрузка метода ToString
        public override string ToString()
        {
            return $"Person<Name: {_Name}, Surname: {_Surname}, BirthDate: {_BirthDate.ToString(CultureInfo.CurrentCulture)}>";
        }
        
        // Публичный виртуальный метод
        public virtual string ToShortString()
        {
            return $"Person<Name: {_Name}, Surname: {_Surname}>";
        }
        
        // Перегрузка операторов != и ==
        public static bool operator ==(Person p1, Person p2)
        {
            if (p1.Name == p2.Name && p1.Surname == p2.Surname && p1.BirthDate == p2.BirthDate) 
                return true;
            return false;
        }
        
        public static bool operator !=(Person p1, Person p2)
        {
            if (p1.Name == p2.Name && p1.Surname == p2.Surname && p1.BirthDate == p2.BirthDate) 
                return false;
            return true;
        }
        
        // Переопределение функции получения хеша
        public override int GetHashCode()
        {
            return HashCode.Combine(_Name, _Surname, _BirthDate);
        }
        
        // Реализация интерфейса INameAndCopy
        public object DeepCopy()
        {
            return new Person(_Name, _Surname, _BirthDate);
        }
    }
}