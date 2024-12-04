using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_painel_producao.Utils {

    public class ServiceResponse<T> {

        public bool Success { get; set; } = true;

        public string Message { get; set; } = string.Empty;

        public bool PermissionDenied { get; set; } = false;

        public T? Data { get; set; } = default;


        private ServiceResponse (bool success, string message, T? data = default) {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ServiceResponse<T> Ok (T data, string message = "Operação bem-sucedida.") {
            return new ServiceResponse<T>(true, message, data);
        }

        public static ServiceResponse<T> Fail (string message) {
            return new ServiceResponse<T>(false, message);
        }

        public static ServiceResponse<T> DenyPermission (string message = "Action failed: You do not have the required permissions.") { 
            return new ServiceResponse<T>(false, message) { PermissionDenied = true };
        }

        private ServiceResponse() { }
    }
}
