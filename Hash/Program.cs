using Hash.DBStructure;
using System.Text;
using Var21.DBStructure;

namespace Hash
{
    internal class Program
    {
        public static UInt32 BadHash(string input)
        {
            UInt32 result = 0;

            for(UInt32 i = 0; i < input.Length; i += 2)
            {
                result += (Convert.ToUInt32(input[Convert.ToInt32(i)]) << Convert.ToInt32(i % 15)) ^ (Convert.ToUInt32(input[Convert.ToInt32(i)]));
            }
            for (UInt32 i = 1; i < input.Length; i += 2)
            {
                result += (Convert.ToUInt32(input[Convert.ToInt32(i)]) >> Convert.ToInt32(i % 15)) ^ (Convert.ToUInt32(input[Convert.ToInt32(i)]));
            }

            return result;
        }

        public static UInt32 GoodHash(string input)
        {
            UInt32 result = 0;
            List<int> shifts = new List<int> { 5, 1, 3, 2 };

            string first = input;

            while ((first.Length & 3) != 0)
                first = first + Convert.ToChar(((first.Length) % 10) ^ 0x4BAA);


            List<UInt32> list = new List<UInt32>();

            for (int i = 0; i < first.Length - 1; i++)
            {
                list.Add(Convert.ToUInt32(first[i]) ^ Convert.ToUInt32(first[i + 1]) ^ 0xC5E2);
            }

            list.Add((Convert.ToUInt32(first[first.Length - 1]) ^ Convert.ToUInt32(first[0])) ^ 0xC5E2);

            List<UInt32> list2 = new List<UInt32>();

            for (int i = 0; i < list.Count; i += 4)
            {
                list2.Add(list[i + 3] + (list[i + 2] << 8) + (list[i + 1] << 16) + (list[i] << 24));
            }

            for (int i = 0; i < list2.Count; i += 2)
            {
                result += (list2[i] << shifts[i & 3]) ^ 0x9C3B;
            }
            for (int i = 1; i < list2.Count; i += 2)
            {
                result += (list2[i] >> shifts[i & 3]) ^ 0x8B6C;
            }

            return result;
        }

        static InfoGood SearchElement(Lookup<UInt32, InfoGood> data, InfoGood find)
        {
            return data.Where(x => x.Key == find.Hash).SelectMany(x => x).First(x => x == find);
        }

        static InfoBad SearchElement(Lookup<UInt32, InfoBad> data, InfoBad find)
        {
            return data.Where(x => x.Key == find.Hash).SelectMany(x => x).First(x => x == find);
        }

        static string CreatString()
        {
            string result = "";

            Random rand = new Random();

            int lenght = rand.Next(3, 60);

            for (int i = 0; i < lenght; i++)
                result = result + Convert.ToChar(rand.Next(50, 256));

            return result;
        }

        static void Main(string[] args)
        {
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                List<string> datas = new List<string>();

                for (int k = 0; k < 326797; k++)
                {
                    datas.Add(CreatString());
                }

                datas = datas.Distinct().ToList();

                List<UInt32> hashes = datas.Select(x => BadHash(x)).Distinct().ToList();

                Console.WriteLine($"Number of collisions on {datas.Count} elements is {datas.Count - hashes.Count}");
            }
            
