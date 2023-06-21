using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PreMatriculasParanoa.Domain.Models.Base
{
    public class CommandResult
    {
        public CommandResult()
        {
        }

        public CommandResult(int statusCode)
        {
            StatusCode = statusCode;
        }

        public CommandResult(int statusCode, string error)
        {
            StatusCode = statusCode;
            SetError(error);
        }

        public CommandResult(int statusCode, object data)
        {
            StatusCode = statusCode;
            Data = data;
        }

        public CommandResult(int statusCode, object data, string error)
        {
            StatusCode = statusCode;
            Data = data;
            SetError(error);
        }

        public CommandResult(Exception exception, object data = null)
        {
            StatusCode = 500;
            Data = data;
            SetExceptionError(exception);
        }

        public object Data { get; set; }
        public int StatusCode { get; set; }
        public ICollection<string> Errors { get; private set; } = new List<string>();
        public bool HasError => Errors.Any();

        public void SetError(string message) 
        {
            if(!string.IsNullOrEmpty(message))
                Errors.Add(message);
        }

        public void SetErrors(List<string> messages)
        {
            foreach (var m in messages)
            {
                SetError(m);
            }
        }

        public void SetExceptionError(Exception ex)
        {
            var log = $"\n\r" +
                $"***** [MESSAGE] *****\n\r" +
                $"{ex.Message}\n\r\n\r" +
                $"***** [INNER EXCEPTION] *****\n\r" +
                $"{ex.InnerException?.Message}\n\r";

            if (Data != null)
                log += $"\n\r" +
                    $"***** [DADOS DA REQUISICAO] ***** \n\r " +
                    $"{JsonConvert.SerializeObject(Data)}\n\r\n\r";

            Console.WriteLine(log);
            SetError(log);
        }
    }
}
