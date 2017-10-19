using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEE.Framework.Core.Common
{
    public class CommandResult
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public int ErrorCode { get; set; } = 0;

        public bool Succeeded
        {
            get
            {
                return string.IsNullOrWhiteSpace(ErrorMessage);
            }
        }
        public bool Failed
        {
            get
            {
                return !string.IsNullOrWhiteSpace(ErrorMessage);
            }
        }

        public CommandResult()
        {
            ErrorMessage = string.Empty;
        }

        public CommandResult(Exception exception)
        {
            ErrorMessage = exception.Message;
        }

        public CommandResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public static CommandResult Ok
        {
            get
            {
                return new CommandResult();
            }
        }
    }

    public class CommandResult<T> : CommandResult
    {
        public T Value { get; set; }

        public CommandResult()
            :base()
        {

        }
        public CommandResult(T value)
            :this()
        {
            Value = value;
        }

        public CommandResult(T value, string errorMessage)
        {
            Value = value;
            ErrorMessage = errorMessage;
        }
        public CommandResult(T value, Exception exception)
            : base(exception)
        {
            Value = value;
        }
    }
}
