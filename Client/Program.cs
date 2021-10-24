using System;
using Microchain;

namespace Client
{
    class Program
    {
        private Microchain.Microchain chain;
        public static String header =
         "___  ____                _____ _           _       \n" +
         "|  \\/  (_)              /  __ \\ |         (_)      \n" +
         "| .  . |_  ___ _ __ ___ | /  \\/ |__   __ _ _ _ __  \n" +
         "| |\\/| | |/ __| '__/ _ \\| |   | '_ \\ / _` | | '_ \\ \n" +
         "| |  | | | (__| | | (_) | \\__/\\ | | | (_| | | | | |\n" +
        "\\_|  |_/_|\\___|_|  \\___/ \\____/_| |_|\\__,_|_|_| |_|\n\n";



        private void showChain(){

            foreach(Block block in chain.GetBlocks()){
                Console.WriteLine($"{block.id}.\n{block.value.ToString()}\n");
            }
        }
        private void addBlock(){
            Console.Write("data:");
            String data = Console.ReadLine();
            chain.createBlock(data);
        }
        public int Promp(){
            int result = 0;
            Console.Write("⛓️> ");
            String command = Console.ReadLine();
            switch (command)
            {
                case "test":
                    Console.WriteLine("✔️ OK");
                    break;
                case "show":
                    showChain();
                    break;
                case "add":
                    addBlock();
                    break;
                case "exit":
                    Console.WriteLine("GoodBye 👋");
                    result = -1;
                    break;
                default:
                    Console.WriteLine($"ILEGAL COMMAND {command}");
                    break;
            }
            return result;
        }

        public Program(){
            chain = new Microchain.Microchain();
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine(header);
            int result = 0;   
            do
            {
                 result = program.Promp();
            }while(result != -1);

        }
    }
}
