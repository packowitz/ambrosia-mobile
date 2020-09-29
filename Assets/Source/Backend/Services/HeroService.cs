using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class HeroService
    {
        private List<Hero> heroes;
        private HeroComparer comparer = new HeroComparer();

        public HeroService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (signal.Data.heroes != null)
                {
                    if (heroes == null)
                    {
                        heroes = signal.Data.heroes;
                    }
                    else
                    {
                        foreach (var hero in signal.Data.heroes)
                        {
                            Update(hero);
                        }
                    }
                }

                if (signal.Data.hero != null)
                {
                    Update(signal.Data.hero);
                }

                if (signal.Data.heroIdsRemoved != null)
                {
                    heroes = heroes.Where(h => !signal.Data.heroIdsRemoved.Contains(h.id)).ToList();
                }
            });
        }

        public List<Hero> AllHeroes()
        {
            return heroes;
        }

        public void Update(Hero hero)
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