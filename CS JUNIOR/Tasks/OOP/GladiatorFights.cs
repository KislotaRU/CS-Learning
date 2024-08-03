using System;
using System.Collections.Generic;

//Создать 5 бойцов, пользователь выбирает 2 бойцов и они сражаются друг с другом до смерти. 
//У каждого бойца могут быть свои статы.
//Каждый игрок должен иметь особую способность для атаки, которая свойственна только его классу!
//Если можно выбрать одинаковых бойцов, то это не должна быть одна и та же ссылка на одного бойца, 
//чтобы он не атаковал сам себя. Пример, что может быть уникальное у бойцов. Кто-то каждый 3 удар 
//наносит удвоенный урон, другой имеет 30% увернуться от полученного урона, кто-то при получении 
//урона немного себя лечит. Будут новые поля у наследников. У кого-то может быть мана и это только его особенность.

namespace CS_JUNIOR
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Game game = new Game();

            game.Start();
        }
    }

    abstract class Fighter
    {
        protected const int FullAmount = 100;

        protected const string AttackStandart = "Damage";
        protected const string AttackSkillBerserk = "DamageChapalakh";
        protected const string AttackSkillBum = "DamageVirus";
        protected const string AttackSkillSamurai = "DamageBleeding";

        protected const string ActionAttack = "Attack";
        protected const string ActionHeal = "Heal";
        protected const string CurrentAction = ActionAttack;

        public Fighter(string name, int health, int damage, int armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;

            Attacks = new Dictionary<string, float>();

            NominalHealth = health;
            Action = CurrentAction;
            CurrentActionAttack = AttackStandart;
        }

        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public int Armor { get; protected set; }
        public Dictionary<string, float> Attacks { get; protected set; }
        public int DamageSkill { get; protected set; }
        public int NominalHealth { get; protected set; }
        public int HealthPointsRecovery { get; protected set; }
        public string Action { get; protected set; }
        public string CurrentActionAttack { get; protected set; }
        public string CurrentActionHeal { get; protected set; }

        public virtual void Show()
        {
            Console.WriteLine($"\t{Name}\t\t{Health}\t\t{Damage}\t\t{Armor}");
        }

        public virtual void Act()
        {
            Attacks.Clear();
        }

        public virtual Dictionary<string, float> Attack()
        {
            switch (CurrentActionAttack)
            {
                case AttackStandart:
                    Attacks.Add(AttackStandart, Damage);
                    break;

                case AttackSkillBum:
                    Attacks.Add(AttackStandart, Damage);
                    Attacks.Add(AttackSkillBum, DamageSkill);
                    break;

                case AttackSkillBerserk:
                    Attacks.Add(AttackSkillBerserk, DamageSkill);
                    break;

                case AttackSkillSamurai:
                    Attacks.Add(AttackStandart, Damage);
                    Attacks.Add(AttackSkillSamurai, DamageSkill);
                    break;
            }

            return Attacks;
        }

        public virtual void TakeDamage(Dictionary<string, float> attacks, out float takeDamage)
        {
            takeDamage = 0;

            foreach (var damage in attacks)
                takeDamage += damage.Value;

            takeDamage -= (takeDamage / FullAmount * Armor);

            if (Health - (int)takeDamage < 0)
                Health = 0;
            else
                Health -= (int)takeDamage;
        }

        public virtual float Heal()
        {
            return HealthPointsRecovery;
        }

        public virtual void TakeHealth(float heal, out float takeHealth)
        {
            if (Health + (int)heal > NominalHealth)
            {
                takeHealth = NominalHealth - Health;
                Health = NominalHealth;
            }
            else
            {
                takeHealth = (int)heal;
                Health += (int)heal;
            }
        }

        public virtual void ShowAct(int lastTakeDamage = 0, int lastHeal = 0) { }
    }

    class Berserk : Fighter
    {
        private int _damageChapalakh = 40;
        private int _percentActiveChapalakh = 50;

        public Berserk(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            DamageSkill = _damageChapalakh;
        }

        public override Dictionary<string, float> Attack()
        {
            if (Health <= (NominalHealth / FullAmount * _percentActiveChapalakh))
                CurrentActionAttack = AttackSkillBerserk;
            else
                CurrentActionAttack = AttackStandart;

            return base.Attack();
        }

        public override void ShowAct(int lastTakeDamage = 0, int lastHeal = 0)
        {
            if (CurrentActionAttack == AttackStandart)
            {
                Console.Write($"{Name}\nНанёс урон базовой атакой.\n");
                Console.Write("Спокоен, как удав.\n");
            }

            if (CurrentActionAttack == AttackSkillBerserk)
            {
                Console.Write($"{Name}\nВ ярости.\n");
                Console.Write("Нанёс урон мощным чапалахом.\n");
            }

            Console.Write(new string('-', 30));
            Console.Write($"\nСнял противнику {lastTakeDamage} единиц здоровья.\n");
        }
    }

    class Bum : Fighter
    {
        private int _damageVirus = 30;

        private bool _isActiveVirus = false;

        public Bum(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            DamageSkill = _damageVirus;
        }

        public override Dictionary<string, float> Attack()
        {
            if (CurrentActionAttack == AttackStandart)
            {
                if (_isActiveVirus == true)
                    CurrentActionAttack = AttackSkillBum;

                _isActiveVirus = true;
            }

            return base.Attack();
        }

        public override void ShowAct(int lastTakeDamage = 0, int lastHeal = 0)
        {
            Console.Write($"{Name}\nНанёс урон базовой атакой.\n");

            if (CurrentActionAttack == AttackStandart)
                Console.Write("Подойдя к противнику, заразил его всеми возможными вирусами.\n");

            if (CurrentActionAttack == AttackSkillBum)
                Console.Write("Нанёс пассивный урон вирусами.\n");

            Console.Write(new string('-', 30));
            Console.Write($"\nСнял противнику {lastTakeDamage} единиц здоровья.\n");
        }
    }

    class Samurai : Fighter
    {
        private const int DefaultLifeTimeBleeding = 4;

        private int _damageBleeding = 8;
        private int _lifeTimeBleeding = 0;

        public Samurai(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            DamageSkill = _damageBleeding;
        }

        public override Dictionary<string, float> Attack()
        {
            if (_lifeTimeBleeding > 0)
            {
                CurrentActionAttack = AttackSkillSamurai;
                _lifeTimeBleeding--;
            }
            else
            {
                CurrentActionAttack = AttackStandart;
                _lifeTimeBleeding = DefaultLifeTimeBleeding;
            }

            return base.Attack();
        }

        public override void ShowAct(int lastTakeDamage = 0, int lastHeal = 0)
        {
            Console.Write($"{Name}\nНанёс урон базовой атакой.\n");

            if (CurrentActionAttack == AttackStandart)
                Console.Write("Повесил на противника кровотечение.\n");

            if (CurrentActionAttack == AttackSkillSamurai)
                Console.Write("Нанёс урон от кровотечение\n");

            Console.Write(new string('-', 30));
            Console.Write($"\nСнял противнику {lastTakeDamage} единиц здоровья.\n");
        }
    }

    class Witch : Fighter
    {
        private const int DefaultLifeTimeShield = 4;
        private const int DefaultCooldownShield = 2;

        private int _resistanceVirus = 100;
        private int _realArmor;
        private int _shieldArmor = 100;
        private int _lifeTimeShield = 0;
        private int _cooldownShield = DefaultCooldownShield;

        private int _percentageHealthPointsRecovery = 20;

        private bool _isUsingShield = false;
        private bool _isUsingHeal = false;

        public Witch(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _realArmor = Armor;
        }

        public override void Act()
        {
            if ((_lifeTimeShield == 0) & (HealthPointsRecovery > 0) & (_isUsingHeal == false))
            {
                Action = ActionHeal;
                _isUsingHeal = true;
            }
            else
            {
                Action = ActionAttack;
                _isUsingHeal = false;
            }

            if (_lifeTimeShield > 0)
            {
                Armor = _shieldArmor;
                _lifeTimeShield--;
            }
            else
            {
                Armor = _realArmor;
                _cooldownShield--;
            }

            if (_cooldownShield <= 0)
            {
                _lifeTimeShield = DefaultLifeTimeShield;
                _cooldownShield = DefaultCooldownShield;
            }

            base.Act();
        }

        public override Dictionary<string, float> Attack()
        {
            CurrentActionAttack = AttackStandart;

            return base.Attack();
        }

        public override void TakeDamage(Dictionary<string, float> attacks, out float takeDamage)
        {
            Dictionary<string, float> temporaryTakeDamage = new Dictionary<string, float>();

            foreach (var damage in attacks)
            {
                float temporaryValue = damage.Value;

                if (damage.Key == AttackSkillBum)
                    temporaryValue -= temporaryValue / FullAmount * _resistanceVirus;

                if (Armor == _shieldArmor)
                    HealthPointsRecovery += (int)temporaryValue;

                temporaryTakeDamage.Add(damage.Key, temporaryValue);
            }

            base.TakeDamage(temporaryTakeDamage, out takeDamage);
        }

        public override void TakeHealth(float heal, out float takeHealth)
        {
            heal = HealthPointsRecovery / FullAmount * _percentageHealthPointsRecovery;

            base.TakeHealth(heal, out takeHealth);

            HealthPointsRecovery = 0;
        }

        public override void ShowAct(int lastTakeDamage = 0, int lastHeal = 0)
        {
            if (Action == ActionAttack)
            {
                Console.Write($"{Name}\nНанесла урон базовой атакой.\n");

                if ((Armor == _shieldArmor) & (_isUsingShield == true))
                {
                    Console.Write("Растворилась в пространстве под названием \"Коридор между Мирами\".\n");
                    Console.Write("ПРИМЕЧАНИЕ:*Её атаки также досигаемы до противника.*\n");
                    _isUsingShield = false;
                }
                else if (Armor == _shieldArmor)
                {
                    Console.Write("Находится в пространстве под названием \"Коридор между Мирами\".\n");
                    Console.Write("ПРИМЕЧАНИЕ:*Её атаки также досигаемы до противника.*\n");
                }
                else if (Armor == _realArmor)
                {
                    Console.Write("Действие заклинания накапливается.\n");
                    _isUsingShield = true;
                }

                Console.Write(new string('-', 30));
                Console.Write($"\nСняла противнику {lastTakeDamage} единиц здоровья.\n");
            }

            if (Action == ActionHeal)
            {
                Console.Write($"{Name}\nАккумулировала весь полученный урон в {_percentageHealthPointsRecovery}% лечение.\n");

                Console.Write(new string('-', 30));
                Console.Write($"\nВосстановила себе {lastHeal} единиц здоровья.\n");
            }
        }
    }

    class Scout : Fighter
    {
        private const int DefaultCooldownHeal = 3;

        private int _healthPointsRecovery = 40;
        private int _resistanceVirus = 50;

        private int _cooldownHeal = DefaultCooldownHeal;

        public Scout(string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            HealthPointsRecovery = _healthPointsRecovery;
        }

        public override void Act()
        {
            if (_cooldownHeal <= 0)
            {
                Action = ActionHeal;
                _cooldownHeal = DefaultCooldownHeal;
            }
            else
            {
                Action = ActionAttack;
                _cooldownHeal--;
            }

            base.Act();
        }

        public override Dictionary<string, float> Attack()
        {
            CurrentActionAttack = AttackStandart;

            return base.Attack();
        }

        public override void TakeDamage(Dictionary<string, float> attacks, out float takeDamage)
        {
            Dictionary<string, float> temporaryTakeDamage = new Dictionary<string, float>();

            foreach (var damage in attacks)
            {
                float temporaryValue = damage.Value;

                if (damage.Key == AttackSkillBum)
                    temporaryValue -= temporaryValue / FullAmount * _resistanceVirus;

                if ((Action == ActionHeal) & (damage.Key == AttackSkillSamurai))
                    temporaryValue = 0;

                temporaryTakeDamage.Add(damage.Key, temporaryValue);
            }

            base.TakeDamage(temporaryTakeDamage, out takeDamage);
        }

        public override float Heal()
        {
            return base.Heal();
        }

        public override void ShowAct(int lastTakeDamage = 0, int lastHeal = 0)
        {
            if (Action == ActionAttack)
            {
                Console.Write($"{Name}\nНанёс урон базовой атакой.\n");

                if (_cooldownHeal <= 0)
                    Console.Write("Следующий ход будет перевязывать себе рану.\n");

                Console.Write(new string('-', 30));
                Console.Write($"\nСнял противнику {lastTakeDamage} единиц здоровья.\n");
            }

            if (Action == ActionHeal)
            {
                Console.Write($"{Name}\nПеревязал себе рану.\n");

                Console.Write(new string('-', 30));
                Console.Write($"\nВосстановил себе {lastHeal} единиц здоровья.\n");
            }
        }
    }

    class Arena
    {
        private const string FighterBerserk = "Berserk";
        private const string FighterBum = "Bum";
        private const string FighterSamrai = "Samurai";
        private const string FighterWitch = "Witch";
        private const string FighterScout = "Scout";

        private const int IndexFighter1 = 1;
        private const int IndexFighter2 = 2;

        private Fight _fight;

        private List<Fighter> _fighters = new List<Fighter>()
        {
            new Berserk(FighterBerserk, 200, 25, 35),
            new Bum(FighterBum, 90, 20, 30),
            new Samurai(FighterSamrai, 140, 30, 45),
            new Witch(FighterWitch, 100, 20, 0),
            new Scout(FighterScout, 120, 25, 25),
        };

        private Fighter _player1 = null;
        private Fighter _player2 = null;

        private int _indexFighter = IndexFighter1;
        private string _userInput = null;

        private bool _isFightersReady = false;

        public void Start()
        {
            Reset();
            SelectFighters();
            StartFight();
        }

        private void SelectFighters()
        {
            while (_isFightersReady == false)
            {
                ShowFighters();

                Console.WriteLine("\nНеобходимо выбрать двух бойцов.");

                ShowFighters(_player1, _player2);

                if (_player1 == null | _player2 == null)
                {
                    Console.WriteLine("\nОжидается ввод номера бойца...");
                    _userInput = Console.ReadLine();
                }

                switch (_indexFighter)
                {
                    case IndexFighter1:
                        _player1 = GetFighter();
                        break;

                    case IndexFighter2:
                        _player2 = GetFighter();
                        break;

                    default:
                        CompleteSelectionFighters();
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private Fighter GetFighter()
        {
            Fighter player;

            if (int.TryParse(_userInput, out int numberFighter) & (numberFighter > 0) & (numberFighter <= _fighters.Count))
            {
                player = CloneFighter(_fighters[numberFighter - 1]);

                Console.WriteLine($"Player {_indexFighter++} - выбрал {player.Name}.");
                return player;
            }
            else
            {
                Console.WriteLine("Такого бойца нет.");
                return null;
            }
        }

        private Fighter CloneFighter(Fighter fighter)
        {
            string typeFighter;

            typeFighter = fighter.Name;

            switch (typeFighter)
            {
                case FighterBerserk:
                    return new Berserk(fighter.Name, fighter.Health, fighter.Damage, fighter.Armor);

                case FighterBum:
                    return new Bum(fighter.Name, fighter.Health, fighter.Damage, fighter.Armor);

                case FighterSamrai:
                    return new Samurai(fighter.Name, fighter.Health, fighter.Damage, fighter.Armor);

                case FighterWitch:
                    return new Witch(fighter.Name, fighter.Health, fighter.Damage, fighter.Armor);

                case FighterScout:
                    return new Scout(fighter.Name, fighter.Health, fighter.Damage, fighter.Armor);

                default:
                    return null;
            }
        }

        private void CompleteSelectionFighters()
        {
            _isFightersReady = true;
            Console.WriteLine("\nБойцы укомплектованы.");
        }

        private void StartFight()
        {
            _fight = new Fight(_player1, _player2);

            _fight.Start();
        }

        private void Reset()
        {
            _isFightersReady = false;
            _indexFighter = IndexFighter1;

            _player1 = null;
            _player2 = null;
        }

        private void ShowFighters()
        {
            Console.WriteLine("Весь список бойцов:");
            Console.WriteLine("\nНомер\tБоец       \tЗдоровье        Урон        \tБроня\n");

            for (int i = 0; i < _fighters.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                _fighters[i].Show();
            }
        }

        private void ShowFighters(Fighter player1 = null, Fighter player2 = null)
        {
            Console.WriteLine($"Player {IndexFighter1}: {player1?.Name ?? "..."}");
            Console.WriteLine($"Player {IndexFighter2}: {player2?.Name ?? "..."}");
        }
    }

    class Fight
    {
        private const ConsoleColor ColorDefault = ConsoleColor.White;
        private const ConsoleColor ColorPlayer1 = ConsoleColor.Green;
        private const ConsoleColor ColorPlayer2 = ConsoleColor.Red;

        private const string MessagePlayer1 = "Player 1";
        private const string MessagePlayer2 = "Player 2";

        private const string MoveAttack = "Attack";
        private const string MoveHeal = "Heal";

        private Fighter _player1;
        private Fighter _player2;

        private int _numberFighters = 2;
        private int _movesCount = 1;

        public Fight(Fighter player1, Fighter player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        public void Start()
        {
            while (_player1.Health > 0 && _player2.Health > 0)
            {
                ShowInfo();

                if (_movesCount % _numberFighters != 0)
                {
                    Console.ForegroundColor = ColorPlayer1;
                    MakeMove(_player1, _player2);
                }
                else
                {
                    Console.ForegroundColor = ColorPlayer2;
                    MakeMove(_player2, _player1);
                }

                Console.ForegroundColor = ColorDefault;

                Console.WriteLine("Нажмите любую клавишу для следующего хода.");
                Console.ReadLine();
                Console.Clear();
            }

            if (_player2.Health <= 0)
                AnnounceResult(_player1, MessagePlayer1);
            else if (_player1.Health <= 0)
                AnnounceResult(_player2, MessagePlayer2);

            Console.ReadLine();
        }

        private void AnnounceResult(Fighter player, string messagePlayer)
        {
            Console.ForegroundColor = ColorPlayer1;
            Console.WriteLine($"Победил {messagePlayer} - {player.Name}." +
                              $"\nБитва длилась {_movesCount} ходов.");
            Console.ForegroundColor = ColorDefault;
        }

        private void MakeMove(Fighter player1, Fighter player2)
        {
            Console.WriteLine($"Ход №{_movesCount} {player1.Name}.");

            player1.Act();

            switch (player1.Action)
            {
                case MoveAttack:
                    MakeAttack(player1, player2);
                    break;

                case MoveHeal:
                    MakeHeal(player1);
                    break;

                default:
                    Console.WriteLine("Ничего не произошло.");
                    break;
            }

            _movesCount++;
        }

        private void MakeAttack(Fighter player1, Fighter player2)
        {
            player2.TakeDamage(player1.Attack(), out float takeDamage);
            player1.ShowAct((int)takeDamage);
        }

        private void MakeHeal(Fighter player1)
        {
            player1.TakeHealth(player1.Heal(), out float takeHealth);
            player1.ShowAct(lastHeal: (int)takeHealth);
        }

        private void ShowInfo()
        {
            Console.WriteLine(new string('#', 46));

            Console.ForegroundColor = ColorPlayer1;
            Console.WriteLine($"# Player 1 {_player1.Name}".PadRight(19, ' ') + "# " +
                              $"Здоровье {_player1.Health}".PadRight(13, ' ') + "# " +
                              $"Броня {_player1.Armor}".PadRight(9, ' ') + "#");

            Console.ForegroundColor = ColorPlayer2;
            Console.WriteLine($"# Player 2 {_player2.Name}".PadRight(19, ' ') + "# " +
                              $"Здоровье {_player2.Health}".PadRight(13, ' ') + "# " +
                              $"Броня {_player2.Armor}".PadRight(9, ' ') + "#");

            Console.ForegroundColor = ColorDefault;
            Console.WriteLine(new string('#', 46));

            Console.WriteLine("Информация о действиях:\n");
        }
    }

    class Game
    {
        private const string CommandPlay = "Play";
        private const string CommandLeave = "Exit";

        private Arena _arena;

        private string _userInput = null;

        private bool _isWorking = true;

        public void Start()
        {
            while (_isWorking)
            {
                Console.WriteLine("\t\t########## Арена Шиноби ##########");

                Console.WriteLine($"\n\t{CommandPlay} - Начать игру." +
                                  $"\n\t{CommandLeave} - Выйти.");

                Console.WriteLine("\nОжидается ввод команды...");
                _userInput = Console.ReadLine();

                switch (_userInput)
                {
                    case CommandPlay:
                        Play();
                        break;

                    case CommandLeave:
                        Leave();
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда!");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void Play()
        {
            _arena = new Arena();

            Console.Clear();
            _arena.Start();
        }

        private void Leave()
        {
            _isWorking = false;

            Console.Clear();
            Console.WriteLine("Вы завершили игру.");
        }
    }
}