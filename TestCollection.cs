using System;
using System.Collections.Generic;

namespace Lab3_2_Task3
{
    public delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    
    class TestCollection<TKey, TValue>
    {
        #region Fields
        private List<TKey> KeysList = new();

        private List<string> StringList = new();

        private Dictionary<TKey, TValue> KeyDict = new();

        private Dictionary<string, TValue> StringDict = new();

        private GenerateElement<TKey, TValue> GenerateElement;

        private int ElementsCount;
        #endregion

        #region Constructors
        public TestCollection(int elementsCount, GenerateElement<TKey, TValue> generateElement)
        {
            GenerateElement = generateElement;
            ElementsCount = elementsCount;

            for (int i = 0; i < ElementsCount; i++)
            {
                var el = GenerateElement(i);
                KeysList.Add(el.Key);
                StringList.Add(el.Key.ToString());
                KeyDict.Add(el.Key, el.Value);
                StringDict.Add(el.Key.ToString(), el.Value);
            }
        }
        #endregion

        #region Methods
        public void TestWithContainsInLists()
        {
            var first = GenerateElement(0).Key;
            var middle = GenerateElement((ElementsCount - 1) / 2).Key;
            var last = GenerateElement(ElementsCount - 1).Key;
            var notInCol = GenerateElement(ElementsCount + 2).Key;


            var timeStart = Environment.TickCount;
            KeysList.Contains(first);
            var timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск первого в KeysList: {timeDelta}");

            timeStart = Environment.TickCount;
            KeysList.Contains(middle);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск среднего в KeysList: {timeDelta}");

            timeStart = Environment.TickCount;
            KeysList.Contains(last);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск последнего в KeysList: {timeDelta}");

            timeStart = Environment.TickCount;
            KeysList.Contains(notInCol);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск не входящего в KeysList: {timeDelta}");

            var firstStr = GenerateElement(0).Key.ToString();
            var middleStr = GenerateElement((ElementsCount - 1) / 2).Key.ToString();
            var lastStr = GenerateElement(ElementsCount - 1).Key.ToString();
            var notInColStr = GenerateElement(ElementsCount + 2).Key.ToString();

            timeStart = Environment.TickCount;
            StringList.Contains(firstStr);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск первого в StringList: {timeDelta}");

            timeStart = Environment.TickCount;
            StringList.Contains(middleStr);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск среднего в StringList: {timeDelta}");

            timeStart = Environment.TickCount;
            StringList.Contains(lastStr);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск последнего в StringList: {timeDelta}");

            timeStart = Environment.TickCount;
            StringList.Contains(notInColStr);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск не входящего в StringList: {timeDelta}");
        }

        public void TestWithContainsKeyInDict()
        {
            var first = GenerateElement(0).Key;
            var middle = GenerateElement((ElementsCount - 1) / 2).Key;
            var last = GenerateElement(ElementsCount - 1).Key;
            var notInCol = GenerateElement(ElementsCount + 2).Key;

            var timeStart = Environment.TickCount;
            KeyDict.ContainsKey(first);
            var timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск по ключу первого в KeyDict: {timeDelta}");

            timeStart = Environment.TickCount;
            KeyDict.ContainsKey(middle);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск по ключу среднего в KeyDict: {timeDelta}");

            timeStart = Environment.TickCount;
            KeyDict.ContainsKey(last);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск по ключу последнего в KeyDict: {timeDelta}");

            timeStart = Environment.TickCount;
            KeyDict.ContainsKey(notInCol);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск по ключу не входящего в KeyDict: {timeDelta}");

            var firstStr = GenerateElement(0).Key.ToString();
            var middleStr = GenerateElement((ElementsCount - 1) / 2).Key.ToString();
            var lastStr = GenerateElement(ElementsCount - 1).Key.ToString();
            var notInColStr = GenerateElement(ElementsCount + 2).Key.ToString();

            timeStart = Environment.TickCount;
            StringDict.ContainsKey(firstStr);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск по ключу первого в StringDict: {timeDelta}");

            timeStart = Environment.TickCount;
            StringDict.ContainsKey(middleStr);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск по ключу среднего в StringDict: {timeDelta}");

            timeStart = Environment.TickCount;
            StringDict.ContainsKey(lastStr);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск по ключу последнего в StringDict: {timeDelta}");

            timeStart = Environment.TickCount;
            StringDict.ContainsKey(notInColStr);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск по ключу не входящего в StringDict: {timeDelta}");
        }

        public void TestWithContainsValueInDict()
        {
            var first = GenerateElement(0).Value;
            var middle = GenerateElement((ElementsCount - 1) / 2).Value;
            var last = GenerateElement(ElementsCount - 1).Value;
            var notInCol = GenerateElement(ElementsCount + 2).Value;

            var timeStart = Environment.TickCount;
            KeyDict.ContainsValue(first);
            var timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск по значению первого в KeyDict: {timeDelta}");

            timeStart = Environment.TickCount;
            KeyDict.ContainsValue(middle);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск по значению среднего в KeyDict: {timeDelta}");

            timeStart = Environment.TickCount;
            KeyDict.ContainsValue(last);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск по значению последнего в KeyDict: {timeDelta}");

            timeStart = Environment.TickCount;
            KeyDict.ContainsValue(notInCol);
            timeDelta = Environment.TickCount - timeStart;
            Console.WriteLine($"Поиск по значению не входящего в KeyDict: {timeDelta}");
        }
        #endregion

    }
}