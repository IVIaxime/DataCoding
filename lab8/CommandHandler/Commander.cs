using System;
using System.Collections.Generic;


namespace lab8.CommandHadler
{

    public class Commander
    {
        private List<Command> commands;


        public Commander(List<Command> commands)
        {
            if (commands == null)
                throw new ArgumentNullException();
            foreach (Command command in commands)
                AddCommand(command);
        }

        public Commander()
        {
            commands = new List<Command>();
        }


        public void ExecuteCommand(string command)
        {
            command += " ";
            List<string> arguments = new List<string>(command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            if (arguments.Count == 0)
                throw new ArgumentException("Неверно введена команда");

            string commandName = arguments[0];
            arguments.RemoveAt(0);
            GetBy(commandName, arguments.Count).Execute(arguments);
        }

        public Command GetBy(string commandName, int argsCount)
        {
            foreach (Command command in commands)
            {
                if (command.CommandName == commandName && argsCount == command.ArgsDescription.Count)
                    return command;
            }

            throw new ArgumentException("Команды " + commandName + " не существует");
        }

        public bool Exists(string commandName, int argsCount)
        {
            foreach (Command command in commands)
            {
                if (command.CommandName == commandName && argsCount == command.ArgsDescription.Count)
                    return true;
            }

            return false;
        }

        public void AddCommand(Command command)
        {
            if (command == null)
                throw new ArgumentNullException();
            if (Exists(command.CommandName, command.ArgsDescription.Count))
                throw new ArgumentException("Команда с такими параметрами уже существует");
            commands.Add(command);
        }

        public List<Command> GetCommands()
        {
            return new List<Command>(commands);
        }
    }

}
