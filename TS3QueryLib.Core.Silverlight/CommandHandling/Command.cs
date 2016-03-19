using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TS3QueryLib.Core.CommandHandling
{
    public class Command
    {
        #region Properties

        public string Name { get; protected set; }
        protected List<string> Options { get; set; }
        protected CommandParameterGroup ParameterGroups { get; set; }

        #endregion

        #region Constructors

        public Command(Server.CommandName commandName, params string[] options): this(commandName.ToString(), options)
        {

        }

        public Command(Client.CommandName commandName, params string[] options): this(commandName.ToString(), options)
        {

        }

        public Command(Common.SharedCommandName commandName, params string[] options): this(commandName.ToString(), options)
        {

        }

        public Command(string commandName, params string[] options)
        {
            if (commandName.IsNullOrTrimmedEmpty())
                throw new ArgumentException("commandName is null or emtpy", "commandName");

            Name = commandName;
            ParameterGroups = new CommandParameterGroup();
            Options = new List<string>();

            if (options != null && options.Length > 0)
            {
                foreach (string option in options)
                {
                    AddOption(option);
                }
            }
        }

        public Command(CommandParameterGroup commandWithParams)
        {
            if (commandWithParams == null)
                throw new ArgumentException("commandWithParams is null or emtpy", "commandWithParams");

            Name = commandWithParams[0].Name.ToLower();

            commandWithParams.RemoveAt(0);

            ParameterGroups = commandWithParams;
            Options = new List<string>();
        }

        #endregion

        #region Public Methods

        public void AddParameter(string parameterName)
        {
            ParameterGroups.Add(new CommandParameter(parameterName));
        }

        public void AddParameter(string parameterName, string parameterValue)
        {
            //ParameterGroups.AddParameter(parameterName, parameterValue, 0);
            ParameterGroups.Add(new CommandParameter(parameterName, parameterValue));
        }

        public void AddParameter(string parameterName, string parameterValue, uint? groupIndex)
        {
            //ParameterGroups.AddParameter(parameterName, parameterValue, groupIndex ?? 0);
        }

        public void AddParameter(string parameterName, int parameterValue)
        {
            AddParameter(parameterName, parameterValue.ToString(), 0);
        }

        public void AddParameter(string parameterName, int parameterValue, uint? groupIndex)
        {
            AddParameter(parameterName, parameterValue.ToString(), groupIndex ?? 0);
        }

        public void AddParameter(string parameterName, uint parameterValue)
        {
            AddParameter(parameterName, parameterValue.ToString(), 0);
        }

        public void AddParameter(string parameterName, uint parameterValue, uint? groupIndex)
        {
            AddParameter(parameterName, parameterValue.ToString(), groupIndex ?? 0);
        }

        public void AddParameter(string parameterName, ulong parameterValue)
        {
            AddParameter(parameterName, parameterValue.ToString(), 0);
        }

        public void AddParameter(string parameterName, ulong parameterValue, uint? groupIndex)
        {
            AddParameter(parameterName, parameterValue.ToString(), groupIndex ?? 0);
        }

        public void AddParameter(string parameterName, bool parameterValue)
        {
            AddParameter(parameterName, parameterValue ? "1" : "0", 0);
        }

        public void AddParameter(string parameterName, bool parameterValue, uint? groupIndex)
        {
            AddParameter(parameterName, parameterValue ? "1" : "0", groupIndex ?? 0);
        }

        public void AddParameter(string parameterName, char parameterValue)
        {
            AddParameter(parameterName, parameterValue.ToString(), 0);
        }

        public void AddParameter(string parameterName, char parameterValue, uint? groupIndex)
        {
            AddParameter(parameterName, parameterValue.ToString(), groupIndex ?? 0);
        }

        public void AddOption(string optionName)
        {
            Options.Add(optionName.ToLower());
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(Name.ToLower());

            if (ParameterGroups.Count > 0)
                result.AppendFormat(" {0}", ParameterGroups);

            if (Options.Count > 0)
                result.AppendFormat(" {0}", string.Join(" ", Options.Select(o => o.ToString()).ToArray()));

            return result.ToString();
        }

        #endregion

        #region Conversion Overloading

        public static implicit operator string(Command command)
        {
            return (command == null) ? null : command.ToString();
        }

        #endregion
    }
}