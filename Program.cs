namespace SeqCipher 
{
    static class Program 
    {
        static void Main(string[] args) 
        {
            Console.WriteLine("Enter the command");
            Cipher test = new Cipher(Console.ReadLine());
            test.printQuad();
            test.printStream();
            Console.WriteLine(test.getMessage() + " -> " + test.decrypt());
        }
    }
    class Cipher
    {
        private string message;
        struct indexOfElement
        {
            public indexOfElement(int a, int b) 
            {
                x = a;
                y = b;
            }
            public int x;
            public int y;
        }
        int[,] magicQuad = { { 8, 11, 14, 1 }, { 13, 2, 7, 12 }, { 3, 16, 9, 6 }, { 10, 5, 4, 15 } };
        List<char[,]> stream = new List<char[,]>();
        public Cipher(string _message)
        {
            createStream(_message);
            message = "";
            message = encrypt(_message);
        }
        private string encrypt(string toEncrypt)
        {
            string buff = "";
            for (int s = 0; s < stream.Count; s++)
            {
                int num = 1;
                for (int i = s*16; i < toEncrypt.Length; i++)
                {
                    replaceElement(num, toEncrypt[i], s);
                    num++;
                }
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        buff += stream[s][i, j];
                    }
                }
            }
            return buff;
        }
        public void printQuad()
        {
            for(int j = 0; j< 4; j++) 
                {
                Console.WriteLine(magicQuad[j, 0] + "   " + magicQuad[j, 1] + "   " + magicQuad[j, 2] + "   " + magicQuad[j, 3] + "\n");
            }
        }
        private indexOfElement getIndexOfElement(int num, int[,] matrix)
        {
            indexOfElement buff = new indexOfElement(0, 0);
            for (int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++) 
                {
                    if(magicQuad[i, j] == num) 
                    {
                        buff.x = i;
                        buff.y = j;
                        break;
                    }
                    else 
                    {
                        buff.x = -1;
                        buff.y = -1;
                    }
                }
                if(buff.x != -1) 
                { 
                    break;
                }
            }
            return buff;
        }
        public string getMessage()
        {
            return message;
        }
        public string decrypt()
        {
            string buff = "";
            for (int s = 0; s < stream.Count; s++)
            {
                int num = 1;
                for (int i = s*16; i < message.Length; i++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        indexOfElement index = getIndexOfElement(num, magicQuad);
                        if (index.x != -1)
                        {
                            buff += stream[s][index.x, index.y];
                            break;
                        }
                    }
                    num++;
                }
            }
            return buff;
        }
        private void replaceElement(int num, char element, int streamNum) 
        {
            for (int k = 0; k < 4; k++)
            {
                indexOfElement index = getIndexOfElement(num, magicQuad);
                if (index.x != -1)
                {
                    stream[streamNum][index.x, index.y] = element;
                    break;
                }
            }
        }
        private void createStream(string message) 
        {
            int blocks = (int)Math.Ceiling((double)message.Length / 16);
            for(int i = 0; i < blocks; i++) 
            {
                stream.Add(new char[4, 4] { { '.', '.', '.', '.' }, { '.', '.', '.', '.' }, { '.', '.', '.', '.' }, { '.', '.', '.', '.' } });
            }
        }
        public void printStream() 
        {
            Console.WriteLine("=============\n");
            for (int i = 0; i < stream.Count; i++) 
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine(stream[i][j, 0] + "   " + stream[i][j, 1] + "   " + stream[i][j, 2] + "   " + stream[i][j, 3] + "\n");
                }
                Console.WriteLine("=============\n");
            }
        }
    }
}