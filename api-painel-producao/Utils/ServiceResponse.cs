using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_painel_producao.Utils {

    public class ServiceResponse<T> {

        public bool Success { get; set; } = true;

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; } = default;


        public ServiceResponse (bool success, string message, T? data = default) {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ServiceResponse<T> Ok(T data, string message = "Operação bem-sucedida.") {
            return new ServiceResponse<T>(true, message, data);
        }

        public static ServiceResponse<T> Fail(string message) {
            return new ServiceResponse<T>(false, message);
        }

        public ServiceResponse() { }
    }
}
