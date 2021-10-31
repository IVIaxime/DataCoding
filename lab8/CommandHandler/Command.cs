using System;
using System.Collections.Generic;


namespace coding.CommandHadler
{

    public class Command
    {
        public delegate void Executable(List<string> commandArgs);

        private Executable execution;
        private string commandName, description;
        private readonly List<string> argsDescription;


        public Command(string commandName, string commandDescription, Executable execution)
        {
            CommandFunction = execution;
            CommandName = commandName;
            Description = commandDescription;
            argsDescription = new List<string>();
        }

        public Command(string commandName, string commandDescription, List<string> argsDescription, Executable execution)
        : this(commandName, commandDescription, execution)
        {
            if (argsDescription == null)
                throw new ArgumentNullException();
            this.argsDescription.AddRange(argsDescription);
        }


        public void Execute(List<string> commandArgs)
        {
            execution(commandArgs);
        }


        public Executable CommandFunction
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                execution = value;
            }
        }

        public string CommandName
        {
            get
            {
                return commandName;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                commandName = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                description = value;
            }
        }

        public List<string> ArgsDescription
        {
            get
            {
                return new List<string>(argsDescription);
            }
        }

        public void InsertArgDescription(string argDescription, int index)
        {
            if (argDescription == null)
                throw new ArgumentNullException();
            argsDescription.Insert(index, argDescription);
        }

        public void RemoveArgDescription(int index)
        {
            argsDescription.RemoveAt(index);
        }
    }

}