            using (var db = new DataContext100())
            {
                List<BaseInfo> infos = db.Info.ToList();
                List<InfoGood> infosGood = infos.Select(x => new InfoGood(x)).ToList();
                List<InfoBad> infosBad = infos.Select(x => new InfoBad(x)).ToList();

                Lookup<UInt32, InfoGood> infosGoodLook = (Lookup<UInt32, InfoGood>)infosGood.ToLookup(x => x.Hash, x => x);
                Lookup<UInt32, InfoBad> infosBadLook = (Lookup<UInt32, InfoBad>)infosBad.ToLookup(x => x.Hash, x => x);

                InfoGood findGood = infosGood[random.Next(infosGood.Count)];

                DateTime start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                findGood = infosGood[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                DateTime end = DateTime.Now;

                Console.WriteLine($"Good Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                InfoBad findBad = infosBad[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosBadLook, findBad);

                end = DateTime.Now;

                Console.WriteLine($"Bad Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                Console.WriteLine($"GoodHash, {infos.Count} elements, {infosGood.Count - infosGoodLook.Count} collisions");
                Console.WriteLine($"BadHash, {infos.Count} elements, {infosBad.Count - infosBadLook.Count} collisions");
            };

            using (var db = new DataContext1000())
            {
                List<BaseInfo> infos = db.Info.ToList();
                List<InfoGood> infosGood = infos.Select(x => new InfoGood(x)).ToList();
                List<InfoBad> infosBad = infos.Select(x => new InfoBad(x)).ToList();

                Lookup<UInt32, InfoGood> infosGoodLook = (Lookup<UInt32, InfoGood>)infosGood.ToLookup(x => x.Hash, x => x);
                Lookup<UInt32, InfoBad> infosBadLook = (Lookup<UInt32, InfoBad>)infosBad.ToLookup(x => x.Hash, x => x);

                InfoGood findGood = infosGood[random.Next(infosGood.Count)];

                DateTime start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                findGood = infosGood[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                DateTime end = DateTime.Now;

                Console.WriteLine($"Good Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                InfoBad findBad = infosBad[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosBadLook, findBad);

                end = DateTime.Now;

                Console.WriteLine($"Bad Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                Console.WriteLine($"GoodHash, {infos.Count} elements, {infosGood.Count - infosGoodLook.Count} collisions");
                Console.WriteLine($"BadHash, {infos.Count} elements, {infosBad.Count - infosBadLook.Count} collisions");
            };

            using (var db = new DataContext10000())
            {
                List<BaseInfo> infos = db.Info.ToList();
                List<InfoGood> infosGood = infos.Select(x => new InfoGood(x)).ToList();
                List<InfoBad> infosBad = infos.Select(x => new InfoBad(x)).ToList();

                Lookup<UInt32, InfoGood> infosGoodLook = (Lookup<UInt32, InfoGood>)infosGood.ToLookup(x => x.Hash, x => x);
                Lookup<UInt32, InfoBad> infosBadLook = (Lookup<UInt32, InfoBad>)infosBad.ToLookup(x => x.Hash, x => x);

                InfoGood findGood = infosGood[random.Next(infosGood.Count)];

                DateTime start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                findGood = infosGood[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                DateTime end = DateTime.Now;

                Console.WriteLine($"Good Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                InfoBad findBad = infosBad[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosBadLook, findBad);

                end = DateTime.Now;

                Console.WriteLine($"Bad Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                Console.WriteLine($"GoodHash, {infos.Count} elements, {infosGood.Count - infosGoodLook.Count} collisions");
                Console.WriteLine($"BadHash, {infos.Count} elements, {infosBad.Count - infosBadLook.Count} collisions");
            };

            using (var db = new DataContext20000())
            {
                List<BaseInfo> infos = db.Info.ToList();
                List<InfoGood> infosGood = infos.Select(x => new InfoGood(x)).ToList();
                List<InfoBad> infosBad = infos.Select(x => new InfoBad(x)).ToList();

                Lookup<UInt32, InfoGood> infosGoodLook = (Lookup<UInt32, InfoGood>)infosGood.ToLookup(x => x.Hash, x => x);
                Lookup<UInt32, InfoBad> infosBadLook = (Lookup<UInt32, InfoBad>)infosBad.ToLookup(x => x.Hash, x => x);

                InfoGood findGood = infosGood[random.Next(infosGood.Count)];

                DateTime start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                findGood = infosGood[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                DateTime end = DateTime.Now;

                Console.WriteLine($"Good Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                InfoBad findBad = infosBad[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosBadLook, findBad);

                end = DateTime.Now;

                Console.WriteLine($"Bad Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                Console.WriteLine($"GoodHash, {infos.Count} elements, {infosGood.Count - infosGoodLook.Count} collisions");
                Console.WriteLine($"BadHash, {infos.Count} elements, {infosBad.Count - infosBadLook.Count} collisions");
            };

            using (var db = new DataContext40000())
            {
                List<BaseInfo> infos = db.Info.ToList();
                List<InfoGood> infosGood = infos.Select(x => new InfoGood(x)).ToList();
                List<InfoBad> infosBad = infos.Select(x => new InfoBad(x)).ToList();

                Lookup<UInt32, InfoGood> infosGoodLook = (Lookup<UInt32, InfoGood>)infosGood.ToLookup(x => x.Hash, x => x);
                Lookup<UInt32, InfoBad> infosBadLook = (Lookup<UInt32, InfoBad>)infosBad.ToLookup(x => x.Hash, x => x);

                InfoGood findGood = infosGood[random.Next(infosGood.Count)];

                DateTime start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                findGood = infosGood[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                DateTime end = DateTime.Now;

                Console.WriteLine($"Good Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                InfoBad findBad = infosBad[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosBadLook, findBad);

                end = DateTime.Now;

                Console.WriteLine($"Bad Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                Console.WriteLine($"GoodHash, {infos.Count} elements, {infosGood.Count - infosGoodLook.Count} collisions");
                Console.WriteLine($"BadHash, {infos.Count} elements, {infosBad.Count - infosBadLook.Count} collisions");
            };

            using (var db = new DataContext60000())
            {
                List<BaseInfo> infos = db.Info.ToList();
                List<InfoGood> infosGood = infos.Select(x => new InfoGood(x)).ToList();
                List<InfoBad> infosBad = infos.Select(x => new InfoBad(x)).ToList();

                Lookup<UInt32, InfoGood> infosGoodLook = (Lookup<UInt32, InfoGood>)infosGood.ToLookup(x => x.Hash, x => x);
                Lookup<UInt32, InfoBad> infosBadLook = (Lookup<UInt32, InfoBad>)infosBad.ToLookup(x => x.Hash, x => x);

                InfoGood findGood = infosGood[random.Next(infosGood.Count)];

                DateTime start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                findGood = infosGood[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                DateTime end = DateTime.Now;

                Console.WriteLine($"Good Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                InfoBad findBad = infosBad[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosBadLook, findBad);

                end = DateTime.Now;

                Console.WriteLine($"Bad Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                Console.WriteLine($"GoodHash, {infos.Count} elements, {infosGood.Count - infosGoodLook.Count} collisions");
                Console.WriteLine($"BadHash, {infos.Count} elements, {infosBad.Count - infosBadLook.Count} collisions");
            };

            using (var db = new DataContext80000())
            {
                List<BaseInfo> infos = db.Info.ToList();
                List<InfoGood> infosGood = infos.Select(x => new InfoGood(x)).ToList();
                List<InfoBad> infosBad = infos.Select(x => new InfoBad(x)).ToList();

                Lookup<UInt32, InfoGood> infosGoodLook = (Lookup<UInt32, InfoGood>)infosGood.ToLookup(x => x.Hash, x => x);
                Lookup<UInt32, InfoBad> infosBadLook = (Lookup<UInt32, InfoBad>)infosBad.ToLookup(x => x.Hash, x => x);

                InfoGood findGood = infosGood[random.Next(infosGood.Count)];

                DateTime start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                findGood = infosGood[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                DateTime end = DateTime.Now;

                Console.WriteLine($"Good Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                InfoBad findBad = infosBad[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosBadLook, findBad);

                end = DateTime.Now;

                Console.WriteLine($"Bad Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                Console.WriteLine($"GoodHash, {infos.Count} elements, {infosGood.Count - infosGoodLook.Count} collisions");
                Console.WriteLine($"BadHash, {infos.Count} elements, {infosBad.Count - infosBadLook.Count} collisions");
            };

            using (var db = new DataContext100000())
            {
                List<BaseInfo> infos = db.Info.ToList();
                List<InfoGood> infosGood = infos.Select(x => new InfoGood(x)).ToList();
                List<InfoBad> infosBad = infos.Select(x => new InfoBad(x)).ToList();

                Lookup<UInt32, InfoGood> infosGoodLook = (Lookup<UInt32, InfoGood>)infosGood.ToLookup(x => x.Hash, x => x);
                Lookup<UInt32, InfoBad> infosBadLook = (Lookup<UInt32, InfoBad>)infosBad.ToLookup(x => x.Hash, x => x);

                InfoGood findGood = infosGood[random.Next(infosGood.Count)];

                DateTime start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                findGood = infosGood[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                DateTime end = DateTime.Now;

                Console.WriteLine($"Good Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                InfoBad findBad = infosBad[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosBadLook, findBad);

                end = DateTime.Now;

                Console.WriteLine($"Bad Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                Console.WriteLine($"GoodHash, {infos.Count} elements, {infosGood.Count - infosGoodLook.Count} collisions");
                Console.WriteLine($"BadHash, {infos.Count} elements, {infosBad.Count - infosBadLook.Count} collisions");
            };

            using (var db = new DataContext300000())
            {
                List<BaseInfo> infos = db.Info.ToList();
                List<InfoGood> infosGood = infos.Select(x => new InfoGood(x)).ToList();
                List<InfoBad> infosBad = infos.Select(x => new InfoBad(x)).ToList();

                Lookup<UInt32, InfoGood> infosGoodLook = (Lookup<UInt32, InfoGood>)infosGood.ToLookup(x => x.Hash, x => x);
                Lookup<UInt32, InfoBad> infosBadLook = (Lookup<UInt32, InfoBad>)infosBad.ToLookup(x => x.Hash, x => x);

                InfoGood findGood = infosGood[random.Next(infosGood.Count)];

                DateTime start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                findGood = infosGood[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosGoodLook, findGood);

                DateTime end = DateTime.Now;

                Console.WriteLine($"Good Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                InfoBad findBad = infosBad[random.Next(infosGood.Count)];

                start = DateTime.Now;

                SearchElement(infosBadLook, findBad);

                end = DateTime.Now;

                Console.WriteLine($"Bad Hash, {infos.Count} elements, {(end - start).TotalMilliseconds} time to find element");

                Console.WriteLine($"GoodHash, {infos.Count} elements, {infosGood.Count - infosGoodLook.Count} collisions");
                Console.WriteLine($"BadHash, {infos.Count} elements, {infosBad.Count - infosBadLook.Count} collisions");
            };
        }
    }
}