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
        protected CommandParameterGroupList ParameterGroups { get; set; }

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
            ParameterGroups = new CommandParameterGroupList();
            Options = new List<string>();

            if (options != null && options.Length > 0)
            {
                foreach (string option in options)
                {
                    AddOption(option);
                }
            }
        }

        public Command(CommandParameterGroupList commandWithParams)
        {
            if (commandWithParams == null)
                throw new ArgumentException("commandWithParams is null or emtpy", "commandWithParams");

            Name = commandWithParams[0][0].Name;

            commandWithParams.ForEach(cmdWithParam => cmdWithParam.RemoveAt(0));

            ParameterGroups = commandWithParams;
            Options = new List<string>();
        }

        #endregion

        #region Public Methods

        public void AddParameter(string parameterName)
        {
            ParameterGroups.AddParameter(parameterName, null, 0);
        }

        public void AddParameter(string parameterName, string parameterValue)
        {
            ParameterGroups.AddParameter(parameterName, parameterValue, 0);
        }

        public void AddParameter(string parameterName, string parameterValue, uint? groupIndex)
        {
            ParameterGroups.AddParameter(parameterName, parameterValue, groupIndex ?? 0);
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
            return ToString(false);
        }

        public string ToString(bool appendDash)
        {
            //Legacy code appended dashes for Options array.. Leaving flexible functionality in just in case.
            string extraChars = "";

            if (appendDash)
                extraChars = "-";

            StringBuilder result = new StringBuilder();
            result.Append(Name.ToLower());

            if (ParameterGroups.Count > 0)
                result.AppendFormat(" {0}{1}", extraChars, ParameterGroups);

            if (Options.Count > 0)
                result.AppendFormat(" {0]{1}", extraChars, string.Join(" -", Options.Select(o => o.ToString()).ToArray()));

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