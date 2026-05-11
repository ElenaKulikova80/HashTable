using System.Text;

namespace HashTable
{
    internal class Program
    {
        private static int GetCRC(byte[] message)
        {
            int CRC = 0xFFFF;
            for (int p = 0; p < message.Length; p++)
            {
                CRC ^= (int)message[p];
                for (int i = 8; i != 0; i--)
                {
                    if ((CRC & 0x0001) != 0)
                    {
                        CRC >>= 1;
                        CRC ^= 0xA001;
                    }
                    else
                        CRC >>= 1;
                }
            }
            return CRC;
        }

        static void Main(string[] args)
        {
            const int size = 17;
            string[] key = { "lion", "tiger", "bear", "crow", "frog", "swan", "bee", "bat", "deer", "parrot" };
            string[] value = { "gold", "orange", "brown", "black", "green", "white", "yellow", "black", "brown", "green" };
            
            List<(string, string)>[] array = new List<(string, string)>[size];
            
            for (int i = 0; i < key.Length; i++)
            {
                byte[] msg = Encoding.ASCII.GetBytes(key[i]);
                int CRC = GetCRC(msg);
                int hash = CRC % size;
                if (array[hash] == null)
                    array[hash] = new List<(string, string)>();
                array[hash].Add((key[i], value[i]));
            }

            for (int i = 0; i < size; i++)
            {
                Console.Write(i.ToString() + ": ");
                if (array[i] != null)
                    foreach ((string, string) item in array[i])
                        Console.Write(item.Item1 + " - " + item.Item2 + "\t");
                Console.WriteLine();
            }
            Console.ReadKey();

        }
    }
}
