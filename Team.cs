using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab3_2_Task3
{
    public enum TimeFrame
    {
        Year,
        TwoYears,
        Long
    }

    public class Team : INameAndCopy
    {
        #region Fields
        protected int _RegistrationNumber;
        protected string _OrganizationName;
        #endregion

        #region Properties
        public string Name
        {
            get => _OrganizationName;
            set => _OrganizationName = value;
        }

        public int RegistrationNumber
        {
            get => _RegistrationNumber;
            set
            {
                if (value <= 0) throw new Exception("Регистрационный номер не может быть меньше или равен 0");
                _RegistrationNumber = value;
            }
        }
        #endregion

        #region Constructors
        public Team(string organizationName, int registrationNumber)
        {
            _OrganizationName = organizationName;
            _RegistrationNumber = registrationNumber;
        }

        public Team()
        {
            _OrganizationName = "Неизвестно";
            _RegistrationNumber = 000;
        }
        #endregion

        #region Methods
        public static bool operator ==(Team t1, Team t2)
        {
            if (t1.Name == t2.Name && t1.RegistrationNumber == t2.RegistrationNumber)
                return true;
            return false;
        }// Перегрузки оператора ==

        public static bool operator !=(Team t1, Team t2)
        {
            if (t1.Name == t2.Name && t1.RegistrationNumber == t2.RegistrationNumber)
                return false;
            return true;
        } // Перегрузки оператора !=

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Team otherTeam = (Team)obj;
            return _OrganizationName == otherTeam.Name && _RegistrationNumber == otherTeam.RegistrationNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_OrganizationName, _RegistrationNumber);
        }

        public override string ToString()
        {
            return $"Team<Name: {_OrganizationName}, RegistrtaionNumber: {_RegistrationNumber}>";
        }

        public virtual object DeepCopy()
        {
            return new Team(_OrganizationName, _RegistrationNumber);
        }
        #endregion
    }

    public class ResearchTeam : Team, IEnumerable
    {
        #region Fields
        private string _ResearchTheme;
        private TimeFrame _ResearchTimeFrame;
        private List<Person> _MembersList = new();
        private List<Paper> _PapersList = new();
        #endregion

        #region Properties
        public Paper LatestPaper
        {
            get
            {
                if (_PapersList.Count == 0) return null;

                Paper latest = (Paper)_PapersList[0];

                foreach (Paper paper in _PapersList)
                {
                    if (paper.PublishDate > latest.PublishDate)
                    {
                        latest = paper;
                    }
                }

                return latest;
            }
        }

        public string ResearchTheme
        {
            get => _ResearchTheme;
            set => _ResearchTheme = value;
        }

        public TimeFrame ResearchTimeFrame
        {
            get => _ResearchTimeFrame;
            set => _ResearchTimeFrame = value;
        }

        public List<Paper> PapersList
        {
            get => _PapersList;
            set => _PapersList = value;
        }

        public List<Person> MembersList
        {
            get => _MembersList;
            set => _MembersList = value;
        }

        public Team TeamInfo
        {
            get => new Team(Name, RegistrationNumber);
            set
            {
                Team teamParam = value;
                _OrganizationName = teamParam.Name;
                _RegistrationNumber = teamParam.RegistrationNumber;
            }
        }
        #endregion

        #region Constructors
        public ResearchTeam(string researchTheme, string organizationName, int registrationNumber, TimeFrame researchTimeFrame)
    : base(organizationName, registrationNumber)
        {
            _ResearchTheme = researchTheme;
            _ResearchTimeFrame = researchTimeFrame;
            _PapersList = new();
            _MembersList = new();
        }

        public ResearchTeam()
        {
            Random rnd = new();

            for (int i = 0; i < rnd.Next(2, 4); i++)
            {
                var person = new Person(
                    $"Name of Person {rnd.Next(10, 100)}",
                    $"Surname {rnd.Next(10, 100)}",
                    DateTime.Now - new TimeSpan(rnd.Next(5, 30), 0, 0, 0)
                );
                var paper = new Paper(
                    $"PaperName {rnd.Next(10, 100)}",
                    person,
                    DateTime.Now - new TimeSpan(rnd.Next(5, 30), 0, 0, 0)
                );
                AddMembers(person);
                AddPapers(paper);
            }

            _ResearchTheme = $"Тема исследования {rnd.Next(10, 10000)}";
            _OrganizationName = $"Название организации {rnd.Next(10, 10000)}";
            _RegistrationNumber = rnd.Next(100, 10000);
            _ResearchTimeFrame = TimeFrame.Year + rnd.Next(0, 10) % 3;
        }
        #endregion

        #region Methods
        public void AddPapers(params Paper[] papers)
        {
            _PapersList.AddRange(papers);
        }

        public void AddMembers(params Person[] members)
        {
            _MembersList.AddRange(members);
        }

        public virtual string ToShortString()
        {
            return $"ResearchTeam<ResearchTheme: {_ResearchTheme}, " +
                   $"OrganizationName: {_OrganizationName}, " +
                   $"RegistrationNumber: {_RegistrationNumber}, " +
                   $"ResearchTimeFrame: {_ResearchTimeFrame.ToString()}>";
        }

        public override string ToString()
        {
            string papersList = String.Join(", ", _PapersList);
            string membersList = String.Join(", ", _MembersList);

            return $"ResearchTeam<ResearchTheme: {_ResearchTheme}, " +
                   $"ResearchMembers: [{membersList}], " +
                   $"OrganizationName: {_OrganizationName}, " +
                   $"RegistrationNumber: {_RegistrationNumber}, " +
                   $"ResearchTimeFrame: {_ResearchTimeFrame.ToString()}, " +
                   $"Papers: [{papersList}]>";
        }

        public override object DeepCopy()
        {
            List<Person> copyMemberList = new();
            List<Paper> copyPaperList = new();

            foreach (Person member in _MembersList) copyMemberList.Add((Person)member.DeepCopy());
            foreach (Paper paper in _PapersList) copyPaperList.Add((Paper)paper.DeepCopy());

            ResearchTeam newResearchTeam = new ResearchTeam(_ResearchTheme, _OrganizationName,
                _RegistrationNumber, _ResearchTimeFrame)
            {
                PapersList = copyPaperList,
                MembersList = copyMemberList
            };

            return newResearchTeam;
        }//Копия объекта

        public void SortPapersByPublishDate() =>
            PapersList.Sort((p1, p2) => p1.CompareTo(p2));//Сортировка по дате публикации

        public void SortPapersByName() =>
            PapersList.Sort((p1, p2) => new Paper().Compare(p1, p2));//Сортировка по названию публикации

        public void SortPapersByAuthorSurname() =>
            PapersList.Sort((p1, p2) => new PaperAuthorSurnameComparer().Compare(p1, p2));//Сортировка по фамилии автора

        #endregion

        #region Iterators
        public IEnumerable<Person> IMembersWithoutPublications()
        {
            bool isNotPublicated = true;
            foreach (Person person in _MembersList)
            {
                foreach (Paper paper in _PapersList)
                {
                    if (paper.Author == person)
                    {
                        isNotPublicated = false;
                        break;
                    }
                }

                if (isNotPublicated) yield return person;
                isNotPublicated = true;
            }
        }

        public IEnumerable<Paper> IPapersReleasedOverYears(int years)
        {
            DateTime relativeDateTime = DateTime.Now.AddYears(-years);

            foreach (Paper paper in _PapersList)
            {
                if (paper.PublishDate > relativeDateTime) yield return paper;
            }


        }

        public IEnumerable<Person> IMembersWithMoreThanOnePapers()
        {
            int publishes = 0;
            foreach (Person person in _MembersList)
            {
                foreach (Paper paper in _PapersList)
                {
                    if (paper.Author == person) publishes++;
                }

                if (publishes > 1) yield return person;
                publishes = 0;
            }
        }

        public IEnumerable<Paper> IPapersForLastYear()
        {
            DateTime relationDateTime = DateTime.Now.AddYears(-1);

            foreach (Paper paper in _PapersList)
            {
                if (paper.PublishDate >= relationDateTime) yield return paper;
            }
        }

        public IEnumerator GetEnumerator()
        {
            List<Person> personsWithPublishes = new();
            foreach (Person person in _MembersList)
            {
                foreach (Paper paper in _PapersList)
                {
                    if (paper.Author == person)
                    {
                        personsWithPublishes.Add(person);
                        break;
                    }
                }
            }
            return new ResearchTeamEnumerator(personsWithPublishes);
        } // Реализация интерфейса IEnumerable
        #endregion
    }

    public class ResearchTeamEnumerator : IEnumerator
    {
        private readonly List<Person> PersonsList;
        private int position = -1;
        
        public ResearchTeamEnumerator(List<Person> personsList)
        {
            PersonsList = personsList;
        }
        
        public bool MoveNext()
        {
            position++;
            return position < PersonsList.Count;
        }

        public void Reset() => position = -1;
        

        object IEnumerator.Current => Current;
        
        public Person Current
        {
            get
            {
                try
                {
                    return PersonsList[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        
    }
}