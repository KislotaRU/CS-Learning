using System;
using System.Collections.Generic;
using System.IO;

//У вас есть программа, которая помогает пользователю составить план поезда.
//Есть 4 основных шага в создании плана:
//-Создать направление - создает направление для поезда(к примеру Бийск - Барнаул)
//-Продать билеты - вы получаете рандомное кол-во пассажиров, которые купили билеты на это направление
//-Сформировать поезд - вы создаете поезд и добавляете ему столько вагонов(вагоны могут быть разные по вместительности),
//сколько хватит для перевозки всех пассажиров.
//-Отправить поезд - вы отправляете поезд, после чего можете снова создать направление.
//В верхней части программы должна выводиться полная информация о текущем рейсе или его отсутствии.

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Station station = new Station();
        }
    }

    class Station
    {
        private const string StatusDefault = "Нет данных.";
        private const string StatusSetDirection = "Ожидается выбор направление.";
        private const string StatusTicketSales = "Продажа билетов.";
        private const string StatusForm = "Формируется поезд.";
        private const string StatusReady = "Поезд готов к отправке.";
        private const string StatusOnWay = "Поезд в пути.";
        private const string StatusNextFlight = "Ожидается следующий рейс.";

        private Train _train;
        private Passengers _passengers;

        public Station()
        {
            Status = StatusDefault;

            Start();
        }

        public string Status { get; private set; }

        public void Start()
        {
            bool isWorking = true;

            CreateFlight();

            while (isWorking)
            {
                ShowInfo();

                switch (Status)
                {
                    case StatusDefault:
                        StartPreparingTrain();
                        break;

                    case StatusSetDirection:
                        SetDirection();
                        break;

                    case StatusTicketSales:
                        SellTicket();
                        break;

                    case StatusForm:
                        FormTrain();
                        break;

                    case StatusReady:
                        Status = StatusOnWay;
                        break;

                    case StatusOnWay:
                        Status = StatusNextFlight;
                        break;

                    case StatusNextFlight:
                        CreateFlight();
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }

        private void StartPreparingTrain()
        {
            Status = StatusSetDirection;
        }

        private void SetDirection()
        {
            Console.WriteLine("Создаётся направление для поезда...");

            if (_train.GetCountDirections() > 0)
            {
                _train.SetDirection();
                Status = StatusTicketSales;
            }
            else
            {
                Console.WriteLine("Невозможно выбрать направление." +
                                  "\nПричина: Отсутсвие каких либо направлений.");
            }

        }

        private void SellTicket()
        {
            Console.WriteLine("Пассажиры покупают билеты...");
            _passengers.SetCount();
            Status = StatusForm;
        }

        private void FormTrain()
        {
            Console.WriteLine($"Формируется поезд для {_passengers.Count} пассажиров...");
            _train.Form(_passengers.Count);

            if (_passengers.Count <= _train.GetCountPlaces())
            {
                Console.WriteLine("Пассажиры все в вагонах.");
                Status = StatusReady;
            }
        }

        private void CreateFlight()
        {
            Status = StatusDefault;

            _train = new Train();
            _passengers = new Passengers();
        }

        private void ShowInfo()
        {
            Console.WriteLine(">>> Текущий рейс:" +
                                    $"\n>>> {_train.Direction.PointA} - {_train.Direction.PointB}" +
                                    $"\n>>> Билетов куплено: {_passengers.Count}" +
                                    $"\n>>> Кол-во пассажиров: {_passengers.Count}" +
                                    $"\n>>> Всего мест в поезде: {_train.GetCountPlaces()}" +
                                    $"\n>>> Кол-во вагонов: {_train.GetCountWagons()}" +
                                    $"\n>>> Статус: {Status}\n\n");
        }
    }

    class Train
    {
        private Wagons _wagons = new Wagons();
        private Directions _directions = new Directions();
        private Wagons _wagonsForConnection = new Wagons();

        public Train()
        {
            Direction = new Direction();
        }

        public Direction Direction { get; private set; }

        public void AddWagon(Wagon wagon)
        {
            _wagons.AddWagon(wagon);
        }

        public void SetDirection()
        {
            Direction = _directions.GetDirection();
        }

        public int GetCountDirections()
        {
            return _directions.Count;
        }

        public void Form(int countPassengers)
        {
            string userInput;
            int numberWagon;

            if (_wagonsForConnection.Count <= 0)
            {
                _wagonsForConnection.Create();
            }

            if (countPassengers <= GetCountPlaces())
            {
                Console.WriteLine("Все пассажиры вместились.");

                return;
            }

            Console.WriteLine("\nВам предоставляются вагоны." +
                                "\nВместите всех пассажиров в поезд.");

            Console.WriteLine("Доступные вагоны на данных момент:");
            _wagonsForConnection.Show();

            do
            {
                Console.Write("\nВведите номер вагона для присоединения к поезду." +
                              "\nНомер вагона: ");
                userInput = Console.ReadLine();
            }
            while (int.TryParse(userInput, out numberWagon) == false);

            if (_wagonsForConnection.Count > numberWagon - 1 && numberWagon >= 1)
            {
                _wagons.AddWagon(_wagonsForConnection.GetWagon(numberWagon - 1));
                _wagonsForConnection.RemoveWagon(_wagonsForConnection.GetWagon(numberWagon - 1));
            }
            else
            {
                Console.WriteLine("Такого вагона нет.");
            }
        }

        public int GetCountWagons()
        {
            return _wagons.Count;
        }

        public int GetCountPlaces()
        {
            return _wagons.GetCountPlaces();
        }
    }

    class Wagons
    {
        private const int CountToSearch = 10;

        private List<Wagon> _wagons = new List<Wagon>();

        public int Count { get; private set; }

        public void Show()
        {
            int numberWagon = 0;

            for (int i = 0; i < _wagons.Count; i++)
            {
                numberWagon++;

                Console.Write($"{numberWagon}. Вагон с кол-вом мест: {_wagons[i].CountPlaces}\n");
            }
        }

        public void AddWagon(Wagon wagon)
        {
            _wagons.Add(wagon);
            Count++;
        }

        public void RemoveWagon(Wagon wagon)
        {
            _wagons.Remove(wagon);
            Count--;
        }

        public void Create()
        {
            Random random = new Random();

            for (int i = 0; i < CountToSearch; i++)
            {
                _wagons.Add(new Wagon(random));
            }

            Count = CountToSearch;
        }

        public Wagon GetWagon(int index)
        {
            return _wagons[index];
        }

        public int GetCountPlaces()
        {
            int countPlaces = 0;

            foreach (Wagon wagon in _wagons)
            {
                countPlaces += wagon.CountPlaces;
            }

            return countPlaces;
        }
    }

    class Wagon
    {
        private const int MinCountPlaces = 15;
        private const int MaxCountPlaces = 28;

        public Wagon(Random random)
        {
            Create(random);
        }

        public int CountPlaces { get; private set; }

        public void Create(Random random)
        {
            CountPlaces = random.Next(MinCountPlaces, MaxCountPlaces);
        }
    }

    class Passengers
    {
        public int Count { get; private set; }

        public void SetCount()
        {
            Count = GetCount();
        }

        private int GetCount()
        {
            Random random = new Random();

            int minCountPassengers = 72;
            int maxCountPassengers = 150;

            return random.Next(minCountPassengers, maxCountPassengers);
        }
    }

    class Directions
    {
        private const string NameFile = "Directions";

        private List<Direction> _directions = new List<Direction>();

        public Directions()
        {
            Add();
        }

        public int Count { get; private set; }

        public Direction GetDirection()
        {
            Random random = new Random();

            int minRange = 0;
            int maxRange = _directions.Count;

            return _directions[random.Next(minRange, maxRange)];
        }

        private void Add()
        {
            string[] directions = new string[6]
            {
                "СПб-Москва",
                "СПб-Минск",
                "Архангельск-Москва",
                "Сочи-СПб",
                "СПб-Псков",
                "Бийск-Баранул",
            };

            for (int i = 0; i < directions.Length; i++)
            {
                string[] direction = directions[i].Split('-');

                Write(direction);
            }
        }

        private void ReadFile(string nameFile = null)
        {
            string[] directions;

            if (File.Exists($"DataBase/{nameFile}.txt") == true)
                directions = File.ReadAllLines($"DataBase/{nameFile}.txt");
            else
                return;

            for (int i = 0; i < directions.Length; i++)
            {
                string[] direction = directions[i].Split('-');

                Write(direction);
            }
        }

        private void Write(string[] direction)
        {
            int i = 0;

            string pointA;
            string pointB;

            pointA = direction[i++];
            pointB = direction[i];

            _directions.Add(new Direction(pointA, pointB));
            Count++;
        }
    }

    class Direction
    {
        public Direction(string pointA = "Нет данных", string pointB = "Нет данных")
        {
            PointA = pointA;
            PointB = pointB;
        }

        public string PointA { get; private set; }
        public string PointB { get; private set; }
    }
}