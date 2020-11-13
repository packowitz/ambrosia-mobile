using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class HeroService
    {
        private List<Hero> heroes;
        private readonly HeroComparer comparer = new HeroComparer();

        public HeroService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        public List<Hero> AllHeroes()
        {
            return heroes;
        }

        public Hero Hero(long id)
        {
            return heroes.Find(h => h.id == id);
        }

        public Hero AvailableHero(long? id)
        {
            if (id == null)
            {
                return null;
            }
            var hero = heroes.Find(h => h.id == id);
            if (hero?.IsAvailable() == true)
            {
                return hero;
            }

            return null;
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.heroes != null)
            {
                if (heroes == null)
                {
                    heroes = data.heroes;
                    SortHeroes();
                }
                else
                {
                    foreach (var hero in data.heroes)
                    {
                        Update(hero);
                    }
                }
            }

            if (data.hero != null)
            {
                Update(data.hero);
            }

            if (data.heroIdsRemoved != null)
            {
                heroes = heroes.Where(h => !data.heroIdsRemoved.Contains(h.id)).ToList();
            }
        }

        private void Update(Hero hero)
        {
            var idx = heroes.FindIndex(h => h.id == hero.id);
            if (idx >= 0)
            {
                heroes[idx] = hero;
            }
            else
            {
                heroes.Add(hero);
                SortHeroes();
            }
        }

        private void SortHeroes()
        {
            heroes.Sort(comparer);
        }
    }

    public class HeroComparer : IComparer<Hero>
    {
        public int Compare(Hero x, Hero y)
        {
            if (x.level != y.level)
            {
                return y.level.CompareTo(x.level);
            }
            
            if (x.stars != y.stars)
            {
                return y.stars.CompareTo(x.stars);
            }
            
            if (x.heroBaseId != y.heroBaseId)
            {
                return x.heroBaseId.CompareTo(y.heroBaseId);
            }
            
            return x.id.CompareTo(y.id);
        }
    }
}