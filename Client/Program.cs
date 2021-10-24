using System;
using System.Text;
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
                Console.WriteLine($"{block.id}.\n{block.value.ToString()}\n🔗");
            }
        }
        private void addBlock(){
            Console.Write("data: ");
            String data = Console.ReadLine();
            chain.createBlock(data);
        }
        private void save(){
            Console.Write("file name: ");
            String name = Console.ReadLine();
            chain.SaveToFile(name);
        }
        private void load(){
            Console.Write("file name: ");
            String name = Console.ReadLine();
            chain.LoadFromFile(name);
        }
        public int Promp(){
            int result = 0;
            Console.Write("⛓️> ");
            String[] command = Console.ReadLine().Trim().Split();
            switch (command[0])
            {
                case "test":
                    Console.WriteLine("✔️ OK");
                    break;
                case "echo":
                    if (command.Length >= 2) Console.WriteLine(command[1]);
                    break;
                case "show":
                    showChain();
                    break;
                case "add":
                    if (command.Length >= 2) chain.createBlock(command[1]);
                    else addBlock();
                    break;
                case "check":
                    Console.WriteLine($"Integrity: {(chain.check() ? "✔️": '❌' )}");
                    break;
                case "exit":
                    Console.WriteLine("GoodBye 👋");
                    result = -1;
                    break;
                case "clear":
                case "cls":
                    Console.Clear();
                    break;
                case "save":
                    if (command.Length >= 2) chain.SaveToFile(command[1]);
                    else save();
                    break;
                case "load":
                    if (command.Length >= 2) chain.LoadFromFile(command[1]);
                    else load();
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
            Console.OutputEncoding = Encoding.UTF8;
            Program program = new Program();
            Console.WriteLine(header);
            int result = 0;   
            do
            {
                try{
                 result = program.Promp();
                }catch(Exception){
                    Console.WriteLine("💣ERROR");
                }
            }while(result != -1);

        }
    }
}
