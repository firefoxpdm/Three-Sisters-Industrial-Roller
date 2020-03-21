using System.Collections.Generic;
using System.Linq;

namespace ThreeSistersIndustrialRoller
{
    public class ThreeSistersResolver
    {
        static readonly Dictionary<string, ISet<string>> plantsProduces = new Dictionary<string, ISet<string>>();

        static ThreeSistersResolver()
        {
            add("BcLS_SistersAItemSpawner", "RawBlueCorn", "RawRedLentil", "Rawsquash");
            add("CLS_SistersAItemSpawner", "RawCorn", "RawRedLentil", "Rawsquash");
            add("CLP_SistersAItemSpawner", "RawCorn", "RawRedLentil", "Rawpumpkin");
            add("CBS_SistersAItemSpawner", "RawCorn", "Rawbean", "Rawsquash");
            add("CBP_SistersAItemSpawner", "RawCorn", "Rawbean", "Rawpumpkin");
            add("BcLP_SistersAItemSpawner", "RawBlueCorn", "RawRedLentil", "Rawpumpkin");
            add("BcBS_SistersAItemSpawner", "RawBlueCorn", "Rawbean", "Rawsquash");
            add("BcBP_SistersAItemSpawner", "RawBlueCorn", "Rawbean", "Rawpumpkin");

            add("PLS_SistersAItemSpawner", "Rawpeach", "RawRedLentil", "RawBerries");
            add("PBS_SistersAItemSpawner", "Rawpeach", "Rawbean", "RawBerries");
            add("ALS_SistersAItemSpawner", "Rawapple", "RawRedLentil", "RawBerries");
            add("ABS_SistersAItemSpawner", "Rawapple", "Rawbean", "RawBerries");

            add("Pomato_SistersAItemSpawner", "RawTomatoes", "RawPotatoes");
        }

        private static void add(string plant, params string[] products)
        {
            ISet<string> set = new HashSet<string>();
            products.All(product => set.Add(product));
            plantsProduces.Add(plant, set);
        }

        public static bool ItemMatchesGrowthZonePlantProduce(string item, string plantProduce)
        {
            if (item.Equals(plantProduce))
            {
                return true;
            }

            ISet<string> set;
            plantsProduces.TryGetValue(plantProduce, out set);
            if (set == null)
            {
                return false;
            }
            bool result = set.Contains(item);
            return result;
        }
    }
}
